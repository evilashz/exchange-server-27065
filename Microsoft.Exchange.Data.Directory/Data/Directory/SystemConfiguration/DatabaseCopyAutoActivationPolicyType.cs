using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000589 RID: 1417
	public enum DatabaseCopyAutoActivationPolicyType
	{
		// Token: 0x04002CC8 RID: 11464
		[LocDescription(DirectoryStrings.IDs.DatabaseCopyAutoActivationPolicyUnrestricted)]
		Unrestricted,
		// Token: 0x04002CC9 RID: 11465
		[LocDescription(DirectoryStrings.IDs.DatabaseCopyAutoActivationPolicyIntrasiteOnly)]
		IntrasiteOnly,
		// Token: 0x04002CCA RID: 11466
		[LocDescription(DirectoryStrings.IDs.DatabaseCopyAutoActivationPolicyBlocked)]
		Blocked
	}
}
