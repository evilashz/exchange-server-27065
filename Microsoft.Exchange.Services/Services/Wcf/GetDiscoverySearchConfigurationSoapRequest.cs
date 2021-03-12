using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D38 RID: 3384
	[MessageContract(IsWrapped = false)]
	public class GetDiscoverySearchConfigurationSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030CA RID: 12490
		[MessageBodyMember(Name = "GetDiscoverySearchConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetDiscoverySearchConfigurationRequest Body;
	}
}
