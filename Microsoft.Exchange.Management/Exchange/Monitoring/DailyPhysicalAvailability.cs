using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000588 RID: 1416
	public class DailyPhysicalAvailability : DailyAvailability
	{
		// Token: 0x060031E3 RID: 12771 RVA: 0x000CABBD File Offset: 0x000C8DBD
		public DailyPhysicalAvailability(DateTime date) : base(date)
		{
		}

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x060031E4 RID: 12772 RVA: 0x000CABC6 File Offset: 0x000C8DC6
		// (set) Token: 0x060031E5 RID: 12773 RVA: 0x000CABCE File Offset: 0x000C8DCE
		public double RawAvailabilityPercentage
		{
			get
			{
				return this.rawAvailabilityPercentage;
			}
			internal set
			{
				this.rawAvailabilityPercentage = value;
			}
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x000CABD8 File Offset: 0x000C8DD8
		public override string ToString()
		{
			return string.Format("{0} - {1:p2} ({2:p2})", base.Date.ToString("d"), base.AvailabilityPercentage, this.RawAvailabilityPercentage);
		}

		// Token: 0x0400233C RID: 9020
		private double rawAvailabilityPercentage;
	}
}
