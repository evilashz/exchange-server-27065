using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreIntegrityCheck;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000004 RID: 4
	public class AggregateCountsCheckTask : IntegrityCheckTaskBase
	{
		// Token: 0x06000014 RID: 20 RVA: 0x0000303F File Offset: 0x0000123F
		public AggregateCountsCheckTask(IJobExecutionTracker tracker) : base(tracker)
		{
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00003048 File Offset: 0x00001248
		public override string TaskName
		{
			get
			{
				return "AggregateCounts";
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00003050 File Offset: 0x00001250
		public override ErrorCode ExecuteOneFolder(Mailbox mailbox, MailboxEntry mailboxEntry, FolderEntry folderEntry, bool detectOnly, Func<bool> shouldContinue)
		{
			if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.OnlineIsintegTracer.TraceDebug<string, string>(0L, "Execute task {0} on folder {1}", this.TaskName, folderEntry.ToString());
			}
			this.currentMailbox = mailboxEntry;
			this.currentFolder = folderEntry;
			ErrorCode errorCode = ErrorCode.NoError;
			Context currentOperationContext = mailbox.CurrentOperationContext;
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(currentOperationContext.Database);
			using (Folder folder = Folder.OpenFolder(currentOperationContext, mailbox, folderEntry.FolderId))
			{
				List<AggregateCountsCheckTask.IAggregateFolderCounter> aggregationProperties = this.SetupAggregationProperties(currentOperationContext, folder, messageTable);
				errorCode = this.GetAggregateCounters(mailbox, folderEntry.FolderId, aggregationProperties, shouldContinue);
				if (errorCode != ErrorCode.NoError)
				{
					if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.OnlineIsintegTracer.TraceError<string, ErrorCode>(0L, "Unexpected error when reading aggregate counters in folder {0}, error code {1}", folderEntry.ToString(), errorCode);
					}
					return errorCode.Propagate((LID)2391158077U);
				}
				if (!shouldContinue())
				{
					if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.OnlineIsintegTracer.TraceError(0L, "Task aborted");
					}
					return ErrorCode.CreateExiting((LID)3464899901U);
				}
				errorCode = this.ReportAndFixCorruption(mailbox, folder, aggregationProperties, detectOnly, shouldContinue);
				if (errorCode != ErrorCode.NoError)
				{
					if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.OnlineIsintegTracer.TraceError<string, ErrorCode>(0L, "Unexpected error when fixing corruption in folder {0}, error code {1}", folderEntry.ToString(), errorCode);
					}
					return errorCode.Propagate((LID)2995137853U);
				}
			}
			return ErrorCode.NoError;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000033B8 File Offset: 0x000015B8
		private List<AggregateCountsCheckTask.IAggregateFolderCounter> SetupAggregationProperties(Context context, Folder folder, MessageTable messageTable)
		{
			List<AggregateCountsCheckTask.IAggregateFolderCounter> list = new List<AggregateCountsCheckTask.IAggregateFolderCounter>
			{
				new AggregateCountsCheckTask.AggregateFolderCounter<long>(folder.FolderTable.MessageCount, new Column[]
				{
					messageTable.IsHidden
				}, (Reader reader, long origValue) => origValue + (reader.GetBoolean(messageTable.IsHidden) ? 0L : 1L)),
				new AggregateCountsCheckTask.AggregateFolderCounter<long>(folder.FolderTable.HiddenItemCount, new Column[]
				{
					messageTable.IsHidden
				}, (Reader reader, long origValue) => origValue + (reader.GetBoolean(messageTable.IsHidden) ? 1L : 0L)),
				new AggregateCountsCheckTask.AggregateFolderCounter<long>(folder.FolderTable.MessageHasAttachCount, new Column[]
				{
					messageTable.IsHidden,
					messageTable.HasAttachments
				}, delegate(Reader reader, long origValue)
				{
					bool boolean = reader.GetBoolean(messageTable.IsHidden);
					bool boolean2 = reader.GetBoolean(messageTable.HasAttachments);
					return origValue + ((boolean2 && !boolean) ? 1L : 0L);
				}),
				new AggregateCountsCheckTask.AggregateFolderCounter<long>(folder.FolderTable.HiddenItemHasAttachCount, new Column[]
				{
					messageTable.IsHidden,
					messageTable.HasAttachments
				}, delegate(Reader reader, long origValue)
				{
					bool boolean = reader.GetBoolean(messageTable.IsHidden);
					bool boolean2 = reader.GetBoolean(messageTable.HasAttachments);
					return origValue + ((boolean2 && boolean) ? 1L : 0L);
				}),
				new AggregateCountsCheckTask.AggregateFolderCounter<long>(folder.FolderTable.MessageSize, new Column[]
				{
					messageTable.IsHidden,
					messageTable.Size
				}, (Reader reader, long origValue) => origValue + (reader.GetBoolean(messageTable.IsHidden) ? 0L : reader.GetInt64(messageTable.Size))),
				new AggregateCountsCheckTask.AggregateFolderCounter<long>(folder.FolderTable.HiddenItemSize, new Column[]
				{
					messageTable.IsHidden,
					messageTable.Size
				}, (Reader reader, long origValue) => origValue + (reader.GetBoolean(messageTable.IsHidden) ? reader.GetInt64(messageTable.Size) : 0L))
			};
			if (!folder.IsPerUserReadUnreadTrackingEnabled)
			{
				List<AggregateCountsCheckTask.IAggregateFolderCounter> list2 = list;
				List<AggregateCountsCheckTask.IAggregateFolderCounter> list3 = new List<AggregateCountsCheckTask.IAggregateFolderCounter>();
				list3.Add(new AggregateCountsCheckTask.AggregateFolderCounter<long>(folder.FolderTable.UnreadMessageCount, new Column[]
				{
					messageTable.IsHidden,
					messageTable.IsRead
				}, delegate(Reader reader, long origValue)
				{
					bool boolean = reader.GetBoolean(messageTable.IsHidden);
					bool flag = !reader.GetBoolean(messageTable.IsRead);
					return origValue + ((flag && !boolean) ? 1L : 0L);
				}));
				list3.Add(new AggregateCountsCheckTask.AggregateFolderCounter<long>(folder.FolderTable.UnreadHiddenItemCount, new Column[]
				{
					messageTable.IsHidden,
					messageTable.IsRead
				}, delegate(Reader reader, long origValue)
				{
					bool boolean = reader.GetBoolean(messageTable.IsHidden);
					bool flag = !reader.GetBoolean(messageTable.IsRead);
					return origValue + ((flag && boolean) ? 1L : 0L);
				}));
				list2.AddRange(list3);
			}
			else
			{
				ExTraceGlobals.OnlineIsintegTracer.TraceDebug<string, string>(0L, "Execute task {0} skipping folder {1} with no PerUserReadUnreadTrackingEnabled", this.TaskName, folder.GetName(context));
			}
			return list;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000036D0 File Offset: 0x000018D0
		private ErrorCode GetAggregateCounters(Mailbox mailbox, ExchangeId folderId, List<AggregateCountsCheckTask.IAggregateFolderCounter> aggregationProperties, Func<bool> shouldContinue)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			Context currentOperationContext = mailbox.CurrentOperationContext;
			MessagePropValueGetter messagePropValueGetter = new MessagePropValueGetter(currentOperationContext, mailbox.MailboxNumber, folderId);
			if (!shouldContinue())
			{
				if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.OnlineIsintegTracer.TraceError(0L, "Task aborted");
				}
				return ErrorCode.CreateExiting((LID)4068879677U);
			}
			Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(currentOperationContext.Database);
			HashSet<Column> hashSet = new HashSet<Column>();
			foreach (AggregateCountsCheckTask.IAggregateFolderCounter aggregateFolderCounter in aggregationProperties)
			{
				hashSet.UnionWith(aggregateFolderCounter.ColumnsToFetch());
			}
			errorCode = messagePropValueGetter.Execute(false, hashSet.ToArray<Column>(), delegate(Reader reader)
			{
				foreach (AggregateCountsCheckTask.IAggregateFolderCounter aggregateFolderCounter2 in aggregationProperties)
				{
					aggregateFolderCounter2.UpdateAggregation(reader);
				}
				return ErrorCode.NoError;
			}, shouldContinue);
			if (errorCode != ErrorCode.NoError)
			{
				if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.OnlineIsintegTracer.TraceError<ErrorCode>(0L, "Unexpected error when check messages for aggregation, error code {0}", errorCode);
				}
				return errorCode.Propagate((LID)2458266941U);
			}
			return ErrorCode.NoError;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00003800 File Offset: 0x00001A00
		private ErrorCode ReportAndFixCorruption(Mailbox mailbox, Folder folder, List<AggregateCountsCheckTask.IAggregateFolderCounter> aggregationProperties, bool detectOnly, Func<bool> shouldContinue)
		{
			Context currentOperationContext = mailbox.CurrentOperationContext;
			bool flag = false;
			foreach (AggregateCountsCheckTask.IAggregateFolderCounter aggregateFolderCounter in aggregationProperties)
			{
				string text;
				flag |= aggregateFolderCounter.ReportAndFixCorruption(currentOperationContext, mailbox, folder, detectOnly, out text);
				if (text != null)
				{
					base.ReportCorruption(text, this.currentMailbox, this.currentFolder, null, CorruptionType.AggregateCountMismatch, flag);
				}
				if (!shouldContinue())
				{
					if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.OnlineIsintegTracer.TraceError(0L, "Task aborted");
					}
					return ErrorCode.CreateExiting((LID)3532008765U);
				}
			}
			if (flag)
			{
				folder.Save(currentOperationContext);
			}
			return ErrorCode.NoError;
		}

		// Token: 0x04000006 RID: 6
		private MailboxEntry currentMailbox;

		// Token: 0x04000007 RID: 7
		private FolderEntry currentFolder;

		// Token: 0x02000005 RID: 5
		public interface IAggregateFolderCounter
		{
			// Token: 0x0600001A RID: 26
			void UpdateAggregation(Reader reader);

			// Token: 0x0600001B RID: 27
			Column[] ColumnsToFetch();

			// Token: 0x0600001C RID: 28
			bool ReportAndFixCorruption(Context context, Mailbox mailbox, Folder folder, bool detectOnly, out string corruptionInfo);
		}

		// Token: 0x02000006 RID: 6
		private class AggregateFolderCounter<ColT> : AggregateCountsCheckTask.IAggregateFolderCounter where ColT : IComparable<ColT>
		{
			// Token: 0x0600001D RID: 29 RVA: 0x000038C8 File Offset: 0x00001AC8
			public AggregateFolderCounter(PhysicalColumn column, Column[] columnsToFetch, Func<Reader, ColT, ColT> aggregateGetter)
			{
				this.column = column;
				this.columnsToFetch = columnsToFetch;
				this.aggregateGetter = aggregateGetter;
			}

			// Token: 0x0600001E RID: 30 RVA: 0x000038E5 File Offset: 0x00001AE5
			public virtual void UpdateAggregation(Reader reader)
			{
				this.currentValue = this.aggregateGetter(reader, this.currentValue);
			}

			// Token: 0x0600001F RID: 31 RVA: 0x000038FF File Offset: 0x00001AFF
			public virtual Column[] ColumnsToFetch()
			{
				return this.columnsToFetch;
			}

			// Token: 0x06000020 RID: 32 RVA: 0x00003908 File Offset: 0x00001B08
			public virtual bool ReportAndFixCorruption(Context context, Mailbox mailbox, Folder folder, bool detectOnly, out string corruptionInfo)
			{
				bool result = false;
				ColT colT = (ColT)((object)folder.GetColumnValue(context, this.column));
				corruptionInfo = null;
				if (colT.CompareTo(this.currentValue) != 0)
				{
					if (!detectOnly)
					{
						folder.SetColumn(context, this.column, this.currentValue);
						result = true;
					}
					corruptionInfo = string.Format("Column {0}:{1} -> {2}", this.column.Name, colT, this.currentValue);
				}
				return result;
			}

			// Token: 0x04000008 RID: 8
			private ColT currentValue;

			// Token: 0x04000009 RID: 9
			private PhysicalColumn column;

			// Token: 0x0400000A RID: 10
			private Column[] columnsToFetch;

			// Token: 0x0400000B RID: 11
			private Func<Reader, ColT, ColT> aggregateGetter;
		}
	}
}
