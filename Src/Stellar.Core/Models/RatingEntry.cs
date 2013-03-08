using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Stellar.Core.Data;

namespace Stellar.Core.Models
{
    [PetaPoco.TableName("StellarRatingEntries")]
    [PetaPoco.PrimaryKey("Id")]
    public class RatingEntry
    {
        public delegate void RatingEntryCreatedEventHandler(RatingEntry sender, EventArgs e);

        public static event RatingEntryCreatedEventHandler RatingEntryCreated;

        protected virtual void OnRatingEntryCreated(EventArgs e)
        {
            if (RatingEntryCreated != null) RatingEntryCreated(this, e);
        }

        [Key()]
        public int Id { get; set; }

        public int NodeId { get; set; }

        public int MemberId { get; set; }

        public decimal Value { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? Created { get; set; }

        public string Text { get; set; }

        public string IpAddress { get; set; }

        public static List<RatingEntry> GetAllForNodeId(int nodeId)
        {
            var dbContext = DbUtil.Connection;
            string x = "";
            return dbContext.Query<RatingEntry>("SELECT * FROM StellarRatingEntries WHERE NodeId = @0", nodeId).ToList();
        }

        public void Save(HttpContext context)
        {
            var db = DbUtil.Connection;
            db.Insert(this);

            this.OnRatingEntryCreated(EventArgs.Empty);

            context.Response.SetCookie(new HttpCookie("rated" + NodeId, "1"));
        }

    }
}
