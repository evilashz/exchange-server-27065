using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.Types.Conversations;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004F4 RID: 1268
	[XmlType("GetConversationItemsResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetConversationItemsResponseMessage : ResponseMessage
	{
		// Token: 0x060024D2 RID: 9426 RVA: 0x000A5330 File Offset: 0x000A3530
		public GetConversationItemsResponseMessage()
		{
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000A5338 File Offset: 0x000A3538
		internal GetConversationItemsResponseMessage(ServiceResultCode code, ServiceError error, ConversationResponseType conversation) : base(code, error)
		{
			this.Conversation = conversation;
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000A5349 File Offset: 0x000A3549
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetConversationItemsResponseMessage;
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x060024D5 RID: 9429 RVA: 0x000A534D File Offset: 0x000A354D
		// (set) Token: 0x060024D6 RID: 9430 RVA: 0x000A5355 File Offset: 0x000A3555
		[DataMember(EmitDefaultValue = false)]
		[XmlElement(ElementName = "Conversation")]
		public ConversationResponseType Conversation { get; set; }
	}
}
