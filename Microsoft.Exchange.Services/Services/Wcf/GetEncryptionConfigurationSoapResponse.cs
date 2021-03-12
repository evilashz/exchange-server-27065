using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D4D RID: 3405
	[MessageContract(IsWrapped = false)]
	public class GetEncryptionConfigurationSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030DF RID: 12511
		[MessageBodyMember(Name = "GetEncryptionConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetEncryptionConfigurationResponse Body;
	}
}
