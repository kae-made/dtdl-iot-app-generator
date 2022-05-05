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
    public abstract class ColorForGeneration
    {
        public DTEntityInfo Target { get; set; }
        public abstract string TransformText();
        /// <summary>
        /// Target:DTEntryInfo の Description に記載されたColoringを解釈する
        /// </summary>
        /// <returns></returns>
        public abstract bool ResolveColoring();
    }
}
