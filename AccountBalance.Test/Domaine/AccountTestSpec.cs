using AccountBalance.Domaine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AccountBalance.Test.Domaine
{
    public class AccountTestSpec
    {
        [Fact]

        public void Account_Should_Be_Created()
        {
            //arrange
            var account = new Account("Mohamed", 100);

            //act 


            //assert

            Assert.NotNull(account);

          
        }

        [Theory]
        [InlineData("",0, 0, 0)]
        [InlineData(null, 0, 0, 0)]
        [InlineData("Med",-100,0,0)]
        [InlineData("Med", 0, -100, 0)]
        [InlineData("Med", 0, 0, -100)]
        [InlineData("Med", -100, -100, 0)]
        [InlineData("Med", -100, -100, -100)]

        public void Account_should_be_not_Created(string name,decimal Debt, decimal OverdraftLimit,decimal DailyWireTransferLimit)
        {

            Action action = () => { var account = new Account(name, Debt, OverdraftLimit, DailyWireTransferLimit); };

            Assert.Throws<InvalidOperationException>(action);

        }

        [Fact]

        public void WithdrawCash_need_update_debt_ammount()
        {
            var account = new Account("Med", 500, 100, 50);

            account.WithdrawCash(50);

            Assert.Equal(450, account.AccountDetail.Debt);


        }

        [Fact]

        public void wire_transfer_should_update_debt_and_add_amount_withdrawn_today()
        {
            var account = new Account("Med", 500, 100, 50);


            account.WireTransfer(50);

            Assert.Equal(450, account.AccountDetail.Debt);

            Assert.Equal(50, account.AccountDetail.WithdrawnToday);


        }


       [Fact]
       public void account_state_should_change_to_blocked_when_withdraw_cash_pass_overdraft_limit()
        {
            var account = new Account("Med", 50, 50,50);

            Action action = ()=>  account.WithdrawCash(200);

            Assert.Throws<InvalidOperationException>(action);
            Assert.Equal(State.Blocked, account.AccountState);


        }



        [Fact]

        public void deposit_cash_should_update_debt_and_keep_account_details_state()
        {
            var account = new Account("Med", 100, 100, 100);

            account.DepositeCash(100);


            Assert.Equal(200, account.AccountDetail.Debt);
            Assert.Equal(100, account.AccountDetail.DailyWireTransferLimit);
            Assert.Equal(100, account.AccountDetail.OverdraftLimit);


        }


        [Fact]

        public void deposite_cash_should_update_account_state_if_its_blocked()
        {

            var account = new Account("Med", 50, 50, 100);
            try {
                account.WithdrawCash(200);
            } catch{ };

            account.DepositeCash(200);

            Assert.Equal(State.Active, account.AccountState);
        }


        [Theory]

        [InlineData(0)]
        [InlineData(-100)]

        public void deposite_negative_amount_of_cash_should_throw_exception(decimal  amount)
        {
            var account = new Account("Med", 100, 100, 100);

            Action action = () => account.DepositeCash(amount);


            Assert.Throws<ArgumentOutOfRangeException>(action);

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]

        public void deposite__amount_of_cash_should_throw_exception_and_keep_same_account_state(decimal amount)
        {
            var account = new Account("Med", 100, 100, 100);

            try
            {
                account.WithdrawCash(500);
            }
            catch{ }
            Action action = () => account.DepositeCash(amount);


            Assert.Throws<ArgumentOutOfRangeException>(action);
            Assert.Equal(State.Blocked, account.AccountState);
        }


    }
}
