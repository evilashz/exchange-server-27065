using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.RpcProxy
{
	// Token: 0x02000002 RID: 2
	public static class Globals
	{
		// Token: 0x04000001 RID: 1
		public const byte MaxDatabasesMountedStandard = 5;

		// Token: 0x04000002 RID: 2
		public const byte MaxRecoveryDatabasesMounted = 1;

		// Token: 0x04000003 RID: 3
		public const byte MaxRecoveryDatabasesMountedStandardSKU = 1;

		// Token: 0x04000004 RID: 4
		public static readonly byte MaxDatabasesMountedEnterprise = DefaultSettings.Get.MaximumMountedDatabases;
	}
}
