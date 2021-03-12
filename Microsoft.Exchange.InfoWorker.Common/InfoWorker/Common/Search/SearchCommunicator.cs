using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x02000232 RID: 562
	internal class SearchCommunicator
	{
		// Token: 0x06000F78 RID: 3960 RVA: 0x00044800 File Offset: 0x00042A00
		internal SearchCommunicator()
		{
			this.progressEvent = new AutoResetEvent(false);
			this.abortEvent = new ManualResetEvent(false);
			this.workerExceptions = new List<Pair<int, Exception>>();
			this.workerLogs = new List<StreamLogItem.LogItem>();
			this.completedWorkers = new List<SearchMailboxWorker>();
			this.processedMessages = new HashSet<UniqueItemHash>();
			this.processedMessageIds = new HashSet<string>();
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x00044864 File Offset: 0x00042A64
		internal SearchCommunicator(HashSet<UniqueItemHash> processedMessages, HashSet<string> processedMessageIds)
		{
			this.progressEvent = new AutoResetEvent(false);
			this.abortEvent = new ManualResetEvent(false);
			this.workerExceptions = new List<Pair<int, Exception>>();
			this.workerLogs = new List<StreamLogItem.LogItem>();
			this.completedWorkers = new List<SearchMailboxWorker>();
			this.processedMessages = processedMessages;
			this.processedMessageIds = processedMessageIds;
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x000448BE File Offset: 0x00042ABE
		internal bool IsAborted
		{
			get
			{
				return this.abortEvent.WaitOne(0, false);
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x000448CD File Offset: 0x00042ACD
		// (set) Token: 0x06000F7C RID: 3964 RVA: 0x000448D5 File Offset: 0x00042AD5
		internal List<Pair<int, Exception>> WorkerExceptions
		{
			get
			{
				return this.workerExceptions;
			}
			set
			{
				this.workerExceptions = value;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x000448DE File Offset: 0x00042ADE
		internal List<StreamLogItem.LogItem> WorkerLogs
		{
			get
			{
				return this.workerLogs;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x000448E6 File Offset: 0x00042AE6
		internal List<SearchMailboxWorker> CompletedWorkers
		{
			get
			{
				return this.completedWorkers;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x000448EE File Offset: 0x00042AEE
		internal AutoResetEvent ProgressEvent
		{
			get
			{
				return this.progressEvent;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x000448F6 File Offset: 0x00042AF6
		internal ManualResetEvent AbortEvent
		{
			get
			{
				return this.abortEvent;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x000448FE File Offset: 0x00042AFE
		// (set) Token: 0x06000F82 RID: 3970 RVA: 0x00044906 File Offset: 0x00042B06
		internal double OverallProgress { get; set; }

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x0004490F File Offset: 0x00042B0F
		// (set) Token: 0x06000F84 RID: 3972 RVA: 0x00044917 File Offset: 0x00042B17
		internal long OverallResultItems { get; set; }

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x00044920 File Offset: 0x00042B20
		// (set) Token: 0x06000F86 RID: 3974 RVA: 0x00044928 File Offset: 0x00042B28
		internal ByteQuantifiedSize OverallResultSize { get; set; }

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x00044931 File Offset: 0x00042B31
		// (set) Token: 0x06000F88 RID: 3976 RVA: 0x00044939 File Offset: 0x00042B39
		internal int ProgressingWorkers
		{
			get
			{
				return this.progressingWorkers;
			}
			set
			{
				this.progressingWorkers = value;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x00044942 File Offset: 0x00042B42
		internal HashSet<UniqueItemHash> ProcessedMessages
		{
			get
			{
				return this.processedMessages;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x0004494A File Offset: 0x00042B4A
		internal HashSet<string> ProcessedMessageIds
		{
			get
			{
				return this.processedMessageIds;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x00044952 File Offset: 0x00042B52
		// (set) Token: 0x06000F8C RID: 3980 RVA: 0x0004495A File Offset: 0x00042B5A
		internal MultiValuedProperty<string> SuccessfulMailboxes { get; set; }

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x00044963 File Offset: 0x00042B63
		// (set) Token: 0x06000F8E RID: 3982 RVA: 0x0004496B File Offset: 0x00042B6B
		internal MultiValuedProperty<string> UnsuccessfulMailboxes { get; set; }

		// Token: 0x06000F8F RID: 3983 RVA: 0x00044974 File Offset: 0x00042B74
		internal void ReportException(int senderId, Exception e)
		{
			this.WorkerExceptions.Add(new Pair<int, Exception>(senderId, e));
			this.ProgressEvent.Set();
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x00044994 File Offset: 0x00042B94
		internal void UpdateProgress(SearchMailboxWorker worker)
		{
			int workerId = worker.WorkerId;
			if (!worker.ExcludeDuplicateMessages)
			{
				int num = worker.SearchResult.ResultItemsCount - this.workerResultItems[workerId];
				this.workerResultItems[workerId] = worker.SearchResult.ResultItemsCount;
				this.OverallResultItems += (long)num;
				ByteQuantifiedSize value = worker.SearchResult.ResultItemsSize - this.workerResultSize[workerId];
				this.workerResultSize[workerId] = worker.SearchResult.ResultItemsSize;
				this.OverallResultSize += value;
			}
			else if (worker.TargetMailbox != null && worker.TargetSubFolderId != null)
			{
				this.UpdateResults(worker.TargetMailbox, worker.TargetSubFolderId);
			}
			double num2 = (worker.CurrentProgress - this.workerProgresses[workerId]) / (double)this.workerProgresses.Length;
			if (num2 > 0.0)
			{
				this.workerProgresses[workerId] = worker.CurrentProgress;
				this.OverallProgress += num2;
				if (this.OverallProgress > 100.0)
				{
					this.OverallProgress = 100.0;
				}
				this.ProgressEvent.Set();
			}
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x00044ACC File Offset: 0x00042CCC
		internal void UpdateResults(MailboxSession targetMailbox, StoreId targetFolder)
		{
			int num = 0;
			ByteQuantifiedSize zero = ByteQuantifiedSize.Zero;
			SearchUtils.GetFolderItemsCountAndSize(targetMailbox, targetFolder, out num, out zero);
			this.OverallResultItems = (long)num;
			this.OverallResultSize = zero;
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00044AFC File Offset: 0x00042CFC
		internal void ResetWorker(SearchMailboxWorker worker, bool done)
		{
			int workerId = worker.WorkerId;
			this.OverallProgress -= this.workerProgresses[workerId] / (double)this.workerProgresses.Length;
			this.workerProgresses[workerId] = 0.0;
			if (this.OverallProgress < 0.0)
			{
				this.OverallProgress = 0.0;
			}
			if (!worker.ExcludeDuplicateMessages)
			{
				if (worker.SearchResult.ResultItemsCount == this.workerResultItems[workerId])
				{
					this.OverallResultItems -= (long)worker.SearchResult.ResultItemsCount;
				}
				worker.SearchResult.ResultItemsCount = 0;
				this.workerResultItems[workerId] = 0;
				if (worker.SearchResult.ResultItemsSize == this.workerResultSize[workerId])
				{
					this.OverallResultSize -= worker.SearchResult.ResultItemsSize;
				}
				worker.SearchResult.ResultItemsSize = ByteQuantifiedSize.Zero;
				this.workerResultSize[workerId] = ByteQuantifiedSize.Zero;
			}
			if (!done)
			{
				this.ProgressingWorkers++;
			}
			worker.LastException = null;
			worker.TargetMailbox = null;
			this.ProgressEvent.Set();
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00044C3E File Offset: 0x00042E3E
		internal void ReportCompletion(SearchMailboxWorker worker)
		{
			this.ProgressingWorkers--;
			this.CompletedWorkers.Add(worker);
			this.ProgressEvent.Set();
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x00044C88 File Offset: 0x00042E88
		internal void ReportLogs(StreamLogItem.LogItem logItem)
		{
			this.WorkerLogs.Add(logItem);
			int totalLogEntries = 0;
			this.WorkerLogs.ForEach(delegate(StreamLogItem.LogItem x)
			{
				totalLogEntries += x.Logs.Count<LocalizedString>();
			});
			if (totalLogEntries > 1000)
			{
				this.ProgressEvent.Set();
			}
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x00044CDE File Offset: 0x00042EDE
		internal void Abort()
		{
			this.AbortEvent.Set();
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x00044CEC File Offset: 0x00042EEC
		internal void Reset(int workers)
		{
			this.ProgressingWorkers = workers;
			this.workerProgresses = new double[workers];
			this.workerResultItems = new int[workers];
			this.workerResultSize = new ByteQuantifiedSize[workers];
			for (int i = 0; i < this.workerProgresses.Length; i++)
			{
				this.workerProgresses[i] = 0.0;
				this.workerResultItems[i] = 0;
				this.workerResultSize[i] = ByteQuantifiedSize.Zero;
			}
			this.OverallProgress = 0.0;
			this.OverallResultItems = 0L;
			this.OverallResultSize = ByteQuantifiedSize.Zero;
			this.ProgressEvent.Reset();
			this.WorkerLogs.Clear();
			this.WorkerExceptions.Clear();
			this.CompletedWorkers.Clear();
			this.ProcessedMessages.Clear();
			this.processedMessageIds.Clear();
		}

		// Token: 0x04000A94 RID: 2708
		private const int LogBufferCapacity = 1000;

		// Token: 0x04000A95 RID: 2709
		private double[] workerProgresses;

		// Token: 0x04000A96 RID: 2710
		private int[] workerResultItems;

		// Token: 0x04000A97 RID: 2711
		private ByteQuantifiedSize[] workerResultSize;

		// Token: 0x04000A98 RID: 2712
		private int progressingWorkers;

		// Token: 0x04000A99 RID: 2713
		private AutoResetEvent progressEvent;

		// Token: 0x04000A9A RID: 2714
		private ManualResetEvent abortEvent;

		// Token: 0x04000A9B RID: 2715
		private List<Pair<int, Exception>> workerExceptions;

		// Token: 0x04000A9C RID: 2716
		private List<StreamLogItem.LogItem> workerLogs;

		// Token: 0x04000A9D RID: 2717
		private List<SearchMailboxWorker> completedWorkers;

		// Token: 0x04000A9E RID: 2718
		private HashSet<UniqueItemHash> processedMessages;

		// Token: 0x04000A9F RID: 2719
		private HashSet<string> processedMessageIds;
	}
}
