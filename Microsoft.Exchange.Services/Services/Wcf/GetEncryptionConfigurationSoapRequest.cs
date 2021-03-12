using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D4C RID: 3404
	[MessageContract(IsWrapped = false)]
	public class GetEncryptionConfigurationSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030DE RID: 12510
		[MessageBodyMember(Name = "GetEncryptionConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetEncryptionConfigurationRequest Body;
	}
}
