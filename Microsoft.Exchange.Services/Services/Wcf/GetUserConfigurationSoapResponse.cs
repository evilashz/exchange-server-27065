using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CF1 RID: 3313
	[MessageContract(IsWrapped = false)]
	public class GetUserConfigurationSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003083 RID: 12419
		[MessageBodyMember(Name = "GetUserConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetUserConfigurationResponse Body;
	}
}
