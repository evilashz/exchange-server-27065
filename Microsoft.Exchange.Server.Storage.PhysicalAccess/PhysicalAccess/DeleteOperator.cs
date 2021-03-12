using System;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200004E RID: 78
	public abstract class DeleteOperator : DataAccessOperator
	{
		// Token: 0x0600037C RID: 892 RVA: 0x00012108 File Offset: 0x00010308
		protected DeleteOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableOperator tableOperator, bool frequentOperation) : base(connectionProvider, new DeleteOperator.DeleteOperatorDefinition(culture, tableOperator.OperatorDefinition, frequentOperation))
		{
			this.tableOperator = tableOperator;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00012126 File Offset: 0x00010326
		public TableOperator TableOperator
		{
			get
			{
				return this.tableOperator;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0001212E File Offset: 0x0001032E
		public DeleteOperator.DeleteOperatorDefinition OperatorDefinition
		{
			get
			{
				return (DeleteOperator.DeleteOperatorDefinition)base.OperatorDefinitionBase;
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001213B File Offset: 0x0001033B
		public override void EnumerateDescendants(Action<DataAccessOperator> operatorAction)
		{
			operatorAction(this);
			operatorAction(this.tableOperator);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00012150 File Offset: 0x00010350
		public override IExecutionPlanner GetExecutionPlanner()
		{
			return base.GetExecutionPlanner() ?? DataAccessOperator.GetExecutionPlannerOrNull(this.tableOperator);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00012167 File Offset: 0x00010367
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.tableOperator != null)
			{
				this.tableOperator.Dispose();
			}
		}

		// Token: 0x04000113 RID: 275
		private readonly TableOperator tableOperator;

		// Token: 0x0200004F RID: 79
		public class DeleteOperatorDefinition : DataAccessOperator.DataAccessOperatorDefinition
		{
			// Token: 0x06000382 RID: 898 RVA: 0x0001217F File Offset: 0x0001037F
			public DeleteOperatorDefinition(CultureInfo culture, TableOperator.TableOperatorDefinition tableOperatorDefinition, bool frequentOperation) : base(culture, frequentOperation)
			{
				this.tableOperatorDefinition = tableOperatorDefinition;
			}

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x06000383 RID: 899 RVA: 0x00012190 File Offset: 0x00010390
			internal override string OperatorName
			{
				get
				{
					return "DELETE";
				}
			}

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x06000384 RID: 900 RVA: 0x00012197 File Offset: 0x00010397
			private TableOperator.TableOperatorDefinition TableOperatorDefinition
			{
				get
				{
					return this.tableOperatorDefinition;
				}
			}

			// Token: 0x06000385 RID: 901 RVA: 0x0001219F File Offset: 0x0001039F
			public override void EnumerateDescendants(Action<DataAccessOperator.DataAccessOperatorDefinition> operatorDefinitionAction)
			{
				operatorDefinitionAction(this);
				operatorDefinitionAction(this.TableOperatorDefinition);
			}

			// Token: 0x06000386 RID: 902 RVA: 0x000121B4 File Offset: 0x000103B4
			internal override void AppendToStringBuilder(StringBuilder sb, StringFormatOptions formatOptions, int nestingLevel)
			{
				sb.Append("delete");
				bool multiLine = (formatOptions & StringFormatOptions.MultiLine) == StringFormatOptions.MultiLine;
				if (this.TableOperatorDefinition != null && this.TableOperatorDefinition.Table != null)
				{
					sb.Append(" from ");
					sb.Append(this.TableOperatorDefinition.Table.Name);
				}
				if (this.TableOperatorDefinition != null)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  operator:[");
					base.Indent(sb, multiLine, nestingLevel + 1, false);
					if ((formatOptions & StringFormatOptions.IncludeNestedObjectsId) == StringFormatOptions.IncludeNestedObjectsId)
					{
						sb.Append("op:[");
						sb.Append(this.TableOperatorDefinition.OperatorName);
						sb.Append(" ");
						sb.Append(this.TableOperatorDefinition.GetHashCode());
						sb.Append("]");
					}
					this.TableOperatorDefinition.AppendToStringBuilder(sb, formatOptions, nestingLevel + 1);
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

			// Token: 0x06000387 RID: 903 RVA: 0x000122E8 File Offset: 0x000104E8
			internal override void CalculateHashValueForStatisticPurposes(out int simple, out int detail)
			{
				int num;
				int num2;
				this.tableOperatorDefinition.CalculateHashValueForStatisticPurposes(out num, out num2);
				detail = (55224 ^ num2);
				simple = (55224 ^ num);
			}

			// Token: 0x06000388 RID: 904 RVA: 0x00012318 File Offset: 0x00010518
			internal override bool IsEqualsForStatisticPurposes(DataAccessOperator.DataAccessOperatorDefinition other)
			{
				if (object.ReferenceEquals(this, other))
				{
					return true;
				}
				DeleteOperator.DeleteOperatorDefinition deleteOperatorDefinition = other as DeleteOperator.DeleteOperatorDefinition;
				return deleteOperatorDefinition != null && this.tableOperatorDefinition.IsEqualsForStatisticPurposes(deleteOperatorDefinition.tableOperatorDefinition);
			}

			// Token: 0x04000114 RID: 276
			private readonly TableOperator.TableOperatorDefinition tableOperatorDefinition;
		}
	}
}
