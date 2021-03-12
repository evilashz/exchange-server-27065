using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000450 RID: 1104
	public enum RoleAssigneeType
	{
		// Token: 0x0400220A RID: 8714
		User,
		// Token: 0x0400220B RID: 8715
		SecurityGroup = 2,
		// Token: 0x0400220C RID: 8716
		RoleAssignmentPolicy = 4,
		// Token: 0x0400220D RID: 8717
		MailboxPlan = 6,
		// Token: 0x0400220E RID: 8718
		ForeignSecurityPrincipal = 8,
		// Token: 0x0400220F RID: 8719
		RoleGroup = 10,
		// Token: 0x04002210 RID: 8720
		PartnerLinkedRoleGroup,
		// Token: 0x04002211 RID: 8721
		LinkedRoleGroup,
		// Token: 0x04002212 RID: 8722
		Computer = 14
	}
}
