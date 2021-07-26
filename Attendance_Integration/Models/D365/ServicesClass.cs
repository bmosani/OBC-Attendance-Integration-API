using System;
using System.IO;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Attendance_Integration.Models.D365
{
    public class ServicesClass
    {
        public static object GetXmlFromObject(object o)
        {
            var stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = null;
            try
            {
                var xmlSerializer = new XmlSerializer(o.GetType());
                xmlTextWriter = new XmlTextWriter(stringWriter);
                xmlSerializer.Serialize(xmlTextWriter, o);
            }
            catch (Exception ex)
            {
                // ignored
            }
            finally
            {
                stringWriter.Close();
                xmlTextWriter?.Close();
            }

            return stringWriter.ToString();
        }

        public static string RequestBody()
        {
            var streamReader = new StreamReader(HttpContext.Current.Request.InputStream);
            streamReader.BaseStream.Seek(0L, SeekOrigin.Begin);
            return streamReader.ReadToEnd();
        }
    }
}