using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000314 RID: 788
	public enum DeviceAccessLevel
	{
		// Token: 0x0400166F RID: 5743
		[LocDescription(DirectoryStrings.IDs.AccessGranted)]
		Allow,
		// Token: 0x04001670 RID: 5744
		[LocDescription(DirectoryStrings.IDs.AccessDenied)]
		Block,
		// Token: 0x04001671 RID: 5745
		[LocDescription(DirectoryStrings.IDs.AccessQuarantined)]
		Quarantine
	}
}
