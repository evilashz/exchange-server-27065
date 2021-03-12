using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.CalendarSync
{
	// Token: 0x020000CB RID: 203
	internal static class CalendarSyncPerformanceCounters
	{
		// Token: 0x060008AC RID: 2220 RVA: 0x0003B740 File Offset: 0x00039940
		public static CalendarSyncPerformanceCountersInstance GetInstance(string instanceName)
		{
			return (CalendarSyncPerformanceCountersInstance)CalendarSyncPerformanceCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0003B752 File Offset: 0x00039952
		public static void CloseInstance(string instanceName)
		{
			CalendarSyncPerformanceCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0003B75F File Offset: 0x0003995F
		public static bool InstanceExists(string instanceName)
		{
			return CalendarSyncPerformanceCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0003B76C File Offset: 0x0003996C
		public static string[] GetInstanceNames()
		{
			return CalendarSyncPerformanceCounters.counters.GetInstanceNames();
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0003B778 File Offset: 0x00039978
		public static void RemoveInstance(string instanceName)
		{
			CalendarSyncPerformanceCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0003B785 File Offset: 0x00039985
		public static void ResetInstance(string instanceName)
		{
			CalendarSyncPerformanceCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0003B792 File Offset: 0x00039992
		public static void RemoveAllInstances()
		{
			CalendarSyncPerformanceCounters.counters.RemoveAllInstances();
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0003B79E File Offset: 0x0003999E
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new CalendarSyncPerformanceCountersInstance(instanceName, (CalendarSyncPerformanceCountersInstance)totalInstance);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0003B7AC File Offset: 0x000399AC
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new CalendarSyncPerformanceCountersInstance(instanceName);
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0003B7B4 File Offset: 0x000399B4
		public static CalendarSyncPerformanceCountersInstance TotalInstance
		{
			get
			{
				return (CalendarSyncPerformanceCountersInstance)CalendarSyncPerformanceCounters.counters.TotalInstance;
			}
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0003B7C5 File Offset: 0x000399C5
		public static void GetPerfCounterInfo(XElement element)
		{
			if (CalendarSyncPerformanceCounters.counters == null)
			{
				return;
			}
			CalendarSyncPerformanceCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000603 RID: 1539
		public const string CategoryName = "MSExchange Calendar Sync Assistant";

		// Token: 0x04000604 RID: 1540
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchange Calendar Sync Assistant", new CreateInstanceDelegate(CalendarSyncPerformanceCounters.CreateInstance), new CreateTotalInstanceDelegate(CalendarSyncPerformanceCounters.CreateTotalInstance));
	}
}
