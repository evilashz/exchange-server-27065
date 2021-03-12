using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200000B RID: 11
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CloudStatistics
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00006820 File Offset: 0x00004A20
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00006828 File Offset: 0x00004A28
		internal long? TotalItemsInSourceMailbox { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00006831 File Offset: 0x00004A31
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00006839 File Offset: 0x00004A39
		internal long? TotalFoldersInSourceMailbox { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00006842 File Offset: 0x00004A42
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x0000684A File Offset: 0x00004A4A
		internal long? TotalSizeOfSourceMailbox { get; set; }
	}
}
