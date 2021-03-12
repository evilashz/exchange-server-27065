using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Search.Query
{
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InstantSearchTransientException : StorageTransientException
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00005CE0 File Offset: 0x00003EE0
		public InstantSearchTransientException(LocalizedString message, QueryStatistics statistics) : base(message)
		{
			this.QueryStatistics = statistics;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005CF0 File Offset: 0x00003EF0
		public InstantSearchTransientException(LocalizedString message, Exception innerException, QueryStatistics statistics) : base(message, innerException)
		{
			this.QueryStatistics = statistics;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00005D01 File Offset: 0x00003F01
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00005D09 File Offset: 0x00003F09
		public QueryStatistics QueryStatistics { get; private set; }
	}
}
