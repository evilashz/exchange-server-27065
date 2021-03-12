using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005BD RID: 1469
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class EntityExtractionResultType
	{
		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06002CB9 RID: 11449 RVA: 0x000B1392 File Offset: 0x000AF592
		// (set) Token: 0x06002CBA RID: 11450 RVA: 0x000B139A File Offset: 0x000AF59A
		[XmlArrayItem(ElementName = "AddressEntity", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(AddressEntityType))]
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public AddressEntityType[] Addresses { get; set; }

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06002CBB RID: 11451 RVA: 0x000B13A3 File Offset: 0x000AF5A3
		// (set) Token: 0x06002CBC RID: 11452 RVA: 0x000B13AB File Offset: 0x000AF5AB
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		[XmlArrayItem(ElementName = "MeetingSuggestion", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(MeetingSuggestionType))]
		public MeetingSuggestionType[] MeetingSuggestions { get; set; }

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06002CBD RID: 11453 RVA: 0x000B13B4 File Offset: 0x000AF5B4
		// (set) Token: 0x06002CBE RID: 11454 RVA: 0x000B13BC File Offset: 0x000AF5BC
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		[XmlArrayItem(ElementName = "TaskSuggestion", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(TaskSuggestionType))]
		public TaskSuggestionType[] TaskSuggestions { get; set; }

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06002CBF RID: 11455 RVA: 0x000B13C5 File Offset: 0x000AF5C5
		// (set) Token: 0x06002CC0 RID: 11456 RVA: 0x000B13CD File Offset: 0x000AF5CD
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		[XmlArrayItem(ElementName = "BusinessName", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		public string[] BusinessNames { get; set; }

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06002CC1 RID: 11457 RVA: 0x000B13D6 File Offset: 0x000AF5D6
		// (set) Token: 0x06002CC2 RID: 11458 RVA: 0x000B13DE File Offset: 0x000AF5DE
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		[XmlArrayItem(ElementName = "PeopleName", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		public string[] PeopleNames { get; set; }

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06002CC3 RID: 11459 RVA: 0x000B13E7 File Offset: 0x000AF5E7
		// (set) Token: 0x06002CC4 RID: 11460 RVA: 0x000B13EF File Offset: 0x000AF5EF
		[XmlArrayItem(ElementName = "EmailAddressEntity", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(EmailAddressEntityType))]
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public EmailAddressEntityType[] EmailAddresses { get; set; }

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06002CC5 RID: 11461 RVA: 0x000B13F8 File Offset: 0x000AF5F8
		// (set) Token: 0x06002CC6 RID: 11462 RVA: 0x000B1400 File Offset: 0x000AF600
		[XmlArrayItem(ElementName = "Contact", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(ContactType))]
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public ContactType[] Contacts { get; set; }

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06002CC7 RID: 11463 RVA: 0x000B1409 File Offset: 0x000AF609
		// (set) Token: 0x06002CC8 RID: 11464 RVA: 0x000B1411 File Offset: 0x000AF611
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		[XmlArrayItem(ElementName = "UrlEntity", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(UrlEntityType))]
		public UrlEntityType[] Urls { get; set; }

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06002CC9 RID: 11465 RVA: 0x000B141A File Offset: 0x000AF61A
		// (set) Token: 0x06002CCA RID: 11466 RVA: 0x000B1422 File Offset: 0x000AF622
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		[XmlArrayItem(ElementName = "Phone", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(PhoneEntityType))]
		public PhoneEntityType[] PhoneNumbers { get; set; }
	}
}
