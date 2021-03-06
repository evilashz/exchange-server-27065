using System;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Infrastructure;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model;
using Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Task
{
	// Token: 0x02000057 RID: 87
	internal class MailboxInfoCreation : SearchTask<SearchSource>
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00017FD8 File Offset: 0x000161D8
		public MailboxInfoCreation.MailboxInfoCreationContext TaskContext
		{
			get
			{
				return (MailboxInfoCreation.MailboxInfoCreationContext)base.Context.TaskContext;
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00017FEC File Offset: 0x000161EC
		public override void Process(SearchSource item)
		{
			Recorder.Trace(4L, TraceType.InfoTrace, new object[]
			{
				"MailboxInfoCreation.Process Item:",
				item,
				"SourceType:",
				item.SourceType,
				"SourceLocation:",
				item.SourceLocation,
				"SuppressDuplicates:",
				this.TaskContext.SuppressDuplicates
			});
			bool flag = false;
			if (item != null)
			{
				if (item.SourceLocation == SourceLocation.All || item.SourceLocation == SourceLocation.PrimaryOnly)
				{
					flag = true;
					item.MailboxInfo = new MailboxInfo(item.Recipient.ADEntry, MailboxType.Primary)
					{
						Folder = item.FolderSpec,
						SourceMailbox = item
					};
					if (!this.IsDuplicate(item) && (item.MailboxInfo.MdbGuid != Guid.Empty || item.MailboxInfo.IsRemoteMailbox))
					{
						base.Executor.EnqueueNext(item);
					}
					else if (item.MailboxInfo.MdbGuid == Guid.Empty)
					{
						Recorder.Trace(4L, TraceType.InfoTrace, new object[]
						{
							"MailboxInfoCreation.Process Ignoring primary mailbox:",
							item.ReferenceId,
							"Primary database is empty and mailbox is not remote"
						});
					}
					else
					{
						Recorder.Trace(4L, TraceType.WarningTrace, new object[]
						{
							"MailboxInfoCreation.Process Duplicate:",
							item.ReferenceId,
							"SourceType:",
							item.SourceType
						});
					}
				}
				if (item.SourceLocation == SourceLocation.All || item.SourceLocation == SourceLocation.ArchiveOnly)
				{
					if (flag)
					{
						item = item.Clone();
					}
					item.MailboxInfo = new MailboxInfo(item.Recipient.ADEntry, MailboxType.Archive)
					{
						Folder = item.FolderSpec,
						SourceMailbox = item
					};
					if (!this.IsDuplicate(item) && (item.MailboxInfo.ArchiveDatabase != Guid.Empty || item.MailboxInfo.IsRemoteMailbox || item.MailboxInfo.IsCloudArchive) && item.MailboxInfo.ArchiveGuid != Guid.Empty)
					{
						base.Executor.EnqueueNext(item);
					}
					else if (item.MailboxInfo.ArchiveDatabase == Guid.Empty)
					{
						Recorder.Trace(4L, TraceType.InfoTrace, new object[]
						{
							"MailboxInfoCreation.Process Ignoring archive mailbox:",
							item.ReferenceId,
							"Archive database is empty and mailbox is not remote"
						});
					}
					else
					{
						Recorder.Trace(4L, TraceType.WarningTrace, new object[]
						{
							"MailboxInfoCreation.Process Duplicate:",
							item.ReferenceId,
							"SourceType:",
							item.SourceType
						});
					}
				}
				if (this.TaskContext.SuppressDuplicates && this.TaskContext.MaximumItems < this.TaskContext.DupeCheck.Count)
				{
					Recorder.Trace(4L, TraceType.WarningTrace, new object[]
					{
						"MailboxInfoCreation.Process Failed TooManySources Count:",
						this.TaskContext.DupeCheck.Count,
						"Limit:",
						this.TaskContext.MaximumItems
					});
					throw new SearchException(KnownError.TooManyMailboxesException, new object[]
					{
						this.TaskContext.DupeCheck.Count,
						this.TaskContext.MaximumItems
					});
				}
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00018360 File Offset: 0x00016560
		private bool IsDuplicate(SearchSource source)
		{
			if (this.TaskContext.SuppressDuplicates)
			{
				string text = string.Format("{0}{1}{2}{3}", new object[]
				{
					source.MailboxInfo.DisplayName,
					source.MailboxInfo.ExchangeGuid,
					source.MailboxInfo.IsArchive,
					source.MailboxInfo.Folder
				});
				if (this.TaskContext.DupeCheck.Contains(text))
				{
					return true;
				}
				this.TaskContext.DupeCheck.Add(text);
			}
			return false;
		}

		// Token: 0x02000058 RID: 88
		internal class MailboxInfoCreationContext
		{
			// Token: 0x17000125 RID: 293
			// (get) Token: 0x060003A6 RID: 934 RVA: 0x000183FE File Offset: 0x000165FE
			// (set) Token: 0x060003A7 RID: 935 RVA: 0x00018406 File Offset: 0x00016606
			public bool SuppressDuplicates { get; set; }

			// Token: 0x17000126 RID: 294
			// (get) Token: 0x060003A8 RID: 936 RVA: 0x0001840F File Offset: 0x0001660F
			// (set) Token: 0x060003A9 RID: 937 RVA: 0x00018417 File Offset: 0x00016617
			public int MaximumItems { get; set; }

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x060003AA RID: 938 RVA: 0x00018420 File Offset: 0x00016620
			internal ConcurrentBag<string> DupeCheck
			{
				get
				{
					return this.dupeCheck;
				}
			}

			// Token: 0x04000198 RID: 408
			private ConcurrentBag<string> dupeCheck = new ConcurrentBag<string>();
		}
	}
}
