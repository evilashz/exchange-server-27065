using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B1F RID: 2847
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetModernConversationAttachmentsResponse : BaseInfoResponse
	{
		// Token: 0x060050C1 RID: 20673 RVA: 0x00109ECA File Offset: 0x001080CA
		public GetModernConversationAttachmentsResponse() : base(ResponseType.GetModernConversationAttachmentsResponseMessage)
		{
		}

		// Token: 0x060050C2 RID: 20674 RVA: 0x00109ED7 File Offset: 0x001080D7
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue item)
		{
			return new GetModernConversationAttachmentsResponseMessage(code, error, item as ModernConversationAttachmentsResponseType);
		}
	}
}
