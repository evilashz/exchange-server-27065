using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types.Conversations;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000582 RID: 1410
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetThreadedConversationItemsResponseMessage : ResponseMessage
	{
		// Token: 0x06002725 RID: 10021 RVA: 0x000A6F6B File Offset: 0x000A516B
		public GetThreadedConversationItemsResponseMessage()
		{
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x000A6F73 File Offset: 0x000A5173
		internal GetThreadedConversationItemsResponseMessage(ServiceResultCode code, ServiceError error, ThreadedConversationResponseType conversation) : base(code, error)
		{
			this.Conversation = conversation;
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x000A6F84 File Offset: 0x000A5184
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetThreadedConversationItemsResponseMessage;
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06002728 RID: 10024 RVA: 0x000A6F88 File Offset: 0x000A5188
		// (set) Token: 0x06002729 RID: 10025 RVA: 0x000A6F90 File Offset: 0x000A5190
		[DataMember(EmitDefaultValue = false)]
		public ThreadedConversationResponseType Conversation { get; set; }
	}
}
