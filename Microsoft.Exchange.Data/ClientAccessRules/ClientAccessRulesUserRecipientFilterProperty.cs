using System;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Data.ClientAccessRules
{
	// Token: 0x02000111 RID: 273
	internal class ClientAccessRulesUserRecipientFilterProperty : Property
	{
		// Token: 0x06000979 RID: 2425 RVA: 0x0001DF28 File Offset: 0x0001C128
		public ClientAccessRulesUserRecipientFilterProperty(string propertyName, Type type) : base(propertyName, type)
		{
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0001DF34 File Offset: 0x0001C134
		protected override object OnGetValue(RulesEvaluationContext baseContext)
		{
			ClientAccessRulesEvaluationContext clientAccessRulesEvaluationContext = (ClientAccessRulesEvaluationContext)baseContext;
			return clientAccessRulesEvaluationContext.User;
		}

		// Token: 0x040005F8 RID: 1528
		public const string PropertyName = "UserRecipientFilterProperty";
	}
}
