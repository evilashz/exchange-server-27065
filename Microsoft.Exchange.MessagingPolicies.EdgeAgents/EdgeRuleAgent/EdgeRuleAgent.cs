using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.Transport.Agent.AntiSpam.Common;

namespace Microsoft.Exchange.MessagingPolicies.EdgeRuleAgent
{
	// Token: 0x02000003 RID: 3
	internal class EdgeRuleAgent : SmtpReceiveAgent
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002449 File Offset: 0x00000649
		public EdgeRuleAgent(SmtpServer server, TransportRuleCollection rules, bool shouldDefer)
		{
			base.OnEndOfData += this.EndOfDataHandler;
			this.server = server;
			this.rules = rules;
			this.shouldDefer = shouldDefer;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002478 File Offset: 0x00000678
		public void EndOfDataHandler(ReceiveMessageEventSource source, EndOfDataEventArgs args)
		{
			if (this.shouldDefer)
			{
				AgentLog.Instance.LogRejectMessage(base.Name, base.EventTopic, args, args.SmtpSession, args.MailItem, SmtpResponse.DataTransactionFailed, EdgeRuleAgent.TransientError);
				source.RejectMessage(SmtpResponse.DataTransactionFailed);
				return;
			}
			if (args.MailItem.Recipients.Count == 0)
			{
				return;
			}
			try
			{
				if (this.rules != null)
				{
					ExecutionStatus executionStatus = this.rules.Run(this.server, args.MailItem, source, args.SmtpSession, null);
					if (executionStatus == ExecutionStatus.TransientError)
					{
						AgentLog.Instance.LogRejectMessage(base.Name, base.EventTopic, args, args.SmtpSession, args.MailItem, SmtpResponse.DataTransactionFailed, EdgeRuleAgent.TransientError);
						source.RejectMessage(SmtpResponse.DataTransactionFailed);
					}
				}
			}
			catch (ExchangeDataException ex)
			{
				AgentLog.Instance.LogRejectMessage(base.Name, base.EventTopic, args, args.SmtpSession, args.MailItem, SmtpResponse.InvalidContent, new LogEntry("InvalidContent", ex.Message));
				source.RejectMessage(SmtpResponse.InvalidContent);
			}
		}

		// Token: 0x04000008 RID: 8
		private static readonly LogEntry TransientError = new LogEntry("TransientError", string.Empty);

		// Token: 0x04000009 RID: 9
		private TransportRuleCollection rules;

		// Token: 0x0400000A RID: 10
		private SmtpServer server;

		// Token: 0x0400000B RID: 11
		private bool shouldDefer;
	}
}
