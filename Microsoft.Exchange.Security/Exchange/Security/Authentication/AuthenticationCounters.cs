using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200000B RID: 11
	internal static class AuthenticationCounters
	{
		// Token: 0x0600002C RID: 44 RVA: 0x0000583C File Offset: 0x00003A3C
		public static AuthenticationCountersInstance GetInstance(string instanceName)
		{
			return (AuthenticationCountersInstance)AuthenticationCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000584E File Offset: 0x00003A4E
		public static void CloseInstance(string instanceName)
		{
			AuthenticationCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000585B File Offset: 0x00003A5B
		public static bool InstanceExists(string instanceName)
		{
			return AuthenticationCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00005868 File Offset: 0x00003A68
		public static string[] GetInstanceNames()
		{
			return AuthenticationCounters.counters.GetInstanceNames();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00005874 File Offset: 0x00003A74
		public static void RemoveInstance(string instanceName)
		{
			AuthenticationCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00005881 File Offset: 0x00003A81
		public static void ResetInstance(string instanceName)
		{
			AuthenticationCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000588E File Offset: 0x00003A8E
		public static void RemoveAllInstances()
		{
			AuthenticationCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000589A File Offset: 0x00003A9A
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new AuthenticationCountersInstance(instanceName, (AuthenticationCountersInstance)totalInstance);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000058A8 File Offset: 0x00003AA8
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new AuthenticationCountersInstance(instanceName);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000058B0 File Offset: 0x00003AB0
		public static void GetPerfCounterInfo(XElement element)
		{
			if (AuthenticationCounters.counters == null)
			{
				return;
			}
			AuthenticationCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040000EE RID: 238
		public const string CategoryName = "MSExchange Authentication";

		// Token: 0x040000EF RID: 239
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Authentication", new CreateInstanceDelegate(AuthenticationCounters.CreateInstance));
	}
}
