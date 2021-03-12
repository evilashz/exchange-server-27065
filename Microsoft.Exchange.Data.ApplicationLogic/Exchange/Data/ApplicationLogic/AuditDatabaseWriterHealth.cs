using System;
using System.Linq;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x02000096 RID: 150
	public class AuditDatabaseWriterHealth
	{
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00017EF4 File Offset: 0x000160F4
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x00017EFC File Offset: 0x000160FC
		public int FailedBatchCount { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00017F05 File Offset: 0x00016105
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x00017F0D File Offset: 0x0001610D
		public int RecordCount { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00017F16 File Offset: 0x00016116
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x00017F1E File Offset: 0x0001611E
		public int BatchRetryCount { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00017F40 File Offset: 0x00016140
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x00017F9C File Offset: 0x0001619C
		public RecordProcessingResult[] BadRecords
		{
			get
			{
				return (from br in this.badRecords
				where br.Exception != null
				orderby br.Index
				select br).ToArray<RecordProcessingResult>();
			}
			set
			{
				throw new NotSupportedException("Do not set BadRecords.");
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00017FA8 File Offset: 0x000161A8
		public void Add(RecordProcessingResult result)
		{
			this.badRecords[result.Index] = result;
		}

		// Token: 0x040002DE RID: 734
		public const int MaxRecordProcessingResultCount = 25;

		// Token: 0x040002DF RID: 735
		private RecordProcessingResult[] badRecords = new RecordProcessingResult[25];
	}
}
