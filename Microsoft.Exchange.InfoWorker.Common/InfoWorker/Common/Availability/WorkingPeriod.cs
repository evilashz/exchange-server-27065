using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000188 RID: 392
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class WorkingPeriod
	{
		// Token: 0x06000A81 RID: 2689 RVA: 0x0002C940 File Offset: 0x0002AB40
		public static DaysOfWeek DayToDays(DayOfWeek dow)
		{
			switch (dow)
			{
			case System.DayOfWeek.Sunday:
				return DaysOfWeek.Sunday;
			case System.DayOfWeek.Monday:
				return DaysOfWeek.Monday;
			case System.DayOfWeek.Tuesday:
				return DaysOfWeek.Tuesday;
			case System.DayOfWeek.Wednesday:
				return DaysOfWeek.Wednesday;
			case System.DayOfWeek.Thursday:
				return DaysOfWeek.Thursday;
			case System.DayOfWeek.Friday:
				return DaysOfWeek.Friday;
			case System.DayOfWeek.Saturday:
				return DaysOfWeek.Saturday;
			default:
				throw new ArgumentException("dow");
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x0002C98E File Offset: 0x0002AB8E
		// (set) Token: 0x06000A83 RID: 2691 RVA: 0x0002C996 File Offset: 0x0002AB96
		[DataMember]
		public DaysOfWeek DayOfWeek
		{
			get
			{
				return this.dayOfWeek;
			}
			set
			{
				this.dayOfWeek = value;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0002C99F File Offset: 0x0002AB9F
		// (set) Token: 0x06000A85 RID: 2693 RVA: 0x0002C9A7 File Offset: 0x0002ABA7
		[DataMember]
		public int StartTimeInMinutes
		{
			get
			{
				return this.startTimeInMinutes;
			}
			set
			{
				this.startTimeInMinutes = value;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0002C9B0 File Offset: 0x0002ABB0
		// (set) Token: 0x06000A87 RID: 2695 RVA: 0x0002C9B8 File Offset: 0x0002ABB8
		[DataMember]
		public int EndTimeInMinutes
		{
			get
			{
				return this.endTimeInMinutes;
			}
			set
			{
				this.endTimeInMinutes = value;
			}
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0002C9C4 File Offset: 0x0002ABC4
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = string.Concat(new object[]
				{
					"Day of week=",
					this.dayOfWeek,
					"Start time=",
					this.startTimeInMinutes,
					"End time=",
					this.endTimeInMinutes
				});
			}
			return this.toString;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0002CA34 File Offset: 0x0002AC34
		public WorkingPeriod()
		{
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0002CA3C File Offset: 0x0002AC3C
		internal WorkingPeriod(DaysOfWeek dayOfWeek, int startTimeInMinutes, int endTimeInMinutes)
		{
			this.dayOfWeek = dayOfWeek;
			this.startTimeInMinutes = startTimeInMinutes;
			this.endTimeInMinutes = endTimeInMinutes;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0002CA59 File Offset: 0x0002AC59
		internal bool IsSameWorkDay(DaysOfWeek dayToTest)
		{
			return this.DayOfWeek == dayToTest;
		}

		// Token: 0x04000802 RID: 2050
		private DaysOfWeek dayOfWeek;

		// Token: 0x04000803 RID: 2051
		private int startTimeInMinutes;

		// Token: 0x04000804 RID: 2052
		private int endTimeInMinutes;

		// Token: 0x04000805 RID: 2053
		private string toString;
	}
}
