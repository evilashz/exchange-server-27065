using System;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Data.ClientAccessRules
{
	// Token: 0x0200010F RID: 271
	internal class ClientAccessRulesSourcePortNumberProperty : Property
	{
		// Token: 0x06000973 RID: 2419 RVA: 0x0001DE67 File Offset: 0x0001C067
		public ClientAccessRulesSourcePortNumberProperty(string propertyName, Type type) : base(propertyName, type)
		{
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0001DE74 File Offset: 0x0001C074
		protected override object OnGetValue(RulesEvaluationContext baseContext)
		{
			ClientAccessRulesEvaluationContext clientAccessRulesEvaluationContext = (ClientAccessRulesEvaluationContext)baseContext;
			return clientAccessRulesEvaluationContext.RemoteEndpoint.Port;
		}

		// Token: 0x040005F6 RID: 1526
		public const string PropertyName = "SourceTcpPortNumberProperty";
	}
}
