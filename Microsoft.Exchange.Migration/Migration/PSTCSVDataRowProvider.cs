using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Management.Migration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000127 RID: 295
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PSTCSVDataRowProvider : MigrationCSVDataRowProvider, IMigrationDataRowProvider
	{
		// Token: 0x06000F33 RID: 3891 RVA: 0x00041555 File Offset: 0x0003F755
		internal PSTCSVDataRowProvider(MigrationJob job, IMigrationDataProvider dataProvider) : base(job, dataProvider, new PSTImportCsvSchema())
		{
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00041564 File Offset: 0x0003F764
		protected override IMigrationDataRow CreateDataRow(CsvRow row)
		{
			InvalidDataRow invalidDataRow = base.GetInvalidDataRow(row, MigrationType.PSTImport);
			if (invalidDataRow != null)
			{
				return invalidDataRow;
			}
			string text = row["PSTPathFileName"];
			if (string.IsNullOrEmpty(text))
			{
				return base.GetInvalidDataRow(row, Strings.ValueNotProvidedForColumn("PSTPathFileName"), MigrationType.PSTImport);
			}
			SmtpAddress empty = SmtpAddress.Empty;
			string emailAddress = row["TargetMailboxId"];
			if (!MigrationServiceHelper.TryParseSmtpAddress(emailAddress, out empty))
			{
				return base.GetInvalidDataRow(row, Strings.ValueNotProvidedForColumn("TargetMailboxId"), MigrationType.PSTImport);
			}
			MigrationMailboxType targetMailboxType = MigrationMailboxType.PrimaryOnly;
			string value = null;
			if (row.TryGetColumnValue("TargetMailboxType", out value))
			{
				Enum.TryParse<MigrationMailboxType>(value, out targetMailboxType);
			}
			string text2 = null;
			if (row.TryGetColumnValue("SourceRootFolderName", out text2) && string.IsNullOrEmpty(text2))
			{
				text2 = null;
			}
			string text3 = null;
			if (row.TryGetColumnValue("TargetRootFolderName", out text3) && string.IsNullOrEmpty(text3))
			{
				text3 = null;
			}
			return new PSTMigrationDataRow(row.Index, text, empty, targetMailboxType, text2, text3);
		}
	}
}
