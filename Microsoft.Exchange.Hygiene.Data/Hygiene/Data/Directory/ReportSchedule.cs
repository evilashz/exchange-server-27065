using System;
using System.Data.SqlTypes;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x02000236 RID: 566
	internal class ReportSchedule : ConfigurablePropertyBag
	{
		// Token: 0x060016CA RID: 5834 RVA: 0x00046792 File Offset: 0x00044992
		public ReportSchedule(Guid tenantId, string scheduleName) : this(tenantId, scheduleName, DateTime.Today)
		{
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x000467A1 File Offset: 0x000449A1
		public ReportSchedule(Guid tenantId, string scheduleName, DateTime scheduleStartTime) : this()
		{
			this.TenantId = tenantId;
			this.ScheduleName = scheduleName;
			this[ReportScheduleSchema.Id] = CombGuidGenerator.NewGuid();
			this.ScheduleStartTime = scheduleStartTime;
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x000467D3 File Offset: 0x000449D3
		public ReportSchedule()
		{
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x000467DC File Offset: 0x000449DC
		public void SetExecutionStatus(Guid executionContextId, ReportExecutionStatusType executionStatus)
		{
			if (this.CurrentExecutionContextId != executionContextId)
			{
				throw new InvalidDataException(string.Format("The execution context Id does not match for schedule '{0}', tenant: {1}", this.ScheduleName ?? string.Empty, this.TenantId));
			}
			if (ReportSchedule.IsExecutionCompleted(executionStatus))
			{
				this.LastExecutionContextId = executionContextId;
				this.LastExecutionStatus = executionStatus;
				this.LastExecutionTime = DateTime.UtcNow;
				this.CurrentExecutionContextId = Guid.Empty;
				this.CurrentExecutionStatus = ReportExecutionStatusType.None;
				return;
			}
			this.CurrentExecutionStatus = executionStatus;
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x0004685C File Offset: 0x00044A5C
		public void SetExecutionContext(Guid executionContextId)
		{
			this.CurrentExecutionContextId = executionContextId;
			this.CurrentExecutionStatus = ReportExecutionStatusType.Queued;
			this.LastScheduleTime = DateTime.UtcNow;
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x00046878 File Offset: 0x00044A78
		public DateTime GetNextScheduleTime()
		{
			DateTime result = SqlDateTime.MaxValue.Value;
			switch (this.ScheduleFrequency)
			{
			case ReportScheduleFrequencyType.Daily:
				result = this.LastScheduleTime.AddDays(1.0);
				break;
			case ReportScheduleFrequencyType.Monthly:
			{
				result = new DateTime(this.LastScheduleTime.Year, this.LastScheduleTime.Month, 1).AddMonths(1);
				DateTime dateTime = result.AddMonths(1).AddDays(-1.0);
				DateTime dateTime2 = result.AddDays((double)(this.ScheduleMask - 1));
				result = ((dateTime2 < dateTime) ? dateTime2 : dateTime);
				break;
			}
			}
			return result;
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x060016D0 RID: 5840 RVA: 0x0004693C File Offset: 0x00044B3C
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this[ReportScheduleSchema.Id].ToString());
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x00046953 File Offset: 0x00044B53
		// (set) Token: 0x060016D2 RID: 5842 RVA: 0x00046965 File Offset: 0x00044B65
		public Guid TenantId
		{
			get
			{
				return (Guid)this[ReportScheduleSchema.TenantId];
			}
			set
			{
				this[ReportScheduleSchema.TenantId] = value;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x00046978 File Offset: 0x00044B78
		// (set) Token: 0x060016D4 RID: 5844 RVA: 0x0004698A File Offset: 0x00044B8A
		public bool Enabled
		{
			get
			{
				return (bool)this[ReportScheduleSchema.Enabled];
			}
			set
			{
				this[ReportScheduleSchema.Enabled] = value;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x0004699D File Offset: 0x00044B9D
		// (set) Token: 0x060016D6 RID: 5846 RVA: 0x000469AF File Offset: 0x00044BAF
		public string ScheduleName
		{
			get
			{
				return this[ReportScheduleSchema.ScheduleName] as string;
			}
			private set
			{
				this[ReportScheduleSchema.ScheduleName] = value;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x000469BD File Offset: 0x00044BBD
		// (set) Token: 0x060016D8 RID: 5848 RVA: 0x000469CF File Offset: 0x00044BCF
		public ReportScheduleFrequencyType ScheduleFrequency
		{
			get
			{
				return (ReportScheduleFrequencyType)this[ReportScheduleSchema.ScheduleFrequency];
			}
			set
			{
				this[ReportScheduleSchema.ScheduleFrequency] = value;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x000469E2 File Offset: 0x00044BE2
		// (set) Token: 0x060016DA RID: 5850 RVA: 0x000469F4 File Offset: 0x00044BF4
		public byte ScheduleMask
		{
			get
			{
				return (byte)this[ReportScheduleSchema.ScheduleMask];
			}
			set
			{
				this[ReportScheduleSchema.ScheduleMask] = value;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x00046A07 File Offset: 0x00044C07
		// (set) Token: 0x060016DC RID: 5852 RVA: 0x00046A1C File Offset: 0x00044C1C
		public DateTime ScheduleStartTime
		{
			get
			{
				return (DateTime)this[ReportScheduleSchema.ScheduleStartTime];
			}
			private set
			{
				DateTime dateTime = value;
				if (dateTime < DateTime.Today)
				{
					dateTime = DateTime.Today;
				}
				this[ReportScheduleSchema.ScheduleStartTime] = dateTime;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x00046A4F File Offset: 0x00044C4F
		// (set) Token: 0x060016DE RID: 5854 RVA: 0x00046A61 File Offset: 0x00044C61
		public string ReportName
		{
			get
			{
				return this[ReportScheduleSchema.ReportName] as string;
			}
			set
			{
				this[ReportScheduleSchema.ReportName] = value;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x00046A6F File Offset: 0x00044C6F
		// (set) Token: 0x060016E0 RID: 5856 RVA: 0x00046A81 File Offset: 0x00044C81
		public ReportFormatType ReportFormat
		{
			get
			{
				return (ReportFormatType)this[ReportScheduleSchema.ReportFormat];
			}
			set
			{
				this[ReportScheduleSchema.ReportFormat] = value;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x00046A94 File Offset: 0x00044C94
		// (set) Token: 0x060016E2 RID: 5858 RVA: 0x00046AA6 File Offset: 0x00044CA6
		public string ReportSubject
		{
			get
			{
				return this[ReportScheduleSchema.ReportSubject] as string;
			}
			set
			{
				this[ReportScheduleSchema.ReportSubject] = value;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x00046AB4 File Offset: 0x00044CB4
		// (set) Token: 0x060016E4 RID: 5860 RVA: 0x00046AC6 File Offset: 0x00044CC6
		public string ReportRecipients
		{
			get
			{
				return this[ReportScheduleSchema.ReportRecipients] as string;
			}
			set
			{
				this[ReportScheduleSchema.ReportRecipients] = value;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x00046AD4 File Offset: 0x00044CD4
		// (set) Token: 0x060016E6 RID: 5862 RVA: 0x00046AE6 File Offset: 0x00044CE6
		public string ReportFilter
		{
			get
			{
				return this[ReportScheduleSchema.ReportFilter] as string;
			}
			set
			{
				this[ReportScheduleSchema.ReportFilter] = value;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x00046AF4 File Offset: 0x00044CF4
		// (set) Token: 0x060016E8 RID: 5864 RVA: 0x00046B06 File Offset: 0x00044D06
		public string ReportLanguage
		{
			get
			{
				return this[ReportScheduleSchema.ReportLanguage] as string;
			}
			set
			{
				this[ReportScheduleSchema.ReportLanguage] = value;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x00046B14 File Offset: 0x00044D14
		// (set) Token: 0x060016EA RID: 5866 RVA: 0x00046B26 File Offset: 0x00044D26
		public Guid? BatchId
		{
			get
			{
				return (Guid?)this[ReportScheduleSchema.BatchId];
			}
			set
			{
				this[ReportScheduleSchema.BatchId] = value;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x00046B39 File Offset: 0x00044D39
		// (set) Token: 0x060016EC RID: 5868 RVA: 0x00046B4B File Offset: 0x00044D4B
		public DateTime LastScheduleTime
		{
			get
			{
				return (DateTime)this[ReportScheduleSchema.LastScheduleTime];
			}
			private set
			{
				this[ReportScheduleSchema.LastScheduleTime] = value;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x00046B5E File Offset: 0x00044D5E
		// (set) Token: 0x060016EE RID: 5870 RVA: 0x00046B70 File Offset: 0x00044D70
		public DateTime LastExecutionTime
		{
			get
			{
				return (DateTime)this[ReportScheduleSchema.LastExecutionTime];
			}
			private set
			{
				this[ReportScheduleSchema.LastExecutionTime] = value;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x00046B83 File Offset: 0x00044D83
		// (set) Token: 0x060016F0 RID: 5872 RVA: 0x00046B95 File Offset: 0x00044D95
		public ReportExecutionStatusType LastExecutionStatus
		{
			get
			{
				return (ReportExecutionStatusType)this[ReportScheduleSchema.LastExecutionStatus];
			}
			private set
			{
				this[ReportScheduleSchema.LastExecutionStatus] = value;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x00046BA8 File Offset: 0x00044DA8
		// (set) Token: 0x060016F2 RID: 5874 RVA: 0x00046BBA File Offset: 0x00044DBA
		public Guid LastExecutionContextId
		{
			get
			{
				return (Guid)this[ReportScheduleSchema.LastExecutionContextId];
			}
			private set
			{
				this[ReportScheduleSchema.LastExecutionContextId] = value;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x00046BCD File Offset: 0x00044DCD
		// (set) Token: 0x060016F4 RID: 5876 RVA: 0x00046BDF File Offset: 0x00044DDF
		public ReportExecutionStatusType CurrentExecutionStatus
		{
			get
			{
				return (ReportExecutionStatusType)this[ReportScheduleSchema.CurrentExecutionStatus];
			}
			private set
			{
				this[ReportScheduleSchema.CurrentExecutionStatus] = value;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x00046BF2 File Offset: 0x00044DF2
		// (set) Token: 0x060016F6 RID: 5878 RVA: 0x00046C04 File Offset: 0x00044E04
		public Guid CurrentExecutionContextId
		{
			get
			{
				return (Guid)this[ReportScheduleSchema.CurrentExecutionContextId];
			}
			private set
			{
				this[ReportScheduleSchema.CurrentExecutionContextId] = value;
			}
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00046C17 File Offset: 0x00044E17
		public override Type GetSchemaType()
		{
			return typeof(ReportScheduleSchema);
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x00046C23 File Offset: 0x00044E23
		private static bool IsExecutionCompleted(ReportExecutionStatusType executionStatus)
		{
			return executionStatus == ReportExecutionStatusType.Completed || executionStatus == ReportExecutionStatusType.Failed || executionStatus == ReportExecutionStatusType.Cancelled;
		}
	}
}
