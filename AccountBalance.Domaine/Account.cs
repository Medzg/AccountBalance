using AccountBalance.Domaine.ValueObjects.Cash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBalance.Domaine
{
    public enum State{Blocked , Active};
    public class Account
    {
        
        public Guid Id { get; private set; }
        public string AccountName { get; private set; }

        public Cash AccountCash { get; private set; }
     
        public State AccountState { get; private set; } 

        public Account(string accountName , decimal blance ,  decimal overdraftLimit = 0 ,decimal dailyWireTransferLimit = 0)
        {
            if (string.IsNullOrEmpty(accountName))
                throw new InvalidOperationException("Name account is required");
            Id = Guid.NewGuid();
            AccountName = accountName;
            AccountCash = new Cash(blance, overdraftLimit, dailyWireTransferLimit);

          
        }


        public void DepositeCash(decimal amount)
        {
            if (amount < 0)
                throw new InvalidOperationException("You can't deposite negative amount");
            AccountCash += new Cash(amount);
            if (AccountState == State.Blocked && AccountCash.Balance >= 0)
                AccountState = State.Active;

        }


        public void ChangeDailyWireTransferLimit(decimal wireTransfer_value)
        {
            AccountCash = Cash.SetDailyWireTransferLimit(AccountCash, wireTransfer_value);
        }

        public void ChangeOverdraftLimit(decimal overdraft_value)
        {

            AccountCash = Cash.SetOverdraftLimit(AccountCash, overdraft_value);
          
        }


    }
}
