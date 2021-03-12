using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C44 RID: 3140
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetThreadedConversationItemsJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F9B RID: 12187
		[DataMember(IsRequired = true, Order = 0)]
		public GetThreadedConversationItemsResponse Body;
	}
}
