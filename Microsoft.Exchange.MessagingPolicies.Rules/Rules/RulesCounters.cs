using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x020000B5 RID: 181
	internal static class RulesCounters
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x0001870E File Offset: 0x0001690E
		public static RulesCountersInstance GetInstance(string instanceName)
		{
			return (RulesCountersInstance)RulesCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00018720 File Offset: 0x00016920
		public static void CloseInstance(string instanceName)
		{
			RulesCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001872D File Offset: 0x0001692D
		public static bool InstanceExists(string instanceName)
		{
			return RulesCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001873A File Offset: 0x0001693A
		public static string[] GetInstanceNames()
		{
			return RulesCounters.counters.GetInstanceNames();
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00018746 File Offset: 0x00016946
		public static void RemoveInstance(string instanceName)
		{
			RulesCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00018753 File Offset: 0x00016953
		public static void ResetInstance(string instanceName)
		{
			RulesCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00018760 File Offset: 0x00016960
		public static void RemoveAllInstances()
		{
			RulesCounters.counters.RemoveAllInstances();
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001876C File Offset: 0x0001696C
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new RulesCountersInstance(instanceName, (RulesCountersInstance)totalInstance);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001877A File Offset: 0x0001697A
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new RulesCountersInstance(instanceName);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00018782 File Offset: 0x00016982
		public static void GetPerfCounterInfo(XElement element)
		{
			if (RulesCounters.counters == null)
			{
				return;
			}
			RulesCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000354 RID: 852
		public const string CategoryName = "MSExchange Transport Rules";

		// Token: 0x04000355 RID: 853
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Transport Rules", new CreateInstanceDelegate(RulesCounters.CreateInstance));
	}
}
