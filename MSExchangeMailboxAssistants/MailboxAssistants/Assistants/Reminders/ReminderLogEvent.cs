using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x0200024A RID: 586
	internal class ReminderLogEvent : ILogEvent
	{
		// Token: 0x060015FE RID: 5630 RVA: 0x0007B63C File Offset: 0x0007983C
		public ReminderLogEvent(ReminderLogEventType eventType, string objectClass, GlobalObjectId reminderItemId, Guid reminderId, ICollection<KeyValuePair<string, object>> additional = null)
		{
			this.eventType = eventType;
			this.objectClass = objectClass;
			this.reminderItemId = reminderItemId;
			this.reminderId = reminderId;
			this.additional = additional;
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x0007B669 File Offset: 0x00079869
		public string EventId
		{
			get
			{
				return this.eventType.ToString();
			}
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x0007B67C File Offset: 0x0007987C
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			ICollection<KeyValuePair<string, object>> collection = new Dictionary<string, object>
			{
				{
					"A",
					this.eventType.ToString()
				},
				{
					"C",
					this.objectClass.ToString()
				},
				{
					"IID",
					this.reminderItemId.ToString()
				},
				{
					"RID",
					this.reminderId
				}
			};
			if (this.additional != null && this.additional.Count > 0)
			{
				foreach (KeyValuePair<string, object> item in this.additional)
				{
					collection.Add(item);
				}
			}
			return collection;
		}

		// Token: 0x04000CE8 RID: 3304
		public const string FieldEventType = "A";

		// Token: 0x04000CE9 RID: 3305
		public const string FieldObjectClass = "C";

		// Token: 0x04000CEA RID: 3306
		public const string FieldReminderItemId = "IID";

		// Token: 0x04000CEB RID: 3307
		public const string FieldReminderId = "RID";

		// Token: 0x04000CEC RID: 3308
		public const string FieldReminderReceivedTime = "RRT";

		// Token: 0x04000CED RID: 3309
		public const string FieldMessageLength = "LEN";

		// Token: 0x04000CEE RID: 3310
		public const string FieldReminderEventTime = "ET";

		// Token: 0x04000CEF RID: 3311
		public const string FieldReminderTime = "RT";

		// Token: 0x04000CF0 RID: 3312
		public const string FieldReminderSendTime = "ST";

		// Token: 0x04000CF1 RID: 3313
		public const string FieldReminderTimeOffset = "OF";

		// Token: 0x04000CF2 RID: 3314
		public const string FieldIsOrganizerReminder = "ORG";

		// Token: 0x04000CF3 RID: 3315
		public const string FieldReminderHasState = "HS";

		// Token: 0x04000CF4 RID: 3316
		public const string FieldIsRecurring = "IR";

		// Token: 0x04000CF5 RID: 3317
		public const string FieldOccurrenceChange = "OC";

		// Token: 0x04000CF6 RID: 3318
		public const string FieldMailboxId = "MID";

		// Token: 0x04000CF7 RID: 3319
		public const string FormatStringTimeSpan = "c";

		// Token: 0x04000CF8 RID: 3320
		public const string FormatStringTime = "o";

		// Token: 0x04000CF9 RID: 3321
		private readonly ReminderLogEventType eventType;

		// Token: 0x04000CFA RID: 3322
		private readonly string objectClass;

		// Token: 0x04000CFB RID: 3323
		private readonly Guid reminderId;

		// Token: 0x04000CFC RID: 3324
		private readonly GlobalObjectId reminderItemId;

		// Token: 0x04000CFD RID: 3325
		private readonly ICollection<KeyValuePair<string, object>> additional;
	}
}
