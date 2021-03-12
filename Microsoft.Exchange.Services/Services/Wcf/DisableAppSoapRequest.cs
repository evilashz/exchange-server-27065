using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D56 RID: 3414
	[MessageContract(IsWrapped = false)]
	public class DisableAppSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030E8 RID: 12520
		[MessageBodyMember(Name = "DisableApp", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public DisableAppRequest Body;
	}
}
