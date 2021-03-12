using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C48 RID: 3144
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetModernConversationAttachmentsJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F9F RID: 12191
		[DataMember(IsRequired = true, Order = 0)]
		public GetModernConversationAttachmentsResponse Body;
	}
}
