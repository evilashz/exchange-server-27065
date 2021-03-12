using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000052 RID: 82
	public abstract class UpdateOperator : DataAccessOperator
	{
		// Token: 0x0600039B RID: 923 RVA: 0x0001264D File Offset: 0x0001084D
		protected UpdateOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableOperator tableOperator, IList<Column> columnsToUpdate, IList<object> valuesToUpdate, bool frequentOperation) : base(connectionProvider, new UpdateOperator.UpdateOperatorDefinition(culture, tableOperator.OperatorDefinition, columnsToUpdate, valuesToUpdate, frequentOperation))
		{
			this.tableOperator = tableOperator;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0001266F File Offset: 0x0001086F
		public TableOperator TableOperator
		{
			get
			{
				return this.tableOperator;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00012677 File Offset: 0x00010877
		protected IList<object> ValuesToUpdate
		{
			get
			{
				return this.OperatorDefinition.ValuesToUpdate;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00012684 File Offset: 0x00010884
		protected IList<Column> ColumnsToUpdate
		{
			get
			{
				return this.OperatorDefinition.ColumnsToUpdate;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00012691 File Offset: 0x00010891
		public UpdateOperator.UpdateOperatorDefinition OperatorDefinition
		{
			get
			{
				return (UpdateOperator.UpdateOperatorDefinition)base.OperatorDefinitionBase;
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001269E File Offset: 0x0001089E
		public override void EnumerateDescendants(Action<DataAccessOperator> operatorAction)
		{
			operatorAction(this);
			operatorAction(this.tableOperator);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x000126B3 File Offset: 0x000108B3
		public override IExecutionPlanner GetExecutionPlanner()
		{
			return base.GetExecutionPlanner() ?? DataAccessOperator.GetExecutionPlannerOrNull(this.tableOperator);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x000126CA File Offset: 0x000108CA
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.tableOperator != null)
			{
				this.tableOperator.Dispose();
			}
		}

		// Token: 0x04000117 RID: 279
		private readonly TableOperator tableOperator;

		// Token: 0x02000053 RID: 83
		public class UpdateOperatorDefinition : DataAccessOperator.DataAccessOperatorDefinition
		{
			// Token: 0x060003A3 RID: 931 RVA: 0x000126E2 File Offset: 0x000108E2
			public UpdateOperatorDefinition(CultureInfo culture, TableOperator.TableOperatorDefinition tableOperatorDefinition, IList<Column> columnsToUpdate, IList<object> valuesToUpdate, bool frequentOperation) : base(culture, frequentOperation)
			{
				this.tableOperatorDefinition = tableOperatorDefinition;
				this.columnsToUpdate = columnsToUpdate;
				this.valuesToUpdate = valuesToUpdate;
				DataAccessOperator.DataAccessOperatorDefinition.CheckValueSizes(this.columnsToUpdate, this.valuesToUpdate);
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x060003A4 RID: 932 RVA: 0x00012714 File Offset: 0x00010914
			internal override string OperatorName
			{
				get
				{
					return "UPDATE";
				}
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x060003A5 RID: 933 RVA: 0x0001271B File Offset: 0x0001091B
			internal IList<object> ValuesToUpdate
			{
				get
				{
					return this.valuesToUpdate;
				}
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x060003A6 RID: 934 RVA: 0x00012723 File Offset: 0x00010923
			internal IList<Column> ColumnsToUpdate
			{
				get
				{
					return this.columnsToUpdate;
				}
			}

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x060003A7 RID: 935 RVA: 0x0001272B File Offset: 0x0001092B
			private TableOperator.TableOperatorDefinition TableOperatorDefinition
			{
				get
				{
					return this.tableOperatorDefinition;
				}
			}

			// Token: 0x060003A8 RID: 936 RVA: 0x00012733 File Offset: 0x00010933
			public override void EnumerateDescendants(Action<DataAccessOperator.DataAccessOperatorDefinition> operatorDefinitionAction)
			{
				operatorDefinitionAction(this);
				operatorDefinitionAction(this.TableOperatorDefinition);
			}

			// Token: 0x060003A9 RID: 937 RVA: 0x00012748 File Offset: 0x00010948
			internal override void AppendToStringBuilder(StringBuilder sb, StringFormatOptions formatOptions, int nestingLevel)
			{
				sb.Append("update");
				bool multiLine = (formatOptions & StringFormatOptions.MultiLine) == StringFormatOptions.MultiLine;
				if (this.TableOperatorDefinition != null && this.TableOperatorDefinition.Table != null)
				{
					sb.Append(" ");
					sb.Append(this.TableOperatorDefinition.Table.Name);
				}
				if (this.ColumnsToUpdate != null)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  columns:[");
					DataAccessOperator.DataAccessOperatorDefinition.AppendColumnsSummaryToStringBuilder(sb, this.ColumnsToUpdate, this.ValuesToUpdate, formatOptions);
					sb.Append("]");
				}
				if (this.TableOperatorDefinition != null)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  from:[");
					base.Indent(sb, multiLine, nestingLevel + 1, false);
					if ((formatOptions & StringFormatOptions.IncludeNestedObjectsId) == StringFormatOptions.IncludeNestedObjectsId)
					{
						sb.Append("op:[");
						sb.Append(this.TableOperatorDefinition.OperatorName);
						sb.Append(" ");
						sb.Append(this.TableOperatorDefinition.GetHashCode());
						sb.Append("] ");
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

			// Token: 0x060003AA RID: 938 RVA: 0x000128BC File Offset: 0x00010ABC
			internal override void CalculateHashValueForStatisticPurposes(out int simple, out int detail)
			{
				int num;
				int num2;
				this.tableOperatorDefinition.CalculateHashValueForStatisticPurposes(out num, out num2);
				detail = (59240 ^ ((this.columnsToUpdate != null) ? this.columnsToUpdate.Count : 0) ^ num2);
				simple = (59240 ^ num);
			}

			// Token: 0x060003AB RID: 939 RVA: 0x00012904 File Offset: 0x00010B04
			internal override bool IsEqualsForStatisticPurposes(DataAccessOperator.DataAccessOperatorDefinition other)
			{
				if (object.ReferenceEquals(this, other))
				{
					return true;
				}
				UpdateOperator.UpdateOperatorDefinition updateOperatorDefinition = other as UpdateOperator.UpdateOperatorDefinition;
				if (updateOperatorDefinition == null || (this.columnsToUpdate != null && updateOperatorDefinition.columnsToUpdate == null) || (this.columnsToUpdate == null && updateOperatorDefinition.columnsToUpdate != null) || (this.columnsToUpdate != null && updateOperatorDefinition.columnsToUpdate != null && this.columnsToUpdate.Count != updateOperatorDefinition.columnsToUpdate.Count) || !this.tableOperatorDefinition.IsEqualsForStatisticPurposes(updateOperatorDefinition.tableOperatorDefinition))
				{
					return false;
				}
				if (this.columnsToUpdate != null)
				{
					for (int i = 0; i < this.columnsToUpdate.Count; i++)
					{
						if (this.columnsToUpdate[i].Name != updateOperatorDefinition.columnsToUpdate[i].Name)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x04000118 RID: 280
			private readonly TableOperator.TableOperatorDefinition tableOperatorDefinition;

			// Token: 0x04000119 RID: 281
			private readonly IList<object> valuesToUpdate;

			// Token: 0x0400011A RID: 282
			private readonly IList<Column> columnsToUpdate;
		}
	}
}
