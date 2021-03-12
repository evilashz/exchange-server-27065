using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000041 RID: 65
	internal class MessageRecSorter
	{
		// Token: 0x0600034D RID: 845 RVA: 0x0001538D File Offset: 0x0001358D
		public MessageRecSorter()
		{
			this.maxFolderTolerance = MessageRecSorter.DefaultFolderTolerance;
			this.minBatchSize = ConfigBase<MRSConfigSchema>.GetConfig<int>("MinBatchSize");
			this.minBatchByteSize = ConfigBase<MRSConfigSchema>.GetConfig<ulong>("MinBatchSizeKB") * 1024UL;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000153C8 File Offset: 0x000135C8
		public Queue<List<MessageRec>> Sort(List<MessageRec> messagesToCopy, MessageRecSortBy sortBy)
		{
			if (!sortBy.Equals(MessageRecSortBy.SkipSort))
			{
				MrsTracer.Service.Debug("Sorting input msgRec list ({0} entries) by timestamp.", new object[]
				{
					messagesToCopy.Count
				});
				messagesToCopy.Sort(sortBy.Equals(MessageRecSortBy.DescendingTimeStamp) ? MessageRecComparer.DescendingComparer : MessageRecComparer.Comparer);
			}
			MrsTracer.Service.Debug("Splitting sorted list into batches.", new object[0]);
			this.outputQueue = new Queue<List<MessageRec>>();
			this.currentBatch = null;
			this.currentBatchByteSize = 0UL;
			Queue<FolderBatch> queue = new Queue<FolderBatch>();
			using (List<MessageRec>.Enumerator enumerator = messagesToCopy.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MessageRec messageRec = enumerator.Current;
					if (!sortBy.Equals(MessageRecSortBy.SkipSort))
					{
						DateTime dateTime = messageRec.CreationTimestamp;
						if (sortBy.Equals(MessageRecSortBy.DescendingTimeStamp))
						{
							if (dateTime < DateTime.MaxValue - this.maxFolderTolerance)
							{
								dateTime += this.maxFolderTolerance;
							}
							else
							{
								dateTime = DateTime.MaxValue;
							}
						}
						else if (dateTime > DateTime.MinValue + this.maxFolderTolerance)
						{
							dateTime -= this.maxFolderTolerance;
						}
						else
						{
							dateTime = DateTime.MinValue;
						}
						while (queue.Count > 0 && (sortBy.Equals(MessageRecSortBy.DescendingTimeStamp) ? (queue.Peek().HeadTimestamp > dateTime) : (queue.Peek().HeadTimestamp < dateTime)))
						{
							this.FlushBatch(queue.Dequeue());
						}
					}
					FolderBatch folderBatch = null;
					foreach (FolderBatch folderBatch2 in queue)
					{
						if (CommonUtils.IsSameEntryId(folderBatch2.FolderId, messageRec.FolderId))
						{
							folderBatch = folderBatch2;
							break;
						}
					}
					if (folderBatch == null)
					{
						folderBatch = new FolderBatch(messageRec.FolderId);
						queue.Enqueue(folderBatch);
					}
					folderBatch.AddMsg(messageRec);
					if (folderBatch.Batch.Count >= this.minBatchSize || folderBatch.BatchByteSize >= this.minBatchByteSize)
					{
						FolderBatch folderBatch3;
						do
						{
							folderBatch3 = queue.Dequeue();
							this.FlushBatch(folderBatch3);
						}
						while (folderBatch3 != folderBatch);
					}
				}
				goto IL_248;
			}
			IL_23C:
			this.FlushBatch(queue.Dequeue());
			IL_248:
			if (queue.Count <= 0)
			{
				if (this.currentBatch != null)
				{
					this.outputQueue.Enqueue(this.currentBatch);
					this.currentBatch = null;
					this.currentBatchByteSize = 0UL;
				}
				if (MrsTracer.Service.IsEnabled(TraceType.DebugTrace))
				{
					int num = 0;
					ulong num2 = 0UL;
					foreach (List<MessageRec> list in this.outputQueue)
					{
						num += list.Count;
						foreach (MessageRec messageRec2 in list)
						{
							num2 += (ulong)((long)messageRec2.MessageSize);
						}
					}
					ExAssert.RetailAssert(num == messagesToCopy.Count, "We should not have lost any messages");
					int num3 = (this.outputQueue.Count > 0) ? (num / this.outputQueue.Count) : 0;
					ulong byteValue = (this.outputQueue.Count > 0) ? (num2 / (ulong)((long)this.outputQueue.Count)) : 0UL;
					MrsTracer.Service.Debug("Created {0} batches, avg batch size {1}, avg batch byte size {2}, total message byte size {3}", new object[]
					{
						this.outputQueue.Count,
						num3,
						new ByteQuantifiedSize(byteValue).ToString(),
						new ByteQuantifiedSize(num2).ToString()
					});
				}
				return this.outputQueue;
			}
			goto IL_23C;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0001580C File Offset: 0x00013A0C
		private void FlushBatch(FolderBatch folderBatch)
		{
			if (this.currentBatch == null)
			{
				this.currentBatch = folderBatch.Batch;
				this.currentBatchByteSize = folderBatch.BatchByteSize;
			}
			else
			{
				this.currentBatch.AddRange(folderBatch.Batch);
				this.currentBatchByteSize += folderBatch.BatchByteSize;
			}
			if (this.currentBatch.Count >= this.minBatchSize || this.currentBatchByteSize >= this.minBatchByteSize)
			{
				this.outputQueue.Enqueue(this.currentBatch);
				this.currentBatch = null;
				this.currentBatchByteSize = 0UL;
			}
		}

		// Token: 0x0400014E RID: 334
		public static readonly TimeSpan DefaultFolderTolerance = TimeSpan.FromDays(1.0);

		// Token: 0x0400014F RID: 335
		private readonly TimeSpan maxFolderTolerance;

		// Token: 0x04000150 RID: 336
		private readonly int minBatchSize;

		// Token: 0x04000151 RID: 337
		private readonly ulong minBatchByteSize;

		// Token: 0x04000152 RID: 338
		private Queue<List<MessageRec>> outputQueue;

		// Token: 0x04000153 RID: 339
		private List<MessageRec> currentBatch;

		// Token: 0x04000154 RID: 340
		private ulong currentBatchByteSize;
	}
}
