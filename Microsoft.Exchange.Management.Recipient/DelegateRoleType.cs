using System;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200009D RID: 157
	public enum DelegateRoleType
	{
		// Token: 0x04000229 RID: 553
		[LocDescription(Strings.IDs.DelegateRoleTypeOrgAdmin)]
		OrgAdmin,
		// Token: 0x0400022A RID: 554
		[LocDescription(Strings.IDs.DelegateRoleTypeRecipientAdmin)]
		RecipientAdmin,
		// Token: 0x0400022B RID: 555
		[LocDescription(Strings.IDs.DelegateRoleTypeServerAdmin)]
		ServerAdmin,
		// Token: 0x0400022C RID: 556
		[LocDescription(Strings.IDs.DelegateRoleTypeViewOnlyAdmin)]
		ViewOnlyAdmin,
		// Token: 0x0400022D RID: 557
		[LocDescription(Strings.IDs.DelegateRoleTypePublicFolderAdmin)]
		PublicFolderAdmin
	}
}
