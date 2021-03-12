using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000A49 RID: 2633
	internal static class NspiRpcClientConnectionPerformanceCounters
	{
		// Token: 0x0600786B RID: 30827 RVA: 0x0018ED40 File Offset: 0x0018CF40
		public static NspiRpcClientConnectionPerformanceCountersInstance GetInstance(string instanceName)
		{
			return (NspiRpcClientConnectionPerformanceCountersInstance)NspiRpcClientConnectionPerformanceCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x0600786C RID: 30828 RVA: 0x0018ED52 File Offset: 0x0018CF52
		public static void CloseInstance(string instanceName)
		{
			NspiRpcClientConnectionPerformanceCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x0600786D RID: 30829 RVA: 0x0018ED5F File Offset: 0x0018CF5F
		public static bool InstanceExists(string instanceName)
		{
			return NspiRpcClientConnectionPerformanceCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x0600786E RID: 30830 RVA: 0x0018ED6C File Offset: 0x0018CF6C
		public static string[] GetInstanceNames()
		{
			return NspiRpcClientConnectionPerformanceCounters.counters.GetInstanceNames();
		}

		// Token: 0x0600786F RID: 30831 RVA: 0x0018ED78 File Offset: 0x0018CF78
		public static void RemoveInstance(string instanceName)
		{
			NspiRpcClientConnectionPerformanceCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06007870 RID: 30832 RVA: 0x0018ED85 File Offset: 0x0018CF85
		public static void ResetInstance(string instanceName)
		{
			NspiRpcClientConnectionPerformanceCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06007871 RID: 30833 RVA: 0x0018ED92 File Offset: 0x0018CF92
		public static void RemoveAllInstances()
		{
			NspiRpcClientConnectionPerformanceCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06007872 RID: 30834 RVA: 0x0018ED9E File Offset: 0x0018CF9E
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new NspiRpcClientConnectionPerformanceCountersInstance(instanceName, (NspiRpcClientConnectionPerformanceCountersInstance)totalInstance);
		}

		// Token: 0x06007873 RID: 30835 RVA: 0x0018EDAC File Offset: 0x0018CFAC
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new NspiRpcClientConnectionPerformanceCountersInstance(instanceName);
		}

		// Token: 0x06007874 RID: 30836 RVA: 0x0018EDB4 File Offset: 0x0018CFB4
		public static void GetPerfCounterInfo(XElement element)
		{
			if (NspiRpcClientConnectionPerformanceCounters.counters == null)
			{
				return;
			}
			NspiRpcClientConnectionPerformanceCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04004F56 RID: 20310
		public const string CategoryName = "MSExchange NSPI RPC Client Connections";

		// Token: 0x04004F57 RID: 20311
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange NSPI RPC Client Connections", new CreateInstanceDelegate(NspiRpcClientConnectionPerformanceCounters.CreateInstance));
	}
}
