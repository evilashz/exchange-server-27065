using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200005E RID: 94
	public abstract class OrdinalPositionOperator : DataAccessOperator
	{
		// Token: 0x060003FE RID: 1022 RVA: 0x00013B8C File Offset: 0x00011D8C
		protected OrdinalPositionOperator(CultureInfo culture, IConnectionProvider connectionProvider, SimpleQueryOperator queryOperator, SortOrder keySortOrder, StartStopKey key, bool frequentOperation) : this(connectionProvider, new OrdinalPositionOperator.OrdinalPositionOperatorDefinition(culture, (queryOperator != null) ? queryOperator.OperatorDefinition : null, keySortOrder, key, frequentOperation))
		{
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00013BAD File Offset: 0x00011DAD
		protected OrdinalPositionOperator(IConnectionProvider connectionProvider, OrdinalPositionOperator.OrdinalPositionOperatorDefinition definition) : base(connectionProvider, definition)
		{
			this.queryOperator = ((definition.QueryOperatorDefinition != null) ? definition.QueryOperatorDefinition.CreateOperator(connectionProvider) : null);
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00013BD4 File Offset: 0x00011DD4
		protected SimpleQueryOperator QueryOperator
		{
			get
			{
				return this.queryOperator;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x00013BDC File Offset: 0x00011DDC
		protected SortOrder KeySortOrder
		{
			get
			{
				return this.OperatorDefinition.KeySortOrder;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00013BE9 File Offset: 0x00011DE9
		protected StartStopKey Key
		{
			get
			{
				return this.OperatorDefinition.Key;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x00013BF6 File Offset: 0x00011DF6
		public OrdinalPositionOperator.OrdinalPositionOperatorDefinition OperatorDefinition
		{
			get
			{
				return (OrdinalPositionOperator.OrdinalPositionOperatorDefinition)base.OperatorDefinitionBase;
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00013C03 File Offset: 0x00011E03
		public override void EnumerateDescendants(Action<DataAccessOperator> operatorAction)
		{
			operatorAction(this);
			if (this.queryOperator != null)
			{
				this.queryOperator.EnumerateDescendants(operatorAction);
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00013C20 File Offset: 0x00011E20
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.queryOperator != null)
			{
				this.queryOperator.Dispose();
			}
		}

		// Token: 0x0400012C RID: 300
		private readonly SimpleQueryOperator queryOperator;

		// Token: 0x0200005F RID: 95
		public class OrdinalPositionOperatorDefinition : DataAccessOperator.DataAccessOperatorDefinition
		{
			// Token: 0x06000406 RID: 1030 RVA: 0x00013C38 File Offset: 0x00011E38
			public OrdinalPositionOperatorDefinition(CultureInfo culture, SimpleQueryOperator.SimpleQueryOperatorDefinition queryOperatorDefinition, SortOrder keySortOrder, StartStopKey key, bool frequentOperation) : base(culture, frequentOperation)
			{
				this.queryOperatorDefinition = queryOperatorDefinition;
				this.keySortOrder = keySortOrder;
				this.key = key;
			}

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x06000407 RID: 1031 RVA: 0x00013C59 File Offset: 0x00011E59
			internal override string OperatorName
			{
				get
				{
					return "ORDINAL";
				}
			}

			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x06000408 RID: 1032 RVA: 0x00013C60 File Offset: 0x00011E60
			public SortOrder KeySortOrder
			{
				get
				{
					return this.keySortOrder;
				}
			}

			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x06000409 RID: 1033 RVA: 0x00013C68 File Offset: 0x00011E68
			public StartStopKey Key
			{
				get
				{
					return this.key;
				}
			}

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x0600040A RID: 1034 RVA: 0x00013C70 File Offset: 0x00011E70
			public SimpleQueryOperator.SimpleQueryOperatorDefinition QueryOperatorDefinition
			{
				get
				{
					return this.queryOperatorDefinition;
				}
			}

			// Token: 0x0600040B RID: 1035 RVA: 0x00013C78 File Offset: 0x00011E78
			public OrdinalPositionOperator CreateOperator(IConnectionProvider connectionProvider)
			{
				return Factory.CreateOrdinalPositionOperator(connectionProvider, this);
			}

			// Token: 0x0600040C RID: 1036 RVA: 0x00013C81 File Offset: 0x00011E81
			public override void EnumerateDescendants(Action<DataAccessOperator.DataAccessOperatorDefinition> operatorDefinitionAction)
			{
				operatorDefinitionAction(this);
				if (this.QueryOperatorDefinition != null)
				{
					this.QueryOperatorDefinition.EnumerateDescendants(operatorDefinitionAction);
				}
			}

			// Token: 0x0600040D RID: 1037 RVA: 0x00013CA0 File Offset: 0x00011EA0
			internal override void AppendToStringBuilder(StringBuilder sb, StringFormatOptions formatOptions, int nestingLevel)
			{
				sb.Append("select ordinal position");
				bool multiLine = (formatOptions & StringFormatOptions.MultiLine) == StringFormatOptions.MultiLine;
				if (this.QueryOperatorDefinition != null)
				{
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
				}
				if (!this.Key.IsEmpty)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  key:");
					this.Key.AppendToStringBuilder(sb, formatOptions);
				}
				if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  frequentOp:[");
					sb.Append(base.FrequentOperation);
					sb.Append("]");
				}
			}

			// Token: 0x0600040E RID: 1038 RVA: 0x00013DCC File Offset: 0x00011FCC
			[Conditional("DEBUG")]
			private void ValidateKey()
			{
				for (int i = 0; i < this.key.Count; i++)
				{
				}
				if (this.QueryOperatorDefinition != null)
				{
					for (int j = 0; j < this.key.Count; j++)
					{
					}
				}
			}

			// Token: 0x0600040F RID: 1039 RVA: 0x00013E14 File Offset: 0x00012014
			internal override void CalculateHashValueForStatisticPurposes(out int simple, out int detail)
			{
				int num = 0;
				int num2 = 0;
				if (this.queryOperatorDefinition != null)
				{
					this.queryOperatorDefinition.CalculateHashValueForStatisticPurposes(out num, out num2);
				}
				detail = (46952 ^ num2);
				simple = (46952 ^ num);
			}

			// Token: 0x06000410 RID: 1040 RVA: 0x00013E50 File Offset: 0x00012050
			internal override bool IsEqualsForStatisticPurposes(DataAccessOperator.DataAccessOperatorDefinition other)
			{
				OrdinalPositionOperator.OrdinalPositionOperatorDefinition ordinalPositionOperatorDefinition = other as OrdinalPositionOperator.OrdinalPositionOperatorDefinition;
				return ordinalPositionOperatorDefinition != null && ((this.queryOperatorDefinition == null && ordinalPositionOperatorDefinition.queryOperatorDefinition == null) || (this.queryOperatorDefinition != null && ordinalPositionOperatorDefinition.queryOperatorDefinition != null && this.queryOperatorDefinition.IsEqualsForStatisticPurposes(ordinalPositionOperatorDefinition.queryOperatorDefinition)));
			}

			// Token: 0x0400012D RID: 301
			private readonly SimpleQueryOperator.SimpleQueryOperatorDefinition queryOperatorDefinition;

			// Token: 0x0400012E RID: 302
			private readonly SortOrder keySortOrder;

			// Token: 0x0400012F RID: 303
			private readonly StartStopKey key;
		}
	}
}
