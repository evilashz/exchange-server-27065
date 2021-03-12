using System;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000098 RID: 152
	internal class CalendarSettings
	{
		// Token: 0x0600052D RID: 1325 RVA: 0x0001C3B9 File Offset: 0x0001A5B9
		public CalendarSettings(CalendarFlags flags, int reminderTime)
		{
			this.settingsFlags = flags;
			this.defaultReminderTime = reminderTime;
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0001C3CF File Offset: 0x0001A5CF
		// (set) Token: 0x0600052F RID: 1327 RVA: 0x0001C3D7 File Offset: 0x0001A5D7
		public CalendarFlags Flags
		{
			get
			{
				return this.settingsFlags;
			}
			set
			{
				this.settingsFlags = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0001C3E0 File Offset: 0x0001A5E0
		public bool AutomaticBooking
		{
			get
			{
				return (this.settingsFlags & CalendarFlags.AutoBooking) == CalendarFlags.AutoBooking;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0001C3ED File Offset: 0x0001A5ED
		public bool CalendarAssistantActive
		{
			get
			{
				return (this.settingsFlags & CalendarFlags.CalendarAssistantActive) == CalendarFlags.CalendarAssistantActive;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0001C3FA File Offset: 0x0001A5FA
		public bool CalendarAssistantNoiseReduction
		{
			get
			{
				return (this.settingsFlags & CalendarFlags.CalendarAssistantNoiseReduction) == CalendarFlags.CalendarAssistantNoiseReduction;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0001C407 File Offset: 0x0001A607
		public bool CalendarAssistantAddNewItems
		{
			get
			{
				return (this.settingsFlags & CalendarFlags.CalendarAssistantAddNewItems) == CalendarFlags.CalendarAssistantAddNewItems;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001C414 File Offset: 0x0001A614
		public bool CalendarAssistantProcessExternal
		{
			get
			{
				return (this.settingsFlags & CalendarFlags.CalendarAssistantProcessExternal) == CalendarFlags.CalendarAssistantProcessExternal;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0001C423 File Offset: 0x0001A623
		public bool SkipProcessing
		{
			get
			{
				return (this.settingsFlags & CalendarFlags.SkipProcessing) == CalendarFlags.SkipProcessing;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0001C432 File Offset: 0x0001A632
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x0001C43A File Offset: 0x0001A63A
		public int DefaultReminderTime
		{
			get
			{
				return this.defaultReminderTime;
			}
			set
			{
				this.defaultReminderTime = value;
			}
		}

		// Token: 0x040002D5 RID: 725
		private CalendarFlags settingsFlags;

		// Token: 0x040002D6 RID: 726
		private int defaultReminderTime;
	}
}
