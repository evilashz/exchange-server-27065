using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D49 RID: 3401
	[MessageContract(IsWrapped = false)]
	public class GetClientExtensionSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030DB RID: 12507
		[MessageBodyMember(Name = "GetClientExtensionResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetClientExtensionResponse Body;
	}
}
