using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Search.Query
{
	// Token: 0x02000007 RID: 7
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InstantSearchPermanentException : StorageTransientException
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00003EFA File Offset: 0x000020FA
		public InstantSearchPermanentException(LocalizedString message, QueryStatistics statistics) : base(message)
		{
			this.QueryStatistics = statistics;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003F0A File Offset: 0x0000210A
		public InstantSearchPermanentException(LocalizedString message, Exception innerException, QueryStatistics statistics) : base(message, innerException)
		{
			this.QueryStatistics = statistics;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003F1B File Offset: 0x0000211B
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00003F23 File Offset: 0x00002123
		public QueryStatistics QueryStatistics { get; private set; }
	}
}
