using System;
using System.IO;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000206 RID: 518
	internal class MigrationCsvUploadHandler : FileEncodeUploadHandler
	{
		// Token: 0x17001BEB RID: 7147
		// (get) Token: 0x060026B2 RID: 9906 RVA: 0x0007846F File Offset: 0x0007666F
		public override Type SetParameterType
		{
			get
			{
				return typeof(MigrationCsvUploadParameters);
			}
		}

		// Token: 0x17001BEC RID: 7148
		// (get) Token: 0x060026B3 RID: 9907 RVA: 0x0007847B File Offset: 0x0007667B
		public override int MaxFileSize
		{
			get
			{
				return MigrationCsvUploadHandler.maxCsvFileSize;
			}
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x00078484 File Offset: 0x00076684
		public override PowerShellResults ProcessUpload(UploadFileContext context, WebServiceParameters parameters)
		{
			string migrationBatchType = ((MigrationCsvUploadParameters)parameters).MigrationBatchType;
			string a;
			if ((a = migrationBatchType) != null)
			{
				MigrationBatchCsvSchema migrationBatchCsvSchema;
				if (!(a == "LocalMove"))
				{
					if (!(a == "RemoteMove"))
					{
						if (!(a == "Staged"))
						{
							if (!(a == "IMAP"))
							{
								if (!(a == "Cutover"))
								{
									goto IL_7C;
								}
								goto IL_7C;
							}
							else
							{
								migrationBatchCsvSchema = new MigrationBatchCsvSchema();
							}
						}
						else
						{
							migrationBatchCsvSchema = new ExchangeMigrationBatchCsvSchema();
						}
					}
					else
					{
						migrationBatchCsvSchema = new MigrationRemoteMoveCsvSchema();
					}
				}
				else
				{
					migrationBatchCsvSchema = new MigrationLocalMoveCsvSchema();
				}
				migrationBatchCsvSchema.AllowUnknownColumnsInCSV = ((MigrationCsvUploadParameters)parameters).AllowUnknownColumnsInCsv;
				CsvRow? csvRow = null;
				int num = 0;
				using (MemoryStream memoryStream = new MemoryStream((int)context.FileStream.Length))
				{
					context.FileStream.CopyTo(memoryStream);
					context.FileStream.Position = 0L;
					memoryStream.Position = 0L;
					try
					{
						foreach (CsvRow csvRow2 in migrationBatchCsvSchema.Read(memoryStream))
						{
							if (csvRow2.Index > 0)
							{
								csvRow = new CsvRow?(csvRow ?? csvRow2);
								num++;
							}
						}
					}
					catch (CsvUnknownColumnsException ex)
					{
						throw new UnknownColumnsInCsvException(ex.Columns);
					}
				}
				if (num == 0)
				{
					throw new LocalizedException(Strings.NoMailboxInCsv);
				}
				PowerShellResults<EncodedFile> powerShellResults = (PowerShellResults<EncodedFile>)base.ProcessUpload(context, parameters);
				return new PowerShellResults<MigrationCsvFile>
				{
					Output = new MigrationCsvFile[]
					{
						new MigrationCsvFile
						{
							FileContent = powerShellResults.Value.FileContent,
							MailboxCount = num,
							FirstSmtpAddress = ((csvRow != null) ? csvRow.Value["EmailAddress"] : null)
						}
					}
				};
			}
			IL_7C:
			throw new Exception(string.Format("Migration type '{0}' doesn't require CSV input.", migrationBatchType));
		}

		// Token: 0x04001F7F RID: 8063
		private static int maxCsvFileSize = AppConfigLoader.GetConfigIntValue("MaxMigrationCsvFileSize", 0, int.MaxValue, 10485760);
	}
}
