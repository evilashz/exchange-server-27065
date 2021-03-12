using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D57 RID: 3415
	[MessageContract(IsWrapped = false)]
	public class DisableAppSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030E9 RID: 12521
		[MessageBodyMember(Name = "DisableAppResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public DisableAppResponse Body;
	}
}
