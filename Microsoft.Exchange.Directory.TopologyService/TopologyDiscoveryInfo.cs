using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService;
using Microsoft.Exchange.Directory.TopologyService.Configuration;
using Microsoft.Exchange.Directory.TopologyService.Data;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x02000018 RID: 24
	[DebuggerDisplay("{ForestFqdn} Version = {TopologyVersion} State = {State} Queue = {QueueType}")]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TopologyDiscoveryInfo
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00006424 File Offset: 0x00004624
		public TopologyDiscoveryInfo(string forestFqdn)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("forestFqdn", forestFqdn);
			this.ForestFqdn = forestFqdn;
			this.lastDiscoveryFailuresResetTick = Environment.TickCount;
			this.QueueType = QueueType.None;
			this.forestPerfCounter = ForestDiscoveryPerfCounters.GetInstance(this.ForestFqdn);
			this.forestPerfCounter.Reset();
			this.versionMediator = new TopologyDiscoveryInfo.TopologyVersionMediator(this.ForestFqdn, 0, this.forestPerfCounter.TopologyVersion);
			this.ss = new SemaphoreSlim(1);
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000064A0 File Offset: 0x000046A0
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x000064A8 File Offset: 0x000046A8
		public string ForestFqdn { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000064B1 File Offset: 0x000046B1
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x000064B9 File Offset: 0x000046B9
		public ADTopology Topology { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000064C2 File Offset: 0x000046C2
		// (set) Token: 0x060000AB RID: 171 RVA: 0x000064CA File Offset: 0x000046CA
		public Exception LastDiscoveryException { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000064D3 File Offset: 0x000046D3
		public int TopologyVersion
		{
			get
			{
				return this.versionMediator.Version;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000064E0 File Offset: 0x000046E0
		// (set) Token: 0x060000AE RID: 174 RVA: 0x000064E8 File Offset: 0x000046E8
		public QueueType QueueType { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000064F4 File Offset: 0x000046F4
		public DiscoveryState State
		{
			get
			{
				WeakReference weakReference = this.discoveryWI;
				if (weakReference == null || !weakReference.IsAlive)
				{
					return DiscoveryState.None;
				}
				ADTopologyDiscovery adtopologyDiscovery = (ADTopologyDiscovery)weakReference.Target;
				if (adtopologyDiscovery == null)
				{
					return DiscoveryState.None;
				}
				if (adtopologyDiscovery.IsPending)
				{
					return DiscoveryState.Scheduled;
				}
				return DiscoveryState.InProgress;
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00006534 File Offset: 0x00004734
		public bool TryGetDiscoveryWorkItemId(out string id)
		{
			id = null;
			WeakReference weakReference = this.discoveryWI;
			if (weakReference == null || !weakReference.IsAlive)
			{
				return false;
			}
			ADTopologyDiscovery adtopologyDiscovery = (ADTopologyDiscovery)weakReference.Target;
			if (adtopologyDiscovery == null)
			{
				return false;
			}
			id = adtopologyDiscovery.Id;
			return true;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00006574 File Offset: 0x00004774
		public void SetDiscoveryWI(ADTopologyDiscovery wi, QueueType queueType)
		{
			ArgumentValidator.ThrowIfNull("wi", wi);
			if (queueType == QueueType.None)
			{
				throw new ArgumentException(string.Format("Invalid queue type {0}", queueType.ToString()));
			}
			if (this.discoveryWI != null)
			{
				this.discoveryWI = null;
			}
			this.discoveryWI = new WeakReference(wi);
			this.QueueType = queueType;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000065CC File Offset: 0x000047CC
		public void ClearDiscoveryWI()
		{
			if (this.discoveryWI != null)
			{
				this.discoveryWI = null;
			}
			this.QueueType = QueueType.None;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000065E4 File Offset: 0x000047E4
		public bool CanDiscover()
		{
			if (this.ForestFqdn.Equals(TopologyProvider.LocalForestFqdn, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			if (Globals.GetTickDifference(this.lastDiscoveryFailuresResetTick, Environment.TickCount) > 3600000UL)
			{
				this.ResetDiscoveryFailures();
				return true;
			}
			return this.discoveryFailures <= ConfigurationData.Instance.MaxRemoteForestDiscoveryErrorsPerHour;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000663B File Offset: 0x0000483B
		public bool IsDiscoveryScheduled()
		{
			return DiscoveryState.None != this.State;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00006649 File Offset: 0x00004849
		public bool IsDiscoveryInProgress()
		{
			return DiscoveryState.InProgress == this.State;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00006654 File Offset: 0x00004854
		public void SetADTopology(ADTopology topology, TimeSpan discoveryTime)
		{
			ArgumentValidator.ThrowIfNull("topology", topology);
			if (!topology.ForestFqdn.Equals(this.ForestFqdn, StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException("Topology forest doesnt match Discovery info forest");
			}
			if (TimeSpan.MaxValue == discoveryTime || TimeSpan.MinValue == discoveryTime)
			{
				throw new ArgumentException(string.Format("Invalid discovery time. Value {0}", discoveryTime));
			}
			if (discoveryTime < TimeSpan.Zero)
			{
				throw new ArgumentException(string.Format("Invalid discovery time. Negative. Value {0}", discoveryTime));
			}
			this.Topology = topology;
			this.ResetDiscoveryFailures();
			this.Topology.RefreshStatsAndPerfCounters();
			this.forestPerfCounter.AverageDiscoveryTime.IncrementBy(discoveryTime.Ticks);
			this.forestPerfCounter.AverageDiscoveryTimeBase.Increment();
			this.forestPerfCounter.MaxDiscoveryTime.RawValue = Math.Max(this.forestPerfCounter.MaxDiscoveryTime.RawValue, (long)discoveryTime.TotalSeconds);
			this.forestPerfCounter.DiscoveryTime.RawValue = (long)discoveryTime.TotalSeconds;
			this.forestPerfCounter.TopologyVersion.RawValue = (long)this.TopologyVersion;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000677B File Offset: 0x0000497B
		public void GetLock()
		{
			this.ss.Wait();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00006788 File Offset: 0x00004988
		public void ReleaseLock()
		{
			this.ss.Release();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00006796 File Offset: 0x00004996
		public void SetLastDiscoveryException(Exception ex)
		{
			if (ex != null)
			{
				this.IncrementDiscoveryFailures();
				ex.PreserveExceptionStack();
			}
			this.LastDiscoveryException = ex;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000067AE File Offset: 0x000049AE
		public TopologyDiscoveryInfo.TopologyVersionMediator GetTopologyVersionMediator()
		{
			return this.versionMediator;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000067B8 File Offset: 0x000049B8
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			TopologyDiscoveryInfo topologyDiscoveryInfo = obj as TopologyDiscoveryInfo;
			return topologyDiscoveryInfo != null && topologyDiscoveryInfo.ForestFqdn.Equals(this.ForestFqdn, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000067ED File Offset: 0x000049ED
		public override int GetHashCode()
		{
			return this.ForestFqdn.GetHashCode() | base.GetHashCode();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00006801 File Offset: 0x00004A01
		public override string ToString()
		{
			return string.Format("{0}- Version {1}  Discovery State {2}", this.ForestFqdn, this.TopologyVersion, this.State);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00006829 File Offset: 0x00004A29
		private void ResetDiscoveryFailures()
		{
			Interlocked.Exchange(ref this.discoveryFailures, 0);
			Interlocked.Exchange(ref this.lastDiscoveryFailuresResetTick, Environment.TickCount);
			this.SetLastDiscoveryException(null);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00006850 File Offset: 0x00004A50
		private void IncrementDiscoveryFailures()
		{
			Interlocked.Increment(ref this.discoveryFailures);
			this.forestPerfCounter.DiscoveryFailures.Increment();
		}

		// Token: 0x04000053 RID: 83
		private const int DiscoveryFailuresResetEveryNMilliseconds = 3600000;

		// Token: 0x04000054 RID: 84
		private SemaphoreSlim ss;

		// Token: 0x04000055 RID: 85
		private int discoveryFailures;

		// Token: 0x04000056 RID: 86
		private int lastDiscoveryFailuresResetTick;

		// Token: 0x04000057 RID: 87
		private ForestDiscoveryPerfCountersInstance forestPerfCounter;

		// Token: 0x04000058 RID: 88
		private WeakReference discoveryWI;

		// Token: 0x04000059 RID: 89
		private TopologyDiscoveryInfo.TopologyVersionMediator versionMediator;

		// Token: 0x02000019 RID: 25
		public class TopologyVersionMediator
		{
			// Token: 0x060000C0 RID: 192 RVA: 0x0000686F File Offset: 0x00004A6F
			public TopologyVersionMediator(string forest = null, int initialVersion = 0, ExPerformanceCounter perfCounter = null)
			{
				this.version = new Counter(initialVersion);
				this.perfCounter = perfCounter;
				this.forest = forest;
			}

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x060000C1 RID: 193 RVA: 0x00006891 File Offset: 0x00004A91
			public int Version
			{
				get
				{
					return this.version.Value;
				}
			}

			// Token: 0x060000C2 RID: 194 RVA: 0x000068A0 File Offset: 0x00004AA0
			public void IncrementVersion()
			{
				if (!string.IsNullOrEmpty(this.forest))
				{
					ExTraceGlobals.TopologyTracer.TraceDebug<string, int>((long)this.GetHashCode(), "{0} - Incrementing Topology Version. Actual Version {1}.", this.forest, this.version.Value);
				}
				this.version.Increment();
				if (this.perfCounter != null)
				{
					this.perfCounter.RawValue = (long)this.version.Value;
				}
			}

			// Token: 0x0400005E RID: 94
			private readonly string forest;

			// Token: 0x0400005F RID: 95
			private readonly ExPerformanceCounter perfCounter;

			// Token: 0x04000060 RID: 96
			private Counter version;
		}
	}
}
