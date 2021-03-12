using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.SecureMail
{
	// Token: 0x02000555 RID: 1365
	internal static class SecureMailTransportPerfCounters
	{
		// Token: 0x06003EDF RID: 16095 RVA: 0x0010D92A File Offset: 0x0010BB2A
		public static SecureMailTransportPerfCountersInstance GetInstance(string instanceName)
		{
			return (SecureMailTransportPerfCountersInstance)SecureMailTransportPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003EE0 RID: 16096 RVA: 0x0010D93C File Offset: 0x0010BB3C
		public static void CloseInstance(string instanceName)
		{
			SecureMailTransportPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003EE1 RID: 16097 RVA: 0x0010D949 File Offset: 0x0010BB49
		public static bool InstanceExists(string instanceName)
		{
			return SecureMailTransportPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003EE2 RID: 16098 RVA: 0x0010D956 File Offset: 0x0010BB56
		public static string[] GetInstanceNames()
		{
			return SecureMailTransportPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003EE3 RID: 16099 RVA: 0x0010D962 File Offset: 0x0010BB62
		public static void RemoveInstance(string instanceName)
		{
			SecureMailTransportPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003EE4 RID: 16100 RVA: 0x0010D96F File Offset: 0x0010BB6F
		public static void ResetInstance(string instanceName)
		{
			SecureMailTransportPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003EE5 RID: 16101 RVA: 0x0010D97C File Offset: 0x0010BB7C
		public static void RemoveAllInstances()
		{
			SecureMailTransportPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003EE6 RID: 16102 RVA: 0x0010D988 File Offset: 0x0010BB88
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new SecureMailTransportPerfCountersInstance(instanceName, (SecureMailTransportPerfCountersInstance)totalInstance);
		}

		// Token: 0x06003EE7 RID: 16103 RVA: 0x0010D996 File Offset: 0x0010BB96
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new SecureMailTransportPerfCountersInstance(instanceName);
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x0010D99E File Offset: 0x0010BB9E
		public static void GetPerfCounterInfo(XElement element)
		{
			if (SecureMailTransportPerfCounters.counters == null)
			{
				return;
			}
			SecureMailTransportPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04002307 RID: 8967
		public const string CategoryName = "MSExchange Secure Mail Transport";

		// Token: 0x04002308 RID: 8968
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Secure Mail Transport", new CreateInstanceDelegate(SecureMailTransportPerfCounters.CreateInstance));
	}
}
