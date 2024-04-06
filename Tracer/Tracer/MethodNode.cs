﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Tracer.Entities;

namespace Tracer.Tracer
{
    public class MethodNode
    {
        private MethodStruct _methodStruct;
        private Stopwatch _stopwatch;

        [JsonProperty(PropertyName = "method properties")]
        public MethodStruct GetMethodStruct { get { return _methodStruct; } }

        
        [JsonIgnore]
        [XmlIgnore]
        public MethodNode parentMethod;

        private (string, string) GetCallingMethodNameAndClassName()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();

            int skipFrames = 2;


            for (int i = skipFrames; i < stackFrames.Length; i++)
            {
                MethodBase method = stackFrames[i].GetMethod();
                if (method.DeclaringType != typeof(TracerClass))
                {
                    return (method.Name, method.DeclaringType.Name);
                }
            }

            return (string.Empty, string.Empty);
        }


        public void StartStopwatch()
        {
            _stopwatch = Stopwatch.StartNew();
        }

        public void StopStopwatch()
        {
            TimeSpan elapsedTime = _stopwatch.Elapsed;
            _methodStruct.Time = elapsedTime.TotalMilliseconds;
        }

        public MethodNode()
        {
            _methodStruct.internalMethodStructs = new List<MethodNode>();
            StackTrace stackTrace = new StackTrace();
            (string, string) res = GetCallingMethodNameAndClassName();

            _methodStruct.Name = res.Item1;
            _methodStruct.ClassName = res.Item2;
            _methodStruct.MethodDepth = stackTrace.FrameCount - 4;
        }
    }
}
