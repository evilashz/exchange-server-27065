using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000635 RID: 1589
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class COWSessionBase : DisposableObject, IDumpsterItemOperations
	{
		// Token: 0x06004152 RID: 16722 RVA: 0x00113AE0 File Offset: 0x00111CE0
		protected COWSessionBase()
		{
			this.inCallback = false;
			this.folderIdState = FolderIdState.FolderIdDefered;
		}

		// Token: 0x17001354 RID: 4948
		// (get) Token: 0x06004153 RID: 16723 RVA: 0x00113AF6 File Offset: 0x00111CF6
		// (set) Token: 0x06004154 RID: 16724 RVA: 0x00113AFE File Offset: 0x00111CFE
		protected virtual bool InCallback
		{
			get
			{
				return this.inCallback;
			}
			set
			{
				this.inCallback = value;
			}
		}

		// Token: 0x17001355 RID: 4949
		// (get) Token: 0x06004155 RID: 16725 RVA: 0x00113B07 File Offset: 0x00111D07
		// (set) Token: 0x06004156 RID: 16726 RVA: 0x00113B1A File Offset: 0x00111D1A
		public StoreObjectId RecoverableItemsDeletionsFolderId
		{
			get
			{
				this.CheckDisposed("get_RecoverableItemsDeletionsFolderId");
				return this.recoverableItemsDeletionsFolderId;
			}
			protected set
			{
				this.CheckDisposed("set_RecoverableItemsDeletionsFolderId");
				this.recoverableItemsDeletionsFolderId = value;
			}
		}

		// Token: 0x17001356 RID: 4950
		// (get) Token: 0x06004157 RID: 16727 RVA: 0x00113B2E File Offset: 0x00111D2E
		// (set) Token: 0x06004158 RID: 16728 RVA: 0x00113B41 File Offset: 0x00111D41
		public StoreObjectId RecoverableItemsVersionsFolderId
		{
			get
			{
				this.CheckDisposed("get_RecoverableItemsVersionsFolderId");
				return this.recoverableItemsVersionsFolderId;
			}
			protected set
			{
				this.CheckDisposed("set_RecoverableItemsVersionsFolderId");
				this.recoverableItemsVersionsFolderId = value;
			}
		}

		// Token: 0x17001357 RID: 4951
		// (get) Token: 0x06004159 RID: 16729 RVA: 0x00113B55 File Offset: 0x00111D55
		// (set) Token: 0x0600415A RID: 16730 RVA: 0x00113B68 File Offset: 0x00111D68
		public StoreObjectId RecoverableItemsPurgesFolderId
		{
			get
			{
				this.CheckDisposed("get_RecoverableItemsPurgesFolderId");
				return this.recoverableItemsPurgesFolderId;
			}
			protected set
			{
				this.CheckDisposed("set_RecoverableItemsPurgesFolderId");
				this.recoverableItemsPurgesFolderId = value;
			}
		}

		// Token: 0x17001358 RID: 4952
		// (get) Token: 0x0600415B RID: 16731 RVA: 0x00113B7C File Offset: 0x00111D7C
		// (set) Token: 0x0600415C RID: 16732 RVA: 0x00113B8F File Offset: 0x00111D8F
		public StoreObjectId RecoverableItemsDiscoveryHoldsFolderId
		{
			get
			{
				this.CheckDisposed("get_RecoverableItemsDiscoveryHoldsFolderId");
				return this.recoverableItemsDiscoveryHoldsFolderId;
			}
			protected set
			{
				this.CheckDisposed("set_RecoverableItemsDiscoveryHoldsFolderId");
				this.recoverableItemsDiscoveryHoldsFolderId = value;
			}
		}

		// Token: 0x17001359 RID: 4953
		// (get) Token: 0x0600415D RID: 16733 RVA: 0x00113BA3 File Offset: 0x00111DA3
		// (set) Token: 0x0600415E RID: 16734 RVA: 0x00113BB6 File Offset: 0x00111DB6
		public StoreObjectId RecoverableItemsMigratedMessagesFolderId
		{
			get
			{
				this.CheckDisposed("get_RecoverableItemsMigratedMessagesFolderId");
				return this.recoverableItemsMigratedMessagesFolderId;
			}
			protected set
			{
				this.CheckDisposed("set_RecoverableItemsMigratedMessagesFolderId");
				this.recoverableItemsMigratedMessagesFolderId = value;
			}
		}

		// Token: 0x1700135A RID: 4954
		// (get) Token: 0x0600415F RID: 16735 RVA: 0x00113BCA File Offset: 0x00111DCA
		// (set) Token: 0x06004160 RID: 16736 RVA: 0x00113BDD File Offset: 0x00111DDD
		public StoreObjectId RecoverableItemsRootFolderId
		{
			get
			{
				this.CheckDisposed("get_RecoverableItemsRootFolderId");
				return this.recoverableItemsRootFolderId;
			}
			protected set
			{
				this.CheckDisposed("set_RecoverableItemsRootFolderId");
				this.recoverableItemsRootFolderId = value;
			}
		}

		// Token: 0x1700135B RID: 4955
		// (get) Token: 0x06004161 RID: 16737 RVA: 0x00113BF1 File Offset: 0x00111DF1
		// (set) Token: 0x06004162 RID: 16738 RVA: 0x00113C04 File Offset: 0x00111E04
		public StoreObjectId CalendarLoggingFolderId
		{
			get
			{
				this.CheckDisposed("get_CalendarLoggingFolderId");
				return this.calendarLoggingFolderId;
			}
			protected set
			{
				this.CheckDisposed("set_CalendarLoggingFolderId");
				this.calendarLoggingFolderId = value;
			}
		}

		// Token: 0x1700135C RID: 4956
		// (get) Token: 0x06004163 RID: 16739 RVA: 0x00113C18 File Offset: 0x00111E18
		// (set) Token: 0x06004164 RID: 16740 RVA: 0x00113C2B File Offset: 0x00111E2B
		public StoreObjectId AuditsFolderId
		{
			get
			{
				this.CheckDisposed("get_AuditsFolderId");
				return this.auditsFolderId;
			}
			protected set
			{
				this.CheckDisposed("set_AuditsFolderId");
				this.auditsFolderId = value;
			}
		}

		// Token: 0x1700135D RID: 4957
		// (get) Token: 0x06004165 RID: 16741 RVA: 0x00113C3F File Offset: 0x00111E3F
		// (set) Token: 0x06004166 RID: 16742 RVA: 0x00113C52 File Offset: 0x00111E52
		public StoreObjectId AdminAuditLogsFolderId
		{
			get
			{
				this.CheckDisposed("get_AdminAuditLogsFolderId");
				return this.adminAuditLogsFolderId;
			}
			protected set
			{
				this.CheckDisposed("set_AdminAuditLogsFolderId");
				this.adminAuditLogsFolderId = value;
			}
		}

		// Token: 0x1700135E RID: 4958
		// (get) Token: 0x06004167 RID: 16743 RVA: 0x00113C66 File Offset: 0x00111E66
		// (set) Token: 0x06004168 RID: 16744 RVA: 0x00113C79 File Offset: 0x00111E79
		public COWResults Results
		{
			get
			{
				this.CheckDisposed("get_Results");
				return this.results;
			}
			protected set
			{
				this.CheckDisposed("set_Results");
				this.results = value;
			}
		}

		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x06004169 RID: 16745
		public abstract StoreSession StoreSession { get; }

		// Token: 0x17001360 RID: 4960
		// (get) Token: 0x0600416A RID: 16746 RVA: 0x00113C8D File Offset: 0x00111E8D
		// (set) Token: 0x0600416B RID: 16747 RVA: 0x00113CA0 File Offset: 0x00111EA0
		protected FolderIdState FolderIdState
		{
			get
			{
				this.CheckDisposed("get_FolderIdState");
				return this.folderIdState;
			}
			set
			{
				this.CheckDisposed("set_FolderIdState");
				this.folderIdState = value;
			}
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x00113CB4 File Offset: 0x00111EB4
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<COWSessionBase>(this);
		}

		// Token: 0x0600416D RID: 16749 RVA: 0x00113CBC File Offset: 0x00111EBC
		public GroupOperationResult GetCallbackResults()
		{
			this.CheckDisposed("GetCallbackResults");
			if (this.results == null)
			{
				return null;
			}
			return this.results.GetResults();
		}

		// Token: 0x0600416E RID: 16750 RVA: 0x00113CE0 File Offset: 0x00111EE0
		public bool IsDumpsterFolder(MailboxSession sessionWithBestAccess, StoreObjectId folderId)
		{
			this.CheckDisposed("IsDumpsterFolder");
			if (folderId == null)
			{
				return false;
			}
			this.CheckFolderInitState(sessionWithBestAccess);
			return folderId.Equals(this.recoverableItemsRootFolderId) || folderId.Equals(this.recoverableItemsDeletionsFolderId) || folderId.Equals(this.recoverableItemsVersionsFolderId) || folderId.Equals(this.recoverableItemsPurgesFolderId) || folderId.Equals(this.recoverableItemsDiscoveryHoldsFolderId) || folderId.Equals(this.calendarLoggingFolderId) || folderId.Equals(this.recoverableItemsMigratedMessagesFolderId) || folderId.Equals(this.auditsFolderId) || folderId.Equals(this.adminAuditLogsFolderId);
		}

		// Token: 0x0600416F RID: 16751 RVA: 0x00113D82 File Offset: 0x00111F82
		public virtual bool IsAuditFolder(StoreObjectId folderId)
		{
			return false;
		}

		// Token: 0x06004170 RID: 16752 RVA: 0x00113D88 File Offset: 0x00111F88
		protected void CheckFolderInitState(MailboxSession sessionWithBestAccess)
		{
			if (this.folderIdState == FolderIdState.FolderIdDefered)
			{
				this.folderIdState = this.InternalInitFolders(sessionWithBestAccess);
			}
			if (this.folderIdState != FolderIdState.FolderIdSuccess)
			{
				ExTraceGlobals.SessionTracer.TraceDebug((long)sessionWithBestAccess.GetHashCode(), "Recoverable Items folders/folderIds missing");
				throw new RecoverableItemsAccessDeniedException("Recoverable Items");
			}
		}

		// Token: 0x06004171 RID: 16753
		protected abstract FolderIdState InternalInitFolders(MailboxSession sessionWithBestAccess);

		// Token: 0x06004172 RID: 16754 RVA: 0x00113DD4 File Offset: 0x00111FD4
		public virtual StoreObjectId CopyItemToDumpster(MailboxSession sessionWithBestAccess, StoreObjectId destinationFolderId, ICoreItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x00113DDB File Offset: 0x00111FDB
		public void CopyItemsToDumpster(MailboxSession sessionWithBestAccess, StoreObjectId destinationFolderId, StoreObjectId[] itemIds)
		{
			this.CheckDisposed("CopyItemsToDumpster");
			this.InternalCopyOrMoveItemsToDumpster(sessionWithBestAccess, destinationFolderId, itemIds, false, false, false);
		}

		// Token: 0x06004174 RID: 16756 RVA: 0x00113DF5 File Offset: 0x00111FF5
		public void CopyItemsToDumpster(MailboxSession sessionWithBestAccess, StoreObjectId destinationFolderId, StoreObjectId[] itemIds, bool forceNonIPM)
		{
			this.CheckDisposed("CopyItemsToDumpster");
			this.InternalCopyOrMoveItemsToDumpster(sessionWithBestAccess, destinationFolderId, itemIds, false, forceNonIPM, false);
		}

		// Token: 0x06004175 RID: 16757 RVA: 0x00113E10 File Offset: 0x00112010
		public void MoveItemsToDumpster(MailboxSession sessionWithBestAccess, StoreObjectId destinationFolderId, StoreObjectId[] itemIds)
		{
			this.CheckDisposed("MoveItemsToDumpster");
			this.InternalCopyOrMoveItemsToDumpster(sessionWithBestAccess, destinationFolderId, itemIds, true, false, false);
		}

		// Token: 0x06004176 RID: 16758 RVA: 0x00113E2A File Offset: 0x0011202A
		protected virtual IList<StoreObjectId> InternalCopyOrMoveItemsToDumpster(MailboxSession sessionWithBestAccess, StoreObjectId destinationFolderId, StoreObjectId[] itemIds, bool moveRequest, bool forceNonIPM, bool returnNewItemIds)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004177 RID: 16759 RVA: 0x00113E31 File Offset: 0x00112031
		public virtual void RollbackItemVersion(MailboxSession sessionWithBestAccess, CoreItem itemUpdated, StoreObjectId itemIdToRollback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004178 RID: 16760 RVA: 0x00113E38 File Offset: 0x00112038
		public virtual bool IsDumpsterOverWarningQuota(COWSettings settings)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004179 RID: 16761 RVA: 0x00113E3F File Offset: 0x0011203F
		public virtual void DisableCalendarLogging()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x00113E46 File Offset: 0x00112046
		public virtual bool IsDumpsterOverCalendarLoggingQuota(MailboxSession sessionWithBestAccess, COWSettings settings)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600417B RID: 16763 RVA: 0x00113E50 File Offset: 0x00112050
		internal static COWTriggerAction GetTriggerAction(FolderChangeOperation operation)
		{
			EnumValidator.ThrowIfInvalid<FolderChangeOperation>(operation, "operation");
			switch (operation)
			{
			case FolderChangeOperation.Copy:
				return COWTriggerAction.Copy;
			case FolderChangeOperation.Move:
				return COWTriggerAction.Move;
			case FolderChangeOperation.MoveToDeletedItems:
				return COWTriggerAction.MoveToDeletedItems;
			case FolderChangeOperation.SoftDelete:
				return COWTriggerAction.SoftDelete;
			case FolderChangeOperation.HardDelete:
				return COWTriggerAction.HardDelete;
			case FolderChangeOperation.DoneWithMessageDelete:
				return COWTriggerAction.DoneWithMessageDelete;
			default:
				throw new NotSupportedException("Invalid folder change operation");
			}
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x00113EA1 File Offset: 0x001120A1
		internal static bool IsDeleteOperation(COWTriggerAction operation)
		{
			return operation == COWTriggerAction.SoftDelete || operation == COWTriggerAction.HardDelete;
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x00113EB0 File Offset: 0x001120B0
		internal static List<StoreObjectId> InternalFilterFolders(ICollection<StoreObjectId> itemIds)
		{
			List<StoreObjectId> list = new List<StoreObjectId>(1);
			foreach (StoreObjectId storeObjectId in itemIds)
			{
				if (Folder.IsFolderId(storeObjectId) && !list.Contains(storeObjectId))
				{
					list.Add(storeObjectId);
				}
			}
			return list;
		}

		// Token: 0x0600417E RID: 16766 RVA: 0x00113F14 File Offset: 0x00112114
		internal static List<StoreObjectId> InternalFilterItems(ICollection<StoreObjectId> itemIds)
		{
			HashSet<StoreObjectId> hashSet = new HashSet<StoreObjectId>(itemIds);
			List<StoreObjectId> list = new List<StoreObjectId>(hashSet.Count);
			foreach (StoreObjectId storeObjectId in hashSet)
			{
				if (!Folder.IsFolderId(storeObjectId))
				{
					list.Add(storeObjectId);
				}
			}
			return list;
		}

		// Token: 0x04002402 RID: 9218
		private bool inCallback;

		// Token: 0x04002403 RID: 9219
		private StoreObjectId recoverableItemsDeletionsFolderId;

		// Token: 0x04002404 RID: 9220
		private StoreObjectId recoverableItemsVersionsFolderId;

		// Token: 0x04002405 RID: 9221
		private StoreObjectId recoverableItemsPurgesFolderId;

		// Token: 0x04002406 RID: 9222
		private StoreObjectId recoverableItemsDiscoveryHoldsFolderId;

		// Token: 0x04002407 RID: 9223
		private StoreObjectId recoverableItemsMigratedMessagesFolderId;

		// Token: 0x04002408 RID: 9224
		private StoreObjectId recoverableItemsRootFolderId;

		// Token: 0x04002409 RID: 9225
		private StoreObjectId calendarLoggingFolderId;

		// Token: 0x0400240A RID: 9226
		private StoreObjectId auditsFolderId;

		// Token: 0x0400240B RID: 9227
		private StoreObjectId adminAuditLogsFolderId;

		// Token: 0x0400240C RID: 9228
		private COWResults results;

		// Token: 0x0400240D RID: 9229
		protected FolderIdState folderIdState;
	}
}
