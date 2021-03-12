using System;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence
{
	// Token: 0x0200006A RID: 106
	public sealed class AbsoluteYearlyRecurrencePattern : RecurrencePattern
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00006541 File Offset: 0x00004741
		// (set) Token: 0x0600031B RID: 795 RVA: 0x00006549 File Offset: 0x00004749
		public int DayOfMonth { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00006552 File Offset: 0x00004752
		// (set) Token: 0x0600031D RID: 797 RVA: 0x0000655A File Offset: 0x0000475A
		public int Month { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00006563 File Offset: 0x00004763
		public override RecurrencePatternType Type
		{
			get
			{
				return RecurrencePatternType.AbsoluteYearly;
			}
		}
	}
}
