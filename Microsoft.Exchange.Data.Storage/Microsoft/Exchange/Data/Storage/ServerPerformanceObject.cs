using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200029B RID: 667
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ServerPerformanceObject
	{
		// Token: 0x06001B98 RID: 7064 RVA: 0x0007FE00 File Offset: 0x0007E000
		public ServerPerformanceObject(Fqdn identity)
		{
			string key = identity.ToString().ToLower();
			lock (ServerPerformanceObject.latencyDetectionContextFactoriesLock)
			{
				if (!ServerPerformanceObject.latencyDetectionContextFactories.TryGetValue(key, out this.latencyDetectionContextFactory))
				{
					this.latencyDetectionContextFactory = LatencyDetectionContextFactory.CreateFactory(identity);
					ServerPerformanceObject.latencyDetectionContextFactories[key] = this.latencyDetectionContextFactory;
				}
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06001B99 RID: 7065 RVA: 0x0007FE8C File Offset: 0x0007E08C
		public TimeSpan? LastRpcLatency
		{
			get
			{
				return this.lastRpcLatency;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06001B9A RID: 7066 RVA: 0x0007FE94 File Offset: 0x0007E094
		public TimeSpan AverageRpcLatency
		{
			get
			{
				return new TimeSpan((long)this.averageRpcLatency);
			}
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x0007FEA4 File Offset: 0x0007E0A4
		public void Start()
		{
			if (this.latencyDetectionContext != null)
			{
				throw new InvalidOperationException("Start has already been invoked.");
			}
			this.lastRpcLatency = null;
			this.latencyDetectionContext = this.latencyDetectionContextFactory.CreateContext(ContextOptions.DoNotMeasureTime | ContextOptions.DoNotCreateReport, ServerPerformanceObject.Version, this, new IPerformanceDataProvider[]
			{
				RpcDataProvider.Instance
			});
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x0007FEF8 File Offset: 0x0007E0F8
		public void StopAndCollectPerformanceData()
		{
			if (this.latencyDetectionContext == null)
			{
				throw new InvalidOperationException("Must call Start before Stop for collecting performance data.");
			}
			TaskPerformanceData[] array = this.latencyDetectionContext.StopAndFinalizeCollection();
			this.latencyDetectionContext = null;
			if (array == null || array.Length <= 0)
			{
				return;
			}
			this.lastRpcLatency = new TimeSpan?(array[0].Difference.Latency);
			if (this.lastRpcLatency != null)
			{
				double currentValue = (double)this.lastRpcLatency.Value.Ticks;
				this.averageRpcLatency = ServerPerformanceObject.ComputeAveragedPerformanceValue(this.averageRpcLatency, currentValue, 1024);
			}
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x0007FF89 File Offset: 0x0007E189
		private static double ComputeAveragedPerformanceValue(double averagedValue, double currentValue, int sampleSize)
		{
			return (averagedValue * (double)sampleSize - averagedValue + currentValue) / (double)sampleSize;
		}

		// Token: 0x04001331 RID: 4913
		public const int LatencySamples = 1024;

		// Token: 0x04001332 RID: 4914
		private static readonly Dictionary<string, LatencyDetectionContextFactory> latencyDetectionContextFactories = new Dictionary<string, LatencyDetectionContextFactory>();

		// Token: 0x04001333 RID: 4915
		private static readonly object latencyDetectionContextFactoriesLock = new object();

		// Token: 0x04001334 RID: 4916
		private static readonly string Version = typeof(ServerPerformanceObject).GetApplicationVersion();

		// Token: 0x04001335 RID: 4917
		private readonly LatencyDetectionContextFactory latencyDetectionContextFactory;

		// Token: 0x04001336 RID: 4918
		private LatencyDetectionContext latencyDetectionContext;

		// Token: 0x04001337 RID: 4919
		private double averageRpcLatency;

		// Token: 0x04001338 RID: 4920
		private TimeSpan? lastRpcLatency = null;
	}
}
