using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200007A RID: 122
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IToDoItem : IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000892 RID: 2194
		// (set) Token: 0x06000893 RID: 2195
		string Subject { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000894 RID: 2196
		string InternetMessageId { get; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000895 RID: 2197
		// (set) Token: 0x06000896 RID: 2198
		Reminders<ModernReminder> ModernReminders { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000897 RID: 2199
		// (set) Token: 0x06000898 RID: 2200
		RemindersState<ModernReminderState> ModernRemindersState { get; set; }

		// Token: 0x06000899 RID: 2201
		GlobalObjectId GetGlobalObjectId();
	}
}
