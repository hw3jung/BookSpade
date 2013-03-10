using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BRApplication.Models
{
    public class TransactionStatus
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }

        internal bool insert()
        {
            throw new NotImplementedException();
        }
    }
}