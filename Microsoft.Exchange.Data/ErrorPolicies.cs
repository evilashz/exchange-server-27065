using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000AD RID: 173
	[Flags]
	public enum ErrorPolicies
	{
		// Token: 0x040002A3 RID: 675
		[LocDescription(DataStrings.IDs.ErrorPoliciesDefault)]
		Default = 0,
		// Token: 0x040002A4 RID: 676
		[LocDescription(DataStrings.IDs.ErrorPoliciesDowngradeDnsFailures)]
		DowngradeDnsFailures = 4,
		// Token: 0x040002A5 RID: 677
		[LocDescription(DataStrings.IDs.ErrorPoliciesDowngradeCustomFailures)]
		DowngradeCustomFailures = 8,
		// Token: 0x040002A6 RID: 678
		[LocDescription(DataStrings.IDs.ErrorPoliciesUpgradeCustomFailures)]
		UpgradeCustomFailures = 16
	}
}
