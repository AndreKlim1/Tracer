using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
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

        public Tracer(int threadID)
        {
            _traceResultStruct.Id = threadID;
            _traceResultStruct.Methods = new List<MethodNode>();
            if (_tracersDict == null)
            {
                _tracersDict = new ConcurrentDictionary<int, TraceResultStruct>();
            }
        }

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
            _prevMethod = _method;
            _method = new MethodNode();
            _method.parentMethod = _prevMethod;

            int prevMethodDepth = _prevMethod?.GetMethodStruct.MethodDepth ?? -1;
            _currentMethodDepth = _method.GetMethodStruct.MethodDepth;

            switch (prevMethodDepth)
            {
                case var depth when depth == -1:
                    _traceResultStruct.Methods.Add(_method);
                    break;
                case var depth when depth == (_currentMethodDepth - 1):
                    _prevMethod.GetMethodStruct.internalMethodStructs.Add(_method);
                    break;
                default:
                    _traceResultStruct.Methods.Add(_method);
                    break;
            }

            _method.StartStopwatch();
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

        public void ConsoleResult(string resJSON, string resXML)
        {
            Console.WriteLine(resJSON);
            Console.WriteLine(resXML);
        }

        public void FileOutputResult(string filePath1, string filePath2, string resJSON, string resXML)
        {
            File.WriteAllText(filePath1, resJSON);
            File.WriteAllText(filePath2, resXML);
        }

        private string GetJSON(TraceResultStruct result)
        {
            return JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented); ;
        }

        private string GetXML(TraceResultStruct result)
        {
            var xmlSerializer = new XmlSerializer(typeof(TraceResultStruct));

            var settings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t"
            };

            using (StringWriter textWriter = new StringWriter())
            {
                using (var writer = XmlWriter.Create(textWriter, settings))
                {
                    var namespaces = new XmlSerializerNamespaces();
                    namespaces.Add(string.Empty, string.Empty); 

                    xmlSerializer.Serialize(writer, result, namespaces);
                }
                return textWriter.ToString();
            }
        }

        public void GetMultiThreadResult(string filePath1, string filePath2)
        {
            string json = string.Empty;
            string xml = string.Empty;
            foreach (var thread in _tracersDict)
            {
                json += GetJSON(thread.Value);
                xml += GetXML(thread.Value);
            }
            ConsoleResult(json, xml);
            FileOutputResult(filePath1, filePath2, json, xml);
        }
    }
}
