using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;

namespace Attendance_Integration.Models.D365
{
    public class OAuthHelper
    {
        /// <summary>
        ///     The header to use for OAuth authentication.
        /// </summary>
        public const string OAuthHeader = "Authorization";

        /// <summary>
        ///     Retrieves an authentication header from the service.
        /// </summary>
        /// <returns>The authentication header for the Web API call.</returns>
        public static string GetAuthenticationHeader(bool useWebAppAuthentication = true)
        {
            var aadTenant = ClientConfiguration.Default.ActiveDirectoryTenant;
            var aadClientAppId = ClientConfiguration.Default.ActiveDirectoryClientAppId;
            var aadClientAppSecret = ClientConfiguration.Default.ActiveDirectoryClientAppSecret;
            var aadResource = ClientConfiguration.Default.ActiveDirectoryResource;

            var authenticationContext = new AuthenticationContext(aadTenant, false);
            AuthenticationResult authenticationResult;

            if (useWebAppAuthentication)
            {
                if (string.IsNullOrEmpty(aadClientAppSecret))
                {
                    Console.WriteLine(
                        "Please fill AAD application secret in ClientConfiguration if you choose authentication by the application.");
                    throw new Exception("Failed OAuth by empty application secret.");
                }

                try
                {
                    // OAuth through application by application id and application secret.
                    var creadential = new ClientCredential(aadClientAppId, aadClientAppSecret);
                    authenticationResult = authenticationContext.AcquireTokenAsync(aadResource, creadential).Result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        "Failed to authenticate with AAD by application with exception {0} and the stack trace {1}", ex,
                        ex.StackTrace);
                    throw new Exception("Failed to authenticate with AAD by application.");
                }
            }
            else
            {
                // OAuth through username and password.
                var username = ClientConfiguration.Default.UserName;
                var password = ClientConfiguration.Default.Password;

                if (string.IsNullOrEmpty(password))
                {
                    Console.WriteLine(
                        "Please fill user password in ClientConfiguration if you choose authentication by the credential.");
                    throw new Exception("Failed OAuth by empty password.");
                }

                try
                {
                    // Get token object
                    var userCredential = new UserPasswordCredential(username, password);

                    authenticationResult = authenticationContext
                        .AcquireTokenAsync(aadResource, aadClientAppId, userCredential).Result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        "Failed to authenticate with AAD by the credential with exception {0} and the stack trace {1}",
                        ex, ex.StackTrace);
                    throw new Exception("Failed to authenticate with AAD by the credential.");
                }
            }

            // Create and get JWT token
            return authenticationResult.CreateAuthorizationHeader();
        }
    }
}