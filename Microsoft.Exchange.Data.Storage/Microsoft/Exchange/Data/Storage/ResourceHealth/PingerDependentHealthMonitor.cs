using System;
using System.Threading;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Data.Storage.ResourceHealth
{
	// Token: 0x02000B25 RID: 2853
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class PingerDependentHealthMonitor : CacheableResourceHealthMonitor
	{
		// Token: 0x17001C70 RID: 7280
		// (get) Token: 0x0600675F RID: 26463 RVA: 0x001B5066 File Offset: 0x001B3266
		// (set) Token: 0x06006760 RID: 26464 RVA: 0x001B506D File Offset: 0x001B326D
		public static Action<ResourceKey, Guid, PingerDependentHealthMonitor.PingResult> OnExecuteForTest { get; set; }

		// Token: 0x17001C71 RID: 7281
		// (get) Token: 0x06006761 RID: 26465 RVA: 0x001B5075 File Offset: 0x001B3275
		// (set) Token: 0x06006762 RID: 26466 RVA: 0x001B507C File Offset: 0x001B327C
		public static Action<ResourceKey, int, TimeSpan> OnPingIntervalUpdate { get; set; }

		// Token: 0x17001C72 RID: 7282
		// (get) Token: 0x06006763 RID: 26467 RVA: 0x001B5084 File Offset: 0x001B3284
		// (set) Token: 0x06006764 RID: 26468 RVA: 0x001B508B File Offset: 0x001B328B
		public static bool IgnorePingProximity { get; set; }

		// Token: 0x06006765 RID: 26469 RVA: 0x001B5093 File Offset: 0x001B3293
		static PingerDependentHealthMonitor()
		{
			PingerDependentHealthMonitor.ReadPingConfiguration();
		}

		// Token: 0x17001C73 RID: 7283
		// (get) Token: 0x06006766 RID: 26470 RVA: 0x001B50C0 File Offset: 0x001B32C0
		// (set) Token: 0x06006767 RID: 26471 RVA: 0x001B50C7 File Offset: 0x001B32C7
		internal static TimeSpan MinimumPingInterval { get; private set; }

		// Token: 0x17001C74 RID: 7284
		// (get) Token: 0x06006768 RID: 26472 RVA: 0x001B50CF File Offset: 0x001B32CF
		// (set) Token: 0x06006769 RID: 26473 RVA: 0x001B50D6 File Offset: 0x001B32D6
		internal static TimeSpan MaximumPingInterval { get; private set; }

		// Token: 0x17001C75 RID: 7285
		// (get) Token: 0x0600676A RID: 26474 RVA: 0x001B50DE File Offset: 0x001B32DE
		internal virtual DateTime RawLastUpdateUtc
		{
			get
			{
				return this.LastUpdateUtc;
			}
		}

		// Token: 0x0600676B RID: 26475 RVA: 0x001B50E8 File Offset: 0x001B32E8
		private static void ReadPingConfiguration()
		{
			TimeSpanAppSettingsEntry timeSpanAppSettingsEntry = new TimeSpanAppSettingsEntry("MinimumPingIntervalInSeconds", TimeSpanUnit.Seconds, PingerDependentHealthMonitor.DefaultMinimumPingInterval, ExTraceGlobals.DatabasePingerTracer);
			TimeSpanAppSettingsEntry timeSpanAppSettingsEntry2 = new TimeSpanAppSettingsEntry("MaximumPingIntervalInSeconds", TimeSpanUnit.Seconds, PingerDependentHealthMonitor.DefaultMaximumPingInterval, ExTraceGlobals.DatabasePingerTracer);
			PingerDependentHealthMonitor.SetPingerIntervals(timeSpanAppSettingsEntry.Value, timeSpanAppSettingsEntry2.Value);
		}

		// Token: 0x0600676C RID: 26476 RVA: 0x001B5132 File Offset: 0x001B3332
		internal static void SetPingerIntervals(TimeSpan minToUse, TimeSpan maxToUse)
		{
			if (minToUse > TimeSpan.Zero && maxToUse >= minToUse)
			{
				PingerDependentHealthMonitor.MinimumPingInterval = minToUse;
				PingerDependentHealthMonitor.MaximumPingInterval = maxToUse;
				return;
			}
			PingerDependentHealthMonitor.MinimumPingInterval = PingerDependentHealthMonitor.DefaultMinimumPingInterval;
			PingerDependentHealthMonitor.MaximumPingInterval = PingerDependentHealthMonitor.DefaultMaximumPingInterval;
		}

		// Token: 0x0600676D RID: 26477 RVA: 0x001B516B File Offset: 0x001B336B
		internal PingerDependentHealthMonitor(ResourceKey key, Guid mdbGuid) : base(key)
		{
			if (mdbGuid == Guid.Empty)
			{
				throw new ArgumentException("[PingerDependentHealthMonitor.ctor] mdbGuid cannot be Guid.Empty");
			}
			this.MdbGuid = mdbGuid;
		}

		// Token: 0x0600676E RID: 26478 RVA: 0x001B519E File Offset: 0x001B339E
		public override ResourceLoad GetResourceLoad(WorkloadClassification classification, bool raw = false, object optionalData = null)
		{
			this.PingIfNecessary();
			return base.GetResourceLoad(classification, raw, null);
		}

		// Token: 0x17001C76 RID: 7286
		// (get) Token: 0x0600676F RID: 26479 RVA: 0x001B51AF File Offset: 0x001B33AF
		internal virtual TimeSpan CurrentPingInterval
		{
			get
			{
				return this.GetPingInterval(this.consecutivePings);
			}
		}

		// Token: 0x17001C77 RID: 7287
		// (get) Token: 0x06006770 RID: 26480 RVA: 0x001B51BD File Offset: 0x001B33BD
		internal virtual TimeSpan PreviousPingInterval
		{
			get
			{
				return this.GetPingInterval(this.consecutivePings - 1);
			}
		}

		// Token: 0x06006771 RID: 26481 RVA: 0x001B51D0 File Offset: 0x001B33D0
		internal virtual TimeSpan GetPingInterval(int consecutivePings)
		{
			TimeSpan timeSpan;
			if (consecutivePings < 0)
			{
				timeSpan = PingerDependentHealthMonitor.MinimumPingInterval;
			}
			else if (consecutivePings > 10)
			{
				timeSpan = PingerDependentHealthMonitor.MaximumPingInterval;
			}
			else
			{
				timeSpan = TimeSpan.FromSeconds(PingerDependentHealthMonitor.MinimumPingInterval.TotalSeconds * Math.Pow(2.0, (double)consecutivePings));
			}
			if (timeSpan > PingerDependentHealthMonitor.MaximumPingInterval)
			{
				timeSpan = PingerDependentHealthMonitor.MaximumPingInterval;
			}
			return timeSpan;
		}

		// Token: 0x06006772 RID: 26482 RVA: 0x001B5308 File Offset: 0x001B3508
		private void PingIfNecessary()
		{
			if (this.pingCheckedOrScheduled)
			{
				this.DoOnExecuteForTest(PingerDependentHealthMonitor.PingResult.PingAlreadyScheduled);
				return;
			}
			bool flag = true;
			try
			{
				lock (this.instanceLock)
				{
					if (this.pingCheckedOrScheduled)
					{
						this.DoOnExecuteForTest(PingerDependentHealthMonitor.PingResult.PingAlreadyScheduled);
						return;
					}
					this.pingCheckedOrScheduled = true;
				}
				if (this.RawLastUpdateUtc != DateTime.MinValue && TimeProvider.UtcNow - this.RawLastUpdateUtc < this.PreviousPingInterval && !PingerDependentHealthMonitor.IgnorePingProximity)
				{
					this.DoOnExecuteForTest(PingerDependentHealthMonitor.PingResult.TrafficTooClose);
				}
				else if (this.Expired)
				{
					this.DoOnExecuteForTest(PingerDependentHealthMonitor.PingResult.MonitorExpired);
					ExTraceGlobals.DatabasePingerTracer.TraceDebug<ResourceKey>((long)this.GetHashCode(), "[PingerDependentHealthMonitor.Execute] Will not ping on resource {0} because the monitor is marked as expired.", base.Key);
				}
				else
				{
					IMdbSystemMailboxPinger mdbSystemMailboxPinger = PingerCache.Singleton.Get(this.MdbGuid);
					if (!object.ReferenceEquals(mdbSystemMailboxPinger, this.pingerReference))
					{
						lock (this.instanceLock)
						{
							if (!object.ReferenceEquals(mdbSystemMailboxPinger, this.pingerReference))
							{
								this.pingerReference = mdbSystemMailboxPinger;
							}
						}
					}
					if (mdbSystemMailboxPinger == null)
					{
						this.DoOnExecuteForTest(PingerDependentHealthMonitor.PingResult.FailedToGetPinger);
						ExTraceGlobals.DatabasePingerTracer.TraceDebug<ResourceKey>((long)this.GetHashCode(), "[PingerDependentHealthMonitor.Execute] PingerCache returned a null pinger.  Will not ping.  Resource: {0}", base.Key);
					}
					else if (mdbSystemMailboxPinger.LastPingAttemptUtc != DateTime.MinValue && TimeProvider.UtcNow - mdbSystemMailboxPinger.LastPingAttemptUtc < this.PreviousPingInterval && !PingerDependentHealthMonitor.IgnorePingProximity)
					{
						TimeSpan arg = TimeProvider.UtcNow - mdbSystemMailboxPinger.LastPingAttemptUtc;
						this.DoOnExecuteForTest(PingerDependentHealthMonitor.PingResult.PingAttemptTooClose);
						ExTraceGlobals.DatabasePingerTracer.TraceDebug<ResourceKey, TimeSpan, TimeSpan>((long)this.GetHashCode(), "[PingerDependentHealthMonitor.Execute] Will not ping on resource {0} because only {1} has elapsed since the last ping attempt. Expected interval: {2}", base.Key, arg, this.PreviousPingInterval);
					}
					else
					{
						ThreadPool.QueueUserWorkItem(delegate(object state)
						{
							try
							{
								IMdbSystemMailboxPinger mdbSystemMailboxPinger2 = PingerCache.Singleton.Get(this.MdbGuid);
								if (mdbSystemMailboxPinger2 == null)
								{
									this.DoOnExecuteForTest(PingerDependentHealthMonitor.PingResult.FailedToGetPinger);
									ExTraceGlobals.DatabasePingerTracer.TraceDebug<ResourceKey>((long)this.GetHashCode(), "[PingerDependentHealthMonitor.Execute] PingerCache returned a null pinger.  Will not ping.  Resource: {0}", base.Key);
								}
								else if (mdbSystemMailboxPinger2.Ping())
								{
									this.DoOnExecuteForTest(PingerDependentHealthMonitor.PingResult.Pinged);
								}
								else
								{
									Interlocked.Increment(ref this.consecutivePings);
									if (PingerDependentHealthMonitor.OnPingIntervalUpdate != null)
									{
										PingerDependentHealthMonitor.OnPingIntervalUpdate(base.Key, this.consecutivePings, this.CurrentPingInterval);
									}
									this.DoOnExecuteForTest(PingerDependentHealthMonitor.PingResult.FailedPing);
								}
							}
							finally
							{
								lock (this.instanceLock)
								{
									this.pingCheckedOrScheduled = false;
								}
								this.DoOnExecuteForTest(PingerDependentHealthMonitor.PingResult.PingLockReleased);
							}
						});
						flag = false;
					}
				}
			}
			finally
			{
				if (flag)
				{
					lock (this.instanceLock)
					{
						this.pingCheckedOrScheduled = false;
					}
					this.DoOnExecuteForTest(PingerDependentHealthMonitor.PingResult.PingLockReleased);
				}
			}
		}

		// Token: 0x17001C78 RID: 7288
		// (get) Token: 0x06006773 RID: 26483 RVA: 0x001B5584 File Offset: 0x001B3784
		// (set) Token: 0x06006774 RID: 26484 RVA: 0x001B558C File Offset: 0x001B378C
		internal Guid MdbGuid { get; private set; }

		// Token: 0x17001C79 RID: 7289
		// (get) Token: 0x06006775 RID: 26485 RVA: 0x001B5595 File Offset: 0x001B3795
		internal int ConsecutivePings
		{
			get
			{
				return this.consecutivePings;
			}
		}

		// Token: 0x17001C7A RID: 7290
		// (get) Token: 0x06006776 RID: 26486 RVA: 0x001B55A0 File Offset: 0x001B37A0
		protected bool Pinging
		{
			get
			{
				IMdbSystemMailboxPinger mdbSystemMailboxPinger = this.pingerReference;
				return mdbSystemMailboxPinger != null && mdbSystemMailboxPinger.Pinging;
			}
		}

		// Token: 0x06006777 RID: 26487 RVA: 0x001B55C0 File Offset: 0x001B37C0
		protected void ReceivedUpdate()
		{
			IMdbSystemMailboxPinger mdbSystemMailboxPinger = this.pingerReference;
			if (mdbSystemMailboxPinger == null && PingerCache.CreatePingerTestHook != null)
			{
				mdbSystemMailboxPinger = PingerCache.CreatePingerTestHook(this.MdbGuid);
			}
			if (mdbSystemMailboxPinger != null)
			{
				if (mdbSystemMailboxPinger.Pinging)
				{
					ExTraceGlobals.DatabasePingerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "RPC traffic due to database pinger for Mdb: {0}", this.MdbGuid);
					Interlocked.Increment(ref this.consecutivePings);
				}
				else
				{
					Interlocked.Exchange(ref this.consecutivePings, 0);
				}
				if (PingerDependentHealthMonitor.OnPingIntervalUpdate != null)
				{
					PingerDependentHealthMonitor.OnPingIntervalUpdate(base.Key, this.consecutivePings, this.CurrentPingInterval);
				}
			}
		}

		// Token: 0x06006778 RID: 26488 RVA: 0x001B5654 File Offset: 0x001B3854
		private void DoOnExecuteForTest(PingerDependentHealthMonitor.PingResult reason)
		{
			if (PingerDependentHealthMonitor.OnExecuteForTest != null)
			{
				PingerDependentHealthMonitor.OnExecuteForTest(base.Key, this.MdbGuid, reason);
			}
		}

		// Token: 0x04003A8B RID: 14987
		public static readonly TimeSpan DefaultMinimumPingInterval = TimeSpan.FromSeconds(30.0);

		// Token: 0x04003A8C RID: 14988
		public static readonly TimeSpan DefaultMaximumPingInterval = TimeSpan.FromSeconds(30.0);

		// Token: 0x04003A8D RID: 14989
		protected object instanceLock = new object();

		// Token: 0x04003A8E RID: 14990
		private IMdbSystemMailboxPinger pingerReference;

		// Token: 0x04003A8F RID: 14991
		private int consecutivePings;

		// Token: 0x04003A90 RID: 14992
		private bool pingCheckedOrScheduled;

		// Token: 0x02000B26 RID: 2854
		internal enum PingResult
		{
			// Token: 0x04003A98 RID: 15000
			None,
			// Token: 0x04003A99 RID: 15001
			Pinged,
			// Token: 0x04003A9A RID: 15002
			PingNotNeeded,
			// Token: 0x04003A9B RID: 15003
			TrafficTooClose,
			// Token: 0x04003A9C RID: 15004
			PingAttemptTooClose,
			// Token: 0x04003A9D RID: 15005
			FailedToGetPinger,
			// Token: 0x04003A9E RID: 15006
			MonitorExpired,
			// Token: 0x04003A9F RID: 15007
			FailedPing,
			// Token: 0x04003AA0 RID: 15008
			PingTimeNotReached,
			// Token: 0x04003AA1 RID: 15009
			PingAlreadyScheduled,
			// Token: 0x04003AA2 RID: 15010
			PingLockReleased
		}
	}
}
