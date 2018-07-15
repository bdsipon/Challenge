using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exercise.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exercise.Controllers
{
    public class TransactionController : Controller
    {
        private TransactionDbContext context = new TransactionDbContext();
        public IActionResult Viewtransaction(string accountNumber)
        {

            if(HttpContext.Request.Query["accno"].ToString() !="")
            {
                string AccountNumber = HttpContext.Request.Query["accno"].ToString();
                accountNumber = AccountNumber;
            }

            if(accountNumber !=null)
            {
                var result = context.transactions.Where(i => i.accountNumber == accountNumber);
                    if(result !=null)
                {
                    List<TransactionList> Tlist = new List<TransactionList>();
                    foreach (var data in result)
                    {
                        Tlist.Add(new TransactionList
                        {
                            accountNumber = data.accountNumber,
                            tranactionType = data.tranactionType,
                            amount = data.amount.ToString(),
                            balance = data.balance.ToString(),
                            description = data.description,
                            tranaction_date = data.transaction_date.ToString()
                        });

                    }
                return View(Tlist);
                }
            }
            return View();
        }
    }
}