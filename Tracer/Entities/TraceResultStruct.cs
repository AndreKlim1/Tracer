using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Tracer.Tracer;

namespace Tracer.Entities
{
    [XmlRoot(ElementName = "root")]
    public struct TraceResultStruct
    {
        [XmlAttribute("id")]
        public int Id;
        [XmlAttribute("time")]
        public double Time;
        public List<MethodNode> Methods;
    }
}
