using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Storage.ActiveManager;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000027 RID: 39
	internal class AmDatabaseOperationQueue
	{
		// Token: 0x060001BB RID: 443 RVA: 0x0000B348 File Offset: 0x00009548
		internal AmDatabaseOperationQueue(Guid databaseGuid)
		{
			this.databaseGuid = databaseGuid;
			this.IsEnabled = true;
			this.m_queue = new Queue<AmDbOperation>();
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000B374 File Offset: 0x00009574
		// (set) Token: 0x060001BD RID: 445 RVA: 0x0000B37C File Offset: 0x0000957C
		internal bool IsEnabled { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000B388 File Offset: 0x00009588
		internal AmDbOperation OperationServiced
		{
			get
			{
				AmDbOperation operationServiced;
				lock (this.m_locker)
				{
					operationServiced = this.m_operationServiced;
				}
				return operationServiced;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000B3CC File Offset: 0x000095CC
		internal bool IsIdle
		{
			get
			{
				bool result;
				lock (this.m_locker)
				{
					result = (this.m_queue.Count == 0 && !this.m_isInUse);
				}
				return result;
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000B424 File Offset: 0x00009624
		internal bool Add(List<AmDbOperation> oprList, bool checkIfQueueIdle)
		{
			bool flag = false;
			lock (this.m_locker)
			{
				if (this.IsEnabled)
				{
					if (!checkIfQueueIdle || this.IsIdle)
					{
						foreach (AmDbOperation amDbOperation in oprList)
						{
							AmTrace.Debug("AmDatabaseOperationQueue: Add operation {0} into the queue. checkIfQueueIdle:{1}, IsIdle:{2}", new object[]
							{
								amDbOperation.ToString(),
								checkIfQueueIdle,
								this.IsIdle
							});
							this.AddNoLock(amDbOperation);
						}
						flag = true;
					}
					else
					{
						foreach (AmDbOperation amDbOperation2 in oprList)
						{
							AmTrace.Debug("AmDatabaseOperationQueue: Skip operation {0}. checkIfQueueIdle:{1}, IsIdle:{2}", new object[]
							{
								amDbOperation2.ToString(),
								checkIfQueueIdle,
								this.IsIdle
							});
							this.LogQueueInfo(amDbOperation2);
						}
					}
				}
				if (!flag)
				{
					foreach (AmDbOperation amDbOperation3 in oprList)
					{
						amDbOperation3.Cancel();
					}
				}
			}
			return flag;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000B5DC File Offset: 0x000097DC
		internal bool Add(AmDbOperation op, bool checkIfQueueIdle)
		{
			bool flag = false;
			if (!this.IsEnabled)
			{
				op.Cancel();
				return false;
			}
			lock (this.m_locker)
			{
				if (!checkIfQueueIdle || this.IsIdle)
				{
					AmTrace.Debug("AmDatabaseOperationQueue: Add operation {0} into the queue. checkIfQueueIdle:{1}, IsIdle:{2}", new object[]
					{
						op.ToString(),
						checkIfQueueIdle,
						this.IsIdle
					});
					this.AddNoLock(op);
					flag = true;
				}
				else
				{
					AmTrace.Debug("AmDatabaseOperationQueue: Skip operation {0}. checkIfQueueIdle:{1}, IsIdle:{2}", new object[]
					{
						op.ToString(),
						checkIfQueueIdle,
						this.IsIdle
					});
					this.LogQueueInfo(op);
				}
				if (!flag)
				{
					op.Cancel();
				}
			}
			return flag;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000B6B8 File Offset: 0x000098B8
		internal void LogQueueInfo(AmDbOperation operation)
		{
			string name = operation.GetType().Name;
			string uniqueId = operation.UniqueId;
			string text = operation.ToString();
			int count = this.m_queue.Count;
			string text2 = "<none>";
			string text3 = "<none>";
			string text4 = "<none>";
			if (this.m_operationServiced != null)
			{
				text2 = this.m_operationServiced.GetType().Name;
				text3 = this.m_operationServiced.UniqueId;
				text4 = this.m_operationServiced.ToString();
			}
			string text5 = "<none>";
			string text6 = "<none>";
			string text7 = "<none>";
			AmDbOperation amDbOperation = this.m_queue.FirstOrDefault<AmDbOperation>();
			if (amDbOperation != null)
			{
				text5 = amDbOperation.GetType().Name;
				text6 = amDbOperation.UniqueId;
				text7 = amDbOperation.ToString();
			}
			ReplayCrimsonEvents.DatabaseOperationSkippedSinceAlreadyActionsQueued.Log<Guid, int, string, string, string, string, string, string, string, string, string>(this.databaseGuid, count, name, uniqueId, text, text2, text3, text4, text5, text6, text7);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000B79C File Offset: 0x0000999C
		internal void PerformAction(object context)
		{
			AmDbOperation amDbOperation = null;
			for (;;)
			{
				lock (this.m_locker)
				{
					if (this.m_queue.Count <= 0)
					{
						this.m_operationServiced = null;
						this.m_isInUse = false;
						break;
					}
					amDbOperation = this.m_queue.Dequeue();
					this.m_operationServiced = amDbOperation;
				}
				if (this.IsEnabled)
				{
					bool flag2 = true;
					try
					{
						amDbOperation.ReportStatus(amDbOperation.Database, AmDbActionStatus.Started);
						this.RunOperation(amDbOperation);
						amDbOperation.ReportStatus(amDbOperation.Database, AmDbActionStatus.Completed);
						flag2 = false;
						continue;
					}
					finally
					{
						if (flag2)
						{
							amDbOperation.ReportStatus(amDbOperation.Database, AmDbActionStatus.Failed);
						}
					}
				}
				amDbOperation.Cancel();
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000B864 File Offset: 0x00009A64
		private void AddNoLock(AmDbOperation op)
		{
			this.m_queue.Enqueue(op);
			if (!this.m_isInUse)
			{
				this.m_isInUse = true;
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.PerformAction));
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000B894 File Offset: 0x00009A94
		private void RunOperation(AmDbOperation opr)
		{
			AmTrace.Debug("Running database operation: {0}", new object[]
			{
				opr
			});
			AmFaultInject.SleepIfRequired(opr.Database.Guid, AmSleepTag.GenericDbOperationProcessingDelay);
			opr.Run();
			if (opr.LastException != null)
			{
				AmTrace.Error("Error from database operation {0}\n{1}", new object[]
				{
					opr,
					opr.LastException
				});
				return;
			}
			AmTrace.Debug("Database operation finished: {0}", new object[]
			{
				opr
			});
		}

		// Token: 0x040000B0 RID: 176
		private readonly Guid databaseGuid;

		// Token: 0x040000B1 RID: 177
		private object m_locker = new object();

		// Token: 0x040000B2 RID: 178
		private bool m_isInUse;

		// Token: 0x040000B3 RID: 179
		private AmDbOperation m_operationServiced;

		// Token: 0x040000B4 RID: 180
		private Queue<AmDbOperation> m_queue;
	}
}
