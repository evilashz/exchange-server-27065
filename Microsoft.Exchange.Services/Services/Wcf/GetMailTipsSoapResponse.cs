using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CF7 RID: 3319
	[MessageContract(IsWrapped = false)]
	public class GetMailTipsSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003089 RID: 12425
		[MessageBodyMember(Name = "GetMailTipsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetMailTipsResponseMessage Body;
	}
}
