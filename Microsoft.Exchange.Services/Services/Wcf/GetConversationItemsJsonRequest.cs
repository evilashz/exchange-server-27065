using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C41 RID: 3137
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetConversationItemsJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F98 RID: 12184
		[DataMember(IsRequired = true, Order = 0)]
		public GetConversationItemsRequest Body;
	}
}
