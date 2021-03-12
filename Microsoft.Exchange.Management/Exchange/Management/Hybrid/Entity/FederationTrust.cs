using System;

namespace Microsoft.Exchange.Management.Hybrid.Entity
{
	// Token: 0x020008EC RID: 2284
	internal class FederationTrust : IFederationTrust
	{
		// Token: 0x17001835 RID: 6197
		// (get) Token: 0x060050F0 RID: 20720 RVA: 0x00151AB7 File Offset: 0x0014FCB7
		// (set) Token: 0x060050F1 RID: 20721 RVA: 0x00151ABF File Offset: 0x0014FCBF
		public string Name { get; set; }

		// Token: 0x17001836 RID: 6198
		// (get) Token: 0x060050F2 RID: 20722 RVA: 0x00151AC8 File Offset: 0x0014FCC8
		// (set) Token: 0x060050F3 RID: 20723 RVA: 0x00151AD0 File Offset: 0x0014FCD0
		public Uri TokenIssuerUri { get; set; }
	}
}
