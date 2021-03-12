using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000006 RID: 6
	internal class AmServerNameCacheLogEvent : AmServerNameCache
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002927 File Offset: 0x00000B27
		public AmServerNameCacheLogEvent()
		{
			base.Enable();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002938 File Offset: 0x00000B38
		public override string GetFqdn(string shortNodeName, bool throwException)
		{
			string result;
			try
			{
				ExTraceGlobals.FaultInjectionTracer.TraceTest(3081121085U);
				result = base.GetFqdn(shortNodeName, true);
			}
			catch (AmServerNameResolveFqdnException ex)
			{
				AmTrace.Error("FQDN resolution of the short name {0} failed. Error: {1}", new object[]
				{
					shortNodeName,
					ex.Message
				});
				ReplayEventLogConstants.Tuple_FqdnResolutionFailure.LogEvent(ExTraceGlobals.ActiveManagerTracer.GetHashCode().ToString(), new object[]
				{
					shortNodeName,
					ex.Message
				});
				result = AmServerNameCache.GetFqdnWithLocalDomainSuffix(shortNodeName);
			}
			return result;
		}
	}
}
