using System;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence
{
	// Token: 0x0200006F RID: 111
	public sealed class NumberedRecurrenceRange : RecurrenceRange
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600032C RID: 812 RVA: 0x000065B9 File Offset: 0x000047B9
		// (set) Token: 0x0600032D RID: 813 RVA: 0x000065C1 File Offset: 0x000047C1
		public int NumberOfOccurrences { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600032E RID: 814 RVA: 0x000065CA File Offset: 0x000047CA
		public override RecurrenceRangeType Type
		{
			get
			{
				return RecurrenceRangeType.Numbered;
			}
		}
	}
}
