using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D4E RID: 3406
	[MessageContract(IsWrapped = false)]
	public class SetEncryptionConfigurationSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030E0 RID: 12512
		[MessageBodyMember(Name = "SetEncryptionConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SetEncryptionConfigurationRequest Body;
	}
}
