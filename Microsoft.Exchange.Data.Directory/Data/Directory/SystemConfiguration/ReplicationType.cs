using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003D1 RID: 977
	public enum ReplicationType
	{
		// Token: 0x04001E14 RID: 7700
		[LocDescription(DirectoryStrings.IDs.ReplicationTypeNone)]
		None,
		// Token: 0x04001E15 RID: 7701
		[LocDescription(DirectoryStrings.IDs.ReplicationTypeRemote)]
		Remote = 2,
		// Token: 0x04001E16 RID: 7702
		[LocDescription(DirectoryStrings.IDs.ReplicationTypeUnknown)]
		Unknown
	}
}
