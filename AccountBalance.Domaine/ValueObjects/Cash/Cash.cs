using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBalance.Domaine.ValueObjects.Cash
{
    public class Cash : ValueObject<Cash>
    {
        public decimal Balance { get; }

        public decimal OverdraftLimit { get; }

        public decimal DailyWireTransferLimit { get; }

        public decimal WithdrawnToday { get; } 

        public Cash(decimal balance ,  decimal overdraftLimit = 0,decimal dailyWireTransferLimit = 0,decimal withdrawnToday = 0)
        {
           
            if (overdraftLimit < 0)
                throw new InvalidOperationException("overdraft Limit can't be negative");
            if (dailyWireTransferLimit < 0)
                throw new InvalidOperationException("dialy Wire Transfer can't be negative");
            if (withdrawnToday < 0)
                throw new InvalidOperationException("withdrawn Today can't be negative");
            Balance = balance;
            OverdraftLimit = overdraftLimit;
            DailyWireTransferLimit = dailyWireTransferLimit;
            WithdrawnToday = withdrawnToday; 
            
            
        }
       
        public static Cash SetDailyWireTransferLimit(Cash AccountCash,decimal wire_transfer)
        {
            return new Cash(AccountCash.Balance, AccountCash.DailyWireTransferLimit, wire_transfer, AccountCash.WithdrawnToday);

        }

        public static Cash SetOverdraftLimit(Cash AccountCash,decimal overdraft)
        {

            return new Cash(AccountCash.Balance, overdraft, AccountCash.DailyWireTransferLimit, AccountCash.WithdrawnToday);
        }
        public static Cash operator +(Cash cash1 , Cash cash2)
        {
            Cash sum = new Cash(cash1.Balance + cash2.Balance, cash1.OverdraftLimit, cash1.DailyWireTransferLimit,cash1.WithdrawnToday);
            return sum;
        }
        public static Cash operator -(Cash cash1, Cash cash2)
        {
            if(Math.Abs(cash1.Balance - cash2.Balance) < cash1.OverdraftLimit)
                throw new InvalidOperationException("you can't pass pass your overdraft limit");
            if (cash2.Balance + cash1.WithdrawnToday > cash1.WithdrawnToday)
                throw new InvalidOperationException("you reach your limit today try tomrrow");
            Cash sum = new Cash(cash1.Balance - cash2.Balance, cash1.OverdraftLimit, cash1.DailyWireTransferLimit, cash1.WithdrawnToday+cash2.Balance);
            return sum;
        }
        protected override bool EqualsCore(Cash valueObject)
        {
            return Balance == valueObject.Balance &&
                this.DailyWireTransferLimit == valueObject.DailyWireTransferLimit &&
                this.OverdraftLimit == valueObject.OverdraftLimit;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {

                int hashCode = 269;
                hashCode = (hashCode * 47) + (int)Balance;
                hashCode = (hashCode * 47) + (int)OverdraftLimit;
                hashCode = (hashCode * 47) + (int)DailyWireTransferLimit;
                return hashCode;

            }
        }
    }
}
