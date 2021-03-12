using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D21 RID: 3361
	[MessageContract(IsWrapped = false)]
	public class FindConversationSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030B3 RID: 12467
		[MessageBodyMember(Name = "FindConversationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public FindConversationResponseMessage Body;
	}
}
