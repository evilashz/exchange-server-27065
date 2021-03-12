using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Data.Transport.StoreDriver;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x02000024 RID: 36
	internal sealed class InterceptorSubmissionAgent : SubmissionAgent
	{
		// Token: 0x06000151 RID: 337 RVA: 0x000075CD File Offset: 0x000057CD
		public InterceptorSubmissionAgent(FilteredRuleCache filteredRuleCache)
		{
			this.filteredRuleCache = filteredRuleCache;
			base.OnDemotedMessage += this.DemotedMessageEventHandler;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000075EE File Offset: 0x000057EE
		private void DemotedMessageEventHandler(StoreDriverEventSource source, StoreDriverSubmissionEventArgs e)
		{
			this.HandleMessage(source, e);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007638 File Offset: 0x00005838
		private void HandleMessage(StoreDriverEventSource source, StoreDriverSubmissionEventArgs e)
		{
			InterceptorAgentRule interceptorAgentRule = InterceptorAgentRuleEvaluator.Evaluate(this.filteredRuleCache.Rules, e);
			if (interceptorAgentRule == null)
			{
				return;
			}
			interceptorAgentRule.PerformAction(e.MailItem, delegate
			{
				throw new SmtpResponseException(new SmtpResponse("250", "2.7.0", new string[]
				{
					"STOREDRV.Submit; message deleted by administrative rule"
				}), "Interceptor Submission Agent");
			}, delegate(SmtpResponse response)
			{
				throw new SmtpResponseException(response);
			}, null);
		}

		// Token: 0x040000C4 RID: 196
		private readonly FilteredRuleCache filteredRuleCache;
	}
}
