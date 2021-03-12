using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C7E RID: 3198
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetUserUnifiedGroupsJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FD1 RID: 12241
		[DataMember(IsRequired = true, Order = 0)]
		public GetUserUnifiedGroupsRequest Body;
	}
}
