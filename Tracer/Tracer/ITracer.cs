﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer.Entities;

namespace Tracer.Tracer
{
    public interface ITracer
    {

        void StartTrace();


        void StopTrace();


        TraceResultStruct GetTraceResult();
    }
}
