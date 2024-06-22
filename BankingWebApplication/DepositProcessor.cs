using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using BankingWebApplication.Models;
using BankingWebApplication.Controllers;

namespace BankingWebApplication
{
    public class DepositProcessor
    {

        private static readonly ConcurrentQueue<DepositViewModel> depositQueue = AccountsApiController.GetDepositQueue();
        private static readonly ConcurrentQueue<WithdrawViewModel> withdrawQueue = AccountsApiController.GetWithdrawQueue();
        private static readonly ConcurrentQueue<TransferFunds> transferFundsQueue = AccountsApiController.GetTransferFundsQueue();
        private static readonly KirJemBankingEntities db = new KirJemBankingEntities();
        private static bool isRunning = false;

        public static void StartProcessing()
        {
            if (isRunning) return;
            isRunning = true;

            Task.Run(() =>
            {
                while (isRunning)
                {
                    ProcessDeposits();
                    Thread.Sleep(1000); // Process deposits every second
                }
            });
        }

        public static void StopProcessing()
        {
            isRunning = false;
        }

        private static void ProcessDeposits()
        {
            int batchSize = 2; // Number of deposits to process per second
            for (int i = 0; i < batchSize; i++)
            {
                if (depositQueue.TryDequeue(out var deposit))
                {
                    var account = db.Accounts.FirstOrDefault(a => a.AccountNumber == deposit.AccountNumber);
                    if (account != null)
                    {
                        account.Balance += deposit.Amount;
                        db.Entry(account).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                if (withdrawQueue.TryDequeue(out var withdraw))
                {
                    var account = db.Accounts.FirstOrDefault(a => a.AccountNumber == withdraw.AccountNumber);
                    if (account != null)
                    {
                        account.Balance -= withdraw.Amount;
                        db.Entry(account).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                if (transferFundsQueue.TryDequeue(out var transfer))
                {
                    var accountTo = db.Accounts.FirstOrDefault(a => a.AccountNumber == transfer.ReceiverAccountNumber);
                    var accountFrom = db.Accounts.FirstOrDefault(a => a.AccountNumber == transfer.SenderAccountNumber);

                    if ((accountTo!= null) && (accountFrom!= null))
                    {
                        accountFrom.Balance -= transfer.Amount;
                        accountTo.Balance += transfer.Amount;
                        db.Entry(accountFrom).State = System.Data.Entity.EntityState.Modified;
                        db.Entry(accountTo).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    break; // Exit the loop if the queues are empty
                }
            }
        }
    }
}
