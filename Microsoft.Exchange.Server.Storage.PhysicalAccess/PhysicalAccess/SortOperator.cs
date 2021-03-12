using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000054 RID: 84
	public abstract class SortOperator : SimpleQueryOperator
	{
		// Token: 0x060003AC RID: 940 RVA: 0x000129D0 File Offset: 0x00010BD0
		protected SortOperator(CultureInfo culture, IConnectionProvider connectionProvider, SimpleQueryOperator queryOperator, int skipTo, int maxRows, SortOrder sortOrder, IList<KeyRange> keyRanges, bool backwards, bool frequentOperation) : this(connectionProvider, new SortOperator.SortOperatorDefinition(culture, queryOperator.OperatorDefinition, skipTo, maxRows, sortOrder, keyRanges, backwards, frequentOperation))
		{
		}

		// Token: 0x060003AD RID: 941 RVA: 0x000129FC File Offset: 0x00010BFC
		protected SortOperator(IConnectionProvider connectionProvider, SortOperator.SortOperatorDefinition definition) : base(connectionProvider, definition)
		{
			this.queryOperator = definition.QueryOperatorDefinition.CreateOperator(connectionProvider);
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00012A18 File Offset: 0x00010C18
		protected SimpleQueryOperator QueryOperator
		{
			get
			{
				return this.queryOperator;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003AF RID: 943 RVA: 0x00012A20 File Offset: 0x00010C20
		internal IList<KeyRange> KeyRanges
		{
			get
			{
				return this.OperatorDefinition.KeyRanges;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00012A2D File Offset: 0x00010C2D
		public new SortOperator.SortOperatorDefinition OperatorDefinition
		{
			get
			{
				return (SortOperator.SortOperatorDefinition)base.OperatorDefinitionBase;
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00012A3A File Offset: 0x00010C3A
		public override void EnumerateDescendants(Action<DataAccessOperator> operatorAction)
		{
			operatorAction(this);
			if (this.queryOperator != null)
			{
				this.queryOperator.EnumerateDescendants(operatorAction);
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00012A57 File Offset: 0x00010C57
		public override void RemoveChildren()
		{
			this.queryOperator = null;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00012A60 File Offset: 0x00010C60
		public override IExecutionPlanner GetExecutionPlanner()
		{
			return base.GetExecutionPlanner() ?? DataAccessOperator.GetExecutionPlannerOrNull(this.queryOperator);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00012A77 File Offset: 0x00010C77
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.queryOperator != null)
			{
				this.queryOperator.Dispose();
			}
		}

		// Token: 0x0400011B RID: 283
		private SimpleQueryOperator queryOperator;

		// Token: 0x02000055 RID: 85
		public class SortOperatorDefinition : SimpleQueryOperator.SimpleQueryOperatorDefinition
		{
			// Token: 0x060003B5 RID: 949 RVA: 0x00012A90 File Offset: 0x00010C90
			public SortOperatorDefinition(CultureInfo culture, SimpleQueryOperator.SimpleQueryOperatorDefinition queryOperatorDefinition, int skipTo, int maxRows, SortOrder sortOrder, IList<KeyRange> keyRanges, bool backwards, bool frequentOperation) : base(culture, null, queryOperatorDefinition.ColumnsToFetch, null, null, skipTo, maxRows, frequentOperation)
			{
				this.queryOperatorDefinition = queryOperatorDefinition;
				this.sortOrder = sortOrder;
				this.keyRanges = KeyRange.Normalize(keyRanges, sortOrder, (culture == null) ? null : culture.CompareInfo, backwards);
				this.backwards = backwards;
			}

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x060003B6 RID: 950 RVA: 0x00012AE6 File Offset: 0x00010CE6
			internal override string OperatorName
			{
				get
				{
					return "SORT";
				}
			}

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x060003B7 RID: 951 RVA: 0x00012AED File Offset: 0x00010CED
			public override SortOrder SortOrder
			{
				get
				{
					return this.sortOrder;
				}
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x060003B8 RID: 952 RVA: 0x00012AF5 File Offset: 0x00010CF5
			public IList<KeyRange> KeyRanges
			{
				get
				{
					return this.keyRanges;
				}
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x060003B9 RID: 953 RVA: 0x00012AFD File Offset: 0x00010CFD
			public override bool Backwards
			{
				get
				{
					return this.backwards;
				}
			}

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x060003BA RID: 954 RVA: 0x00012B05 File Offset: 0x00010D05
			public SimpleQueryOperator.SimpleQueryOperatorDefinition QueryOperatorDefinition
			{
				get
				{
					return this.queryOperatorDefinition;
				}
			}

			// Token: 0x060003BB RID: 955 RVA: 0x00012B0D File Offset: 0x00010D0D
			public override SimpleQueryOperator CreateOperator(IConnectionProvider connectionProvider)
			{
				return Factory.CreateSortOperator(connectionProvider, this);
			}

			// Token: 0x060003BC RID: 956 RVA: 0x00012B16 File Offset: 0x00010D16
			public override void EnumerateDescendants(Action<DataAccessOperator.DataAccessOperatorDefinition> operatorDefinitionAction)
			{
				operatorDefinitionAction(this);
				if (this.QueryOperatorDefinition != null)
				{
					this.QueryOperatorDefinition.EnumerateDescendants(operatorDefinitionAction);
				}
			}

			// Token: 0x060003BD RID: 957 RVA: 0x00012B34 File Offset: 0x00010D34
			internal override void AppendToStringBuilder(StringBuilder sb, StringFormatOptions formatOptions, int nestingLevel)
			{
				sb.Append("select sort");
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
				if (!this.SortOrder.IsEmpty)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  order_by:[");
					this.SortOrder.AppendToStringBuilder(sb, formatOptions);
					sb.Append("]");
				}
				if (this.KeyRanges.Count != 1 || !this.KeyRanges[0].IsAllRows)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  KeyRanges:[");
					if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails || this.KeyRanges.Count <= 4)
					{
						for (int i = 0; i < this.KeyRanges.Count; i++)
						{
							if (i != 0)
							{
								sb.Append(" ,");
							}
							this.KeyRanges[i].AppendToStringBuilder(sb, formatOptions);
						}
					}
					else
					{
						if ((formatOptions & StringFormatOptions.SkipParametersData) == StringFormatOptions.None)
						{
							sb.Append(this.KeyRanges.Count);
						}
						else
						{
							sb.Append("multiple");
						}
						sb.Append(" ranges");
					}
					sb.Append("]");
				}
				if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails || this.Backwards)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  backwards:[");
					sb.Append(this.Backwards);
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
				if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  frequentOp:[");
					sb.Append(base.FrequentOperation);
					sb.Append("]");
				}
			}

			// Token: 0x060003BE RID: 958 RVA: 0x00012E08 File Offset: 0x00011008
			internal override void CalculateHashValueForStatisticPurposes(out int simple, out int detail)
			{
				int num;
				int num2;
				this.queryOperatorDefinition.CalculateHashValueForStatisticPurposes(out num, out num2);
				detail = (63336 ^ this.sortOrder.GetHashCode() ^ this.keyRanges.Count ^ num2);
				simple = (63336 ^ num);
			}

			// Token: 0x060003BF RID: 959 RVA: 0x00012E58 File Offset: 0x00011058
			internal override bool IsEqualsForStatisticPurposes(DataAccessOperator.DataAccessOperatorDefinition other)
			{
				if (object.ReferenceEquals(this, other))
				{
					return true;
				}
				SortOperator.SortOperatorDefinition sortOperatorDefinition = other as SortOperator.SortOperatorDefinition;
				return sortOperatorDefinition != null && this.sortOrder.Equals(sortOperatorDefinition.sortOrder) && this.keyRanges.Count == sortOperatorDefinition.keyRanges.Count && this.queryOperatorDefinition.IsEqualsForStatisticPurposes(sortOperatorDefinition.queryOperatorDefinition);
			}

			// Token: 0x0400011C RID: 284
			private readonly SortOrder sortOrder;

			// Token: 0x0400011D RID: 285
			private readonly IList<KeyRange> keyRanges;

			// Token: 0x0400011E RID: 286
			private readonly bool backwards;

			// Token: 0x0400011F RID: 287
			private SimpleQueryOperator.SimpleQueryOperatorDefinition queryOperatorDefinition;
		}
	}
}
