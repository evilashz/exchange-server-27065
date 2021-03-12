using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C42 RID: 3138
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetConversationItemsJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F99 RID: 12185
		[DataMember(IsRequired = true, Order = 0)]
		public GetConversationItemsResponse Body;
	}
}
