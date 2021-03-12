using System;
using System.Collections.Generic;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200003D RID: 61
	public class CorrelatedMonitorMatchInfo
	{
		// Token: 0x060004BB RID: 1211 RVA: 0x00011E5F File Offset: 0x0001005F
		internal CorrelatedMonitorMatchInfo(CorrelatedMonitorInfo monitorInfo)
		{
			this.CorrelatedMonitorInfo = monitorInfo;
			this.DetailedMonitorResultMap = new Dictionary<int, CorrelatedMonitorMatchInfo.MonitorResultDetailed>();
			this.MatchingMonitorResultsDetailed = new List<CorrelatedMonitorMatchInfo.MonitorResultDetailed>();
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00011E84 File Offset: 0x00010084
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x00011E8C File Offset: 0x0001008C
		internal CorrelatedMonitorInfo CorrelatedMonitorInfo { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00011E95 File Offset: 0x00010095
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x00011E9D File Offset: 0x0001009D
		internal Dictionary<int, CorrelatedMonitorMatchInfo.MonitorResultDetailed> DetailedMonitorResultMap { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00011EA6 File Offset: 0x000100A6
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x00011EAE File Offset: 0x000100AE
		internal List<CorrelatedMonitorMatchInfo.MonitorResultDetailed> MatchingMonitorResultsDetailed { get; set; }

		// Token: 0x0200003E RID: 62
		public class MonitorResultDetailed
		{
			// Token: 0x060004C2 RID: 1218 RVA: 0x00011EB7 File Offset: 0x000100B7
			internal MonitorResultDetailed(MonitorDefinition definition)
			{
				this.Definition = definition;
			}

			// Token: 0x170001AB RID: 427
			// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00011EC6 File Offset: 0x000100C6
			// (set) Token: 0x060004C4 RID: 1220 RVA: 0x00011ECE File Offset: 0x000100CE
			internal MonitorDefinition Definition { get; set; }

			// Token: 0x170001AC RID: 428
			// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00011ED7 File Offset: 0x000100D7
			// (set) Token: 0x060004C6 RID: 1222 RVA: 0x00011EDF File Offset: 0x000100DF
			internal MonitorResult Result { get; set; }
		}
	}
}
