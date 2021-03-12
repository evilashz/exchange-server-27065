using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CED RID: 3309
	[MessageContract(IsWrapped = false)]
	public class CreateUserConfigurationSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400307F RID: 12415
		[MessageBodyMember(Name = "CreateUserConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CreateUserConfigurationResponse Body;
	}
}
