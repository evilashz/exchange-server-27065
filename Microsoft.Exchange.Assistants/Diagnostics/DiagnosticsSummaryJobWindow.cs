using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Assistants.Diagnostics
{
	// Token: 0x0200009F RID: 159
	internal class DiagnosticsSummaryJobWindow
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00019262 File Offset: 0x00017462
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x0001926A File Offset: 0x0001746A
		public int TotalOnDatabaseCount { get; private set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00019273 File Offset: 0x00017473
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x0001927B File Offset: 0x0001747B
		public int InterestingCount { get; private set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00019284 File Offset: 0x00017484
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x0001928C File Offset: 0x0001748C
		public int NotInterestingCount { get; private set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00019295 File Offset: 0x00017495
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x0001929D File Offset: 0x0001749D
		public int FilteredMailboxCount { get; private set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x000192A6 File Offset: 0x000174A6
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x000192AE File Offset: 0x000174AE
		public int FailedFilteringCount { get; private set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x000192B7 File Offset: 0x000174B7
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x000192BF File Offset: 0x000174BF
		public int ProcessedSeparatelyCount { get; private set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x000192C8 File Offset: 0x000174C8
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x000192D0 File Offset: 0x000174D0
		public DiagnosticsSummaryJob DiagnosticsSummaryJob { get; private set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x000192D9 File Offset: 0x000174D9
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x000192E1 File Offset: 0x000174E1
		public DateTime StartTime { get; private set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x000192EA File Offset: 0x000174EA
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x000192F2 File Offset: 0x000174F2
		public DateTime EndTime { get; private set; }

		// Token: 0x060004CB RID: 1227 RVA: 0x000192FC File Offset: 0x000174FC
		public DiagnosticsSummaryJobWindow() : this(0, 0, 0, 0, 0, 0, DateTime.MinValue, DateTime.MinValue, new DiagnosticsSummaryJob())
		{
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00019324 File Offset: 0x00017524
		public DiagnosticsSummaryJobWindow(int totalOnDatabase, int interesting, int notInteresting, int filtered, int failedFiltering, int processedSeparately, DateTime start, DateTime end, DiagnosticsSummaryJob summaryJob)
		{
			ArgumentValidator.ThrowIfNull("summaryJob", summaryJob);
			this.TotalOnDatabaseCount = totalOnDatabase;
			this.InterestingCount = interesting;
			this.NotInterestingCount = notInteresting;
			this.FilteredMailboxCount = filtered;
			this.FailedFilteringCount = failedFiltering;
			this.ProcessedSeparatelyCount = processedSeparately;
			this.StartTime = start;
			this.EndTime = end;
			this.DiagnosticsSummaryJob = summaryJob;
		}
	}
}
