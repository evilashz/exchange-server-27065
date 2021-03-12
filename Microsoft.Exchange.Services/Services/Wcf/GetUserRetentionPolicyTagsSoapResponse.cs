using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D79 RID: 3449
	[MessageContract(IsWrapped = false)]
	public class GetUserRetentionPolicyTagsSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400310B RID: 12555
		[MessageBodyMember(Name = "GetUserRetentionPolicyTagsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetUserRetentionPolicyTagsResponse Body;
	}
}
