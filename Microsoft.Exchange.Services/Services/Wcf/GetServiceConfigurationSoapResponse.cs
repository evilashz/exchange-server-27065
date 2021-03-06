using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CF5 RID: 3317
	[MessageContract(IsWrapped = false)]
	public class GetServiceConfigurationSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003087 RID: 12423
		[MessageBodyMember(Name = "GetServiceConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetServiceConfigurationResponseMessage Body;
	}
}
