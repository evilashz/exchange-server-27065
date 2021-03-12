using System;
using Microsoft.Office.Compliance.Audit;
using Microsoft.Office.Compliance.Audit.Schema;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x02000097 RID: 151
	public struct RecordProcessingResult
	{
		// Token: 0x0600069B RID: 1691 RVA: 0x00017FD8 File Offset: 0x000161D8
		public RecordProcessingResult(AuditRecord record, Exception exception, bool retry)
		{
			this = default(RecordProcessingResult);
			this.RecordType = record.RecordType;
			this.FailTime = DateTime.UtcNow;
			this.RecordId = record.Id;
			this.Exception = ExceptionDetails.FromException(exception);
			this.Retry = retry;
			this.Index = RecordProcessingResult.count++;
			RecordProcessingResult.count %= 25;
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x00018042 File Offset: 0x00016242
		// (set) Token: 0x0600069D RID: 1693 RVA: 0x0001804A File Offset: 0x0001624A
		public AuditLogRecordType RecordType { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00018053 File Offset: 0x00016253
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x0001805B File Offset: 0x0001625B
		public DateTime FailTime { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00018064 File Offset: 0x00016264
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x0001806C File Offset: 0x0001626C
		public Guid RecordId { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00018075 File Offset: 0x00016275
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x0001807D File Offset: 0x0001627D
		public bool Retry { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00018086 File Offset: 0x00016286
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x0001808E File Offset: 0x0001628E
		public ExceptionDetails Exception { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00018097 File Offset: 0x00016297
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x0001809F File Offset: 0x0001629F
		internal int Index { get; set; }

		// Token: 0x040002E5 RID: 741
		private static int count;
	}
}
