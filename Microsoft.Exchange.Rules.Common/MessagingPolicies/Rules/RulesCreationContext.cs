using System;
using Microsoft.Exchange.TextProcessing;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000031 RID: 49
	public class RulesCreationContext
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00005E6C File Offset: 0x0000406C
		internal MatchFactory MatchFactory
		{
			get
			{
				return this.matchFactory.Value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00005E79 File Offset: 0x00004079
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00005E81 File Offset: 0x00004081
		public int ConditionAndActionSize { get; set; }

		// Token: 0x0400009B RID: 155
		private Lazy<MatchFactory> matchFactory = new Lazy<MatchFactory>();
	}
}
