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
        [InlineData("Med", 0, -100, 0)]
        [InlineData("Med", 0, 0, -100)]
        [InlineData("Med", -100, -100, 0)]
        [InlineData("Med", -100, -100, -100)]

        public void Account_should_be_not_Created(string name,decimal Debt, decimal OverdraftLimit,decimal DailyWireTransferLimit)
        {

            Action action = () => { var account = new Account(name, Debt, OverdraftLimit, DailyWireTransferLimit); };

            Assert.Throws<InvalidOperationException>(action);

        }


     

    }
}
