using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence
{
	// Token: 0x0200006C RID: 108
	public abstract class RecurrenceRange
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000322 RID: 802
		public abstract RecurrenceRangeType Type { get; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00006579 File Offset: 0x00004779
		// (set) Token: 0x06000324 RID: 804 RVA: 0x00006581 File Offset: 0x00004781
		public ExDateTime StartDate { get; set; }
	}
}
