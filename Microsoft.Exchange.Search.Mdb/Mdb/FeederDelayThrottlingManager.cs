using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x0200000F RID: 15
	internal class FeederDelayThrottlingManager : IFeederDelayThrottlingManager
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00005C9C File Offset: 0x00003E9C
		internal FeederDelayThrottlingManager(ISearchServiceConfig config)
		{
			this.tracingContext = (long)this.GetHashCode();
			this.diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession("FeederDelayThrottlingManager", ExTraceGlobals.FeederThrottlingTracer, this.tracingContext);
			this.throttlingEnabled = config.WLMThrottlingEnabled;
			if (this.throttlingEnabled)
			{
				ResourceHealthMonitorManager.Initialize(ResourceHealthComponent.Search);
				ResourceLoadDelayInfo.Initialize();
				this.resourceHealthMonitor = ResourceHealthMonitorManager.Singleton.Get(ProcessorResourceKey.Local);
				this.mappings = default(FeederDelayThrottlingManager.ThrottlingDelayValues);
				this.mappings.Underloaded = config.RetryDelayWhenUnderloaded;
				this.mappings.Good = config.RetryDelayWhenGood;
				this.mappings.Overloaded = config.RetryDelayWhenOverloaded;
				this.mappings.Critical = config.RetryDelayWhenCritical;
				this.mappings.Unknown = TimeSpan.Zero;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00005D70 File Offset: 0x00003F70
		public TimeSpan DelayForThrottling()
		{
			TimeSpan timeSpan = TimeSpan.Zero;
			if (this.throttlingEnabled)
			{
				ResourceLoad resourceLoad = this.resourceHealthMonitor.GetResourceLoad(WorkloadClassification.Discretionary, false, null);
				switch (resourceLoad.State)
				{
				case ResourceLoadState.Underloaded:
					timeSpan = this.mappings.Underloaded;
					goto IL_88;
				case ResourceLoadState.Full:
					timeSpan = this.mappings.Good;
					goto IL_88;
				case ResourceLoadState.Overloaded:
					timeSpan = this.mappings.Overloaded;
					goto IL_88;
				case ResourceLoadState.Critical:
					timeSpan = this.mappings.Critical;
					goto IL_88;
				}
				timeSpan = this.mappings.Unknown;
				IL_88:
				this.diagnosticsSession.Tracer.TracePerformance<ResourceLoad, TimeSpan>(this.tracingContext, "Throttling feeder with load state: {0} will be delayed for TimeSpan value: {1}", resourceLoad, timeSpan);
			}
			return timeSpan;
		}

		// Token: 0x0400003B RID: 59
		private const string ComponentName = "FeederDelayThrottlingManager";

		// Token: 0x0400003C RID: 60
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x0400003D RID: 61
		private readonly long tracingContext;

		// Token: 0x0400003E RID: 62
		private readonly bool throttlingEnabled;

		// Token: 0x0400003F RID: 63
		private readonly IResourceLoadMonitor resourceHealthMonitor;

		// Token: 0x04000040 RID: 64
		private FeederDelayThrottlingManager.ThrottlingDelayValues mappings;

		// Token: 0x02000010 RID: 16
		private struct ThrottlingDelayValues
		{
			// Token: 0x17000006 RID: 6
			// (get) Token: 0x0600004E RID: 78 RVA: 0x00005E23 File Offset: 0x00004023
			// (set) Token: 0x0600004F RID: 79 RVA: 0x00005E2B File Offset: 0x0000402B
			public TimeSpan Unknown { get; set; }

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000050 RID: 80 RVA: 0x00005E34 File Offset: 0x00004034
			// (set) Token: 0x06000051 RID: 81 RVA: 0x00005E3C File Offset: 0x0000403C
			public TimeSpan Underloaded { get; set; }

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000052 RID: 82 RVA: 0x00005E45 File Offset: 0x00004045
			// (set) Token: 0x06000053 RID: 83 RVA: 0x00005E4D File Offset: 0x0000404D
			public TimeSpan Good { get; set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000054 RID: 84 RVA: 0x00005E56 File Offset: 0x00004056
			// (set) Token: 0x06000055 RID: 85 RVA: 0x00005E5E File Offset: 0x0000405E
			public TimeSpan Overloaded { get; set; }

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000056 RID: 86 RVA: 0x00005E67 File Offset: 0x00004067
			// (set) Token: 0x06000057 RID: 87 RVA: 0x00005E6F File Offset: 0x0000406F
			public TimeSpan Critical { get; set; }
		}
	}
}
