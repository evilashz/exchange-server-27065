using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200004C RID: 76
	public abstract class InsertOperator : DataAccessOperator
	{
		// Token: 0x06000366 RID: 870 RVA: 0x00011CF0 File Offset: 0x0000FEF0
		protected InsertOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, SimpleQueryOperator simpleQueryOperator, IList<Column> columnsToInsert, IList<object> valuesToInsert, Column columnToFetch, bool frequentOperation) : base(connectionProvider, new InsertOperator.InsertOperatorDefinition(culture, table, (simpleQueryOperator != null) ? simpleQueryOperator.OperatorDefinition : null, columnsToInsert, valuesToInsert, columnToFetch, frequentOperation))
		{
			this.simpleQueryOperator = simpleQueryOperator;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00011D29 File Offset: 0x0000FF29
		public Table Table
		{
			get
			{
				return this.OperatorDefinition.Table;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00011D36 File Offset: 0x0000FF36
		public IList<Column> ColumnsToInsert
		{
			get
			{
				return this.OperatorDefinition.ColumnsToInsert;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00011D43 File Offset: 0x0000FF43
		protected IList<object> ValuesToInsert
		{
			get
			{
				return this.OperatorDefinition.ValuesToInsert;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00011D50 File Offset: 0x0000FF50
		public SimpleQueryOperator SimpleQueryOperator
		{
			get
			{
				return this.simpleQueryOperator;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00011D58 File Offset: 0x0000FF58
		protected Column ColumnToFetch
		{
			get
			{
				return this.OperatorDefinition.ColumnToFetch;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00011D65 File Offset: 0x0000FF65
		public InsertOperator.InsertOperatorDefinition OperatorDefinition
		{
			get
			{
				return (InsertOperator.InsertOperatorDefinition)base.OperatorDefinitionBase;
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00011D72 File Offset: 0x0000FF72
		public override void EnumerateDescendants(Action<DataAccessOperator> operatorAction)
		{
			operatorAction(this);
			if (this.simpleQueryOperator != null)
			{
				this.simpleQueryOperator.EnumerateDescendants(operatorAction);
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00011D8F File Offset: 0x0000FF8F
		public override IExecutionPlanner GetExecutionPlanner()
		{
			return base.GetExecutionPlanner() ?? DataAccessOperator.GetExecutionPlannerOrNull(this.simpleQueryOperator);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00011DA6 File Offset: 0x0000FFA6
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.simpleQueryOperator != null)
			{
				this.simpleQueryOperator.Dispose();
			}
		}

		// Token: 0x0400010D RID: 269
		private readonly SimpleQueryOperator simpleQueryOperator;

		// Token: 0x0200004D RID: 77
		public class InsertOperatorDefinition : DataAccessOperator.DataAccessOperatorDefinition
		{
			// Token: 0x06000370 RID: 880 RVA: 0x00011DC0 File Offset: 0x0000FFC0
			public InsertOperatorDefinition(CultureInfo culture, Table table, SimpleQueryOperator.SimpleQueryOperatorDefinition simpleQueryOperatorDefinition, IList<Column> columnsToInsert, IList<object> valuesToInsert, Column columnToFetch, bool frequentOperation) : base(culture, frequentOperation)
			{
				this.table = table;
				this.columnsToInsert = columnsToInsert;
				this.valuesToInsert = valuesToInsert;
				this.columnToFetch = columnToFetch;
				this.simpleQueryOperatorDefinition = simpleQueryOperatorDefinition;
				if (this.simpleQueryOperatorDefinition == null)
				{
					DataAccessOperator.DataAccessOperatorDefinition.CheckValueSizes(this.columnsToInsert, this.valuesToInsert);
				}
			}

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x06000371 RID: 881 RVA: 0x00011E15 File Offset: 0x00010015
			internal override string OperatorName
			{
				get
				{
					return "INSERT";
				}
			}

			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x06000372 RID: 882 RVA: 0x00011E1C File Offset: 0x0001001C
			public Table Table
			{
				get
				{
					return this.table;
				}
			}

			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x06000373 RID: 883 RVA: 0x00011E24 File Offset: 0x00010024
			public IList<Column> ColumnsToInsert
			{
				get
				{
					return this.columnsToInsert;
				}
			}

			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x06000374 RID: 884 RVA: 0x00011E2C File Offset: 0x0001002C
			public IList<object> ValuesToInsert
			{
				get
				{
					return this.valuesToInsert;
				}
			}

			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x06000375 RID: 885 RVA: 0x00011E34 File Offset: 0x00010034
			public SimpleQueryOperator.SimpleQueryOperatorDefinition SimpleQueryOperatorDefinition
			{
				get
				{
					return this.simpleQueryOperatorDefinition;
				}
			}

			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x06000376 RID: 886 RVA: 0x00011E3C File Offset: 0x0001003C
			public Column ColumnToFetch
			{
				get
				{
					return this.columnToFetch;
				}
			}

			// Token: 0x06000377 RID: 887 RVA: 0x00011E44 File Offset: 0x00010044
			public override void EnumerateDescendants(Action<DataAccessOperator.DataAccessOperatorDefinition> operatorDefinitionAction)
			{
				operatorDefinitionAction(this);
				if (this.SimpleQueryOperatorDefinition != null)
				{
					this.SimpleQueryOperatorDefinition.EnumerateDescendants(operatorDefinitionAction);
				}
			}

			// Token: 0x06000378 RID: 888 RVA: 0x00011E64 File Offset: 0x00010064
			internal override void AppendToStringBuilder(StringBuilder sb, StringFormatOptions formatOptions, int nestingLevel)
			{
				sb.Append("insert");
				bool multiLine = (formatOptions & StringFormatOptions.MultiLine) == StringFormatOptions.MultiLine;
				if (this.Table != null)
				{
					sb.Append(" into ");
					sb.Append(this.Table.Name);
				}
				if (this.ColumnsToInsert != null)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  columns:[");
					DataAccessOperator.DataAccessOperatorDefinition.AppendColumnsSummaryToStringBuilder(sb, this.ColumnsToInsert, this.ValuesToInsert, formatOptions);
					sb.Append("]");
				}
				if (this.SimpleQueryOperatorDefinition != null)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  from:[");
					base.Indent(sb, multiLine, nestingLevel + 1, false);
					if ((formatOptions & StringFormatOptions.IncludeNestedObjectsId) == StringFormatOptions.IncludeNestedObjectsId)
					{
						sb.Append("op:[");
						sb.Append(this.SimpleQueryOperatorDefinition.OperatorName);
						sb.Append(" ");
						sb.Append(this.SimpleQueryOperatorDefinition.GetHashCode());
						sb.Append("] ");
					}
					this.SimpleQueryOperatorDefinition.AppendToStringBuilder(sb, formatOptions, nestingLevel + 1);
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  ]");
				}
				if (this.ColumnToFetch != null)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  fetch:[");
					this.ColumnToFetch.AppendToString(sb, formatOptions);
					sb.Append("]");
				}
				if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  frequentOp:[");
					sb.Append(base.FrequentOperation);
					sb.Append("]");
				}
			}

			// Token: 0x06000379 RID: 889 RVA: 0x00012000 File Offset: 0x00010200
			[Conditional("DEBUG")]
			private void SanityCheckColumnsDebugOnly()
			{
				this.ColumnToFetch != null;
				if (this.SimpleQueryOperatorDefinition != null)
				{
					for (int i = 0; i < this.ColumnsToInsert.Count; i++)
					{
					}
				}
				IList<object> list = this.valuesToInsert;
			}

			// Token: 0x0600037A RID: 890 RVA: 0x00012040 File Offset: 0x00010240
			internal override void CalculateHashValueForStatisticPurposes(out int simple, out int detail)
			{
				int num = 0;
				int num2 = 0;
				if (this.simpleQueryOperatorDefinition != null)
				{
					this.simpleQueryOperatorDefinition.CalculateHashValueForStatisticPurposes(out num, out num2);
				}
				detail = (36712 ^ this.Table.GetHashCode() ^ num2);
				simple = (36712 ^ this.Table.TableClass.GetHashCode() ^ num);
			}

			// Token: 0x0600037B RID: 891 RVA: 0x0001209C File Offset: 0x0001029C
			internal override bool IsEqualsForStatisticPurposes(DataAccessOperator.DataAccessOperatorDefinition other)
			{
				if (object.ReferenceEquals(this, other))
				{
					return true;
				}
				InsertOperator.InsertOperatorDefinition insertOperatorDefinition = other as InsertOperator.InsertOperatorDefinition;
				return insertOperatorDefinition != null && this.Table.Equals(insertOperatorDefinition.Table) && ((this.simpleQueryOperatorDefinition == null && insertOperatorDefinition.simpleQueryOperatorDefinition == null) || (this.simpleQueryOperatorDefinition != null && insertOperatorDefinition.simpleQueryOperatorDefinition != null && this.simpleQueryOperatorDefinition.IsEqualsForStatisticPurposes(insertOperatorDefinition.simpleQueryOperatorDefinition)));
			}

			// Token: 0x0400010E RID: 270
			private readonly SimpleQueryOperator.SimpleQueryOperatorDefinition simpleQueryOperatorDefinition;

			// Token: 0x0400010F RID: 271
			private readonly Table table;

			// Token: 0x04000110 RID: 272
			private readonly IList<Column> columnsToInsert;

			// Token: 0x04000111 RID: 273
			private readonly IList<object> valuesToInsert;

			// Token: 0x04000112 RID: 274
			private readonly Column columnToFetch;
		}
	}
}
