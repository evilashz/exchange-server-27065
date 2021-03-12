using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D8A RID: 3466
	[MessageContract(IsWrapped = false)]
	public class ValidateUMPinSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400311C RID: 12572
		[MessageBodyMember(Name = "ValidateUMPin", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public ValidateUMPinRequest Body;
	}
}
