using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel.DataContracts
{
	// Token: 0x020000F8 RID: 248
	[DataContract]
	public class TimeOfDayItem
	{
		// Token: 0x06001EC5 RID: 7877 RVA: 0x0005C5BC File Offset: 0x0005A7BC
		public TimeOfDayItem()
		{
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x0005C5C4 File Offset: 0x0005A7C4
		public TimeOfDayItem(TimeOfDay taskTimeOfDay)
		{
			this.CustomPeriodDays = (int)taskTimeOfDay.CustomPeriodDays;
			this.CustomPeriodEndTime = ((taskTimeOfDay.CustomPeriodEndTime != null) ? taskTimeOfDay.CustomPeriodEndTime.Value.TotalMinutes.ToString() : null);
			this.CustomPeriodStartTime = ((taskTimeOfDay.CustomPeriodStartTime != null) ? taskTimeOfDay.CustomPeriodStartTime.Value.TotalMinutes.ToString() : null);
			this.TimeOfDayType = (int)taskTimeOfDay.TimeOfDayType;
		}

		// Token: 0x170019E1 RID: 6625
		// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x0005C661 File Offset: 0x0005A861
		// (set) Token: 0x06001EC8 RID: 7880 RVA: 0x0005C669 File Offset: 0x0005A869
		[DataMember]
		public int CustomPeriodDays { get; set; }

		// Token: 0x170019E2 RID: 6626
		// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x0005C672 File Offset: 0x0005A872
		// (set) Token: 0x06001ECA RID: 7882 RVA: 0x0005C67A File Offset: 0x0005A87A
		[DataMember]
		public string CustomPeriodEndTime { get; set; }

		// Token: 0x170019E3 RID: 6627
		// (get) Token: 0x06001ECB RID: 7883 RVA: 0x0005C683 File Offset: 0x0005A883
		// (set) Token: 0x06001ECC RID: 7884 RVA: 0x0005C68B File Offset: 0x0005A88B
		[DataMember]
		public string CustomPeriodStartTime { get; set; }

		// Token: 0x170019E4 RID: 6628
		// (get) Token: 0x06001ECD RID: 7885 RVA: 0x0005C694 File Offset: 0x0005A894
		// (set) Token: 0x06001ECE RID: 7886 RVA: 0x0005C69C File Offset: 0x0005A89C
		[DataMember]
		public int TimeOfDayType { get; set; }

		// Token: 0x06001ECF RID: 7887 RVA: 0x0005C6A8 File Offset: 0x0005A8A8
		public TimeOfDay ToTaskObject()
		{
			TimeOfDayType timeOfDayType = (TimeOfDayType)this.TimeOfDayType;
			DaysOfWeek customPeriodDays = (DaysOfWeek)this.CustomPeriodDays;
			if (timeOfDayType == Microsoft.Exchange.Data.TimeOfDayType.CustomPeriod)
			{
				int num = 0;
				int num2 = 0;
				int.TryParse(this.CustomPeriodStartTime, out num);
				int.TryParse(this.CustomPeriodEndTime, out num2);
				TimeSpan value = TimeSpan.FromMinutes((double)num);
				TimeSpan value2 = TimeSpan.FromMinutes((double)num2);
				return new TimeOfDay(timeOfDayType, customPeriodDays, new TimeSpan?(value), new TimeSpan?(value2));
			}
			return new TimeOfDay(timeOfDayType, customPeriodDays, null, null);
		}
	}
}
