using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B14 RID: 2836
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal class EventTimeBasedInboxReminder : IReminder
	{
		// Token: 0x060066D1 RID: 26321 RVA: 0x001B4141 File Offset: 0x001B2341
		public EventTimeBasedInboxReminder()
		{
			this.Initialize();
		}

		// Token: 0x17001C41 RID: 7233
		// (get) Token: 0x060066D2 RID: 26322 RVA: 0x001B414F File Offset: 0x001B234F
		// (set) Token: 0x060066D3 RID: 26323 RVA: 0x001B4157 File Offset: 0x001B2357
		[DataMember]
		public Guid Identifier { get; set; }

		// Token: 0x17001C42 RID: 7234
		// (get) Token: 0x060066D4 RID: 26324 RVA: 0x001B4160 File Offset: 0x001B2360
		// (set) Token: 0x060066D5 RID: 26325 RVA: 0x001B4168 File Offset: 0x001B2368
		[DataMember]
		public int ReminderOffset
		{
			get
			{
				return this.reminderOffset;
			}
			set
			{
				if (value < -2628000 || value > 2628000)
				{
					throw new InvalidParamException(ServerStrings.InvalidReminderOffset(value, -2628000, 2628000));
				}
				this.reminderOffset = value;
			}
		}

		// Token: 0x17001C43 RID: 7235
		// (get) Token: 0x060066D6 RID: 26326 RVA: 0x001B4197 File Offset: 0x001B2397
		// (set) Token: 0x060066D7 RID: 26327 RVA: 0x001B419F File Offset: 0x001B239F
		[DataMember]
		public string CustomMessage
		{
			get
			{
				return this.customMessage;
			}
			set
			{
				this.customMessage = (value ?? string.Empty);
				if (this.customMessage.Length > 1024)
				{
					throw new InvalidParamException(ServerStrings.CustomMessageLengthExceeded);
				}
			}
		}

		// Token: 0x17001C44 RID: 7236
		// (get) Token: 0x060066D8 RID: 26328 RVA: 0x001B41CE File Offset: 0x001B23CE
		// (set) Token: 0x060066D9 RID: 26329 RVA: 0x001B41D6 File Offset: 0x001B23D6
		[DataMember]
		public bool IsOrganizerReminder { get; set; }

		// Token: 0x17001C45 RID: 7237
		// (get) Token: 0x060066DA RID: 26330 RVA: 0x001B41DF File Offset: 0x001B23DF
		// (set) Token: 0x060066DB RID: 26331 RVA: 0x001B41E7 File Offset: 0x001B23E7
		[DataMember]
		public EmailReminderChangeType OccurrenceChange { get; set; }

		// Token: 0x17001C46 RID: 7238
		// (get) Token: 0x060066DC RID: 26332 RVA: 0x001B41F0 File Offset: 0x001B23F0
		// (set) Token: 0x060066DD RID: 26333 RVA: 0x001B41F8 File Offset: 0x001B23F8
		[DataMember]
		public Guid SeriesReminderId { get; set; }

		// Token: 0x17001C47 RID: 7239
		// (get) Token: 0x060066DE RID: 26334 RVA: 0x001B4201 File Offset: 0x001B2401
		public bool IsFormerSeriesReminder
		{
			get
			{
				return this.OccurrenceChange == EmailReminderChangeType.Override || this.OccurrenceChange == EmailReminderChangeType.Deleted;
			}
		}

		// Token: 0x17001C48 RID: 7240
		// (get) Token: 0x060066DF RID: 26335 RVA: 0x001B4217 File Offset: 0x001B2417
		public bool IsActiveExceptionReminder
		{
			get
			{
				return this.OccurrenceChange == EmailReminderChangeType.Added || this.OccurrenceChange == EmailReminderChangeType.Override;
			}
		}

		// Token: 0x060066E0 RID: 26336 RVA: 0x001B4230 File Offset: 0x001B2430
		public static void UpdateIdentifiersForModifiedReminders(Reminders<EventTimeBasedInboxReminder> reminders)
		{
			if (reminders != null)
			{
				foreach (EventTimeBasedInboxReminder eventTimeBasedInboxReminder in reminders.ReminderList)
				{
					if (eventTimeBasedInboxReminder.IsFormerSeriesReminder && eventTimeBasedInboxReminder.SeriesReminderId == Guid.Empty)
					{
						eventTimeBasedInboxReminder.SeriesReminderId = eventTimeBasedInboxReminder.Identifier;
						eventTimeBasedInboxReminder.Identifier = Guid.NewGuid();
					}
				}
			}
		}

		// Token: 0x060066E1 RID: 26337 RVA: 0x001B42B0 File Offset: 0x001B24B0
		public static EventTimeBasedInboxReminder GetSeriesReminder(Reminders<EventTimeBasedInboxReminder> reminders, Guid reminderId)
		{
			foreach (EventTimeBasedInboxReminder eventTimeBasedInboxReminder in reminders.ReminderList)
			{
				Guid a = eventTimeBasedInboxReminder.IsFormerSeriesReminder ? eventTimeBasedInboxReminder.SeriesReminderId : eventTimeBasedInboxReminder.Identifier;
				if (a == reminderId)
				{
					return eventTimeBasedInboxReminder;
				}
			}
			return null;
		}

		// Token: 0x060066E2 RID: 26338 RVA: 0x001B4324 File Offset: 0x001B2524
		[OnDeserializing]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x060066E3 RID: 26339 RVA: 0x001B432C File Offset: 0x001B252C
		public int GetCurrentVersion()
		{
			return 3;
		}

		// Token: 0x060066E4 RID: 26340 RVA: 0x001B432F File Offset: 0x001B252F
		private void Initialize()
		{
			this.ReminderOffset = 10080;
			this.CustomMessage = string.Empty;
			this.IsOrganizerReminder = false;
			this.OccurrenceChange = EmailReminderChangeType.None;
			this.SeriesReminderId = Guid.Empty;
		}

		// Token: 0x04003A4B RID: 14923
		private const int DefaultReminderOffset = 10080;

		// Token: 0x04003A4C RID: 14924
		private const int MinReminderOffset = -2628000;

		// Token: 0x04003A4D RID: 14925
		private const int MaxReminderOffset = 2628000;

		// Token: 0x04003A4E RID: 14926
		private const int MaxCustomMessageLength = 1024;

		// Token: 0x04003A4F RID: 14927
		private const int CurrentVersion = 3;

		// Token: 0x04003A50 RID: 14928
		private int reminderOffset;

		// Token: 0x04003A51 RID: 14929
		private string customMessage;
	}
}
