using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage.ResourceHealth
{
	// Token: 0x02000B2E RID: 2862
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MdbResourceHealthMonitor : PingerDependentHealthMonitor
	{
		// Token: 0x0600678E RID: 26510 RVA: 0x001B5874 File Offset: 0x001B3A74
		private static int ReadMinimumRequestRate()
		{
			IntAppSettingsEntry intAppSettingsEntry = new IntAppSettingsEntry("MdbLatencyMonitor.MinimumRequestRate", 20, ExTraceGlobals.ResourceHealthManagerTracer);
			if (intAppSettingsEntry.Value < 0)
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceError<int>(0L, "[MdbResourceHealthMonitor.ReadMinimumRequestRate] App.Config request rate is invalid '{0}'.  Must be >= 0", intAppSettingsEntry.Value);
				return 20;
			}
			return intAppSettingsEntry.Value;
		}

		// Token: 0x17001C7E RID: 7294
		// (get) Token: 0x0600678F RID: 26511 RVA: 0x001B58BC File Offset: 0x001B3ABC
		internal static int MinimumAcceptableRequestsPerSecond
		{
			get
			{
				return MdbResourceHealthMonitor.minimumAcceptableRequestsPerSecond;
			}
		}

		// Token: 0x06006790 RID: 26512 RVA: 0x001B58C4 File Offset: 0x001B3AC4
		internal static int SetMinimumRequestRateForTest(int newRate)
		{
			int result = MdbResourceHealthMonitor.minimumAcceptableRequestsPerSecond;
			MdbResourceHealthMonitor.minimumAcceptableRequestsPerSecond = newRate;
			return result;
		}

		// Token: 0x06006791 RID: 26513 RVA: 0x001B58E0 File Offset: 0x001B3AE0
		internal MdbResourceHealthMonitor(MdbResourceHealthMonitorKey key) : base(key, key.DatabaseGuid)
		{
			this.rpcLatencyAverage = new FixedTimeAverage(1000, 60, Environment.TickCount);
		}

		// Token: 0x06006792 RID: 26514 RVA: 0x001B5934 File Offset: 0x001B3B34
		internal void Reset()
		{
			lock (this.instanceLock)
			{
				int tickCount = Environment.TickCount;
				this.rpcLatencyAverage.Clear(tickCount);
			}
		}

		// Token: 0x06006793 RID: 26515 RVA: 0x001B5980 File Offset: 0x001B3B80
		private void Update()
		{
			lock (this.instanceLock)
			{
				this.rpcLatencyAverage.Update(Environment.TickCount);
			}
		}

		// Token: 0x06006794 RID: 26516 RVA: 0x001B59CC File Offset: 0x001B3BCC
		public void Update(int averageRpcLatency, uint operationCount)
		{
			lock (this.instanceLock)
			{
				DateTime utcNow = TimeProvider.UtcNow;
				TimeSpan t = utcNow - this.lastOperationUpdateUtc;
				if (!(t < MdbResourceHealthMonitor.UpdateInterval))
				{
					if (this.lastOperationUpdateUtc == DateTime.MinValue || operationCount < this.oldOperationCount)
					{
						this.oldOperationCount = operationCount;
						this.lastOperationUpdateUtc = utcNow;
					}
					else
					{
						uint num = (uint)((operationCount - this.oldOperationCount) / t.TotalSeconds);
						this.oldOperationCount = operationCount;
						this.lastOperationUpdateUtc = utcNow;
						if ((ulong)num >= (ulong)((long)MdbResourceHealthMonitor.MinimumAcceptableRequestsPerSecond) || base.Pinging)
						{
							this.Update(averageRpcLatency);
						}
					}
				}
			}
		}

		// Token: 0x06006795 RID: 26517 RVA: 0x001B5A94 File Offset: 0x001B3C94
		internal void Update(int averageRpcLatency)
		{
			DateTime utcNow = TimeProvider.UtcNow;
			TimeSpan t = utcNow - this.RawLastUpdateUtc;
			if (t < MdbResourceHealthMonitor.UpdateInterval)
			{
				return;
			}
			base.ReceivedUpdate();
			this.LastUpdateUtc = utcNow;
			this.lastRPCLatencyValue = averageRpcLatency;
			this.rpcLatencyAverage.Add((uint)averageRpcLatency);
			base.FireNotifications();
		}

		// Token: 0x06006796 RID: 26518 RVA: 0x001B5AEB File Offset: 0x001B3CEB
		public override ResourceHealthMonitorWrapper CreateWrapper()
		{
			return new MdbResourcehealthMonitorWrapper(this);
		}

		// Token: 0x17001C7F RID: 7295
		// (get) Token: 0x06006797 RID: 26519 RVA: 0x001B5AF3 File Offset: 0x001B3CF3
		public int LastRPCLatencyValue
		{
			get
			{
				return this.lastRPCLatencyValue;
			}
		}

		// Token: 0x17001C80 RID: 7296
		// (get) Token: 0x06006798 RID: 26520 RVA: 0x001B5B00 File Offset: 0x001B3D00
		public int AverageRPCLatencyValue
		{
			get
			{
				int result;
				lock (this.instanceLock)
				{
					this.Update();
					result = (int)this.rpcLatencyAverage.GetValue();
				}
				return result;
			}
		}

		// Token: 0x17001C81 RID: 7297
		// (get) Token: 0x06006799 RID: 26521 RVA: 0x001B5B50 File Offset: 0x001B3D50
		public override DateTime LastUpdateUtc
		{
			get
			{
				if (this.AverageUtcUpdateNeeded(base.LastUpdateUtc))
				{
					lock (this.lastAverageUpdateUtcLock)
					{
						DateTime lastUpdateUtc = base.LastUpdateUtc;
						if (this.AverageUtcUpdateNeeded(lastUpdateUtc))
						{
							this.lastAverageUpdateUtc = new DateTime?(lastUpdateUtc);
						}
					}
				}
				return this.lastAverageUpdateUtc.Value;
			}
		}

		// Token: 0x17001C82 RID: 7298
		// (get) Token: 0x0600679A RID: 26522 RVA: 0x001B5BC0 File Offset: 0x001B3DC0
		internal override DateTime RawLastUpdateUtc
		{
			get
			{
				return base.LastUpdateUtc;
			}
		}

		// Token: 0x17001C83 RID: 7299
		// (get) Token: 0x0600679B RID: 26523 RVA: 0x001B5BC8 File Offset: 0x001B3DC8
		protected override int InternalMetricValue
		{
			get
			{
				if (!this.rpcLatencyAverage.IsEmpty)
				{
					return this.AverageRPCLatencyValue;
				}
				return -1;
			}
		}

		// Token: 0x0600679C RID: 26524 RVA: 0x001B5BE0 File Offset: 0x001B3DE0
		private bool AverageUtcUpdateNeeded(DateTime lastUpdateUtc)
		{
			return this.lastAverageUpdateUtc == null || (this.lastAverageUpdateUtc.Value != lastUpdateUtc && TimeProvider.UtcNow - this.lastAverageUpdateUtc.Value > TimeSpan.FromSeconds(60.0));
		}

		// Token: 0x04003AAA RID: 15018
		private const int TimeWindowInSeconds = 60;

		// Token: 0x04003AAB RID: 15019
		private const string MinimumRequestRateKeyName = "MdbLatencyMonitor.MinimumRequestRate";

		// Token: 0x04003AAC RID: 15020
		private const int DefaultMinimumRequestRate = 20;

		// Token: 0x04003AAD RID: 15021
		private const int MsecInOneSecond = 1000;

		// Token: 0x04003AAE RID: 15022
		private static int minimumAcceptableRequestsPerSecond = MdbResourceHealthMonitor.ReadMinimumRequestRate();

		// Token: 0x04003AAF RID: 15023
		private DateTime? lastAverageUpdateUtc = null;

		// Token: 0x04003AB0 RID: 15024
		private object lastAverageUpdateUtcLock = new object();

		// Token: 0x04003AB1 RID: 15025
		private DateTime lastOperationUpdateUtc = DateTime.MinValue;

		// Token: 0x04003AB2 RID: 15026
		private uint oldOperationCount;

		// Token: 0x04003AB3 RID: 15027
		public static TimeSpan UpdateInterval = TimeSpan.FromSeconds(1.0);

		// Token: 0x04003AB4 RID: 15028
		private FixedTimeAverage rpcLatencyAverage;

		// Token: 0x04003AB5 RID: 15029
		private volatile int lastRPCLatencyValue;
	}
}
