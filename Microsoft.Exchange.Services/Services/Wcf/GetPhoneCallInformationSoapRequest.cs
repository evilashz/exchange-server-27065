using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CFA RID: 3322
	[MessageContract(IsWrapped = false)]
	public class GetPhoneCallInformationSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400308C RID: 12428
		[MessageBodyMember(Name = "GetPhoneCallInformation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetPhoneCallInformationRequest Body;
	}
}
