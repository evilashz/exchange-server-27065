using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000462 RID: 1122
	public enum SubscribeToNotificationMetadata
	{
		// Token: 0x040015D4 RID: 5588
		[DisplayName("SN", "UCL")]
		UserContextLatency,
		// Token: 0x040015D5 RID: 5589
		[DisplayName("SN", "C")]
		SubscriptionCount,
		// Token: 0x040015D6 RID: 5590
		[DisplayName("SN", "TZ")]
		RequestTimeZone,
		// Token: 0x040015D7 RID: 5591
		[DisplayName("SN", "HieL")]
		HierarchyNotificationLatency,
		// Token: 0x040015D8 RID: 5592
		[DisplayName("SN", "RemL")]
		ReminderNotificationLatency,
		// Token: 0x040015D9 RID: 5593
		[DisplayName("SN", "PoPL")]
		PlayOnPhoneNotificationLatency,
		// Token: 0x040015DA RID: 5594
		[DisplayName("SN", "RowL")]
		RowNotificationLatency,
		// Token: 0x040015DB RID: 5595
		[DisplayName("SN", "CalL")]
		CalendarItemNotificationLatency,
		// Token: 0x040015DC RID: 5596
		[DisplayName("SN", "PIKL")]
		PeopleIKnowNotificationLatency,
		// Token: 0x040015DD RID: 5597
		[DisplayName("SN", "IML")]
		InstantMessageNotificationLatency,
		// Token: 0x040015DE RID: 5598
		[DisplayName("SN", "NML")]
		NewMailNotificationLatency,
		// Token: 0x040015DF RID: 5599
		[DisplayName("SN", "UINL")]
		UnseenItemNotificationLatency,
		// Token: 0x040015E0 RID: 5600
		[DisplayName("SN", "GAL")]
		GroupAssociationNotificationLatency,
		// Token: 0x040015E1 RID: 5601
		[DisplayName("SN", "OL")]
		Other
	}
}
