using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020002AB RID: 683
	public class PiiResource
	{
		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x0004E3E7 File Offset: 0x0004C5E7
		// (set) Token: 0x060018A8 RID: 6312 RVA: 0x0004E3EF File Offset: 0x0004C5EF
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x0004E3F8 File Offset: 0x0004C5F8
		// (set) Token: 0x060018AA RID: 6314 RVA: 0x0004E400 File Offset: 0x0004C600
		[XmlArray(ElementName = "LocStrings")]
		[XmlArrayItem(ElementName = "LocString")]
		public PiiLocString[] LocStrings { get; set; }
	}
}
