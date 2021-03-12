using System;
using Microsoft.Exchange.HttpProxy.Common;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000083 RID: 131
	internal static class ConcurrencyGuardHelper
	{
		// Token: 0x060003F7 RID: 1015 RVA: 0x00017D60 File Offset: 0x00015F60
		public static void IncrementTargetBackendDagAndForest(ProxyRequestHandler request)
		{
			string bucketName;
			string text;
			string text2;
			if (ConcurrencyGuardHelper.TryGetBackendDagAndForest(request, out bucketName, out text, out text2))
			{
				ConcurrencyGuards.TargetBackend.Increment(bucketName, request.Logger);
				ConcurrencyGuards.TargetForest.Increment(text2, request.Logger);
				ConcurrencyGuardHelper.GetPerfCounter(text2).OutstandingProxyRequestsToForest.Increment();
				PerfCounters.HttpProxyCountersInstance.OutstandingProxyRequests.Increment();
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00017DC0 File Offset: 0x00015FC0
		public static void DecrementTargetBackendDagAndForest(ProxyRequestHandler request)
		{
			string bucketName;
			string text;
			string text2;
			if (ConcurrencyGuardHelper.TryGetBackendDagAndForest(request, out bucketName, out text, out text2))
			{
				ConcurrencyGuards.TargetBackend.Decrement(bucketName, request.Logger);
				ConcurrencyGuards.TargetForest.Decrement(text2, request.Logger);
				ConcurrencyGuardHelper.GetPerfCounter(text2).OutstandingProxyRequestsToForest.Decrement();
				PerfCounters.HttpProxyCountersInstance.OutstandingProxyRequests.Decrement();
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00017E20 File Offset: 0x00016020
		private static bool TryGetBackendDagAndForest(ProxyRequestHandler request, out string targetFqdn, out string dag, out string forestFqdn)
		{
			dag = null;
			forestFqdn = null;
			targetFqdn = request.ServerRequest.Address.Host.ToLower();
			if (string.IsNullOrWhiteSpace(targetFqdn))
			{
				return false;
			}
			forestFqdn = Utilities.GetForestFqdnFromServerFqdn(targetFqdn);
			return !string.IsNullOrWhiteSpace(forestFqdn);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00017E5F File Offset: 0x0001605F
		private static HttpProxyCountersInstance GetPerfCounter(string instanceName)
		{
			return HttpProxyCounters.GetInstance(HttpProxyGlobals.ProtocolType + "_" + instanceName);
		}
	}
}
