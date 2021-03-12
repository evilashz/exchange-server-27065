using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000D6 RID: 214
	internal class OneTimeSearch : DisposableBase
	{
		// Token: 0x06000702 RID: 1794 RVA: 0x0001AFC0 File Offset: 0x000191C0
		private OneTimeSearch()
		{
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001AFC8 File Offset: 0x000191C8
		private OneTimeSearch(UMSubscriber user, StoreObjectId folderId, int itemCount)
		{
			this.user = user;
			this.folderId = folderId;
			this.itemCount = itemCount;
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0001AFE5 File Offset: 0x000191E5
		internal StoreObjectId FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0001AFED File Offset: 0x000191ED
		internal int ItemCount
		{
			get
			{
				return this.itemCount;
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001AFF8 File Offset: 0x000191F8
		internal static OneTimeSearch Execute(UMSubscriber user, MailboxSession session, StoreId fid, QueryFilter filter)
		{
			string displayName = "EXUM-23479-" + Guid.NewGuid().ToString();
			OneTimeSearch result;
			using (SearchFolder searchFolder = SearchFolder.Create(session, session.GetDefaultFolderId(DefaultFolderType.SearchFolders), displayName, CreateMode.OpenIfExists))
			{
				searchFolder.Save();
				searchFolder.Load();
				IAsyncResult asyncResult = searchFolder.BeginApplyOneTimeSearch(new SearchFolderCriteria(filter, new StoreId[]
				{
					fid
				})
				{
					DeepTraversal = false
				}, null, null);
				searchFolder.EndApplyOneTimeSearch(asyncResult);
				searchFolder.Save();
				searchFolder.Load(new PropertyDefinition[]
				{
					FolderSchema.ItemCount
				});
				result = new OneTimeSearch(user, searchFolder.Id.ObjectId, searchFolder.ItemCount);
			}
			return result;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001B0C8 File Offset: 0x000192C8
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.folderId != null)
			{
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
				{
					mailboxSessionLock.Session.Delete(DeleteItemFlags.HardDelete, new StoreId[]
					{
						this.folderId
					});
				}
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001B128 File Offset: 0x00019328
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OneTimeSearch>(this);
		}

		// Token: 0x0400040E RID: 1038
		private const string UmPrefix = "EXUM-23479-";

		// Token: 0x0400040F RID: 1039
		private StoreObjectId folderId;

		// Token: 0x04000410 RID: 1040
		private int itemCount;

		// Token: 0x04000411 RID: 1041
		private UMSubscriber user;
	}
}
