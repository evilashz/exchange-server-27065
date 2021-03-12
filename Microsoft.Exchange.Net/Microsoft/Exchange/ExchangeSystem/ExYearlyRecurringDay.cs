using System;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x02000061 RID: 97
	public sealed class ExYearlyRecurringDay : ExYearlyRecurringTime
	{
		// Token: 0x06000369 RID: 873 RVA: 0x0000E416 File Offset: 0x0000C616
		public ExYearlyRecurringDay()
		{
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000E420 File Offset: 0x0000C620
		public ExYearlyRecurringDay(int occurrence, DayOfWeek dayOfWeek, int month, int hour, int minute, int second, int milliseconds) : this()
		{
			this.Occurrence = occurrence;
			this.DayOfWeek = dayOfWeek;
			base.Month = month;
			base.Hour = hour;
			base.Minute = minute;
			base.Second = second;
			base.Milliseconds = milliseconds;
			this.Validate();
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000E470 File Offset: 0x0000C670
		public override DateTime GetInstance(int year)
		{
			DateTime result;
			if (this.Occurrence == -1)
			{
				DateTime dateTime = new DateTime(year, base.Month, DateTime.DaysInMonth(year, base.Month), base.Hour, base.Minute, base.Second, base.Milliseconds, DateTimeKind.Unspecified);
				int num = dateTime.DayOfWeek - this.DayOfWeek;
				if (num < 0)
				{
					num += 7;
				}
				result = dateTime.AddDays((double)(-(double)num));
			}
			else
			{
				DateTime dateTime2 = new DateTime(year, base.Month, 1, base.Hour, base.Minute, base.Second, base.Milliseconds, DateTimeKind.Unspecified);
				int num2 = this.DayOfWeek - dateTime2.DayOfWeek;
				if (num2 < 0)
				{
					num2 += 7;
				}
				num2 += (this.Occurrence - 1) * 7;
				result = dateTime2.AddDays((double)num2);
			}
			return result;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000E53C File Offset: 0x0000C73C
		public override string ToString()
		{
			return string.Format("Yearly recurring day: Occurrence={0}; DayOfWeek={1}; Month={2}; Hour={3}; Minute={4}; Second={5}; Millisecond={6}", new object[]
			{
				this.Occurrence,
				this.DayOfWeek,
				base.Month,
				base.Hour,
				base.Minute,
				base.Second,
				base.Milliseconds
			});
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000E5C0 File Offset: 0x0000C7C0
		internal override void Validate()
		{
			if (this.Occurrence > 4 || (this.Occurrence < 1 && this.Occurrence != -1))
			{
				throw new ArgumentOutOfRangeException("Occurrence");
			}
			if (this.DayOfWeek < DayOfWeek.Sunday || this.DayOfWeek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException("DayOfWeek");
			}
			if (base.Month < 1 || base.Month > 12)
			{
				throw new ArgumentOutOfRangeException("Month");
			}
			if (base.Hour < 0 || base.Hour > 23)
			{
				throw new ArgumentOutOfRangeException("Hour");
			}
			if (base.Minute < 0 || base.Minute > 59)
			{
				throw new ArgumentOutOfRangeException("Minute");
			}
			if (base.Second < 0 || base.Second > 59)
			{
				throw new ArgumentOutOfRangeException("Second");
			}
			if (base.Milliseconds < 0 || base.Milliseconds > 999)
			{
				throw new ArgumentOutOfRangeException("Milliseconds");
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000E6A9 File Offset: 0x0000C8A9
		// (set) Token: 0x0600036F RID: 879 RVA: 0x0000E6B1 File Offset: 0x0000C8B1
		public int Occurrence
		{
			get
			{
				return this.occurrence;
			}
			set
			{
				this.occurrence = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000E6BA File Offset: 0x0000C8BA
		// (set) Token: 0x06000371 RID: 881 RVA: 0x0000E6C2 File Offset: 0x0000C8C2
		public DayOfWeek DayOfWeek
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

		// Token: 0x06000372 RID: 882 RVA: 0x0000E6CC File Offset: 0x0000C8CC
		protected override int GetSortIndex()
		{
			int num = (this.Occurrence == -1) ? 4 : this.Occurrence;
			return 31 * (base.Month - 1) + num;
		}

		// Token: 0x040001A0 RID: 416
		private DayOfWeek dayOfWeek;

		// Token: 0x040001A1 RID: 417
		private int occurrence;
	}
}
