using System;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000138 RID: 312
	[Serializable]
	public sealed class SyncInfo
	{
		// Token: 0x06000914 RID: 2324 RVA: 0x0001F161 File Offset: 0x0001D361
		public SyncInfo()
		{
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0001F169 File Offset: 0x0001D369
		public SyncInfo(byte[] previousSyncCookie, byte[] currentSyncCookie, PolicyVersion latestVersion)
		{
			this.PreviousSyncCookie = previousSyncCookie;
			this.CurrentSyncCookie = currentSyncCookie;
			this.LatestVersion = latestVersion;
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0001F186 File Offset: 0x0001D386
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x0001F18E File Offset: 0x0001D38E
		public byte[] PreviousSyncCookie { get; set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x0001F197 File Offset: 0x0001D397
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x0001F19F File Offset: 0x0001D39F
		public byte[] CurrentSyncCookie { get; set; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0001F1A8 File Offset: 0x0001D3A8
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x0001F1B0 File Offset: 0x0001D3B0
		public PolicyVersion LatestVersion { get; set; }
	}
}
