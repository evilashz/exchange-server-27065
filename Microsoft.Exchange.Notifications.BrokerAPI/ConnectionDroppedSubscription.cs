using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000022 RID: 34
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConnectionDroppedSubscription : BaseSubscription
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00003744 File Offset: 0x00001944
		public ConnectionDroppedSubscription() : base(NotificationType.ConnectionDropped)
		{
		}

		// Token: 0x04000061 RID: 97
		public static readonly Guid WellKnownSubscriptionId = new Guid("D83F5839-3220-4F98-BDAD-CC4AF2B6B713");
	}
}
