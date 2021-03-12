using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B98 RID: 2968
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class GuidCondition : Condition
	{
		// Token: 0x06006A91 RID: 27281 RVA: 0x001C734C File Offset: 0x001C554C
		protected GuidCondition(ConditionType conditionType, Rule rule, Guid[] guids) : base(conditionType, rule)
		{
			bool flag = false;
			if (guids != null && guids.Length > 0)
			{
				for (int i = 0; i < guids.Length; i++)
				{
					if (guids[i].Equals(Guid.Empty))
					{
						flag = true;
						break;
					}
				}
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				rule.ThrowValidateException(delegate
				{
					throw new ArgumentException("guids");
				}, "guids");
			}
			this.guids = guids;
		}

		// Token: 0x17001D10 RID: 7440
		// (get) Token: 0x06006A92 RID: 27282 RVA: 0x001C73C7 File Offset: 0x001C55C7
		public Guid[] Guids
		{
			get
			{
				return this.guids;
			}
		}

		// Token: 0x04003CE2 RID: 15586
		private readonly Guid[] guids;
	}
}
