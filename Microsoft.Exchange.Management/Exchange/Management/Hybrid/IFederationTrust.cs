using System;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x020008EB RID: 2283
	internal interface IFederationTrust
	{
		// Token: 0x17001833 RID: 6195
		// (get) Token: 0x060050EE RID: 20718
		string Name { get; }

		// Token: 0x17001834 RID: 6196
		// (get) Token: 0x060050EF RID: 20719
		Uri TokenIssuerUri { get; }
	}
}
