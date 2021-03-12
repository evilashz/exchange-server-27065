using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CF0 RID: 3312
	[MessageContract(IsWrapped = false)]
	public class GetUserConfigurationSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003082 RID: 12418
		[MessageBodyMember(Name = "GetUserConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetUserConfigurationRequest Body;
	}
}
