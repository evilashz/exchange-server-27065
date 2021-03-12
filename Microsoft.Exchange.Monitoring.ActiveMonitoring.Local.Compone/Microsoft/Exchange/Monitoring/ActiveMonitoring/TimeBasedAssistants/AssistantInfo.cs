using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TimeBasedAssistants
{
	// Token: 0x02000527 RID: 1319
	internal class AssistantInfo
	{
		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06002067 RID: 8295 RVA: 0x000C67A0 File Offset: 0x000C49A0
		// (set) Token: 0x06002068 RID: 8296 RVA: 0x000C67A8 File Offset: 0x000C49A8
		public string AssistantName { get; set; }

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06002069 RID: 8297 RVA: 0x000C67B1 File Offset: 0x000C49B1
		// (set) Token: 0x0600206A RID: 8298 RVA: 0x000C67B9 File Offset: 0x000C49B9
		public TimeSpan WorkcycleLength { get; set; }

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x0600206B RID: 8299 RVA: 0x000C67C2 File Offset: 0x000C49C2
		// (set) Token: 0x0600206C RID: 8300 RVA: 0x000C67CA File Offset: 0x000C49CA
		public TimeSpan WorkcycleCheckpointLength { get; set; }
	}
}
