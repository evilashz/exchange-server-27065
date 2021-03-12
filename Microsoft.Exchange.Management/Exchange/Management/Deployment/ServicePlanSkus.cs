using System;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000239 RID: 569
	[Flags]
	public enum ServicePlanSkus : byte
	{
		// Token: 0x04000833 RID: 2099
		Datacenter = 1,
		// Token: 0x04000834 RID: 2100
		Hosted = 2,
		// Token: 0x04000835 RID: 2101
		All = 3
	}
}
