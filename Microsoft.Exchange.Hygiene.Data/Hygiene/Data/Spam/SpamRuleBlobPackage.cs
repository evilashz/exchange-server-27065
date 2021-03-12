using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x02000205 RID: 517
	internal class SpamRuleBlobPackage
	{
		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001597 RID: 5527 RVA: 0x00043420 File Offset: 0x00041620
		// (set) Token: 0x06001598 RID: 5528 RVA: 0x00043428 File Offset: 0x00041628
		public List<SpamRuleBlob> SpamRuleBlobs { get; set; }

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x00043431 File Offset: 0x00041631
		// (set) Token: 0x0600159A RID: 5530 RVA: 0x00043439 File Offset: 0x00041639
		public List<SpamRuleProcessorBlob> SpamRuleProcessorBlobs { get; set; }
	}
}
