using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200076F RID: 1903
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RecurrenceEndDateTooBigException : RecurrenceException
	{
		// Token: 0x060048AC RID: 18604 RVA: 0x001315EC File Offset: 0x0012F7EC
		public RecurrenceEndDateTooBigException(ExDateTime endDate, LocalizedString message) : base(message)
		{
			this.EndDate = endDate;
		}

		// Token: 0x04002768 RID: 10088
		public readonly ExDateTime EndDate;

		// Token: 0x04002769 RID: 10089
		public static readonly ExDateTime MaximumAllowedEndDate = new ExDateTime(ExTimeZone.UnspecifiedTimeZone, 4500, 9, 1, 0, 0, 0);
	}
}
