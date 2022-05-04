using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.IoT.PnP.Generator
{
    public interface IVersionedGenerator
    {
        string Version { get; set; }
    }
}
