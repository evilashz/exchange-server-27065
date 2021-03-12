using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200004C RID: 76
	internal class MonadFilterSchema : FilterSchema
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000851C File Offset: 0x0000671C
		public override string And
		{
			get
			{
				return "-and";
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00008523 File Offset: 0x00006723
		public override string Or
		{
			get
			{
				return "-or";
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000852A File Offset: 0x0000672A
		public override string Not
		{
			get
			{
				return "-not";
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00008531 File Offset: 0x00006731
		public override string Like
		{
			get
			{
				return " -like ";
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00008538 File Offset: 0x00006738
		public override string QuotationMark
		{
			get
			{
				return "'";
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000853F File Offset: 0x0000673F
		public override bool SupportQuotedPrefix
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00008544 File Offset: 0x00006744
		public override string GetRelationalOperator(ComparisonOperator op)
		{
			switch (op)
			{
			case ComparisonOperator.Equal:
				return " -eq ";
			case ComparisonOperator.NotEqual:
				return " -ne ";
			case ComparisonOperator.LessThan:
				return " -lt ";
			case ComparisonOperator.LessThanOrEqual:
				return " -le ";
			case ComparisonOperator.GreaterThan:
				return " -gt ";
			case ComparisonOperator.GreaterThanOrEqual:
				return " -ge ";
			case ComparisonOperator.Like:
				return " -like ";
			default:
				return null;
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000085A4 File Offset: 0x000067A4
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

		// Token: 0x06000229 RID: 553 RVA: 0x000085EB File Offset: 0x000067EB
		public override string GetExistsFilter(ExistsFilter filter)
		{
			return string.Format("{0} -ne $null", filter.Property.Name);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00008602 File Offset: 0x00006802
		public override string GetFalseFilter()
		{
			return "$False";
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00008609 File Offset: 0x00006809
		public override string GetPropertyName(string propertyName)
		{
			return propertyName;
		}
	}
}
