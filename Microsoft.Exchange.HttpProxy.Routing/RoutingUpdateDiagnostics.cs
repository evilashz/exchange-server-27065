using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Exchange.HttpProxy.Routing
{
	// Token: 0x02000042 RID: 66
	public class RoutingUpdateDiagnostics : IRoutingDiagnostics
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00004360 File Offset: 0x00002560
		public RoutingUpdateDiagnostics()
		{
			this.accountForestLatencies = new List<long>(2);
			this.globalLocatorLatencies = new List<long>(2);
			this.resourceForestLatencies = new List<long>(2);
			this.serverLocatorLatencies = new List<long>(2);
			this.activeManagerLatencies = new List<long>(2);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000043AF File Offset: 0x000025AF
		void IRoutingDiagnostics.AddAccountForestLatency(TimeSpan latency)
		{
			this.accountForestLatencies.Add(Convert.ToInt64(latency.TotalMilliseconds));
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000043C8 File Offset: 0x000025C8
		void IRoutingDiagnostics.AddResourceForestLatency(TimeSpan latency)
		{
			this.resourceForestLatencies.Add(Convert.ToInt64(latency.TotalMilliseconds));
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000043E1 File Offset: 0x000025E1
		void IRoutingDiagnostics.AddActiveManagerLatency(TimeSpan latency)
		{
			this.activeManagerLatencies.Add(Convert.ToInt64(latency.TotalMilliseconds));
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000043FA File Offset: 0x000025FA
		void IRoutingDiagnostics.AddGlobalLocatorLatency(TimeSpan latency)
		{
			this.globalLocatorLatencies.Add(Convert.ToInt64(latency.TotalMilliseconds));
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004413 File Offset: 0x00002613
		void IRoutingDiagnostics.AddServerLocatorLatency(TimeSpan latency)
		{
			this.serverLocatorLatencies.Add(Convert.ToInt64(latency.TotalMilliseconds));
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000442C File Offset: 0x0000262C
		void IRoutingDiagnostics.AddSharedCacheLatency(TimeSpan latency)
		{
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000442E File Offset: 0x0000262E
		void IRoutingDiagnostics.AddDiagnosticText(string text)
		{
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004430 File Offset: 0x00002630
		internal long GetTotalLatency()
		{
			long num = 0L;
			num += this.accountForestLatencies.Sum();
			num += this.globalLocatorLatencies.Sum();
			num += this.resourceForestLatencies.Sum();
			num += this.serverLocatorLatencies.Sum();
			return num + this.activeManagerLatencies.Sum();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004487 File Offset: 0x00002687
		internal void Clear()
		{
			this.accountForestLatencies.Clear();
			this.globalLocatorLatencies.Clear();
			this.resourceForestLatencies.Clear();
			this.serverLocatorLatencies.Clear();
			this.activeManagerLatencies.Clear();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000044C0 File Offset: 0x000026C0
		internal void LogLatencies(RequestDetailsLogger logger)
		{
			logger.Set(RoutingUpdateModuleMetadata.AccountForestLatencyBreakup, RoutingUpdateDiagnostics.GetBreakupOfLatencies(this.accountForestLatencies));
			logger.Set(RoutingUpdateModuleMetadata.TotalAccountForestLatency, this.accountForestLatencies.Sum());
			logger.Set(RoutingUpdateModuleMetadata.GlsLatencyBreakup, RoutingUpdateDiagnostics.GetBreakupOfLatencies(this.globalLocatorLatencies));
			logger.Set(RoutingUpdateModuleMetadata.TotalGlsLatency, this.globalLocatorLatencies.Sum());
			logger.Set(RoutingUpdateModuleMetadata.ResourceForestLatencyBreakup, RoutingUpdateDiagnostics.GetBreakupOfLatencies(this.resourceForestLatencies));
			logger.Set(RoutingUpdateModuleMetadata.TotalResourceForestLatency, this.resourceForestLatencies.Sum());
			logger.Set(RoutingUpdateModuleMetadata.ActiveManagerLatencyBreakup, RoutingUpdateDiagnostics.GetBreakupOfLatencies(this.activeManagerLatencies));
			logger.Set(RoutingUpdateModuleMetadata.TotalActiveManagerLatency, this.activeManagerLatencies.Sum());
			logger.Set(RoutingUpdateModuleMetadata.ServerLocatorLatency, this.serverLocatorLatencies.Sum());
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000045E4 File Offset: 0x000027E4
		private static string GetBreakupOfLatencies(List<long> latencies)
		{
			if (latencies == null)
			{
				throw new ArgumentNullException("latencies");
			}
			StringBuilder result = new StringBuilder();
			latencies.ForEach(delegate(long latency)
			{
				result.Append(latency);
				result.Append(';');
			});
			return result.ToString();
		}

		// Token: 0x04000067 RID: 103
		private readonly List<long> accountForestLatencies;

		// Token: 0x04000068 RID: 104
		private readonly List<long> globalLocatorLatencies;

		// Token: 0x04000069 RID: 105
		private readonly List<long> resourceForestLatencies;

		// Token: 0x0400006A RID: 106
		private readonly List<long> serverLocatorLatencies;

		// Token: 0x0400006B RID: 107
		private readonly List<long> activeManagerLatencies;
	}
}
