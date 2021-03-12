using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200004D RID: 77
	internal class MigrationBatchCsvProcessor
	{
		// Token: 0x06000392 RID: 914 RVA: 0x0000D2ED File Offset: 0x0000B4ED
		public MigrationBatchCsvProcessor(MigrationCsvSchemaBase schema)
		{
			this.schema = schema;
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000D2FC File Offset: 0x0000B4FC
		protected virtual bool ValidationWarningAsError
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000D300 File Offset: 0x0000B500
		public static MigrationBatchError GetRowValidationError(CsvRow row, MigrationCsvSchemaBase schema)
		{
			LocalizedString localizedString = LocalizedString.Empty;
			foreach (string text in row.Errors.Keys)
			{
				PropertyValidationError propertyValidationError = row.Errors[text];
				LocalizedString localizedString2 = ServerStrings.ColumnError(text, propertyValidationError.Description);
				localizedString = (localizedString.IsEmpty ? localizedString2 : ServerStrings.CompositeError(localizedString, localizedString2));
			}
			if (!localizedString.IsEmpty)
			{
				return schema.CreateValidationError(row, localizedString);
			}
			return null;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000D39C File Offset: 0x0000B59C
		internal LocalizedException ProcessCsv(MigrationBatch batch, byte[] csvData)
		{
			Stream stream2;
			int totalCount;
			MultiValuedProperty<MigrationBatchError> validationWarnings;
			LocalizedException result;
			using (Stream stream = new MemoryStream(csvData))
			{
				stream2 = new MemoryStream(Convert.ToInt32(stream.Length));
				using (StreamWriter streamWriter = new StreamWriter(stream2, MigrationBatchCsvProcessor.UTF8NoBOM, 1024, true))
				{
					this.schema.AllowUnknownColumnsInCSV = batch.AllowUnknownColumnsInCsv;
					result = this.CopyAndValidateCsv(stream, streamWriter, out totalCount, out validationWarnings);
				}
				stream2.Seek(0L, SeekOrigin.Begin);
			}
			batch.CsvStream = stream2;
			batch.TotalCount = totalCount;
			batch.ValidationWarnings = validationWarnings;
			return result;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000D450 File Offset: 0x0000B650
		internal LocalizedException CopyAndValidateCsv(Stream sourceStream, StreamWriter destinationWriter, out int dataRowCount, out MultiValuedProperty<MigrationBatchError> validationWarnings)
		{
			Dictionary<int, MigrationBatchError> dictionary = new Dictionary<int, MigrationBatchError>();
			dataRowCount = 0;
			validationWarnings = new MultiValuedProperty<MigrationBatchError>();
			try
			{
				foreach (CsvRow row in this.schema.Read(sourceStream))
				{
					bool flag;
					LocalizedException ex = this.ProcessCsvRow(row, dictionary, out flag);
					if (ex != null)
					{
						return ex;
					}
					if (flag)
					{
						dataRowCount++;
					}
					destinationWriter.WriteCsvLine(row.Data);
				}
				if (dataRowCount == 0)
				{
					return new CsvFileIsEmptyException();
				}
			}
			finally
			{
				destinationWriter.Flush();
			}
			validationWarnings = new MultiValuedProperty<MigrationBatchError>(dictionary.Values);
			return this.Validate();
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000D514 File Offset: 0x0000B714
		protected virtual LocalizedException InternalProcessRow(CsvRow row, out bool isDataRow)
		{
			isDataRow = (row.Index != 0);
			return null;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000D526 File Offset: 0x0000B726
		protected virtual LocalizedException Validate()
		{
			return null;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000D52C File Offset: 0x0000B72C
		private LocalizedException ProcessCsvRow(CsvRow row, Dictionary<int, MigrationBatchError> warnings, out bool isDataRow)
		{
			if (row.Index == 0)
			{
				isDataRow = false;
				return null;
			}
			MigrationBatchError migrationBatchError = MigrationBatchCsvProcessor.GetRowValidationError(row, this.schema);
			MigrationBatchError migrationBatchError2;
			this.schema.ProcessRow(row, out migrationBatchError2);
			migrationBatchError = (migrationBatchError ?? migrationBatchError2);
			if (migrationBatchError != null)
			{
				if (this.ValidationWarningAsError)
				{
					isDataRow = false;
					return new MigrationCSVParsingException(row.Index, migrationBatchError.LocalizedErrorMessage);
				}
				warnings[migrationBatchError.RowIndex] = migrationBatchError;
			}
			return this.InternalProcessRow(row, out isDataRow);
		}

		// Token: 0x04000103 RID: 259
		private const int DefaultBufferSize = 1024;

		// Token: 0x04000104 RID: 260
		private static readonly UTF8Encoding UTF8NoBOM = new UTF8Encoding(false, true);

		// Token: 0x04000105 RID: 261
		private readonly MigrationCsvSchemaBase schema;
	}
}
