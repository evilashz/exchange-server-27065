using System;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x0200000E RID: 14
	internal interface IWritableAD
	{
		// Token: 0x0600006C RID: 108
		bool SetDatabaseLegacyDnAndOwningServer(Guid mdbGuid, AmServerName lastMountedServerName, AmServerName masterServerName, bool isForceUpdate);

		// Token: 0x0600006D RID: 109
		void ResetAllowFileRestoreDsFlag(Guid mdbGuid, AmServerName lastMountedServerName, AmServerName masterServerName);
	}
}
