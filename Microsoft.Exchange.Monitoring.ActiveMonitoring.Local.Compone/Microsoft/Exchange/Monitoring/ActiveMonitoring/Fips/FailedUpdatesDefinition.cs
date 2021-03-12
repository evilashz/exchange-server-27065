using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Fips
{
	// Token: 0x02000511 RID: 1297
	public class FailedUpdatesDefinition
	{
		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001FED RID: 8173 RVA: 0x000C2E40 File Offset: 0x000C1040
		// (set) Token: 0x06001FEE RID: 8174 RVA: 0x000C2E48 File Offset: 0x000C1048
		public int NumberOfFailedEngine { get; set; }

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x000C2E51 File Offset: 0x000C1051
		// (set) Token: 0x06001FF0 RID: 8176 RVA: 0x000C2E59 File Offset: 0x000C1059
		public int ConsecutiveFailures { get; set; }
	}
}
