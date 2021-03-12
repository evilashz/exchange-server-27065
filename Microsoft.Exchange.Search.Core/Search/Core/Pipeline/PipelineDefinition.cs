using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Search.Core.Pipeline
{
	// Token: 0x020000B5 RID: 181
	[XmlRoot(ElementName = "Pipeline")]
	[Serializable]
	public sealed class PipelineDefinition
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00011B99 File Offset: 0x0000FD99
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x00011BA1 File Offset: 0x0000FDA1
		[XmlElement]
		public string Name { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00011BAA File Offset: 0x0000FDAA
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x00011BB2 File Offset: 0x0000FDB2
		[XmlElement]
		public int MaxConcurrency { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00011BBB File Offset: 0x0000FDBB
		// (set) Token: 0x06000584 RID: 1412 RVA: 0x00011BC3 File Offset: 0x0000FDC3
		[XmlElement]
		public int PoisonComponentThreshold { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x00011BCC File Offset: 0x0000FDCC
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x00011BD4 File Offset: 0x0000FDD4
		[XmlArrayItem(ElementName = "Component")]
		[XmlArray]
		public PipelineComponentDefinition[] Components { get; set; }

		// Token: 0x06000587 RID: 1415 RVA: 0x00011BE0 File Offset: 0x0000FDE0
		internal static PipelineDefinition LoadFrom(string definition)
		{
			PipelineDefinition.diagnosticsSession.TraceDebug<string>("Loading an instance of pipeline definition from string: {0}", definition);
			PipelineDefinition result;
			using (TextReader textReader = new StringReader(definition))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(PipelineDefinition));
				result = (PipelineDefinition)xmlSerializer.Deserialize(textReader);
			}
			return result;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00011C40 File Offset: 0x0000FE40
		internal static PipelineDefinition LoadFromFile(string filepath)
		{
			PipelineDefinition.diagnosticsSession.TraceDebug<string>("Loading an instance of pipeline definition from file: {0}", filepath);
			PipelineDefinition result;
			using (XmlReader xmlReader = new XmlTextReader(filepath))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(PipelineDefinition));
				result = (PipelineDefinition)xmlSerializer.Deserialize(xmlReader);
			}
			return result;
		}

		// Token: 0x0400027D RID: 637
		private static readonly IDiagnosticsSession diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession("PipelineDefinition", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.PipelineLoaderTracer, 0L);
	}
}
