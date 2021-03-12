using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CF6 RID: 3318
	[MessageContract(IsWrapped = false)]
	public class GetMailTipsSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003088 RID: 12424
		[MessageBodyMember(Name = "GetMailTips", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetMailTipsRequest Body;
	}
}
