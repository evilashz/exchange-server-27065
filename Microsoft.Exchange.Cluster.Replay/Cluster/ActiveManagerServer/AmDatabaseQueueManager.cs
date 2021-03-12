using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000028 RID: 40
	internal class AmDatabaseQueueManager
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x0000B90C File Offset: 0x00009B0C
		internal AmDatabaseQueueManager()
		{
			this.IsEnabled = true;
			this.m_dbMap = new Dictionary<Guid, AmDatabaseOperationQueue>();
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000B931 File Offset: 0x00009B31
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x0000B939 File Offset: 0x00009B39
		internal bool IsEnabled { get; set; }

		// Token: 0x060001C9 RID: 457 RVA: 0x0000B944 File Offset: 0x00009B44
		internal void Stop()
		{
			lock (this.m_locker)
			{
				this.IsEnabled = false;
				foreach (Guid key in this.m_dbMap.Keys)
				{
					AmDatabaseOperationQueue amDatabaseOperationQueue = this.m_dbMap[key];
					amDatabaseOperationQueue.IsEnabled = false;
					if (amDatabaseOperationQueue.OperationServiced != null)
					{
						amDatabaseOperationQueue.OperationServiced.Cancel();
					}
				}
			}
			for (;;)
			{
				bool flag2 = false;
				lock (this.m_locker)
				{
					foreach (Guid key2 in this.m_dbMap.Keys)
					{
						AmDatabaseOperationQueue amDatabaseOperationQueue2 = this.m_dbMap[key2];
						if (!amDatabaseOperationQueue2.IsIdle)
						{
							flag2 = true;
							break;
						}
					}
				}
				if (!flag2)
				{
					break;
				}
				Thread.Sleep(50);
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000BA88 File Offset: 0x00009C88
		internal AmDatabaseOperationQueue GetOperationQueueByGuid(Guid guid, bool isCreateIfNotExist)
		{
			AmDatabaseOperationQueue amDatabaseOperationQueue = null;
			lock (this.m_locker)
			{
				if ((!this.m_dbMap.TryGetValue(guid, out amDatabaseOperationQueue) || amDatabaseOperationQueue == null) && isCreateIfNotExist)
				{
					amDatabaseOperationQueue = new AmDatabaseOperationQueue(guid);
					this.m_dbMap[guid] = amDatabaseOperationQueue;
				}
			}
			return amDatabaseOperationQueue;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000BAF0 File Offset: 0x00009CF0
		internal bool Enqueue(AmDbOperation opr)
		{
			bool result = false;
			lock (this.m_locker)
			{
				if (this.IsEnabled)
				{
					AmDatabaseOperationQueue operationQueueByGuid = this.GetOperationQueueByGuid(opr.Database.Guid, true);
					result = operationQueueByGuid.Add(opr, false);
				}
				else
				{
					opr.Cancel();
				}
			}
			return result;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000BB5C File Offset: 0x00009D5C
		internal bool Enqueue(Guid dbGuid, List<AmDbOperation> oprList, bool checkIfQueueIsIdle)
		{
			bool result = false;
			lock (this.m_locker)
			{
				if (this.IsEnabled)
				{
					AmDatabaseOperationQueue operationQueueByGuid = this.GetOperationQueueByGuid(dbGuid, true);
					result = operationQueueByGuid.Add(oprList, checkIfQueueIsIdle);
				}
				else
				{
					foreach (AmDbOperation amDbOperation in oprList)
					{
						amDbOperation.Cancel();
					}
				}
			}
			return result;
		}

		// Token: 0x040000B6 RID: 182
		private object m_locker = new object();

		// Token: 0x040000B7 RID: 183
		private Dictionary<Guid, AmDatabaseOperationQueue> m_dbMap;
	}
}
