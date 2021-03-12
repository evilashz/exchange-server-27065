using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x02000007 RID: 7
	internal static class LiveIdBasicAuthenticationCounters
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000294C File Offset: 0x00000B4C
		public static LiveIdBasicAuthenticationCountersInstance GetInstance(string instanceName)
		{
			return (LiveIdBasicAuthenticationCountersInstance)LiveIdBasicAuthenticationCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000295E File Offset: 0x00000B5E
		public static void CloseInstance(string instanceName)
		{
			LiveIdBasicAuthenticationCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000296B File Offset: 0x00000B6B
		public static bool InstanceExists(string instanceName)
		{
			return LiveIdBasicAuthenticationCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002978 File Offset: 0x00000B78
		public static string[] GetInstanceNames()
		{
			return LiveIdBasicAuthenticationCounters.counters.GetInstanceNames();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002984 File Offset: 0x00000B84
		public static void RemoveInstance(string instanceName)
		{
			LiveIdBasicAuthenticationCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002991 File Offset: 0x00000B91
		public static void ResetInstance(string instanceName)
		{
			LiveIdBasicAuthenticationCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000299E File Offset: 0x00000B9E
		public static void RemoveAllInstances()
		{
			LiveIdBasicAuthenticationCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000029AA File Offset: 0x00000BAA
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new LiveIdBasicAuthenticationCountersInstance(instanceName, (LiveIdBasicAuthenticationCountersInstance)totalInstance);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000029B8 File Offset: 0x00000BB8
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new LiveIdBasicAuthenticationCountersInstance(instanceName);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000029C0 File Offset: 0x00000BC0
		public static void GetPerfCounterInfo(XElement element)
		{
			if (LiveIdBasicAuthenticationCounters.counters == null)
			{
				return;
			}
			LiveIdBasicAuthenticationCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000091 RID: 145
		public const string CategoryName = "MSExchange LiveIdBasicAuthentication";

		// Token: 0x04000092 RID: 146
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange LiveIdBasicAuthentication", new CreateInstanceDelegate(LiveIdBasicAuthenticationCounters.CreateInstance));
	}
}
