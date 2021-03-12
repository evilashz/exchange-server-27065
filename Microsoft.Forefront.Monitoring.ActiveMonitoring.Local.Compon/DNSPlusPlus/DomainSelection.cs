using System;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.DNSPlusPlus
{
	// Token: 0x02000082 RID: 130
	internal enum DomainSelection
	{
		// Token: 0x040001F0 RID: 496
		AnyTargetServiceAnyZone,
		// Token: 0x040001F1 RID: 497
		AllTargetServicesAnyZone,
		// Token: 0x040001F2 RID: 498
		AnyTargetServiceAllZones,
		// Token: 0x040001F3 RID: 499
		Ipv6AnyTargetServiceAnyZone,
		// Token: 0x040001F4 RID: 500
		Ipv6AllTargetServicesAnyZone,
		// Token: 0x040001F5 RID: 501
		Ipv6AnyTargetServiceAllZones,
		// Token: 0x040001F6 RID: 502
		AnyZone,
		// Token: 0x040001F7 RID: 503
		AllZones,
		// Token: 0x040001F8 RID: 504
		InvalidDomainWithFallback,
		// Token: 0x040001F9 RID: 505
		InvalidDomainWithoutFallback,
		// Token: 0x040001FA RID: 506
		InvalidZone,
		// Token: 0x040001FB RID: 507
		CustomQuery
	}
}
