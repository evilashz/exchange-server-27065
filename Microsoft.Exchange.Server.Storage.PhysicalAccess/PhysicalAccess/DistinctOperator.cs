using System;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000050 RID: 80
	public abstract class DistinctOperator : SimpleQueryOperator
	{
		// Token: 0x06000389 RID: 905 RVA: 0x0001234D File Offset: 0x0001054D
		protected DistinctOperator(IConnectionProvider connectionProvider, int skipTo, int maxRows, SimpleQueryOperator outerQuery, bool frequentOperation) : this(connectionProvider, new DistinctOperator.DistinctOperatorDefinition(skipTo, maxRows, outerQuery.OperatorDefinition, frequentOperation))
		{
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00012366 File Offset: 0x00010566
		protected DistinctOperator(IConnectionProvider connectionProvider, DistinctOperator.DistinctOperatorDefinition definition) : base(connectionProvider, definition)
		{
			this.outerQuery = definition.OuterQueryDefinition.CreateOperator(connectionProvider);
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00012382 File Offset: 0x00010582
		public SimpleQueryOperator OuterQuery
		{
			get
			{
				return this.outerQuery;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0001238A File Offset: 0x0001058A
		public new DistinctOperator.DistinctOperatorDefinition OperatorDefinition
		{
			get
			{
				return (DistinctOperator.DistinctOperatorDefinition)base.OperatorDefinitionBase;
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00012397 File Offset: 0x00010597
		public override void EnumerateDescendants(Action<DataAccessOperator> operatorAction)
		{
			operatorAction(this);
			if (this.outerQuery != null)
			{
				this.outerQuery.EnumerateDescendants(operatorAction);
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x000123B4 File Offset: 0x000105B4
		public override void RemoveChildren()
		{
			this.outerQuery = null;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000123BD File Offset: 0x000105BD
		public override IExecutionPlanner GetExecutionPlanner()
		{
			return base.GetExecutionPlanner() ?? DataAccessOperator.GetExecutionPlannerOrNull(this.outerQuery);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000123D4 File Offset: 0x000105D4
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.outerQuery != null)
			{
				this.outerQuery.Dispose();
			}
		}

		// Token: 0x04000115 RID: 277
		private SimpleQueryOperator outerQuery;

		// Token: 0x02000051 RID: 81
		public class DistinctOperatorDefinition : SimpleQueryOperator.SimpleQueryOperatorDefinition
		{
			// Token: 0x06000391 RID: 913 RVA: 0x000123EC File Offset: 0x000105EC
			public DistinctOperatorDefinition(int skipTo, int maxRows, SimpleQueryOperator.SimpleQueryOperatorDefinition outerQueryDefinition, bool frequentOperation) : base(outerQueryDefinition.Culture, outerQueryDefinition.Table, outerQueryDefinition.ColumnsToFetch, outerQueryDefinition.Criteria, outerQueryDefinition.RenameDictionary, skipTo, maxRows, frequentOperation)
			{
				this.outerQueryDefinition = outerQueryDefinition;
			}

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x06000392 RID: 914 RVA: 0x00012428 File Offset: 0x00010628
			internal override string OperatorName
			{
				get
				{
					return "UNIQUE";
				}
			}

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x06000393 RID: 915 RVA: 0x0001242F File Offset: 0x0001062F
			public SimpleQueryOperator.SimpleQueryOperatorDefinition OuterQueryDefinition
			{
				get
				{
					return this.outerQueryDefinition;
				}
			}

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x06000394 RID: 916 RVA: 0x00012437 File Offset: 0x00010637
			public override SortOrder SortOrder
			{
				get
				{
					return SortOrder.Empty;
				}
			}

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x06000395 RID: 917 RVA: 0x0001243E File Offset: 0x0001063E
			public override bool Backwards
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000396 RID: 918 RVA: 0x00012441 File Offset: 0x00010641
			public override SimpleQueryOperator CreateOperator(IConnectionProvider connectionProvider)
			{
				return Factory.CreateDistinctOperator(connectionProvider, this);
			}

			// Token: 0x06000397 RID: 919 RVA: 0x0001244A File Offset: 0x0001064A
			public override void EnumerateDescendants(Action<DataAccessOperator.DataAccessOperatorDefinition> operatorDefinitionAction)
			{
				operatorDefinitionAction(this);
				if (this.OuterQueryDefinition != null)
				{
					this.OuterQueryDefinition.EnumerateDescendants(operatorDefinitionAction);
				}
			}

			// Token: 0x06000398 RID: 920 RVA: 0x00012468 File Offset: 0x00010668
			internal override void AppendToStringBuilder(StringBuilder sb, StringFormatOptions formatOptions, int nestingLevel)
			{
				sb.Append("select distinct");
				bool multiLine = (formatOptions & StringFormatOptions.MultiLine) == StringFormatOptions.MultiLine;
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
					sb.Append("  from:[");
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

			// Token: 0x06000399 RID: 921 RVA: 0x000125E8 File Offset: 0x000107E8
			internal override void CalculateHashValueForStatisticPurposes(out int simple, out int detail)
			{
				int num;
				int num2;
				this.outerQueryDefinition.CalculateHashValueForStatisticPurposes(out num, out num2);
				detail = (47921 ^ num2);
				simple = (47921 ^ num);
			}

			// Token: 0x0600039A RID: 922 RVA: 0x00012618 File Offset: 0x00010818
			internal override bool IsEqualsForStatisticPurposes(DataAccessOperator.DataAccessOperatorDefinition other)
			{
				if (object.ReferenceEquals(this, other))
				{
					return true;
				}
				DistinctOperator.DistinctOperatorDefinition distinctOperatorDefinition = other as DistinctOperator.DistinctOperatorDefinition;
				return distinctOperatorDefinition != null && this.outerQueryDefinition.IsEqualsForStatisticPurposes(distinctOperatorDefinition.outerQueryDefinition);
			}

			// Token: 0x04000116 RID: 278
			private SimpleQueryOperator.SimpleQueryOperatorDefinition outerQueryDefinition;
		}
	}
}
