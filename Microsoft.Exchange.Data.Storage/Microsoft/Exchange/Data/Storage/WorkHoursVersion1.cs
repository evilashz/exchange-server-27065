using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EDD RID: 3805
	[Serializable]
	public class WorkHoursVersion1
	{
		// Token: 0x170022DF RID: 8927
		// (get) Token: 0x0600835D RID: 33629 RVA: 0x0023B4C1 File Offset: 0x002396C1
		// (set) Token: 0x0600835E RID: 33630 RVA: 0x0023B4C9 File Offset: 0x002396C9
		public WorkHoursTimeZone TimeZone
		{
			get
			{
				return this.timeZone;
			}
			set
			{
				this.timeZone = value;
			}
		}

		// Token: 0x170022E0 RID: 8928
		// (get) Token: 0x0600835F RID: 33631 RVA: 0x0023B4D2 File Offset: 0x002396D2
		// (set) Token: 0x06008360 RID: 33632 RVA: 0x0023B4DA File Offset: 0x002396DA
		public TimeSlot TimeSlot
		{
			get
			{
				return this.timeSlot;
			}
			set
			{
				this.timeSlot = value;
			}
		}

		// Token: 0x170022E1 RID: 8929
		// (get) Token: 0x06008361 RID: 33633 RVA: 0x0023B4E3 File Offset: 0x002396E3
		// (set) Token: 0x06008362 RID: 33634 RVA: 0x0023B4EB File Offset: 0x002396EB
		public DaysOfWeek WorkDays
		{
			get
			{
				return this.workDays;
			}
			set
			{
				this.workDays = value;
			}
		}

		// Token: 0x06008363 RID: 33635 RVA: 0x0023B4F4 File Offset: 0x002396F4
		public WorkHoursVersion1()
		{
		}

		// Token: 0x06008364 RID: 33636 RVA: 0x0023B4FC File Offset: 0x002396FC
		internal WorkHoursVersion1(WorkHoursTimeZone timeZone, TimeSlot timeSlot, DaysOfWeek workDays)
		{
			this.timeZone = timeZone;
			this.timeSlot = timeSlot;
			this.workDays = workDays;
		}

		// Token: 0x040057F3 RID: 22515
		private WorkHoursTimeZone timeZone;

		// Token: 0x040057F4 RID: 22516
		private TimeSlot timeSlot;

		// Token: 0x040057F5 RID: 22517
		private DaysOfWeek workDays;
	}
}
