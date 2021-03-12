using System;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000013 RID: 19
	public class GlobalTunables
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000038BB File Offset: 0x00001ABB
		// (set) Token: 0x06000078 RID: 120 RVA: 0x000038C3 File Offset: 0x00001AC3
		public string LocalMachineName { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000038CC File Offset: 0x00001ACC
		// (set) Token: 0x0600007A RID: 122 RVA: 0x000038D4 File Offset: 0x00001AD4
		public TimeSpan ThrottleGroupCacheRefreshFrequency { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000038DD File Offset: 0x00001ADD
		// (set) Token: 0x0600007C RID: 124 RVA: 0x000038E5 File Offset: 0x00001AE5
		public TimeSpan ThrottleGroupCacheRefreshStartDelay { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000038EE File Offset: 0x00001AEE
		// (set) Token: 0x0600007E RID: 126 RVA: 0x000038F6 File Offset: 0x00001AF6
		public int ThrottlingV2SupportedServerVersion { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000038FF File Offset: 0x00001AFF
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00003907 File Offset: 0x00001B07
		public bool IsRunningMock { get; set; }
	}
}
