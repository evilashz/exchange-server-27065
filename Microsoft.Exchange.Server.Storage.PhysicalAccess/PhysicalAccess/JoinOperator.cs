using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000064 RID: 100
	public abstract class JoinOperator : SimpleQueryOperator
	{
		// Token: 0x06000425 RID: 1061 RVA: 0x000141B0 File Offset: 0x000123B0
		protected JoinOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, IList<Column> columnsToFetch, IList<Column> longValueColumnsToPreread, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<Column> keyColumns, SimpleQueryOperator outerQuery, bool frequentOperation) : this(connectionProvider, new JoinOperator.JoinOperatorDefinition(culture, table, columnsToFetch, longValueColumnsToPreread, restriction, renameDictionary, skipTo, maxRows, keyColumns, outerQuery.OperatorDefinition, frequentOperation))
		{
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000141E2 File Offset: 0x000123E2
		protected JoinOperator(IConnectionProvider connectionProvider, JoinOperator.JoinOperatorDefinition definition) : base(connectionProvider, definition)
		{
			this.outerQuery = definition.OuterQueryDefinition.CreateOperator(connectionProvider);
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00014210 File Offset: 0x00012410
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x00014218 File Offset: 0x00012418
		public int PreReadCacheSize
		{
			get
			{
				return this.preReadCacheSize;
			}
			set
			{
				this.preReadCacheSize = value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00014221 File Offset: 0x00012421
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x00014229 File Offset: 0x00012429
		public bool PreReadAhead
		{
			get
			{
				return this.preReadAhead;
			}
			set
			{
				this.preReadAhead = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00014232 File Offset: 0x00012432
		protected IList<Column> KeyColumns
		{
			get
			{
				return this.OperatorDefinition.KeyColumns;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0001423F File Offset: 0x0001243F
		public SimpleQueryOperator OuterQuery
		{
			get
			{
				return this.outerQuery;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00014247 File Offset: 0x00012447
		public IList<Column> LongValueColumnsToPreread
		{
			get
			{
				return this.OperatorDefinition.LongValueColumnsToPreread;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x00014254 File Offset: 0x00012454
		public new JoinOperator.JoinOperatorDefinition OperatorDefinition
		{
			get
			{
				return (JoinOperator.JoinOperatorDefinition)base.OperatorDefinitionBase;
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00014261 File Offset: 0x00012461
		public override void EnumerateDescendants(Action<DataAccessOperator> operatorAction)
		{
			operatorAction(this);
			if (this.outerQuery != null)
			{
				this.outerQuery.EnumerateDescendants(operatorAction);
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001427E File Offset: 0x0001247E
		public override void RemoveChildren()
		{
			this.outerQuery = null;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00014287 File Offset: 0x00012487
		public override IExecutionPlanner GetExecutionPlanner()
		{
			return base.GetExecutionPlanner() ?? DataAccessOperator.GetExecutionPlannerOrNull(this.outerQuery);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001429E File Offset: 0x0001249E
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.outerQuery != null)
			{
				this.outerQuery.Dispose();
			}
		}

		// Token: 0x0400013F RID: 319
		protected const int DefaultPreReadCacheSize = 150;

		// Token: 0x04000140 RID: 320
		private int preReadCacheSize = 150;

		// Token: 0x04000141 RID: 321
		private bool preReadAhead = true;

		// Token: 0x04000142 RID: 322
		private SimpleQueryOperator outerQuery;

		// Token: 0x02000065 RID: 101
		public class JoinOperatorDefinition : SimpleQueryOperator.SimpleQueryOperatorDefinition
		{
			// Token: 0x06000433 RID: 1075 RVA: 0x000142B8 File Offset: 0x000124B8
			public JoinOperatorDefinition(CultureInfo culture, Table table, IList<Column> columnsToFetch, IList<Column> longValueColumnsToPreread, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<Column> keyColumns, SimpleQueryOperator.SimpleQueryOperatorDefinition outerQueryDefinition, bool frequentOperation) : base(culture, table, columnsToFetch, restriction, renameDictionary, skipTo, maxRows, frequentOperation)
			{
				this.keyColumns = keyColumns;
				this.longValueColumnsToPreread = longValueColumnsToPreread;
				this.outerQueryDefinition = outerQueryDefinition;
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x06000434 RID: 1076 RVA: 0x000142F0 File Offset: 0x000124F0
			internal override string OperatorName
			{
				get
				{
					return "JOIN";
				}
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x06000435 RID: 1077 RVA: 0x000142F7 File Offset: 0x000124F7
			public IList<Column> KeyColumns
			{
				get
				{
					return this.keyColumns;
				}
			}

			// Token: 0x17000107 RID: 263
			// (get) Token: 0x06000436 RID: 1078 RVA: 0x000142FF File Offset: 0x000124FF
			public IList<Column> LongValueColumnsToPreread
			{
				get
				{
					return this.longValueColumnsToPreread;
				}
			}

			// Token: 0x17000108 RID: 264
			// (get) Token: 0x06000437 RID: 1079 RVA: 0x00014307 File Offset: 0x00012507
			public SimpleQueryOperator.SimpleQueryOperatorDefinition OuterQueryDefinition
			{
				get
				{
					return this.outerQueryDefinition;
				}
			}

			// Token: 0x17000109 RID: 265
			// (get) Token: 0x06000438 RID: 1080 RVA: 0x0001430F File Offset: 0x0001250F
			public override SortOrder SortOrder
			{
				get
				{
					return this.OuterQueryDefinition.SortOrder;
				}
			}

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x06000439 RID: 1081 RVA: 0x0001431C File Offset: 0x0001251C
			public override bool Backwards
			{
				get
				{
					return this.OuterQueryDefinition.Backwards;
				}
			}

			// Token: 0x0600043A RID: 1082 RVA: 0x00014329 File Offset: 0x00012529
			public override SimpleQueryOperator CreateOperator(IConnectionProvider connectionProvider)
			{
				return Factory.CreateJoinOperator(connectionProvider, this);
			}

			// Token: 0x0600043B RID: 1083 RVA: 0x00014332 File Offset: 0x00012532
			public override void EnumerateDescendants(Action<DataAccessOperator.DataAccessOperatorDefinition> operatorDefinitionAction)
			{
				operatorDefinitionAction(this);
				if (this.OuterQueryDefinition != null)
				{
					this.OuterQueryDefinition.EnumerateDescendants(operatorDefinitionAction);
				}
			}

			// Token: 0x0600043C RID: 1084 RVA: 0x00014350 File Offset: 0x00012550
			internal override void AppendToStringBuilder(StringBuilder sb, StringFormatOptions formatOptions, int nestingLevel)
			{
				sb.Append("select join");
				bool multiLine = (formatOptions & StringFormatOptions.MultiLine) == StringFormatOptions.MultiLine;
				if (this.OuterQueryDefinition != null)
				{
					if (this.OuterQueryDefinition.Table != null)
					{
						sb.Append(" ");
						sb.Append(this.OuterQueryDefinition.Table.Name);
						if (this.OuterQueryDefinition is TableFunctionOperator.TableFunctionOperatorDefinition)
						{
							sb.Append("()");
						}
					}
					else
					{
						sb.Append(" <outer:>");
					}
				}
				else
				{
					sb.Append(" <null>");
				}
				if (base.Table != null)
				{
					sb.Append(" and ");
					sb.Append(base.Table.Name);
				}
				if (base.ColumnsToFetch != null)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  fetch:[");
					DataAccessOperator.DataAccessOperatorDefinition.AppendColumnsSummaryToStringBuilder(sb, base.ColumnsToFetch, null, formatOptions);
					sb.Append("]");
				}
				if (this.KeyColumns != null)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  keyColumns:[");
					for (int i = 0; i < this.KeyColumns.Count; i++)
					{
						if (i != 0)
						{
							sb.Append(", ");
						}
						sb.Append(this.KeyColumns[i].Name);
					}
					sb.Append("]");
				}
				if (base.Criteria != null)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  where:[");
					base.Criteria.AppendToString(sb, formatOptions);
					sb.Append("]");
				}
				if (base.SkipTo != 0)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  skipTo:[");
					sb.Append(base.SkipTo);
					sb.Append("]");
				}
				if (base.MaxRows != 0)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  maxRows:[");
					if ((formatOptions & StringFormatOptions.SkipParametersData) == StringFormatOptions.None || base.MaxRows == 1)
					{
						sb.Append(base.MaxRows);
					}
					else
					{
						sb.Append("X");
					}
					sb.Append("]");
				}
				if (this.OuterQueryDefinition != null)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  outer:[");
					base.Indent(sb, multiLine, nestingLevel + 1, false);
					if ((formatOptions & StringFormatOptions.IncludeNestedObjectsId) == StringFormatOptions.IncludeNestedObjectsId)
					{
						sb.Append("op:[");
						sb.Append(this.OuterQueryDefinition.OperatorName);
						sb.Append(" ");
						sb.Append(this.OuterQueryDefinition.GetHashCode());
						sb.Append("] ");
					}
					this.OuterQueryDefinition.AppendToStringBuilder(sb, formatOptions, nestingLevel + 1);
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  ]");
				}
				if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  frequentOp:[");
					sb.Append(base.FrequentOperation);
					sb.Append("]");
				}
			}

			// Token: 0x0600043D RID: 1085 RVA: 0x00014648 File Offset: 0x00012848
			[Conditional("DEBUG")]
			private void VerifyJoinCriteria()
			{
				for (int i = 0; i < this.KeyColumns.Count; i++)
				{
				}
			}

			// Token: 0x0600043E RID: 1086 RVA: 0x0001466C File Offset: 0x0001286C
			[Conditional("DEBUG")]
			private void VerifyIndex()
			{
				for (int i = 0; i < base.Table.Indexes.Count; i++)
				{
					if (base.Table.Indexes[i].Columns.Count >= this.KeyColumns.Count)
					{
						bool flag = true;
						for (int j = 0; j < this.KeyColumns.Count; j++)
						{
							if (base.Table.Indexes[i].Columns[j] != this.KeyColumns[j])
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							return;
						}
					}
				}
			}

			// Token: 0x0600043F RID: 1087 RVA: 0x00014710 File Offset: 0x00012910
			internal override void CalculateHashValueForStatisticPurposes(out int simple, out int detail)
			{
				int num;
				int num2;
				this.outerQueryDefinition.CalculateHashValueForStatisticPurposes(out num, out num2);
				detail = (53096 ^ num2);
				simple = (53096 ^ num);
			}

			// Token: 0x06000440 RID: 1088 RVA: 0x00014740 File Offset: 0x00012940
			internal override bool IsEqualsForStatisticPurposes(DataAccessOperator.DataAccessOperatorDefinition other)
			{
				if (object.ReferenceEquals(this, other))
				{
					return true;
				}
				JoinOperator.JoinOperatorDefinition joinOperatorDefinition = other as JoinOperator.JoinOperatorDefinition;
				return joinOperatorDefinition != null && this.outerQueryDefinition.IsEqualsForStatisticPurposes(joinOperatorDefinition.outerQueryDefinition);
			}

			// Token: 0x04000143 RID: 323
			private readonly IList<Column> keyColumns;

			// Token: 0x04000144 RID: 324
			private readonly IList<Column> longValueColumnsToPreread;

			// Token: 0x04000145 RID: 325
			private SimpleQueryOperator.SimpleQueryOperatorDefinition outerQueryDefinition;
		}
	}
}
