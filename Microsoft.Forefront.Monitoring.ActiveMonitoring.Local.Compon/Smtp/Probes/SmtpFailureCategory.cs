using System;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000258 RID: 600
	public enum SmtpFailureCategory
	{
		// Token: 0x040009B9 RID: 2489
		TransportComponent,
		// Token: 0x040009BA RID: 2490
		MonitoringComponent,
		// Token: 0x040009BB RID: 2491
		DnsFailure,
		// Token: 0x040009BC RID: 2492
		DependentComponent,
		// Token: 0x040009BD RID: 2493
		DependentCoveredComponent
	}
}
