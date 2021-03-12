using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x020008F5 RID: 2293
	internal interface IOrganizationConfig
	{
		// Token: 0x17001858 RID: 6232
		// (get) Token: 0x06005147 RID: 20807
		string Name { get; }

		// Token: 0x17001859 RID: 6233
		// (get) Token: 0x06005148 RID: 20808
		Guid Guid { get; }

		// Token: 0x1700185A RID: 6234
		// (get) Token: 0x06005149 RID: 20809
		ExchangeObjectVersion AdminDisplayVersion { get; }

		// Token: 0x1700185B RID: 6235
		// (get) Token: 0x0600514A RID: 20810
		bool IsUpgradingOrganization { get; }

		// Token: 0x1700185C RID: 6236
		// (get) Token: 0x0600514B RID: 20811
		string OrganizationConfigHash { get; }

		// Token: 0x1700185D RID: 6237
		// (get) Token: 0x0600514C RID: 20812
		bool IsDehydrated { get; }
	}
}
