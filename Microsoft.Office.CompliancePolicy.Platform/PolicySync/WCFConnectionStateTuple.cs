using System;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000115 RID: 277
	internal class WCFConnectionStateTuple<TClient> : IPooledServiceProxy<TClient>
	{
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x000175D7 File Offset: 0x000157D7
		// (set) Token: 0x06000796 RID: 1942 RVA: 0x000175DF File Offset: 0x000157DF
		public TClient Client { get; set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x000175E8 File Offset: 0x000157E8
		// (set) Token: 0x06000798 RID: 1944 RVA: 0x000175F0 File Offset: 0x000157F0
		public DateTime LastUsed { get; set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x000175F9 File Offset: 0x000157F9
		// (set) Token: 0x0600079A RID: 1946 RVA: 0x00017601 File Offset: 0x00015801
		public string Tag { get; set; }
	}
}
