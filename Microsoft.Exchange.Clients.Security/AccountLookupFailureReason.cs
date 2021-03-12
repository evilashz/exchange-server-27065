using System;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x02000023 RID: 35
	internal enum AccountLookupFailureReason
	{
		// Token: 0x0400007E RID: 126
		ADAccountNotFound,
		// Token: 0x0400007F RID: 127
		AccountDisabled,
		// Token: 0x04000080 RID: 128
		SharedMailboxAccountDisabled,
		// Token: 0x04000081 RID: 129
		NonUniqueRecipientException,
		// Token: 0x04000082 RID: 130
		CannotResolvePartitionException,
		// Token: 0x04000083 RID: 131
		CannotResolveTenantNameException,
		// Token: 0x04000084 RID: 132
		DataSourceOperationException,
		// Token: 0x04000085 RID: 133
		EmptySid,
		// Token: 0x04000086 RID: 134
		OrganizationNotFound,
		// Token: 0x04000087 RID: 135
		MailboxRecentlyCreated,
		// Token: 0x04000088 RID: 136
		MailboxSoftDeleted
	}
}
