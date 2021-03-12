using System;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Data.ClientAccessRules
{
	// Token: 0x02000106 RID: 262
	internal abstract class ClientAccessRuleAction : Microsoft.Exchange.MessagingPolicies.Rules.Action
	{
		// Token: 0x06000953 RID: 2387 RVA: 0x0001D9AD File Offset: 0x0001BBAD
		public ClientAccessRuleAction(ShortList<Argument> arguments) : base(arguments)
		{
		}
	}
}
