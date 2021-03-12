using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D39 RID: 3385
	[MessageContract(IsWrapped = false)]
	public class GetDiscoverySearchConfigurationSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030CB RID: 12491
		[MessageBodyMember(Name = "GetDiscoverySearchConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetDiscoverySearchConfigurationResponse Body;
	}
}
