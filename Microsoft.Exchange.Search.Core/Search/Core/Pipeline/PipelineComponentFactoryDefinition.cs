using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Search.Core.Pipeline
{
	// Token: 0x020000AC RID: 172
	[Serializable]
	public sealed class PipelineComponentFactoryDefinition
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x00010F9C File Offset: 0x0000F19C
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x00010FA4 File Offset: 0x0000F1A4
		[XmlAttribute(AttributeName = "Assembly")]
		public string AssemblyFullName { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x00010FAD File Offset: 0x0000F1AD
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x00010FB5 File Offset: 0x0000F1B5
		[XmlText]
		public string TypeFullName { get; set; }
	}
}
