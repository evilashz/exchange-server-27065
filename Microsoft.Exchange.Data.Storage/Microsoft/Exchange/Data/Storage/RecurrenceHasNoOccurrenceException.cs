using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000770 RID: 1904
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RecurrenceHasNoOccurrenceException : RecurrenceException
	{
		// Token: 0x060048AE RID: 18606 RVA: 0x00131618 File Offset: 0x0012F818
		public RecurrenceHasNoOccurrenceException(ExDateTime effectiveStartDate, ExDateTime effectiveEndDate, LocalizedString message) : base(message)
		{
			this.EffectiveEndDate = effectiveEndDate;
			this.EffectiveStartDate = effectiveStartDate;
		}

		// Token: 0x0400276A RID: 10090
		public readonly ExDateTime EffectiveStartDate;

		// Token: 0x0400276B RID: 10091
		public readonly ExDateTime EffectiveEndDate;
	}
}
