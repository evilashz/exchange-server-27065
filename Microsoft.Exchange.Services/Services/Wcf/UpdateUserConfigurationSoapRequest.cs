using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CF2 RID: 3314
	[MessageContract(IsWrapped = false)]
	public class UpdateUserConfigurationSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003084 RID: 12420
		[MessageBodyMember(Name = "UpdateUserConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public UpdateUserConfigurationRequest Body;
	}
}
