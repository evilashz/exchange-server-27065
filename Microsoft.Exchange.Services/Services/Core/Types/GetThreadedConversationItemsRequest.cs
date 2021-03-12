using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Conversations;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004A0 RID: 1184
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class GetThreadedConversationItemsRequest : GetConversationItemsRequest
	{
		// Token: 0x0600237D RID: 9085 RVA: 0x000A3F75 File Offset: 0x000A2175
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetThreadedConversationItems(callContext, this);
		}
	}
}
