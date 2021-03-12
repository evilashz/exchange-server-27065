using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000F4 RID: 244
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationPreexistingBatchCsvDataRowProvider : MigrationCSVDataRowProvider
	{
		// Token: 0x06000C3C RID: 3132 RVA: 0x00035351 File Offset: 0x00033551
		public MigrationPreexistingBatchCsvDataRowProvider(MigrationJob job, IMigrationDataProvider dataProvider) : base(job, dataProvider, new MigrationPreexistingBatchCsvSchema())
		{
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00035360 File Offset: 0x00033560
		protected override IMigrationDataRow CreateDataRow(CsvRow row)
		{
			string text = row["JobItemGuid"];
			Guid identity;
			if (Guid.TryParse(text, out identity))
			{
				MigrationJobItem byGuid = MigrationJobItem.GetByGuid(base.DataProvider, identity);
				if (byGuid != null)
				{
					if ((base.MigrationJob.BatchFlags & MigrationBatchFlags.DisableOnCopy) == MigrationBatchFlags.DisableOnCopy && byGuid.State != MigrationState.Disabled)
					{
						MigrationLogger.Log(MigrationEventType.Verbose, "disabling old migration job item {0}", new object[]
						{
							byGuid
						});
						byGuid.SetStatus(base.DataProvider, MigrationUserStatus.Failed, MigrationState.Disabled, null, null, null, null, null, null, false, new MigrationUserMovedToAnotherBatchException(base.MigrationJob.JobName));
					}
					return new MigrationPreexistingDataRow(row.Index, byGuid);
				}
			}
			return new InvalidDataRow(row.Index, new MigrationBatchError
			{
				RowIndex = row.Index,
				EmailAddress = text,
				LocalizedErrorMessage = Strings.MigrationJobItemNotFound(text)
			}, base.MigrationJob.MigrationType);
		}
	}
}
