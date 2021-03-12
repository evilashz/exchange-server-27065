using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PhysicalAccess.ExtensionMethods;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000076 RID: 118
	public abstract class IndexOrOperator : SimpleQueryOperator
	{
		// Token: 0x0600053F RID: 1343 RVA: 0x00018E32 File Offset: 0x00017032
		protected IndexOrOperator(CultureInfo culture, IConnectionProvider connectionProvider, IList<Column> columnsToFetch, SimpleQueryOperator[] queryOperators, bool frequentOperation) : this(connectionProvider, new IndexOrOperator.IndexOrOperatorDefinition(culture, columnsToFetch, (from op in queryOperators
		select op.OperatorDefinition).ToArray<SimpleQueryOperator.SimpleQueryOperatorDefinition>(), frequentOperation))
		{
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00018E6D File Offset: 0x0001706D
		protected IndexOrOperator(IConnectionProvider connectionProvider, IndexOrOperator.IndexOrOperatorDefinition definition) : base(connectionProvider, definition)
		{
			this.queryOperators = ((definition.QueryOperatorDefinitions != null) ? definition.QueryOperatorDefinitions.CreateOperators(connectionProvider).ToArray<SimpleQueryOperator>() : null);
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x00018E99 File Offset: 0x00017099
		protected SimpleQueryOperator[] QueryOperators
		{
			get
			{
				return this.queryOperators;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00018EA1 File Offset: 0x000170A1
		public new IndexOrOperator.IndexOrOperatorDefinition OperatorDefinition
		{
			get
			{
				return (IndexOrOperator.IndexOrOperatorDefinition)base.OperatorDefinitionBase;
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00018EB0 File Offset: 0x000170B0
		public override void EnumerateDescendants(Action<DataAccessOperator> operatorAction)
		{
			operatorAction(this);
			if (this.QueryOperators != null)
			{
				foreach (SimpleQueryOperator dataAccessOperator in this.QueryOperators)
				{
					dataAccessOperator.EnumerateDescendants(operatorAction);
				}
			}
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00018EEC File Offset: 0x000170EC
		public override void RemoveChildren()
		{
			this.queryOperators = null;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00018EF8 File Offset: 0x000170F8
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.queryOperators != null)
			{
				foreach (SimpleQueryOperator simpleQueryOperator in this.queryOperators)
				{
					simpleQueryOperator.Dispose();
				}
			}
		}

		// Token: 0x04000196 RID: 406
		private SimpleQueryOperator[] queryOperators;

		// Token: 0x02000077 RID: 119
		public class IndexOrOperatorDefinition : SimpleQueryOperator.SimpleQueryOperatorDefinition
		{
			// Token: 0x06000547 RID: 1351 RVA: 0x00018F30 File Offset: 0x00017130
			public IndexOrOperatorDefinition(CultureInfo culture, IList<Column> columnsToFetch, SimpleQueryOperator.SimpleQueryOperatorDefinition[] queryOperatorDefinitions, bool frequentOperation) : base(culture, null, columnsToFetch, null, null, 0, 0, frequentOperation)
			{
				this.queryOperatorDefinitions = queryOperatorDefinitions;
			}

			// Token: 0x1700016B RID: 363
			// (get) Token: 0x06000548 RID: 1352 RVA: 0x00018F53 File Offset: 0x00017153
			internal override string OperatorName
			{
				get
				{
					return "IndexOR";
				}
			}

			// Token: 0x1700016C RID: 364
			// (get) Token: 0x06000549 RID: 1353 RVA: 0x00018F5A File Offset: 0x0001715A
			public override SortOrder SortOrder
			{
				get
				{
					return SortOrder.Empty;
				}
			}

			// Token: 0x1700016D RID: 365
			// (get) Token: 0x0600054A RID: 1354 RVA: 0x00018F61 File Offset: 0x00017161
			public override bool Backwards
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x0600054B RID: 1355 RVA: 0x00018F64 File Offset: 0x00017164
			public SimpleQueryOperator.SimpleQueryOperatorDefinition[] QueryOperatorDefinitions
			{
				get
				{
					return this.queryOperatorDefinitions;
				}
			}

			// Token: 0x0600054C RID: 1356 RVA: 0x00018F6C File Offset: 0x0001716C
			public override SimpleQueryOperator CreateOperator(IConnectionProvider connectionProvider)
			{
				return Factory.CreateIndexOrOperator(connectionProvider, this);
			}

			// Token: 0x0600054D RID: 1357 RVA: 0x00018F78 File Offset: 0x00017178
			public override void EnumerateDescendants(Action<DataAccessOperator.DataAccessOperatorDefinition> operatorDefinitionAction)
			{
				operatorDefinitionAction(this);
				if (this.QueryOperatorDefinitions != null)
				{
					foreach (SimpleQueryOperator.SimpleQueryOperatorDefinition dataAccessOperatorDefinition in this.QueryOperatorDefinitions)
					{
						dataAccessOperatorDefinition.EnumerateDescendants(operatorDefinitionAction);
					}
				}
			}

			// Token: 0x0600054E RID: 1358 RVA: 0x00018FB4 File Offset: 0x000171B4
			internal override void AppendToStringBuilder(StringBuilder sb, StringFormatOptions formatOptions, int nestingLevel)
			{
				sb.Append("select indexOr:[");
				bool multiLine = (formatOptions & StringFormatOptions.MultiLine) == StringFormatOptions.MultiLine;
				bool flag = true;
				foreach (Column column in base.ColumnsToFetch)
				{
					if (!flag)
					{
						sb.Append(", ");
					}
					column.AppendToString(sb, StringFormatOptions.None);
					flag = false;
				}
				sb.Append("]");
				base.Indent(sb, multiLine, nestingLevel, false);
				sb.Append("  from:[");
				foreach (SimpleQueryOperator.SimpleQueryOperatorDefinition simpleQueryOperatorDefinition in this.QueryOperatorDefinitions)
				{
					base.Indent(sb, multiLine, nestingLevel + 1, false);
					if ((formatOptions & StringFormatOptions.IncludeNestedObjectsId) == StringFormatOptions.IncludeNestedObjectsId)
					{
						sb.Append(" op:[");
						sb.Append(simpleQueryOperatorDefinition.OperatorName);
						sb.Append(" ");
						sb.Append(simpleQueryOperatorDefinition.GetHashCode());
						sb.Append("] ");
					}
					simpleQueryOperatorDefinition.AppendToStringBuilder(sb, formatOptions, nestingLevel + 1);
				}
				base.Indent(sb, multiLine, nestingLevel, false);
				sb.Append("  ]");
				if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  frequentOp:[");
					sb.Append(base.FrequentOperation);
					sb.Append("]");
				}
			}

			// Token: 0x0600054F RID: 1359 RVA: 0x00019118 File Offset: 0x00017318
			[Conditional("DEBUG")]
			private void AssertColumnIntegrity()
			{
				for (int i = 0; i < this.QueryOperatorDefinitions.Length; i++)
				{
					SimpleQueryOperator.SimpleQueryOperatorDefinition simpleQueryOperatorDefinition = this.QueryOperatorDefinitions[i];
					for (int j = 0; j < base.ColumnsToFetch.Count; j++)
					{
					}
				}
			}

			// Token: 0x06000550 RID: 1360 RVA: 0x00019158 File Offset: 0x00017358
			internal override void CalculateHashValueForStatisticPurposes(out int simple, out int detail)
			{
				int num;
				int num2;
				this.queryOperatorDefinitions[0].CalculateHashValueForStatisticPurposes(out num, out num2);
				int num3;
				int num4;
				this.queryOperatorDefinitions[1].CalculateHashValueForStatisticPurposes(out num3, out num4);
				detail = (61288 ^ this.queryOperatorDefinitions.Length ^ num2 ^ num4);
				simple = (61288 ^ num ^ num3);
			}

			// Token: 0x06000551 RID: 1361 RVA: 0x000191A8 File Offset: 0x000173A8
			internal override bool IsEqualsForStatisticPurposes(DataAccessOperator.DataAccessOperatorDefinition other)
			{
				if (object.ReferenceEquals(this, other))
				{
					return true;
				}
				IndexOrOperator.IndexOrOperatorDefinition indexOrOperatorDefinition = other as IndexOrOperator.IndexOrOperatorDefinition;
				return indexOrOperatorDefinition != null && this.queryOperatorDefinitions.Length == indexOrOperatorDefinition.queryOperatorDefinitions.Length && this.queryOperatorDefinitions[0].IsEqualsForStatisticPurposes(indexOrOperatorDefinition.queryOperatorDefinitions[0]) && this.queryOperatorDefinitions[1].IsEqualsForStatisticPurposes(indexOrOperatorDefinition.queryOperatorDefinitions[1]);
			}

			// Token: 0x04000198 RID: 408
			private SimpleQueryOperator.SimpleQueryOperatorDefinition[] queryOperatorDefinitions;
		}
	}
}
