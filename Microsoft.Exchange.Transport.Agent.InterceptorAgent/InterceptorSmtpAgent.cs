using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Transport.Agent.AntiSpam.Common;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x0200001E RID: 30
	internal sealed class InterceptorSmtpAgent : SmtpReceiveAgent
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00007138 File Offset: 0x00005338
		public InterceptorSmtpAgent(FilteredRuleCache filteredRuleCache)
		{
			this.filteredRuleCache = filteredRuleCache;
			base.OnEndOfData += this.EodHandler;
			base.OnEndOfHeaders += this.EohHandler;
			base.OnRcptCommand += this.RcptHandler;
			base.OnMailCommand += this.MailHandler;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000719C File Offset: 0x0000539C
		private void MailHandler(ReceiveCommandEventSource source, MailCommandEventArgs mailArgs)
		{
			InterceptorAgentRule rule = InterceptorAgentRuleEvaluator.Evaluate(this.filteredRuleCache.Rules, mailArgs);
			this.Run(rule, null, new InterceptorSmtpAgent.RejectCommand(source.RejectCommand), null, source.SmtpSession, mailArgs, InterceptorAgentEvent.OnMailFrom, true);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000071DC File Offset: 0x000053DC
		private void RcptHandler(ReceiveCommandEventSource source, RcptCommandEventArgs rcptArgs)
		{
			InterceptorAgentRule rule = InterceptorAgentRuleEvaluator.Evaluate(this.filteredRuleCache.Rules, rcptArgs);
			this.Run(rule, null, new InterceptorSmtpAgent.RejectCommand(source.RejectCommand), null, source.SmtpSession, rcptArgs, InterceptorAgentEvent.OnRcptTo, true);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000721C File Offset: 0x0000541C
		private void EohHandler(ReceiveMessageEventSource source, EndOfHeadersEventArgs eohArgs)
		{
			InterceptorAgentRule rule = InterceptorAgentRuleEvaluator.Evaluate(this.filteredRuleCache.Rules, eohArgs);
			this.Run(rule, eohArgs.MailItem, new InterceptorSmtpAgent.RejectCommand(source.RejectMessage), new InterceptorSmtpAgent.DiscardMessage(source.DiscardMessage), source.SmtpSession, eohArgs, InterceptorAgentEvent.OnEndOfHeaders, false);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000726C File Offset: 0x0000546C
		private void EodHandler(ReceiveMessageEventSource source, EndOfDataEventArgs eodArgs)
		{
			InterceptorAgentRule rule = InterceptorAgentRuleEvaluator.Evaluate(this.filteredRuleCache.Rules, eodArgs);
			this.Run(rule, eodArgs.MailItem, new InterceptorSmtpAgent.RejectCommand(source.RejectMessage), new InterceptorSmtpAgent.DiscardMessage(source.DiscardMessage), source.SmtpSession, eodArgs, InterceptorAgentEvent.OnEndOfData, false);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007384 File Offset: 0x00005584
		private void Run(InterceptorAgentRule rule, MailItem mail, InterceptorSmtpAgent.RejectCommand rejectCmd, InterceptorSmtpAgent.DiscardMessage discardMessage, SmtpSession session, EventArgs args, InterceptorAgentEvent evt, bool logCommand)
		{
			if (rule == null)
			{
				return;
			}
			string sourceContext = rule.GetSourceContext(base.Name, evt, false);
			rule.PerformAction(mail, delegate
			{
				discardMessage(SmtpResponse.SuccessfulConnection, sourceContext);
			}, delegate(SmtpResponse response)
			{
				if (logCommand)
				{
					AgentLog.Instance.LogRejectCommand(this.Name, this.EventTopic, (ReceiveCommandEventArgs)args, response, new LogEntry(string.Empty, sourceContext));
				}
				else
				{
					AgentLog.Instance.LogRejectMessage(this.Name, this.EventTopic, args, session, mail, response, new LogEntry(string.Empty, sourceContext));
				}
				rejectCmd(response, sourceContext);
			}, null);
		}

		// Token: 0x040000BC RID: 188
		private readonly FilteredRuleCache filteredRuleCache;

		// Token: 0x0200001F RID: 31
		// (Invoke) Token: 0x0600013B RID: 315
		private delegate void RejectCommand(SmtpResponse resp, string context);

		// Token: 0x02000020 RID: 32
		// (Invoke) Token: 0x0600013F RID: 319
		private delegate void DiscardMessage(SmtpResponse response, string context);
	}
}
