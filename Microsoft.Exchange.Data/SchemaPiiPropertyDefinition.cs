using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020002AD RID: 685
	public class SchemaPiiPropertyDefinition
	{
		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x0004E43B File Offset: 0x0004C63B
		// (set) Token: 0x060018B2 RID: 6322 RVA: 0x0004E443 File Offset: 0x0004C643
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x0004E44C File Offset: 0x0004C64C
		// (set) Token: 0x060018B4 RID: 6324 RVA: 0x0004E454 File Offset: 0x0004C654
		[XmlArray(ElementName = "PiiProperties")]
		[XmlArrayItem(ElementName = "Property")]
		public PiiPropertyDefinition[] PiiProperties { get; set; }
	}
}
