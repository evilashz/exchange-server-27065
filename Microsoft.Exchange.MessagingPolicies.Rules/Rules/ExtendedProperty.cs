using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200002C RID: 44
	internal sealed class ExtendedProperty : Property
	{
		// Token: 0x06000193 RID: 403 RVA: 0x000081FF File Offset: 0x000063FF
		public ExtendedProperty(string propertyName, Type type) : base(propertyName, type)
		{
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000820C File Offset: 0x0000640C
		protected override object OnGetValue(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			object obj = null;
			if (transportRulesEvaluationContext.Message.ExtendedProperties.TryGetValue(base.Name, out obj))
			{
				TransportRulesEvaluator.Trace(transportRulesEvaluationContext.TransportRulesTracer, transportRulesEvaluationContext.MailItem, string.Format("{0} extended property value evaluated as rule condition: '{1}'", base.Name, obj));
				return obj;
			}
			TransportRulesEvaluator.Trace(transportRulesEvaluationContext.TransportRulesTracer, transportRulesEvaluationContext.MailItem, string.Format("{0} extended property value evaluated 'null'", base.Name));
			return null;
		}

		// Token: 0x0400013B RID: 315
		public const string Prefix = "Message.ExtendedProperties";
	}
}
