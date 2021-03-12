using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E8D RID: 3725
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class PolicyDistributionErrorDetail
	{
		// Token: 0x17002265 RID: 8805
		// (get) Token: 0x060081A3 RID: 33187 RVA: 0x00236F57 File Offset: 0x00235157
		// (set) Token: 0x060081A4 RID: 33188 RVA: 0x00236F5F File Offset: 0x0023515F
		public Workload Workload { get; set; }

		// Token: 0x17002266 RID: 8806
		// (get) Token: 0x060081A5 RID: 33189 RVA: 0x00236F68 File Offset: 0x00235168
		// (set) Token: 0x060081A6 RID: 33190 RVA: 0x00236F70 File Offset: 0x00235170
		public string Endpoint { get; set; }

		// Token: 0x17002267 RID: 8807
		// (get) Token: 0x060081A7 RID: 33191 RVA: 0x00236F79 File Offset: 0x00235179
		// (set) Token: 0x060081A8 RID: 33192 RVA: 0x00236F81 File Offset: 0x00235181
		public UnifiedPolicyErrorCode ErrorCode { get; set; }

		// Token: 0x17002268 RID: 8808
		// (get) Token: 0x060081A9 RID: 33193 RVA: 0x00236F8A File Offset: 0x0023518A
		// (set) Token: 0x060081AA RID: 33194 RVA: 0x00236F92 File Offset: 0x00235192
		public string ErrorMessage { get; set; }

		// Token: 0x17002269 RID: 8809
		// (get) Token: 0x060081AB RID: 33195 RVA: 0x00236F9B File Offset: 0x0023519B
		// (set) Token: 0x060081AC RID: 33196 RVA: 0x00236FA3 File Offset: 0x002351A3
		public DateTime? ErrorTimeUTC { get; set; }
	}
}
