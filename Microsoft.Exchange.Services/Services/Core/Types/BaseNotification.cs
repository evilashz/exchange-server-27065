using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006E2 RID: 1762
	public class BaseNotification
	{
		// Token: 0x060035FE RID: 13822 RVA: 0x000C1C5E File Offset: 0x000BFE5E
		public BaseNotification(NotificationKindType notificationKind)
		{
			this.NotificationKind = notificationKind;
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x000C1C6D File Offset: 0x000BFE6D
		public BaseNotification()
		{
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06003600 RID: 13824 RVA: 0x000C1C75 File Offset: 0x000BFE75
		// (set) Token: 0x06003601 RID: 13825 RVA: 0x000C1C7D File Offset: 0x000BFE7D
		public NotificationKindType NotificationKind { get; private set; }

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06003602 RID: 13826 RVA: 0x000C1C86 File Offset: 0x000BFE86
		// (set) Token: 0x06003603 RID: 13827 RVA: 0x000C1C8E File Offset: 0x000BFE8E
		public NotificationTypeType NotificationType { get; set; }
	}
}
