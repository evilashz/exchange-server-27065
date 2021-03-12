using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Transport.Sync.Worker.Agents
{
	// Token: 0x02000020 RID: 32
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SubscriptionWorkItemFailedLoadSubscriptionEventArgs : SubscriptionWorkItemEventArgs<SubscriptionWorkItemFailedLoadSubscriptionEventResult>
	{
		// Token: 0x060001ED RID: 493 RVA: 0x00008F3C File Offset: 0x0000713C
		public SubscriptionWorkItemFailedLoadSubscriptionEventArgs(SyncLogSession syncLogSession, Guid subscriptionId, Exception workItemResultException, StoreObjectId subscriptionMessageId, Guid mailboxGuid, string userLegacyDn, Guid tenantGuid, OrganizationId organizationId) : base(new SubscriptionWorkItemFailedLoadSubscriptionEventResult(), syncLogSession, subscriptionId, workItemResultException, subscriptionMessageId, mailboxGuid, userLegacyDn, tenantGuid, organizationId)
		{
		}
	}
}
