using System;
using System.Net;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000505 RID: 1285
	internal struct UserWithCredential
	{
		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x06002E20 RID: 11808 RVA: 0x000B8D0E File Offset: 0x000B6F0E
		// (set) Token: 0x06002E21 RID: 11809 RVA: 0x000B8D16 File Offset: 0x000B6F16
		public ADUser User { get; set; }

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x06002E22 RID: 11810 RVA: 0x000B8D1F File Offset: 0x000B6F1F
		// (set) Token: 0x06002E23 RID: 11811 RVA: 0x000B8D27 File Offset: 0x000B6F27
		public NetworkCredential Credential { get; set; }
	}
}
