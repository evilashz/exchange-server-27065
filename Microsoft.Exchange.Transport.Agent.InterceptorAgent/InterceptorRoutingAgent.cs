using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x0200001C RID: 28
	internal sealed class InterceptorRoutingAgent : RoutingAgent
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00006E4C File Offset: 0x0000504C
		public InterceptorRoutingAgent(FilteredRuleCache filteredRuleCache)
		{
			this.filteredRuleCache = filteredRuleCache;
			base.OnSubmittedMessage += this.OnSubmittedHandler;
			base.OnResolvedMessage += this.OnResolvedHandler;
			base.OnRoutedMessage += this.OnRoutedHandler;
			base.OnCategorizedMessage += this.OnCategorizedHandler;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00006EAE File Offset: 0x000050AE
		private void OnSubmittedHandler(SubmittedMessageEventSource source, QueuedMessageEventArgs args)
		{
			this.HandleMessage(source, args, InterceptorAgentEvent.OnSubmittedMessage);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00006EBA File Offset: 0x000050BA
		private void OnResolvedHandler(ResolvedMessageEventSource source, QueuedMessageEventArgs args)
		{
			this.HandleMessage(source, args, InterceptorAgentEvent.OnResolvedMessage);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006EC6 File Offset: 0x000050C6
		private void OnRoutedHandler(RoutedMessageEventSource source, QueuedMessageEventArgs args)
		{
			this.HandleMessage(source, args, InterceptorAgentEvent.OnRoutedMessage);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006ED2 File Offset: 0x000050D2
		private void OnCategorizedHandler(CategorizedMessageEventSource source, QueuedMessageEventArgs args)
		{
			this.HandleMessage(source, args, InterceptorAgentEvent.OnCategorizedMessage);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006F6C File Offset: 0x0000516C
		private void HandleMessage(QueuedMessageEventSource source, QueuedMessageEventArgs args, InterceptorAgentEvent evt)
		{
			InterceptorAgentRule rule = InterceptorAgentRuleEvaluator.Evaluate(this.filteredRuleCache.Rules, args, evt);
			if (rule == null)
			{
				return;
			}
			rule.PerformAction(args.MailItem, delegate
			{
				this.DeleteMessage(args.MailItem, source, rule, evt);
			}, delegate(SmtpResponse response)
			{
				this.NdrMessage(args.MailItem, response, source, rule, evt);
			}, delegate(TimeSpan deferTime)
			{
				this.DeferMessage(args.MailItem, deferTime, source, rule, evt);
			});
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00007004 File Offset: 0x00005204
		private void NdrMessage(MailItem mailItem, SmtpResponse response, QueuedMessageEventSource source, InterceptorAgentRule rule, InterceptorAgentEvent evt)
		{
			EnvelopeRecipientCollection recipients = mailItem.Recipients;
			if (recipients == null || recipients.Count == 0)
			{
				ExTraceGlobals.InterceptorAgentTracer.TraceInformation<string>(0, (long)this.GetHashCode(), "Interceptor routing agent: message with subject '{0} has no recipients to NDR", mailItem.Message.Subject);
				return;
			}
			for (int i = recipients.Count - 1; i >= 0; i--)
			{
				ExTraceGlobals.InterceptorAgentTracer.TraceInformation<string, string>(0, (long)this.GetHashCode(), "Interceptor routing agent dropping message with subject '{0} for recipient '{1}'", mailItem.Message.Subject, recipients[i].Address.ToString());
				string sourceContext = rule.GetSourceContext(base.Name, evt, false);
				mailItem.Recipients.Remove(recipients[i], DsnType.Failure, response, sourceContext);
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000070BC File Offset: 0x000052BC
		private void DeferMessage(MailItem mailItem, TimeSpan deferTime, QueuedMessageEventSource source, InterceptorAgentRule rule, InterceptorAgentEvent evt)
		{
			source.Defer(deferTime, rule.GetSourceContext(base.Name, evt, true));
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000070D5 File Offset: 0x000052D5
		private void DeleteMessage(MailItem mailItem, QueuedMessageEventSource source, InterceptorAgentRule rule, InterceptorAgentEvent evt)
		{
			source.Delete(rule.GetSourceContext(base.Name, evt, true));
		}

		// Token: 0x040000B9 RID: 185
		private readonly FilteredRuleCache filteredRuleCache;
	}
}
