using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003E7 RID: 999
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NoEndRecurrenceRange : RecurrenceRange
	{
		// Token: 0x06002D8F RID: 11663 RVA: 0x000BBB9A File Offset: 0x000B9D9A
		public NoEndRecurrenceRange(ExDateTime startDate)
		{
			this.StartDate = startDate;
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x000BBBA9 File Offset: 0x000B9DA9
		public override bool Equals(RecurrenceRange value)
		{
			return value is NoEndRecurrenceRange && base.Equals(value);
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x000BBBBC File Offset: 0x000B9DBC
		public override string ToString()
		{
			return string.Format("Starts {0}, no end date", this.StartDate.ToString(DateTimeFormatInfo.InvariantInfo));
		}
	}
}
