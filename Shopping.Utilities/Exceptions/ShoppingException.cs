using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Utilities.Exceptions
{
    public class ShoppingException:Exception
    {
        public ShoppingException()
        {

        }
        public ShoppingException(string message) : base(message)
        {

        }
        public ShoppingException(string message,Exception inner):
            base(message, inner)
        {

        }
    }
}
