using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000F2 RID: 242
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MoveCsvDataRowProvider : MigrationCSVDataRowProvider
	{
		// Token: 0x06000C1F RID: 3103 RVA: 0x00034FA0 File Offset: 0x000331A0
		public MoveCsvDataRowProvider(MigrationJob job, IMigrationDataProvider dataProvider) : base(job, dataProvider, MigrationCSVDataRowProvider.CreateCsvSchema(job))
		{
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00034FB0 File Offset: 0x000331B0
		protected override IMigrationDataRow CreateDataRow(CsvRow row)
		{
			InvalidDataRow invalidDataRow = base.GetInvalidDataRow(row, base.MigrationJob.MigrationType);
			if (invalidDataRow != null)
			{
				return invalidDataRow;
			}
			MoveJobSubscriptionSettings moveJobSubscriptionSettings = base.MigrationJob.SubscriptionSettings as MoveJobSubscriptionSettings;
			return new MoveMigrationDataRow(row.Index, row["EmailAddress"], base.MigrationJob.JobDirection, row, moveJobSubscriptionSettings != null && moveJobSubscriptionSettings.ArchiveOnly != null && moveJobSubscriptionSettings.ArchiveOnly.Value);
		}
	}
}
