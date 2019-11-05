using AccountBalance.Domaine.ValueObjects.Cash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AccountBalance.Test.Domaine.ValueObject
{
   public class AccountDetailTestSpec
    {
        [Fact]

        public void Deposite_ammount_of_money_should_added_to_dept()
        {
            // arrange
            var accountDetail = new AccountDetails(100);

            //act

            accountDetail = AccountDetails.depositMoney(accountDetail, 100);

            //assert

            Assert.Equal(200, accountDetail.Debt);


        }

        [Theory]
        [InlineData(0)]
        [InlineData(-55)]

        public void deposite_negative_amount_of_money_should_throw_exception(decimal amount)
        {

            var accountDetail = new AccountDetails(100);

            Action action = () => { accountDetail = AccountDetails.depositMoney(accountDetail, amount); };


            Assert.Throws<ArgumentOutOfRangeException>(action);


        }



        [Fact]
        public void DailyWireTransferLimit_should_update_it_value()
        {

            var accountDetail = new AccountDetails(100);

            accountDetail = AccountDetails.SetDailyWireTransferLimit(accountDetail, 500);


            Assert.Equal(100, accountDetail.Debt);

            Assert.Equal(500, accountDetail.DailyWireTransferLimit);

        }

        [Fact]

        public void DailyWireTransferLimit_should_throw_exception_and_all_other_property_keep_their_state()
        {
            var accountDetail = new AccountDetails(100,100,100);

            Action action = () => accountDetail = AccountDetails.SetDailyWireTransferLimit(accountDetail,-100);

            Assert.Throws<InvalidOperationException>(action);

            Assert.Equal(100, accountDetail.Debt);
            Assert.Equal(100, accountDetail.DailyWireTransferLimit);
            Assert.Equal(100, accountDetail.OverdraftLimit);


        }


    }
}
