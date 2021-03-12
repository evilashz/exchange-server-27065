using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004F5 RID: 1269
	public enum EmailAgeFilterType
	{
		// Token: 0x0400267E RID: 9854
		[LocDescription(DirectoryStrings.IDs.EmailAgeFilterAll)]
		All,
		// Token: 0x0400267F RID: 9855
		[LocDescription(DirectoryStrings.IDs.EmailAgeFilterOneDay)]
		OneDay,
		// Token: 0x04002680 RID: 9856
		[LocDescription(DirectoryStrings.IDs.EmailAgeFilterThreeDays)]
		ThreeDays,
		// Token: 0x04002681 RID: 9857
		[LocDescription(DirectoryStrings.IDs.EmailAgeFilterOneWeek)]
		OneWeek,
		// Token: 0x04002682 RID: 9858
		[LocDescription(DirectoryStrings.IDs.EmailAgeFilterTwoWeeks)]
		TwoWeeks,
		// Token: 0x04002683 RID: 9859
		[LocDescription(DirectoryStrings.IDs.EmailAgeFilterOneMonth)]
		OneMonth
	}
}
