using System;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence
{
	// Token: 0x0200006B RID: 107
	public sealed class DailyRecurrencePattern : RecurrencePattern
	{
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000656E File Offset: 0x0000476E
		public override RecurrencePatternType Type
		{
			get
			{
				return RecurrencePatternType.Daily;
			}
		}
	}
}
