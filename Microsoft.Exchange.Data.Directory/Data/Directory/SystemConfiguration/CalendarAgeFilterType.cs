using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004F4 RID: 1268
	public enum CalendarAgeFilterType
	{
		// Token: 0x04002678 RID: 9848
		[LocDescription(DirectoryStrings.IDs.CalendarAgeFilterAll)]
		All,
		// Token: 0x04002679 RID: 9849
		[LocDescription(DirectoryStrings.IDs.CalendarAgeFilterTwoWeeks)]
		TwoWeeks = 4,
		// Token: 0x0400267A RID: 9850
		[LocDescription(DirectoryStrings.IDs.CalendarAgeFilterOneMonth)]
		OneMonth,
		// Token: 0x0400267B RID: 9851
		[LocDescription(DirectoryStrings.IDs.CalendarAgeFilterThreeMonths)]
		ThreeMonths,
		// Token: 0x0400267C RID: 9852
		[LocDescription(DirectoryStrings.IDs.CalendarAgeFilterSixMonths)]
		SixMonths
	}
}
