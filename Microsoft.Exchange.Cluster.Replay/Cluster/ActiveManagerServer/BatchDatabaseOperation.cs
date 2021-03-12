using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200009A RID: 154
	internal class BatchDatabaseOperation : DisposeTrackableBase
	{
		// Token: 0x06000643 RID: 1603 RVA: 0x0001F0F2 File Offset: 0x0001D2F2
		internal List<AmDbOperation> GetCompletedOperationList()
		{
			if (this.Phase != BatchDatabaseOperation.BatchPhase.Complete)
			{
				throw new InvalidOperationException("Operations cannot be fetched before completion");
			}
			return this.opList;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x0001F10E File Offset: 0x0001D30E
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x0001F116 File Offset: 0x0001D316
		internal BatchDatabaseOperation.BatchPhase Phase { get; private set; }

		// Token: 0x06000646 RID: 1606 RVA: 0x0001F11F File Offset: 0x0001D31F
		internal BatchDatabaseOperation()
		{
			this.Phase = BatchDatabaseOperation.BatchPhase.Initializing;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001F148 File Offset: 0x0001D348
		internal void AddOperation(AmDbOperation operation)
		{
			lock (this.locker)
			{
				if (this.Phase != BatchDatabaseOperation.BatchPhase.Initializing)
				{
					throw new InvalidOperationException("Operations cannot be added after Dispatch");
				}
				operation.CompletionCallback = (AmReportCompletionDelegate)Delegate.Combine(operation.CompletionCallback, new AmReportCompletionDelegate(this.OnOperationComplete));
				this.opList.Add(operation);
			}
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001F1C4 File Offset: 0x0001D3C4
		private void OnOperationComplete(IADDatabase db)
		{
			lock (this.locker)
			{
				if (++this.totalOperationsCompleted == this.opList.Count)
				{
					this.Phase = BatchDatabaseOperation.BatchPhase.Complete;
					if (this.completionEvent != null)
					{
						this.completionEvent.Set();
					}
				}
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001F238 File Offset: 0x0001D438
		internal void DispatchOperations()
		{
			bool flag = false;
			lock (this.locker)
			{
				if (this.Phase != BatchDatabaseOperation.BatchPhase.Initializing)
				{
					throw new InvalidOperationException("Batch can only be dispatched once");
				}
				if (this.opList.Count == 0)
				{
					this.Phase = BatchDatabaseOperation.BatchPhase.Complete;
				}
				else
				{
					this.Phase = BatchDatabaseOperation.BatchPhase.Dispatching;
					this.completionEvent = new ManualOneShotEvent("BatchDatabaseOperation");
					flag = true;
				}
			}
			if (flag)
			{
				AmDatabaseQueueManager databaseQueueManager = AmSystemManager.Instance.DatabaseQueueManager;
				foreach (AmDbOperation opr in this.opList)
				{
					databaseQueueManager.Enqueue(opr);
				}
				lock (this.locker)
				{
					if (this.Phase == BatchDatabaseOperation.BatchPhase.Dispatching)
					{
						this.Phase = BatchDatabaseOperation.BatchPhase.Running;
					}
				}
			}
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001F348 File Offset: 0x0001D548
		internal bool WaitForComplete(TimeSpan timeout)
		{
			bool result = false;
			ManualOneShotEvent manualOneShotEvent = null;
			lock (this.locker)
			{
				if (this.completionEvent != null)
				{
					manualOneShotEvent = this.completionEvent;
				}
				else
				{
					result = (this.Phase == BatchDatabaseOperation.BatchPhase.Complete);
				}
			}
			if (manualOneShotEvent != null && manualOneShotEvent.WaitOne(timeout) == ManualOneShotEvent.Result.Success)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001F3B4 File Offset: 0x0001D5B4
		internal void WaitForComplete()
		{
			TimeSpan timeSpan = TimeSpan.FromMinutes(30.0);
			if (!this.WaitForComplete(timeSpan))
			{
				string message = string.Format("Timeout after {0} waiting for BatchDatabaseOperation", timeSpan);
				throw new TimeoutException(message);
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001F3F4 File Offset: 0x0001D5F4
		protected override void InternalDispose(bool disposing)
		{
			lock (this.locker)
			{
				if (!this.disposeCalled)
				{
					if (disposing && this.completionEvent != null)
					{
						this.completionEvent.Dispose();
						this.completionEvent = null;
					}
					this.disposeCalled = true;
				}
			}
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001F45C File Offset: 0x0001D65C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<BatchDatabaseOperation>(this);
		}

		// Token: 0x04000291 RID: 657
		private List<AmDbOperation> opList = new List<AmDbOperation>(20);

		// Token: 0x04000292 RID: 658
		private int totalOperationsCompleted;

		// Token: 0x04000293 RID: 659
		private object locker = new object();

		// Token: 0x04000294 RID: 660
		private bool disposeCalled;

		// Token: 0x04000295 RID: 661
		private ManualOneShotEvent completionEvent;

		// Token: 0x0200009B RID: 155
		internal enum BatchPhase
		{
			// Token: 0x04000298 RID: 664
			Initializing,
			// Token: 0x04000299 RID: 665
			Dispatching,
			// Token: 0x0400029A RID: 666
			Running,
			// Token: 0x0400029B RID: 667
			Complete
		}
	}
}
