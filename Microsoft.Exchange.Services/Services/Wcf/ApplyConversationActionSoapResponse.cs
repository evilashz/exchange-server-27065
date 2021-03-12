using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D23 RID: 3363
	[MessageContract(IsWrapped = false)]
	public class ApplyConversationActionSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030B5 RID: 12469
		[MessageBodyMember(Name = "ApplyConversationActionResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public ApplyConversationActionResponse Body;
	}
}
