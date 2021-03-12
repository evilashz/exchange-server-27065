using System;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x020008E9 RID: 2281
	internal interface IFederationInformation
	{
		// Token: 0x1700182F RID: 6191
		// (get) Token: 0x060050E7 RID: 20711
		string TargetAutodiscoverEpr { get; }

		// Token: 0x17001830 RID: 6192
		// (get) Token: 0x060050E8 RID: 20712
		string TargetApplicationUri { get; }
	}
}
