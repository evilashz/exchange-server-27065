using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000A RID: 10
	internal class MapiSourceFolder : MapiFolder, ISourceFolder, IFolder, IDisposable
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00007494 File Offset: 0x00005694
		void ISourceFolder.CopyTo(IFxProxy destFolderProxy, CopyPropertiesFlags flags, PropTag[] excludeTags)
		{
			MrsTracer.Provider.Function("MapiSourceFolder.CopyTo", new object[0]);
			if ((base.Mailbox.Flags & LocalMailboxFlags.StripLargeRulesForDownlevelTargets) != LocalMailboxFlags.None)
			{
				flags |= CopyPropertiesFlags.StripLargeRulesForDownlevelTargets;
			}
			bool flag = false;
			try
			{
				using (base.Mailbox.RHTracker.Start())
				{
					using (FxProxyBudgetWrapper fxProxyBudgetWrapper = new FxProxyBudgetWrapper(destFolderProxy, false, new Func<IDisposable>(base.Mailbox.RHTracker.StartExclusive), new Action<uint>(base.Mailbox.RHTracker.Charge)))
					{
						base.Folder.ExportObject(fxProxyBudgetWrapper, flags, excludeTags);
					}
				}
				flag = true;
				destFolderProxy.Flush();
			}
			catch (LocalizedException)
			{
				if (!flag)
				{
					MrsTracer.Provider.Debug("Flushing target proxy after receiving an exception.", new object[0]);
					CommonUtils.CatchKnownExceptions(new Action(destFolderProxy.Flush), null);
				}
				throw;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000759C File Offset: 0x0000579C
		void ISourceFolder.ExportMessages(IFxProxy destFolderProxy, CopyMessagesFlags flags, byte[][] entryIds)
		{
			MrsTracer.Provider.Function("MapiSourceFolder.CopyTo", new object[0]);
			bool flag = false;
			try
			{
				using (base.Mailbox.RHTracker.Start())
				{
					using (FxProxyBudgetWrapper fxProxyBudgetWrapper = new FxProxyBudgetWrapper(destFolderProxy, false, new Func<IDisposable>(base.Mailbox.RHTracker.StartExclusive), new Action<uint>(base.Mailbox.RHTracker.Charge)))
					{
						base.Folder.ExportMessages(fxProxyBudgetWrapper, flags, entryIds);
					}
				}
				flag = true;
				destFolderProxy.Flush();
			}
			catch (LocalizedException)
			{
				if (!flag)
				{
					MrsTracer.Provider.Debug("Flushing target proxy after receiving an exception.", new object[0]);
					CommonUtils.CatchKnownExceptions(new Action(destFolderProxy.Flush), null);
				}
				throw;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000768C File Offset: 0x0000588C
		FolderChangesManifest ISourceFolder.EnumerateChanges(EnumerateContentChangesFlags flags, int maxChanges)
		{
			if (!base.Folder.IsContentAvailable)
			{
				return new FolderChangesManifest(base.FolderId);
			}
			SyncContentsManifestState syncState = ((MapiSourceMailbox)base.Mailbox).SyncState[base.FolderId];
			bool flag = maxChanges != 0;
			if (!flag || flags.HasFlag(EnumerateContentChangesFlags.FirstPage))
			{
				MapiStore mapiStore = base.Mailbox.MapiStore;
				int[] array = new int[4];
				array[0] = 8;
				if (mapiStore.IsVersionGreaterOrEqualThan(array))
				{
					this.contentChangesFetcher = new ManifestContentChangesFetcher(this, base.Folder, base.Mailbox, flag);
				}
				else
				{
					this.contentChangesFetcher = new TitaniumContentChangesFetcher(this, base.Folder, base.Mailbox);
				}
			}
			return this.contentChangesFetcher.EnumerateContentChanges(syncState, flags, maxChanges);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000774D File Offset: 0x0000594D
		List<MessageRec> ISourceFolder.EnumerateMessagesPaged(int maxPageSize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00007754 File Offset: 0x00005954
		int ISourceFolder.GetEstimatedItemCount()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000775B File Offset: 0x0000595B
		internal override void Config(byte[] folderId, MapiFolder folder, MapiMailbox mailbox)
		{
			base.Config(folderId, folder, mailbox);
			base.Folder.AllowWarnings = true;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00007850 File Offset: 0x00005A50
		internal void CopyBatch(IFxProxyPool proxyPool, List<MessageRec> batch)
		{
			MapiSourceFolder.<>c__DisplayClass1 CS$<>8__locals1 = new MapiSourceFolder.<>c__DisplayClass1();
			CS$<>8__locals1.<>4__this = this;
			if (batch.Count == 0)
			{
				return;
			}
			byte[][] array = new byte[batch.Count][];
			for (int i = 0; i < batch.Count; i++)
			{
				array[i] = batch[i].EntryId;
			}
			CS$<>8__locals1.flags = CopyMessagesFlags.SendEntryId;
			using (IMapiFxProxy destFolderProxy = proxyPool.GetFolderProxy(base.FolderId))
			{
				if (destFolderProxy == null)
				{
					MrsTracer.Provider.Warning("Destination folder {0} does not exist.", new object[]
					{
						TraceUtils.DumpEntryId(base.FolderId)
					});
				}
				else
				{
					MapiUtils.ProcessMapiCallInBatches<byte[]>(array, delegate(byte[][] smallBatch)
					{
						using (CS$<>8__locals1.<>4__this.Mailbox.RHTracker.Start())
						{
							using (FxProxyBudgetWrapper fxProxyBudgetWrapper = new FxProxyBudgetWrapper(destFolderProxy, false, new Func<IDisposable>(CS$<>8__locals1.<>4__this.Mailbox.RHTracker.StartExclusive), new Action<uint>(CS$<>8__locals1.<>4__this.Mailbox.RHTracker.Charge)))
							{
								CS$<>8__locals1.<>4__this.Folder.ExportMessages(fxProxyBudgetWrapper, CS$<>8__locals1.flags, smallBatch);
							}
						}
					});
				}
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000793C File Offset: 0x00005B3C
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.contentChangesFetcher != null)
			{
				this.contentChangesFetcher.Dispose();
				this.contentChangesFetcher = null;
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00007962 File Offset: 0x00005B62
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiSourceFolder>(this);
		}

		// Token: 0x04000022 RID: 34
		private IContentChangesFetcher contentChangesFetcher;
	}
}
