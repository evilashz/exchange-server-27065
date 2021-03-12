using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000A00 RID: 2560
	[Serializable]
	public sealed class CalendarConfiguration : XsoMailboxConfigurationObject
	{
		// Token: 0x1700199A RID: 6554
		// (get) Token: 0x06005D8B RID: 23947 RVA: 0x0018C720 File Offset: 0x0018A920
		internal override XsoMailboxConfigurationObjectSchema Schema
		{
			get
			{
				return CalendarConfiguration.schema;
			}
		}

		// Token: 0x1700199B RID: 6555
		// (get) Token: 0x06005D8D RID: 23949 RVA: 0x0018C72F File Offset: 0x0018A92F
		// (set) Token: 0x06005D8E RID: 23950 RVA: 0x0018C741 File Offset: 0x0018A941
		[Parameter(Mandatory = false)]
		public CalendarProcessingFlags AutomateProcessing
		{
			get
			{
				return (CalendarProcessingFlags)this[CalendarConfigurationSchema.AutomateProcessing];
			}
			set
			{
				this[CalendarConfigurationSchema.AutomateProcessing] = value;
			}
		}

		// Token: 0x1700199C RID: 6556
		// (get) Token: 0x06005D8F RID: 23951 RVA: 0x0018C754 File Offset: 0x0018A954
		// (set) Token: 0x06005D90 RID: 23952 RVA: 0x0018C766 File Offset: 0x0018A966
		[Parameter(Mandatory = false)]
		public bool AllowConflicts
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.AllowConflicts];
			}
			set
			{
				this[CalendarConfigurationSchema.AllowConflicts] = value;
			}
		}

		// Token: 0x1700199D RID: 6557
		// (get) Token: 0x06005D91 RID: 23953 RVA: 0x0018C779 File Offset: 0x0018A979
		// (set) Token: 0x06005D92 RID: 23954 RVA: 0x0018C78B File Offset: 0x0018A98B
		[Parameter(Mandatory = false)]
		public int BookingWindowInDays
		{
			get
			{
				return (int)this[CalendarConfigurationSchema.BookingWindowInDays];
			}
			set
			{
				this[CalendarConfigurationSchema.BookingWindowInDays] = value;
			}
		}

		// Token: 0x1700199E RID: 6558
		// (get) Token: 0x06005D93 RID: 23955 RVA: 0x0018C79E File Offset: 0x0018A99E
		// (set) Token: 0x06005D94 RID: 23956 RVA: 0x0018C7B0 File Offset: 0x0018A9B0
		[Parameter(Mandatory = false)]
		public int MaximumDurationInMinutes
		{
			get
			{
				return (int)this[CalendarConfigurationSchema.MaximumDurationInMinutes];
			}
			set
			{
				this[CalendarConfigurationSchema.MaximumDurationInMinutes] = value;
			}
		}

		// Token: 0x1700199F RID: 6559
		// (get) Token: 0x06005D95 RID: 23957 RVA: 0x0018C7C3 File Offset: 0x0018A9C3
		// (set) Token: 0x06005D96 RID: 23958 RVA: 0x0018C7D5 File Offset: 0x0018A9D5
		[Parameter(Mandatory = false)]
		public bool AllowRecurringMeetings
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.AllowRecurringMeetings];
			}
			set
			{
				this[CalendarConfigurationSchema.AllowRecurringMeetings] = value;
			}
		}

		// Token: 0x170019A0 RID: 6560
		// (get) Token: 0x06005D97 RID: 23959 RVA: 0x0018C7E8 File Offset: 0x0018A9E8
		// (set) Token: 0x06005D98 RID: 23960 RVA: 0x0018C7FA File Offset: 0x0018A9FA
		[Parameter(Mandatory = false)]
		public bool EnforceSchedulingHorizon
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.EnforceSchedulingHorizon];
			}
			set
			{
				this[CalendarConfigurationSchema.EnforceSchedulingHorizon] = value;
			}
		}

		// Token: 0x170019A1 RID: 6561
		// (get) Token: 0x06005D99 RID: 23961 RVA: 0x0018C80D File Offset: 0x0018AA0D
		// (set) Token: 0x06005D9A RID: 23962 RVA: 0x0018C81F File Offset: 0x0018AA1F
		[Parameter(Mandatory = false)]
		public bool ScheduleOnlyDuringWorkHours
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.ScheduleOnlyDuringWorkHours];
			}
			set
			{
				this[CalendarConfigurationSchema.ScheduleOnlyDuringWorkHours] = value;
			}
		}

		// Token: 0x170019A2 RID: 6562
		// (get) Token: 0x06005D9B RID: 23963 RVA: 0x0018C832 File Offset: 0x0018AA32
		// (set) Token: 0x06005D9C RID: 23964 RVA: 0x0018C844 File Offset: 0x0018AA44
		[Parameter(Mandatory = false)]
		public int ConflictPercentageAllowed
		{
			get
			{
				return (int)this[CalendarConfigurationSchema.ConflictPercentageAllowed];
			}
			set
			{
				this[CalendarConfigurationSchema.ConflictPercentageAllowed] = value;
			}
		}

		// Token: 0x170019A3 RID: 6563
		// (get) Token: 0x06005D9D RID: 23965 RVA: 0x0018C857 File Offset: 0x0018AA57
		// (set) Token: 0x06005D9E RID: 23966 RVA: 0x0018C869 File Offset: 0x0018AA69
		[Parameter(Mandatory = false)]
		public int MaximumConflictInstances
		{
			get
			{
				return (int)this[CalendarConfigurationSchema.MaximumConflictInstances];
			}
			set
			{
				this[CalendarConfigurationSchema.MaximumConflictInstances] = value;
			}
		}

		// Token: 0x170019A4 RID: 6564
		// (get) Token: 0x06005D9F RID: 23967 RVA: 0x0018C87C File Offset: 0x0018AA7C
		// (set) Token: 0x06005DA0 RID: 23968 RVA: 0x0018C88E File Offset: 0x0018AA8E
		[Parameter(Mandatory = false)]
		public bool ForwardRequestsToDelegates
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.ForwardRequestsToDelegates];
			}
			set
			{
				this[CalendarConfigurationSchema.ForwardRequestsToDelegates] = value;
			}
		}

		// Token: 0x170019A5 RID: 6565
		// (get) Token: 0x06005DA1 RID: 23969 RVA: 0x0018C8A1 File Offset: 0x0018AAA1
		// (set) Token: 0x06005DA2 RID: 23970 RVA: 0x0018C8B3 File Offset: 0x0018AAB3
		[Parameter(Mandatory = false)]
		public bool DeleteAttachments
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.DeleteAttachments];
			}
			set
			{
				this[CalendarConfigurationSchema.DeleteAttachments] = value;
			}
		}

		// Token: 0x170019A6 RID: 6566
		// (get) Token: 0x06005DA3 RID: 23971 RVA: 0x0018C8C6 File Offset: 0x0018AAC6
		// (set) Token: 0x06005DA4 RID: 23972 RVA: 0x0018C8D8 File Offset: 0x0018AAD8
		[Parameter(Mandatory = false)]
		public bool DeleteComments
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.DeleteComments];
			}
			set
			{
				this[CalendarConfigurationSchema.DeleteComments] = value;
			}
		}

		// Token: 0x170019A7 RID: 6567
		// (get) Token: 0x06005DA5 RID: 23973 RVA: 0x0018C8EB File Offset: 0x0018AAEB
		// (set) Token: 0x06005DA6 RID: 23974 RVA: 0x0018C8FD File Offset: 0x0018AAFD
		[Parameter(Mandatory = false)]
		public bool RemovePrivateProperty
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.RemovePrivateProperty];
			}
			set
			{
				this[CalendarConfigurationSchema.RemovePrivateProperty] = value;
			}
		}

		// Token: 0x170019A8 RID: 6568
		// (get) Token: 0x06005DA7 RID: 23975 RVA: 0x0018C910 File Offset: 0x0018AB10
		// (set) Token: 0x06005DA8 RID: 23976 RVA: 0x0018C922 File Offset: 0x0018AB22
		[Parameter(Mandatory = false)]
		public bool DeleteSubject
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.DeleteSubject];
			}
			set
			{
				this[CalendarConfigurationSchema.DeleteSubject] = value;
			}
		}

		// Token: 0x170019A9 RID: 6569
		// (get) Token: 0x06005DA9 RID: 23977 RVA: 0x0018C935 File Offset: 0x0018AB35
		// (set) Token: 0x06005DAA RID: 23978 RVA: 0x0018C947 File Offset: 0x0018AB47
		[Parameter(Mandatory = false)]
		public bool AddOrganizerToSubject
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.AddOrganizerToSubject];
			}
			set
			{
				this[CalendarConfigurationSchema.AddOrganizerToSubject] = value;
			}
		}

		// Token: 0x170019AA RID: 6570
		// (get) Token: 0x06005DAB RID: 23979 RVA: 0x0018C95A File Offset: 0x0018AB5A
		// (set) Token: 0x06005DAC RID: 23980 RVA: 0x0018C96C File Offset: 0x0018AB6C
		[Parameter(Mandatory = false)]
		public bool DeleteNonCalendarItems
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.DeleteNonCalendarItems];
			}
			set
			{
				this[CalendarConfigurationSchema.DeleteNonCalendarItems] = value;
			}
		}

		// Token: 0x170019AB RID: 6571
		// (get) Token: 0x06005DAD RID: 23981 RVA: 0x0018C97F File Offset: 0x0018AB7F
		// (set) Token: 0x06005DAE RID: 23982 RVA: 0x0018C991 File Offset: 0x0018AB91
		[Parameter(Mandatory = false)]
		public bool TentativePendingApproval
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.TentativePendingApproval];
			}
			set
			{
				this[CalendarConfigurationSchema.TentativePendingApproval] = value;
			}
		}

		// Token: 0x170019AC RID: 6572
		// (get) Token: 0x06005DAF RID: 23983 RVA: 0x0018C9A4 File Offset: 0x0018ABA4
		// (set) Token: 0x06005DB0 RID: 23984 RVA: 0x0018C9B6 File Offset: 0x0018ABB6
		[Parameter(Mandatory = false)]
		public bool EnableResponseDetails
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.EnableResponseDetails];
			}
			set
			{
				this[CalendarConfigurationSchema.EnableResponseDetails] = value;
			}
		}

		// Token: 0x170019AD RID: 6573
		// (get) Token: 0x06005DB1 RID: 23985 RVA: 0x0018C9C9 File Offset: 0x0018ABC9
		// (set) Token: 0x06005DB2 RID: 23986 RVA: 0x0018C9DB File Offset: 0x0018ABDB
		[Parameter(Mandatory = false)]
		public bool OrganizerInfo
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.OrganizerInfo];
			}
			set
			{
				this[CalendarConfigurationSchema.OrganizerInfo] = value;
			}
		}

		// Token: 0x170019AE RID: 6574
		// (get) Token: 0x06005DB3 RID: 23987 RVA: 0x0018C9EE File Offset: 0x0018ABEE
		// (set) Token: 0x06005DB4 RID: 23988 RVA: 0x0018CA00 File Offset: 0x0018AC00
		public MultiValuedProperty<ADObjectId> ResourceDelegates
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[CalendarConfigurationSchema.ResourceDelegates];
			}
			set
			{
				this[CalendarConfigurationSchema.ResourceDelegates] = value;
			}
		}

		// Token: 0x170019AF RID: 6575
		// (get) Token: 0x06005DB5 RID: 23989 RVA: 0x0018CA0E File Offset: 0x0018AC0E
		// (set) Token: 0x06005DB6 RID: 23990 RVA: 0x0018CA20 File Offset: 0x0018AC20
		public MultiValuedProperty<string> RequestOutOfPolicy
		{
			get
			{
				return (MultiValuedProperty<string>)this[CalendarConfigurationSchema.RequestOutOfPolicyLegDN];
			}
			set
			{
				this[CalendarConfigurationSchema.RequestOutOfPolicyLegDN] = value;
			}
		}

		// Token: 0x170019B0 RID: 6576
		// (get) Token: 0x06005DB7 RID: 23991 RVA: 0x0018CA2E File Offset: 0x0018AC2E
		// (set) Token: 0x06005DB8 RID: 23992 RVA: 0x0018CA40 File Offset: 0x0018AC40
		[Parameter(Mandatory = false)]
		public bool AllRequestOutOfPolicy
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.AllRequestOutOfPolicy];
			}
			set
			{
				this[CalendarConfigurationSchema.AllRequestOutOfPolicy] = value;
			}
		}

		// Token: 0x170019B1 RID: 6577
		// (get) Token: 0x06005DB9 RID: 23993 RVA: 0x0018CA53 File Offset: 0x0018AC53
		// (set) Token: 0x06005DBA RID: 23994 RVA: 0x0018CA65 File Offset: 0x0018AC65
		public MultiValuedProperty<string> BookInPolicy
		{
			get
			{
				return (MultiValuedProperty<string>)this[CalendarConfigurationSchema.BookInPolicyLegDN];
			}
			set
			{
				this[CalendarConfigurationSchema.BookInPolicyLegDN] = value;
			}
		}

		// Token: 0x170019B2 RID: 6578
		// (get) Token: 0x06005DBB RID: 23995 RVA: 0x0018CA73 File Offset: 0x0018AC73
		// (set) Token: 0x06005DBC RID: 23996 RVA: 0x0018CA85 File Offset: 0x0018AC85
		[Parameter(Mandatory = false)]
		public bool AllBookInPolicy
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.AllBookInPolicy];
			}
			set
			{
				this[CalendarConfigurationSchema.AllBookInPolicy] = value;
			}
		}

		// Token: 0x170019B3 RID: 6579
		// (get) Token: 0x06005DBD RID: 23997 RVA: 0x0018CA98 File Offset: 0x0018AC98
		// (set) Token: 0x06005DBE RID: 23998 RVA: 0x0018CAAA File Offset: 0x0018ACAA
		public MultiValuedProperty<string> RequestInPolicy
		{
			get
			{
				return (MultiValuedProperty<string>)this[CalendarConfigurationSchema.RequestInPolicyLegDN];
			}
			set
			{
				this[CalendarConfigurationSchema.RequestInPolicyLegDN] = value;
			}
		}

		// Token: 0x170019B4 RID: 6580
		// (get) Token: 0x06005DBF RID: 23999 RVA: 0x0018CAB8 File Offset: 0x0018ACB8
		// (set) Token: 0x06005DC0 RID: 24000 RVA: 0x0018CACA File Offset: 0x0018ACCA
		[Parameter(Mandatory = false)]
		public bool AllRequestInPolicy
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.AllRequestInPolicy];
			}
			set
			{
				this[CalendarConfigurationSchema.AllRequestInPolicy] = value;
			}
		}

		// Token: 0x170019B5 RID: 6581
		// (get) Token: 0x06005DC1 RID: 24001 RVA: 0x0018CADD File Offset: 0x0018ACDD
		// (set) Token: 0x06005DC2 RID: 24002 RVA: 0x0018CAEF File Offset: 0x0018ACEF
		[Parameter(Mandatory = false)]
		public bool AddAdditionalResponse
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.AddAdditionalResponse];
			}
			set
			{
				this[CalendarConfigurationSchema.AddAdditionalResponse] = value;
			}
		}

		// Token: 0x170019B6 RID: 6582
		// (get) Token: 0x06005DC3 RID: 24003 RVA: 0x0018CB02 File Offset: 0x0018AD02
		// (set) Token: 0x06005DC4 RID: 24004 RVA: 0x0018CB14 File Offset: 0x0018AD14
		[Parameter(Mandatory = false)]
		public string AdditionalResponse
		{
			get
			{
				return (string)this[CalendarConfigurationSchema.AdditionalResponse];
			}
			set
			{
				this[CalendarConfigurationSchema.AdditionalResponse] = value;
			}
		}

		// Token: 0x170019B7 RID: 6583
		// (get) Token: 0x06005DC5 RID: 24005 RVA: 0x0018CB22 File Offset: 0x0018AD22
		// (set) Token: 0x06005DC6 RID: 24006 RVA: 0x0018CB34 File Offset: 0x0018AD34
		[Parameter(Mandatory = false)]
		public bool RemoveOldMeetingMessages
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.RemoveOldMeetingMessages];
			}
			set
			{
				this[CalendarConfigurationSchema.RemoveOldMeetingMessages] = value;
			}
		}

		// Token: 0x170019B8 RID: 6584
		// (get) Token: 0x06005DC7 RID: 24007 RVA: 0x0018CB47 File Offset: 0x0018AD47
		// (set) Token: 0x06005DC8 RID: 24008 RVA: 0x0018CB59 File Offset: 0x0018AD59
		[Parameter(Mandatory = false)]
		public bool AddNewRequestsTentatively
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.AddNewRequestsTentatively];
			}
			set
			{
				this[CalendarConfigurationSchema.AddNewRequestsTentatively] = value;
			}
		}

		// Token: 0x170019B9 RID: 6585
		// (get) Token: 0x06005DC9 RID: 24009 RVA: 0x0018CB6C File Offset: 0x0018AD6C
		// (set) Token: 0x06005DCA RID: 24010 RVA: 0x0018CB7E File Offset: 0x0018AD7E
		[Parameter(Mandatory = false)]
		public bool ProcessExternalMeetingMessages
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.ProcessExternalMeetingMessages];
			}
			set
			{
				this[CalendarConfigurationSchema.ProcessExternalMeetingMessages] = value;
			}
		}

		// Token: 0x170019BA RID: 6586
		// (get) Token: 0x06005DCB RID: 24011 RVA: 0x0018CB91 File Offset: 0x0018AD91
		// (set) Token: 0x06005DCC RID: 24012 RVA: 0x0018CBA3 File Offset: 0x0018ADA3
		[Parameter(Mandatory = false)]
		public bool RemoveForwardedMeetingNotifications
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.RemoveForwardedMeetingNotifications];
			}
			set
			{
				this[CalendarConfigurationSchema.RemoveForwardedMeetingNotifications] = value;
			}
		}

		// Token: 0x170019BB RID: 6587
		// (get) Token: 0x06005DCD RID: 24013 RVA: 0x0018CBB6 File Offset: 0x0018ADB6
		// (set) Token: 0x06005DCE RID: 24014 RVA: 0x0018CBC8 File Offset: 0x0018ADC8
		internal int DefaultReminderTime
		{
			get
			{
				return (int)this[CalendarConfigurationSchema.DefaultReminderTime];
			}
			set
			{
				this[CalendarConfigurationSchema.DefaultReminderTime] = value;
			}
		}

		// Token: 0x170019BC RID: 6588
		// (get) Token: 0x06005DCF RID: 24015 RVA: 0x0018CBDB File Offset: 0x0018ADDB
		// (set) Token: 0x06005DD0 RID: 24016 RVA: 0x0018CBED File Offset: 0x0018ADED
		internal bool DisableReminders
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.DisableReminders];
			}
			set
			{
				this[CalendarConfigurationSchema.DisableReminders] = value;
			}
		}

		// Token: 0x170019BD RID: 6589
		// (get) Token: 0x06005DD1 RID: 24017 RVA: 0x0018CC00 File Offset: 0x0018AE00
		// (set) Token: 0x06005DD2 RID: 24018 RVA: 0x0018CC12 File Offset: 0x0018AE12
		internal bool SkipProcessing
		{
			get
			{
				return (bool)this[CalendarConfigurationSchema.SkipProcessing];
			}
			set
			{
				this[CalendarConfigurationSchema.SkipProcessing] = value;
			}
		}

		// Token: 0x170019BE RID: 6590
		// (get) Token: 0x06005DD3 RID: 24019 RVA: 0x0018CC25 File Offset: 0x0018AE25
		// (set) Token: 0x06005DD4 RID: 24020 RVA: 0x0018CC37 File Offset: 0x0018AE37
		internal MultiValuedProperty<ADObjectId> RequestOutOfPolicyLegacy
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[CalendarConfigurationSchema.RequestOutOfPolicy];
			}
			set
			{
				this[CalendarConfigurationSchema.RequestOutOfPolicy] = value;
			}
		}

		// Token: 0x170019BF RID: 6591
		// (get) Token: 0x06005DD5 RID: 24021 RVA: 0x0018CC45 File Offset: 0x0018AE45
		// (set) Token: 0x06005DD6 RID: 24022 RVA: 0x0018CC57 File Offset: 0x0018AE57
		internal MultiValuedProperty<ADObjectId> BookInPolicyLegacy
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[CalendarConfigurationSchema.BookInPolicy];
			}
			set
			{
				this[CalendarConfigurationSchema.BookInPolicy] = value;
			}
		}

		// Token: 0x170019C0 RID: 6592
		// (get) Token: 0x06005DD7 RID: 24023 RVA: 0x0018CC65 File Offset: 0x0018AE65
		// (set) Token: 0x06005DD8 RID: 24024 RVA: 0x0018CC77 File Offset: 0x0018AE77
		internal MultiValuedProperty<ADObjectId> RequestInPolicyLegacy
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[CalendarConfigurationSchema.RequestInPolicy];
			}
			set
			{
				this[CalendarConfigurationSchema.RequestInPolicy] = value;
			}
		}

		// Token: 0x04003460 RID: 13408
		private static CalendarConfigurationSchema schema = ObjectSchema.GetInstance<CalendarConfigurationSchema>();
	}
}
