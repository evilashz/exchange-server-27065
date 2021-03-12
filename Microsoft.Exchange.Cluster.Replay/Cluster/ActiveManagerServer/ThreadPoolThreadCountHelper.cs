using System;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000004 RID: 4
	internal static class ThreadPoolThreadCountHelper
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020D0 File Offset: 0x000002D0
		internal static void IncreaseForDatabaseOperations(int numDatabaseOps)
		{
			int numThreadsPerPamDbOperation = RegistryParameters.NumThreadsPerPamDbOperation;
			ThreadPoolThreadCountHelper.IncrementBy(numThreadsPerPamDbOperation * numDatabaseOps);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020EC File Offset: 0x000002EC
		internal static void IncreaseForServerOperations(AmConfig cfg)
		{
			int threadCount = cfg.IsPamOrSam ? cfg.DagConfig.MemberServers.Length : 1;
			ThreadPoolThreadCountHelper.IncrementBy(threadCount);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002118 File Offset: 0x00000318
		internal static void IncrementBy(int threadCount)
		{
			Dependencies.ThreadPoolThreadCountManager.IncrementMinThreadsBy(threadCount, null);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000213A File Offset: 0x0000033A
		internal static void Reset()
		{
			Dependencies.ThreadPoolThreadCountManager.Reset(true);
		}
	}
}
