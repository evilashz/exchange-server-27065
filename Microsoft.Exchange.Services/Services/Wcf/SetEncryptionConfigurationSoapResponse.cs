using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D4F RID: 3407
	[MessageContract(IsWrapped = false)]
	public class SetEncryptionConfigurationSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030E1 RID: 12513
		[MessageBodyMember(Name = "SetEncryptionConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SetEncryptionConfigurationResponse Body;
	}
}
