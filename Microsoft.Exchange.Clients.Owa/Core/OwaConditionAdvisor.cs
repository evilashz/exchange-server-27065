using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200017B RID: 379
	internal sealed class OwaConditionAdvisor : DisposeTrackableBase
	{
		// Token: 0x06000D6C RID: 3436 RVA: 0x00059754 File Offset: 0x00057954
		internal OwaConditionAdvisor(MailboxSession mailboxSession, OwaStoreObjectId folderId, EventObjectType objectType, EventType eventType)
		{
			this.eventCondition = new EventCondition();
			this.folderId = folderId;
			this.eventCondition.ObjectType = objectType;
			this.eventCondition.EventType = eventType;
			this.eventCondition.ContainerFolderIds.Add(folderId.StoreObjectId);
			this.conditionAdvisor = ConditionAdvisor.Create(mailboxSession, this.eventCondition);
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x000597BA File Offset: 0x000579BA
		public OwaStoreObjectId FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x000597C2 File Offset: 0x000579C2
		// (set) Token: 0x06000D6F RID: 3439 RVA: 0x000597CA File Offset: 0x000579CA
		public bool IsRecycled
		{
			get
			{
				return this.isRecycled;
			}
			set
			{
				this.isRecycled = value;
			}
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x000597D4 File Offset: 0x000579D4
		public bool IsConditionTrue(MailboxSession mailboxSession)
		{
			bool result = false;
			try
			{
				result = this.conditionAdvisor.IsConditionTrue;
			}
			catch (StorageTransientException)
			{
				this.AttemptToRestoreConditionAdvisor(mailboxSession);
			}
			catch (StoragePermanentException)
			{
				this.AttemptToRestoreConditionAdvisor(mailboxSession);
			}
			return result;
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00059824 File Offset: 0x00057A24
		public void ResetCondition(MailboxSession mailboxSession)
		{
			try
			{
				this.conditionAdvisor.ResetCondition();
			}
			catch (StorageTransientException)
			{
				this.AttemptToRestoreConditionAdvisor(mailboxSession);
			}
			catch (StoragePermanentException)
			{
				this.AttemptToRestoreConditionAdvisor(mailboxSession);
			}
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00059870 File Offset: 0x00057A70
		private void AttemptToRestoreConditionAdvisor(MailboxSession mailboxSession)
		{
			ConditionAdvisor conditionAdvisor = ConditionAdvisor.Create(mailboxSession, this.eventCondition);
			this.conditionAdvisor.Dispose();
			this.conditionAdvisor = conditionAdvisor;
			this.isRecycled = true;
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x000598A4 File Offset: 0x00057AA4
		protected override void InternalDispose(bool isDisposing)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<bool>((long)this.GetHashCode(), "OwaConditionAdvisor.InternalDispose. IsDisposing: {0}", isDisposing);
			if (this.isDisposed)
			{
				return;
			}
			if (isDisposing)
			{
				if (this.conditionAdvisor != null)
				{
					this.conditionAdvisor.Dispose();
					this.conditionAdvisor = null;
				}
				this.isDisposed = true;
			}
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x000598F5 File Offset: 0x00057AF5
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OwaConditionAdvisor>(this);
		}

		// Token: 0x04000930 RID: 2352
		private ConditionAdvisor conditionAdvisor;

		// Token: 0x04000931 RID: 2353
		private EventCondition eventCondition;

		// Token: 0x04000932 RID: 2354
		private OwaStoreObjectId folderId;

		// Token: 0x04000933 RID: 2355
		private bool isRecycled;

		// Token: 0x04000934 RID: 2356
		private bool isDisposed;
	}
}
