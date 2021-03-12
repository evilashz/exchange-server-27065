using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CEF RID: 3311
	[MessageContract(IsWrapped = false)]
	public class DeleteUserConfigurationSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003081 RID: 12417
		[MessageBodyMember(Name = "DeleteUserConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public DeleteUserConfigurationResponse Body;
	}
}
