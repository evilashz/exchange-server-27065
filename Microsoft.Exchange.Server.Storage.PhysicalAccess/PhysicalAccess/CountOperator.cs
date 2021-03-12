using System;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000056 RID: 86
	public abstract class CountOperator : DataAccessOperator
	{
		// Token: 0x060003C0 RID: 960 RVA: 0x00012EBB File Offset: 0x000110BB
		protected CountOperator(CultureInfo culture, IConnectionProvider connectionProvider, SimpleQueryOperator queryOperator, bool frequentOperation) : this(connectionProvider, new CountOperator.CountOperatorDefinition(culture, (queryOperator != null) ? queryOperator.OperatorDefinition : null, frequentOperation))
		{
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00012ED8 File Offset: 0x000110D8
		protected CountOperator(IConnectionProvider connectionProvider, CountOperator.CountOperatorDefinition definition) : base(connectionProvider, definition)
		{
			this.queryOperator = ((definition.QueryOperatorDefinition != null) ? definition.QueryOperatorDefinition.CreateOperator(connectionProvider) : null);
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00012EFF File Offset: 0x000110FF
		public CountOperator.CountOperatorDefinition OperatorDefinition
		{
			get
			{
				return (CountOperator.CountOperatorDefinition)base.OperatorDefinitionBase;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x00012F0C File Offset: 0x0001110C
		internal SimpleQueryOperator QueryOperator
		{
			get
			{
				return this.queryOperator;
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00012F14 File Offset: 0x00011114
		public override void EnumerateDescendants(Action<DataAccessOperator> operatorAction)
		{
			operatorAction(this);
			if (this.queryOperator != null)
			{
				this.queryOperator.EnumerateDescendants(operatorAction);
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00012F31 File Offset: 0x00011131
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.queryOperator != null)
			{
				this.queryOperator.Dispose();
			}
		}

		// Token: 0x04000120 RID: 288
		private readonly SimpleQueryOperator queryOperator;

		// Token: 0x02000057 RID: 87
		public class CountOperatorDefinition : DataAccessOperator.DataAccessOperatorDefinition
		{
			// Token: 0x060003C6 RID: 966 RVA: 0x00012F49 File Offset: 0x00011149
			public CountOperatorDefinition(CultureInfo culture, SimpleQueryOperator.SimpleQueryOperatorDefinition queryOperatorDefinition, bool frequentOperation) : base(culture, frequentOperation)
			{
				this.queryOperatorDefinition = queryOperatorDefinition;
			}

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x060003C7 RID: 967 RVA: 0x00012F5A File Offset: 0x0001115A
			internal override string OperatorName
			{
				get
				{
					return "COUNT";
				}
			}

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x060003C8 RID: 968 RVA: 0x00012F61 File Offset: 0x00011161
			public SimpleQueryOperator.SimpleQueryOperatorDefinition QueryOperatorDefinition
			{
				get
				{
					return this.queryOperatorDefinition;
				}
			}

			// Token: 0x060003C9 RID: 969 RVA: 0x00012F69 File Offset: 0x00011169
			public CountOperator CreateOperator(IConnectionProvider connectionProvider)
			{
				return Factory.CreateCountOperator(connectionProvider, this);
			}

			// Token: 0x060003CA RID: 970 RVA: 0x00012F72 File Offset: 0x00011172
			public override void EnumerateDescendants(Action<DataAccessOperator.DataAccessOperatorDefinition> operatorDefinitionAction)
			{
				operatorDefinitionAction(this);
				if (this.QueryOperatorDefinition != null)
				{
					this.QueryOperatorDefinition.EnumerateDescendants(operatorDefinitionAction);
				}
			}

			// Token: 0x060003CB RID: 971 RVA: 0x00012F90 File Offset: 0x00011190
			internal override void AppendToStringBuilder(StringBuilder sb, StringFormatOptions formatOptions, int nestingLevel)
			{
				sb.Append("select count");
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
						sb.Append("]");
					}
					this.QueryOperatorDefinition.AppendToStringBuilder(sb, formatOptions, nestingLevel + 1);
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

			// Token: 0x060003CC RID: 972 RVA: 0x00013088 File Offset: 0x00011288
			internal override void CalculateHashValueForStatisticPurposes(out int simple, out int detail)
			{
				int num = 0;
				int num2 = 0;
				if (this.queryOperatorDefinition != null)
				{
					this.queryOperatorDefinition.CalculateHashValueForStatisticPurposes(out num, out num2);
				}
				detail = (38840 ^ num2);
				simple = (38840 ^ num);
			}

			// Token: 0x060003CD RID: 973 RVA: 0x000130C4 File Offset: 0x000112C4
			internal override bool IsEqualsForStatisticPurposes(DataAccessOperator.DataAccessOperatorDefinition other)
			{
				if (object.ReferenceEquals(this, other))
				{
					return true;
				}
				CountOperator.CountOperatorDefinition countOperatorDefinition = other as CountOperator.CountOperatorDefinition;
				return countOperatorDefinition != null && ((this.queryOperatorDefinition == null && countOperatorDefinition.queryOperatorDefinition == null) || (this.queryOperatorDefinition != null && countOperatorDefinition.queryOperatorDefinition != null && this.queryOperatorDefinition.IsEqualsForStatisticPurposes(countOperatorDefinition.queryOperatorDefinition)));
			}

			// Token: 0x04000121 RID: 289
			private readonly SimpleQueryOperator.SimpleQueryOperatorDefinition queryOperatorDefinition;
		}
	}
}
