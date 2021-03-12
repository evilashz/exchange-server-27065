using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009DD RID: 2525
	public enum CalendarNotificationType
	{
		// Token: 0x040033CC RID: 13260
		[LocDescription(ServerStrings.IDs.CalNotifTypeUninteresting)]
		Uninteresting,
		// Token: 0x040033CD RID: 13261
		[LocDescription(ServerStrings.IDs.CalNotifTypeSummary)]
		Summary,
		// Token: 0x040033CE RID: 13262
		[LocDescription(ServerStrings.IDs.CalNotifTypeReminder)]
		Reminder,
		// Token: 0x040033CF RID: 13263
		[LocDescription(ServerStrings.IDs.CalNotifTypeNewUpdate)]
		NewUpdate,
		// Token: 0x040033D0 RID: 13264
		[LocDescription(ServerStrings.IDs.CalNotifTypeChangedUpdate)]
		ChangedUpdate,
		// Token: 0x040033D1 RID: 13265
		[LocDescription(ServerStrings.IDs.CalNotifTypeDeletedUpdate)]
		DeletedUpdate
	}
}
