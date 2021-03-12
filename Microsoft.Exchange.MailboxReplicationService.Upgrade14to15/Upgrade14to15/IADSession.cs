using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000002 RID: 2
	internal interface IADSession
	{
		// Token: 0x06000001 RID: 1
		IEnumerable<RecipientWrapper> FindPagedMiniRecipient(UpgradeBatchCreatorScheduler.MailboxType mailboxType, ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties);

		// Token: 0x06000002 RID: 2
		IEnumerable<RecipientWrapper> FindPilotUsersADRawEntry(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties);

		// Token: 0x06000003 RID: 3
		QueryFilter BuildE14MailboxQueryFilter();
	}
}
