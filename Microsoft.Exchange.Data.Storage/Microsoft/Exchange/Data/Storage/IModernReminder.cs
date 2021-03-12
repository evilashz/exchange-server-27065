using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B1A RID: 2842
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IModernReminder : IReminder
	{
		// Token: 0x17001C4D RID: 7245
		// (get) Token: 0x060066F4 RID: 26356
		// (set) Token: 0x060066F5 RID: 26357
		ReminderTimeHint ReminderTimeHint { get; set; }

		// Token: 0x17001C4E RID: 7246
		// (get) Token: 0x060066F6 RID: 26358
		// (set) Token: 0x060066F7 RID: 26359
		Hours Hours { get; set; }

		// Token: 0x17001C4F RID: 7247
		// (get) Token: 0x060066F8 RID: 26360
		// (set) Token: 0x060066F9 RID: 26361
		Priority Priority { get; set; }

		// Token: 0x17001C50 RID: 7248
		// (get) Token: 0x060066FA RID: 26362
		// (set) Token: 0x060066FB RID: 26363
		int Duration { get; set; }

		// Token: 0x17001C51 RID: 7249
		// (get) Token: 0x060066FC RID: 26364
		// (set) Token: 0x060066FD RID: 26365
		ExDateTime ReferenceTime { get; set; }

		// Token: 0x17001C52 RID: 7250
		// (get) Token: 0x060066FE RID: 26366
		// (set) Token: 0x060066FF RID: 26367
		ExDateTime CustomReminderTime { get; set; }

		// Token: 0x17001C53 RID: 7251
		// (get) Token: 0x06006700 RID: 26368
		// (set) Token: 0x06006701 RID: 26369
		ExDateTime DueDate { get; set; }
	}
}
