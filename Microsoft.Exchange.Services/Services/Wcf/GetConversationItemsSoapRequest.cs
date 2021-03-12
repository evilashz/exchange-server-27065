using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D76 RID: 3446
	[MessageContract(IsWrapped = false)]
	public class GetConversationItemsSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003108 RID: 12552
		[MessageBodyMember(Name = "GetConversationItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetConversationItemsRequest Body;
	}
}
