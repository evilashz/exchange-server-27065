using System;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000004 RID: 4
	internal interface ICacheEntry
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8
		string OrgName { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9
		string ExternalDirectoryOrganizationId { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10
		IADSession ADSessionProxy { get; }
	}
}
