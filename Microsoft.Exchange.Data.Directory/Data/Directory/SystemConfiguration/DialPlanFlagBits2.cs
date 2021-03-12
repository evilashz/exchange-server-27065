using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000608 RID: 1544
	[Flags]
	internal enum DialPlanFlagBits2
	{
		// Token: 0x040032B9 RID: 12985
		[LocDescription(DirectoryStrings.IDs.None)]
		None = 0,
		// Token: 0x040032BA RID: 12986
		[LocDescription(DirectoryStrings.IDs.PAAEnabled)]
		PAAEnabled = 2,
		// Token: 0x040032BB RID: 12987
		[LocDescription(DirectoryStrings.IDs.SipResourceIdRequired)]
		SipResourceIdRequired = 4,
		// Token: 0x040032BC RID: 12988
		All = -1
	}
}
