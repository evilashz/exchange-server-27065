using System;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000078 RID: 120
	public enum DeviceRemoteWipeStatus
	{
		// Token: 0x0400020D RID: 525
		[LocDescription(Strings.IDs.DeviceOk)]
		DeviceOk,
		// Token: 0x0400020E RID: 526
		[LocDescription(Strings.IDs.DeviceBlocked)]
		DeviceBlocked,
		// Token: 0x0400020F RID: 527
		[LocDescription(Strings.IDs.DeviceWipePending)]
		DeviceWipePending,
		// Token: 0x04000210 RID: 528
		[LocDescription(Strings.IDs.DeviceWipeSucceeded)]
		DeviceWipeSucceeded
	}
}
