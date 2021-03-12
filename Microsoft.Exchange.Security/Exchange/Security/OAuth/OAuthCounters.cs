using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x02000009 RID: 9
	internal static class OAuthCounters
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000051DC File Offset: 0x000033DC
		public static OAuthCountersInstance GetInstance(string instanceName)
		{
			return (OAuthCountersInstance)OAuthCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000051EE File Offset: 0x000033EE
		public static void CloseInstance(string instanceName)
		{
			OAuthCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000051FB File Offset: 0x000033FB
		public static bool InstanceExists(string instanceName)
		{
			return OAuthCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00005208 File Offset: 0x00003408
		public static string[] GetInstanceNames()
		{
			return OAuthCounters.counters.GetInstanceNames();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00005214 File Offset: 0x00003414
		public static void RemoveInstance(string instanceName)
		{
			OAuthCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00005221 File Offset: 0x00003421
		public static void ResetInstance(string instanceName)
		{
			OAuthCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000522E File Offset: 0x0000342E
		public static void RemoveAllInstances()
		{
			OAuthCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000523A File Offset: 0x0000343A
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new OAuthCountersInstance(instanceName, (OAuthCountersInstance)totalInstance);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00005248 File Offset: 0x00003448
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new OAuthCountersInstance(instanceName);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00005250 File Offset: 0x00003450
		public static void GetPerfCounterInfo(XElement element)
		{
			if (OAuthCounters.counters == null)
			{
				return;
			}
			OAuthCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040000E4 RID: 228
		public const string CategoryName = "MSExchange OAuth";

		// Token: 0x040000E5 RID: 229
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange OAuth", new CreateInstanceDelegate(OAuthCounters.CreateInstance));
	}
}
