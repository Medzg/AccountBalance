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

        public AccountDetails AccountDetail { get; private set; }
     
        public State AccountState { get; private set; } 

        public Account(string accountName , decimal debt ,  decimal overdraftLimit = 0 ,decimal dailyWireTransferLimit = 0)
        {
            if (string.IsNullOrEmpty(accountName))
                throw new InvalidOperationException("Name account is required");
            if (debt < 0)
                throw new InvalidOperationException("account debt should be positive or 0");
            Id = Guid.NewGuid();
            AccountName = accountName;
            AccountDetail = new AccountDetails(debt, overdraftLimit, dailyWireTransferLimit);

          
        }


        public void DepositeCash(decimal amount)
        {
         
            AccountDetail = AccountDetails.depositMoney(AccountDetail, amount);
            if (AccountState == State.Blocked && AccountDetail.Debt >= 0)
                ChangeState(State.Active);

        }



        public void DepositeCheques(decimal amount)
        {

            // need to be implemented
            throw new NotImplementedException();
        }



        public void WithdrawCash(decimal amount)
        {
            try { 

               AccountDetail = AccountDetails.WithdrowMoney(AccountDetail,amount);
              }
            catch (InvalidOperationException ex)
            {
                ChangeState(State.Blocked);
                throw ex;
            }
         
        }

        private void ChangeState(State state)
        {

            AccountState = state;
        }

        public void WireTransfer(decimal amount)
        {


            try
            {
               AccountDetail = AccountDetails.WithdrowMoney(AccountDetail, amount, true);
            }
            catch (InvalidOperationException ex)
            {
                ChangeState(State.Blocked);

                throw ex;
            }
         

        }
        public void ChangeDailyWireTransferLimit(decimal wireTransfer_value)
        {
            AccountDetail = AccountDetails.SetDailyWireTransferLimit(AccountDetail, wireTransfer_value);
        }

        public void ChangeOverdraftLimit(decimal overdraft_value)
        {

            AccountDetail = AccountDetails.SetOverdraftLimit(AccountDetail, overdraft_value);
          
        }


    }
}
