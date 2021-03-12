using System;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008B1 RID: 2225
	public enum MountStatus
	{
		// Token: 0x04002EC7 RID: 11975
		[LocDescription(Strings.IDs.MountStatusUnknown)]
		Unknown,
		// Token: 0x04002EC8 RID: 11976
		[LocDescription(Strings.IDs.MountStatusMounted)]
		Mounted,
		// Token: 0x04002EC9 RID: 11977
		[LocDescription(Strings.IDs.MountStatusDismounted)]
		Dismounted,
		// Token: 0x04002ECA RID: 11978
		[LocDescription(Strings.IDs.MountStatusMounting)]
		Mounting,
		// Token: 0x04002ECB RID: 11979
		[LocDescription(Strings.IDs.MountStatusDismounting)]
		Dismounting
	}
}
