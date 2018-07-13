using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise.Models
{
    public class TransactionList
    {
        public string accountNumber { get; set; }
        public string tranactionType { get; set; }
        public string amount { get; set; }
        public string balance { get; set; }
        public string description { get; set; }
        public string tranaction_date { get; set; }
    }
}
