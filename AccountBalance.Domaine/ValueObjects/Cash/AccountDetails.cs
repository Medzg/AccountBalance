using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBalance.Domaine.ValueObjects.Cash
{
    public class AccountDetails : ValueObject<AccountDetails>
    {
        public decimal Debt { get; }

        public decimal OverdraftLimit { get; }

        public decimal DailyWireTransferLimit { get; }

        public decimal WithdrawnToday { get; } 

        public AccountDetails(decimal balance ,  decimal overdraftLimit = 0,decimal dailyWireTransferLimit = 0,decimal withdrawnToday = 0)
        {
           
            if (overdraftLimit < 0)
                throw new InvalidOperationException("overdraft Limit can't be negative");
            if (dailyWireTransferLimit < 0)
                throw new InvalidOperationException("dialy Wire Transfer can't be negative");
            if (withdrawnToday < 0)
                throw new InvalidOperationException("withdrawn Today can't be negative");
            Debt = balance;
            OverdraftLimit = overdraftLimit;
            DailyWireTransferLimit = dailyWireTransferLimit;
            WithdrawnToday = withdrawnToday; 
            
            
        }
       
        public static AccountDetails SetDailyWireTransferLimit(AccountDetails AccountCash,decimal wire_transfer)
        {
            return new AccountDetails(AccountCash.Debt, AccountCash.DailyWireTransferLimit, wire_transfer, AccountCash.WithdrawnToday);

        }

        public static AccountDetails SetOverdraftLimit(AccountDetails AccountCash,decimal overdraft)
        {

            return new AccountDetails(AccountCash.Debt, overdraft, AccountCash.DailyWireTransferLimit, AccountCash.WithdrawnToday);
        }


       public static AccountDetails WithdrowMoney(AccountDetails accountDetail ,  decimal amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException("amount can't be negative");
            if (accountDetail.Debt - amount < -accountDetail.OverdraftLimit)
                throw new InvalidOperationException("over draft limit passed");
            return new AccountDetails(accountDetail.Debt - amount, accountDetail.OverdraftLimit, accountDetail.OverdraftLimit, accountDetail.WithdrawnToday);

        }


       public static  AccountDetails depositMoney(AccountDetails accountDetails, decimal amount)
        {

            if (amount < 0)
                throw new ArgumentOutOfRangeException("amount can't be negative");
            return new AccountDetails(accountDetails.Debt + amount, accountDetails.OverdraftLimit, accountDetails.DailyWireTransferLimit, accountDetails.WithdrawnToday);
        }





        protected override bool EqualsCore(AccountDetails valueObject)
        {
            return Debt == valueObject.Debt &&
                this.DailyWireTransferLimit == valueObject.DailyWireTransferLimit &&
                this.OverdraftLimit == valueObject.OverdraftLimit;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {

                int hashCode = 269;
                hashCode = (hashCode * 47) + (int)Debt;
                hashCode = (hashCode * 47) + (int)OverdraftLimit;
                hashCode = (hashCode * 47) + (int)DailyWireTransferLimit;
                return hashCode;

            }
        }
    }
}
