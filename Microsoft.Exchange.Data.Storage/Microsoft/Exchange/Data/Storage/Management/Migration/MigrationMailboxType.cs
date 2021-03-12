using System;

namespace Microsoft.Exchange.Data.Storage.Management.Migration
{
	// Token: 0x02000A2D RID: 2605
	[Serializable]
	internal enum MigrationMailboxType
	{
		// Token: 0x04003631 RID: 13873
		PrimaryAndArchive,
		// Token: 0x04003632 RID: 13874
		PrimaryOnly,
		// Token: 0x04003633 RID: 13875
		ArchiveOnly
	}
}
