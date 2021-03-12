using System;

namespace Microsoft.Exchange.HttpProxy.Routing
{
	// Token: 0x02000009 RID: 9
	public class NullRoutingDiagnostics : IRoutingDiagnostics
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000020D0 File Offset: 0x000002D0
		void IRoutingDiagnostics.AddAccountForestLatency(TimeSpan latency)
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000020D2 File Offset: 0x000002D2
		void IRoutingDiagnostics.AddResourceForestLatency(TimeSpan latency)
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000020D4 File Offset: 0x000002D4
		void IRoutingDiagnostics.AddActiveManagerLatency(TimeSpan latency)
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000020D6 File Offset: 0x000002D6
		void IRoutingDiagnostics.AddGlobalLocatorLatency(TimeSpan latency)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000020D8 File Offset: 0x000002D8
		void IRoutingDiagnostics.AddServerLocatorLatency(TimeSpan latency)
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000020DA File Offset: 0x000002DA
		void IRoutingDiagnostics.AddSharedCacheLatency(TimeSpan latency)
		{
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000020DC File Offset: 0x000002DC
		void IRoutingDiagnostics.AddDiagnosticText(string text)
		{
		}

		// Token: 0x04000001 RID: 1
		public static readonly NullRoutingDiagnostics Instance = new NullRoutingDiagnostics();
	}
}
