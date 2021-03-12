using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200002F RID: 47
	internal class BodyProperty : MessageProperty
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x00008C44 File Offset: 0x00006E44
		public BodyProperty() : base("Message.Body", typeof(IContent))
		{
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00008C5C File Offset: 0x00006E5C
		protected override object OnGetValue(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			TransportRulesEvaluator.Trace(transportRulesEvaluationContext.TransportRulesTracer, transportRulesEvaluationContext.MailItem, "Body property value is evaluated as rule condition");
			return transportRulesEvaluationContext.Message.Body;
		}

		// Token: 0x04000155 RID: 341
		public const string PropertyName = "Message.Body";
	}
}
