using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000587 RID: 1415
	public class DailyServiceAvailability : DailyAvailability
	{
		// Token: 0x060031E1 RID: 12769 RVA: 0x000CAB7D File Offset: 0x000C8D7D
		public DailyServiceAvailability(DateTime date) : base(date)
		{
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x000CAB88 File Offset: 0x000C8D88
		public override string ToString()
		{
			return string.Format("{0} - {1:p2}", base.Date.ToString("d"), base.AvailabilityPercentage);
		}
	}
}
