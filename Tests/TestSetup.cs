using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer.Entities;
using Tracer.Tracer;
using Xunit.Sdk;

namespace Tests
{
    public class TestSetup
    {
        public TraceResultStruct _traceResultStruct { get; }

        public TestSetup()
        {
            ITracer tracer = new TracerClass(1);
            TestClasses.TestClass1 First = new TestClasses.TestClass1(tracer);
            TestClasses.TestClass2 Second = new TestClasses.TestClass2(tracer);

            First.Method0();
            Second.Method1();

            _traceResultStruct = tracer.GetTraceResult();
        }
    }
}
