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



        [Fact]

        public void two_AccountDetail_object_should_equals_when_they_have_same_value_property()
        {

            var accountDetail1 = new AccountDetails(100, 100, 100, 100);

            var accountDetail2 = new AccountDetails(100, 100, 100, 100);



            Assert.Equal(accountDetail1, accountDetail2);

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
        public void DailyWireTransferLimit_should_update_its_value()
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



        [Fact]
        public void OverdraftLimit_should_up_its_value()
        {


            var accountDetail = new AccountDetails(100);

            accountDetail = AccountDetails.SetOverdraftLimit(accountDetail, 500);


            Assert.Equal(100, accountDetail.Debt);

            Assert.Equal(500, accountDetail.OverdraftLimit);

        }



        [Fact]

        public void OverdraftLimit_should_throw_exception_and_all_other_property_keep_their_state()
        {
            var accountDetail = new AccountDetails(100, 100, 100);

            Action action = () => accountDetail = AccountDetails.SetOverdraftLimit(accountDetail, -100);

            Assert.Throws<InvalidOperationException>(action);

            Assert.Equal(100, accountDetail.Debt);
            Assert.Equal(100, accountDetail.DailyWireTransferLimit);
            Assert.Equal(100, accountDetail.OverdraftLimit);


        }



        [Fact]

        public void WithdrowMoney_from_account_should_update_debt_ammount()
        {

            var accountDetail = new AccountDetails(100, 100);

            accountDetail = AccountDetails.WithdrowMoney(accountDetail, 50);


            Assert.Equal(50, accountDetail.Debt);

        }


        [Fact]

        public void withdrowMoney_from_account_should_upadte_debt_ammount_even_if_debt_ammount_is_negative()
        {

            var accountdetail = new AccountDetails(100, 100);


            accountdetail = AccountDetails.WithdrowMoney(accountdetail, 150);


            Assert.Equal(-50, accountdetail.Debt);

        }


        [Fact]

        public void withdrowMoney_should_not_pass_OverdraftLimit()
        {

            var accountdetail = new AccountDetails(100, 100);

            Action action = () => AccountDetails.WithdrowMoney(accountdetail, 250);

            Assert.Throws<InvalidOperationException>(action);
        }





    }
}
