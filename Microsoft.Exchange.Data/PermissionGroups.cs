using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000A1 RID: 161
	[Flags]
	public enum PermissionGroups
	{
		// Token: 0x04000250 RID: 592
		[LocDescription(DataStrings.IDs.PermissionGroupsNone)]
		None = 0,
		// Token: 0x04000251 RID: 593
		[LocDescription(DataStrings.IDs.AnonymousUsers)]
		AnonymousUsers = 1,
		// Token: 0x04000252 RID: 594
		[LocDescription(DataStrings.IDs.ExchangeUsers)]
		ExchangeUsers = 2,
		// Token: 0x04000253 RID: 595
		[LocDescription(DataStrings.IDs.ExchangeServers)]
		ExchangeServers = 4,
		// Token: 0x04000254 RID: 596
		[LocDescription(DataStrings.IDs.ExchangeLegacyServers)]
		ExchangeLegacyServers = 8,
		// Token: 0x04000255 RID: 597
		[LocDescription(DataStrings.IDs.Partners)]
		Partners = 16,
		// Token: 0x04000256 RID: 598
		[LocDescription(DataStrings.IDs.PermissionGroupsCustom)]
		Custom = 32
	}
}
