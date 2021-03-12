using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D20 RID: 3360
	[MessageContract(IsWrapped = false)]
	public class FindConversationSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030B2 RID: 12466
		[MessageBodyMember(Name = "FindConversation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public FindConversationRequest Body;
	}
}
