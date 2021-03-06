using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CEC RID: 3308
	[MessageContract(IsWrapped = false)]
	public class CreateUserConfigurationSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400307E RID: 12414
		[MessageBodyMember(Name = "CreateUserConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CreateUserConfigurationRequest Body;
	}
}
