using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000110 RID: 272
	internal class DayOfWeekTimeContext
	{
		// Token: 0x060007A6 RID: 1958 RVA: 0x0001F21F File Offset: 0x0001D41F
		private DayOfWeekTimeContext(ExDateTime dt, bool showDay, bool showTime)
		{
			this.DateTime = dt;
			this.ShowDay = showDay;
			this.ShowTime = showTime;
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x0001F23C File Offset: 0x0001D43C
		// (set) Token: 0x060007A8 RID: 1960 RVA: 0x0001F244 File Offset: 0x0001D444
		public bool ShowDay { get; private set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x0001F24D File Offset: 0x0001D44D
		// (set) Token: 0x060007AA RID: 1962 RVA: 0x0001F255 File Offset: 0x0001D455
		public bool ShowTime { get; private set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0001F25E File Offset: 0x0001D45E
		// (set) Token: 0x060007AC RID: 1964 RVA: 0x0001F266 File Offset: 0x0001D466
		public ExDateTime DateTime { get; private set; }

		// Token: 0x060007AD RID: 1965 RVA: 0x0001F26F File Offset: 0x0001D46F
		public static DayOfWeekTimeContext WithDayAndTime(ExDateTime dt)
		{
			return new DayOfWeekTimeContext(dt, true, true);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001F279 File Offset: 0x0001D479
		public static DayOfWeekTimeContext WithDayOnly(ExDateTime dt)
		{
			return new DayOfWeekTimeContext(dt, true, false);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001F283 File Offset: 0x0001D483
		public static DayOfWeekTimeContext WithTimeOnly(ExDateTime dt)
		{
			return new DayOfWeekTimeContext(dt, false, true);
		}
	}
}
