using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020002AE RID: 686
	public class PiiPropertyDefinition
	{
		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x0004E465 File Offset: 0x0004C665
		// (set) Token: 0x060018B7 RID: 6327 RVA: 0x0004E46D File Offset: 0x0004C66D
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x0004E476 File Offset: 0x0004C676
		// (set) Token: 0x060018B9 RID: 6329 RVA: 0x0004E47E File Offset: 0x0004C67E
		[XmlAttribute(AttributeName = "redactor")]
		public string Redactor { get; set; }

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x0004E487 File Offset: 0x0004C687
		// (set) Token: 0x060018BB RID: 6331 RVA: 0x0004E48F File Offset: 0x0004C68F
		[XmlAttribute(AttributeName = "enumerable")]
		public bool Enumerable { get; set; }

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060018BC RID: 6332 RVA: 0x0004E498 File Offset: 0x0004C698
		// (set) Token: 0x060018BD RID: 6333 RVA: 0x0004E4A0 File Offset: 0x0004C6A0
		[XmlAttribute(AttributeName = "addIntoMap")]
		public bool AddIntoMap { get; set; }
	}
}
