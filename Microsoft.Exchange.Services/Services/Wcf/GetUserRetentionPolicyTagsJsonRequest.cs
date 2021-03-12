using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C49 RID: 3145
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetUserRetentionPolicyTagsJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FA0 RID: 12192
		[DataMember(IsRequired = true, Order = 0)]
		public GetUserRetentionPolicyTagsRequest Body;
	}
}
