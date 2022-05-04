using Kae.IoT.PnP.Generator;
using Microsoft.Azure.DigitalTwins.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.IoT.PnP.Generator.Csharp.Common.template
{
    partial class ObjectTypeDecl : IVersionedGenerator
    {
        private string scopeName;
        private string typeName;
        private string indent;
        private string indentUnit;
        private GElemDTObjectInfo schemaInfo;
        public ObjectTypeDecl(GElemDTObjectInfo schemaInfo, string scopeName, string typeName, string indent, string indentUnit)
        {
            this.scopeName = scopeName;
            this.typeName = typeName;
            this.schemaInfo = schemaInfo;
            this.indent = indent;
            this.indentUnit = indentUnit;
        }

        public string Version { get; set; }

        public FieldDecl CreateFieldDecl(string indentUnit, string indent, string fieldName, DTSchemaInfo fieldSchema)
        {
            return new FieldDecl(indentUnit, indent, fieldName, new GElemDTSchemaInfo() { Info = fieldSchema });
        }

    }
}
