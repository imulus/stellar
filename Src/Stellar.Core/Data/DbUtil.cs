using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace Stellar.Core.Data
{
    public class DbUtil
    {

        public static Database Connection 
        {
            get 
            {
                return new Database("stellarRatings");
            }
        }

    }
}
