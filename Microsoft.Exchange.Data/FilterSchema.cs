using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200004B RID: 75
	internal abstract class FilterSchema
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000215 RID: 533
		public abstract string And { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000216 RID: 534
		public abstract string Or { get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000217 RID: 535
		public abstract string Not { get; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000218 RID: 536
		public abstract string Like { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000219 RID: 537
		public abstract string QuotationMark { get; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600021A RID: 538
		public abstract bool SupportQuotedPrefix { get; }

		// Token: 0x0600021B RID: 539
		public abstract string GetRelationalOperator(ComparisonOperator op);

		// Token: 0x0600021C RID: 540
		public abstract string EscapeStringValue(object o);

		// Token: 0x0600021D RID: 541
		public abstract string GetExistsFilter(ExistsFilter filter);

		// Token: 0x0600021E RID: 542
		public abstract string GetFalseFilter();

		// Token: 0x0600021F RID: 543
		public abstract string GetPropertyName(string propertyName);
	}
}
