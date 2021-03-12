using System;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001F6 RID: 502
	public class RuleMetaData
	{
		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x000421D8 File Offset: 0x000403D8
		// (set) Token: 0x06001510 RID: 5392 RVA: 0x000421E0 File Offset: 0x000403E0
		public string Author { get; set; }

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x000421E9 File Offset: 0x000403E9
		// (set) Token: 0x06001512 RID: 5394 RVA: 0x000421F1 File Offset: 0x000403F1
		public string ValidationFailedReason { get; set; }
	}
}
