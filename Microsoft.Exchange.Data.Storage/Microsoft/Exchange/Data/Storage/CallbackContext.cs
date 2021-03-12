using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000617 RID: 1559
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CallbackContext : DisposableObject
	{
		// Token: 0x06003FFB RID: 16379 RVA: 0x0010A9AC File Offset: 0x00108BAC
		public CallbackContext(StoreSession session)
		{
			if (session != null && session is MailboxSession)
			{
				this.session = (MailboxSession)session;
			}
		}

		// Token: 0x17001310 RID: 4880
		// (get) Token: 0x06003FFC RID: 16380 RVA: 0x0010A9E1 File Offset: 0x00108BE1
		internal IDictionary<StoreObjectId, FolderAuditInfo> FolderAuditInfo
		{
			get
			{
				this.CheckDisposed(null);
				if (this.folderAuditInfo == null)
				{
					this.folderAuditInfo = new Dictionary<StoreObjectId, FolderAuditInfo>();
				}
				return this.folderAuditInfo;
			}
		}

		// Token: 0x17001311 RID: 4881
		// (get) Token: 0x06003FFD RID: 16381 RVA: 0x0010AA03 File Offset: 0x00108C03
		internal IDictionary<StoreObjectId, ItemAuditInfo> ItemAuditInfo
		{
			get
			{
				this.CheckDisposed(null);
				if (this.itemAuditInfo == null)
				{
					this.itemAuditInfo = new Dictionary<StoreObjectId, ItemAuditInfo>();
				}
				return this.itemAuditInfo;
			}
		}

		// Token: 0x17001312 RID: 4882
		// (get) Token: 0x06003FFE RID: 16382 RVA: 0x0010AA25 File Offset: 0x00108C25
		// (set) Token: 0x06003FFF RID: 16383 RVA: 0x0010AA2D File Offset: 0x00108C2D
		internal ItemAuditInfo ItemOperationAuditInfo { get; set; }

		// Token: 0x17001313 RID: 4883
		// (get) Token: 0x06004000 RID: 16384 RVA: 0x0010AA36 File Offset: 0x00108C36
		// (set) Token: 0x06004001 RID: 16385 RVA: 0x0010AA3E File Offset: 0x00108C3E
		internal bool? AuditSkippedOnBefore { get; set; }

		// Token: 0x17001314 RID: 4884
		// (get) Token: 0x06004002 RID: 16386 RVA: 0x0010AA47 File Offset: 0x00108C47
		// (set) Token: 0x06004003 RID: 16387 RVA: 0x0010AA4F File Offset: 0x00108C4F
		internal MailboxAuditOperations SubmitAuditOperation { get; set; }

		// Token: 0x17001315 RID: 4885
		// (get) Token: 0x06004004 RID: 16388 RVA: 0x0010AA58 File Offset: 0x00108C58
		// (set) Token: 0x06004005 RID: 16389 RVA: 0x0010AA60 File Offset: 0x00108C60
		internal MailboxSession SubmitEffectiveMailboxSession { get; set; }

		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x06004006 RID: 16390 RVA: 0x0010AA69 File Offset: 0x00108C69
		// (set) Token: 0x06004007 RID: 16391 RVA: 0x0010AA71 File Offset: 0x00108C71
		internal ExchangePrincipal SubmitEffectiveMailboxOwner { get; set; }

		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x06004008 RID: 16392 RVA: 0x0010AA7A File Offset: 0x00108C7A
		// (set) Token: 0x06004009 RID: 16393 RVA: 0x0010AA82 File Offset: 0x00108C82
		internal ContactLinkingProcessingState ContactLinkingProcessingState { get; set; }

		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x0600400A RID: 16394 RVA: 0x0010AA8B File Offset: 0x00108C8B
		// (set) Token: 0x0600400B RID: 16395 RVA: 0x0010AA93 File Offset: 0x00108C93
		internal COWProcessorState SiteMailboxMessageDedupState { get; set; }

		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x0600400C RID: 16396 RVA: 0x0010AA9C File Offset: 0x00108C9C
		// (set) Token: 0x0600400D RID: 16397 RVA: 0x0010AAA4 File Offset: 0x00108CA4
		internal COWProcessorState COWGroupMessageEscalationState { get; set; }

		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x0600400E RID: 16398 RVA: 0x0010AAAD File Offset: 0x00108CAD
		// (set) Token: 0x0600400F RID: 16399 RVA: 0x0010AAB5 File Offset: 0x00108CB5
		internal COWProcessorState COWGroupMessageWSPublishingState { get; set; }

		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x06004010 RID: 16400 RVA: 0x0010AAC0 File Offset: 0x00108CC0
		public MailboxSession SessionWithBestAccess
		{
			get
			{
				this.CheckDisposed(null);
				if (this.sessionWithBestAccess != null)
				{
					return this.sessionWithBestAccess;
				}
				if (this.session != null && COWSession.IsDelegateSession(this.session))
				{
					this.sessionWithBestAccess = COWSettings.GetAdminMailboxSession(this.session);
					this.sessionWithBestAccess.SetClientIPEndpoints(this.session.ClientIPAddress, this.session.ServerIPAddress);
					COWSession.PerfCounters.DumpsterDelegateSessionsActive.Increment();
					return this.sessionWithBestAccess;
				}
				return this.session;
			}
		}

		// Token: 0x06004011 RID: 16401 RVA: 0x0010AB47 File Offset: 0x00108D47
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<CallbackContext>(this);
		}

		// Token: 0x06004012 RID: 16402 RVA: 0x0010AB50 File Offset: 0x00108D50
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.SubmitEffectiveMailboxSession != null)
				{
					this.SubmitEffectiveMailboxSession.Dispose();
					this.SubmitEffectiveMailboxSession = null;
				}
				if (this.sessionWithBestAccess != null)
				{
					COWSettings.ReturnAdminMailboxSession(this.sessionWithBestAccess);
					this.sessionWithBestAccess = null;
					COWSession.PerfCounters.DumpsterDelegateSessionsActive.Decrement();
				}
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x0400235F RID: 9055
		private Dictionary<StoreObjectId, FolderAuditInfo> folderAuditInfo = new Dictionary<StoreObjectId, FolderAuditInfo>();

		// Token: 0x04002360 RID: 9056
		private Dictionary<StoreObjectId, ItemAuditInfo> itemAuditInfo = new Dictionary<StoreObjectId, ItemAuditInfo>();

		// Token: 0x04002361 RID: 9057
		private MailboxSession session;

		// Token: 0x04002362 RID: 9058
		private MailboxSession sessionWithBestAccess;
	}
}
