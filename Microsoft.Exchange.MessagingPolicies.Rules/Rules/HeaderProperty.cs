using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200002D RID: 45
	internal class HeaderProperty : Property
	{
		// Token: 0x06000195 RID: 405 RVA: 0x00008282 File Offset: 0x00006482
		public HeaderProperty(string headerName) : base(headerName, typeof(List<string>))
		{
			if (!TransportUtils.IsHeaderValid(headerName))
			{
				throw new RulesValidationException(TransportRulesStrings.InvalidHeaderName(headerName));
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000082AC File Offset: 0x000064AC
		protected override object OnGetValue(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			object obj = transportRulesEvaluationContext.Message.Headers[base.Name];
			TransportRulesEvaluator.Trace(transportRulesEvaluationContext.TransportRulesTracer, transportRulesEvaluationContext.MailItem, string.Format("Header property value evaluated as rule condition: '{0}'", obj ?? "null"));
			if (transportRulesEvaluationContext.IsTestMessage && obj != null)
			{
				TransportRulesEvaluator.Trace(transportRulesEvaluationContext.TransportRulesTracer, transportRulesEvaluationContext.MailItem, string.Format("property is a collection of values: '{0}'", string.Join(",", obj as IEnumerable<string>)));
			}
			return obj;
		}

		// Token: 0x0400013C RID: 316
		public const string Prefix = "Message.Headers";

		// Token: 0x0400013D RID: 317
		public const string TypeName = "List<string>";
	}
}
