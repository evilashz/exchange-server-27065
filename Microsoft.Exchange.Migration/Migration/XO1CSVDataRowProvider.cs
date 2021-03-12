using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200018C RID: 396
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class XO1CSVDataRowProvider : MigrationCSVDataRowProvider, IMigrationDataRowProvider
	{
		// Token: 0x06001274 RID: 4724 RVA: 0x0004E05E File Offset: 0x0004C25E
		internal XO1CSVDataRowProvider(MigrationJob job, IMigrationDataProvider dataProvider) : base(job, dataProvider, new XO1CsvSchema())
		{
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x0004E070 File Offset: 0x0004C270
		protected override IMigrationDataRow CreateDataRow(CsvRow row)
		{
			InvalidDataRow invalidDataRow = base.GetInvalidDataRow(row, MigrationType.XO1);
			if (invalidDataRow != null)
			{
				return invalidDataRow;
			}
			string text = row["EmailAddress"];
			SmtpAddress smtpAddress;
			if (!MigrationServiceHelper.TryParseSmtpAddress(text, out smtpAddress))
			{
				return base.GetInvalidDataRow(row, Strings.ValueNotProvidedForColumn("EmailAddress"), MigrationType.XO1);
			}
			long puid;
			if (!long.TryParse(row["Puid"], out puid))
			{
				return base.GetInvalidDataRow(row, Strings.ValueNotProvidedForColumn("Puid"), MigrationType.XO1);
			}
			ExTimeZoneValue exTimeZoneValue;
			if (!ExTimeZoneValue.TryParse(row["TimeZone"], out exTimeZoneValue))
			{
				return base.GetInvalidDataRow(row, Strings.ValueNotProvidedForColumn("TimeZone"), MigrationType.XO1);
			}
			int localeId;
			if (!int.TryParse(row["Lcid"], out localeId))
			{
				return base.GetInvalidDataRow(row, Strings.ValueNotProvidedForColumn("Lcid"), MigrationType.XO1);
			}
			long accountSize;
			if (!long.TryParse(row["AccountSize"], out accountSize))
			{
				return base.GetInvalidDataRow(row, Strings.ValueNotProvidedForColumn("AccountSize"), MigrationType.XO1);
			}
			string text2 = null;
			string[] array = null;
			if (row.TryGetColumnValue("Aliases", out text2) && !string.IsNullOrEmpty(text2))
			{
				array = text2.Split(new char[]
				{
					'\u0001'
				});
				foreach (string emailAddress in array)
				{
					SmtpAddress smtpAddress2;
					if (!MigrationServiceHelper.TryParseSmtpAddress(emailAddress, out smtpAddress2))
					{
						return base.GetInvalidDataRow(row, Strings.ValueNotProvidedForColumn("Aliases"), MigrationType.XO1);
					}
				}
			}
			string text3;
			if (row.TryGetColumnValue("FirstName", out text3) && string.IsNullOrEmpty(text3))
			{
				text3 = null;
			}
			string text4;
			if (row.TryGetColumnValue("LastName", out text4) && string.IsNullOrEmpty(text4))
			{
				text4 = null;
			}
			return new XO1MigrationDataRow(row.Index, text, puid, text3, text4, exTimeZoneValue.ExTimeZone, localeId, array, accountSize);
		}
	}
}
