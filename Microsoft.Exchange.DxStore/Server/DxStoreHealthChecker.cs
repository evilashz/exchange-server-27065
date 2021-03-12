using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;
using System.Threading;
using Microsoft.Exchange.DxStore.Common;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.DxStore.Server
{
	// Token: 0x02000051 RID: 81
	public class DxStoreHealthChecker
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x000053C4 File Offset: 0x000035C4
		public DxStoreHealthChecker(DxStoreInstance instance)
		{
			this.instance = instance;
			this.StoreState = StoreState.Initializing;
			this.instanceClientFactory = new InstanceClientFactory(instance.GroupConfig, null);
			this.WhenMajority = Subject.Synchronize<GroupStatusInfo, GroupStatusInfo>(new Subject<GroupStatusInfo>(), Scheduler.TaskPool);
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000542F File Offset: 0x0000362F
		// (set) Token: 0x060002BA RID: 698 RVA: 0x00005437 File Offset: 0x00003637
		public int ConsecutiveMajority { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00005440 File Offset: 0x00003640
		// (set) Token: 0x060002BC RID: 700 RVA: 0x00005448 File Offset: 0x00003648
		public int ConsecutiveNoMajority { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00005451 File Offset: 0x00003651
		// (set) Token: 0x060002BE RID: 702 RVA: 0x00005459 File Offset: 0x00003659
		public StoreState StoreState { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00005462 File Offset: 0x00003662
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000546A File Offset: 0x0000366A
		public ISubject<GroupStatusInfo, GroupStatusInfo> WhenMajority { get; private set; }

		// Token: 0x060002C1 RID: 705 RVA: 0x0000547C File Offset: 0x0000367C
		public void Start()
		{
			this.timer = new GuardedTimer(delegate(object unused)
			{
				this.CollectStatus();
			}, null, TimeSpan.Zero, this.instance.GroupConfig.Settings.GroupHealthCheckAggressiveDuration);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x000054B0 File Offset: 0x000036B0
		public void Stop()
		{
			this.timer.Dispose(true);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x000054C0 File Offset: 0x000036C0
		public bool IsStoreReady()
		{
			return this.instance.IsStartupCompleted && this.ConsecutiveNoMajority < 2 && (this.StoreState & StoreState.Current) == StoreState.Current;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00005501 File Offset: 0x00003701
		public GroupStatusInfo GetLastGroupStatusInfo()
		{
			return this.locker.WithReadLock(() => this.groupStatuses.LastOrDefault<GroupStatusInfo>());
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00005598 File Offset: 0x00003798
		public GroupStatusInfo CollectStatus()
		{
			GroupStatusInfo gsi;
			lock (this.runLock)
			{
				GroupStatusCollector groupStatusCollector = new GroupStatusCollector(this.instance.GroupConfig.Self, this.instanceClientFactory, this.instance.GroupConfig, this.instance.EventLogger, (double)this.instance.GroupConfig.Settings.DefaultHealthCheckRequiredNodePercent);
				groupStatusCollector.Run(this.instance.GroupConfig.Settings.GroupStatusWaitTimeout);
				gsi = groupStatusCollector.GroupStatusInfo;
			}
			this.locker.WithWriteLock(delegate()
			{
				this.DetermineStoreState(gsi);
				this.groupStatuses.Add(gsi);
				if (this.groupStatuses.Count > this.instance.GroupConfig.Settings.MaxEntriesToKeep)
				{
					this.groupStatuses.RemoveAt(0);
				}
			});
			if (gsi.IsMajoritySuccessfulyReplied)
			{
				this.WhenMajority.OnNext(gsi);
			}
			return gsi;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000568C File Offset: 0x0000388C
		public void ChangeTimerDuration(TimeSpan duration)
		{
			if (this.timer != null)
			{
				this.timer.Change(duration, duration);
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000056A4 File Offset: 0x000038A4
		private void DetermineStoreState(GroupStatusInfo gsi)
		{
			int lag = gsi.Lag;
			if (gsi.LocalInstance == null)
			{
				this.StoreState = StoreState.Initializing;
				return;
			}
			StoreState storeState;
			if (lag > 0)
			{
				bool flag = false;
				GroupStatusInfo groupStatusInfo = this.groupStatuses.LastOrDefault<GroupStatusInfo>();
				if (groupStatusInfo != null && groupStatusInfo.Lag > 0 && groupStatusInfo.LocalInstance != null && groupStatusInfo.LocalInstance.InstanceNumber == gsi.LocalInstance.InstanceNumber)
				{
					flag = true;
				}
				if (!flag)
				{
					if (lag <= this.instance.GroupConfig.Settings.MaximumAllowedInstanceNumberLag)
					{
						storeState = (StoreState.Current | StoreState.CatchingUp);
					}
					else
					{
						storeState = (StoreState.Stale | StoreState.CatchingUp);
					}
				}
				else
				{
					storeState = (StoreState.Stale | StoreState.Struck);
				}
			}
			else
			{
				storeState = StoreState.Current;
			}
			if (gsi.IsMajoritySuccessfulyReplied)
			{
				this.ConsecutiveNoMajority = 0;
				this.ConsecutiveMajority++;
				storeState &= ~StoreState.NoMajority;
			}
			else
			{
				storeState |= StoreState.NoMajority;
				this.ConsecutiveMajority = 0;
				this.ConsecutiveNoMajority++;
			}
			this.StoreState = storeState;
		}

		// Token: 0x0400017F RID: 383
		private readonly List<GroupStatusInfo> groupStatuses = new List<GroupStatusInfo>(10);

		// Token: 0x04000180 RID: 384
		private readonly ReaderWriterLockSlim locker = new ReaderWriterLockSlim();

		// Token: 0x04000181 RID: 385
		private readonly object runLock = new object();

		// Token: 0x04000182 RID: 386
		private readonly DxStoreInstance instance;

		// Token: 0x04000183 RID: 387
		private InstanceClientFactory instanceClientFactory;

		// Token: 0x04000184 RID: 388
		private GuardedTimer timer;
	}
}
