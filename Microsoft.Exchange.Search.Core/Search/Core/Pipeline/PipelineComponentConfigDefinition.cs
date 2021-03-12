using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Search.Core.Pipeline
{
	// Token: 0x020000A9 RID: 169
	[Serializable]
	public sealed class PipelineComponentConfigDefinition
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00010D93 File Offset: 0x0000EF93
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x00010D9B File Offset: 0x0000EF9B
		[XmlAttribute]
		public string Name { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00010DA4 File Offset: 0x0000EFA4
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x00010DAC File Offset: 0x0000EFAC
		[XmlAttribute]
		public string Value { get; set; }
	}
}
