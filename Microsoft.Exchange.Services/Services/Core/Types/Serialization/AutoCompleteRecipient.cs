using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types.Serialization
{
	// Token: 0x020005A2 RID: 1442
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "AutoCompleteRecipientType")]
	[XmlType("AutoCompleteRecipientType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class AutoCompleteRecipient
	{
		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x060028D9 RID: 10457 RVA: 0x000AC9C3 File Offset: 0x000AABC3
		// (set) Token: 0x060028DA RID: 10458 RVA: 0x000AC9CB File Offset: 0x000AABCB
		[DataMember(Order = 1)]
		[XmlElement]
		public string PersonaId { get; set; }

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x060028DB RID: 10459 RVA: 0x000AC9D4 File Offset: 0x000AABD4
		// (set) Token: 0x060028DC RID: 10460 RVA: 0x000AC9DC File Offset: 0x000AABDC
		[XmlElement]
		[DataMember(Order = 2)]
		public string RecipientId { get; set; }

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x060028DD RID: 10461 RVA: 0x000AC9E5 File Offset: 0x000AABE5
		// (set) Token: 0x060028DE RID: 10462 RVA: 0x000AC9ED File Offset: 0x000AABED
		[XmlElement]
		[DataMember(Order = 3)]
		public string EmailAddress { get; set; }

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x060028DF RID: 10463 RVA: 0x000AC9F6 File Offset: 0x000AABF6
		// (set) Token: 0x060028E0 RID: 10464 RVA: 0x000AC9FE File Offset: 0x000AABFE
		[XmlElement]
		[DataMember(Order = 4)]
		public int RelevanceScore { get; set; }

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x060028E1 RID: 10465 RVA: 0x000ACA07 File Offset: 0x000AAC07
		// (set) Token: 0x060028E2 RID: 10466 RVA: 0x000ACA0F File Offset: 0x000AAC0F
		[XmlElement]
		[DataMember(Order = 5)]
		public string DisplayName { get; set; }

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x060028E3 RID: 10467 RVA: 0x000ACA18 File Offset: 0x000AAC18
		// (set) Token: 0x060028E4 RID: 10468 RVA: 0x000ACA20 File Offset: 0x000AAC20
		[XmlElement]
		[DataMember(Order = 6)]
		public string FolderName { get; set; }

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x060028E5 RID: 10469 RVA: 0x000ACA29 File Offset: 0x000AAC29
		// (set) Token: 0x060028E6 RID: 10470 RVA: 0x000ACA31 File Offset: 0x000AAC31
		[DataMember(Order = 7)]
		[XmlElement]
		public string Surname { get; set; }

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x060028E7 RID: 10471 RVA: 0x000ACA3A File Offset: 0x000AAC3A
		// (set) Token: 0x060028E8 RID: 10472 RVA: 0x000ACA42 File Offset: 0x000AAC42
		[XmlElement]
		[DataMember(Order = 8)]
		public string GivenName { get; set; }
	}
}
