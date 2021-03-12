using System;
using System.Threading;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000088 RID: 136
	public sealed class SynchronizedAction : IDisposable
	{
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x00012B07 File Offset: 0x00010D07
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x00012B0F File Offset: 0x00010D0F
		private Action ProtectedAction { get; set; }

		// Token: 0x060004EE RID: 1262 RVA: 0x00012B18 File Offset: 0x00010D18
		public SynchronizedAction(Action action)
		{
			this.ProtectedAction = action;
			this.cycles = new SynchronizedAction.CycleData[2];
			for (int i = 0; i < 2; i++)
			{
				this.cycles[i] = new SynchronizedAction.CycleData();
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00012B64 File Offset: 0x00010D64
		public bool TryAction(TimeSpan timeout)
		{
			bool flag = false;
			SynchronizedAction.CycleData cycleData = null;
			lock (this.lockObj)
			{
				if (this.disposed)
				{
					return false;
				}
				SynchronizedAction.CycleData cycleData2 = this.cycles[this.activeCycleIndex];
				if (cycleData2.state == SynchronizedAction.CycleState.Idle)
				{
					flag = true;
					cycleData2.state = SynchronizedAction.CycleState.Running;
				}
				else
				{
					if (timeout == TimeSpan.Zero)
					{
						return false;
					}
					cycleData = this.cycles[(this.activeCycleIndex + 1) % 2];
					if (cycleData.state == SynchronizedAction.CycleState.Idle)
					{
						cycleData.waitEvent.Reset();
						cycleData.state = SynchronizedAction.CycleState.Waiting;
						cycleData.waiterCount = 1;
					}
					else if (cycleData.state == SynchronizedAction.CycleState.Waiting)
					{
						cycleData.waiterCount++;
					}
				}
			}
			if (flag)
			{
				try
				{
					this.ProtectedAction();
					return flag;
				}
				finally
				{
					if (this.FinishCurrentCycle())
					{
						this.StartWorker();
					}
				}
			}
			if (cycleData != null)
			{
				return cycleData.waitEvent.WaitOne(timeout) && !this.disposed;
			}
			return flag;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00012C88 File Offset: 0x00010E88
		private bool FinishCurrentCycle()
		{
			bool result;
			lock (this.lockObj)
			{
				if (this.disposed)
				{
					result = false;
				}
				else
				{
					SynchronizedAction.CycleData cycleData = this.cycles[this.activeCycleIndex];
					cycleData.state = SynchronizedAction.CycleState.Idle;
					cycleData.waiterCount = 0;
					cycleData.waitEvent.Set();
					this.activeCycleIndex = (this.activeCycleIndex + 1) % 2;
					cycleData = this.cycles[this.activeCycleIndex];
					if (cycleData.state == SynchronizedAction.CycleState.Waiting)
					{
						result = true;
					}
					else
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00012D2C File Offset: 0x00010F2C
		private void StartWorker()
		{
			ThreadPool.QueueUserWorkItem(delegate(object param0)
			{
				this.WorkerEntryPoint();
			});
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00012D40 File Offset: 0x00010F40
		private void WorkerEntryPoint()
		{
			do
			{
				lock (this.lockObj)
				{
					if (this.disposed)
					{
						break;
					}
					SynchronizedAction.CycleData cycleData = this.cycles[this.activeCycleIndex];
					cycleData.state = SynchronizedAction.CycleState.Running;
				}
				this.ProtectedAction();
			}
			while (this.FinishCurrentCycle());
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00012DAC File Offset: 0x00010FAC
		public void Dispose()
		{
			lock (this.lockObj)
			{
				if (!this.disposed)
				{
					this.disposed = true;
					foreach (SynchronizedAction.CycleData cycleData in this.cycles)
					{
						cycleData.waitEvent.Set();
						cycleData.waitEvent.Dispose();
					}
				}
			}
		}

		// Token: 0x040002B0 RID: 688
		private int activeCycleIndex;

		// Token: 0x040002B1 RID: 689
		private SynchronizedAction.CycleData[] cycles;

		// Token: 0x040002B2 RID: 690
		private object lockObj = new object();

		// Token: 0x040002B3 RID: 691
		private bool disposed;

		// Token: 0x02000089 RID: 137
		private enum CycleState
		{
			// Token: 0x040002B6 RID: 694
			Idle,
			// Token: 0x040002B7 RID: 695
			Running,
			// Token: 0x040002B8 RID: 696
			Waiting
		}

		// Token: 0x0200008A RID: 138
		private class CycleData
		{
			// Token: 0x040002B9 RID: 697
			public SynchronizedAction.CycleState state;

			// Token: 0x040002BA RID: 698
			public int waiterCount;

			// Token: 0x040002BB RID: 699
			public ManualResetEvent waitEvent = new ManualResetEvent(true);
		}
	}
}
