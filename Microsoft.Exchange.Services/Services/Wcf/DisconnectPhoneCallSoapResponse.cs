using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CFD RID: 3325
	[MessageContract(IsWrapped = false)]
	public class DisconnectPhoneCallSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400308F RID: 12431
		[MessageBodyMember(Name = "DisconnectPhoneCallResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public DisconnectPhoneCallResponseMessage Body;
	}
}
