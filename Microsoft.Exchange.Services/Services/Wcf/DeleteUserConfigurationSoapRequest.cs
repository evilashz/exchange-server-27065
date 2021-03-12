using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CEE RID: 3310
	[MessageContract(IsWrapped = false)]
	public class DeleteUserConfigurationSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003080 RID: 12416
		[MessageBodyMember(Name = "DeleteUserConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public DeleteUserConfigurationRequest Body;
	}
}
