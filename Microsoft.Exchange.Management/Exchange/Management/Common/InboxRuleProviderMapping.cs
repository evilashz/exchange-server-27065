using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x02000112 RID: 274
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct InboxRuleProviderMapping
	{
		// Token: 0x0600098D RID: 2445 RVA: 0x0002A226 File Offset: 0x00028426
		internal InboxRuleProviderMapping(RuleProviderId id, string name)
		{
			this.RuleProviderId = id;
			this.RuleProviderString = name;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0002A238 File Offset: 0x00028438
		internal static string GetRuleProviderString(RuleProviderId id)
		{
			for (int i = 0; i < InboxRuleProviderMapping.table.Length; i++)
			{
				if (InboxRuleProviderMapping.table[i].RuleProviderId == id)
				{
					return InboxRuleProviderMapping.table[i].RuleProviderString;
				}
			}
			throw new ArgumentException("RuleProviderId '" + id.ToString() + "' is not supported.");
		}

		// Token: 0x040004FE RID: 1278
		internal readonly RuleProviderId RuleProviderId;

		// Token: 0x040004FF RID: 1279
		internal readonly string RuleProviderString;

		// Token: 0x04000500 RID: 1280
		private static InboxRuleProviderMapping[] table = new InboxRuleProviderMapping[]
		{
			new InboxRuleProviderMapping(RuleProviderId.Unknown, null),
			new InboxRuleProviderMapping(RuleProviderId.OL98Plus, "RuleOrganizer"),
			new InboxRuleProviderMapping(RuleProviderId.Exchange14, "ExchangeMailboxRules14")
		};
	}
}
