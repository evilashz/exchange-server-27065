using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000B5 RID: 181
	[DataContract]
	public class SetSchedulingOptionsConfiguration : SetResourceConfigurationBase
	{
		// Token: 0x06001C6E RID: 7278 RVA: 0x00058898 File Offset: 0x00056A98
		public SetSchedulingOptionsConfiguration()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x000588BA File Offset: 0x00056ABA
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.SetMailboxCalendarConfiguration = new SetMailboxCalendarConfiguration();
		}

		// Token: 0x170018F5 RID: 6389
		// (get) Token: 0x06001C70 RID: 7280 RVA: 0x000588C7 File Offset: 0x00056AC7
		// (set) Token: 0x06001C71 RID: 7281 RVA: 0x000588CF File Offset: 0x00056ACF
		public SetMailboxCalendarConfiguration SetMailboxCalendarConfiguration { get; private set; }

		// Token: 0x170018F6 RID: 6390
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x000588D8 File Offset: 0x00056AD8
		// (set) Token: 0x06001C73 RID: 7283 RVA: 0x000588F4 File Offset: 0x00056AF4
		[DataMember]
		public bool AutoAcceptAutomateProcessing
		{
			get
			{
				return (bool)(base["AutomateProcessing"] ?? false);
			}
			set
			{
				base["AutomateProcessing"] = (value ? CalendarProcessingFlags.AutoAccept : CalendarProcessingFlags.None);
			}
		}

		// Token: 0x170018F7 RID: 6391
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x0005890D File Offset: 0x00056B0D
		// (set) Token: 0x06001C75 RID: 7285 RVA: 0x0005891A File Offset: 0x00056B1A
		[DataMember]
		public bool DisableReminders
		{
			get
			{
				return this.SetMailboxCalendarConfiguration.DisableReminders;
			}
			set
			{
				this.SetMailboxCalendarConfiguration.DisableReminders = value;
			}
		}

		// Token: 0x170018F8 RID: 6392
		// (get) Token: 0x06001C76 RID: 7286 RVA: 0x00058928 File Offset: 0x00056B28
		// (set) Token: 0x06001C77 RID: 7287 RVA: 0x00058930 File Offset: 0x00056B30
		[DataMember]
		public string BookingWindowInDays { get; set; }

		// Token: 0x170018F9 RID: 6393
		// (get) Token: 0x06001C78 RID: 7288 RVA: 0x00058939 File Offset: 0x00056B39
		// (set) Token: 0x06001C79 RID: 7289 RVA: 0x00058955 File Offset: 0x00056B55
		[DataMember]
		public bool EnforceSchedulingHorizon
		{
			get
			{
				return (bool)(base["EnforceSchedulingHorizon"] ?? false);
			}
			set
			{
				base["EnforceSchedulingHorizon"] = value;
			}
		}

		// Token: 0x170018FA RID: 6394
		// (get) Token: 0x06001C7A RID: 7290 RVA: 0x00058968 File Offset: 0x00056B68
		// (set) Token: 0x06001C7B RID: 7291 RVA: 0x00058970 File Offset: 0x00056B70
		[DataMember]
		public bool? LimitDuration { get; set; }

		// Token: 0x170018FB RID: 6395
		// (get) Token: 0x06001C7C RID: 7292 RVA: 0x00058979 File Offset: 0x00056B79
		// (set) Token: 0x06001C7D RID: 7293 RVA: 0x00058981 File Offset: 0x00056B81
		[DataMember]
		public string MaximumDurationInMinutes { get; set; }

		// Token: 0x170018FC RID: 6396
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x0005898A File Offset: 0x00056B8A
		// (set) Token: 0x06001C7F RID: 7295 RVA: 0x000589A6 File Offset: 0x00056BA6
		[DataMember]
		public bool ScheduleOnlyDuringWorkHours
		{
			get
			{
				return (bool)(base["ScheduleOnlyDuringWorkHours"] ?? false);
			}
			set
			{
				base["ScheduleOnlyDuringWorkHours"] = value;
			}
		}

		// Token: 0x170018FD RID: 6397
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x000589B9 File Offset: 0x00056BB9
		// (set) Token: 0x06001C81 RID: 7297 RVA: 0x000589D5 File Offset: 0x00056BD5
		[DataMember]
		public bool AllowRecurringMeetings
		{
			get
			{
				return (bool)(base["AllowRecurringMeetings"] ?? false);
			}
			set
			{
				base["AllowRecurringMeetings"] = value;
			}
		}

		// Token: 0x170018FE RID: 6398
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x000589E8 File Offset: 0x00056BE8
		// (set) Token: 0x06001C83 RID: 7299 RVA: 0x00058A04 File Offset: 0x00056C04
		[DataMember]
		public bool AllowConflicts
		{
			get
			{
				return (bool)(base["AllowConflicts"] ?? false);
			}
			set
			{
				base["AllowConflicts"] = value;
			}
		}

		// Token: 0x170018FF RID: 6399
		// (get) Token: 0x06001C84 RID: 7300 RVA: 0x00058A17 File Offset: 0x00056C17
		// (set) Token: 0x06001C85 RID: 7301 RVA: 0x00058A1F File Offset: 0x00056C1F
		[DataMember]
		public string MaximumConflictInstances { get; set; }

		// Token: 0x17001900 RID: 6400
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x00058A28 File Offset: 0x00056C28
		// (set) Token: 0x06001C87 RID: 7303 RVA: 0x00058A30 File Offset: 0x00056C30
		[DataMember]
		public string ConflictPercentageAllowed { get; set; }

		// Token: 0x06001C88 RID: 7304 RVA: 0x00058A3C File Offset: 0x00056C3C
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (this.BookingWindowInDays != null)
			{
				int num;
				if (!int.TryParse(this.BookingWindowInDays, out num) || num < 0 || num > 1080)
				{
					throw new FaultException(OwaOptionStrings.BookingWindowInDaysErrorMessage);
				}
				base["BookingWindowInDays"] = num;
			}
			if (this.LimitDuration != null && !this.LimitDuration.Value)
			{
				base["MaximumDurationInMinutes"] = 0;
			}
			else if (this.MaximumDurationInMinutes != null)
			{
				int num2;
				if (!int.TryParse(this.MaximumDurationInMinutes, out num2) || num2 < 0 || num2 > 2147483647)
				{
					throw new FaultException(OwaOptionStrings.MaximumDurationInMinutesErrorMessage);
				}
				base["MaximumDurationInMinutes"] = num2;
			}
			if (this.MaximumConflictInstances != null)
			{
				int num3;
				if (!int.TryParse(this.MaximumConflictInstances, out num3) || num3 < 0 || num3 > 2147483647)
				{
					throw new FaultException(OwaOptionStrings.MaximumConflictInstancesErrorMessage);
				}
				base["MaximumConflictInstances"] = num3;
			}
			if (this.ConflictPercentageAllowed == null)
			{
				return;
			}
			int num4;
			if (int.TryParse(this.ConflictPercentageAllowed, out num4) && num4 >= 0 && num4 <= 100)
			{
				base["ConflictPercentageAllowed"] = num4;
				return;
			}
			throw new FaultException(OwaOptionStrings.ConflictPercentageAllowedErrorMessage);
		}
	}
}
