using Attendance_Integration.DAPAttendanceServiceGroup;
using Attendance_Integration.Models.D365;
using log4net;
using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web.Http;

namespace Attendance_Integration.Controllers
{
    public class AttendanceController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AttendanceController).Name);

        [HttpPost]
        public string Post([FromBody] DAPAttendanceList[] attendanceList)
        {
            Log.Info("Fetched Records: \n" + attendanceList);
            string message;

            var appSetting = ConfigurationManager.AppSettings["Company"];
            var serviceName = "DAPAttendanceServiceGroup";
            var uriString = ClientConfiguration.Default.UriString;
            var authenticationHeader = OAuthHelper.GetAuthenticationHeader(true);
            IClientChannel innerChannel = new DAPAttendanceServicesClient
                (SoapHelper.GetBinding(), new EndpointAddress(SoapHelper.GetSoapServiceUriString(serviceName, uriString))).InnerChannel;
            try
            {

                var callContext = new CallContext
                {
                    Company = appSetting,
                    PartitionKey = ConfigurationManager.AppSettings["Partition"],
                    Language = ConfigurationManager.AppSettings["Language"]
                };
                using (new OperationContextScope(innerChannel))
                {
                    var requestMessageProperty = new HttpRequestMessageProperty();
                    requestMessageProperty.Headers["Authorization"] = authenticationHeader;
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessageProperty;
                    message = ((DAPAttendanceServices)innerChannel).CreateAttandance(new CreateAttandance(callContext, attendanceList)).result;

                    Log.Info("Date: " + DateTime.Now.Date + "\n D365 response: " + message + "\n *******************************************************");
                }
            }

            catch (Exception ex)
            {
                message = ex.Message;
                Log.Error(message);
            }

            return message;
        }
    }
}
