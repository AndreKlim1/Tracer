using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer.Tracer;

namespace Tests.TestClasses
{
    internal class TestClass2
    {
        private readonly ITracer _tracer;

        internal TestClass2(ITracer tracer)
        {
            _tracer = tracer;
        }

        internal void Method1()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
        }
    }
}
