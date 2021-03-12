using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D0B RID: 3339
	[MessageContract(IsWrapped = false)]
	public class GetSharingMetadataSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400309D RID: 12445
		[MessageBodyMember(Name = "GetSharingMetadataResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetSharingMetadataResponseMessage Body;
	}
}
