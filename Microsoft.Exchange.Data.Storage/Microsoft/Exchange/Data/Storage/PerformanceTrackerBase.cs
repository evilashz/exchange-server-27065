using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.Win32;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004C2 RID: 1218
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class PerformanceTrackerBase : IPerformanceTracker
	{
		// Token: 0x0600357B RID: 13691 RVA: 0x000D79AF File Offset: 0x000D5BAF
		public PerformanceTrackerBase()
		{
			this.stopwatch = new Stopwatch();
			this.internalState = PerformanceTrackerBase.InternalState.Stopped;
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x000D79C9 File Offset: 0x000D5BC9
		public PerformanceTrackerBase(IMailboxSession mailboxSession) : this()
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			this.SetMailboxSessionToTrack(mailboxSession);
		}

		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x0600357D RID: 13693 RVA: 0x000D79E3 File Offset: 0x000D5BE3
		protected TimeSpan ElapsedTime
		{
			get
			{
				return this.stopwatch.Elapsed;
			}
		}

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x0600357E RID: 13694 RVA: 0x000D79F0 File Offset: 0x000D5BF0
		// (set) Token: 0x0600357F RID: 13695 RVA: 0x000D79F8 File Offset: 0x000D5BF8
		private protected TimeSpan CpuTime { protected get; private set; }

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x06003580 RID: 13696 RVA: 0x000D7A01 File Offset: 0x000D5C01
		// (set) Token: 0x06003581 RID: 13697 RVA: 0x000D7A09 File Offset: 0x000D5C09
		private protected TimeSpan StoreRpcLatency { protected get; private set; }

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x06003582 RID: 13698 RVA: 0x000D7A12 File Offset: 0x000D5C12
		// (set) Token: 0x06003583 RID: 13699 RVA: 0x000D7A1A File Offset: 0x000D5C1A
		private protected int StoreRpcCount { protected get; private set; }

		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x06003584 RID: 13700 RVA: 0x000D7A23 File Offset: 0x000D5C23
		// (set) Token: 0x06003585 RID: 13701 RVA: 0x000D7A2B File Offset: 0x000D5C2B
		private protected TimeSpan DirectoryLatency { protected get; private set; }

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x06003586 RID: 13702 RVA: 0x000D7A34 File Offset: 0x000D5C34
		// (set) Token: 0x06003587 RID: 13703 RVA: 0x000D7A3C File Offset: 0x000D5C3C
		private protected int DirectoryCount { protected get; private set; }

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x06003588 RID: 13704 RVA: 0x000D7A45 File Offset: 0x000D5C45
		// (set) Token: 0x06003589 RID: 13705 RVA: 0x000D7A4D File Offset: 0x000D5C4D
		private protected TimeSpan StoreTimeInServer { protected get; private set; }

		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x0600358A RID: 13706 RVA: 0x000D7A56 File Offset: 0x000D5C56
		// (set) Token: 0x0600358B RID: 13707 RVA: 0x000D7A5E File Offset: 0x000D5C5E
		private protected TimeSpan StoreTimeInCPU { protected get; private set; }

		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x0600358C RID: 13708 RVA: 0x000D7A67 File Offset: 0x000D5C67
		// (set) Token: 0x0600358D RID: 13709 RVA: 0x000D7A6F File Offset: 0x000D5C6F
		private protected int StorePagesRead { protected get; private set; }

		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x0600358E RID: 13710 RVA: 0x000D7A78 File Offset: 0x000D5C78
		// (set) Token: 0x0600358F RID: 13711 RVA: 0x000D7A80 File Offset: 0x000D5C80
		private protected int StorePagesPreread { protected get; private set; }

		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x06003590 RID: 13712 RVA: 0x000D7A89 File Offset: 0x000D5C89
		// (set) Token: 0x06003591 RID: 13713 RVA: 0x000D7A91 File Offset: 0x000D5C91
		private protected int StoreLogRecords { protected get; private set; }

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x06003592 RID: 13714 RVA: 0x000D7A9A File Offset: 0x000D5C9A
		// (set) Token: 0x06003593 RID: 13715 RVA: 0x000D7AA2 File Offset: 0x000D5CA2
		private protected int StoreLogBytes { protected get; private set; }

		// Token: 0x06003594 RID: 13716 RVA: 0x000D7AAC File Offset: 0x000D5CAC
		public void SetMailboxSessionToTrack(IMailboxSession session)
		{
			if (session != null)
			{
				if (this.mailboxSession != null && !object.ReferenceEquals(session, this.mailboxSession))
				{
					throw new InvalidOperationException("Only one mailbox session can be tracked at a time");
				}
				this.mailboxSession = session;
				if (this.internalState == PerformanceTrackerBase.InternalState.Started)
				{
					this.startCumulativeRPCPerformanceStatistics = session.GetStoreCumulativeRPCStats();
				}
			}
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x000D7AFC File Offset: 0x000D5CFC
		public virtual void Start()
		{
			this.EnforceInternalState(PerformanceTrackerBase.InternalState.Stopped, "Start");
			this.stopwatch.Start();
			this.startThreadTimes = ThreadTimes.GetFromCurrentThread();
			this.startStorePerformanceData = RpcDataProvider.Instance.TakeSnapshot(true);
			this.startDirectoryPerformanceData = PerformanceContext.Current.TakeSnapshot(true);
			if (this.mailboxSession != null)
			{
				this.startCumulativeRPCPerformanceStatistics = this.mailboxSession.GetStoreCumulativeRPCStats();
			}
			this.internalState = PerformanceTrackerBase.InternalState.Started;
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x000D7B70 File Offset: 0x000D5D70
		public virtual void Stop()
		{
			this.EnforceInternalState(PerformanceTrackerBase.InternalState.Started, "Stop");
			this.stopwatch.Stop();
			ThreadTimes fromCurrentThread = ThreadTimes.GetFromCurrentThread();
			PerformanceData pd = RpcDataProvider.Instance.TakeSnapshot(false);
			PerformanceData pd2 = PerformanceContext.Current.TakeSnapshot(false);
			this.internalState = PerformanceTrackerBase.InternalState.Stopped;
			this.CpuTime += fromCurrentThread.Kernel - this.startThreadTimes.Kernel + (fromCurrentThread.User - this.startThreadTimes.User);
			PerformanceData performanceData = pd - this.startStorePerformanceData;
			this.StoreRpcLatency += performanceData.Latency;
			this.StoreRpcCount += (int)performanceData.Count;
			PerformanceData performanceData2 = pd2 - this.startDirectoryPerformanceData;
			this.DirectoryLatency += performanceData2.Latency;
			this.DirectoryCount += (int)performanceData2.Count;
			this.CalculateStorePerformanceStatistics();
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x000D7C74 File Offset: 0x000D5E74
		private void CalculateStorePerformanceStatistics()
		{
			if (this.mailboxSession != null)
			{
				CumulativeRPCPerformanceStatistics storeCumulativeRPCStats = this.mailboxSession.GetStoreCumulativeRPCStats();
				this.StoreTimeInServer += storeCumulativeRPCStats.timeInServer - this.startCumulativeRPCPerformanceStatistics.timeInServer;
				this.StoreTimeInCPU += storeCumulativeRPCStats.timeInCPU - this.startCumulativeRPCPerformanceStatistics.timeInCPU;
				this.StorePagesRead += (int)(storeCumulativeRPCStats.pagesRead - this.startCumulativeRPCPerformanceStatistics.pagesRead);
				this.StorePagesPreread += (int)(storeCumulativeRPCStats.pagesPreread - this.startCumulativeRPCPerformanceStatistics.pagesPreread);
				this.StoreLogRecords += (int)(storeCumulativeRPCStats.logRecords - this.startCumulativeRPCPerformanceStatistics.logRecords);
				this.StoreLogBytes += (int)(storeCumulativeRPCStats.logBytes - this.startCumulativeRPCPerformanceStatistics.logBytes);
			}
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x000D7D68 File Offset: 0x000D5F68
		protected void EnforceInternalState(PerformanceTrackerBase.InternalState expectedState, string action)
		{
			if (this.internalState != expectedState)
			{
				throw new InvalidOperationException(string.Format("{0} can only be performed when state is {1}. Present state is {2}", action, expectedState, this.internalState));
			}
		}

		// Token: 0x04001CB3 RID: 7347
		private readonly Stopwatch stopwatch;

		// Token: 0x04001CB4 RID: 7348
		private IMailboxSession mailboxSession;

		// Token: 0x04001CB5 RID: 7349
		private ThreadTimes startThreadTimes;

		// Token: 0x04001CB6 RID: 7350
		private PerformanceData startStorePerformanceData;

		// Token: 0x04001CB7 RID: 7351
		private PerformanceData startDirectoryPerformanceData;

		// Token: 0x04001CB8 RID: 7352
		private CumulativeRPCPerformanceStatistics startCumulativeRPCPerformanceStatistics;

		// Token: 0x04001CB9 RID: 7353
		private PerformanceTrackerBase.InternalState internalState;

		// Token: 0x020004C3 RID: 1219
		protected enum InternalState
		{
			// Token: 0x04001CC6 RID: 7366
			Stopped,
			// Token: 0x04001CC7 RID: 7367
			Started
		}
	}
}
