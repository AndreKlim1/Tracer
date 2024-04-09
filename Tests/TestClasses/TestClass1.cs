using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer.Tracer;

namespace Tests.TestClasses
{
    internal class TestClass1
    {
        private readonly ITracer _tracer;
        private readonly TestClass2 _2;

        internal TestClass1(ITracer tracer)
        {
            _tracer = tracer;
            _2 = new TestClass2(_tracer);
        }

        internal void Method0()
        {
            _tracer.StartTrace();
            Method1();
            _2.Method1();
            Thread.Sleep(10);
            _tracer.StopTrace();
        }

        internal void Method1()
        {
            _tracer.StartTrace();
            Thread.Sleep(20);
            Method2();
            _tracer.StopTrace();
        }

        internal void Method2()
        {
            _tracer.StartTrace();
            Thread.Sleep(30);
            _tracer.StopTrace();
        }
    }
}
