using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200072F RID: 1839
	public class ConversationNotification : BaseNotification
	{
		// Token: 0x0600379F RID: 14239 RVA: 0x000C5B59 File Offset: 0x000C3D59
		public ConversationNotification() : base(NotificationKindType.Conversation)
		{
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x060037A0 RID: 14240 RVA: 0x000C5B62 File Offset: 0x000C3D62
		// (set) Token: 0x060037A1 RID: 14241 RVA: 0x000C5B6A File Offset: 0x000C3D6A
		public ConversationType Conversation { get; set; }
	}
}
