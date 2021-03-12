using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types.Conversations;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000581 RID: 1409
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetThreadedConversationItemsResponse : BaseInfoResponse
	{
		// Token: 0x06002723 RID: 10019 RVA: 0x000A6F4D File Offset: 0x000A514D
		public GetThreadedConversationItemsResponse() : base(ResponseType.GetThreadedConversationItemsResponseMessage)
		{
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x000A6F57 File Offset: 0x000A5157
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue item)
		{
			return new GetThreadedConversationItemsResponseMessage(code, error, item as ThreadedConversationResponseType);
		}
	}
}
