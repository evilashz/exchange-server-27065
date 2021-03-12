using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.Transport.Agent.AntiSpam.Common
{
	// Token: 0x02000003 RID: 3
	internal interface IAgentLog
	{
		// Token: 0x06000007 RID: 7
		void LogRejectConnection(string agentName, string eventTopic, ConnectEventArgs eventArgs, SmtpResponse smtpResponse, LogEntry logEntry);

		// Token: 0x06000008 RID: 8
		void LogRejectAuthentication(string agentName, string eventTopic, EndOfAuthenticationEventArgs eventArgs, SmtpResponse smtpResponse, LogEntry logEntry);

		// Token: 0x06000009 RID: 9
		void LogRejectCommand(string agentName, string eventTopic, ReceiveCommandEventArgs eventArgs, SmtpResponse smtpResponse, LogEntry logEntry);

		// Token: 0x0600000A RID: 10
		void LogRejectMessage(string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, MailItem mailItem, SmtpResponse smtpResponse, LogEntry logEntry);

		// Token: 0x0600000B RID: 11
		void LogDeleteMessage(string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, MailItem mailItem, LogEntry logEntry);

		// Token: 0x0600000C RID: 12
		void LogQuarantineAction(string agentName, string eventTopic, EndOfDataEventArgs eventArgs, AgentAction action, IEnumerable<EnvelopeRecipient> recipients, SmtpResponse smtpResponse, LogEntry logEntry);

		// Token: 0x0600000D RID: 13
		void LogDisconnect(string agentName, string eventTopic, SmtpSession smtpSession, MailItem mailItem, LogEntry logEntry);

		// Token: 0x0600000E RID: 14
		void LogRejectRecipients(string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, MailItem mailItem, IEnumerable<EnvelopeRecipient> recipients, SmtpResponse smtpResponse, LogEntry logEntry);

		// Token: 0x0600000F RID: 15
		void LogDeleteRecipients(string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, MailItem mailItem, IEnumerable<EnvelopeRecipient> recipients, LogEntry logEntry);

		// Token: 0x06000010 RID: 16
		void LogAcceptMessage(string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, MailItem mailItem, LogEntry logEntry);

		// Token: 0x06000011 RID: 17
		void LogModifyHeaders(string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, MailItem mailItem, LogEntry logEntry);

		// Token: 0x06000012 RID: 18
		void LogStampScl(string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, MailItem mailItem, LogEntry logEntry);

		// Token: 0x06000013 RID: 19
		void LogAttributionResult(string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, MailItem mailItem, LogEntry logEntry);

		// Token: 0x06000014 RID: 20
		void LogOnPremiseInboundConnectorInfo(string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, MailItem mailItem, LogEntry logEntry);

		// Token: 0x06000015 RID: 21
		void LogInvalidCertificate(string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, MailItem mailItem, LogEntry logEntry);

		// Token: 0x06000016 RID: 22
		void LogNukeAction(string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, MailItem mailItem, IEnumerable<EnvelopeRecipient> recipients, SmtpResponse smtpResponse, LogEntry logEntry);

		// Token: 0x06000017 RID: 23
		void LogAgentAction(string agentName, string eventTopic, EventArgs eventArgs, IEnumerable<RoutingAddress> recipients, AgentAction action, SmtpResponse smtpResponse, LogEntry logEntry, IDictionary<AgentLogField, object> agentLogData, Guid systemProbeId, string internetMessageId);

		// Token: 0x06000018 RID: 24
		IDictionary<AgentLogField, object> GetAgentLogData(SmtpSession smtpSession, MailItem mailItem);
	}
}
