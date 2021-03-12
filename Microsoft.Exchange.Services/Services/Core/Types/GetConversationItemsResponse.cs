using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.Types.Conversations;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004F3 RID: 1267
	[XmlType("GetConversationItemsResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetConversationItemsResponse : BaseInfoResponse
	{
		// Token: 0x060024D0 RID: 9424 RVA: 0x000A5312 File Offset: 0x000A3512
		public GetConversationItemsResponse() : base(ResponseType.GetConversationItemsResponseMessage)
		{
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x000A531C File Offset: 0x000A351C
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue item)
		{
			return new GetConversationItemsResponseMessage(code, error, item as ConversationResponseType);
		}
	}
}
