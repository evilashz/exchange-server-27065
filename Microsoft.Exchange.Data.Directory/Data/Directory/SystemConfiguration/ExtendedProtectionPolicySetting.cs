using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000553 RID: 1363
	[Flags]
	public enum ExtendedProtectionPolicySetting
	{
		// Token: 0x04002970 RID: 10608
		[LocDescription(DirectoryStrings.IDs.ReceiveExtendedProtectionPolicyNone)]
		None = 0,
		// Token: 0x04002971 RID: 10609
		[LocDescription(DirectoryStrings.IDs.ReceiveExtendedProtectionPolicyAllow)]
		Allow = 1,
		// Token: 0x04002972 RID: 10610
		[LocDescription(DirectoryStrings.IDs.ReceiveExtendedProtectionPolicyRequire)]
		Require = 2
	}
}
