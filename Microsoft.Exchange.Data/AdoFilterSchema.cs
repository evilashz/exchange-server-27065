using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200004D RID: 77
	internal class AdoFilterSchema : FilterSchema
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00008614 File Offset: 0x00006814
		public override string And
		{
			get
			{
				return "AND";
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000861B File Offset: 0x0000681B
		public override string Or
		{
			get
			{
				return "OR";
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00008622 File Offset: 0x00006822
		public override string Not
		{
			get
			{
				return "NOT";
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00008629 File Offset: 0x00006829
		public override string Like
		{
			get
			{
				return " LIKE ";
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00008630 File Offset: 0x00006830
		public override string QuotationMark
		{
			get
			{
				return "'";
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00008637 File Offset: 0x00006837
		public override bool SupportQuotedPrefix
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000863C File Offset: 0x0000683C
		public override string GetRelationalOperator(ComparisonOperator op)
		{
			switch (op)
			{
			case ComparisonOperator.Equal:
				return " = ";
			case ComparisonOperator.NotEqual:
				return " <> ";
			case ComparisonOperator.LessThan:
				return " < ";
			case ComparisonOperator.LessThanOrEqual:
				return " <= ";
			case ComparisonOperator.GreaterThan:
				return " > ";
			case ComparisonOperator.GreaterThanOrEqual:
				return " >= ";
			case ComparisonOperator.Like:
				return " LIKE ";
			default:
				return null;
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000869C File Offset: 0x0000689C
		public override string EscapeStringValue(object o)
		{
			if (o == null)
			{
				return null;
			}
			IDnFormattable dnFormattable = o as IDnFormattable;
			if (dnFormattable != null)
			{
				return dnFormattable.ToDNString().Replace("'", "''");
			}
			return o.ToString().Replace("'", "''");
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000086E3 File Offset: 0x000068E3
		public override string GetExistsFilter(ExistsFilter filter)
		{
			return string.Format("(NOT({0} IS NULL))", filter.Property.Name);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000086FA File Offset: 0x000068FA
		public override string GetFalseFilter()
		{
			return "FALSE";
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00008701 File Offset: 0x00006901
		public override string GetPropertyName(string propertyName)
		{
			return propertyName;
		}
	}
}
