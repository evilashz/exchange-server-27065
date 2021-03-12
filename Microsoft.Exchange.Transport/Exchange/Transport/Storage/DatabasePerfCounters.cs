using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x0200055D RID: 1373
	internal static class DatabasePerfCounters
	{
		// Token: 0x06003F17 RID: 16151 RVA: 0x0010EB90 File Offset: 0x0010CD90
		public static DatabasePerfCountersInstance GetInstance(string instanceName)
		{
			return (DatabasePerfCountersInstance)DatabasePerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003F18 RID: 16152 RVA: 0x0010EBA2 File Offset: 0x0010CDA2
		public static void CloseInstance(string instanceName)
		{
			DatabasePerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x0010EBAF File Offset: 0x0010CDAF
		public static bool InstanceExists(string instanceName)
		{
			return DatabasePerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003F1A RID: 16154 RVA: 0x0010EBBC File Offset: 0x0010CDBC
		public static string[] GetInstanceNames()
		{
			return DatabasePerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003F1B RID: 16155 RVA: 0x0010EBC8 File Offset: 0x0010CDC8
		public static void RemoveInstance(string instanceName)
		{
			DatabasePerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x0010EBD5 File Offset: 0x0010CDD5
		public static void ResetInstance(string instanceName)
		{
			DatabasePerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003F1D RID: 16157 RVA: 0x0010EBE2 File Offset: 0x0010CDE2
		public static void RemoveAllInstances()
		{
			DatabasePerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003F1E RID: 16158 RVA: 0x0010EBEE File Offset: 0x0010CDEE
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new DatabasePerfCountersInstance(instanceName, (DatabasePerfCountersInstance)totalInstance);
		}

		// Token: 0x06003F1F RID: 16159 RVA: 0x0010EBFC File Offset: 0x0010CDFC
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new DatabasePerfCountersInstance(instanceName);
		}

		// Token: 0x170012E3 RID: 4835
		// (get) Token: 0x06003F20 RID: 16160 RVA: 0x0010EC04 File Offset: 0x0010CE04
		public static DatabasePerfCountersInstance TotalInstance
		{
			get
			{
				return (DatabasePerfCountersInstance)DatabasePerfCounters.counters.TotalInstance;
			}
		}

		// Token: 0x06003F21 RID: 16161 RVA: 0x0010EC15 File Offset: 0x0010CE15
		public static void GetPerfCounterInfo(XElement element)
		{
			if (DatabasePerfCounters.counters == null)
			{
				return;
			}
			DatabasePerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04002327 RID: 8999
		public const string CategoryName = "MSExchangeTransport Database";

		// Token: 0x04002328 RID: 9000
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangeTransport Database", new CreateInstanceDelegate(DatabasePerfCounters.CreateInstance), new CreateTotalInstanceDelegate(DatabasePerfCounters.CreateTotalInstance));
	}
}
