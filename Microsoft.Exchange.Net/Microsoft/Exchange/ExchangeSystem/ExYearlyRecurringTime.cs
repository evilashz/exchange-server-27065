using System;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x0200005F RID: 95
	public abstract class ExYearlyRecurringTime : IComparable, IComparable<ExYearlyRecurringTime>
	{
		// Token: 0x06000350 RID: 848 RVA: 0x0000E185 File Offset: 0x0000C385
		public ExYearlyRecurringTime()
		{
		}

		// Token: 0x06000351 RID: 849
		public abstract DateTime GetInstance(int year);

		// Token: 0x06000352 RID: 850 RVA: 0x0000E18D File Offset: 0x0000C38D
		public int CompareTo(ExYearlyRecurringTime value)
		{
			return Math.Sign(this.GetSortIndex() - value.GetSortIndex());
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000E1A4 File Offset: 0x0000C3A4
		public int CompareTo(object value)
		{
			ExYearlyRecurringTime exYearlyRecurringTime = value as ExYearlyRecurringTime;
			if (value != null && exYearlyRecurringTime == null)
			{
				throw new ArgumentException();
			}
			return this.CompareTo(exYearlyRecurringTime);
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000E1CB File Offset: 0x0000C3CB
		internal int SortIndex
		{
			get
			{
				return this.GetSortIndex();
			}
		}

		// Token: 0x06000355 RID: 853
		protected abstract int GetSortIndex();

		// Token: 0x06000356 RID: 854
		internal abstract void Validate();

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000E1D3 File Offset: 0x0000C3D3
		// (set) Token: 0x06000358 RID: 856 RVA: 0x0000E1DB File Offset: 0x0000C3DB
		public int Month
		{
			get
			{
				return this.month;
			}
			set
			{
				this.month = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000E1E4 File Offset: 0x0000C3E4
		// (set) Token: 0x0600035A RID: 858 RVA: 0x0000E1EC File Offset: 0x0000C3EC
		public int Hour
		{
			get
			{
				return this.hour;
			}
			set
			{
				this.hour = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000E1F5 File Offset: 0x0000C3F5
		// (set) Token: 0x0600035C RID: 860 RVA: 0x0000E1FD File Offset: 0x0000C3FD
		public int Minute
		{
			get
			{
				return this.minute;
			}
			set
			{
				this.minute = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000E206 File Offset: 0x0000C406
		// (set) Token: 0x0600035E RID: 862 RVA: 0x0000E20E File Offset: 0x0000C40E
		public int Second
		{
			get
			{
				return this.second;
			}
			set
			{
				this.second = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000E217 File Offset: 0x0000C417
		// (set) Token: 0x06000360 RID: 864 RVA: 0x0000E21F File Offset: 0x0000C41F
		public int Milliseconds
		{
			get
			{
				return this.milliseconds;
			}
			set
			{
				this.milliseconds = value;
			}
		}

		// Token: 0x0400019A RID: 410
		private int month;

		// Token: 0x0400019B RID: 411
		private int hour;

		// Token: 0x0400019C RID: 412
		private int minute;

		// Token: 0x0400019D RID: 413
		private int second;

		// Token: 0x0400019E RID: 414
		private int milliseconds;
	}
}
