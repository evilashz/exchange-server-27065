using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002DE RID: 734
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractPushNotificationStorage : AbstractItem, IPushNotificationStorage, IDisposable
	{
		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06001F64 RID: 8036 RVA: 0x00086552 File Offset: 0x00084752
		public virtual string TenantId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x00086559 File Offset: 0x00084759
		public virtual List<PushNotificationServerSubscription> GetActiveNotificationSubscriptions(IMailboxSession mailboxSession, uint expirationInHours)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x00086560 File Offset: 0x00084760
		public virtual List<StoreObjectId> GetExpiredNotificationSubscriptions(uint expirationInHours)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x00086567 File Offset: 0x00084767
		public virtual List<PushNotificationServerSubscription> GetNotificationSubscriptions(IMailboxSession mailboxSession)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x0008656E File Offset: 0x0008476E
		public virtual IPushNotificationSubscriptionItem CreateOrUpdateSubscriptionItem(IMailboxSession mailboxSession, string subscriptionId, PushNotificationServerSubscription subscription)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x00086575 File Offset: 0x00084775
		public virtual void DeleteAllSubscriptions()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x0008657C File Offset: 0x0008477C
		public virtual void DeleteExpiredSubscriptions(uint expirationInHours)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x00086583 File Offset: 0x00084783
		public virtual void DeleteSubscription(StoreObjectId itemId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x0008658A File Offset: 0x0008478A
		public virtual void DeleteSubscription(string subscriptionId)
		{
			throw new NotImplementedException();
		}
	}
}
