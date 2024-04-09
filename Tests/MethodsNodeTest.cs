using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tracer.Tracer;

namespace Tests
{
    public class MethodsNodeTest
    {
        [Fact]
        public void GetCallingMethodNameAndClassNameReturnsCorrectValues()
        {
            
            MethodNode methodNode = new MethodNode();
            Type methodNodeType = methodNode.GetType();
            MethodInfo getCallingMethodNameAndClassNameMethod = methodNodeType.GetMethod("GetCallingMethodNameAndClassName", BindingFlags.NonPublic | BindingFlags.Instance);

            
            (string methodName, string className) = ((ValueTuple<string, string>)getCallingMethodNameAndClassNameMethod.Invoke(methodNode, null));

            
            Assert.Equal("InvokeWithNoArgs", methodName);
            Assert.Equal("MethodBaseInvoker", className);
        }
    }
}
