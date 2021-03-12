using System;
using System.Collections.Generic;
using Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000364 RID: 868
	internal sealed class SearchMailboxesData
	{
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x00082D94 File Offset: 0x00080F94
		// (set) Token: 0x0600183E RID: 6206 RVA: 0x00082D9C File Offset: 0x00080F9C
		internal MailboxQuery MailboxQuery { get; set; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x00082DA5 File Offset: 0x00080FA5
		// (set) Token: 0x06001840 RID: 6208 RVA: 0x00082DAD File Offset: 0x00080FAD
		internal ResultAggregator ResultAggregator { get; set; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x00082DB6 File Offset: 0x00080FB6
		// (set) Token: 0x06001842 RID: 6210 RVA: 0x00082DBE File Offset: 0x00080FBE
		internal List<FailedSearchMailbox> NonSearchableMailboxes { get; set; }
	}
}
