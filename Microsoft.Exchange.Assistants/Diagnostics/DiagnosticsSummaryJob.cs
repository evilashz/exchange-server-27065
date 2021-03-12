using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Assistants.Diagnostics
{
	// Token: 0x0200009E RID: 158
	internal class DiagnosticsSummaryJob
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00019132 File Offset: 0x00017332
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x0001913A File Offset: 0x0001733A
		public int ProcessingCount { get; private set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00019143 File Offset: 0x00017343
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x0001914B File Offset: 0x0001734B
		public int ProcessedSuccessfullyCount { get; private set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00019154 File Offset: 0x00017354
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x0001915C File Offset: 0x0001735C
		public int ProcessedFailureCount { get; private set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00019165 File Offset: 0x00017365
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x0001916D File Offset: 0x0001736D
		public int FailedToOpenStoreSessionCount { get; private set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00019176 File Offset: 0x00017376
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x0001917E File Offset: 0x0001737E
		public int RetriedCount { get; private set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00019187 File Offset: 0x00017387
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x0001918F File Offset: 0x0001738F
		public int QueuedCount { get; private set; }

		// Token: 0x060004B6 RID: 1206 RVA: 0x00019198 File Offset: 0x00017398
		public DiagnosticsSummaryJob()
		{
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x000191A0 File Offset: 0x000173A0
		public DiagnosticsSummaryJob(int processing, int processedSuccessfully, int processedFailure, int failedToOpenStoreSession, int retriedCount, int queued)
		{
			this.ProcessingCount = processing;
			this.ProcessedSuccessfullyCount = processedSuccessfully;
			this.ProcessedFailureCount = processedFailure;
			this.FailedToOpenStoreSessionCount = failedToOpenStoreSession;
			this.RetriedCount = retriedCount;
			this.QueuedCount = queued;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000191D8 File Offset: 0x000173D8
		public void AddMoreSummary(DiagnosticsSummaryJob mbxDiagnosticsSummary)
		{
			ArgumentValidator.ThrowIfNull("mbxDiagnosticsSummary", mbxDiagnosticsSummary);
			this.ProcessingCount += mbxDiagnosticsSummary.ProcessingCount;
			this.ProcessedSuccessfullyCount += mbxDiagnosticsSummary.ProcessedSuccessfullyCount;
			this.ProcessedFailureCount += mbxDiagnosticsSummary.ProcessedFailureCount;
			this.FailedToOpenStoreSessionCount += mbxDiagnosticsSummary.FailedToOpenStoreSessionCount;
			this.RetriedCount += mbxDiagnosticsSummary.RetriedCount;
			this.QueuedCount += mbxDiagnosticsSummary.QueuedCount;
		}
	}
}
