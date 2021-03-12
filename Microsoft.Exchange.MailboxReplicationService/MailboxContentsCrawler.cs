using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200002E RID: 46
	internal sealed class MailboxContentsCrawler : DisposeTrackableBase
	{
		// Token: 0x060001DF RID: 479 RVA: 0x0000A384 File Offset: 0x00008584
		internal MailboxContentsCrawler(MailboxCopierBase mailboxCopier, IReadOnlyCollection<FolderMapping> foldersToCopy)
		{
			ArgumentValidator.ThrowIfNull("mailboxCopier", mailboxCopier);
			this.mailboxCopier = mailboxCopier;
			this.Initialize(mailboxCopier.SourceMailbox, foldersToCopy, ConfigBase<MRSConfigSchema>.GetConfig<int>("MaxFolderOpened"), ConfigBase<MRSConfigSchema>.GetConfig<int>("CrawlerPageSize"), ConfigBase<MRSConfigSchema>.GetConfig<int>("EnumerateMessagesPageSize"));
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000A3D4 File Offset: 0x000085D4
		internal MailboxContentsCrawler(ISourceMailbox sourceMalibox, IReadOnlyCollection<FolderMapping> foldersToCopy, int maxFoldersOpened, int pageSize, int maxPageSize)
		{
			this.Initialize(sourceMalibox, foldersToCopy, maxFoldersOpened, pageSize, maxPageSize);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000A3EC File Offset: 0x000085EC
		internal FolderMapping GetNextFolderToCopy(out FolderContentsCrawler folderContentsCrawler, out bool shouldCopyProperties)
		{
			MrsTracer.Service.Function("MailboxContentsCrawler.GetNextFolderToCopy", new object[0]);
			base.CheckDisposed();
			folderContentsCrawler = null;
			shouldCopyProperties = false;
			for (int i = 0; i <= this.folderCount; i++)
			{
				if (!this.folderEnumerator.MoveNext())
				{
					MrsTracer.Service.Debug("Tail is reached, move to head", new object[0]);
					this.folderEnumerator.Reset();
					if (!this.folderEnumerator.MoveNext())
					{
						break;
					}
				}
				FolderMapping folderMapping = this.folderEnumerator.Current;
				if (folderMapping.FolderType != FolderType.Search && folderMapping.IsIncluded && !this.crawledFolders.ContainsKey(folderMapping.EntryId))
				{
					FolderContentsCrawler folderContentsCrawler2;
					if (!this.crawlers.TryGetValue(folderMapping.EntryId, out folderContentsCrawler2) && this.crawlers.Count < this.maxFoldersOpened)
					{
						MrsTracer.Service.Debug("Add crawler for the folder: '{0}'", new object[]
						{
							folderMapping.FullFolderName
						});
						ISourceFolder folder = this.sourceMalibox.GetFolder(folderMapping.EntryId);
						if (folder != null)
						{
							folderContentsCrawler2 = new FolderContentsCrawler(folder, this.pageSize, this.maxPageSize)
							{
								MailboxCopier = this.mailboxCopier
							};
							using (DisposeGuard disposeGuard = folderContentsCrawler2.Guard())
							{
								this.crawlers.Add(folderMapping.EntryId, folderContentsCrawler2);
								disposeGuard.Success();
								shouldCopyProperties = true;
							}
						}
					}
					if (folderContentsCrawler2 != null)
					{
						if (folderContentsCrawler2.HasMoreMessages)
						{
							MrsTracer.Service.Debug("Return the folder to copy: '{0}'", new object[]
							{
								folderMapping.FullFolderName
							});
							folderContentsCrawler = folderContentsCrawler2;
							return folderMapping;
						}
						MrsTracer.Service.Debug("The folder has completed crawling: '{0}'", new object[]
						{
							folderMapping.FullFolderName
						});
						this.crawledFolders.Add(folderMapping.EntryId, folderMapping);
						folderContentsCrawler2.Dispose();
						this.crawlers.Remove(folderMapping.EntryId);
					}
				}
			}
			MrsTracer.Service.Debug("No more folders", new object[0]);
			return null;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000A610 File Offset: 0x00008810
		internal void ResetFolder(FolderMapping folder)
		{
			MrsTracer.Service.Function("MailboxContentsCrawler.ResetFolder: '{0}'", new object[]
			{
				folder
			});
			base.CheckDisposed();
			byte[] entryId = folder.EntryId;
			this.crawledFolders.Remove(entryId);
			FolderContentsCrawler folderContentsCrawler;
			if (this.crawlers.TryGetValue(entryId, out folderContentsCrawler))
			{
				folderContentsCrawler.Dispose();
				this.crawlers.Remove(entryId);
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000A675 File Offset: 0x00008875
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxContentsCrawler>(this);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000A680 File Offset: 0x00008880
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.folderEnumerator != null)
				{
					this.folderEnumerator.Dispose();
					this.folderEnumerator = null;
				}
				if (this.crawlers != null)
				{
					foreach (FolderContentsCrawler folderContentsCrawler in this.crawlers.Values)
					{
						if (folderContentsCrawler != null)
						{
							folderContentsCrawler.Dispose();
						}
					}
					this.crawlers.Clear();
					this.crawlers = null;
				}
				if (this.crawledFolders != null)
				{
					this.crawledFolders.Clear();
					this.crawledFolders = null;
				}
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000A730 File Offset: 0x00008930
		private void Initialize(ISourceMailbox sourceMalibox, IReadOnlyCollection<FolderMapping> foldersToCopy, int maxFoldersOpened, int pageSize, int maxPageSize)
		{
			ArgumentValidator.ThrowIfNull("sourceMalibox", sourceMalibox);
			ArgumentValidator.ThrowIfNull("foldersToCopy", foldersToCopy);
			ArgumentValidator.ThrowIfZeroOrNegative("maxFolderOpened", maxFoldersOpened);
			ArgumentValidator.ThrowIfZeroOrNegative("pageSize", pageSize);
			ArgumentValidator.ThrowIfZeroOrNegative("maxPageSize", maxPageSize);
			this.sourceMalibox = sourceMalibox;
			this.pageSize = pageSize;
			this.maxPageSize = maxPageSize;
			this.folderCount = foldersToCopy.Count;
			this.maxFoldersOpened = maxFoldersOpened;
			this.folderEnumerator = foldersToCopy.GetEnumerator();
			this.crawlers = new EntryIdMap<FolderContentsCrawler>(Math.Min(this.folderCount, this.maxFoldersOpened));
			this.crawledFolders = new EntryIdMap<FolderMapping>(this.folderCount);
		}

		// Token: 0x040000D3 RID: 211
		private readonly MailboxCopierBase mailboxCopier;

		// Token: 0x040000D4 RID: 212
		private ISourceMailbox sourceMalibox;

		// Token: 0x040000D5 RID: 213
		private int pageSize;

		// Token: 0x040000D6 RID: 214
		private int maxPageSize;

		// Token: 0x040000D7 RID: 215
		private int maxFoldersOpened;

		// Token: 0x040000D8 RID: 216
		private int folderCount;

		// Token: 0x040000D9 RID: 217
		private IEnumerator<FolderMapping> folderEnumerator;

		// Token: 0x040000DA RID: 218
		private EntryIdMap<FolderContentsCrawler> crawlers;

		// Token: 0x040000DB RID: 219
		private EntryIdMap<FolderMapping> crawledFolders;
	}
}
