using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BA1 RID: 2977
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class WithinDateRangeCondition : Condition
	{
		// Token: 0x06006AB3 RID: 27315 RVA: 0x001C77E4 File Offset: 0x001C59E4
		private WithinDateRangeCondition(Rule rule, ExDateTime? rangeLow, ExDateTime? rangeHigh) : base(ConditionType.WithinDateRangeCondition, rule)
		{
			this.rangeLow = rangeLow;
			this.rangeHigh = rangeHigh;
		}

		// Token: 0x06006AB4 RID: 27316 RVA: 0x001C7830 File Offset: 0x001C5A30
		public static WithinDateRangeCondition Create(Rule rule, ExDateTime? rangeLow, ExDateTime? rangeHigh)
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
			if (rangeLow != null && rangeHigh != null && rangeLow > rangeHigh)
			{
				rule.ThrowValidateException(delegate
				{
					throw new ArgumentOutOfRangeException("rangeLow cannot be > rangeHigh");
				}, "rangeLow, rangeHigh");
			}
			return new WithinDateRangeCondition(rule, rangeLow, rangeHigh);
		}

		// Token: 0x17001D15 RID: 7445
		// (get) Token: 0x06006AB5 RID: 27317 RVA: 0x001C78F7 File Offset: 0x001C5AF7
		public ExDateTime? RangeLow
		{
			get
			{
				return this.rangeLow;
			}
		}

		// Token: 0x17001D16 RID: 7446
		// (get) Token: 0x06006AB6 RID: 27318 RVA: 0x001C78FF File Offset: 0x001C5AFF
		public ExDateTime? RangeHigh
		{
			get
			{
				return this.rangeHigh;
			}
		}

		// Token: 0x06006AB7 RID: 27319 RVA: 0x001C7908 File Offset: 0x001C5B08
		internal override Restriction BuildRestriction()
		{
			return Condition.CreateOneOrTwoTimesRestrictions((this.rangeHigh != null) ? new ExDateTime?(ExTimeZone.UtcTimeZone.ConvertDateTime(this.rangeHigh.Value)) : null, (this.rangeLow != null) ? new ExDateTime?(ExTimeZone.UtcTimeZone.ConvertDateTime(this.rangeLow.Value)) : null);
		}

		// Token: 0x04003CEB RID: 15595
		private readonly ExDateTime? rangeLow = null;

		// Token: 0x04003CEC RID: 15596
		private readonly ExDateTime? rangeHigh = null;
	}
}
