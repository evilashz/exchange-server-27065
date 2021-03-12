using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000002 RID: 2
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MigrationCsvSchemaBase : CsvSchema
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D4 File Offset: 0x000002D4
		protected MigrationCsvSchemaBase(int maximumRowCount, Dictionary<string, ProviderPropertyDefinition> requiredColumns, Dictionary<string, ProviderPropertyDefinition> optionalColumns, IEnumerable<string> prohibitedColumns) : base(maximumRowCount, requiredColumns, optionalColumns, prohibitedColumns)
		{
			this.csvHeaders = null;
			if (requiredColumns == null)
			{
				if (optionalColumns == null)
				{
					throw new ArgumentException("need to pass in either required and/or optional columns");
				}
				this.allColumns = new List<string>(optionalColumns.Keys);
				return;
			}
			else
			{
				if (optionalColumns == null)
				{
					this.allColumns = new List<string>(requiredColumns.Keys);
					return;
				}
				this.allColumns = new List<string>(from columnName in requiredColumns.Keys.Union(optionalColumns.Keys)
				orderby columnName
				select columnName);
				return;
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002169 File Offset: 0x00000369
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002171 File Offset: 0x00000371
		public bool AllowUnknownColumnsInCSV { get; set; }

		// Token: 0x06000004 RID: 4 RVA: 0x0000217A File Offset: 0x0000037A
		public static ProviderPropertyDefinition GetDefaultPropertyDefinition(string propertyName, PropertyDefinitionConstraint[] constraints)
		{
			return MigrationCsvSchemaBase.GetDefaultPropertyDefinition<string>(propertyName, constraints);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002184 File Offset: 0x00000384
		public static ProviderPropertyDefinition GetDefaultPropertyDefinition<T>(string propertyName, PropertyDefinitionConstraint[] constraints)
		{
			if (constraints == null)
			{
				constraints = PropertyDefinitionConstraint.None;
			}
			PropertyDefinitionFlags propertyDefinitionFlags = PropertyDefinitionFlags.None;
			Type typeFromHandle = typeof(T);
			if (typeFromHandle == typeof(int) || typeFromHandle == typeof(uint) || typeFromHandle == typeof(long) || typeFromHandle == typeof(ulong) || typeFromHandle == typeof(ByteQuantifiedSize) || typeFromHandle == typeof(EnhancedTimeSpan) || typeFromHandle == typeof(DateTime))
			{
				propertyDefinitionFlags |= PropertyDefinitionFlags.PersistDefaultValue;
			}
			return new SimpleProviderPropertyDefinition(propertyName, ExchangeObjectVersion.Exchange2010, typeFromHandle, propertyDefinitionFlags, default(T), constraints, constraints);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002248 File Offset: 0x00000448
		public static Dictionary<string, ProviderPropertyDefinition> GenerateDefaultColumnInfo(string[] columnNames = null)
		{
			Dictionary<string, ProviderPropertyDefinition> dictionary = new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase);
			if (columnNames != null)
			{
				foreach (string text in columnNames)
				{
					dictionary[text] = MigrationCsvSchemaBase.GetDefaultPropertyDefinition(text, null);
				}
			}
			return dictionary;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002288 File Offset: 0x00000488
		public MigrationBatchError CreateValidationError(CsvRow row, LocalizedString errorMessage)
		{
			return new MigrationBatchError
			{
				RowIndex = row.Index,
				EmailAddress = this.GetIdentifier(row),
				LocalizedErrorMessage = errorMessage
			};
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022BD File Offset: 0x000004BD
		public virtual string GetIdentifier(CsvRow row)
		{
			return row["EmailAddress"];
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022CB File Offset: 0x000004CB
		public virtual void ProcessRow(CsvRow row, out MigrationBatchError error)
		{
			error = null;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022D0 File Offset: 0x000004D0
		public IEnumerable<string> GetOrderedHeaders()
		{
			if (this.csvHeaders == null)
			{
				throw new InvalidOperationException("expect to have headers before returning ordered row data");
			}
			return this.csvHeaders.OrderedHeaders;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022F0 File Offset: 0x000004F0
		public IEnumerable<string> GetOrderedRowData(CsvRow row)
		{
			if (this.csvHeaders == null)
			{
				throw new InvalidOperationException("expect to have headers before returning ordered row data");
			}
			return this.csvHeaders.GetOrderedRowData(row);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002314 File Offset: 0x00000514
		public override CsvColumnMap CreateColumnMap(IList<string> columnNames, bool throwOnUnknownColumns, bool throwOnDuplicateColumns)
		{
			CsvColumnMap csvColumnMap = base.CreateColumnMap(columnNames, !this.AllowUnknownColumnsInCSV, throwOnDuplicateColumns);
			this.csvHeaders = new MigrationCsvSchemaBase.CsvHeaders(this.allColumns, csvColumnMap);
			return csvColumnMap;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002346 File Offset: 0x00000546
		public virtual void WriteHeaders(StreamWriter streamWriter)
		{
			this.CreateColumnMap(base.RequiredColumns.Keys.Concat(base.OptionalColumns.Keys).ToList<string>(), true, true);
			streamWriter.WriteCsvLine(this.GetOrderedHeaders());
		}

		// Token: 0x04000001 RID: 1
		public const string EmailColumnName = "EmailAddress";

		// Token: 0x04000002 RID: 2
		internal static readonly PropertyDefinitionConstraint[] EmailAddressConstraint = new PropertyDefinitionConstraint[]
		{
			new ValidSmtpAddressConstraint(),
			new StringLengthConstraint(1, 1024)
		};

		// Token: 0x04000003 RID: 3
		private MigrationCsvSchemaBase.CsvHeaders csvHeaders;

		// Token: 0x04000004 RID: 4
		private List<string> allColumns;

		// Token: 0x02000003 RID: 3
		protected class CsvRangedValueConstraint<T> : RangedValueConstraint<T> where T : struct, IComparable
		{
			// Token: 0x06000010 RID: 16 RVA: 0x000023B0 File Offset: 0x000005B0
			public CsvRangedValueConstraint(T minValue, T maxValue) : base(minValue, maxValue)
			{
			}

			// Token: 0x06000011 RID: 17 RVA: 0x000023BC File Offset: 0x000005BC
			public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
			{
				if (!(value is T))
				{
					Exception ex = null;
					try
					{
						TypeConverter converter = TypeDescriptor.GetConverter(propertyDefinition.Type);
						if (value is string)
						{
							value = converter.ConvertFromInvariantString((string)value);
						}
						else
						{
							value = converter.ConvertFrom(value);
						}
					}
					catch (ArgumentException ex2)
					{
						ex = ex2;
					}
					catch (FormatException ex3)
					{
						ex = ex3;
					}
					catch (NotSupportedException ex4)
					{
						ex = ex4;
					}
					catch (Exception ex5)
					{
						if (ex5.InnerException == null || !(ex5.InnerException is FormatException))
						{
							throw;
						}
						ex = ex5;
					}
					if (ex != null)
					{
						return new PropertyConstraintViolationError(DataStrings.PropertyTypeMismatch(value.GetType().ToString(), propertyDefinition.Type.ToString()), propertyDefinition, value, this);
					}
				}
				return base.Validate(value, propertyDefinition, propertyBag);
			}
		}

		// Token: 0x02000004 RID: 4
		private class CsvHeaders
		{
			// Token: 0x06000012 RID: 18 RVA: 0x0000249C File Offset: 0x0000069C
			public CsvHeaders(IList<string> headers, CsvColumnMap columnMap)
			{
				this.headers = headers;
				this.writeIndex = new List<int>();
				this.writeIndex.Capacity = headers.Count;
				foreach (string columnName in headers)
				{
					int item = columnMap.Contains(columnName) ? columnMap[columnName] : -1;
					this.writeIndex.Add(item);
				}
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000013 RID: 19 RVA: 0x00002528 File Offset: 0x00000728
			public IEnumerable<string> OrderedHeaders
			{
				get
				{
					return this.headers;
				}
			}

			// Token: 0x06000014 RID: 20 RVA: 0x000026E4 File Offset: 0x000008E4
			public IEnumerable<string> GetOrderedRowData(CsvRow row)
			{
				foreach (int index in this.writeIndex)
				{
					yield return (index >= 0) ? row.Data[index] : string.Empty;
				}
				yield break;
			}

			// Token: 0x04000007 RID: 7
			private List<int> writeIndex;

			// Token: 0x04000008 RID: 8
			private IList<string> headers;
		}
	}
}
