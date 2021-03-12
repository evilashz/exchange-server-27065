using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D22 RID: 3362
	[MessageContract(IsWrapped = false)]
	public class ApplyConversationActionSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030B4 RID: 12468
		[MessageBodyMember(Name = "ApplyConversationAction", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public ApplyConversationActionRequest Body;
	}
}
