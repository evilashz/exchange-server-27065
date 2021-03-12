using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02000906 RID: 2310
	internal interface ITaskContext
	{
		// Token: 0x170018A0 RID: 6304
		// (get) Token: 0x060051F6 RID: 20982
		HybridConfiguration HybridConfigurationObject { get; }

		// Token: 0x170018A1 RID: 6305
		// (get) Token: 0x060051F7 RID: 20983
		IContextParameters Parameters { get; }

		// Token: 0x170018A2 RID: 6306
		// (get) Token: 0x060051F8 RID: 20984
		ILogger Logger { get; }

		// Token: 0x170018A3 RID: 6307
		// (get) Token: 0x060051F9 RID: 20985
		IOnPremisesSession OnPremisesSession { get; }

		// Token: 0x170018A4 RID: 6308
		// (get) Token: 0x060051FA RID: 20986
		ITenantSession TenantSession { get; }

		// Token: 0x170018A5 RID: 6309
		// (get) Token: 0x060051FB RID: 20987
		IList<LocalizedString> Errors { get; }

		// Token: 0x170018A6 RID: 6310
		// (get) Token: 0x060051FC RID: 20988
		IList<LocalizedString> Warnings { get; }

		// Token: 0x170018A7 RID: 6311
		// (get) Token: 0x060051FD RID: 20989
		IUserInterface UI { get; }
	}
}
