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
            ViewBag.Showlist = false;
            if(HttpContext.Request.Query["accno"].ToString() !="")
            {
                string AccountNumber = HttpContext.Request.Query["accno"].ToString();
                accountNumber = AccountNumber;
            }

            if(accountNumber !=null)
            {
                ViewBag.ShowList = true;
                ViewBag.AccountNumber = accountNumber;
                ViewBag.TotalBalance = getTotalBalance(accountNumber).ToString();
                var result = context.transactions.Where(i => i.accountNumber == accountNumber);
                    if(result !=null)
                {
                    List<TransactionList> Tlist = new List<TransactionList>();
                    foreach (var data in result)
                    {
                        Tlist.Add(new TransactionList
                        {
                            accountNumber = data.accountNumber,
                            transactionType = data.transactionType,
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
        public decimal getTotalBalance(string accountNumber)
        {
            //decimal totalBalance = 0;
            //var result = (from t in ds.tbltransactions orderby t.balance select t).Take(1);
            decimal balance = 0;
            try
            {
                var result = (from t in context.transactions
                              where t.accountNumber == accountNumber
                              orderby t.id descending
                              select t).First();
                balance = decimal.Parse(result.balance.ToString());
            }
            catch (Exception e)
            {
                balance = 0;
            }

            ////var result = ds.tbltransactions.Where(i => i.accountno == accountno).ToList();
            //if (result != null)
            //{
            //    foreach (var dr in result)
            //    {
            //        totalBalance += Convert.ToDecimal(dr.balance);
            //    }
            //}
            //totalBalance = (from od in ds.tbltransactions where od.accountno == accountno select Convert.ToDecimal(od.balance)).Sum();
            return balance;
        }

        public IActionResult AddTransaction(Transaction transaction, string drpdowntype)
        {
            ViewBag.Showlist = false;
            string Typevalue = "";
            string totalBalance = "";

            if(HttpContext.Request.Query["accno"].ToString() !="")
            {
                string AccountNumber = HttpContext.Request.Query["accno"].ToString();
                ViewBag.lblAccountNumber = AccountNumber;
                if (transaction.amount !=null)
                {
                    if(drpdowntype=="Deposit")
                    {
                        Typevalue = "Credit";
                    }

                    else if(drpdowntype=="Withdrawal")
                    {
                        Typevalue = "Debit";
                    }
                    else if(drpdowntype=="InterestPaind")
                    {
                        Typevalue = "Debit";
                    }
                    else
                    {
                        if(transaction.transactionType=="Debit")
                        {
                            Typevalue = "Debit";
                        }
                        else
                        {
                            Typevalue = "Credit";
                        }
                    }


                    if(ModelState.IsValid)
                    {
                        Transaction transactionData = new Transaction();
                        transactionData.accountNumber = AccountNumber;
                        transactionData.description = transaction.description;
                        transactionData.modified_date = DateTime.Now;
                        transactionData.transaction_date = transaction.transaction_date;
                        transactionData.amount = transaction.amount;
                        transactionData.transactionType = Typevalue;
                        totalBalance = getTotalBalance(AccountNumber).ToString();

                        if(Typevalue=="Debit")
                        {
                            transactionData.balance = (Convert.ToDecimal(totalBalance) - (transaction.amount));
                        }
                        else
                        {
                            transactionData.balance = (Convert.ToDecimal(totalBalance) + (transaction.amount));

                        }
                        context.transactions.Add(transactionData);
                        context.SaveChanges();
                        return RedirectToAction("Viewtransaction", "transaction", new { accno = AccountNumber });
                    }
                }
            }
            return View();
        }

        public decimal getBalance(string accountNumber)
        {
            decimal balance = 0;
            try
            {
                var result = (from t in context.transactions
                              where t.accountNumber == accountNumber
                              orderby t.id descending
                              select t).First();
                balance = decimal.Parse(result.balance.ToString());
            }
            catch (Exception e)
            {
                balance = 0;
            }
            return balance;
        }
    }
}