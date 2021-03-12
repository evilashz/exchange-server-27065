using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000291 RID: 657
	internal sealed class ManifestContentChangesFetcher : DisposeTrackableBase, IContentChangesFetcher, IDisposable
	{
		// Token: 0x0600200A RID: 8202 RVA: 0x00044262 File Offset: 0x00042462
		public ManifestContentChangesFetcher(ISourceFolder folder, MapiFolder mapiFolder, MailboxProviderBase mailbox, bool isPagedEnumeration)
		{
			this.folder = folder;
			this.mapiFolder = mapiFolder;
			this.mailbox = mailbox;
			this.isPagedEnumeration = isPagedEnumeration;
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x00044288 File Offset: 0x00042488
		FolderChangesManifest IContentChangesFetcher.EnumerateContentChanges(SyncContentsManifestState syncState, EnumerateContentChangesFlags flags, int maxChanges)
		{
			FolderChangesManifest folderChangesManifest = this.EnumerateChanges(syncState, flags, maxChanges);
			if (MrsTracer.Provider.IsEnabled(TraceType.DebugTrace))
			{
				int num;
				int num2;
				int num3;
				folderChangesManifest.GetMessageCounts(out num, out num2, out num3);
				MrsTracer.Provider.Debug("Discovered {0} new, {1} changed, {2} deleted, {3} read, {4} unread.", new object[]
				{
					num,
					num2,
					num3,
					folderChangesManifest.ReadMessages.Count,
					folderChangesManifest.UnreadMessages.Count
				});
			}
			return folderChangesManifest;
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x00044318 File Offset: 0x00042518
		private FolderChangesManifest EnumerateChanges(SyncContentsManifestState syncState, EnumerateContentChangesFlags flags, int maxChanges)
		{
			FolderChangesManifest folderChangesManifest = new FolderChangesManifest(this.folder.GetFolderId());
			if (this.folder.GetFolderRec(null, GetFolderRecFlags.None).FolderType == FolderType.Search)
			{
				return folderChangesManifest;
			}
			this.ConfigureMapiManifest(syncState, folderChangesManifest, flags, maxChanges);
			ManifestStatus manifestStatus;
			do
			{
				manifestStatus = this.mapiManifest.Synchronize();
			}
			while (manifestStatus != ManifestStatus.Done && manifestStatus != ManifestStatus.Yielded);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				this.mapiManifest.GetState(memoryStream);
				syncState.Data = memoryStream.ToArray();
			}
			return folderChangesManifest;
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x000443A8 File Offset: 0x000425A8
		private void ConfigureMapiManifest(SyncContentsManifestState syncState, FolderChangesManifest changes, EnumerateContentChangesFlags flags, int maxChanges)
		{
			if (this.isPagedEnumeration && !flags.HasFlag(EnumerateContentChangesFlags.FirstPage))
			{
				this.callback.InitializeNextPage(changes, maxChanges);
				return;
			}
			this.callback = new ManifestContentsCallback(this.folder.GetFolderId(), this.isPagedEnumeration);
			this.callback.InitializeNextPage(changes, maxChanges);
			ManifestConfigFlags manifestConfigFlags = ManifestConfigFlags.Associated | ManifestConfigFlags.Normal | ManifestConfigFlags.OrderByDeliveryTime;
			if (flags.HasFlag(EnumerateContentChangesFlags.Catchup))
			{
				manifestConfigFlags |= ManifestConfigFlags.Catchup;
			}
			Restriction restriction = ((this.mailbox.Options & MailboxOptions.IgnoreExtendedRuleFAIs) != MailboxOptions.None) ? ContentChangesFetcherUtils.ExcludeAllRulesRestriction : ContentChangesFetcherUtils.ExcludeV40RulesRestriction;
			using (this.mailbox.RHTracker.Start())
			{
				this.mapiManifest = this.mapiFolder.CreateExportManifest();
			}
			using (MemoryStream memoryStream = (syncState.Data != null) ? new MemoryStream(syncState.Data) : null)
			{
				using (this.mailbox.RHTracker.Start())
				{
					this.mapiManifest.Configure(manifestConfigFlags, restriction, memoryStream, this.callback, new PropTag[0]);
				}
			}
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x000444F8 File Offset: 0x000426F8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ManifestContentChangesFetcher>(this);
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x00044500 File Offset: 0x00042700
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.mapiManifest != null)
			{
				this.mapiManifest.Dispose();
				this.mapiManifest = null;
			}
		}

		// Token: 0x04000CF6 RID: 3318
		private readonly ISourceFolder folder;

		// Token: 0x04000CF7 RID: 3319
		private readonly MapiFolder mapiFolder;

		// Token: 0x04000CF8 RID: 3320
		private readonly MailboxProviderBase mailbox;

		// Token: 0x04000CF9 RID: 3321
		private readonly bool isPagedEnumeration;

		// Token: 0x04000CFA RID: 3322
		private MapiManifest mapiManifest;

		// Token: 0x04000CFB RID: 3323
		private ManifestContentsCallback callback;
	}
}
