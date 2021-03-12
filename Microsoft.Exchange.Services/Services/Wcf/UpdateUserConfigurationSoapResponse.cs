using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CF3 RID: 3315
	[MessageContract(IsWrapped = false)]
	public class UpdateUserConfigurationSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003085 RID: 12421
		[MessageBodyMember(Name = "UpdateUserConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public UpdateUserConfigurationResponse Body;
	}
}
