using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002C3 RID: 707
	[Flags]
	public enum HeuristicsFlags
	{
		// Token: 0x0400138C RID: 5004
		None = 0,
		// Token: 0x0400138D RID: 5005
		SuspendFolderReplication = 1024,
		// Token: 0x0400138E RID: 5006
		PublicFoldersLockedForMigration = 2048,
		// Token: 0x0400138F RID: 5007
		PublicFolderMigrationComplete = 4096,
		// Token: 0x04001390 RID: 5008
		PublicFoldersDisabled = 8192,
		// Token: 0x04001391 RID: 5009
		RemotePublicFolders = 16384,
		// Token: 0x04001392 RID: 5010
		PublicFolderMailboxesLockedForNewConnections = 32768,
		// Token: 0x04001393 RID: 5011
		PublicFolderMailboxesMigrationComplete = 65536
	}
}
