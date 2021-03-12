using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000019 RID: 25
	[Flags]
	public enum FolderRecDataFlags
	{
		// Token: 0x0400005C RID: 92
		None = 0,
		// Token: 0x0400005D RID: 93
		PromotedProperties = 1,
		// Token: 0x0400005E RID: 94
		SecurityDescriptors = 2,
		// Token: 0x0400005F RID: 95
		Rules = 4,
		// Token: 0x04000060 RID: 96
		SearchCriteria = 8,
		// Token: 0x04000061 RID: 97
		FolderAcls = 16,
		// Token: 0x04000062 RID: 98
		Views = 32,
		// Token: 0x04000063 RID: 99
		Restrictions = 64,
		// Token: 0x04000064 RID: 100
		ExtendedAclInformation = 128,
		// Token: 0x04000065 RID: 101
		ExtendedData = 97
	}
}
