using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Product_Management_Core.Services
{
    public interface IXmlService
    {
        string Serialize<T>(T data);
        T Deserialize<T>(string xml);
    }
    public class XmlService : IXmlService
    {
        public T Deserialize<T>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader reader = new StringReader(xml))
            {
                T result = (T)(serializer.Deserialize(reader));
                return result;
            }
        }

        public string Serialize<T>(T data)
        {
            var result = string.Empty;
            var xmlWriterSettings = new XmlWriterSettings
            {
                Indent = false,
                NewLineHandling = NewLineHandling.None,
                OmitXmlDeclaration = true,
                Encoding = Encoding.UTF8
            };

            var stringBuilder = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(stringBuilder, xmlWriterSettings))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                serializer.Serialize(writer, data, ns);
                result = stringBuilder.ToString();
                return result;
            }
        }
    }
}