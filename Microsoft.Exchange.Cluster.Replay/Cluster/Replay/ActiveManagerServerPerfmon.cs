using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000387 RID: 903
	internal static class ActiveManagerServerPerfmon
	{
		// Token: 0x06002454 RID: 9300 RVA: 0x000A9D80 File Offset: 0x000A7F80
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ActiveManagerServerPerfmon.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in ActiveManagerServerPerfmon.AllCounters)
			{
				try
				{
					element.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					element.Add(content);
				}
			}
		}

		// Token: 0x04000F6E RID: 3950
		public const string CategoryName = "MSExchange Active Manager Server";

		// Token: 0x04000F6F RID: 3951
		public static readonly ExPerformanceCounter GetServerForDatabaseServerCalls = new ExPerformanceCounter("MSExchange Active Manager Server", "GetServerForDatabase Server-Side Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F70 RID: 3952
		public static readonly ExPerformanceCounter GetServerForDatabaseServerCallsPerSec = new ExPerformanceCounter("MSExchange Active Manager Server", "Server-Side Calls/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F71 RID: 3953
		public static readonly ExPerformanceCounter DatabaseStateInfoWrites = new ExPerformanceCounter("MSExchange Active Manager Server", "Active Manager Database State writes to Persistent storage", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F72 RID: 3954
		public static readonly ExPerformanceCounter DatabaseStateInfoWritesPerSec = new ExPerformanceCounter("MSExchange Active Manager Server", "Active Manager Database State writes to Persistent storage/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F73 RID: 3955
		public static readonly ExPerformanceCounter CountOfDatabases = new ExPerformanceCounter("MSExchange Active Manager Server", "Total Number of Databases", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F74 RID: 3956
		public static readonly ExPerformanceCounter ActiveManagerRole = new ExPerformanceCounter("MSExchange Active Manager Server", "Active Manager Role", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F75 RID: 3957
		public static readonly ExPerformanceCounter ClusterBatchWriteCalls = new ExPerformanceCounter("MSExchange Active Manager Server", "All cluster batch writes issued on the local node", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F76 RID: 3958
		public static readonly ExPerformanceCounter LastLogRemoteUpdateRpcAttempted = new ExPerformanceCounter("MSExchange Active Manager Server", "LastLog cluster batch remote updates attempted", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F77 RID: 3959
		public static readonly ExPerformanceCounter LastLogRemoteUpdateRpcFailed = new ExPerformanceCounter("MSExchange Active Manager Server", "LastLog cluster batch remote updates failed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F78 RID: 3960
		public static readonly ExPerformanceCounter LastLogLocalClusterBatchUpdatesAttempted = new ExPerformanceCounter("MSExchange Active Manager Server", "LastLog cluster batch local updates attempted", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F79 RID: 3961
		public static readonly ExPerformanceCounter LastLogLocalClusterBatchUpdatesFailed = new ExPerformanceCounter("MSExchange Active Manager Server", "LastLog cluster batch local updates failed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F7A RID: 3962
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			ActiveManagerServerPerfmon.GetServerForDatabaseServerCalls,
			ActiveManagerServerPerfmon.GetServerForDatabaseServerCallsPerSec,
			ActiveManagerServerPerfmon.DatabaseStateInfoWrites,
			ActiveManagerServerPerfmon.DatabaseStateInfoWritesPerSec,
			ActiveManagerServerPerfmon.CountOfDatabases,
			ActiveManagerServerPerfmon.ActiveManagerRole,
			ActiveManagerServerPerfmon.ClusterBatchWriteCalls,
			ActiveManagerServerPerfmon.LastLogRemoteUpdateRpcAttempted,
			ActiveManagerServerPerfmon.LastLogRemoteUpdateRpcFailed,
			ActiveManagerServerPerfmon.LastLogLocalClusterBatchUpdatesAttempted,
			ActiveManagerServerPerfmon.LastLogLocalClusterBatchUpdatesFailed
		};
	}
}
