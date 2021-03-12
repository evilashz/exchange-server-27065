using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000102 RID: 258
	internal class RulesDataContext : DataContext
	{
		// Token: 0x06000942 RID: 2370 RVA: 0x0001292C File Offset: 0x00010B2C
		public RulesDataContext(RuleData[] rules)
		{
			this.rules = rules;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0001293B File Offset: 0x00010B3B
		public override string ToString()
		{
			return string.Format("Rules: {0}", CommonUtils.ConcatEntries<RuleData>(this.rules, null));
		}

		// Token: 0x04000569 RID: 1385
		private RuleData[] rules;
	}
}
