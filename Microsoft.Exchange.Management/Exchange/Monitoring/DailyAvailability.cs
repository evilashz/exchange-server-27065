using System;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000586 RID: 1414
	public abstract class DailyAvailability : IComparable
	{
		// Token: 0x060031DA RID: 12762 RVA: 0x000CAADB File Offset: 0x000C8CDB
		protected DailyAvailability(DateTime date)
		{
			this.date = date;
		}

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x060031DB RID: 12763 RVA: 0x000CAAEA File Offset: 0x000C8CEA
		public DateTime Date
		{
			get
			{
				return this.date;
			}
		}

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x060031DC RID: 12764 RVA: 0x000CAAF2 File Offset: 0x000C8CF2
		// (set) Token: 0x060031DD RID: 12765 RVA: 0x000CAAFA File Offset: 0x000C8CFA
		public double AvailabilityPercentage
		{
			get
			{
				return this.availabilityPercentage;
			}
			internal set
			{
				this.availabilityPercentage = value;
			}
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x000CAB04 File Offset: 0x000C8D04
		public int CompareTo(object obj)
		{
			if (obj is DailyAvailability)
			{
				DailyAvailability dailyAvailability = (DailyAvailability)obj;
				return this.Date.CompareTo(dailyAvailability.Date);
			}
			throw new ArgumentException(Strings.ExceptionIncomparableType(obj.GetType()), "obj");
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x000CAB4F File Offset: 0x000C8D4F
		public override bool Equals(object obj)
		{
			return this.CompareTo(obj) == 0;
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x000CAB5C File Offset: 0x000C8D5C
		public override int GetHashCode()
		{
			return this.Date.GetHashCode();
		}

		// Token: 0x0400233A RID: 9018
		private readonly DateTime date;

		// Token: 0x0400233B RID: 9019
		private double availabilityPercentage;
	}
}
