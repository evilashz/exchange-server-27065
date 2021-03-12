using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001DF RID: 479
	internal sealed class OwaLastEventAdvisor : DisposeTrackableBase
	{
		// Token: 0x06000F5D RID: 3933 RVA: 0x0005F5A4 File Offset: 0x0005D7A4
		internal OwaLastEventAdvisor(UserContext userContext, StoreObjectId folderId, EventObjectType objectType, EventType eventType)
		{
			this.eventCondition = new EventCondition();
			this.folderId = folderId;
			this.eventCondition.EventType = eventType;
			this.eventCondition.ObjectType = objectType;
			this.eventCondition.ContainerFolderIds.Add(folderId);
			this.lastEventAdvisor = LastEventAdvisor.Create(userContext.MailboxSession, this.eventCondition);
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x0005F60A File Offset: 0x0005D80A
		public StoreObjectId FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0005F614 File Offset: 0x0005D814
		public Event GetLastEvent(UserContext userContext)
		{
			Event result = null;
			try
			{
				result = this.lastEventAdvisor.GetLastEvent();
			}
			catch (StoragePermanentException)
			{
				this.AttemptToRestoreLastEventAdvisor(userContext);
			}
			catch (StorageTransientException)
			{
				this.AttemptToRestoreLastEventAdvisor(userContext);
			}
			return result;
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0005F664 File Offset: 0x0005D864
		private void AttemptToRestoreLastEventAdvisor(UserContext userContext)
		{
			LastEventAdvisor lastEventAdvisor = LastEventAdvisor.Create(userContext.MailboxSession, this.eventCondition);
			this.lastEventAdvisor.Dispose();
			this.lastEventAdvisor = lastEventAdvisor;
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0005F698 File Offset: 0x0005D898
		public void Flush()
		{
			try
			{
				this.lastEventAdvisor.GetLastEvent();
			}
			catch (StoragePermanentException)
			{
			}
			catch (StorageTransientException)
			{
			}
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0005F6D8 File Offset: 0x0005D8D8
		protected override void InternalDispose(bool isDisposing)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<bool>((long)this.GetHashCode(), "OwaLastEventAdvisorCall.Dispose. IsDisposing: {0}", isDisposing);
			if (isDisposing && this.lastEventAdvisor != null)
			{
				this.lastEventAdvisor.Dispose();
				this.lastEventAdvisor = null;
			}
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0005F70E File Offset: 0x0005D90E
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OwaLastEventAdvisor>(this);
		}

		// Token: 0x04000A5A RID: 2650
		private LastEventAdvisor lastEventAdvisor;

		// Token: 0x04000A5B RID: 2651
		private EventCondition eventCondition;

		// Token: 0x04000A5C RID: 2652
		private StoreObjectId folderId;
	}
}
