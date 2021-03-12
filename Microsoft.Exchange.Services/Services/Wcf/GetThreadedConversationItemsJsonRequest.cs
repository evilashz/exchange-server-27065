using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C43 RID: 3139
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetThreadedConversationItemsJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F9A RID: 12186
		[DataMember(IsRequired = true, Order = 0)]
		public GetThreadedConversationItemsRequest Body;
	}
}
