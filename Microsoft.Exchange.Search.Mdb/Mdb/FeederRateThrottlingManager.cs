using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x02000012 RID: 18
	internal class FeederRateThrottlingManager : IFeederRateThrottlingManager
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00005E78 File Offset: 0x00004078
		internal FeederRateThrottlingManager(ISearchServiceConfig config, MdbInfo mdbInfo, FeederRateThrottlingManager.ThrottlingRateExecutionType execType) : this(config, mdbInfo, execType, null)
		{
			IResourceLoadMonitor resourceLoadMonitor = null;
			if (this.throttlingEnabled)
			{
				ResourceHealthMonitorManager.Initialize(ResourceHealthComponent.Search);
				ResourceLoadDelayInfo.Initialize();
				resourceLoadMonitor = ResourceHealthMonitorManager.Singleton.Get(ProcessorResourceKey.Local);
			}
			this.resourceHealthMonitor = resourceLoadMonitor;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00005EBC File Offset: 0x000040BC
		internal FeederRateThrottlingManager(ISearchServiceConfig config, MdbInfo mdbInfo, FeederRateThrottlingManager.ThrottlingRateExecutionType execType, IResourceLoadMonitor resourceHealthMonitor)
		{
			this.tracingContext = (long)this.GetHashCode();
			this.diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession("FeederRateThrottlingManager", ExTraceGlobals.FeederThrottlingTracer, this.tracingContext);
			this.execType = execType;
			this.mdbInfo = mdbInfo;
			this.mappings = default(FeederRateThrottlingManager.ThrottlingRateValues);
			switch (this.execType)
			{
			case FeederRateThrottlingManager.ThrottlingRateExecutionType.Fast:
				this.mappings.StartRate = config.CrawlerRateFast;
				this.mappings.Max = config.CrawlerRateMaxFast;
				this.mappings.Min = config.CrawlerRateMinFast;
				this.mappings.Cpu = 100.0;
				break;
			case FeederRateThrottlingManager.ThrottlingRateExecutionType.LowResource:
				this.mappings.StartRate = config.CrawlerRateLowResource;
				this.mappings.Max = config.CrawlerRateMaxLowResource;
				this.mappings.Min = config.CrawlerRateMinLowResource;
				this.mappings.Cpu = (double)config.CrawlerRateCpuLowResource;
				break;
			}
			this.throttlingEnabled = config.WLMThrottlingEnabled;
			this.resourceHealthMonitor = resourceHealthMonitor;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00005FD0 File Offset: 0x000041D0
		public virtual double ThrottlingRateContinue(double currentRate)
		{
			double num = this.mappings.StartRate;
			if (this.throttlingEnabled)
			{
				ResourceLoad load = this.resourceHealthMonitor.GetResourceLoad((this.execType == FeederRateThrottlingManager.ThrottlingRateExecutionType.Fast) ? WorkloadClassification.CustomerExpectation : WorkloadClassification.Discretionary, false, null);
				this.diagnosticsSession.TraceDebug<MdbInfo, double>("(MDB {0}): Inital LoadRatio is {1}", this.mdbInfo, load.LoadRatio);
				load = this.CalibrateResourceLoad(load);
				this.diagnosticsSession.TraceDebug<MdbInfo, double, ResourceLoadState>("(MDB {0}): Adjusted LoadRatio is {1} with state {2}", this.mdbInfo, load.LoadRatio, load.State);
				this.lastUpdateTime = ((this.lastUpdateTime == this.lastChangeTime) ? this.resourceHealthMonitor.LastUpdateUtc : this.lastUpdateTime);
				switch (load.State)
				{
				case ResourceLoadState.Underloaded:
					if (this.lastUpdateTime != this.lastChangeTime && this.lastUpdateTime != this.resourceHealthMonitor.LastUpdateUtc)
					{
						if (currentRate != 0.0)
						{
							double num2 = Math.Max(1.0 + (1.0 - load.LoadRatio) / (2.0 * load.LoadRatio), 1.01);
							num = currentRate * num2;
						}
						else
						{
							num = ((this.mappings.Min > 0.0) ? this.mappings.Min : 0.2);
						}
						this.lastChangeTime = this.resourceHealthMonitor.LastUpdateUtc;
						this.lastUpdateTime = this.lastChangeTime;
						goto IL_22D;
					}
					num = currentRate;
					goto IL_22D;
				case ResourceLoadState.Overloaded:
					if (this.lastUpdateTime != this.lastChangeTime && this.lastUpdateTime != this.resourceHealthMonitor.LastUpdateUtc)
					{
						double num3 = Math.Max(0.9 / load.LoadRatio, 0.1);
						num = currentRate * num3;
						this.lastChangeTime = this.resourceHealthMonitor.LastUpdateUtc;
						this.lastUpdateTime = this.lastChangeTime;
						goto IL_22D;
					}
					num = currentRate;
					goto IL_22D;
				case ResourceLoadState.Critical:
					num = 0.0;
					goto IL_22D;
				}
				num = currentRate;
				IL_22D:
				if (ResourceLoadState.Critical != load.State)
				{
					num = Math.Min(num, this.mappings.Max);
					num = Math.Max(num, this.mappings.Min);
				}
				this.diagnosticsSession.TraceDebug("(MDB {0}): Throttling feeder rate. Load state is {1} and rate will be {2} doc/sec from previous value {3} doc/sec", new object[]
				{
					this.mdbInfo,
					load.State,
					num,
					currentRate
				});
				this.diagnosticsSession.Tracer.TracePerformance(this.tracingContext, "(MDB {0}): Throttling feeder rate. Load state is {1} and rate will be {2} doc/sec from previous value {3} doc/sec", new object[]
				{
					this.mdbInfo,
					load.State,
					num,
					currentRate
				});
			}
			return num;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000062DC File Offset: 0x000044DC
		public virtual double ThrottlingRateStart()
		{
			double currentRate = this.mappings.StartRate;
			if (this.throttlingEnabled)
			{
				this.lastUpdateTime = this.resourceHealthMonitor.LastUpdateUtc;
				this.lastChangeTime = this.resourceHealthMonitor.LastUpdateUtc;
				ResourceLoad resourceLoad = this.resourceHealthMonitor.GetResourceLoad((this.execType == FeederRateThrottlingManager.ThrottlingRateExecutionType.Fast) ? WorkloadClassification.CustomerExpectation : WorkloadClassification.Discretionary, false, null);
				switch (this.CalibrateResourceLoad(resourceLoad).State)
				{
				case ResourceLoadState.Underloaded:
					currentRate = this.mappings.StartRate;
					goto IL_B0;
				case ResourceLoadState.Critical:
					currentRate = 0.0;
					goto IL_B0;
				}
				currentRate = this.mappings.Min;
			}
			IL_B0:
			return this.ThrottlingRateContinue(currentRate);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000063A0 File Offset: 0x000045A0
		private ResourceLoad CalibrateResourceLoad(ResourceLoad load)
		{
			ResourceLoad full;
			if (load.LoadRatio / (this.mappings.Cpu / 100.0) >= 0.95 && load.LoadRatio / (this.mappings.Cpu / 100.0) <= 1.05)
			{
				full = ResourceLoad.Full;
			}
			else if (load.LoadRatio < ResourceLoad.Critical.LoadRatio - (100.0 - this.mappings.Cpu) / 100.0)
			{
				full = new ResourceLoad(load.LoadRatio + (100.0 - this.mappings.Cpu) / 100.0, null, null);
			}
			else
			{
				full = new ResourceLoad(load.LoadRatio, null, null);
			}
			return full;
		}

		// Token: 0x04000046 RID: 70
		private const string ComponentName = "FeederRateThrottlingManager";

		// Token: 0x04000047 RID: 71
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x04000048 RID: 72
		private readonly long tracingContext;

		// Token: 0x04000049 RID: 73
		private readonly bool throttlingEnabled;

		// Token: 0x0400004A RID: 74
		private readonly IResourceLoadMonitor resourceHealthMonitor;

		// Token: 0x0400004B RID: 75
		private readonly FeederRateThrottlingManager.ThrottlingRateExecutionType execType;

		// Token: 0x0400004C RID: 76
		private readonly MdbInfo mdbInfo;

		// Token: 0x0400004D RID: 77
		private readonly FeederRateThrottlingManager.ThrottlingRateValues mappings;

		// Token: 0x0400004E RID: 78
		private DateTime lastUpdateTime;

		// Token: 0x0400004F RID: 79
		private DateTime lastChangeTime;

		// Token: 0x02000013 RID: 19
		public enum ThrottlingRateExecutionType
		{
			// Token: 0x04000051 RID: 81
			Fast,
			// Token: 0x04000052 RID: 82
			LowResource
		}

		// Token: 0x02000014 RID: 20
		private struct ThrottlingRateValues
		{
			// Token: 0x1700000B RID: 11
			// (get) Token: 0x0600005F RID: 95 RVA: 0x000064A1 File Offset: 0x000046A1
			// (set) Token: 0x06000060 RID: 96 RVA: 0x000064A9 File Offset: 0x000046A9
			public double StartRate { get; set; }

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000061 RID: 97 RVA: 0x000064B2 File Offset: 0x000046B2
			// (set) Token: 0x06000062 RID: 98 RVA: 0x000064BA File Offset: 0x000046BA
			public double Max { get; set; }

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x06000063 RID: 99 RVA: 0x000064C3 File Offset: 0x000046C3
			// (set) Token: 0x06000064 RID: 100 RVA: 0x000064CB File Offset: 0x000046CB
			public double Min { get; set; }

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x06000065 RID: 101 RVA: 0x000064D4 File Offset: 0x000046D4
			// (set) Token: 0x06000066 RID: 102 RVA: 0x000064DC File Offset: 0x000046DC
			public double Cpu { get; set; }
		}
	}
}
