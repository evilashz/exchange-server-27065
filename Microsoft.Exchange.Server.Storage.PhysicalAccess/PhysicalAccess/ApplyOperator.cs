using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000046 RID: 70
	public abstract class ApplyOperator : SimpleQueryOperator
	{
		// Token: 0x0600033F RID: 831 RVA: 0x000116E4 File Offset: 0x0000F8E4
		protected ApplyOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableFunction tableFunction, IList<Column> tableFunctionParameters, IList<Column> columnsToFetch, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, SimpleQueryOperator outerQuery, bool frequentOperation) : this(connectionProvider, new ApplyOperator.ApplyOperatorDefinition(culture, tableFunction, tableFunctionParameters, columnsToFetch, restriction, renameDictionary, skipTo, maxRows, outerQuery.OperatorDefinition, frequentOperation))
		{
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00011714 File Offset: 0x0000F914
		protected ApplyOperator(IConnectionProvider connectionProvider, ApplyOperator.ApplyOperatorDefinition definition) : base(connectionProvider, definition)
		{
			this.outerQuery = definition.OuterQueryDefinition.CreateOperator(connectionProvider);
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00011730 File Offset: 0x0000F930
		protected SimpleQueryOperator OuterQuery
		{
			get
			{
				return this.outerQuery;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00011738 File Offset: 0x0000F938
		public TableFunction TableFunction
		{
			get
			{
				return this.OperatorDefinition.TableFunction;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00011745 File Offset: 0x0000F945
		public IList<Column> TableFunctionParameters
		{
			get
			{
				return this.OperatorDefinition.TableFunctionParameters;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00011752 File Offset: 0x0000F952
		public new ApplyOperator.ApplyOperatorDefinition OperatorDefinition
		{
			get
			{
				return (ApplyOperator.ApplyOperatorDefinition)base.OperatorDefinitionBase;
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0001175F File Offset: 0x0000F95F
		public override void EnumerateDescendants(Action<DataAccessOperator> operatorAction)
		{
			operatorAction(this);
			if (this.OuterQuery != null)
			{
				this.OuterQuery.EnumerateDescendants(operatorAction);
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0001177C File Offset: 0x0000F97C
		public override void RemoveChildren()
		{
			this.outerQuery = null;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00011785 File Offset: 0x0000F985
		public override IExecutionPlanner GetExecutionPlanner()
		{
			return base.GetExecutionPlanner() ?? DataAccessOperator.GetExecutionPlannerOrNull(this.outerQuery);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0001179C File Offset: 0x0000F99C
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.outerQuery != null)
			{
				this.outerQuery.Dispose();
			}
		}

		// Token: 0x04000100 RID: 256
		private SimpleQueryOperator outerQuery;

		// Token: 0x02000047 RID: 71
		public class ApplyOperatorDefinition : SimpleQueryOperator.SimpleQueryOperatorDefinition
		{
			// Token: 0x06000349 RID: 841 RVA: 0x000117B4 File Offset: 0x0000F9B4
			public ApplyOperatorDefinition(CultureInfo culture, TableFunction tableFunction, IList<Column> tableFunctionParameters, IList<Column> columnsToFetch, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, SimpleQueryOperator.SimpleQueryOperatorDefinition outerQueryDefinition, bool frequentOperation) : base(culture, tableFunction, columnsToFetch, restriction, renameDictionary, skipTo, maxRows, frequentOperation)
			{
				this.tableFunctionParameters = tableFunctionParameters;
				this.outerQueryDefinition = outerQueryDefinition;
			}

			// Token: 0x170000A2 RID: 162
			// (get) Token: 0x0600034A RID: 842 RVA: 0x000117E4 File Offset: 0x0000F9E4
			internal override string OperatorName
			{
				get
				{
					return "APPLY";
				}
			}

			// Token: 0x170000A3 RID: 163
			// (get) Token: 0x0600034B RID: 843 RVA: 0x000117EB File Offset: 0x0000F9EB
			public SimpleQueryOperator.SimpleQueryOperatorDefinition OuterQueryDefinition
			{
				get
				{
					return this.outerQueryDefinition;
				}
			}

			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x0600034C RID: 844 RVA: 0x000117F3 File Offset: 0x0000F9F3
			public override SortOrder SortOrder
			{
				get
				{
					return this.OuterQueryDefinition.SortOrder;
				}
			}

			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x0600034D RID: 845 RVA: 0x00011800 File Offset: 0x0000FA00
			public override bool Backwards
			{
				get
				{
					return this.OuterQueryDefinition.Backwards;
				}
			}

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x0600034E RID: 846 RVA: 0x0001180D File Offset: 0x0000FA0D
			public TableFunction TableFunction
			{
				get
				{
					return (TableFunction)base.Table;
				}
			}

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x0600034F RID: 847 RVA: 0x0001181A File Offset: 0x0000FA1A
			public IList<Column> TableFunctionParameters
			{
				get
				{
					return this.tableFunctionParameters;
				}
			}

			// Token: 0x06000350 RID: 848 RVA: 0x00011822 File Offset: 0x0000FA22
			public override SimpleQueryOperator CreateOperator(IConnectionProvider connectionProvider)
			{
				return Factory.CreateApplyOperator(connectionProvider, this);
			}

			// Token: 0x06000351 RID: 849 RVA: 0x0001182B File Offset: 0x0000FA2B
			public override void EnumerateDescendants(Action<DataAccessOperator.DataAccessOperatorDefinition> operatorDefinitionAction)
			{
				operatorDefinitionAction(this);
				if (this.OuterQueryDefinition != null)
				{
					this.OuterQueryDefinition.EnumerateDescendants(operatorDefinitionAction);
				}
			}

			// Token: 0x06000352 RID: 850 RVA: 0x00011848 File Offset: 0x0000FA48
			internal override void AppendToStringBuilder(StringBuilder sb, StringFormatOptions formatOptions, int nestingLevel)
			{
				sb.Append("select apply");
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
				sb.Append(" and ");
				sb.Append(this.TableFunction.Name);
				sb.Append("(");
				if (this.TableFunctionParameters != null)
				{
					for (int i = 0; i < this.TableFunctionParameters.Count; i++)
					{
						if (i > 0)
						{
							sb.Append(", ");
						}
						sb.Append(this.TableFunctionParameters[i].Name);
					}
				}
				sb.Append(")");
				if (base.ColumnsToFetch != null)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  fetch:[");
					DataAccessOperator.DataAccessOperatorDefinition.AppendColumnsSummaryToStringBuilder(sb, base.ColumnsToFetch, null, formatOptions);
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
					sb.Append("    ]");
				}
				if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  frequentOp:[");
					sb.Append(base.FrequentOperation);
					sb.Append("]");
				}
			}

			// Token: 0x06000353 RID: 851 RVA: 0x00011B28 File Offset: 0x0000FD28
			internal override void CalculateHashValueForStatisticPurposes(out int simple, out int detail)
			{
				int num;
				int num2;
				this.outerQueryDefinition.CalculateHashValueForStatisticPurposes(out num, out num2);
				detail = (47032 ^ base.Table.GetHashCode() ^ num2);
				simple = (47032 ^ base.Table.TableClass.GetHashCode() ^ num);
			}

			// Token: 0x06000354 RID: 852 RVA: 0x00011B78 File Offset: 0x0000FD78
			internal override bool IsEqualsForStatisticPurposes(DataAccessOperator.DataAccessOperatorDefinition other)
			{
				if (object.ReferenceEquals(this, other))
				{
					return true;
				}
				ApplyOperator.ApplyOperatorDefinition applyOperatorDefinition = other as ApplyOperator.ApplyOperatorDefinition;
				return applyOperatorDefinition != null && base.Table.Equals(applyOperatorDefinition.Table) && this.outerQueryDefinition.IsEqualsForStatisticPurposes(applyOperatorDefinition.outerQueryDefinition);
			}

			// Token: 0x04000101 RID: 257
			private readonly IList<Column> tableFunctionParameters;

			// Token: 0x04000102 RID: 258
			private SimpleQueryOperator.SimpleQueryOperatorDefinition outerQueryDefinition;
		}
	}
}
