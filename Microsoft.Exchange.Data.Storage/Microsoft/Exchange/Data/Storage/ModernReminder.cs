using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B1B RID: 2843
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal class ModernReminder : IModernReminder, IReminder
	{
		// Token: 0x06006702 RID: 26370 RVA: 0x001B43DB File Offset: 0x001B25DB
		public ModernReminder()
		{
			this.Initialize();
		}

		// Token: 0x17001C54 RID: 7252
		// (get) Token: 0x06006703 RID: 26371 RVA: 0x001B43E9 File Offset: 0x001B25E9
		// (set) Token: 0x06006704 RID: 26372 RVA: 0x001B43F1 File Offset: 0x001B25F1
		[DataMember]
		public Guid Identifier { get; set; }

		// Token: 0x17001C55 RID: 7253
		// (get) Token: 0x06006705 RID: 26373 RVA: 0x001B43FA File Offset: 0x001B25FA
		// (set) Token: 0x06006706 RID: 26374 RVA: 0x001B4402 File Offset: 0x001B2602
		[DataMember]
		public ReminderTimeHint ReminderTimeHint { get; set; }

		// Token: 0x17001C56 RID: 7254
		// (get) Token: 0x06006707 RID: 26375 RVA: 0x001B440B File Offset: 0x001B260B
		// (set) Token: 0x06006708 RID: 26376 RVA: 0x001B4413 File Offset: 0x001B2613
		[DataMember]
		public Hours Hours { get; set; }

		// Token: 0x17001C57 RID: 7255
		// (get) Token: 0x06006709 RID: 26377 RVA: 0x001B441C File Offset: 0x001B261C
		// (set) Token: 0x0600670A RID: 26378 RVA: 0x001B4424 File Offset: 0x001B2624
		[DataMember]
		public Priority Priority { get; set; }

		// Token: 0x17001C58 RID: 7256
		// (get) Token: 0x0600670B RID: 26379 RVA: 0x001B442D File Offset: 0x001B262D
		// (set) Token: 0x0600670C RID: 26380 RVA: 0x001B4435 File Offset: 0x001B2635
		[DataMember]
		public int Duration
		{
			get
			{
				return this.duration;
			}
			set
			{
				if (value < 0)
				{
					throw new InvalidParamException(ServerStrings.InvalidDuration(value));
				}
				this.duration = value;
			}
		}

		// Token: 0x17001C59 RID: 7257
		// (get) Token: 0x0600670D RID: 26381 RVA: 0x001B444E File Offset: 0x001B264E
		// (set) Token: 0x0600670E RID: 26382 RVA: 0x001B445C File Offset: 0x001B265C
		[IgnoreDataMember]
		public ExDateTime ReferenceTime
		{
			get
			{
				return this.ToExDateTime(this.InternalReferenceTime);
			}
			set
			{
				this.InternalReferenceTime = this.ToDateTime(value);
			}
		}

		// Token: 0x17001C5A RID: 7258
		// (get) Token: 0x0600670F RID: 26383 RVA: 0x001B446B File Offset: 0x001B266B
		// (set) Token: 0x06006710 RID: 26384 RVA: 0x001B4479 File Offset: 0x001B2679
		[IgnoreDataMember]
		public ExDateTime CustomReminderTime
		{
			get
			{
				return this.ToExDateTime(this.InternalCustomReminderTime);
			}
			set
			{
				this.InternalCustomReminderTime = this.ToDateTime(value);
			}
		}

		// Token: 0x17001C5B RID: 7259
		// (get) Token: 0x06006711 RID: 26385 RVA: 0x001B4488 File Offset: 0x001B2688
		// (set) Token: 0x06006712 RID: 26386 RVA: 0x001B4496 File Offset: 0x001B2696
		[IgnoreDataMember]
		public ExDateTime DueDate
		{
			get
			{
				return this.ToExDateTime(this.InternalDueDate);
			}
			set
			{
				this.InternalDueDate = this.ToDateTime(value);
			}
		}

		// Token: 0x17001C5C RID: 7260
		// (get) Token: 0x06006713 RID: 26387 RVA: 0x001B44A5 File Offset: 0x001B26A5
		// (set) Token: 0x06006714 RID: 26388 RVA: 0x001B44AD File Offset: 0x001B26AD
		[DataMember]
		private DateTime InternalReferenceTime { get; set; }

		// Token: 0x17001C5D RID: 7261
		// (get) Token: 0x06006715 RID: 26389 RVA: 0x001B44B6 File Offset: 0x001B26B6
		// (set) Token: 0x06006716 RID: 26390 RVA: 0x001B44BE File Offset: 0x001B26BE
		[DataMember]
		private DateTime InternalCustomReminderTime { get; set; }

		// Token: 0x17001C5E RID: 7262
		// (get) Token: 0x06006717 RID: 26391 RVA: 0x001B44C7 File Offset: 0x001B26C7
		// (set) Token: 0x06006718 RID: 26392 RVA: 0x001B44CF File Offset: 0x001B26CF
		[DataMember]
		private DateTime InternalDueDate { get; set; }

		// Token: 0x06006719 RID: 26393 RVA: 0x001B44D8 File Offset: 0x001B26D8
		[OnDeserializing]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x0600671A RID: 26394 RVA: 0x001B44E0 File Offset: 0x001B26E0
		[OnDeserialized]
		public void OnDeserialized(StreamingContext context)
		{
			this.Validate();
		}

		// Token: 0x0600671B RID: 26395 RVA: 0x001B44E8 File Offset: 0x001B26E8
		[OnSerializing]
		public void OnSerializing(StreamingContext context)
		{
			this.Validate();
		}

		// Token: 0x0600671C RID: 26396 RVA: 0x001B44F0 File Offset: 0x001B26F0
		public int GetCurrentVersion()
		{
			return 1;
		}

		// Token: 0x0600671D RID: 26397 RVA: 0x001B44F3 File Offset: 0x001B26F3
		private void Initialize()
		{
			this.ReminderTimeHint = ReminderTimeHint.Tomorrow;
			this.Hours = Hours.Any;
			this.Priority = Priority.Normal;
			this.Duration = 30;
			this.CustomReminderTime = ModernReminder.DefaultCustomReminderTime;
			this.DueDate = ModernReminder.DefaultDueDate;
			this.ReferenceTime = ModernReminder.DefaultReferenceTime;
		}

		// Token: 0x0600671E RID: 26398 RVA: 0x001B4534 File Offset: 0x001B2734
		private void Validate()
		{
			if (this.InternalDueDate < this.InternalReferenceTime)
			{
				throw new InvalidParamException(ServerStrings.InvalidDueDate1(this.InternalDueDate.ToString(), this.InternalReferenceTime.ToString()));
			}
			if (this.ReminderTimeHint == ReminderTimeHint.Custom)
			{
				if (this.InternalCustomReminderTime < this.InternalReferenceTime)
				{
					throw new InvalidParamException(ServerStrings.InvalidReminderTime(this.InternalCustomReminderTime.ToString(), this.InternalReferenceTime.ToString()));
				}
				if (this.InternalDueDate < this.InternalCustomReminderTime)
				{
					throw new InvalidParamException(ServerStrings.InvalidDueDate2(this.InternalDueDate.ToString(), this.InternalCustomReminderTime.ToString()));
				}
			}
		}

		// Token: 0x0600671F RID: 26399 RVA: 0x001B4622 File Offset: 0x001B2822
		private DateTime ToDateTime(ExDateTime exDateTime)
		{
			return exDateTime.UniversalTime;
		}

		// Token: 0x06006720 RID: 26400 RVA: 0x001B462B File Offset: 0x001B282B
		private ExDateTime ToExDateTime(DateTime dateTime)
		{
			return new ExDateTime(ExTimeZone.UtcTimeZone, dateTime);
		}

		// Token: 0x04003A6E RID: 14958
		private const ReminderTimeHint DefaultReminderTimeHint = ReminderTimeHint.Tomorrow;

		// Token: 0x04003A6F RID: 14959
		private const Hours DefaultHours = Hours.Any;

		// Token: 0x04003A70 RID: 14960
		private const Priority DefaultPriority = Priority.Normal;

		// Token: 0x04003A71 RID: 14961
		private const int DefaultDuration = 30;

		// Token: 0x04003A72 RID: 14962
		private const int CurrentVersion = 1;

		// Token: 0x04003A73 RID: 14963
		private static readonly ExDateTime DefaultReferenceTime = ExDateTime.MinValue;

		// Token: 0x04003A74 RID: 14964
		private static readonly ExDateTime DefaultCustomReminderTime = ExDateTime.MaxValue;

		// Token: 0x04003A75 RID: 14965
		private static readonly ExDateTime DefaultDueDate = ExDateTime.MaxValue;

		// Token: 0x04003A76 RID: 14966
		private int duration;
	}
}
