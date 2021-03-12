using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D3B RID: 3387
	[MessageContract(IsWrapped = false)]
	public class GetHoldOnMailboxesSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030CD RID: 12493
		[MessageBodyMember(Name = "GetHoldOnMailboxesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetHoldOnMailboxesResponse Body;
	}
}
