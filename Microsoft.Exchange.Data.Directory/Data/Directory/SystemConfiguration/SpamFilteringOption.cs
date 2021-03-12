using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000470 RID: 1136
	public enum SpamFilteringOption
	{
		// Token: 0x040022B5 RID: 8885
		[LocDescription(DirectoryStrings.IDs.SpamFilteringOptionOff)]
		Off,
		// Token: 0x040022B6 RID: 8886
		[LocDescription(DirectoryStrings.IDs.SpamFilteringOptionOn)]
		On,
		// Token: 0x040022B7 RID: 8887
		[LocDescription(DirectoryStrings.IDs.SpamFilteringOptionTest)]
		Test
	}
}
