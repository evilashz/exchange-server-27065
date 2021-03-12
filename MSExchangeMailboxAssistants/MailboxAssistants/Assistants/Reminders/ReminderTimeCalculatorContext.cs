using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x02000258 RID: 600
	internal class ReminderTimeCalculatorContext
	{
		// Token: 0x0600165E RID: 5726 RVA: 0x0007E017 File Offset: 0x0007C217
		internal ReminderTimeCalculatorContext(StorageWorkingHours workingHours, DayOfWeek firstDayOfWeek, ExTimeZone timeZone)
		{
			this.workingHours = workingHours;
			this.startOfWeek = firstDayOfWeek;
			this.timeZone = timeZone;
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x0007E034 File Offset: 0x0007C234
		public StorageWorkingHours WorkingHours
		{
			get
			{
				return this.workingHours;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x0007E03C File Offset: 0x0007C23C
		public DayOfWeek StartOfWeek
		{
			get
			{
				return this.startOfWeek;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x0007E044 File Offset: 0x0007C244
		public ExTimeZone TimeZone
		{
			get
			{
				return this.timeZone;
			}
		}

		// Token: 0x04000D1E RID: 3358
		private readonly StorageWorkingHours workingHours;

		// Token: 0x04000D1F RID: 3359
		private readonly DayOfWeek startOfWeek;

		// Token: 0x04000D20 RID: 3360
		private readonly ExTimeZone timeZone;
	}
}
