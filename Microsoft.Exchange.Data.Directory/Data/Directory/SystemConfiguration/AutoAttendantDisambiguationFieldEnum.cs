using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000603 RID: 1539
	public enum AutoAttendantDisambiguationFieldEnum
	{
		// Token: 0x040032A1 RID: 12961
		[LocDescription(DirectoryStrings.IDs.Title)]
		Title,
		// Token: 0x040032A2 RID: 12962
		[LocDescription(DirectoryStrings.IDs.Department)]
		Department,
		// Token: 0x040032A3 RID: 12963
		[LocDescription(DirectoryStrings.IDs.Location)]
		Location,
		// Token: 0x040032A4 RID: 12964
		[LocDescription(DirectoryStrings.IDs.None)]
		None,
		// Token: 0x040032A5 RID: 12965
		[LocDescription(DirectoryStrings.IDs.PromptForAlias)]
		PromptForAlias,
		// Token: 0x040032A6 RID: 12966
		[LocDescription(DirectoryStrings.IDs.InheritFromDialPlan)]
		InheritFromDialPlan
	}
}
