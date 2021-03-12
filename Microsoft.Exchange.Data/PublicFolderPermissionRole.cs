using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000183 RID: 387
	public enum PublicFolderPermissionRole
	{
		// Token: 0x040007A4 RID: 1956
		[LocDescription(DataStrings.IDs.PublicFolderPermissionRoleOwner)]
		Owner = 2043,
		// Token: 0x040007A5 RID: 1957
		[LocDescription(DataStrings.IDs.PublicFolderPermissionRolePublishingEditor)]
		PublishingEditor = 1275,
		// Token: 0x040007A6 RID: 1958
		[LocDescription(DataStrings.IDs.PublicFolderPermissionRoleEditor)]
		Editor = 1147,
		// Token: 0x040007A7 RID: 1959
		[LocDescription(DataStrings.IDs.PublicFolderPermissionRolePublishingAuthor)]
		PublishingAuthor = 1179,
		// Token: 0x040007A8 RID: 1960
		[LocDescription(DataStrings.IDs.PublicFolderPermissionRoleAuthor)]
		Author = 1051,
		// Token: 0x040007A9 RID: 1961
		[LocDescription(DataStrings.IDs.PublicFolderPermissionRoleNonEditingAuthor)]
		NonEditingAuthor = 1043,
		// Token: 0x040007AA RID: 1962
		[LocDescription(DataStrings.IDs.PublicFolderPermissionRoleReviewer)]
		Reviewer = 1025,
		// Token: 0x040007AB RID: 1963
		[LocDescription(DataStrings.IDs.PublicFolderPermissionRoleContributor)]
		Contributor
	}
}
