﻿using Kae.IoT.PnP.Generator;
// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.IoT.PnP.Generator.Csharp.App.template
{
    partial class Worker_cs : IVersionedGenerator
    {
        private string nameSpace;
        public string Version { get; set; }

        public Worker_cs(string nameSpace)
        {
            this.nameSpace = nameSpace;
        }
    }
}
