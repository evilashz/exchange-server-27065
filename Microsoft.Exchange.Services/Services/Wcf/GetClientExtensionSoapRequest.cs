using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D48 RID: 3400
	[MessageContract(IsWrapped = false)]
	public class GetClientExtensionSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030DA RID: 12506
		[MessageBodyMember(Name = "GetClientExtension", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetClientExtensionRequest Body;
	}
}
