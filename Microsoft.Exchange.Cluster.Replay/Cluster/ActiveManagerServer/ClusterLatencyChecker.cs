using System;
using System.Threading;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200004B RID: 75
	internal class ClusterLatencyChecker : IClusApiHook
	{
		// Token: 0x06000333 RID: 819 RVA: 0x0001230C File Offset: 0x0001050C
		internal static void EnableClusterLatencyChecking()
		{
			ClusterLatencyChecker clusApiHook = new ClusterLatencyChecker();
			ClusApiHook.SetClusApiHook(clusApiHook);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00012328 File Offset: 0x00010528
		private void CountEntry(ClusApiHooks api)
		{
			switch (api)
			{
			case ClusApiHooks.OpenCluster:
				Interlocked.Increment(ref ClusterApiStats.OpenCluster);
				return;
			case ClusApiHooks.CloseCluster:
				Interlocked.Increment(ref ClusterApiStats.CloseCluster);
				return;
			case ClusApiHooks.GetClusterKey:
				Interlocked.Increment(ref ClusterApiStats.GetClusterKey);
				return;
			case ClusApiHooks.ClusterRegBatchAddCommand:
				Interlocked.Increment(ref ClusterApiStats.ClusterRegBatchAddCommand);
				return;
			case ClusApiHooks.ClusterRegCreateBatch:
				Interlocked.Increment(ref ClusterApiStats.ClusterRegCreateBatch);
				return;
			case ClusApiHooks.ClusterRegCloseBatch:
				Interlocked.Increment(ref ClusterApiStats.ClusterRegCloseBatch);
				return;
			case ClusApiHooks.ClusterRegOpenKey:
				Interlocked.Increment(ref ClusterApiStats.ClusterRegOpenKey);
				return;
			case ClusApiHooks.ClusterRegCreateKey:
				Interlocked.Increment(ref ClusterApiStats.ClusterRegCreateKey);
				return;
			case ClusApiHooks.ClusterRegQueryValue:
				Interlocked.Increment(ref ClusterApiStats.ClusterRegQueryValue);
				return;
			case ClusApiHooks.ClusterRegSetValue:
				Interlocked.Increment(ref ClusterApiStats.ClusterRegSetValue);
				return;
			case ClusApiHooks.ClusterRegDeleteValue:
				Interlocked.Increment(ref ClusterApiStats.ClusterRegDeleteValue);
				return;
			case ClusApiHooks.ClusterRegDeleteKey:
				Interlocked.Increment(ref ClusterApiStats.ClusterRegDeleteKey);
				return;
			case ClusApiHooks.ClusterRegCloseKey:
				Interlocked.Increment(ref ClusterApiStats.ClusterRegCloseKey);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00012410 File Offset: 0x00010610
		public int CallBack(ClusApiHooks api, string hintStr, Func<int> func)
		{
			this.CountEntry(api);
			int result;
			if (api == ClusApiHooks.ClusterRegCloseBatch)
			{
				ActiveManagerServerPerfmon.ClusterBatchWriteCalls.Increment();
				result = LatencyChecker.MeasureClusApiAndKillIfExceeds(api.ToString(), hintStr, func);
			}
			else
			{
				result = LatencyChecker.MeasureClusApi(api.ToString(), hintStr, func);
			}
			return result;
		}
	}
}
