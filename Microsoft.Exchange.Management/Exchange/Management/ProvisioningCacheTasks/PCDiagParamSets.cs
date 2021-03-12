using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ProvisioningCacheTasks
{
	// Token: 0x02000642 RID: 1602
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PCDiagParamSets
	{
		// Token: 0x0600381F RID: 14367 RVA: 0x000E843D File Offset: 0x000E663D
		private PCDiagParamSets()
		{
		}

		// Token: 0x040025C0 RID: 9664
		public const string GlobalCache = "GlobalCache";

		// Token: 0x040025C1 RID: 9665
		public const string OrganizationCache = "OrganizationCache";
	}
}
