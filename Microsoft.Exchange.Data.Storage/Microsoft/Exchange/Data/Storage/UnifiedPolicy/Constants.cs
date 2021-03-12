using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E7E RID: 3710
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class Constants
	{
		// Token: 0x040056CA RID: 22218
		public const string UnifiedPolicySyncSessionClientString = "Client=UnifiedPolicy;Action=CommitChanges;Interactive=False";

		// Token: 0x040056CB RID: 22219
		public const string TenantInfoMetadataName = "TenantInfoConfigurations";
	}
}
