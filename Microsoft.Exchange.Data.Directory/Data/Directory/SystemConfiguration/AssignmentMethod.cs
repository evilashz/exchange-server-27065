using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000451 RID: 1105
	[Flags]
	public enum AssignmentMethod
	{
		// Token: 0x04002214 RID: 8724
		None = 0,
		// Token: 0x04002215 RID: 8725
		Direct = 1,
		// Token: 0x04002216 RID: 8726
		SecurityGroup = 2,
		// Token: 0x04002217 RID: 8727
		RoleAssignmentPolicy = 4,
		// Token: 0x04002218 RID: 8728
		MailboxPlan = 8,
		// Token: 0x04002219 RID: 8729
		RoleGroup = 16,
		// Token: 0x0400221A RID: 8730
		ExtraDefaultSids = 32,
		// Token: 0x0400221B RID: 8731
		S4U = 50,
		// Token: 0x0400221C RID: 8732
		All = 63
	}
}
