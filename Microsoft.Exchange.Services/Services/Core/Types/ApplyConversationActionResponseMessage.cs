using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004AD RID: 1197
	[XmlType(TypeName = "ApplyConversationActionResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class ApplyConversationActionResponseMessage : ResponseMessage
	{
		// Token: 0x060023C1 RID: 9153 RVA: 0x000A432B File Offset: 0x000A252B
		public ApplyConversationActionResponseMessage()
		{
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x000A4333 File Offset: 0x000A2533
		internal ApplyConversationActionResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060023C3 RID: 9155 RVA: 0x000A433D File Offset: 0x000A253D
		// (set) Token: 0x060023C4 RID: 9156 RVA: 0x000A4345 File Offset: 0x000A2545
		[DataMember(EmitDefaultValue = false, Name = "MovedItemIds")]
		[XmlIgnore]
		public ItemId[] MovedItemIds { get; set; }
	}
}
