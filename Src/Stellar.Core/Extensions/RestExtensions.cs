using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.XPath;
using Stellar.Core.Data;
using Stellar.Core.Models;
using umbraco.cms.businesslogic.member;
using umbraco.presentation.umbracobase;

namespace Stellar.Core.Extensions
{
    [RestExtension("stellar")]
    public class RestExtensions
    {

        [RestExtensionMethod()]
        public static string AddEntry()
        {
            var nodeId = int.Parse(HttpContext.Current.Request["nodeId"]);
            var value = decimal.Parse(HttpContext.Current.Request["value"]);

            var newEntry = new RatingEntry();
            newEntry.Created = DateTime.Now;
            newEntry.IpAddress = HttpContext.Current.Request.UserHostAddress;
            newEntry.MemberId = Member.GetCurrentMember() != null ? Member.GetCurrentMember().Id : 0;
            newEntry.NodeId = nodeId;
            newEntry.Text = "";
            newEntry.Value = value;
            newEntry.Save(HttpContext.Current);


            return "1";
        }

    }
}
