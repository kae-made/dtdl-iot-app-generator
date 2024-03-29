﻿// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Kae.IoT.PnP.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.IoT.PnP.Generator.Csharp.Edge.template
{
    partial class ProgramEdge_cs : IVersionedGenerator
    {
        private string nameSpace;
        public string Version { get; set; }

        public ProgramEdge_cs(string nameSpace)
        {
            this.nameSpace = nameSpace;
        }
    }
}
