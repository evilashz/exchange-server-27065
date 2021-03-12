using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000005 RID: 5
	internal sealed class FolderContentsCrawler : DisposableWrapper<ISourceFolder>
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000030DC File Offset: 0x000012DC
		internal FolderContentsCrawler(ISourceFolder sourceFolder, int pageSize, int maxPageSize) : base(sourceFolder, true)
		{
			ArgumentValidator.ThrowIfZeroOrNegative("pageSize", pageSize);
			ArgumentValidator.ThrowIfZeroOrNegative("maxPageSize", maxPageSize);
			this.pageSize = pageSize;
			this.maxPageSize = maxPageSize;
			this.pageQueue = new Queue<List<MessageRec>>(maxPageSize / pageSize + 1);
			this.HasMoreMessages = true;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000312C File Offset: 0x0000132C
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00003134 File Offset: 0x00001334
		public bool HasMoreMessages { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000313D File Offset: 0x0000133D
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00003145 File Offset: 0x00001345
		public int TotalMessageCount { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000314E File Offset: 0x0000134E
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00003156 File Offset: 0x00001356
		internal MailboxCopierBase MailboxCopier { get; set; }

		// Token: 0x06000022 RID: 34 RVA: 0x00003160 File Offset: 0x00001360
		internal IReadOnlyCollection<MessageRec> GetMessagesNextPage()
		{
			MrsTracer.Service.Function("FolderContentsCrawler.GetMessagesNextPage", new object[0]);
			base.CheckDisposed();
			bool flag = false;
			while (this.pageQueue.Count <= 0)
			{
				List<MessageRec> list = base.WrappedObject.EnumerateMessagesPaged(this.maxPageSize);
				if (this.MailboxCopier != null)
				{
					this.MailboxCopier.ICSSyncState.ProviderState = this.MailboxCopier.SourceMailbox.GetMailboxSyncState();
					this.MailboxCopier.SaveICSSyncState(true);
				}
				if (list == null)
				{
					MrsTracer.Service.Debug("No more messages", new object[0]);
				}
				else
				{
					if (this.TotalMessageCount == 0)
					{
						this.TotalMessageCount = base.WrappedObject.GetEstimatedItemCount();
					}
					MrsTracer.Service.Debug("Prepare {0} messages into {1} messages/page", new object[]
					{
						list.Count,
						this.pageSize
					});
					using (IEnumerator<MessageRec> enumerator = list.GetEnumerator())
					{
						bool flag2 = false;
						while (!flag2)
						{
							List<MessageRec> list2 = new List<MessageRec>(this.pageSize);
							for (int i = 0; i < this.pageSize; i++)
							{
								if (!enumerator.MoveNext())
								{
									flag2 = true;
									break;
								}
								list2.Add(enumerator.Current);
							}
							if (list2.Count > 0)
							{
								this.pageQueue.Enqueue(list2);
							}
						}
					}
					flag = !flag;
					if (flag)
					{
						continue;
					}
				}
				this.HasMoreMessages = false;
				return Array<MessageRec>.Empty;
			}
			List<MessageRec> list3 = this.pageQueue.Dequeue();
			MrsTracer.Service.Debug("Return {0} messages to copy", new object[]
			{
				list3.Count
			});
			return list3;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000331C File Offset: 0x0000151C
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.pageQueue != null)
			{
				this.pageQueue.Clear();
				this.pageQueue = null;
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x04000009 RID: 9
		private readonly int pageSize;

		// Token: 0x0400000A RID: 10
		private readonly int maxPageSize;

		// Token: 0x0400000B RID: 11
		private Queue<List<MessageRec>> pageQueue;
	}
}
