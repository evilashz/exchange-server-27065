using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PhysicalAccess.ExtensionMethods;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000074 RID: 116
	public abstract class IndexAndOperator : SimpleQueryOperator
	{
		// Token: 0x0600052C RID: 1324 RVA: 0x00018A54 File Offset: 0x00016C54
		protected IndexAndOperator(CultureInfo culture, IConnectionProvider connectionProvider, IList<Column> columnsToFetch, SimpleQueryOperator[] queryOperators, bool frequentOperation) : this(connectionProvider, new IndexAndOperator.IndexAndOperatorDefinition(culture, columnsToFetch, (from op in queryOperators
		select op.OperatorDefinition).ToArray<SimpleQueryOperator.SimpleQueryOperatorDefinition>(), frequentOperation))
		{
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00018A8F File Offset: 0x00016C8F
		protected IndexAndOperator(IConnectionProvider connectionProvider, IndexAndOperator.IndexAndOperatorDefinition definition) : base(connectionProvider, definition)
		{
			this.queryOperators = ((definition.QueryOperatorDefinitions != null) ? definition.QueryOperatorDefinitions.CreateOperators(connectionProvider).ToArray<SimpleQueryOperator>() : null);
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00018ABB File Offset: 0x00016CBB
		protected SimpleQueryOperator[] QueryOperators
		{
			get
			{
				return this.queryOperators;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00018AC3 File Offset: 0x00016CC3
		public new IndexAndOperator.IndexAndOperatorDefinition OperatorDefinition
		{
			get
			{
				return (IndexAndOperator.IndexAndOperatorDefinition)base.OperatorDefinitionBase;
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00018AD0 File Offset: 0x00016CD0
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

		// Token: 0x06000531 RID: 1329 RVA: 0x00018B0C File Offset: 0x00016D0C
		public override void RemoveChildren()
		{
			this.queryOperators = null;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00018B18 File Offset: 0x00016D18
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

		// Token: 0x04000193 RID: 403
		private SimpleQueryOperator[] queryOperators;

		// Token: 0x02000075 RID: 117
		public class IndexAndOperatorDefinition : SimpleQueryOperator.SimpleQueryOperatorDefinition
		{
			// Token: 0x06000534 RID: 1332 RVA: 0x00018B50 File Offset: 0x00016D50
			public IndexAndOperatorDefinition(CultureInfo culture, IList<Column> columnsToFetch, SimpleQueryOperator.SimpleQueryOperatorDefinition[] queryOperatorDefinitions, bool frequentOperation) : base(culture, null, columnsToFetch, null, null, 0, 0, frequentOperation)
			{
				this.queryOperatorDefinitions = queryOperatorDefinitions;
			}

			// Token: 0x17000165 RID: 357
			// (get) Token: 0x06000535 RID: 1333 RVA: 0x00018B73 File Offset: 0x00016D73
			internal override string OperatorName
			{
				get
				{
					return "IndexAND";
				}
			}

			// Token: 0x17000166 RID: 358
			// (get) Token: 0x06000536 RID: 1334 RVA: 0x00018B7A File Offset: 0x00016D7A
			public override SortOrder SortOrder
			{
				get
				{
					return SortOrder.Empty;
				}
			}

			// Token: 0x17000167 RID: 359
			// (get) Token: 0x06000537 RID: 1335 RVA: 0x00018B81 File Offset: 0x00016D81
			public override bool Backwards
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000168 RID: 360
			// (get) Token: 0x06000538 RID: 1336 RVA: 0x00018B84 File Offset: 0x00016D84
			public SimpleQueryOperator.SimpleQueryOperatorDefinition[] QueryOperatorDefinitions
			{
				get
				{
					return this.queryOperatorDefinitions;
				}
			}

			// Token: 0x06000539 RID: 1337 RVA: 0x00018B8C File Offset: 0x00016D8C
			public override SimpleQueryOperator CreateOperator(IConnectionProvider connectionProvider)
			{
				return Factory.CreateIndexAndOperator(connectionProvider, this);
			}

			// Token: 0x0600053A RID: 1338 RVA: 0x00018B98 File Offset: 0x00016D98
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

			// Token: 0x0600053B RID: 1339 RVA: 0x00018BD4 File Offset: 0x00016DD4
			internal override void AppendToStringBuilder(StringBuilder sb, StringFormatOptions formatOptions, int nestingLevel)
			{
				sb.Append("select indexAnd:[");
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
						sb.Append("op:[");
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

			// Token: 0x0600053C RID: 1340 RVA: 0x00018D38 File Offset: 0x00016F38
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

			// Token: 0x0600053D RID: 1341 RVA: 0x00018D78 File Offset: 0x00016F78
			internal override void CalculateHashValueForStatisticPurposes(out int simple, out int detail)
			{
				int num;
				int num2;
				this.queryOperatorDefinitions[0].CalculateHashValueForStatisticPurposes(out num, out num2);
				int num3;
				int num4;
				this.queryOperatorDefinitions[1].CalculateHashValueForStatisticPurposes(out num3, out num4);
				detail = (42936 ^ this.queryOperatorDefinitions.Length ^ num2 ^ num4);
				simple = (42936 ^ num ^ num3);
			}

			// Token: 0x0600053E RID: 1342 RVA: 0x00018DC8 File Offset: 0x00016FC8
			internal override bool IsEqualsForStatisticPurposes(DataAccessOperator.DataAccessOperatorDefinition other)
			{
				if (object.ReferenceEquals(this, other))
				{
					return true;
				}
				IndexAndOperator.IndexAndOperatorDefinition indexAndOperatorDefinition = other as IndexAndOperator.IndexAndOperatorDefinition;
				return indexAndOperatorDefinition != null && this.queryOperatorDefinitions.Length == indexAndOperatorDefinition.queryOperatorDefinitions.Length && this.queryOperatorDefinitions[0].IsEqualsForStatisticPurposes(indexAndOperatorDefinition.queryOperatorDefinitions[0]) && this.queryOperatorDefinitions[1].IsEqualsForStatisticPurposes(indexAndOperatorDefinition.queryOperatorDefinitions[1]);
			}

			// Token: 0x04000195 RID: 405
			private SimpleQueryOperator.SimpleQueryOperatorDefinition[] queryOperatorDefinitions;
		}
	}
}
