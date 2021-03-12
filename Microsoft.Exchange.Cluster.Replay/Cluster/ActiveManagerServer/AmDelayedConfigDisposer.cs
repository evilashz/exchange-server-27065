using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000022 RID: 34
	internal class AmDelayedConfigDisposer : TimerComponent
	{
		// Token: 0x06000146 RID: 326 RVA: 0x000078CC File Offset: 0x00005ACC
		public AmDelayedConfigDisposer() : base(TimeSpan.FromMilliseconds(-1.0), TimeSpan.FromMilliseconds(-1.0), "AmDelayedConfigDisposer")
		{
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007922 File Offset: 0x00005B22
		protected override void StopInternal()
		{
			base.StopInternal();
			this.CleanupObjects(false);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00007934 File Offset: 0x00005B34
		internal void AddEntry(AmConfig cfg)
		{
			AmDelayedConfigDisposer.DisposableContainer disposableContainer = new AmDelayedConfigDisposer.DisposableContainer(cfg);
			lock (this.m_locker)
			{
				ExTraceGlobals.AmConfigManagerTracer.TraceDebug<string, string>(0L, "Adding AmConfig for delayed dispose (Queued for Dispose: {0}, Dispose due at: {1})", disposableContainer.TimeRequestSubmitted.ToString(), disposableContainer.DisposeDueAt.ToString());
				this.m_waitingList.Add(disposableContainer);
				if (disposableContainer.DisposeDueAt < this.m_lowestDueTime)
				{
					this.m_lowestDueTime = disposableContainer.DisposeDueAt;
					this.SetupWakeupTime(this.m_lowestDueTime);
				}
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000079E8 File Offset: 0x00005BE8
		protected override void TimerCallbackInternal()
		{
			this.CleanupObjects(false);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000079F4 File Offset: 0x00005BF4
		private void CleanupObjects(bool isForce)
		{
			List<AmDelayedConfigDisposer.DisposableContainer> list = new List<AmDelayedConfigDisposer.DisposableContainer>(5);
			ExTraceGlobals.AmConfigManagerTracer.TraceDebug(0L, "Entering CleanupObjects");
			lock (this.m_locker)
			{
				ExDateTime now = ExDateTime.Now;
				this.m_lowestDueTime = ExDateTime.MaxValue;
				bool flag2 = false;
				foreach (AmDelayedConfigDisposer.DisposableContainer disposableContainer in this.m_waitingList)
				{
					if (object.ReferenceEquals(disposableContainer.Config, AmSystemManager.Instance.Config))
					{
						ExTraceGlobals.AmConfigManagerTracer.TraceDebug(0L, "System manager is still using config.");
						flag2 = true;
					}
					if (isForce || (now > disposableContainer.DisposeDueAt && !flag2))
					{
						list.Add(disposableContainer);
					}
					else if (disposableContainer.DisposeDueAt < this.m_lowestDueTime)
					{
						this.m_lowestDueTime = disposableContainer.DisposeDueAt;
					}
				}
				foreach (AmDelayedConfigDisposer.DisposableContainer item in list)
				{
					this.m_waitingList.Remove(item);
				}
				if (!isForce && flag2)
				{
					if (this.m_lowestDueTime == ExDateTime.MaxValue)
					{
						this.m_lowestDueTime = ExDateTime.Now.AddMinutes(5.0);
					}
					ExTraceGlobals.AmConfigManagerTracer.TraceDebug<string>(0L, "System manager is still using config. It will try to dispose at {0}", this.m_lowestDueTime.ToString());
				}
				this.SetupWakeupTime(this.m_lowestDueTime);
			}
			foreach (AmDelayedConfigDisposer.DisposableContainer disposableContainer2 in list)
			{
				try
				{
					disposableContainer2.Dispose();
				}
				catch (ClusterException arg)
				{
					ExTraceGlobals.AmConfigManagerTracer.TraceDebug<ClusterException>(0L, "AmDelayedConfigDisposer encountered exception {0} while disposing", arg);
				}
			}
			ExTraceGlobals.AmConfigManagerTracer.TraceDebug(0L, "Exiting CleanupObjects");
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007C5C File Offset: 0x00005E5C
		private void SetupWakeupTime(ExDateTime lowestTime)
		{
			if (lowestTime != ExDateTime.MaxValue)
			{
				ExDateTime now = ExDateTime.Now;
				TimeSpan dueTime = TimeSpan.Zero;
				if (now < lowestTime)
				{
					dueTime = (lowestTime - now).Add(TimeSpan.FromSeconds(2.0));
				}
				base.ChangeTimer(dueTime, TimeSpan.FromMilliseconds(-1.0));
				return;
			}
			base.ChangeTimer(TimeSpan.FromMilliseconds(-1.0), TimeSpan.FromMilliseconds(-1.0));
		}

		// Token: 0x0400008F RID: 143
		private List<AmDelayedConfigDisposer.DisposableContainer> m_waitingList = new List<AmDelayedConfigDisposer.DisposableContainer>(5);

		// Token: 0x04000090 RID: 144
		private object m_locker = new object();

		// Token: 0x04000091 RID: 145
		private ExDateTime m_lowestDueTime = ExDateTime.MaxValue;

		// Token: 0x02000023 RID: 35
		internal class DisposableContainer : DisposeTrackableBase
		{
			// Token: 0x0600014C RID: 332 RVA: 0x00007CE4 File Offset: 0x00005EE4
			public DisposableContainer(AmConfig cfg)
			{
				this.Config = cfg;
				this.TimeRequestSubmitted = ExDateTime.Now;
				this.DisposeDueAt = this.TimeRequestSubmitted.AddSeconds((double)RegistryParameters.AmConfigObjectDisposeDelayInSec);
			}

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x0600014D RID: 333 RVA: 0x00007D23 File Offset: 0x00005F23
			// (set) Token: 0x0600014E RID: 334 RVA: 0x00007D2B File Offset: 0x00005F2B
			internal ExDateTime TimeRequestSubmitted { get; set; }

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x0600014F RID: 335 RVA: 0x00007D34 File Offset: 0x00005F34
			// (set) Token: 0x06000150 RID: 336 RVA: 0x00007D3C File Offset: 0x00005F3C
			internal ExDateTime DisposeDueAt { get; set; }

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x06000151 RID: 337 RVA: 0x00007D45 File Offset: 0x00005F45
			// (set) Token: 0x06000152 RID: 338 RVA: 0x00007D4D File Offset: 0x00005F4D
			internal AmConfig Config { get; set; }

			// Token: 0x06000153 RID: 339 RVA: 0x00007D56 File Offset: 0x00005F56
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<AmDelayedConfigDisposer.DisposableContainer>(this);
			}

			// Token: 0x06000154 RID: 340 RVA: 0x00007D60 File Offset: 0x00005F60
			protected override void InternalDispose(bool disposing)
			{
				lock (this)
				{
					if (disposing)
					{
						AmConfig config = this.Config;
						string arg = config.TimeCreated.ToString();
						ExTraceGlobals.AmConfigManagerTracer.TraceDebug<string>(0L, "Disposing AmConfig sub objects (Creation Time: {0})", arg);
						if (config.DbState != null)
						{
							ExTraceGlobals.AmConfigManagerTracer.TraceDebug<string>(0L, "Disposing DbState of AmConfig (Creation Time: {0})", arg);
							this.Config.DbState.Dispose();
						}
						AmDagConfig dagConfig = this.Config.DagConfig;
						if (dagConfig != null && dagConfig.Cluster != null)
						{
							ExTraceGlobals.AmConfigManagerTracer.TraceDebug<string>(0L, "Disposing Cluster of AmConfig (Creation Time: {0})", arg);
							dagConfig.Cluster.Dispose();
						}
						config.IsInternalObjectsDisposed = true;
					}
				}
			}
		}
	}
}
