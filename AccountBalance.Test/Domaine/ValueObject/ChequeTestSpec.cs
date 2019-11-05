using AccountBalance.Domaine.ValueObjects.Cash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AccountBalance.Test.Domaine.ValueObject
{
   public class ChequeTestSpec
    {
        [Fact]

        public void cheque_need_be_created()
        {
            var cheque = new Cheque(100, DateTime.Now);


            Assert.NotNull(cheque);

        }

        [Fact]
       
        public void cheque_need_throw_exception()
        {
            Action action1 = () => { var cheque = new Cheque(-100, DateTime.Now); };
            Action action2 = () => { var cheque = new Cheque(100, null); };
            Action action3 = () => { var cheque = new Cheque(-100, null); };
            Assert.Throws<InvalidOperationException>(action1);
            Assert.Throws<InvalidOperationException>(action2);
            Assert.Throws<InvalidOperationException>(action3);


        }



        [Fact]

        public void cheque_have_the_same_data_property_should_be_equal()
        {
            var cheque1 = new Cheque(100, new DateTime());
            var cheque2 = new Cheque(100, new DateTime());

            Assert.Equal(cheque1, cheque2);
        }
    }
}
