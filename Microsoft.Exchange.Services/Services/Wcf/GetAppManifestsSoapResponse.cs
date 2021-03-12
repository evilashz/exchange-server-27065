using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D51 RID: 3409
	[MessageContract(IsWrapped = false)]
	public class GetAppManifestsSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030E3 RID: 12515
		[MessageBodyMember(Name = "GetAppManifestsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetAppManifestsResponse Body;
	}
}
