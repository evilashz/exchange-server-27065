using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000007 RID: 7
	internal static class PerformanceCountersPerAssistant
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002EF8 File Offset: 0x000010F8
		public static PerformanceCountersPerAssistantInstance GetInstance(string instanceName)
		{
			return (PerformanceCountersPerAssistantInstance)PerformanceCountersPerAssistant.counters.GetInstance(instanceName);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002F0A File Offset: 0x0000110A
		public static void CloseInstance(string instanceName)
		{
			PerformanceCountersPerAssistant.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002F17 File Offset: 0x00001117
		public static bool InstanceExists(string instanceName)
		{
			return PerformanceCountersPerAssistant.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002F24 File Offset: 0x00001124
		public static string[] GetInstanceNames()
		{
			return PerformanceCountersPerAssistant.counters.GetInstanceNames();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002F30 File Offset: 0x00001130
		public static void RemoveInstance(string instanceName)
		{
			PerformanceCountersPerAssistant.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002F3D File Offset: 0x0000113D
		public static void ResetInstance(string instanceName)
		{
			PerformanceCountersPerAssistant.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002F4A File Offset: 0x0000114A
		public static void RemoveAllInstances()
		{
			PerformanceCountersPerAssistant.counters.RemoveAllInstances();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002F56 File Offset: 0x00001156
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new PerformanceCountersPerAssistantInstance(instanceName, (PerformanceCountersPerAssistantInstance)totalInstance);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002F64 File Offset: 0x00001164
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new PerformanceCountersPerAssistantInstance(instanceName);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002F6C File Offset: 0x0000116C
		public static PerformanceCountersPerAssistantInstance TotalInstance
		{
			get
			{
				return (PerformanceCountersPerAssistantInstance)PerformanceCountersPerAssistant.counters.TotalInstance;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002F7D File Offset: 0x0000117D
		public static void GetPerfCounterInfo(XElement element)
		{
			if (PerformanceCountersPerAssistant.counters == null)
			{
				return;
			}
			PerformanceCountersPerAssistant.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x0400006F RID: 111
		public const string CategoryName = "MSExchange Assistants - Per Assistant";

		// Token: 0x04000070 RID: 112
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchange Assistants - Per Assistant", new CreateInstanceDelegate(PerformanceCountersPerAssistant.CreateInstance), new CreateTotalInstanceDelegate(PerformanceCountersPerAssistant.CreateTotalInstance));
	}
}
