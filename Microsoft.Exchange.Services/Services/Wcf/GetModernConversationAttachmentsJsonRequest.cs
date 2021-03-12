using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C47 RID: 3143
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetModernConversationAttachmentsJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F9E RID: 12190
		[DataMember(IsRequired = true, Order = 0)]
		public GetModernConversationAttachmentsRequest Body;
	}
}
