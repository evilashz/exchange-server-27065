using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001DE RID: 478
	internal class MaxRuleID : ConfigurablePropertyBag
	{
		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x00040BC8 File Offset: 0x0003EDC8
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(Guid.NewGuid().ToString());
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x00040BED File Offset: 0x0003EDED
		// (set) Token: 0x06001446 RID: 5190 RVA: 0x00040BFF File Offset: 0x0003EDFF
		public long? RuleID
		{
			get
			{
				return (long?)this[MaxRuleID.RuleIDProperty];
			}
			set
			{
				this[MaxRuleID.RuleIDProperty] = value;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x00040C12 File Offset: 0x0003EE12
		// (set) Token: 0x06001448 RID: 5192 RVA: 0x00040C24 File Offset: 0x0003EE24
		public RuleType? RuleType
		{
			get
			{
				return (RuleType?)this[MaxRuleID.RuleIDProperty];
			}
			set
			{
				this[MaxRuleID.RuleIDProperty] = value;
			}
		}

		// Token: 0x040009F6 RID: 2550
		public static readonly HygienePropertyDefinition RuleIDProperty = new HygienePropertyDefinition("bi_RuleId", typeof(long?));

		// Token: 0x040009F7 RID: 2551
		public static readonly HygienePropertyDefinition RuleTypeProperty = new HygienePropertyDefinition("ti_RuleType", typeof(byte?));
	}
}
