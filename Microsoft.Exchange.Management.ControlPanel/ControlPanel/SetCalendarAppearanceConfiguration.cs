using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000063 RID: 99
	[DataContract]
	public class SetCalendarAppearanceConfiguration : SetCalendarConfigurationBase
	{
		// Token: 0x17001849 RID: 6217
		// (get) Token: 0x06001A6D RID: 6765 RVA: 0x000544A6 File Offset: 0x000526A6
		// (set) Token: 0x06001A6E RID: 6766 RVA: 0x000544C2 File Offset: 0x000526C2
		[DataMember]
		public int WorkDays
		{
			get
			{
				return (int)(base["WorkDays"] ?? 0);
			}
			set
			{
				base["WorkDays"] = value;
			}
		}

		// Token: 0x1700184A RID: 6218
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x000544D8 File Offset: 0x000526D8
		// (set) Token: 0x06001A70 RID: 6768 RVA: 0x0005450C File Offset: 0x0005270C
		[DataMember]
		public int WorkingHoursStartTime
		{
			get
			{
				return (int)((TimeSpan)(base["WorkingHoursStartTime"] ?? TimeSpan.Zero)).TotalMinutes;
			}
			set
			{
				base["WorkingHoursStartTime"] = TimeSpan.FromMinutes((double)value);
			}
		}

		// Token: 0x1700184B RID: 6219
		// (get) Token: 0x06001A71 RID: 6769 RVA: 0x00054528 File Offset: 0x00052728
		// (set) Token: 0x06001A72 RID: 6770 RVA: 0x0005455C File Offset: 0x0005275C
		[DataMember]
		public int WorkingHoursEndTime
		{
			get
			{
				return (int)((TimeSpan)(base["WorkingHoursEndTime"] ?? TimeSpan.Zero)).TotalMinutes;
			}
			set
			{
				base["WorkingHoursEndTime"] = TimeSpan.FromMinutes((double)value);
			}
		}

		// Token: 0x1700184C RID: 6220
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x00054575 File Offset: 0x00052775
		// (set) Token: 0x06001A74 RID: 6772 RVA: 0x00054587 File Offset: 0x00052787
		[DataMember]
		public string WorkingHoursTimeZone
		{
			get
			{
				return (string)base["WorkingHoursTimeZone"];
			}
			set
			{
				base["WorkingHoursTimeZone"] = value;
			}
		}

		// Token: 0x1700184D RID: 6221
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x00054595 File Offset: 0x00052795
		// (set) Token: 0x06001A76 RID: 6774 RVA: 0x000545B1 File Offset: 0x000527B1
		[DataMember]
		public int WeekStartDay
		{
			get
			{
				return (int)(base["WeekStartDay"] ?? 0);
			}
			set
			{
				base["WeekStartDay"] = value;
				LocalSession.Current.WeekStartDay = -1;
			}
		}

		// Token: 0x1700184E RID: 6222
		// (get) Token: 0x06001A77 RID: 6775 RVA: 0x000545CF File Offset: 0x000527CF
		// (set) Token: 0x06001A78 RID: 6776 RVA: 0x000545EB File Offset: 0x000527EB
		[DataMember]
		public int FirstWeekOfYear
		{
			get
			{
				return (int)(base["FirstWeekOfYear"] ?? FirstWeekRules.FirstDay);
			}
			set
			{
				base["FirstWeekOfYear"] = value;
			}
		}

		// Token: 0x1700184F RID: 6223
		// (get) Token: 0x06001A79 RID: 6777 RVA: 0x000545FE File Offset: 0x000527FE
		// (set) Token: 0x06001A7A RID: 6778 RVA: 0x0005461A File Offset: 0x0005281A
		[DataMember]
		public bool ShowWeekNumbers
		{
			get
			{
				return (bool)(base["ShowWeekNumbers"] ?? false);
			}
			set
			{
				base["ShowWeekNumbers"] = value;
			}
		}

		// Token: 0x17001850 RID: 6224
		// (get) Token: 0x06001A7B RID: 6779 RVA: 0x0005462D File Offset: 0x0005282D
		// (set) Token: 0x06001A7C RID: 6780 RVA: 0x0005463F File Offset: 0x0005283F
		[DataMember]
		public string TimeIncrement
		{
			get
			{
				return (string)base["TimeIncrement"];
			}
			set
			{
				base["TimeIncrement"] = value;
			}
		}
	}
}
