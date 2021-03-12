using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000235 RID: 565
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class BaseWatermark
	{
		// Token: 0x060014C3 RID: 5315 RVA: 0x0004AE84 File Offset: 0x00049084
		protected BaseWatermark(SyncLogSession syncLogSession, string mailboxServerSyncWatermark, ISimpleStateStorage stateStorage, bool loadedFromMailboxServer)
		{
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			if (loadedFromMailboxServer)
			{
				SyncUtilities.ThrowIfArgumentNull("mailboxServerSyncWatermark", mailboxServerSyncWatermark);
			}
			else
			{
				SyncUtilities.ThrowIfArgumentNull("stateStorage", stateStorage);
			}
			this.mailboxServerSyncWatermark = mailboxServerSyncWatermark;
			this.stateStorage = stateStorage;
			this.syncLogSession = syncLogSession;
			this.loadedFromMailboxServer = loadedFromMailboxServer;
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x0004AEDB File Offset: 0x000490DB
		protected ISimpleStateStorage StateStorage
		{
			get
			{
				return this.stateStorage;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x0004AEE3 File Offset: 0x000490E3
		// (set) Token: 0x060014C6 RID: 5318 RVA: 0x0004AEEB File Offset: 0x000490EB
		protected string MailboxServerSyncWatermark
		{
			get
			{
				return this.mailboxServerSyncWatermark;
			}
			set
			{
				this.mailboxServerSyncWatermark = value;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x0004AEF4 File Offset: 0x000490F4
		protected SyncLogSession SyncLogSession
		{
			get
			{
				return this.syncLogSession;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x0004AEFC File Offset: 0x000490FC
		protected bool LoadedFromMailboxServer
		{
			get
			{
				return this.loadedFromMailboxServer;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x0004AF04 File Offset: 0x00049104
		// (set) Token: 0x060014CA RID: 5322 RVA: 0x0004AF0C File Offset: 0x0004910C
		protected string StateStorageEncodedSyncWatermark
		{
			get
			{
				return this.stateStorageEncodedSyncWatermark;
			}
			set
			{
				SyncUtilities.ThrowIfArgumentNull("StateStorageEncodedSyncWatermark", value);
				this.isWatermarkUpdated = true;
				this.stateStorageEncodedSyncWatermark = value;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x0004AF27 File Offset: 0x00049127
		public bool IsSyncWatermarkUpdated
		{
			get
			{
				return this.isWatermarkUpdated;
			}
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0004AF2F File Offset: 0x0004912F
		public override string ToString()
		{
			if (this.LoadedFromMailboxServer)
			{
				return this.mailboxServerSyncWatermark;
			}
			return this.stateStorageEncodedSyncWatermark;
		}

		// Token: 0x04000AC1 RID: 2753
		private readonly ISimpleStateStorage stateStorage;

		// Token: 0x04000AC2 RID: 2754
		private readonly SyncLogSession syncLogSession;

		// Token: 0x04000AC3 RID: 2755
		private readonly bool loadedFromMailboxServer;

		// Token: 0x04000AC4 RID: 2756
		private string stateStorageEncodedSyncWatermark;

		// Token: 0x04000AC5 RID: 2757
		private string mailboxServerSyncWatermark;

		// Token: 0x04000AC6 RID: 2758
		private bool isWatermarkUpdated;
	}
}
