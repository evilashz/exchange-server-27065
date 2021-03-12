using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Data.Transport.Storage;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x02000018 RID: 24
	internal class InterceptorBootScannerAgent : StorageAgent
	{
		// Token: 0x06000112 RID: 274 RVA: 0x000064F9 File Offset: 0x000046F9
		public InterceptorBootScannerAgent(FilteredRuleCache filteredRuleCache)
		{
			this.filteredRuleCache = filteredRuleCache;
			base.OnLoadedMessage += this.OnLoadedMessageHandler;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000654C File Offset: 0x0000474C
		private void OnLoadedMessageHandler(StorageEventSource source, StorageEventArgs args)
		{
			InterceptorAgentRule interceptorAgentRule = InterceptorAgentRuleEvaluator.Evaluate(this.filteredRuleCache.Rules, args);
			if (interceptorAgentRule == null)
			{
				return;
			}
			string context = interceptorAgentRule.GetSourceContext(base.Name, InterceptorAgentEvent.OnLoadedMessage, false);
			ExTraceGlobals.InterceptorAgentTracer.TraceDebug(0, (long)this.GetHashCode(), context);
			interceptorAgentRule.PerformAction(args.MailItem, delegate
			{
				source.Delete(context);
			}, delegate(SmtpResponse response)
			{
				source.DeleteWithNdr(response, context);
			}, null);
		}

		// Token: 0x040000AA RID: 170
		private readonly FilteredRuleCache filteredRuleCache;
	}
}
