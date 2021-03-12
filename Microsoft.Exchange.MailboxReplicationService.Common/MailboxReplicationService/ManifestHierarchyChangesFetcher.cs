using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000293 RID: 659
	internal sealed class ManifestHierarchyChangesFetcher : DisposeTrackableBase, IHierarchyChangesFetcher, IDisposable
	{
		// Token: 0x06002015 RID: 8213 RVA: 0x00044636 File Offset: 0x00042836
		public ManifestHierarchyChangesFetcher(MapiStore mapiStore, MailboxProviderBase mailbox, bool isPagedEnumeration)
		{
			this.mapiFolder = mapiStore.GetRootFolder();
			this.mailbox = mailbox;
			this.isPagedEnumeration = isPagedEnumeration;
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x00044658 File Offset: 0x00042858
		MailboxChangesManifest IHierarchyChangesFetcher.EnumerateHierarchyChanges(SyncHierarchyManifestState hierState, EnumerateHierarchyChangesFlags flags, int maxChanges)
		{
			MailboxChangesManifest mailboxChangesManifest = this.EnumerateChanges(hierState, flags, maxChanges);
			MrsTracer.Provider.Debug("Discovered {0} changed, {1} deleted", new object[]
			{
				mailboxChangesManifest.ChangedFolders.Count,
				mailboxChangesManifest.DeletedFolders.Count
			});
			return mailboxChangesManifest;
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x000446B0 File Offset: 0x000428B0
		private MailboxChangesManifest EnumerateChanges(SyncHierarchyManifestState hierState, EnumerateHierarchyChangesFlags flags, int maxChanges)
		{
			MailboxChangesManifest mailboxChangesManifest = new MailboxChangesManifest();
			this.ConfigureMapiManifest(hierState, mailboxChangesManifest, flags, maxChanges);
			ManifestStatus manifestStatus;
			do
			{
				manifestStatus = this.mapiManifest.Synchronize();
			}
			while (manifestStatus != ManifestStatus.Done && manifestStatus != ManifestStatus.Yielded);
			byte[] idsetGiven;
			byte[] cnsetSeen;
			this.mapiManifest.GetState(out idsetGiven, out cnsetSeen);
			hierState.IdsetGiven = idsetGiven;
			hierState.CnsetSeen = cnsetSeen;
			return mailboxChangesManifest;
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x00044700 File Offset: 0x00042900
		private void ConfigureMapiManifest(SyncHierarchyManifestState syncState, MailboxChangesManifest changes, EnumerateHierarchyChangesFlags flags, int maxChanges)
		{
			if (this.isPagedEnumeration && !flags.HasFlag(EnumerateHierarchyChangesFlags.FirstPage))
			{
				this.callback.InitializeNextPage(changes, maxChanges);
				return;
			}
			this.callback = new ManifestHierarchyCallback(this.isPagedEnumeration);
			this.callback.InitializeNextPage(changes, maxChanges);
			SyncConfigFlags syncConfigFlags = SyncConfigFlags.ManifestHierReturnDeletedEntryIds;
			if (((this.mailbox.ServerVersion >= Server.E14MinVersion && this.mailbox.ServerVersion < Server.E15MinVersion) || (long)this.mailbox.ServerVersion >= ManifestHierarchyChangesFetcher.E15MinVersionSupportsOnlySpecifiedPropsForHierarchy) && !this.mailbox.IsPureMAPI)
			{
				syncConfigFlags |= SyncConfigFlags.OnlySpecifiedProps;
			}
			using (this.mailbox.RHTracker.Start())
			{
				this.mapiManifest = this.mapiFolder.CreateExportHierarchyManifestEx(syncConfigFlags, syncState.IdsetGiven, syncState.CnsetSeen, this.callback, new PropTag[]
				{
					PropTag.EntryId
				}, null);
			}
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x0004480C File Offset: 0x00042A0C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ManifestHierarchyChangesFetcher>(this);
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x00044814 File Offset: 0x00042A14
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.mapiManifest != null)
				{
					this.mapiManifest.Dispose();
					this.mapiManifest = null;
				}
				if (this.mapiFolder != null)
				{
					this.mapiFolder.Dispose();
					this.mapiFolder = null;
				}
			}
		}

		// Token: 0x04000D00 RID: 3328
		private static readonly long E15MinVersionSupportsOnlySpecifiedPropsForHierarchy = (long)new ServerVersion(15, 0, 922, 0).ToInt();

		// Token: 0x04000D01 RID: 3329
		private readonly MailboxProviderBase mailbox;

		// Token: 0x04000D02 RID: 3330
		private readonly bool isPagedEnumeration;

		// Token: 0x04000D03 RID: 3331
		private MapiFolder mapiFolder;

		// Token: 0x04000D04 RID: 3332
		private MapiHierarchyManifestEx mapiManifest;

		// Token: 0x04000D05 RID: 3333
		private ManifestHierarchyCallback callback;
	}
}
