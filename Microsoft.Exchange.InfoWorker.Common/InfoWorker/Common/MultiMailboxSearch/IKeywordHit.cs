using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001EA RID: 490
	internal interface IKeywordHit
	{
		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000CD8 RID: 3288
		string Phrase { get; }

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000CD9 RID: 3289
		ulong Count { get; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000CDA RID: 3290
		ByteQuantifiedSize Size { get; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000CDB RID: 3291
		IList<Pair<MailboxInfo, Exception>> Errors { get; }

		// Token: 0x06000CDC RID: 3292
		void Merge(IKeywordHit hits);
	}
}
