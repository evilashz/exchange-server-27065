using System;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Data.ClientAccessRules
{
	// Token: 0x0200010C RID: 268
	internal class ClientAccessRulesAuthenticationTypeProperty : Property
	{
		// Token: 0x0600096D RID: 2413 RVA: 0x0001DDE4 File Offset: 0x0001BFE4
		public ClientAccessRulesAuthenticationTypeProperty(string propertyName, Type type) : base(propertyName, type)
		{
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0001DDF0 File Offset: 0x0001BFF0
		protected override object OnGetValue(RulesEvaluationContext baseContext)
		{
			ClientAccessRulesEvaluationContext clientAccessRulesEvaluationContext = (ClientAccessRulesEvaluationContext)baseContext;
			return clientAccessRulesEvaluationContext.AuthenticationType;
		}

		// Token: 0x040005F3 RID: 1523
		public const string PropertyName = "AuthenticationTypeProperty";
	}
}
