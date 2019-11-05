using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBalance.Domaine.ValueObjects.Cash
{
    public class Cheque : ValueObject<Cheque>
    {
        public decimal Amount { get; }

        public DateTime DepositTime { get; }

        public Cheque(decimal amount, DateTime? depositeTime)
        {
            if (amount < 0)
                throw new InvalidOperationException("amount can't be negative");
            if (!depositeTime.HasValue)
                throw new InvalidOperationException("Deposite date can't be null");
            Amount = amount;
            DepositTime = depositeTime.Value;


        }
        protected override bool EqualsCore(Cheque valueObject)
        {
            return Amount == valueObject.Amount &&
                 DepositTime == valueObject.DepositTime;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {

                int hashCode = 269;
                hashCode = (hashCode * 47) + (int)Amount;
                hashCode = (hashCode * 47) + DepositTime.GetHashCode();
                return hashCode;
            }
        }
    }
}
