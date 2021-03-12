using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200038D RID: 909
	internal static class ReplayServerPerfmon
	{
		// Token: 0x06002466 RID: 9318 RVA: 0x000AAE60 File Offset: 0x000A9060
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ReplayServerPerfmon.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in ReplayServerPerfmon.AllCounters)
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

		// Token: 0x04001088 RID: 4232
		public const string CategoryName = "MSExchange Replication Server";

		// Token: 0x04001089 RID: 4233
		public static readonly ExPerformanceCounter GetCopyStatusServerCalls = new ExPerformanceCounter("MSExchange Replication Server", "GetCopyStatus Server-Side Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400108A RID: 4234
		public static readonly ExPerformanceCounter GetCopyStatusServerCallsPerSec = new ExPerformanceCounter("MSExchange Replication Server", "GetCopyStatus Server-Side Calls/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400108B RID: 4235
		public static readonly ExPerformanceCounter WCFGetServerForDatabaseCalls = new ExPerformanceCounter("MSExchange Replication Server", "WCF GetServerForDatabase Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400108C RID: 4236
		public static readonly ExPerformanceCounter WCFGetServerForDatabaseCallsPerSec = new ExPerformanceCounter("MSExchange Replication Server", "WCF GetServerForDatabase Calls/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400108D RID: 4237
		public static readonly ExPerformanceCounter WCFGetAllCalls = new ExPerformanceCounter("MSExchange Replication Server", "WCF GetActiveCopiesForDatabaseAvailabilityGroup Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400108E RID: 4238
		public static readonly ExPerformanceCounter WCFGetAllCallsPerSec = new ExPerformanceCounter("MSExchange Replication Server", "WCF GetActiveCopiesForDatabaseAvailabilityGroup Calls/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400108F RID: 4239
		public static readonly ExPerformanceCounter WCFGetServerForDatabaseCallErrors = new ExPerformanceCounter("MSExchange Replication Server", "WCF Calls returning an error", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001090 RID: 4240
		public static readonly ExPerformanceCounter WCFGetServerForDatabaseCallErrorsPerSec = new ExPerformanceCounter("MSExchange Replication Server", "WCF Calls/sec returning an error", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001091 RID: 4241
		public static readonly ExPerformanceCounter AvgWCFCallLatency = new ExPerformanceCounter("MSExchange Replication Server", "Average WCF GetServerForDatabase call latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001092 RID: 4242
		public static readonly ExPerformanceCounter AvgWCFCallLatencyBase = new ExPerformanceCounter("MSExchange Replication Server", "Base for AvgWCFCallLatency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001093 RID: 4243
		public static readonly ExPerformanceCounter AvgWCFGetAllCallLatency = new ExPerformanceCounter("MSExchange Replication Server", "Average WCF GetActiveCopiesForDatabaseAvailabilityGroup call latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001094 RID: 4244
		public static readonly ExPerformanceCounter AvgWCFGetAllCallLatencyBase = new ExPerformanceCounter("MSExchange Replication Server", "Base for AvgWCFGetAllCallLatency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001095 RID: 4245
		public static readonly ExPerformanceCounter ADConfigRefreshCalls = new ExPerformanceCounter("MSExchange Replication Server", "AD Configuration Refresh Operations", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001096 RID: 4246
		public static readonly ExPerformanceCounter ADConfigRefreshCallsPerSec = new ExPerformanceCounter("MSExchange Replication Server", "AD Configuration Refresh Operations/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001097 RID: 4247
		public static readonly ExPerformanceCounter ADConfigRefreshLatency = new ExPerformanceCounter("MSExchange Replication Server", "Avg. sec/AD Config Refresh", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001098 RID: 4248
		public static readonly ExPerformanceCounter ADConfigRefreshLatencyBase = new ExPerformanceCounter("MSExchange Replication Server", "Base not visible", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001099 RID: 4249
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			ReplayServerPerfmon.GetCopyStatusServerCalls,
			ReplayServerPerfmon.GetCopyStatusServerCallsPerSec,
			ReplayServerPerfmon.WCFGetServerForDatabaseCalls,
			ReplayServerPerfmon.WCFGetServerForDatabaseCallsPerSec,
			ReplayServerPerfmon.WCFGetAllCalls,
			ReplayServerPerfmon.WCFGetAllCallsPerSec,
			ReplayServerPerfmon.WCFGetServerForDatabaseCallErrors,
			ReplayServerPerfmon.WCFGetServerForDatabaseCallErrorsPerSec,
			ReplayServerPerfmon.AvgWCFCallLatency,
			ReplayServerPerfmon.AvgWCFCallLatencyBase,
			ReplayServerPerfmon.AvgWCFGetAllCallLatency,
			ReplayServerPerfmon.AvgWCFGetAllCallLatencyBase,
			ReplayServerPerfmon.ADConfigRefreshCalls,
			ReplayServerPerfmon.ADConfigRefreshCallsPerSec,
			ReplayServerPerfmon.ADConfigRefreshLatency,
			ReplayServerPerfmon.ADConfigRefreshLatencyBase
		};
	}
}
