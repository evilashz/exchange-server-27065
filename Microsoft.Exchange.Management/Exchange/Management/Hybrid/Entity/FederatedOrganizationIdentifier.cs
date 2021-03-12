using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.Hybrid.Entity
{
	// Token: 0x020008E8 RID: 2280
	internal class FederatedOrganizationIdentifier : IFederatedOrganizationIdentifier
	{
		// Token: 0x1700182A RID: 6186
		// (get) Token: 0x060050DC RID: 20700 RVA: 0x00151A30 File Offset: 0x0014FC30
		// (set) Token: 0x060050DD RID: 20701 RVA: 0x00151A38 File Offset: 0x0014FC38
		public bool Enabled { get; set; }

		// Token: 0x1700182B RID: 6187
		// (get) Token: 0x060050DE RID: 20702 RVA: 0x00151A41 File Offset: 0x0014FC41
		// (set) Token: 0x060050DF RID: 20703 RVA: 0x00151A49 File Offset: 0x0014FC49
		public SmtpDomain AccountNamespace { get; set; }

		// Token: 0x1700182C RID: 6188
		// (get) Token: 0x060050E0 RID: 20704 RVA: 0x00151A52 File Offset: 0x0014FC52
		// (set) Token: 0x060050E1 RID: 20705 RVA: 0x00151A5A File Offset: 0x0014FC5A
		public ADObjectId DelegationTrustLink { get; set; }

		// Token: 0x1700182D RID: 6189
		// (get) Token: 0x060050E2 RID: 20706 RVA: 0x00151A63 File Offset: 0x0014FC63
		// (set) Token: 0x060050E3 RID: 20707 RVA: 0x00151A6B File Offset: 0x0014FC6B
		public MultiValuedProperty<FederatedDomain> Domains { get; set; }

		// Token: 0x1700182E RID: 6190
		// (get) Token: 0x060050E4 RID: 20708 RVA: 0x00151A74 File Offset: 0x0014FC74
		// (set) Token: 0x060050E5 RID: 20709 RVA: 0x00151A7C File Offset: 0x0014FC7C
		public SmtpDomain DefaultDomain { get; set; }
	}
}
