using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BA0 RID: 2976
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class WithinSizeRangeCondition : Condition
	{
		// Token: 0x06006AAB RID: 27307 RVA: 0x001C763F File Offset: 0x001C583F
		private WithinSizeRangeCondition(Rule rule, int? rangeLow, int? rangeHigh) : base(ConditionType.WithinSizeRangeCondition, rule)
		{
			this.rangeLow = rangeLow;
			this.rangeHigh = rangeHigh;
		}

		// Token: 0x06006AAC RID: 27308 RVA: 0x001C7694 File Offset: 0x001C5894
		public static WithinSizeRangeCondition Create(Rule rule, int? rangeLow, int? rangeHigh)
		{
			Condition.CheckParams(new object[]
			{
				rule
			});
			if (rangeLow == null && rangeHigh == null)
			{
				rule.ThrowValidateException(delegate
				{
					throw new ArgumentException("Both cannot be null");
				}, "rangeLow, rangeHigh");
			}
			if ((rangeLow != null && rangeLow < 0) || (rangeHigh != null && rangeHigh < 0))
			{
				rule.ThrowValidateException(delegate
				{
					throw new ArgumentOutOfRangeException("Cannot be negative");
				}, "rangeLow, rangeHigh");
			}
			if (rangeLow != null && rangeHigh != null && rangeLow > rangeHigh)
			{
				rule.ThrowValidateException(delegate
				{
					throw new ArgumentOutOfRangeException("rangeLow cannot be > rangeHigh");
				}, "rangeLow, rangeHigh");
			}
			return new WithinSizeRangeCondition(rule, rangeLow, rangeHigh);
		}

		// Token: 0x17001D13 RID: 7443
		// (get) Token: 0x06006AAD RID: 27309 RVA: 0x001C77C1 File Offset: 0x001C59C1
		public int? RangeLow
		{
			get
			{
				return this.rangeLow;
			}
		}

		// Token: 0x17001D14 RID: 7444
		// (get) Token: 0x06006AAE RID: 27310 RVA: 0x001C77C9 File Offset: 0x001C59C9
		public int? RangeHigh
		{
			get
			{
				return this.rangeHigh;
			}
		}

		// Token: 0x06006AAF RID: 27311 RVA: 0x001C77D1 File Offset: 0x001C59D1
		internal override Restriction BuildRestriction()
		{
			return Condition.CreateSizeRestriction(this.rangeLow, this.rangeHigh);
		}

		// Token: 0x04003CE6 RID: 15590
		private readonly int? rangeLow = null;

		// Token: 0x04003CE7 RID: 15591
		private readonly int? rangeHigh = null;
	}
}
