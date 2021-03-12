using System;

namespace Microsoft.Exchange.Data.ApplicationLogic.TimeZoneOffsets
{
	// Token: 0x020001BB RID: 443
	public class TimeZoneOffset
	{
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x00046E91 File Offset: 0x00045091
		// (set) Token: 0x0600111B RID: 4379 RVA: 0x00046E99 File Offset: 0x00045099
		public string TimeZoneId { get; set; }

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x00046EA2 File Offset: 0x000450A2
		// (set) Token: 0x0600111D RID: 4381 RVA: 0x00046EAA File Offset: 0x000450AA
		public TimeZoneRange[] OffsetRanges { get; set; }
	}
}
