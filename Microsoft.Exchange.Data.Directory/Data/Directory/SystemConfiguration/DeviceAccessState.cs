using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004ED RID: 1261
	public enum DeviceAccessState
	{
		// Token: 0x0400261B RID: 9755
		[LocDescription(DirectoryStrings.IDs.Unknown)]
		Unknown,
		// Token: 0x0400261C RID: 9756
		[LocDescription(DirectoryStrings.IDs.Allowed)]
		Allowed,
		// Token: 0x0400261D RID: 9757
		[LocDescription(DirectoryStrings.IDs.Blocked)]
		Blocked,
		// Token: 0x0400261E RID: 9758
		[LocDescription(DirectoryStrings.IDs.Quarantined)]
		Quarantined,
		// Token: 0x0400261F RID: 9759
		[LocDescription(DirectoryStrings.IDs.DeviceDiscovery)]
		DeviceDiscovery
	}
}
