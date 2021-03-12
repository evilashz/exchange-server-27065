using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001DB RID: 475
	internal sealed class OwaFolderCountAdvisor : DisposeTrackableBase
	{
		// Token: 0x06000F52 RID: 3922 RVA: 0x0005F39C File Offset: 0x0005D59C
		internal OwaFolderCountAdvisor(MailboxSession mailboxSession, OwaStoreObjectId folderId, EventObjectType objectType, EventType eventType)
		{
			this.folderId = folderId;
			this.mailboxOwner = mailboxSession.MailboxOwner;
			this.eventCondition = new EventCondition();
			this.eventCondition.EventType = eventType;
			this.eventCondition.ObjectType = objectType;
			if (folderId != null)
			{
				this.eventCondition.ContainerFolderIds.Add(folderId.StoreObjectId);
				if (mailboxSession.LogonType == LogonType.Delegated)
				{
					this.eventCondition.ObjectIds.Add(folderId.StoreObjectId);
				}
			}
			this.countAdvisor = ItemCountAdvisor.Create(mailboxSession, this.eventCondition);
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x0005F430 File Offset: 0x0005D630
		public OwaStoreObjectId FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x0005F438 File Offset: 0x0005D638
		public IExchangePrincipal MailboxOwner
		{
			get
			{
				return this.mailboxOwner;
			}
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0005F440 File Offset: 0x0005D640
		public Dictionary<StoreObjectId, ItemCountPair> GetItemCounts(MailboxSession mailboxSession)
		{
			Dictionary<StoreObjectId, ItemCountPair> result = null;
			try
			{
				result = this.countAdvisor.GetItemCounts();
			}
			catch (StorageTransientException)
			{
				this.AttemptToRestoreFolderCountAdvisor(mailboxSession);
			}
			catch (StoragePermanentException)
			{
				this.AttemptToRestoreFolderCountAdvisor(mailboxSession);
			}
			return result;
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0005F490 File Offset: 0x0005D690
		private void AttemptToRestoreFolderCountAdvisor(MailboxSession mailboxSession)
		{
			ItemCountAdvisor itemCountAdvisor = ItemCountAdvisor.Create(mailboxSession, this.eventCondition);
			this.countAdvisor.Dispose();
			this.countAdvisor = itemCountAdvisor;
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0005F4BC File Offset: 0x0005D6BC
		protected override void InternalDispose(bool isDisposing)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<bool>((long)this.GetHashCode(), "OwaFolderCountAdvisor.Dispose. IsDisposing: {0}", isDisposing);
			if (isDisposing && this.countAdvisor != null)
			{
				this.countAdvisor.Dispose();
				this.countAdvisor = null;
			}
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0005F4F2 File Offset: 0x0005D6F2
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OwaFolderCountAdvisor>(this);
		}

		// Token: 0x04000A3D RID: 2621
		private ItemCountAdvisor countAdvisor;

		// Token: 0x04000A3E RID: 2622
		private EventCondition eventCondition;

		// Token: 0x04000A3F RID: 2623
		private OwaStoreObjectId folderId;

		// Token: 0x04000A40 RID: 2624
		private IExchangePrincipal mailboxOwner;
	}
}
