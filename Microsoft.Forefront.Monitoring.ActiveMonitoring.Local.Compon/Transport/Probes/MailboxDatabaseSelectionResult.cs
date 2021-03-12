using System;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Transport.Probes
{
	// Token: 0x02000274 RID: 628
	public enum MailboxDatabaseSelectionResult
	{
		// Token: 0x04000A00 RID: 2560
		Success,
		// Token: 0x04000A01 RID: 2561
		NoMonitoringMDBs,
		// Token: 0x04000A02 RID: 2562
		NoMonitoringMDBsAreActive,
		// Token: 0x04000A03 RID: 2563
		NoLocalEndpointManager
	}
}
