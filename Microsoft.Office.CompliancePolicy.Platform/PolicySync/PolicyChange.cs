using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000F9 RID: 249
	public sealed class PolicyChange
	{
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00014959 File Offset: 0x00012B59
		// (set) Token: 0x060006AC RID: 1708 RVA: 0x00014961 File Offset: 0x00012B61
		public IEnumerable<PolicyConfigurationBase> Changes { get; set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0001496A File Offset: 0x00012B6A
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x00014972 File Offset: 0x00012B72
		public TenantCookie NewCookie { get; set; }
	}
}
