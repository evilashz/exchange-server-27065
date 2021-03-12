using System;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Data.ClientAccessRules
{
	// Token: 0x0200010D RID: 269
	internal class ClientAccessRulesClientIpProperty : Property
	{
		// Token: 0x0600096F RID: 2415 RVA: 0x0001DE0F File Offset: 0x0001C00F
		public ClientAccessRulesClientIpProperty(string propertyName, Type type) : base(propertyName, type)
		{
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0001DE1C File Offset: 0x0001C01C
		protected override object OnGetValue(RulesEvaluationContext baseContext)
		{
			ClientAccessRulesEvaluationContext clientAccessRulesEvaluationContext = (ClientAccessRulesEvaluationContext)baseContext;
			return clientAccessRulesEvaluationContext.RemoteEndpoint.Address;
		}

		// Token: 0x040005F4 RID: 1524
		public const string PropertyName = "ClientIpProperty";
	}
}
