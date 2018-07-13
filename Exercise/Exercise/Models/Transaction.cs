using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise.Models
{
    public partial class Transaction
    {
        public long id { get; set; }
        public string accountNumber { get; set; }
        public string tranactionType { get; set; }
        public Nullable<decimal> amount { get; set; }
        public Nullable<decimal> balance { get; set; }
        public string description { get; set; }
        public Nullable<DateTime> transaction_date { get; set; }
        public Nullable<DateTime> modified_date { get; set; }
    }
}
