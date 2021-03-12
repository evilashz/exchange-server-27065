using System;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x02000060 RID: 96
	public sealed class ExYearlyRecurringDate : ExYearlyRecurringTime
	{
		// Token: 0x06000361 RID: 865 RVA: 0x0000E228 File Offset: 0x0000C428
		public ExYearlyRecurringDate()
		{
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000E230 File Offset: 0x0000C430
		public ExYearlyRecurringDate(int month, int day, int hour, int minute, int second, int milliseconds) : this()
		{
			base.Month = month;
			this.Day = day;
			base.Hour = hour;
			base.Minute = minute;
			base.Second = second;
			base.Milliseconds = milliseconds;
			this.Validate();
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000E26B File Offset: 0x0000C46B
		public override DateTime GetInstance(int year)
		{
			return new DateTime(year, base.Month, this.Day, base.Hour, base.Minute, base.Second, base.Milliseconds, DateTimeKind.Unspecified);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000E298 File Offset: 0x0000C498
		protected override int GetSortIndex()
		{
			return 31 * (base.Month - 1) + (this.Day - 1) / 7;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000E2B0 File Offset: 0x0000C4B0
		public override string ToString()
		{
			return string.Format("Yearly recurring date: Month={0}; Day={1}; Hour={2}; Minute={3}; Second={4}; Millisecond={5}", new object[]
			{
				base.Month,
				this.Day,
				base.Hour,
				base.Minute,
				base.Second,
				base.Milliseconds
			});
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000E324 File Offset: 0x0000C524
		internal override void Validate()
		{
			if (base.Month < 1 || base.Month > 12)
			{
				throw new ArgumentOutOfRangeException("Month");
			}
			int num = (base.Month == 2) ? 28 : DateTime.DaysInMonth(2000, base.Month);
			if (this.Day < 1 || this.Day > num)
			{
				throw new ArgumentOutOfRangeException("Day");
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

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000E405 File Offset: 0x0000C605
		// (set) Token: 0x06000368 RID: 872 RVA: 0x0000E40D File Offset: 0x0000C60D
		public int Day
		{
			get
			{
				return this.day;
			}
			set
			{
				this.day = value;
			}
		}

		// Token: 0x0400019F RID: 415
		private int day;
	}
}
