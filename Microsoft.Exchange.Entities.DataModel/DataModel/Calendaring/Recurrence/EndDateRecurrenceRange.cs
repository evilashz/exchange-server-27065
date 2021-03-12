using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence
{
	// Token: 0x0200006D RID: 109
	public sealed class EndDateRecurrenceRange : RecurrenceRange
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00006592 File Offset: 0x00004792
		// (set) Token: 0x06000327 RID: 807 RVA: 0x0000659A File Offset: 0x0000479A
		public ExDateTime EndDate { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000328 RID: 808 RVA: 0x000065A3 File Offset: 0x000047A3
		public override RecurrenceRangeType Type
		{
			get
			{
				return RecurrenceRangeType.EndDate;
			}
		}
	}
}
