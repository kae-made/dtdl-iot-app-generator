using Kae.IoT.PnP.Generator;
using Microsoft.Azure.DigitalTwins.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.IoT.PnP.Generator.Csharp.Common.template
{
    partial class FieldDecl : IVersionedGenerator
    {
        private string indentUnit;
        private string indent;
        private string fieldName;
        private GElemDTSchemaInfo schemaInfo;

        public string Version { get; set; }

        public FieldDecl(string indentUnit, string indent, string fieldName, GElemDTSchemaInfo schemaInfo)
        {
            this.indentUnit =indentUnit;
            this.indent = indent;
            this.fieldName = fieldName;
            this.schemaInfo = schemaInfo;
        }

        protected FieldDecl CreateChildFieldDecl(string indentUnit, string indent, string fieldName, DTSchemaInfo schema)
        {
            return new FieldDecl(indentUnit, indent, fieldName, new GElemDTSchemaInfo() { Info = schema });
        }

        protected EnumTypeDecl CreateEnumTypeDecl(DTEnumInfo enumSchema, string scope, string typeName, string indent, string indentUnit)
        {
            return new EnumTypeDecl(new GElemDTEnumInfo() { Info = enumSchema }, scope, typeName, indent, indentUnit);
        }

        protected ObjectTypeDecl CreateObjectTypeDecl(DTObjectInfo objSchema, string scope, string typeName, string indent, string indentUnit)
        {
            return new ObjectTypeDecl(new GElemDTObjectInfo() { Info = objSchema }, scope, typeName, indent, indentUnit);
        }



        public void FixFieldName(string name=null)
        {
            if (name is null)
            {
                var origName = fieldName;
                fieldName = origName.Substring(0, 1).ToUpper() + origName.Substring(1);
            }
            else
            {
                fieldName = name;
            }
        }

        public string GetDataTypeProgramName(DTPrimitiveSchemaInfo dataSchema)
        {
            string typeName = "unknown";
            if (dataSchema is DTStringInfo)
            {
                typeName = "string";
            }
            else if (dataSchema is DTBooleanInfo)
            {
                typeName = "bool";
            }
            else if (dataSchema is DTIntegerInfo)
            {
                typeName = "int";
            }
            else if (dataSchema is DTLongInfo)
            {
                typeName = "long";
            }
            else if (dataSchema is DTFloatInfo)
            {
                typeName = "float";
            }
            else if (dataSchema is DTDoubleInfo)
            {
                typeName = "double";
            }
            else if (dataSchema is DTDateInfo || dataSchema is DTTimeInfo || dataSchema is DTDateTimeInfo)
            {
                typeName = "DateTime";
            }
            else if (dataSchema is DTDurationInfo)
            {
                typeName = "TimeSpan";
            }
            return typeName;
        }

        void Prototype()
        {
            string fieldName = "";
            string indent = "    ";
            string indentUnit = "    ";
            DTFieldInfo fieldInfo = null;
            DTSchemaInfo schema = fieldInfo.Schema;
            if (schema is DTPrimitiveSchemaInfo)
            {
                if (schema is DTStringInfo)
                {

                }
                else if (schema is DTBooleanInfo)
                {

                }
                else if (schema is DTFloatInfo)
                {

                }
                else if (schema is DTIntegerInfo)
                {

                }
                else if (schema is DTLongInfo)
                {

                }
                else if (schema is DTDateInfo)
                {

                }
                else if (schema is DTDateTimeInfo)
                {

                }
                else if (schema is DTDurationInfo)
                {

                }
                else if (schema is DTTimeInfo)
                {

                }
                else
                {

                }

            }
            else if (schema is DTComplexSchemaInfo)
            {
                if (schema is DTArrayInfo)
                {
                    var arraySchema = (DTArrayInfo)schema;
                    var arrayTypeSchema = arraySchema.ElementSchema;


                }
                else if (schema is DTEnumInfo)
                {
                    var enumSchema = (DTEnumInfo)schema;
                    bool isNumericEnum = false;
                    if (enumSchema.ValueSchema is DTIntegerInfo)
                    {
                        isNumericEnum = true;
                    }
                    var enumSchemaTypeName = fieldName + "DataType";
                    foreach (var enumValue in enumSchema.EnumValues)
                    {
                        if (isNumericEnum)
                        {

                        }
                        else
                        {

                        }
                    }
                }

                else if (schema is DTMapInfo)
                {

                }
                else if (schema is DTObjectInfo)
                {
                    var objectSchema = (DTObjectInfo)schema;
                    var objectSchemaTypeName = fieldName + "DataType";

                    indent += indentUnit;
                    foreach (var objectField in objectSchema.Fields)
                    {
                        var objectFieldGen = new FieldDecl(indentUnit, indent, objectField.Name, new GElemDTSchemaInfo() { Info = objectField.Schema });
                        objectFieldGen.FixFieldName();
                        var objectFieldContent = objectFieldGen.TransformText();
                    }
                    indent = indent.Substring(indent.Length - indentUnit.Length);

                }
                else
                {

                }
            }
            else
            {

            }
        }
    }
}
