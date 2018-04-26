using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ttf.BusinessLayer
{
    public class InvalidOptionException : InvalidOperationException
    {
        public InvalidOptionException()
            : base()
        { }

        public InvalidOptionException(string message)
            : base(message)
        { }
    }
}
