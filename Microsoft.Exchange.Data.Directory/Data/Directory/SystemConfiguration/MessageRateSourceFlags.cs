using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000552 RID: 1362
	[Flags]
	public enum MessageRateSourceFlags
	{
		// Token: 0x0400296B RID: 10603
		[LocDescription(DirectoryStrings.IDs.MessageRateSourceFlagsNone)]
		None = 0,
		// Token: 0x0400296C RID: 10604
		[LocDescription(DirectoryStrings.IDs.MessageRateSourceFlagsIPAddress)]
		IPAddress = 1,
		// Token: 0x0400296D RID: 10605
		[LocDescription(DirectoryStrings.IDs.MessageRateSourceFlagsUser)]
		User = 2,
		// Token: 0x0400296E RID: 10606
		[LocDescription(DirectoryStrings.IDs.MessageRateSourceFlagsAll)]
		All = 3
	}
}
