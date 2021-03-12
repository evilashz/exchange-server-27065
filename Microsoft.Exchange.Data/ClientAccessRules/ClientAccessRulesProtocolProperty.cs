using System;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Data.ClientAccessRules
{
	// Token: 0x0200010E RID: 270
	internal class ClientAccessRulesProtocolProperty : Property
	{
		// Token: 0x06000971 RID: 2417 RVA: 0x0001DE3B File Offset: 0x0001C03B
		public ClientAccessRulesProtocolProperty(string propertyName, Type type) : base(propertyName, type)
		{
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0001DE48 File Offset: 0x0001C048
		protected override object OnGetValue(RulesEvaluationContext baseContext)
		{
			ClientAccessRulesEvaluationContext clientAccessRulesEvaluationContext = (ClientAccessRulesEvaluationContext)baseContext;
			return clientAccessRulesEvaluationContext.Protocol;
		}

		// Token: 0x040005F5 RID: 1525
		public const string PropertyName = "ProtocolProperty";
	}
}
