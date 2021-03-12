using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x020000A8 RID: 168
	internal static class LocalDataAccessPerfCounters
	{
		// Token: 0x060007E8 RID: 2024 RVA: 0x00020EB4 File Offset: 0x0001F0B4
		public static void GetPerfCounterInfo(XElement element)
		{
			if (LocalDataAccessPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in LocalDataAccessPerfCounters.AllCounters)
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

		// Token: 0x0400060B RID: 1547
		public const string CategoryName = "MSExchangeWorkerTaskFrameworkLocalDataAccess";

		// Token: 0x0400060C RID: 1548
		public static readonly ExPerformanceCounter LastProbeResultId = new ExPerformanceCounter("MSExchangeWorkerTaskFrameworkLocalDataAccess", "Last Probe Result ID", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400060D RID: 1549
		public static readonly ExPerformanceCounter LastMonitorResultId = new ExPerformanceCounter("MSExchangeWorkerTaskFrameworkLocalDataAccess", "Last Monitor Result ID", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400060E RID: 1550
		public static readonly ExPerformanceCounter LastResponderResultId = new ExPerformanceCounter("MSExchangeWorkerTaskFrameworkLocalDataAccess", "Last Responder Result ID", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400060F RID: 1551
		public static readonly ExPerformanceCounter LastMaintenanceResultId = new ExPerformanceCounter("MSExchangeWorkerTaskFrameworkLocalDataAccess", "Last Maintenance Result ID", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000610 RID: 1552
		public static readonly ExPerformanceCounter DurationPersistentStateWrite = new ExPerformanceCounter("MSExchangeWorkerTaskFrameworkLocalDataAccess", "Duration of PersistentState Write", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000611 RID: 1553
		public static readonly ExPerformanceCounter DurationPersistentStateRead = new ExPerformanceCounter("MSExchangeWorkerTaskFrameworkLocalDataAccess", "Duration of PersistentState Read", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000612 RID: 1554
		public static readonly ExPerformanceCounter NumberofResultsPersistentStateWrite = new ExPerformanceCounter("MSExchangeWorkerTaskFrameworkLocalDataAccess", "Number of PersistentState results write into the file", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000613 RID: 1555
		public static readonly ExPerformanceCounter NumberofResultsPersistentStateRead = new ExPerformanceCounter("MSExchangeWorkerTaskFrameworkLocalDataAccess", "Number of PersistentState results read from the file", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000614 RID: 1556
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			LocalDataAccessPerfCounters.LastProbeResultId,
			LocalDataAccessPerfCounters.LastMonitorResultId,
			LocalDataAccessPerfCounters.LastResponderResultId,
			LocalDataAccessPerfCounters.LastMaintenanceResultId,
			LocalDataAccessPerfCounters.DurationPersistentStateWrite,
			LocalDataAccessPerfCounters.DurationPersistentStateRead,
			LocalDataAccessPerfCounters.NumberofResultsPersistentStateWrite,
			LocalDataAccessPerfCounters.NumberofResultsPersistentStateRead
		};
	}
}
