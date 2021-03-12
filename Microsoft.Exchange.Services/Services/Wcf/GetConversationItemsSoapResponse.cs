using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D77 RID: 3447
	[MessageContract(IsWrapped = false)]
	public class GetConversationItemsSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003109 RID: 12553
		[MessageBodyMember(Name = "GetConversationItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetConversationItemsResponse Body;
	}
}
