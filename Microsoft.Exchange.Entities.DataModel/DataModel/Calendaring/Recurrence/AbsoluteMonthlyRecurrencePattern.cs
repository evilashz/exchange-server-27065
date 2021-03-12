using System;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence
{
	// Token: 0x02000069 RID: 105
	public sealed class AbsoluteMonthlyRecurrencePattern : RecurrencePattern
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00006525 File Offset: 0x00004725
		// (set) Token: 0x06000317 RID: 791 RVA: 0x0000652D File Offset: 0x0000472D
		public int DayOfMonth { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00006536 File Offset: 0x00004736
		public override RecurrencePatternType Type
		{
			get
			{
				return RecurrencePatternType.AbsoluteMonthly;
			}
		}
	}
}
