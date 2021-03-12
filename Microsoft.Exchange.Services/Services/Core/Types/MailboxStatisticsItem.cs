using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000873 RID: 2163
	[DataContract(Name = "MailboxStatisticsItem", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "MailboxStatisticsItemType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class MailboxStatisticsItem
	{
		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06003E09 RID: 15881 RVA: 0x000D81E3 File Offset: 0x000D63E3
		// (set) Token: 0x06003E0A RID: 15882 RVA: 0x000D81EB File Offset: 0x000D63EB
		[XmlElement("MailboxId")]
		[DataMember(Name = "MailboxId", IsRequired = true)]
		public string MailboxId { get; set; }

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06003E0B RID: 15883 RVA: 0x000D81F4 File Offset: 0x000D63F4
		// (set) Token: 0x06003E0C RID: 15884 RVA: 0x000D81FC File Offset: 0x000D63FC
		[XmlElement("DisplayName")]
		[DataMember(Name = "DisplayName", IsRequired = true)]
		public string DisplayName { get; set; }

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06003E0D RID: 15885 RVA: 0x000D8205 File Offset: 0x000D6405
		// (set) Token: 0x06003E0E RID: 15886 RVA: 0x000D820D File Offset: 0x000D640D
		[DataMember(Name = "ItemCount", IsRequired = true)]
		[XmlElement("ItemCount")]
		public long ItemCount { get; set; }

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06003E0F RID: 15887 RVA: 0x000D8216 File Offset: 0x000D6416
		// (set) Token: 0x06003E10 RID: 15888 RVA: 0x000D821E File Offset: 0x000D641E
		[XmlElement("Size")]
		[DataMember(Name = "Size", IsRequired = true)]
		public ulong Size { get; set; }
	}
}
