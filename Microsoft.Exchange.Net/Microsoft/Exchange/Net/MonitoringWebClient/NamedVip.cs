using System;
using System.Net;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007C7 RID: 1991
	internal class NamedVip
	{
		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x0600292D RID: 10541 RVA: 0x00058284 File Offset: 0x00056484
		// (set) Token: 0x0600292E RID: 10542 RVA: 0x0005828C File Offset: 0x0005648C
		public string Name { get; set; }

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x0600292F RID: 10543 RVA: 0x00058295 File Offset: 0x00056495
		// (set) Token: 0x06002930 RID: 10544 RVA: 0x0005829D File Offset: 0x0005649D
		public IPAddress IPAddress { get; set; }

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06002931 RID: 10545 RVA: 0x000582A6 File Offset: 0x000564A6
		// (set) Token: 0x06002932 RID: 10546 RVA: 0x000582AE File Offset: 0x000564AE
		public string IPAddressString { get; set; }

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06002933 RID: 10547 RVA: 0x000582B7 File Offset: 0x000564B7
		// (set) Token: 0x06002934 RID: 10548 RVA: 0x000582BF File Offset: 0x000564BF
		public string ForestName { get; set; }
	}
}
