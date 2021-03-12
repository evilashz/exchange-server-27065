using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000744 RID: 1860
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LastOccurrenceDeletionException : RecurrenceException
	{
		// Token: 0x06004818 RID: 18456 RVA: 0x0013098B File Offset: 0x0012EB8B
		public LastOccurrenceDeletionException(ExDateTime dateId, LocalizedString message) : base(message)
		{
			this.DateId = dateId;
		}

		// Token: 0x0400273D RID: 10045
		public readonly ExDateTime DateId;
	}
}
