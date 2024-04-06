using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer.Entities;

namespace Tracer.Tracer
{
    public class Tracer : ITracer
    {
        private TraceResultStruct _traceResultStruct;
        private MethodNode _method;
        private MethodNode _prevMethod;
        private int _currentMethodDepth;
        private static ConcurrentDictionary<int, TraceResultStruct> _tracersDict;

        public TraceResultStruct GetTraceResult()
        {
            CountTotalTime();

            _tracersDict.AddOrUpdate(_traceResultStruct.Id, _traceResultStruct, (key, existingValue) => _traceResultStruct);
            return _traceResultStruct;
        }

        private void CountTotalTime()
        {
            _traceResultStruct.Time = 0;
            foreach (var method in _traceResultStruct.Methods)
            {
                _traceResultStruct.Time += method.GetMethodStruct.Time;
            }
        }

        public void StartTrace()
        {
            throw new NotImplementedException();
        }

        public void StopTrace()
        {
            _method.StopStopwatch();



            if (_prevMethod == _method)
            {
                if (_prevMethod.parentMethod != null)
                {
                    _method = _prevMethod.parentMethod;
                }
            }
            else
            {
                if (_method.GetMethodStruct.MethodDepth == 0)
                {
                    _prevMethod = _method;
                }
                else
                {
                    _method = _prevMethod;
                }
            }

        }

        public Tracer(int threadID)
        {
            _traceResultStruct.Id = threadID;
            _traceResultStruct.Methods = new List<MethodNode>();
            if (_tracersDict == null)
            {
                _tracersDict = new ConcurrentDictionary<int, TraceResultStruct>();
            }
        }
    }
}
