using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000535 RID: 1333
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "PerformReminderActionResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class PerformReminderActionResponse : ResponseMessage
	{
		// Token: 0x060025F6 RID: 9718 RVA: 0x000A619B File Offset: 0x000A439B
		public PerformReminderActionResponse()
		{
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x000A61A3 File Offset: 0x000A43A3
		internal PerformReminderActionResponse(ServiceResultCode code, ServiceError error, ItemId[] newItemIds) : base(code, error)
		{
			this.ItemIds = newItemIds;
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060025F8 RID: 9720 RVA: 0x000A61B4 File Offset: 0x000A43B4
		// (set) Token: 0x060025F9 RID: 9721 RVA: 0x000A61BC File Offset: 0x000A43BC
		[DataMember(EmitDefaultValue = false, Order = 1)]
		[XmlArrayItem("ItemId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(ItemId))]
		[XmlArray("UpdatedItemIds", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ItemId[] ItemIds { get; set; }

		// Token: 0x060025FA RID: 9722 RVA: 0x000A61C5 File Offset: 0x000A43C5
		public override ResponseType GetResponseType()
		{
			return ResponseType.PerformReminderActionResponseMessage;
		}
	}
}
