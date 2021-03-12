using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x02000721 RID: 1825
	[ClassAccessLevel(AccessLevel.Implementation)]
	public interface ITpdImporter
	{
		// Token: 0x060040CB RID: 16587
		TrustedDocDomain Import(Guid tenantId);
	}
}
