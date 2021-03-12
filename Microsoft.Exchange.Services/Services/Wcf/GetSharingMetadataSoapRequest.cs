using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D0A RID: 3338
	[MessageContract(IsWrapped = false)]
	public class GetSharingMetadataSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400309C RID: 12444
		[MessageBodyMember(Name = "GetSharingMetadata", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetSharingMetadataRequest Body;
	}
}
