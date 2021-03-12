using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CFB RID: 3323
	[MessageContract(IsWrapped = false)]
	public class GetPhoneCallInformationSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400308D RID: 12429
		[MessageBodyMember(Name = "GetPhoneCallInformationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetPhoneCallInformationResponseMessage Body;
	}
}
