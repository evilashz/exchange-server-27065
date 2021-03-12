using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000208 RID: 520
	internal class CsvSchema
	{
		// Token: 0x0600121C RID: 4636 RVA: 0x00036A10 File Offset: 0x00034C10
		public CsvSchema(int maximumRowCount, Dictionary<string, ProviderPropertyDefinition> requiredColumns, Dictionary<string, ProviderPropertyDefinition> optionalColumns, IEnumerable<string> prohibitedColumns)
		{
			this.MaximumRowCount = maximumRowCount;
			this.RequiredColumns = (requiredColumns ?? new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase));
			this.OptionalColumns = (optionalColumns ?? new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase));
			this.ProhibitedColumns = (prohibitedColumns ?? ((IEnumerable<string>)new string[0]));
			if (this.RequiredColumns.Comparer != StringComparer.OrdinalIgnoreCase)
			{
				throw new ArgumentOutOfRangeException("requiredColumns.Comparer", this.RequiredColumns.Comparer, "requiredColumns.Comparer must be StringComparer.OrdinalIgnoreCase");
			}
			if (this.OptionalColumns.Comparer != StringComparer.OrdinalIgnoreCase)
			{
				throw new ArgumentOutOfRangeException("optionalColumns.Comparer", this.OptionalColumns.Comparer, "optionalColumns.Comparer must be StringComparer.OrdinalIgnoreCase");
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x00036AC5 File Offset: 0x00034CC5
		// (set) Token: 0x0600121E RID: 4638 RVA: 0x00036ACD File Offset: 0x00034CCD
		private protected int MaximumRowCount { protected get; private set; }

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x00036AD6 File Offset: 0x00034CD6
		// (set) Token: 0x06001220 RID: 4640 RVA: 0x00036ADE File Offset: 0x00034CDE
		private protected Dictionary<string, ProviderPropertyDefinition> OptionalColumns { protected get; private set; }

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x00036AE7 File Offset: 0x00034CE7
		// (set) Token: 0x06001222 RID: 4642 RVA: 0x00036AEF File Offset: 0x00034CEF
		private protected IEnumerable<string> ProhibitedColumns { protected get; private set; }

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x00036AF8 File Offset: 0x00034CF8
		// (set) Token: 0x06001224 RID: 4644 RVA: 0x00036B00 File Offset: 0x00034D00
		private protected Dictionary<string, ProviderPropertyDefinition> RequiredColumns { protected get; private set; }

		// Token: 0x06001225 RID: 4645 RVA: 0x00036B09 File Offset: 0x00034D09
		public IEnumerable<CsvRow> Read(Stream sourceStream)
		{
			return this.Read(sourceStream, null, true, true);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00036EB8 File Offset: 0x000350B8
		public IEnumerable<CsvRow> Read(Stream sourceStream, Encoding encoding, bool throwOnUnknownColumns, bool throwOnDuplicateColumns)
		{
			StreamReader sourceReader;
			if (encoding != null)
			{
				sourceReader = new StreamReader(sourceStream, encoding);
			}
			else
			{
				sourceReader = new StreamReader(sourceStream, true);
			}
			using (sourceReader)
			{
				using (CsvReader csvReader = new CsvReader(sourceReader, true))
				{
					if (csvReader.Headers != null)
					{
						CsvColumnMap columnMap = this.CreateColumnMap(csvReader.Headers, throwOnUnknownColumns, throwOnDuplicateColumns);
						yield return this.CreateCsvRow(0, csvReader.Headers, columnMap);
						int index = 0;
						foreach (string[] data in csvReader.ReadRows())
						{
							index++;
							CsvRow row = this.CreateCsvRow(index, data, columnMap);
							this.ValidateRow(row);
							yield return row;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00036EF2 File Offset: 0x000350F2
		public int Copy(Stream sourceStream, StreamWriter destinationWriter)
		{
			return this.Copy(sourceStream, destinationWriter, null);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00036F00 File Offset: 0x00035100
		public int Copy(Stream sourceStream, StreamWriter destinationWriter, Converter<CsvRow, CsvRow> rowProcessor)
		{
			int result;
			try
			{
				int num = 0;
				foreach (CsvRow csvRow in this.Read(sourceStream))
				{
					num = csvRow.Index;
					CsvRow csvRow2 = csvRow;
					if (rowProcessor != null)
					{
						csvRow2 = rowProcessor(csvRow);
					}
					destinationWriter.WriteCsvLine(csvRow2.Data);
				}
				if (num == 0)
				{
					throw new CsvFileIsEmptyException();
				}
				result = num;
			}
			finally
			{
				destinationWriter.Flush();
			}
			return result;
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00036F94 File Offset: 0x00035194
		public virtual CsvColumnMap CreateColumnMap(IList<string> columnNames, bool throwOnUnknownColumns, bool throwOnDuplicateColumns)
		{
			CsvColumnMap result = new CsvColumnMap(columnNames, throwOnDuplicateColumns);
			foreach (string text in this.RequiredColumns.Keys)
			{
				if (!result.Contains(text))
				{
					throw new CsvRequiredColumnMissingException(text);
				}
			}
			foreach (string text2 in this.ProhibitedColumns)
			{
				if (result.Contains(text2))
				{
					throw new CsvProhibitedColumnPresentException(text2);
				}
			}
			if (throwOnUnknownColumns)
			{
				HashSet<string> hashSet = new HashSet<string>(columnNames, StringComparer.OrdinalIgnoreCase);
				hashSet.ExceptWith(this.RequiredColumns.Keys);
				hashSet.ExceptWith(this.OptionalColumns.Keys);
				if (hashSet.Count > 0)
				{
					string columns = '"' + string.Join("\", \"", hashSet.ToArray<string>()) + '"';
					throw new CsvUnknownColumnsException(columns, hashSet);
				}
			}
			return result;
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x000370B4 File Offset: 0x000352B4
		public virtual void ValidateRow(CsvRow row)
		{
			if (row.Index > this.MaximumRowCount)
			{
				throw new CsvTooManyRowsException(this.MaximumRowCount);
			}
			foreach (KeyValuePair<string, ProviderPropertyDefinition> keyValuePair in this.RequiredColumns)
			{
				string key = keyValuePair.Key;
				string value = row[key];
				ProviderPropertyDefinition value2 = keyValuePair.Value;
				if (value.IsNullOrBlank())
				{
					PropertyValidationError error = new PropertyValidationError(DataStrings.PropertyIsMandatory, value2, null);
					this.OnPropertyValidationError(row, key, error);
				}
			}
			foreach (KeyValuePair<string, string> keyValuePair2 in row.GetExistingValues())
			{
				string key2 = keyValuePair2.Key;
				string value3 = keyValuePair2.Value ?? "";
				ProviderPropertyDefinition propertyDefinition = this.GetPropertyDefinition(key2);
				if (propertyDefinition != null)
				{
					foreach (PropertyDefinitionConstraint propertyDefinitionConstraint in propertyDefinition.AllConstraints)
					{
						PropertyConstraintViolationError propertyConstraintViolationError = propertyDefinitionConstraint.Validate(value3, propertyDefinition, null);
						if (propertyConstraintViolationError != null)
						{
							this.OnPropertyValidationError(row, key2, propertyConstraintViolationError);
						}
					}
				}
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600122B RID: 4651 RVA: 0x0003721C File Offset: 0x0003541C
		// (remove) Token: 0x0600122C RID: 4652 RVA: 0x00037254 File Offset: 0x00035454
		public event Action<CsvRow, string, PropertyValidationError> PropertyValidationError;

		// Token: 0x0600122D RID: 4653 RVA: 0x00037289 File Offset: 0x00035489
		protected virtual CsvRow CreateCsvRow(int index, IList<string> data, CsvColumnMap columnMap)
		{
			return new CsvRow(index, data, columnMap);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00037294 File Offset: 0x00035494
		protected void OnPropertyValidationError(CsvRow row, string columnName, PropertyValidationError error)
		{
			row.SetError(columnName, error);
			Action<CsvRow, string, PropertyValidationError> propertyValidationError = this.PropertyValidationError;
			if (propertyValidationError != null)
			{
				propertyValidationError(row, columnName, error);
			}
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x000372C0 File Offset: 0x000354C0
		private ProviderPropertyDefinition GetPropertyDefinition(string columnName)
		{
			ProviderPropertyDefinition result = null;
			if (!this.OptionalColumns.TryGetValue(columnName, out result))
			{
				this.RequiredColumns.TryGetValue(columnName, out result);
			}
			return result;
		}
	}
}
