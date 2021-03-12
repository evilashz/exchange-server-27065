using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B20 RID: 2848
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetModernConversationAttachmentsResponseMessage : ResponseMessage
	{
		// Token: 0x060050C3 RID: 20675 RVA: 0x00109EEB File Offset: 0x001080EB
		public GetModernConversationAttachmentsResponseMessage()
		{
		}

		// Token: 0x060050C4 RID: 20676 RVA: 0x00109EF3 File Offset: 0x001080F3
		internal GetModernConversationAttachmentsResponseMessage(ServiceResultCode code, ServiceError error, ModernConversationAttachmentsResponseType conversationAttachments) : base(code, error)
		{
			this.ConversationAttachments = conversationAttachments;
		}

		// Token: 0x060050C5 RID: 20677 RVA: 0x00109F04 File Offset: 0x00108104
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetModernConversationAttachmentsResponseMessage;
		}

		// Token: 0x17001355 RID: 4949
		// (get) Token: 0x060050C6 RID: 20678 RVA: 0x00109F0B File Offset: 0x0010810B
		// (set) Token: 0x060050C7 RID: 20679 RVA: 0x00109F13 File Offset: 0x00108113
		[DataMember(EmitDefaultValue = false)]
		public ModernConversationAttachmentsResponseType ConversationAttachments { get; set; }
	}
}
