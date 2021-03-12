using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007E1 RID: 2017
	[DataContract(Name = "ItemChange", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "ItemChangeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class ItemChange : StoreObjectChangeBase
	{
		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06003B44 RID: 15172 RVA: 0x000CFF16 File Offset: 0x000CE116
		// (set) Token: 0x06003B45 RID: 15173 RVA: 0x000CFF1E File Offset: 0x000CE11E
		[XmlElement("RecurringMasterItemId", typeof(RecurringMasterItemId))]
		[XmlElement("ItemId", typeof(ItemId))]
		[DataMember(Name = "ItemId", IsRequired = true)]
		[XmlElement("OccurrenceItemId", typeof(OccurrenceItemId))]
		public BaseItemId ItemId { get; set; }

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06003B46 RID: 15174 RVA: 0x000CFF27 File Offset: 0x000CE127
		// (set) Token: 0x06003B47 RID: 15175 RVA: 0x000CFF2F File Offset: 0x000CE12F
		[XmlIgnore]
		[IgnoreDataMember]
		public bool ChangesAlreadyProcessed { get; set; }
	}
}
