using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace TestApp
{
    public class MyException : Exception
    {
        public MyException() : base() { }
        public MyException(string msg) : base(msg) { }
    }

    class PInvokeLayer
    {
        public delegate void UnmanagedExceptionCallback(string msg);

        class ExceptionHelper
        {
            [DllImport("test", EntryPoint="registerExceptionCallback")]
            public static extern void registerExceptionCallback(UnmanagedExceptionCallback callback);
            
            static void MyExceptionCallback(string msg)
            {
                Console.WriteLine($"Constructing exception: {msg}");
                throw new MyException(msg);
            }
            
            static ExceptionHelper()
            {
                Console.WriteLine($"register exception callback");
                registerExceptionCallback(MyExceptionCallback);
            }
        
            public static void Init() {} //Kickstart the static ctor
        }

        [DllImport("test")]
        public static extern int foo();

        static PInvokeLayer()
        {
            ExceptionHelper.Init();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try {
                int result = PInvokeLayer.foo();
                Console.WriteLine($"foo() returned: {result}");
            } catch (MyException ex) {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }
        }
    }
}
