using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000803 RID: 2051
	public class MessageNotification : BaseNotification
	{
		// Token: 0x06003BF3 RID: 15347 RVA: 0x000D4F3A File Offset: 0x000D313A
		public MessageNotification() : base(NotificationKindType.Message)
		{
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06003BF4 RID: 15348 RVA: 0x000D4F43 File Offset: 0x000D3143
		// (set) Token: 0x06003BF5 RID: 15349 RVA: 0x000D4F4B File Offset: 0x000D314B
		public MessageType Message { get; set; }
	}
}
