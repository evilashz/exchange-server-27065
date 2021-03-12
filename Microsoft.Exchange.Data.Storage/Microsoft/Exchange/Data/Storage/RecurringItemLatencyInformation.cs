using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200038D RID: 909
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RecurringItemLatencyInformation
	{
		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x060027F1 RID: 10225 RVA: 0x0009F492 File Offset: 0x0009D692
		// (set) Token: 0x060027F2 RID: 10226 RVA: 0x0009F49A File Offset: 0x0009D69A
		public string Subject { get; set; }

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x060027F3 RID: 10227 RVA: 0x0009F4A3 File Offset: 0x0009D6A3
		// (set) Token: 0x060027F4 RID: 10228 RVA: 0x0009F4AB File Offset: 0x0009D6AB
		public long BlobStreamTime { get; set; }

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x060027F5 RID: 10229 RVA: 0x0009F4B4 File Offset: 0x0009D6B4
		// (set) Token: 0x060027F6 RID: 10230 RVA: 0x0009F4BC File Offset: 0x0009D6BC
		public long BlobParseTime { get; set; }

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x060027F7 RID: 10231 RVA: 0x0009F4C5 File Offset: 0x0009D6C5
		// (set) Token: 0x060027F8 RID: 10232 RVA: 0x0009F4CD File Offset: 0x0009D6CD
		public long BlobExpansionTime { get; set; }

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x060027F9 RID: 10233 RVA: 0x0009F4D6 File Offset: 0x0009D6D6
		// (set) Token: 0x060027FA RID: 10234 RVA: 0x0009F4DE File Offset: 0x0009D6DE
		public long AddRowsForInstancesTime { get; set; }
	}
}
