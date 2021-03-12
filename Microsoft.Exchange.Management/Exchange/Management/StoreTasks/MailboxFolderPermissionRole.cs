using System;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007AF RID: 1967
	public enum MailboxFolderPermissionRole
	{
		// Token: 0x04002AC2 RID: 10946
		[LocDescription(Strings.IDs.RoleOwner)]
		Owner = 2043,
		// Token: 0x04002AC3 RID: 10947
		[LocDescription(Strings.IDs.RolePublishingEditor)]
		PublishingEditor = 1275,
		// Token: 0x04002AC4 RID: 10948
		[LocDescription(Strings.IDs.RoleEditor)]
		Editor = 1147,
		// Token: 0x04002AC5 RID: 10949
		[LocDescription(Strings.IDs.RolePublishingAuthor)]
		PublishingAuthor = 1179,
		// Token: 0x04002AC6 RID: 10950
		[LocDescription(Strings.IDs.RoleAuthor)]
		Author = 1051,
		// Token: 0x04002AC7 RID: 10951
		[LocDescription(Strings.IDs.RoleNonEditingAuthor)]
		NonEditingAuthor = 1043,
		// Token: 0x04002AC8 RID: 10952
		[LocDescription(Strings.IDs.RoleReviewer)]
		Reviewer = 1025,
		// Token: 0x04002AC9 RID: 10953
		[LocDescription(Strings.IDs.RoleContributor)]
		Contributor,
		// Token: 0x04002ACA RID: 10954
		[LocDescription(Strings.IDs.RoleAvailabilityOnly)]
		AvailabilityOnly = 2048,
		// Token: 0x04002ACB RID: 10955
		[LocDescription(Strings.IDs.RoleLimitedDetails)]
		LimitedDetails = 6144
	}
}
