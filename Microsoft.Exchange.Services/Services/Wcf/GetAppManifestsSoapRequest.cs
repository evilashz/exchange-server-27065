using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D50 RID: 3408
	[MessageContract(IsWrapped = false)]
	public class GetAppManifestsSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030E2 RID: 12514
		[MessageBodyMember(Name = "GetAppManifests", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetAppManifestsRequest Body;
	}
}
