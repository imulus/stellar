using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using Stellar.Core.Models;
using umbraco;

namespace Stellar.Core.Extensions
{
    [XsltExtension("stellar.library")]
    public class XsltExtensions
    {

        public static XPathNodeIterator GetEntries(int nodeId)
        {
            var entries = RatingEntry.GetAllForNodeId(nodeId);
            XmlSerializer xs = new XmlSerializer(entries.GetType());
            StringWriter sw = new StringWriter();
            xs.Serialize(sw, entries);

            XmlDocument xd = new XmlDocument();
            xd.LoadXml(sw.ToString());

            return xd.CreateNavigator().Select("//*");
        }


        public static decimal GetScore(int nodeId)
        {
            return Rating.GetForNodeId(nodeId).Score;
        }

    }
}
