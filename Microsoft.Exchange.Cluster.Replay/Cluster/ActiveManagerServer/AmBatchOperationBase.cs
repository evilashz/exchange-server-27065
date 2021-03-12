using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000007 RID: 7
	internal abstract class AmBatchOperationBase
	{
		// Token: 0x0600003E RID: 62 RVA: 0x0000272F File Offset: 0x0000092F
		internal AmBatchOperationBase()
		{
			this.CustomStatus = AmDbActionStatus.None;
			this.m_amConfig = AmSystemManager.Instance.Config;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002764 File Offset: 0x00000964
		// (set) Token: 0x06000040 RID: 64 RVA: 0x0000276C File Offset: 0x0000096C
		internal BatchOperationCompletedDelegate CompletionCallback { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002775 File Offset: 0x00000975
		// (set) Token: 0x06000042 RID: 66 RVA: 0x0000277D File Offset: 0x0000097D
		internal AmDbActionStatus CustomStatus { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002788 File Offset: 0x00000988
		internal bool IsAllDone
		{
			get
			{
				bool isAllDone;
				lock (this.m_locker)
				{
					isAllDone = this.m_isAllDone;
				}
				return isAllDone;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000027CC File Offset: 0x000009CC
		internal void MarkAllDone()
		{
			lock (this.m_locker)
			{
				AmTrace.Debug("{0} finished processing all the databases", new object[]
				{
					base.GetType().Name
				});
				if (!this.m_isAllDone)
				{
					this.m_isAllDone = true;
					this.LogCompletionInternal();
					if (this.CompletionCallback != null)
					{
						this.CompletionCallback(new List<AmDbOperation>(this.m_opList));
					}
					ThreadPoolThreadCountHelper.Reset();
				}
				else
				{
					AmTrace.Debug("IsAllDone() is already marked and completionback was already called. Possibly custom status is reached already.", new object[0]);
				}
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002878 File Offset: 0x00000A78
		internal void Run()
		{
			AmTrace.Entering("AmBatchOperationBase.Run()", new object[0]);
			if (RegistryParameters.AmDisableBatchOperations)
			{
				ReplayCrimsonEvents.BatchMounterOperationsDisabled.Log();
				AmTrace.Leaving("AmBatchOperationBase.Run(), BatchMounterOperationsDisabled", new object[0]);
				return;
			}
			lock (this.m_locker)
			{
				try
				{
					this.m_isDebugOptionEnabled = this.m_amConfig.IsDebugOptionsEnabled();
					this.LogStartupInternal();
					Exception ex = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
					{
						this.RunInternal();
					});
					if (ex != null)
					{
						AmTrace.Error("Batch mounter operation {0} got an exception {1}", new object[]
						{
							base.GetType().Name,
							ex
						});
						ReplayCrimsonEvents.BatchMounterOperationFailed.Log<string>(ex.Message);
					}
				}
				finally
				{
					if (!this.m_derivedManagesAllDone && (this.m_opList == null || this.m_opList.Count == 0))
					{
						this.MarkAllDone();
					}
				}
			}
			AmTrace.Leaving("AmBatchOperationBase.Run()", new object[0]);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002994 File Offset: 0x00000B94
		internal AmDbCompletionReason Wait(TimeSpan timeout)
		{
			AmTrace.Debug("Waiting for {0} to complete (timeout={1})", new object[]
			{
				base.GetType().Name,
				timeout
			});
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			AmDbCompletionReason result;
			while (!AmSystemManager.Instance.IsShutdown)
			{
				if (stopwatch.Elapsed > timeout)
				{
					result = AmDbCompletionReason.Timedout;
				}
				else
				{
					if (!this.IsAllDone)
					{
						Thread.Sleep(50);
						continue;
					}
					result = AmDbCompletionReason.Finished;
				}
				IL_6E:
				AmTrace.Debug("{0}.Wait completed with the reason {1}", new object[]
				{
					base.GetType().Name,
					timeout
				});
				return result;
			}
			result = AmDbCompletionReason.Cancelled;
			goto IL_6E;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A39 File Offset: 0x00000C39
		internal AmDbCompletionReason Wait()
		{
			return this.Wait(TimeSpan.MaxValue);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002A48 File Offset: 0x00000C48
		protected void EnqueueDatabaseOperationBatch(Guid dbGuid, List<AmDbOperation> operationList)
		{
			this.m_totalBatchOperationsQueued++;
			foreach (AmDbOperation operation in operationList)
			{
				this.FixDatabaseOperation(operation);
			}
			this.m_opList.AddRange(operationList);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002AB0 File Offset: 0x00000CB0
		protected void StartDatabaseOperationBatch(Guid dbGuid, List<AmDbOperation> operationList)
		{
			AmDatabaseQueueManager databaseQueueManager = AmSystemManager.Instance.DatabaseQueueManager;
			if (!databaseQueueManager.Enqueue(dbGuid, operationList, false))
			{
				foreach (AmDbOperation opr in operationList)
				{
					this.DecrementCounters(opr);
				}
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B14 File Offset: 0x00000D14
		protected void EnqueueDatabaseOperation(AmDbOperation operation)
		{
			this.m_totalSingleOperationsQueued++;
			this.FixDatabaseOperation(operation);
			this.m_opList.Add(operation);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B38 File Offset: 0x00000D38
		protected void StartDatabaseOperations()
		{
			AmDatabaseQueueManager databaseQueueManager = AmSystemManager.Instance.DatabaseQueueManager;
			foreach (AmDbOperation opr in this.m_opList)
			{
				if (!databaseQueueManager.Enqueue(opr))
				{
					this.DecrementCounters(opr);
				}
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002BA0 File Offset: 0x00000DA0
		protected void DecrementCounters(AmDbOperation opr)
		{
			if (opr is AmDbMountOperation)
			{
				this.m_mountRequests--;
				return;
			}
			if (opr is AmDbDismountMismountedOperation)
			{
				this.m_dismountRequests--;
				return;
			}
			if (opr is AmDbClusterDatabaseSyncOperation)
			{
				this.m_clusDbSyncRequests--;
				return;
			}
			if (opr is AmDbAdPropertySyncOperation)
			{
				this.m_adSyncRequests--;
				return;
			}
			if (opr is AmDbMoveOperation)
			{
				this.m_moveRequests--;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C20 File Offset: 0x00000E20
		protected AmMultiNodeMdbStatusFetcher StartMdbStatusFetcher()
		{
			AmMultiNodeMdbStatusFetcher amMultiNodeMdbStatusFetcher = new AmMultiNodeMdbStatusFetcher();
			ThreadPoolThreadCountHelper.IncreaseForServerOperations(this.m_amConfig);
			amMultiNodeMdbStatusFetcher.Start(this.m_amConfig, new Func<List<AmServerName>>(this.GetServers));
			return amMultiNodeMdbStatusFetcher;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002C80 File Offset: 0x00000E80
		protected void AddDelayedFailoverEntryAsync(AmServerName nodeName, AmDbActionReason reasonCode)
		{
			ThreadPool.QueueUserWorkItem(delegate(object unused)
			{
				AmSystemManager.Instance.TransientFailoverSuppressor.AddEntry(reasonCode, nodeName);
			});
		}

		// Token: 0x0600004F RID: 79
		protected abstract List<AmServerName> GetServers();

		// Token: 0x06000050 RID: 80
		protected abstract void RunInternal();

		// Token: 0x06000051 RID: 81
		protected abstract void LogStartupInternal();

		// Token: 0x06000052 RID: 82
		protected abstract void LogCompletionInternal();

		// Token: 0x06000053 RID: 83 RVA: 0x00002CB3 File Offset: 0x00000EB3
		private void FixDatabaseOperation(AmDbOperation operation)
		{
			operation.CustomStatus = this.CustomStatus;
			operation.CompletionCallback = (AmReportCompletionDelegate)Delegate.Combine(operation.CompletionCallback, new AmReportCompletionDelegate(this.OnOperationComplete));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002CE4 File Offset: 0x00000EE4
		private void OnOperationComplete(IADDatabase db)
		{
			lock (this.m_locker)
			{
				if (++this.m_totalOperationsCompleted == this.m_opList.Count)
				{
					this.MarkAllDone();
				}
			}
		}

		// Token: 0x0400001E RID: 30
		protected List<AmDbOperation> m_opList = new List<AmDbOperation>();

		// Token: 0x0400001F RID: 31
		private int m_totalSingleOperationsQueued;

		// Token: 0x04000020 RID: 32
		private int m_totalBatchOperationsQueued;

		// Token: 0x04000021 RID: 33
		protected int m_totalOperationsCompleted;

		// Token: 0x04000022 RID: 34
		protected AmConfig m_amConfig;

		// Token: 0x04000023 RID: 35
		protected bool m_isDebugOptionEnabled;

		// Token: 0x04000024 RID: 36
		protected bool m_derivedManagesAllDone;

		// Token: 0x04000025 RID: 37
		protected int m_mountRequests;

		// Token: 0x04000026 RID: 38
		protected int m_dismountRequests;

		// Token: 0x04000027 RID: 39
		protected int m_clusDbSyncRequests;

		// Token: 0x04000028 RID: 40
		protected int m_adSyncRequests;

		// Token: 0x04000029 RID: 41
		protected int m_moveRequests;

		// Token: 0x0400002A RID: 42
		private object m_locker = new object();

		// Token: 0x0400002B RID: 43
		private bool m_isAllDone;
	}
}
