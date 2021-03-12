using System;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x020008E1 RID: 2273
	internal interface IAcceptedDomain
	{
		// Token: 0x17001812 RID: 6162
		// (get) Token: 0x0600509E RID: 20638
		string DomainNameDomain { get; }

		// Token: 0x17001813 RID: 6163
		// (get) Token: 0x0600509F RID: 20639
		bool IsCoexistenceDomain { get; }
	}
}
