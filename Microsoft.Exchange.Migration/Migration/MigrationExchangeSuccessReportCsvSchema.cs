using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200012B RID: 299
	internal class MigrationExchangeSuccessReportCsvSchema : ReportCsvSchema
	{
		// Token: 0x06000F41 RID: 3905 RVA: 0x000417EA File Offset: 0x0003F9EA
		public MigrationExchangeSuccessReportCsvSchema(bool isCompletionReport) : base(int.MaxValue, MigrationExchangeSuccessReportCsvSchema.requiredColumns.Value, MigrationExchangeSuccessReportCsvSchema.optionalColumns.Value, null)
		{
			this.isCompletionReport = isCompletionReport;
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x00041813 File Offset: 0x0003FA13
		private string[] Headers
		{
			get
			{
				if (this.isCompletionReport)
				{
					return MigrationExchangeSuccessReportCsvSchema.CompletionHeaders;
				}
				return MigrationExchangeSuccessReportCsvSchema.FinalizationHeaders;
			}
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x00041828 File Offset: 0x0003FA28
		public override void WriteHeader(StreamWriter streamWriter)
		{
			streamWriter.WriteCsvLine(this.Headers);
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00041838 File Offset: 0x0003FA38
		public override void WriteRow(StreamWriter streamWriter, MigrationJobItem jobItem)
		{
			MigrationUtil.ThrowOnNullArgument(jobItem, "jobItem");
			List<string> list = new List<string>(this.Headers.Length);
			list.Add(jobItem.Identifier);
			list.Add(jobItem.RecipientType.ToString());
			if (this.isCompletionReport)
			{
				ExchangeProvisioningDataStorage exchangeProvisioningDataStorage = (ExchangeProvisioningDataStorage)jobItem.ProvisioningData;
				string item = exchangeProvisioningDataStorage.EncryptedPassword;
				if ((jobItem.RecipientType == MigrationUserRecipientType.Mailbox || jobItem.RecipientType == MigrationUserRecipientType.Mailuser) && exchangeProvisioningDataStorage.ExchangeRecipient.DoesADObjectExist)
				{
					string clearString = "<" + Strings.PasswordPreviouslySet + ">";
					item = MigrationServiceFactory.Instance.GetCryptoAdapter().ClearStringToEncryptedString(clearString);
				}
				list.Add(item);
			}
			if (jobItem.ItemsSkipped != 0L)
			{
				list.Add(ServerStrings.MigrationStatisticsPartiallyCompleteStatus);
			}
			else
			{
				list.Add(ServerStrings.MigrationStatisticsCompleteStatus);
			}
			list.Add(jobItem.ItemsSynced.ToString(CultureInfo.CurrentCulture));
			list.Add(jobItem.ItemsSkipped.ToString(CultureInfo.CurrentCulture));
			streamWriter.WriteCsvLine(list);
		}

		// Token: 0x0400052C RID: 1324
		private static readonly string[] RequiredColumnNames = new string[]
		{
			"EmailAddress",
			"ObjectType",
			"Status",
			"ItemsMigrated",
			"ItemsSkipped"
		};

		// Token: 0x0400052D RID: 1325
		private static readonly string[] OptionalColumnNames = new string[]
		{
			"Password",
			"AdditionalComments"
		};

		// Token: 0x0400052E RID: 1326
		private static Lazy<Dictionary<string, ProviderPropertyDefinition>> optionalColumns = new Lazy<Dictionary<string, ProviderPropertyDefinition>>(() => MigrationCsvSchemaBase.GenerateDefaultColumnInfo(MigrationExchangeSuccessReportCsvSchema.OptionalColumnNames), LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x0400052F RID: 1327
		private static Lazy<Dictionary<string, ProviderPropertyDefinition>> requiredColumns = new Lazy<Dictionary<string, ProviderPropertyDefinition>>(() => MigrationCsvSchemaBase.GenerateDefaultColumnInfo(MigrationExchangeSuccessReportCsvSchema.RequiredColumnNames), LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x04000530 RID: 1328
		private static readonly string[] FinalizationHeaders = new string[]
		{
			"EmailAddress",
			"ObjectType",
			"Status",
			"ItemsMigrated",
			"ItemsSkipped"
		};

		// Token: 0x04000531 RID: 1329
		private static readonly string[] CompletionHeaders = new string[]
		{
			"EmailAddress",
			"ObjectType",
			"Password",
			"Status",
			"ItemsMigrated",
			"ItemsSkipped"
		};

		// Token: 0x04000532 RID: 1330
		private readonly bool isCompletionReport;
	}
}
