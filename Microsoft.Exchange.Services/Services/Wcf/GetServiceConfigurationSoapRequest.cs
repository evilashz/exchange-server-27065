using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CF4 RID: 3316
	[MessageContract(IsWrapped = false)]
	public class GetServiceConfigurationSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003086 RID: 12422
		[MessageBodyMember(Name = "GetServiceConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetServiceConfigurationRequest Body;
	}
}
