using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000078 RID: 120
	public abstract class IndexNotOperator : SimpleQueryOperator
	{
		// Token: 0x06000552 RID: 1362 RVA: 0x0001920A File Offset: 0x0001740A
		protected IndexNotOperator(CultureInfo culture, IConnectionProvider connectionProvider, IList<Column> columnsToFetch, SimpleQueryOperator queryOperator, SimpleQueryOperator notOperator, bool frequentOperation) : this(connectionProvider, new IndexNotOperator.IndexNotOperatorDefinition(culture, columnsToFetch, queryOperator.OperatorDefinition, notOperator.OperatorDefinition, frequentOperation))
		{
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001922C File Offset: 0x0001742C
		protected IndexNotOperator(IConnectionProvider connectionProvider, IndexNotOperator.IndexNotOperatorDefinition definition) : base(connectionProvider, definition)
		{
			this.queryOperator = ((definition.QueryOperatorDefinition != null) ? definition.QueryOperatorDefinition.CreateOperator(connectionProvider) : null);
			this.notOperator = ((definition.NotOperatorDefinition != null) ? definition.NotOperatorDefinition.CreateOperator(connectionProvider) : null);
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x0001927B File Offset: 0x0001747B
		protected SimpleQueryOperator QueryOperator
		{
			get
			{
				return this.queryOperator;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00019283 File Offset: 0x00017483
		protected SimpleQueryOperator NotOperator
		{
			get
			{
				return this.notOperator;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0001928B File Offset: 0x0001748B
		public new IndexNotOperator.IndexNotOperatorDefinition OperatorDefinition
		{
			get
			{
				return (IndexNotOperator.IndexNotOperatorDefinition)base.OperatorDefinitionBase;
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00019298 File Offset: 0x00017498
		public override void EnumerateDescendants(Action<DataAccessOperator> operatorAction)
		{
			operatorAction(this);
			if (this.queryOperator != null)
			{
				this.queryOperator.EnumerateDescendants(operatorAction);
			}
			if (this.notOperator != null)
			{
				this.notOperator.EnumerateDescendants(operatorAction);
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x000192C9 File Offset: 0x000174C9
		public override void RemoveChildren()
		{
			this.queryOperator = null;
			this.notOperator = null;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000192D9 File Offset: 0x000174D9
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.queryOperator != null)
				{
					this.queryOperator.Dispose();
				}
				if (this.notOperator != null)
				{
					this.notOperator.Dispose();
				}
			}
		}

		// Token: 0x04000199 RID: 409
		private SimpleQueryOperator queryOperator;

		// Token: 0x0400019A RID: 410
		private SimpleQueryOperator notOperator;

		// Token: 0x02000079 RID: 121
		public class IndexNotOperatorDefinition : SimpleQueryOperator.SimpleQueryOperatorDefinition
		{
			// Token: 0x0600055A RID: 1370 RVA: 0x00019304 File Offset: 0x00017504
			public IndexNotOperatorDefinition(CultureInfo culture, IList<Column> columnsToFetch, SimpleQueryOperator.SimpleQueryOperatorDefinition queryOperatorDefinition, SimpleQueryOperator.SimpleQueryOperatorDefinition notOperatorDefinition, bool frequentOperation) : base(culture, null, columnsToFetch, null, null, 0, 0, frequentOperation)
			{
				this.queryOperatorDefinition = queryOperatorDefinition;
				this.notOperatorDefinition = notOperatorDefinition;
			}

			// Token: 0x17000172 RID: 370
			// (get) Token: 0x0600055B RID: 1371 RVA: 0x0001932F File Offset: 0x0001752F
			internal override string OperatorName
			{
				get
				{
					return "IndexNOT";
				}
			}

			// Token: 0x17000173 RID: 371
			// (get) Token: 0x0600055C RID: 1372 RVA: 0x00019336 File Offset: 0x00017536
			public override SortOrder SortOrder
			{
				get
				{
					return SortOrder.Empty;
				}
			}

			// Token: 0x17000174 RID: 372
			// (get) Token: 0x0600055D RID: 1373 RVA: 0x0001933D File Offset: 0x0001753D
			public override bool Backwards
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x0600055E RID: 1374 RVA: 0x00019340 File Offset: 0x00017540
			public SimpleQueryOperator.SimpleQueryOperatorDefinition QueryOperatorDefinition
			{
				get
				{
					return this.queryOperatorDefinition;
				}
			}

			// Token: 0x17000176 RID: 374
			// (get) Token: 0x0600055F RID: 1375 RVA: 0x00019348 File Offset: 0x00017548
			public SimpleQueryOperator.SimpleQueryOperatorDefinition NotOperatorDefinition
			{
				get
				{
					return this.notOperatorDefinition;
				}
			}

			// Token: 0x06000560 RID: 1376 RVA: 0x00019350 File Offset: 0x00017550
			public override SimpleQueryOperator CreateOperator(IConnectionProvider connectionProvider)
			{
				return Factory.CreateIndexNotOperator(connectionProvider, this);
			}

			// Token: 0x06000561 RID: 1377 RVA: 0x00019359 File Offset: 0x00017559
			public override void EnumerateDescendants(Action<DataAccessOperator.DataAccessOperatorDefinition> operatorDefinitionAction)
			{
				operatorDefinitionAction(this);
				if (this.QueryOperatorDefinition != null)
				{
					this.QueryOperatorDefinition.EnumerateDescendants(operatorDefinitionAction);
				}
				if (this.NotOperatorDefinition != null)
				{
					this.NotOperatorDefinition.EnumerateDescendants(operatorDefinitionAction);
				}
			}

			// Token: 0x06000562 RID: 1378 RVA: 0x0001938C File Offset: 0x0001758C
			internal override void AppendToStringBuilder(StringBuilder sb, StringFormatOptions formatOptions, int nestingLevel)
			{
				sb.Append("select indexNot:[");
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
				base.Indent(sb, multiLine, nestingLevel + 1, false);
				if ((formatOptions & StringFormatOptions.IncludeNestedObjectsId) == StringFormatOptions.IncludeNestedObjectsId)
				{
					sb.Append("op:[");
					sb.Append(this.QueryOperatorDefinition.OperatorName);
					sb.Append(" ");
					sb.Append(this.QueryOperatorDefinition.GetHashCode());
					sb.Append("] ");
				}
				this.QueryOperatorDefinition.AppendToStringBuilder(sb, formatOptions, nestingLevel + 1);
				base.Indent(sb, multiLine, nestingLevel, false);
				sb.Append("  ]");
				base.Indent(sb, multiLine, nestingLevel, false);
				sb.Append("  not:[");
				base.Indent(sb, multiLine, nestingLevel + 1, false);
				if ((formatOptions & StringFormatOptions.IncludeNestedObjectsId) == StringFormatOptions.IncludeNestedObjectsId)
				{
					sb.Append("op:[");
					sb.Append(this.NotOperatorDefinition.OperatorName);
					sb.Append(" ");
					sb.Append(this.NotOperatorDefinition.GetHashCode());
					sb.Append("] ");
				}
				this.NotOperatorDefinition.AppendToStringBuilder(sb, formatOptions, nestingLevel + 1);
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

			// Token: 0x06000563 RID: 1379 RVA: 0x00019570 File Offset: 0x00017770
			[Conditional("DEBUG")]
			private void AssertColumnIntegrity()
			{
				for (int i = 0; i < base.ColumnsToFetch.Count; i++)
				{
				}
			}

			// Token: 0x06000564 RID: 1380 RVA: 0x00019594 File Offset: 0x00017794
			internal override void CalculateHashValueForStatisticPurposes(out int simple, out int detail)
			{
				int num;
				int num2;
				this.queryOperatorDefinition.CalculateHashValueForStatisticPurposes(out num, out num2);
				int num3;
				int num4;
				this.notOperatorDefinition.CalculateHashValueForStatisticPurposes(out num3, out num4);
				detail = (59320 ^ num2 ^ num4);
				simple = (59320 ^ num ^ num3);
			}

			// Token: 0x06000565 RID: 1381 RVA: 0x000195D8 File Offset: 0x000177D8
			internal override bool IsEqualsForStatisticPurposes(DataAccessOperator.DataAccessOperatorDefinition other)
			{
				if (object.ReferenceEquals(this, other))
				{
					return true;
				}
				IndexNotOperator.IndexNotOperatorDefinition indexNotOperatorDefinition = other as IndexNotOperator.IndexNotOperatorDefinition;
				return indexNotOperatorDefinition != null && this.queryOperatorDefinition.IsEqualsForStatisticPurposes(indexNotOperatorDefinition.queryOperatorDefinition) && this.notOperatorDefinition.IsEqualsForStatisticPurposes(indexNotOperatorDefinition.notOperatorDefinition);
			}

			// Token: 0x0400019B RID: 411
			private SimpleQueryOperator.SimpleQueryOperatorDefinition queryOperatorDefinition;

			// Token: 0x0400019C RID: 412
			private SimpleQueryOperator.SimpleQueryOperatorDefinition notOperatorDefinition;
		}
	}
}
