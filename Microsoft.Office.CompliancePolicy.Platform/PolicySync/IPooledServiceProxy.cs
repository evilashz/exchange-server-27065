using System;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x0200010E RID: 270
	internal interface IPooledServiceProxy<out TClient>
	{
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000755 RID: 1877
		TClient Client { get; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000756 RID: 1878
		// (set) Token: 0x06000757 RID: 1879
		string Tag { get; set; }
	}
}
