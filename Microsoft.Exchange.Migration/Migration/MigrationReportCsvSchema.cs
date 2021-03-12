using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000132 RID: 306
	internal class MigrationReportCsvSchema : CsvSchema
	{
		// Token: 0x06000F76 RID: 3958 RVA: 0x00042510 File Offset: 0x00040710
		public MigrationReportCsvSchema(bool includeProvisioning) : base(int.MaxValue, MigrationReportCsvSchema.requiredColumns.Value, MigrationReportCsvSchema.optionalColumns.Value, null)
		{
			if (includeProvisioning)
			{
				this.OrderedColumns = MigrationReportCsvSchema.AllColumns;
				return;
			}
			this.OrderedColumns = MigrationReportCsvSchema.MigrationColumns;
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x0004254C File Offset: 0x0004074C
		// (set) Token: 0x06000F78 RID: 3960 RVA: 0x00042554 File Offset: 0x00040754
		public string[] OrderedColumns { get; private set; }

		// Token: 0x06000F79 RID: 3961 RVA: 0x0004255D File Offset: 0x0004075D
		public void WriteHeader(StreamWriter streamWriter)
		{
			streamWriter.WriteCsvLine(this.OrderedColumns);
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0004256C File Offset: 0x0004076C
		public void WriteRow(StreamWriter streamWriter, MigrationJobItem jobItem)
		{
			MigrationUtil.ThrowOnNullArgument(jobItem, "jobItem");
			List<string> list = new List<string>(this.OrderedColumns.Length);
			foreach (string key in this.OrderedColumns)
			{
				list.Add(MigrationReportCsvSchema.CellWriters[key](jobItem));
			}
			streamWriter.WriteCsvLine(list);
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x00042700 File Offset: 0x00040900
		// Note: this type is marked as 'beforefieldinit'.
		static MigrationReportCsvSchema()
		{
			Dictionary<string, Func<MigrationJobItem, string>> dictionary = new Dictionary<string, Func<MigrationJobItem, string>>();
			dictionary.Add("Identifier", (MigrationJobItem ji) => ji.Identifier);
			dictionary.Add("Status", (MigrationJobItem ji) => ji.Status.ToString());
			dictionary.Add("ObjectType", (MigrationJobItem ji) => ji.RecipientType.ToString());
			dictionary.Add("ItemsMigrated", (MigrationJobItem ji) => ji.ItemsSynced.ToString(CultureInfo.CurrentCulture));
			dictionary.Add("ItemsSkipped", (MigrationJobItem ji) => ji.ItemsSkipped.ToString(CultureInfo.CurrentCulture));
			dictionary.Add("Password", delegate(MigrationJobItem ji)
			{
				if (ji.RecipientType == MigrationUserRecipientType.Mailbox || ji.RecipientType == MigrationUserRecipientType.Mailuser)
				{
					ExchangeProvisioningDataStorage exchangeProvisioningDataStorage = ji.ProvisioningData as ExchangeProvisioningDataStorage;
					if (exchangeProvisioningDataStorage != null && exchangeProvisioningDataStorage.ExchangeRecipient != null && exchangeProvisioningDataStorage.ExchangeRecipient.DoesADObjectExist)
					{
						string clearString = "<" + Strings.PasswordPreviouslySet + ">";
						return MigrationServiceFactory.Instance.GetCryptoAdapter().ClearStringToEncryptedString(clearString);
					}
					if (exchangeProvisioningDataStorage != null)
					{
						return exchangeProvisioningDataStorage.EncryptedPassword;
					}
				}
				return LocalizedString.Empty;
			});
			dictionary.Add("ErrorMessage", (MigrationJobItem ji) => ji.LocalizedError ?? LocalizedString.Empty);
			MigrationReportCsvSchema.CellWriters = dictionary;
			MigrationReportCsvSchema.requiredColumns = new Lazy<Dictionary<string, ProviderPropertyDefinition>>(() => MigrationCsvSchemaBase.GenerateDefaultColumnInfo(null), LazyThreadSafetyMode.ExecutionAndPublication);
			MigrationReportCsvSchema.optionalColumns = new Lazy<Dictionary<string, ProviderPropertyDefinition>>(() => MigrationCsvSchemaBase.GenerateDefaultColumnInfo(MigrationReportCsvSchema.AllColumns), LazyThreadSafetyMode.ExecutionAndPublication);
		}

		// Token: 0x04000566 RID: 1382
		public const string PasswordColumnName = "Password";

		// Token: 0x04000567 RID: 1383
		private const string IdentifierColumnName = "Identifier";

		// Token: 0x04000568 RID: 1384
		private const string StatusColumnName = "Status";

		// Token: 0x04000569 RID: 1385
		private const string ItemsMigratedColumnName = "ItemsMigrated";

		// Token: 0x0400056A RID: 1386
		private const string ItemsSkippedColumnName = "ItemsSkipped";

		// Token: 0x0400056B RID: 1387
		private const string TypeColumnName = "ObjectType";

		// Token: 0x0400056C RID: 1388
		public const string ErrorMessageColumnName = "ErrorMessage";

		// Token: 0x0400056D RID: 1389
		private const int InternalMaximumRowCount = 2147483647;

		// Token: 0x0400056E RID: 1390
		private static readonly string[] AllColumns = new string[]
		{
			"Identifier",
			"ObjectType",
			"Password",
			"Status",
			"ItemsMigrated",
			"ItemsSkipped",
			"ErrorMessage"
		};

		// Token: 0x0400056F RID: 1391
		private static readonly string[] MigrationColumns = new string[]
		{
			"Identifier",
			"Status",
			"ItemsMigrated",
			"ItemsSkipped",
			"ErrorMessage"
		};

		// Token: 0x04000570 RID: 1392
		private static readonly Dictionary<string, Func<MigrationJobItem, string>> CellWriters;

		// Token: 0x04000571 RID: 1393
		private static Lazy<Dictionary<string, ProviderPropertyDefinition>> requiredColumns;

		// Token: 0x04000572 RID: 1394
		private static Lazy<Dictionary<string, ProviderPropertyDefinition>> optionalColumns;
	}
}
