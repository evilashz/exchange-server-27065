using System;

namespace Microsoft.Exchange.HttpProxy.Routing
{
	// Token: 0x02000003 RID: 3
	public interface IRoutingDiagnostics
	{
		// Token: 0x06000004 RID: 4
		void AddAccountForestLatency(TimeSpan latency);

		// Token: 0x06000005 RID: 5
		void AddResourceForestLatency(TimeSpan latency);

		// Token: 0x06000006 RID: 6
		void AddActiveManagerLatency(TimeSpan latency);

		// Token: 0x06000007 RID: 7
		void AddGlobalLocatorLatency(TimeSpan latency);

		// Token: 0x06000008 RID: 8
		void AddServerLocatorLatency(TimeSpan latency);

		// Token: 0x06000009 RID: 9
		void AddSharedCacheLatency(TimeSpan latency);

		// Token: 0x0600000A RID: 10
		void AddDiagnosticText(string text);
	}
}
