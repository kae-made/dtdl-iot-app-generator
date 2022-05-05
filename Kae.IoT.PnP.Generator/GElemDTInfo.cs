// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Microsoft.Azure.DigitalTwins.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.IoT.PnP.Generator
{
    public abstract class GElemDTInfo
    {
        public ColorForGeneration Coloring { get; set; }
        
    }

    public class GElemDTInterfaceInfo : GElemDTInfo
    {
        public DTInterfaceInfo Info { get; set; }
        public Dtmi Dtmi { get; set; }

       
    }
    public class GElemDTComponentInfo : GElemDTInfo
    {
        public DTComponentInfo Info { get; set; }
        public Dtmi Dtmi { get; set; }

       
    }

    public class GElemDTTelemetryInfo : GElemDTInfo
    {
        public DTTelemetryInfo Info { get; set; }
       
    }

    public class GElemDTPropertyInfo : GElemDTInfo
    {
        public DTPropertyInfo Info { get; set; }
       
    }

    public class GElemDTCommandInfo : GElemDTInfo
    {
        public DTCommandInfo Info { get; set; }
       
    }

    public class GElemDTSchemaInfo : GElemDTInfo
    {
        public DTSchemaInfo Info { get; set; }
    }

    public class GElemDTEnumInfo : GElemDTInfo
    {
        public DTEnumInfo Info { get; set; }
    }

    public class GElemDTObjectInfo: GElemDTInfo
    {
        public DTObjectInfo Info { get; set; }
    }
}

