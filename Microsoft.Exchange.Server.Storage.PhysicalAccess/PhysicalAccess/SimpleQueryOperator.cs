using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000044 RID: 68
	public abstract class SimpleQueryOperator : DataAccessOperator, IColumnResolver
	{
		// Token: 0x06000321 RID: 801 RVA: 0x00011373 File Offset: 0x0000F573
		protected SimpleQueryOperator(IConnectionProvider connectionProvider, SimpleQueryOperator.SimpleQueryOperatorDefinition operatorDefinition) : base(connectionProvider, operatorDefinition)
		{
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0001137D File Offset: 0x0000F57D
		public Table Table
		{
			get
			{
				return this.OperatorDefinition.Table;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0001138A File Offset: 0x0000F58A
		public SearchCriteria Criteria
		{
			get
			{
				return this.OperatorDefinition.Criteria;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00011397 File Offset: 0x0000F597
		public IList<Column> ColumnsToFetch
		{
			get
			{
				return this.OperatorDefinition.ColumnsToFetch;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000325 RID: 805 RVA: 0x000113A4 File Offset: 0x0000F5A4
		public IReadOnlyDictionary<Column, Column> RenameDictionary
		{
			get
			{
				return this.OperatorDefinition.RenameDictionary;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000326 RID: 806 RVA: 0x000113B1 File Offset: 0x0000F5B1
		public int SkipTo
		{
			get
			{
				return this.OperatorDefinition.SkipTo;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000327 RID: 807 RVA: 0x000113BE File Offset: 0x0000F5BE
		public int MaxRows
		{
			get
			{
				return this.OperatorDefinition.MaxRows;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000328 RID: 808 RVA: 0x000113CB File Offset: 0x0000F5CB
		public SortOrder SortOrder
		{
			get
			{
				return this.OperatorDefinition.SortOrder;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000329 RID: 809 RVA: 0x000113D8 File Offset: 0x0000F5D8
		public bool Backwards
		{
			get
			{
				return this.OperatorDefinition.Backwards;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600032A RID: 810 RVA: 0x000113E5 File Offset: 0x0000F5E5
		public SimpleQueryOperator.SimpleQueryOperatorDefinition OperatorDefinition
		{
			get
			{
				return (SimpleQueryOperator.SimpleQueryOperatorDefinition)base.OperatorDefinitionBase;
			}
		}

		// Token: 0x0600032B RID: 811
		public abstract Reader ExecuteReader(bool disposeQueryOperator);

		// Token: 0x0600032C RID: 812 RVA: 0x000113F2 File Offset: 0x0000F5F2
		public override object ExecuteScalar()
		{
			throw new NotSupportedException("ExecuteScalar is not supported by this operator");
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000113FE File Offset: 0x0000F5FE
		public virtual bool OperatorUsesTablePartition(Table table, IList<object> partitionKeyPrefix)
		{
			return this.Table == table;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0001140C File Offset: 0x0000F60C
		Column IColumnResolver.Resolve(Column column)
		{
			if (this.RenameDictionary != null)
			{
				column = this.ResolveColumn(column);
			}
			return column;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00011420 File Offset: 0x0000F620
		protected Column ResolveColumn(Column column)
		{
			int num = 0;
			Column column2;
			while (this.RenameDictionary.TryGetValue(column, out column2))
			{
				Globals.AssertRetail(++num < 10, "Rename chain is too long, possible circular renames?");
				column = column2;
			}
			return column;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00011458 File Offset: 0x0000F658
		protected void TraceAppendColumns(StringBuilder sb, ITWIR dataAccessor, IList<Column> columns)
		{
			for (int i = 0; i < columns.Count; i++)
			{
				if (i != 0)
				{
					sb.Append(", ");
				}
				Column column = columns[i];
				column.AppendToString(sb, StringFormatOptions.None);
				sb.Append("=[");
				try
				{
					sb.AppendAsString(dataAccessor.GetColumnValue(column));
				}
				catch (NonFatalDatabaseException ex)
				{
					base.Connection.OnExceptionCatch(ex);
					sb.Append("EXCEPTION:[");
					sb.Append(ex);
					sb.Append("]");
				}
				sb.Append("]");
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00011500 File Offset: 0x0000F700
		[Conditional("DEBUG")]
		private void AddDependentColumns(Column column, HashSet<Column> columns)
		{
			for (;;)
			{
				columns.Add(column);
				Column column2;
				if (this.RenameDictionary != null && this.RenameDictionary.TryGetValue(column, out column2))
				{
					column = column2;
				}
				else
				{
					ConversionColumn conversionColumn = column.ActualColumn as ConversionColumn;
					if (!(conversionColumn != null))
					{
						break;
					}
					column = conversionColumn.ArgumentColumn;
				}
			}
			FunctionColumn functionColumn = column.ActualColumn as FunctionColumn;
			if (functionColumn != null)
			{
				foreach (Column column3 in functionColumn.ArgumentColumns)
				{
				}
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00011584 File Offset: 0x0000F784
		[Conditional("DEBUG")]
		internal void AssertIfNotColumnToFetch(Column column)
		{
		}

		// Token: 0x02000045 RID: 69
		public abstract class SimpleQueryOperatorDefinition : DataAccessOperator.DataAccessOperatorDefinition
		{
			// Token: 0x06000333 RID: 819 RVA: 0x00011586 File Offset: 0x0000F786
			protected SimpleQueryOperatorDefinition(CultureInfo culture, Table table, IList<Column> columnsToFetch, SearchCriteria criteria, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, bool frequentOperation) : base(culture, frequentOperation)
			{
				this.table = table;
				this.columnsToFetch = columnsToFetch;
				this.criteria = criteria;
				this.skipTo = skipTo;
				this.maxRows = maxRows;
				this.renameDictionary = renameDictionary;
			}

			// Token: 0x17000096 RID: 150
			// (get) Token: 0x06000334 RID: 820 RVA: 0x000115BF File Offset: 0x0000F7BF
			public Table Table
			{
				get
				{
					return this.table;
				}
			}

			// Token: 0x17000097 RID: 151
			// (get) Token: 0x06000335 RID: 821 RVA: 0x000115C7 File Offset: 0x0000F7C7
			public SearchCriteria Criteria
			{
				get
				{
					return this.criteria;
				}
			}

			// Token: 0x17000098 RID: 152
			// (get) Token: 0x06000336 RID: 822 RVA: 0x000115CF File Offset: 0x0000F7CF
			public IList<Column> ColumnsToFetch
			{
				get
				{
					return this.columnsToFetch;
				}
			}

			// Token: 0x17000099 RID: 153
			// (get) Token: 0x06000337 RID: 823 RVA: 0x000115D7 File Offset: 0x0000F7D7
			public IReadOnlyDictionary<Column, Column> RenameDictionary
			{
				get
				{
					return this.renameDictionary;
				}
			}

			// Token: 0x1700009A RID: 154
			// (get) Token: 0x06000338 RID: 824 RVA: 0x000115DF File Offset: 0x0000F7DF
			public int SkipTo
			{
				get
				{
					return this.skipTo;
				}
			}

			// Token: 0x1700009B RID: 155
			// (get) Token: 0x06000339 RID: 825 RVA: 0x000115E7 File Offset: 0x0000F7E7
			public int MaxRows
			{
				get
				{
					return this.maxRows;
				}
			}

			// Token: 0x1700009C RID: 156
			// (get) Token: 0x0600033A RID: 826
			public abstract SortOrder SortOrder { get; }

			// Token: 0x1700009D RID: 157
			// (get) Token: 0x0600033B RID: 827
			public abstract bool Backwards { get; }

			// Token: 0x0600033C RID: 828
			public abstract SimpleQueryOperator CreateOperator(IConnectionProvider connectionProvider);

			// Token: 0x0600033D RID: 829 RVA: 0x000115EF File Offset: 0x0000F7EF
			internal override void CalculateHashValueForStatisticPurposes(out int simple, out int detail)
			{
				detail = (((this.columnsToFetch != null) ? this.columnsToFetch.Count : 0) ^ this.maxRows ^ this.skipTo);
				simple = 0;
			}

			// Token: 0x0600033E RID: 830 RVA: 0x0001161C File Offset: 0x0000F81C
			internal override bool IsEqualsForStatisticPurposes(DataAccessOperator.DataAccessOperatorDefinition other)
			{
				SimpleQueryOperator.SimpleQueryOperatorDefinition simpleQueryOperatorDefinition = other as SimpleQueryOperator.SimpleQueryOperatorDefinition;
				if (simpleQueryOperatorDefinition == null || (this.columnsToFetch != null && simpleQueryOperatorDefinition.columnsToFetch == null) || (this.columnsToFetch == null && simpleQueryOperatorDefinition.columnsToFetch != null) || (this.columnsToFetch != null && simpleQueryOperatorDefinition.columnsToFetch != null && this.columnsToFetch.Count != simpleQueryOperatorDefinition.columnsToFetch.Count) || this.maxRows != simpleQueryOperatorDefinition.maxRows || this.skipTo != simpleQueryOperatorDefinition.skipTo)
				{
					return false;
				}
				if (this.columnsToFetch != null)
				{
					for (int i = 0; i < this.columnsToFetch.Count; i++)
					{
						if (this.columnsToFetch[i].Name != simpleQueryOperatorDefinition.columnsToFetch[i].Name)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x040000FA RID: 250
			private readonly int maxRows;

			// Token: 0x040000FB RID: 251
			private readonly int skipTo;

			// Token: 0x040000FC RID: 252
			private Table table;

			// Token: 0x040000FD RID: 253
			private SearchCriteria criteria;

			// Token: 0x040000FE RID: 254
			private IList<Column> columnsToFetch;

			// Token: 0x040000FF RID: 255
			private IReadOnlyDictionary<Column, Column> renameDictionary;
		}
	}
}
