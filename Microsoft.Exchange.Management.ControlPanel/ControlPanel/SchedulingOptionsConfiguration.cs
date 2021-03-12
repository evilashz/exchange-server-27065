using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000B4 RID: 180
	[DataContract]
	public class SchedulingOptionsConfiguration : ResourceConfigurationBase
	{
		// Token: 0x06001C55 RID: 7253 RVA: 0x00058790 File Offset: 0x00056990
		public SchedulingOptionsConfiguration(CalendarConfiguration calendarConfiguration) : base(calendarConfiguration)
		{
		}

		// Token: 0x170018E9 RID: 6377
		// (get) Token: 0x06001C56 RID: 7254 RVA: 0x00058799 File Offset: 0x00056999
		// (set) Token: 0x06001C57 RID: 7255 RVA: 0x000587A1 File Offset: 0x000569A1
		public MailboxCalendarConfiguration MailboxCalendarConfiguration { get; set; }

		// Token: 0x170018EA RID: 6378
		// (get) Token: 0x06001C58 RID: 7256 RVA: 0x000587AA File Offset: 0x000569AA
		// (set) Token: 0x06001C59 RID: 7257 RVA: 0x000587BA File Offset: 0x000569BA
		[DataMember]
		public bool AutoAcceptAutomateProcessing
		{
			get
			{
				return base.CalendarConfiguration.AutomateProcessing == CalendarProcessingFlags.AutoAccept;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018EB RID: 6379
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x000587C1 File Offset: 0x000569C1
		// (set) Token: 0x06001C5B RID: 7259 RVA: 0x000587DB File Offset: 0x000569DB
		[DataMember]
		public bool DisableReminders
		{
			get
			{
				return this.MailboxCalendarConfiguration != null && !this.MailboxCalendarConfiguration.RemindersEnabled;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018EC RID: 6380
		// (get) Token: 0x06001C5C RID: 7260 RVA: 0x000587E2 File Offset: 0x000569E2
		// (set) Token: 0x06001C5D RID: 7261 RVA: 0x000587EF File Offset: 0x000569EF
		[DataMember]
		public int BookingWindowInDays
		{
			get
			{
				return base.CalendarConfiguration.BookingWindowInDays;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018ED RID: 6381
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x000587F6 File Offset: 0x000569F6
		// (set) Token: 0x06001C5F RID: 7263 RVA: 0x00058803 File Offset: 0x00056A03
		[DataMember]
		public bool EnforceSchedulingHorizon
		{
			get
			{
				return base.CalendarConfiguration.EnforceSchedulingHorizon;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018EE RID: 6382
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x0005880A File Offset: 0x00056A0A
		// (set) Token: 0x06001C61 RID: 7265 RVA: 0x00058818 File Offset: 0x00056A18
		[DataMember]
		public bool LimitDuration
		{
			get
			{
				return this.MaximumDurationInMinutes != 0;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018EF RID: 6383
		// (get) Token: 0x06001C62 RID: 7266 RVA: 0x0005881F File Offset: 0x00056A1F
		// (set) Token: 0x06001C63 RID: 7267 RVA: 0x0005882C File Offset: 0x00056A2C
		[DataMember]
		public int MaximumDurationInMinutes
		{
			get
			{
				return base.CalendarConfiguration.MaximumDurationInMinutes;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018F0 RID: 6384
		// (get) Token: 0x06001C64 RID: 7268 RVA: 0x00058833 File Offset: 0x00056A33
		// (set) Token: 0x06001C65 RID: 7269 RVA: 0x00058840 File Offset: 0x00056A40
		[DataMember]
		public bool ScheduleOnlyDuringWorkHours
		{
			get
			{
				return base.CalendarConfiguration.ScheduleOnlyDuringWorkHours;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018F1 RID: 6385
		// (get) Token: 0x06001C66 RID: 7270 RVA: 0x00058847 File Offset: 0x00056A47
		// (set) Token: 0x06001C67 RID: 7271 RVA: 0x00058854 File Offset: 0x00056A54
		[DataMember]
		public bool AllowRecurringMeetings
		{
			get
			{
				return base.CalendarConfiguration.AllowRecurringMeetings;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018F2 RID: 6386
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x0005885B File Offset: 0x00056A5B
		// (set) Token: 0x06001C69 RID: 7273 RVA: 0x00058868 File Offset: 0x00056A68
		[DataMember]
		public bool AllowConflicts
		{
			get
			{
				return base.CalendarConfiguration.AllowConflicts;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018F3 RID: 6387
		// (get) Token: 0x06001C6A RID: 7274 RVA: 0x0005886F File Offset: 0x00056A6F
		// (set) Token: 0x06001C6B RID: 7275 RVA: 0x0005887C File Offset: 0x00056A7C
		[DataMember]
		public int MaximumConflictInstances
		{
			get
			{
				return base.CalendarConfiguration.MaximumConflictInstances;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018F4 RID: 6388
		// (get) Token: 0x06001C6C RID: 7276 RVA: 0x00058883 File Offset: 0x00056A83
		// (set) Token: 0x06001C6D RID: 7277 RVA: 0x00058890 File Offset: 0x00056A90
		[DataMember]
		public int ConflictPercentageAllowed
		{
			get
			{
				return base.CalendarConfiguration.ConflictPercentageAllowed;
			}
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}
