using System;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence
{
	// Token: 0x0200006E RID: 110
	public sealed class NoEndRecurrenceRange : RecurrenceRange
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600032A RID: 810 RVA: 0x000065AE File Offset: 0x000047AE
		public override RecurrenceRangeType Type
		{
			get
			{
				return RecurrenceRangeType.NoEnd;
			}
		}
	}
}
