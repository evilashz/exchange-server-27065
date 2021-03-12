using System;

namespace Microsoft.Exchange.Management.Hybrid.Entity
{
	// Token: 0x020008EA RID: 2282
	internal class FederationInformation : IFederationInformation
	{
		// Token: 0x17001831 RID: 6193
		// (get) Token: 0x060050E9 RID: 20713 RVA: 0x00151A8D File Offset: 0x0014FC8D
		// (set) Token: 0x060050EA RID: 20714 RVA: 0x00151A95 File Offset: 0x0014FC95
		public string TargetAutodiscoverEpr { get; set; }

		// Token: 0x17001832 RID: 6194
		// (get) Token: 0x060050EB RID: 20715 RVA: 0x00151A9E File Offset: 0x0014FC9E
		// (set) Token: 0x060050EC RID: 20716 RVA: 0x00151AA6 File Offset: 0x0014FCA6
		public string TargetApplicationUri { get; set; }
	}
}
