// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Kae.IoT.PnP.Generator;
using Kae.IoT.PnP.Generator.Csharp;
using Microsoft.Azure.DigitalTwins.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.IoT.PnP.Generator.Csharp.Common.template
{
    partial class AppIoTData_cs : IVersionedGenerator
    {
        private string indent;
        private string indentUnit;
        private string nameSpace;
        private IDictionary<string, GElemDTTelemetryInfo> dtTelemetries;
        private IDictionary<string, GElemDTPropertyInfo> dtDesiredProperties;
        private IDictionary<string, GElemDTPropertyInfo> dtReporedProperties;
        private IDictionary<string, GElemDTCommandInfo> dtSyncDirectMethods;
        private IDictionary<string, GElemDTCommandInfo> dtAsyncDirectMethods;

        private string d2cDataTypeName;
        private string d2cDataTypeNameForDeserialize;
        private string dpDataTypeName;
        private string dpDataTypeNameForDeserialize;
        private string rpDataTypeName;
        private string rpDataTypeNameForDeserialize;

        public string Version { get; set; }

        public AppIoTData_cs(string indentUnit, string indent, string nameSpace,
           string d2cDTName,  string d2cDTNameForDeserialize,
            string dpDTName, string dpDTNameForDeserialize,
            string rpDTName,  string rpDTNameForDeserialize,
            IDictionary<string, GElemDTTelemetryInfo> telemetries,
            IDictionary<string, GElemDTPropertyInfo> desiredProperties,
            IDictionary<string, GElemDTPropertyInfo> reportedProperties,
            IDictionary<string, GElemDTCommandInfo> syncDirectMethods,
            IDictionary<string, GElemDTCommandInfo> asyncDirectMethods)
        {
            this.indentUnit = indentUnit;
            this.indent = indent;
            this.nameSpace = nameSpace;

            this.d2cDataTypeName = d2cDTName;
            this.d2cDataTypeNameForDeserialize = d2cDTNameForDeserialize;
            this.dpDataTypeName = dpDTName;
            this.dpDataTypeNameForDeserialize = dpDTNameForDeserialize;
            this.rpDataTypeName = rpDTName;
            this.rpDataTypeNameForDeserialize = rpDTNameForDeserialize;

            this.dtTelemetries = telemetries;
            this.dtDesiredProperties = desiredProperties;
            this.dtReporedProperties = reportedProperties;
            this.dtSyncDirectMethods = syncDirectMethods;
            this.dtAsyncDirectMethods = asyncDirectMethods;

        }
        public FieldDecl CreateFieldDecl(string indentUnit, string indent, string name, DTSchemaInfo schema)
        {
            return new FieldDecl(indentUnit, indent, name, new GElemDTSchemaInfo() { Info = schema });
        }

        public void Prototype()
        {
            foreach (var dmsyncInfo in dtSyncDirectMethods.Values)
            {
                var dmsync = dmsyncInfo.Info;
                var parentTypeName = $"Command_{dmsync.Name}";
                var requestPayload = dmsync.Request;
                var responsePayload = dmsync.Response;
                string genRequest = null;
                string genResponse = null;
                if (requestPayload is not null)
                {
                    string typeName = $"{CSharpCodeGenerator.GetMethodName(dmsync)}_Request_${requestPayload.Name}";
                    genRequest = TransformCommandPayload(requestPayload, typeName);
                }
                if (responsePayload is not null)
                {
                    string typeName = $"{CSharpCodeGenerator.GetMethodName(dmsync)}_Response_${responsePayload.Name}";
                    genResponse = TransformCommandPayload(requestPayload, typeName);
                }
                if ((!string.IsNullOrEmpty(genRequest)) || (!string.IsNullOrEmpty(genResponse)))
                {
                    if (!string.IsNullOrEmpty(genRequest))
                    {

                    }
                    if (!string.IsNullOrEmpty(genResponse))
                    {

                    }
                }
            }
        }

        public string GetMethodName(DTCommandInfo commandInfo)
        {
            return CSharpCodeGenerator.GetMethodName(commandInfo);
        }

        public string TransformCommandPayload(DTCommandPayloadInfo payloadInfo, string typeName)
        {
            string gen = null;

            if (payloadInfo.Schema is DTEnumInfo)
            {
                var xformEnum = new EnumTypeDecl(new GElemDTEnumInfo() { Info = (DTEnumInfo)payloadInfo.Schema }, "public", typeName, indent, indentUnit);
                gen = xformEnum.TransformText();
            }
            else if (payloadInfo.Schema is DTObjectInfo)
            {
                var xformObj = new ObjectTypeDecl(new GElemDTObjectInfo() { Info = (DTObjectInfo)payloadInfo.Schema }, "public", typeName, indent, indentUnit);
            }

            return gen;
        }
    }
}
