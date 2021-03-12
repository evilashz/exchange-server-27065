using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000107 RID: 263
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MigrationJobProcessorResponse : MigrationProcessorResponse
	{
		// Token: 0x06000DBC RID: 3516 RVA: 0x00038CA2 File Offset: 0x00036EA2
		private MigrationJobProcessorResponse(MigrationProcessorResult result, LocalizedException error = null, string lastProcessedRow = null, string batchInputId = null, MigrationCountCache.MigrationStatusChange childStatusChanges = null) : base(result, error)
		{
			this.LastProcessedRow = lastProcessedRow;
			this.ChildStatusChanges = (childStatusChanges ?? MigrationCountCache.MigrationStatusChange.None);
			this.BatchInputId = batchInputId;
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x00038CCC File Offset: 0x00036ECC
		// (set) Token: 0x06000DBE RID: 3518 RVA: 0x00038CD4 File Offset: 0x00036ED4
		public string LastProcessedRow { get; set; }

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x00038CDD File Offset: 0x00036EDD
		// (set) Token: 0x06000DC0 RID: 3520 RVA: 0x00038CE5 File Offset: 0x00036EE5
		public string BatchInputId { get; private set; }

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x00038CEE File Offset: 0x00036EEE
		// (set) Token: 0x06000DC2 RID: 3522 RVA: 0x00038CF6 File Offset: 0x00036EF6
		public MigrationCountCache.MigrationStatusChange ChildStatusChanges { get; private set; }

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00038D00 File Offset: 0x00036F00
		internal static MigrationJobProcessorResponse Create(MigrationProcessorResult result, TimeSpan? delayTime = null, LocalizedException error = null, string lastProcessedRow = null, string batchInputId = null, MigrationCountCache.MigrationStatusChange childStatusChanges = null)
		{
			MigrationJobProcessorResponse migrationJobProcessorResponse = new MigrationJobProcessorResponse(result, error, lastProcessedRow, batchInputId, childStatusChanges);
			if (delayTime != null)
			{
				migrationJobProcessorResponse.DelayTime = delayTime;
			}
			return migrationJobProcessorResponse;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00038D30 File Offset: 0x00036F30
		public override void Aggregate(MigrationProcessorResponse childResponse)
		{
			MigrationJobItemProcessorResponse migrationJobItemProcessorResponse = childResponse as MigrationJobItemProcessorResponse;
			if (migrationJobItemProcessorResponse != null && migrationJobItemProcessorResponse.StatusChange != null)
			{
				this.ChildStatusChanges += migrationJobItemProcessorResponse.StatusChange;
			}
			base.Aggregate(childResponse);
		}
	}
}
