using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200055F RID: 1375
	public class SubscribeToConversationChangesResponseMessage : ResponseMessage
	{
		// Token: 0x06002681 RID: 9857 RVA: 0x000A670D File Offset: 0x000A490D
		public SubscribeToConversationChangesResponseMessage()
		{
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x000A6715 File Offset: 0x000A4915
		internal SubscribeToConversationChangesResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x000A671F File Offset: 0x000A491F
		public override ResponseType GetResponseType()
		{
			return ResponseType.SubscribeToConversationChangesResponseMessage;
		}
	}
}
