using Newtonsoft.Json;
using Solvintech.API.Сommon;
using System.Xml;

namespace Solvintech.API.Utils
{
    public class XmlJsonUtils
    {
        public static string XmlToJson(string content)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(content);

            return xmlDoc.SelectSingleNode(Constants.Xml.Valute) == null
                ? null
                : JsonConvert.SerializeXmlNode(xmlDoc);
        }
    }
}
