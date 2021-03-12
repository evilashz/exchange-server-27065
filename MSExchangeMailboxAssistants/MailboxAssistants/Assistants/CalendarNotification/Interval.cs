using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000E5 RID: 229
	internal struct Interval<T> where T : IComparable<T>
	{
		// Token: 0x06000998 RID: 2456 RVA: 0x00040700 File Offset: 0x0003E900
		public Interval(T minimum, bool minimumIsOpen, T maximum, bool maximumIsOpen)
		{
			if (0 < minimum.CompareTo(maximum) || ((minimumIsOpen || maximumIsOpen) && minimum.CompareTo(maximum) == 0))
			{
				throw new ArgumentOutOfRangeException("minimum");
			}
			this.minimum = minimum;
			this.minimumIsOpen = minimumIsOpen;
			this.maximum = maximum;
			this.maximumIsOpen = maximumIsOpen;
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x00040759 File Offset: 0x0003E959
		public T Minimum
		{
			get
			{
				return this.minimum;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x00040761 File Offset: 0x0003E961
		public bool MinimumIsOpen
		{
			get
			{
				return this.minimumIsOpen;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x00040769 File Offset: 0x0003E969
		public T Maximum
		{
			get
			{
				return this.maximum;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x00040771 File Offset: 0x0003E971
		public bool MaximumIsOpen
		{
			get
			{
				return this.maximumIsOpen;
			}
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0004077C File Offset: 0x0003E97C
		public bool IsOverlapped(Interval<T> other)
		{
			int num = this.minimum.CompareTo(other.maximum);
			int num2 = other.minimum.CompareTo(this.maximum);
			if (!((this.minimumIsOpen || other.maximumIsOpen) ? (0 > num) : (0 >= num)))
			{
				return false;
			}
			if (!other.minimumIsOpen && !this.maximumIsOpen)
			{
				return 0 >= num2;
			}
			return 0 > num2;
		}

		// Token: 0x0400066D RID: 1645
		private T minimum;

		// Token: 0x0400066E RID: 1646
		private bool minimumIsOpen;

		// Token: 0x0400066F RID: 1647
		private T maximum;

		// Token: 0x04000670 RID: 1648
		private bool maximumIsOpen;
	}
}
