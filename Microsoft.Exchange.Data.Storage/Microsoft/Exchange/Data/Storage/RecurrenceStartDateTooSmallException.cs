using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000771 RID: 1905
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RecurrenceStartDateTooSmallException : RecurrenceException
	{
		// Token: 0x060048AF RID: 18607 RVA: 0x0013162F File Offset: 0x0012F82F
		public RecurrenceStartDateTooSmallException(ExDateTime startDate, LocalizedString message) : base(message)
		{
			this.StartDate = startDate;
		}

		// Token: 0x0400276C RID: 10092
		public static readonly ExDateTime MinimumAllowedStartDate = new ExDateTime(ExTimeZone.UnspecifiedTimeZone, 1601, 4, 1, 0, 0, 0);

		// Token: 0x0400276D RID: 10093
		public readonly ExDateTime StartDate;
	}
}
