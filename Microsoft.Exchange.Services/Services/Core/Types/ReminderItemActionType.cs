using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200062E RID: 1582
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Name = "ReminderItemAction", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class ReminderItemActionType
	{
		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x0600314C RID: 12620 RVA: 0x000B6F3B File Offset: 0x000B513B
		// (set) Token: 0x0600314D RID: 12621 RVA: 0x000B6F43 File Offset: 0x000B5143
		[DataMember]
		[XmlElement("ActionType", Order = 1)]
		public ReminderActionType ActionType { get; set; }

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x0600314E RID: 12622 RVA: 0x000B6F4C File Offset: 0x000B514C
		// (set) Token: 0x0600314F RID: 12623 RVA: 0x000B6F54 File Offset: 0x000B5154
		[DataMember]
		[XmlElement("ItemId", Order = 2)]
		public ItemId ItemId { get; set; }

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06003150 RID: 12624 RVA: 0x000B6F5D File Offset: 0x000B515D
		// (set) Token: 0x06003151 RID: 12625 RVA: 0x000B6F65 File Offset: 0x000B5165
		[XmlElement("NewReminderTime", Order = 3)]
		[DataMember]
		public string NewReminderTime { get; set; }
	}
}
