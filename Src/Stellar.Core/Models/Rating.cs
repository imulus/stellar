using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PetaPoco;
using Stellar.Core.Data;

namespace Stellar.Core.Models
{
    public class Rating
    {

        public int NodeId { get; set; }

        public IEnumerable<RatingEntry> RatingEntries { get; set; }

        public decimal Score { get { return CalculateScore(); } }

        #region Static Methods

        public static bool ExistsForNodeId(int nodeId)
        {
            return RatingEntry.GetAllForNodeId(nodeId).Any();
        }

        public static Rating GetForNodeId(int nodeId)
        {
            var nodeRatings = RatingEntry.GetAllForNodeId(nodeId);
            return new Rating()
                       {
                           NodeId = nodeId,
                           RatingEntries = nodeRatings
                       };
        }

        public static bool UserHasRated(HttpContext context, int nodeId)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["rated" + nodeId];
            return cookie != null;
        }

        #endregion

        #region Private Methods

        private decimal CalculateScore()
        {
            var entries = RatingEntry.GetAllForNodeId(NodeId);

            if (!entries.Any())
                return 0;

            var avg = entries.Average(x=>x.Value);
            return Math.Round(avg, 2);
        }

        #endregion
    }
}
