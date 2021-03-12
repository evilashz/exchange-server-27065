using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000F63 RID: 3939
	internal interface IOutlookServiceStorage : IDisposable
	{
		// Token: 0x170016A1 RID: 5793
		// (get) Token: 0x060063B0 RID: 25520
		string TenantId { get; }

		// Token: 0x060063B1 RID: 25521
		IOutlookServiceSubscriptionStorage GetOutlookServiceSubscriptionStorage();
	}
}
