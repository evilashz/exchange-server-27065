using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000A6E RID: 2670
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MailboxCalendarConfiguration : OptionsPropertyChangeTracker
	{
		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x06004BB8 RID: 19384 RVA: 0x00105C68 File Offset: 0x00103E68
		// (set) Token: 0x06004BB9 RID: 19385 RVA: 0x00105C70 File Offset: 0x00103E70
		[DataMember]
		public TimeZoneInformation CurrentTimeZone { get; set; }

		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x06004BBA RID: 19386 RVA: 0x00105C79 File Offset: 0x00103E79
		// (set) Token: 0x06004BBB RID: 19387 RVA: 0x00105C81 File Offset: 0x00103E81
		[DataMember]
		public CalendarReminder DefaultReminderTime
		{
			get
			{
				return this.defaultReminderTime;
			}
			set
			{
				this.defaultReminderTime = value;
				base.TrackPropertyChanged("DefaultReminderTime");
			}
		}

		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x06004BBC RID: 19388 RVA: 0x00105C95 File Offset: 0x00103E95
		// (set) Token: 0x06004BBD RID: 19389 RVA: 0x00105C9D File Offset: 0x00103E9D
		[DataMember]
		public FirstWeekRules FirstWeekOfYear
		{
			get
			{
				return this.firstWeekOfYear;
			}
			set
			{
				this.firstWeekOfYear = value;
				base.TrackPropertyChanged("FirstWeekOfYear");
			}
		}

		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x06004BBE RID: 19390 RVA: 0x00105CB1 File Offset: 0x00103EB1
		// (set) Token: 0x06004BBF RID: 19391 RVA: 0x00105CB9 File Offset: 0x00103EB9
		[DataMember]
		public TimeZoneInformation WorkingHoursTimeZone
		{
			get
			{
				return this.workingHoursTimeZone;
			}
			set
			{
				this.workingHoursTimeZone = value;
				base.TrackPropertyChanged("WorkingHoursTimeZone");
			}
		}

		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x06004BC0 RID: 19392 RVA: 0x00105CCD File Offset: 0x00103ECD
		// (set) Token: 0x06004BC1 RID: 19393 RVA: 0x00105CD5 File Offset: 0x00103ED5
		[DataMember]
		public bool RemindersEnabled
		{
			get
			{
				return this.remindersEnabled;
			}
			set
			{
				this.remindersEnabled = value;
				base.TrackPropertyChanged("RemindersEnabled");
			}
		}

		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x06004BC2 RID: 19394 RVA: 0x00105CE9 File Offset: 0x00103EE9
		// (set) Token: 0x06004BC3 RID: 19395 RVA: 0x00105CF1 File Offset: 0x00103EF1
		[DataMember]
		public bool ReminderSoundEnabled
		{
			get
			{
				return this.reminderSoundEnabled;
			}
			set
			{
				this.reminderSoundEnabled = value;
				base.TrackPropertyChanged("ReminderSoundEnabled");
			}
		}

		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x06004BC4 RID: 19396 RVA: 0x00105D05 File Offset: 0x00103F05
		// (set) Token: 0x06004BC5 RID: 19397 RVA: 0x00105D0D File Offset: 0x00103F0D
		[DataMember]
		public bool ShowWeekNumbers
		{
			get
			{
				return this.showWeekNumbers;
			}
			set
			{
				this.showWeekNumbers = value;
				base.TrackPropertyChanged("ShowWeekNumbers");
			}
		}

		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x06004BC6 RID: 19398 RVA: 0x00105D21 File Offset: 0x00103F21
		// (set) Token: 0x06004BC7 RID: 19399 RVA: 0x00105D29 File Offset: 0x00103F29
		[DataMember]
		public HourIncrement TimeIncrement
		{
			get
			{
				return this.timeIncrement;
			}
			set
			{
				this.timeIncrement = value;
				base.TrackPropertyChanged("TimeIncrement");
			}
		}

		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x06004BC8 RID: 19400 RVA: 0x00105D3D File Offset: 0x00103F3D
		// (set) Token: 0x06004BC9 RID: 19401 RVA: 0x00105D45 File Offset: 0x00103F45
		[DataMember]
		public Microsoft.Exchange.Data.Storage.Management.DayOfWeek WeekStartDay
		{
			get
			{
				return this.weekStartDay;
			}
			set
			{
				this.weekStartDay = value;
				base.TrackPropertyChanged("WeekStartDay");
			}
		}

		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x06004BCA RID: 19402 RVA: 0x00105D59 File Offset: 0x00103F59
		// (set) Token: 0x06004BCB RID: 19403 RVA: 0x00105D61 File Offset: 0x00103F61
		[DataMember]
		public DaysOfWeek WorkDays
		{
			get
			{
				return this.workDays;
			}
			set
			{
				this.workDays = value;
				base.TrackPropertyChanged("WorkDays");
			}
		}

		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x06004BCC RID: 19404 RVA: 0x00105D75 File Offset: 0x00103F75
		// (set) Token: 0x06004BCD RID: 19405 RVA: 0x00105D7D File Offset: 0x00103F7D
		[DataMember]
		public int WorkingHoursStartTime
		{
			get
			{
				return this.workingHoursStartTime;
			}
			set
			{
				this.workingHoursStartTime = value;
				base.TrackPropertyChanged("WorkingHoursStartTime");
			}
		}

		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x06004BCE RID: 19406 RVA: 0x00105D91 File Offset: 0x00103F91
		// (set) Token: 0x06004BCF RID: 19407 RVA: 0x00105D99 File Offset: 0x00103F99
		[DataMember]
		public int WorkingHoursEndTime
		{
			get
			{
				return this.workingHoursEndTime;
			}
			set
			{
				this.workingHoursEndTime = value;
				base.TrackPropertyChanged("WorkingHoursEndTime");
			}
		}

		// Token: 0x04002B00 RID: 11008
		private CalendarReminder defaultReminderTime;

		// Token: 0x04002B01 RID: 11009
		private FirstWeekRules firstWeekOfYear;

		// Token: 0x04002B02 RID: 11010
		private bool remindersEnabled;

		// Token: 0x04002B03 RID: 11011
		private bool reminderSoundEnabled;

		// Token: 0x04002B04 RID: 11012
		private bool showWeekNumbers;

		// Token: 0x04002B05 RID: 11013
		private HourIncrement timeIncrement;

		// Token: 0x04002B06 RID: 11014
		private Microsoft.Exchange.Data.Storage.Management.DayOfWeek weekStartDay;

		// Token: 0x04002B07 RID: 11015
		private DaysOfWeek workDays;

		// Token: 0x04002B08 RID: 11016
		private int workingHoursEndTime;

		// Token: 0x04002B09 RID: 11017
		private int workingHoursStartTime;

		// Token: 0x04002B0A RID: 11018
		private TimeZoneInformation workingHoursTimeZone;
	}
}
