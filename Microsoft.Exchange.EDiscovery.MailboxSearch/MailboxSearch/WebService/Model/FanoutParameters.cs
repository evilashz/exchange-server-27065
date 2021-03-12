using System;
using System.Collections.Generic;
using Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x0200004B RID: 75
	internal class FanoutParameters
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00015C14 File Offset: 0x00013E14
		// (set) Token: 0x0600036D RID: 877 RVA: 0x00015C1C File Offset: 0x00013E1C
		public GroupId GroupId { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00015C25 File Offset: 0x00013E25
		// (set) Token: 0x0600036F RID: 879 RVA: 0x00015C2D File Offset: 0x00013E2D
		public SearchSource Source { get; set; }

		// Token: 0x0200004C RID: 76
		internal class FanoutState
		{
			// Token: 0x1700011E RID: 286
			// (get) Token: 0x06000371 RID: 881 RVA: 0x00015C3E File Offset: 0x00013E3E
			// (set) Token: 0x06000372 RID: 882 RVA: 0x00015C46 File Offset: 0x00013E46
			public IExchangeProxy Proxy { get; set; }

			// Token: 0x1700011F RID: 287
			// (get) Token: 0x06000373 RID: 883 RVA: 0x00015C4F File Offset: 0x00013E4F
			// (set) Token: 0x06000374 RID: 884 RVA: 0x00015C57 File Offset: 0x00013E57
			public IList<FanoutParameters> Parameters { get; set; }
		}
	}
}
