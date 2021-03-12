using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Assistants.Diagnostics
{
	// Token: 0x0200009D RID: 157
	internal class DiagnosticsSummaryDatabase
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0001909A File Offset: 0x0001729A
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x000190A2 File Offset: 0x000172A2
		public DiagnosticsSummaryJobWindow WindowJobStatistics { get; private set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x000190AB File Offset: 0x000172AB
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x000190B3 File Offset: 0x000172B3
		public DiagnosticsSummaryJob OnDemandJobsStatistics { get; private set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x000190BC File Offset: 0x000172BC
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x000190C4 File Offset: 0x000172C4
		public bool IsAssistantEnabled { get; private set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x000190CD File Offset: 0x000172CD
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x000190D5 File Offset: 0x000172D5
		public DateTime StartTime { get; private set; }

		// Token: 0x060004A8 RID: 1192 RVA: 0x000190DE File Offset: 0x000172DE
		public DiagnosticsSummaryDatabase() : this(true, DateTime.MinValue, new DiagnosticsSummaryJobWindow(), new DiagnosticsSummaryJob())
		{
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000190F6 File Offset: 0x000172F6
		public DiagnosticsSummaryDatabase(bool isAssistantEnabled, DateTime startTime, DiagnosticsSummaryJobWindow window, DiagnosticsSummaryJob demand)
		{
			ArgumentValidator.ThrowIfNull("window", window);
			ArgumentValidator.ThrowIfNull("demand", demand);
			this.IsAssistantEnabled = isAssistantEnabled;
			this.StartTime = startTime;
			this.WindowJobStatistics = window;
			this.OnDemandJobsStatistics = demand;
		}
	}
}
