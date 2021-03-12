using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200010A RID: 266
	[Flags]
	public enum MobileDeviceProvisioningMasks
	{
		// Token: 0x040005B4 RID: 1460
		None = 0,
		// Token: 0x040005B5 RID: 1461
		IsDisabledMask = 1,
		// Token: 0x040005B6 RID: 1462
		IsManagedMask = 2,
		// Token: 0x040005B7 RID: 1463
		IsCompliantMask = 4
	}
}
