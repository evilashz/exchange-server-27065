using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C4A RID: 3146
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetUserRetentionPolicyTagsJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FA1 RID: 12193
		[DataMember(IsRequired = true, Order = 0)]
		public GetUserRetentionPolicyTagsResponse Body;
	}
}
