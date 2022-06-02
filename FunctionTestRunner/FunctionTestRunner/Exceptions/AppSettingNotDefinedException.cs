using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionTestRunner.Exceptions;

public class AppSettingNotDefinedException : Exception
{
    public AppSettingNotDefinedException()
    {
    }

    public AppSettingNotDefinedException(string message)
        : base(message)
    {
    }

    public AppSettingNotDefinedException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
