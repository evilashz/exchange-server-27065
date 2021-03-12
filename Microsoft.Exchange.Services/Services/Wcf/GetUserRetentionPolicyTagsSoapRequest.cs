using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D78 RID: 3448
	[MessageContract(IsWrapped = false)]
	public class GetUserRetentionPolicyTagsSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400310A RID: 12554
		[MessageBodyMember(Name = "GetUserRetentionPolicyTags", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetUserRetentionPolicyTagsRequest Body;
	}
}
