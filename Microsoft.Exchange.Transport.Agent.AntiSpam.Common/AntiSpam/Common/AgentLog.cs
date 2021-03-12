using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport.LoggingCommon;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport.Agent.AntiSpam.Common
{
	// Token: 0x02000004 RID: 4
	internal class AgentLog : IAgentLog
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002130 File Offset: 0x00000330
		public AgentLog()
		{
			string[] array = new string[20];
			for (int i = 0; i < 20; i++)
			{
				array[i] = ((AgentLogField)i).ToString();
			}
			this.agentLogSchema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "Agent Log", array);
			this.log = new Log("AgentLog", new LogHeaderFormatter(this.agentLogSchema), "AgentLogs");
			this.Configure(Components.Configuration.LocalServer);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000021C0 File Offset: 0x000003C0
		public static IAgentLog Instance
		{
			get
			{
				if (AgentLog.instance == null)
				{
					lock (AgentLog.agentLogCreationLock)
					{
						if (AgentLog.instance == null)
						{
							AgentLog agentLog = new AgentLog();
							Thread.MemoryBarrier();
							AgentLog.instance = agentLog;
						}
					}
				}
				return AgentLog.instance;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002220 File Offset: 0x00000420
		private static ProcessTransportRole GetProcessTransportRole()
		{
			ITransportConfiguration transportConfiguration;
			ProcessTransportRole result;
			if (Components.TryGetConfigurationComponent(out transportConfiguration))
			{
				result = transportConfiguration.ProcessTransportRole;
			}
			else
			{
				result = ProcessTransportRole.Hub;
			}
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002244 File Offset: 0x00000444
		public void LogRejectConnection(string agentName, string eventTopic, ConnectEventArgs eventArgs, SmtpResponse smtpResponse, LogEntry logEntry)
		{
			this.LogAgentAction(agentName, eventTopic, eventArgs, eventArgs.SmtpSession, null, null, AgentAction.RejectConnection, smtpResponse, logEntry);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002268 File Offset: 0x00000468
		public void LogRejectAuthentication(string agentName, string eventTopic, EndOfAuthenticationEventArgs eventArgs, SmtpResponse smtpResponse, LogEntry logEntry)
		{
			this.LogAgentAction(agentName, eventTopic, eventArgs, eventArgs.SmtpSession, null, null, AgentAction.RejectAuthentication, smtpResponse, logEntry);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000228C File Offset: 0x0000048C
		public void LogRejectCommand(string agentName, string eventTopic, ReceiveCommandEventArgs eventArgs, SmtpResponse smtpResponse, LogEntry logEntry)
		{
			MailItem mailItem = null;
			if (eventArgs != null)
			{
				DataCommandEventArgs dataCommandEventArgs = eventArgs as DataCommandEventArgs;
				if (dataCommandEventArgs != null)
				{
					mailItem = dataCommandEventArgs.MailItem;
				}
				else
				{
					RcptCommandEventArgs rcptCommandEventArgs = eventArgs as RcptCommandEventArgs;
					if (rcptCommandEventArgs != null)
					{
						mailItem = rcptCommandEventArgs.MailItem;
					}
				}
			}
			this.LogAgentAction(agentName, eventTopic, eventArgs, eventArgs.SmtpSession, mailItem, null, AgentAction.RejectCommand, smtpResponse, logEntry);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000022D8 File Offset: 0x000004D8
		public void LogRejectMessage(string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, MailItem mailItem, SmtpResponse smtpResponse, LogEntry logEntry)
		{
			this.LogAgentAction(agentName, eventTopic, eventArgs, smtpSession, mailItem, (mailItem != null) ? mailItem.Recipients : null, AgentAction.RejectMessage, smtpResponse, logEntry);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002308 File Offset: 0x00000508
		public void LogDeleteMessage(string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, MailItem mailItem, LogEntry logEntry)
		{
			this.LogAgentAction(agentName, eventTopic, eventArgs, smtpSession, mailItem, (mailItem != null) ? mailItem.Recipients : null, AgentAction.DeleteMessage, SmtpResponse.Empty, logEntry);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000233C File Offset: 0x0000053C
		public void LogQuarantineAction(string agentName, string eventTopic, EndOfDataEventArgs eventArgs, AgentAction action, IEnumerable<EnvelopeRecipient> recipients, SmtpResponse smtpResponse, LogEntry logEntry)
		{
			this.LogAgentAction(agentName, eventTopic, eventArgs, eventArgs.SmtpSession, eventArgs.MailItem, recipients, action, smtpResponse, logEntry);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002368 File Offset: 0x00000568
		public void LogDisconnect(string agentName, string eventTopic, SmtpSession smtpSession, MailItem mailItem, LogEntry logEntry)
		{
			this.LogAgentAction(agentName, eventTopic, null, smtpSession, mailItem, (mailItem != null) ? mailItem.Recipients : null, AgentAction.Disconnect, SmtpResponse.Empty, logEntry);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002398 File Offset: 0x00000598
		public void LogRejectRecipients(string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, MailItem mailItem, IEnumerable<EnvelopeRecipient> recipients, SmtpResponse smtpResponse, LogEntry logEntry)
		{
			this.LogAgentAction(agentName, eventTopic, eventArgs, smtpSession, mailItem, recipients, AgentAction.RejectRecipients, smtpResponse, logEntry);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023BC File Offset: 0x000005BC
		public void LogDeleteRecipients(string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, MailItem mailItem, IEnumerable<EnvelopeRecipient> recipients, LogEntry logEntry)
		{
			this.LogAgentAction(agentName, eventTopic, eventArgs, smtpSession, mailItem, recipients, AgentAction.DeleteRecipients, SmtpResponse.Empty, logEntry);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000023E4 File Offset: 0x000005E4
		public void LogAcceptMessage(string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, MailItem mailItem, LogEntry logEntry)
		{
			this.LogAgentAction(agentName, eventTopic, eventArgs, smtpSession, mailItem, (mailItem != null) ? mailItem.Recipients : null, AgentAction.AcceptMessage, SmtpResponse.Empty, logEntry);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002414 File Offset: 0x00000614
		public void LogModifyHeaders(string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, MailItem mailItem, LogEntry logEntry)
		{
			this.LogAgentAction(agentName, eventTopic, eventArgs, smtpSession, mailItem, (mailItem != null) ? mailItem.Recipients : null, AgentAction.ModifyHeaders, SmtpResponse.Empty, logEntry);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002448 File Offset: 0x00000648
		public void LogStampScl(string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, MailItem mailItem, LogEntry logEntry)
		{
			this.LogAgentAction(agentName, eventTopic, eventArgs, smtpSession, mailItem, (mailItem != null) ? mailItem.Recipients : null, AgentAction.StampScl, SmtpResponse.Empty, logEntry);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000247C File Offset: 0x0000067C
		public void LogAttributionResult(string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, MailItem mailItem, LogEntry logEntry)
		{
			this.LogAgentAction(agentName, eventTopic, eventArgs, smtpSession, mailItem, (mailItem != null) ? mailItem.Recipients : null, AgentAction.AttributionResult, SmtpResponse.Empty, logEntry);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000024B0 File Offset: 0x000006B0
		public void LogOnPremiseInboundConnectorInfo(string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, MailItem mailItem, LogEntry logEntry)
		{
			this.LogAgentAction(agentName, eventTopic, eventArgs, smtpSession, mailItem, (mailItem != null) ? mailItem.Recipients : null, AgentAction.OnPremiseInboundConnectorInfo, SmtpResponse.Empty, logEntry);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000024E4 File Offset: 0x000006E4
		public void LogInvalidCertificate(string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, MailItem mailItem, LogEntry logEntry)
		{
			this.LogAgentAction(agentName, eventTopic, eventArgs, smtpSession, mailItem, (mailItem != null) ? mailItem.Recipients : null, AgentAction.InvalidCertificate, SmtpResponse.Empty, logEntry);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002518 File Offset: 0x00000718
		public void LogNukeAction(string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, MailItem mailItem, IEnumerable<EnvelopeRecipient> recipients, SmtpResponse smtpResponse, LogEntry logEntry)
		{
			this.LogAgentAction(agentName, eventTopic, eventArgs, smtpSession, mailItem, recipients, AgentAction.AutoNukeRecipient, smtpResponse, logEntry);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002544 File Offset: 0x00000744
		internal void LogAgentAction(string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, MailItem mailItem, IEnumerable<EnvelopeRecipient> recipients, AgentAction action, SmtpResponse smtpResponse, LogEntry logEntry)
		{
			IDictionary<AgentLogField, object> agentLogData = this.GetAgentLogData(smtpSession, mailItem);
			List<RoutingAddress> recipients2 = null;
			if (recipients != null)
			{
				recipients2 = new List<RoutingAddress>(from recipient in recipients
				select recipient.Address);
			}
			if (mailItem == null)
			{
				this.LogAgentAction(agentName, eventTopic, eventArgs, recipients2, action, smtpResponse, logEntry, agentLogData);
				return;
			}
			this.LogAgentAction(agentName, eventTopic, eventArgs, recipients2, action, smtpResponse, logEntry, agentLogData, mailItem.SystemProbeId, mailItem.InternetMessageId);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000025C4 File Offset: 0x000007C4
		public IDictionary<AgentLogField, object> GetAgentLogData(SmtpSession smtpSession, MailItem mailItem)
		{
			Dictionary<AgentLogField, object> dictionary = new Dictionary<AgentLogField, object>();
			if (!this.enabled)
			{
				return dictionary;
			}
			if (smtpSession == null)
			{
				throw new ArgumentNullException("smtpSession");
			}
			dictionary[AgentLogField.SessionId] = smtpSession.SessionId.ToString("X16", CultureInfo.InvariantCulture);
			dictionary[AgentLogField.LocalEndpoint] = smtpSession.LocalEndPoint;
			dictionary[AgentLogField.RemoteEndpoint] = smtpSession.RemoteEndPoint;
			if (smtpSession.IsExternalConnection)
			{
				dictionary[AgentLogField.EnteredOrgFromIP] = smtpSession.RemoteEndPoint.Address;
			}
			else
			{
				dictionary[AgentLogField.EnteredOrgFromIP] = smtpSession.LastExternalIPAddress;
			}
			if (mailItem != null)
			{
				if (mailItem.Message != null)
				{
					dictionary[AgentLogField.MessageId] = mailItem.Message.MessageId;
					dictionary[AgentLogField.P2FromAddresses] = AgentLog.ReplaceNewLinesWithSpace(AgentLog.GetP2From(mailItem));
				}
				dictionary[AgentLogField.P1FromAddress] = AgentLog.ReplaceNewLinesWithSpace(mailItem.FromAddress.ToString());
				if (mailItem.NetworkMessageId != Guid.Empty)
				{
					dictionary[AgentLogField.NetworkMsgID] = mailItem.NetworkMessageId;
				}
				if (mailItem.TenantId != Guid.Empty)
				{
					dictionary[AgentLogField.TenantID] = mailItem.TenantId;
				}
			}
			dictionary[AgentLogField.Directionality] = this.GetDirectionalityString(mailItem);
			return dictionary;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002700 File Offset: 0x00000900
		internal void LogAgentAction(string agentName, string eventTopic, EventArgs eventArgs, IEnumerable<RoutingAddress> recipients, AgentAction action, SmtpResponse smtpResponse, LogEntry logEntry, IDictionary<AgentLogField, object> agentLogData)
		{
			this.LogAgentAction(agentName, eventTopic, eventArgs, recipients, action, smtpResponse, logEntry, agentLogData, Guid.Empty, null);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002728 File Offset: 0x00000928
		public void LogAgentAction(string agentName, string eventTopic, EventArgs eventArgs, IEnumerable<RoutingAddress> recipients, AgentAction action, SmtpResponse smtpResponse, LogEntry logEntry, IDictionary<AgentLogField, object> agentLogData, Guid systemProbeId, string internetMessageId)
		{
			if (!this.enabled)
			{
				return;
			}
			if (agentLogData == null || agentLogData.Count == 0)
			{
				throw new ArgumentNullException("agentLogData");
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.agentLogSchema);
			foreach (KeyValuePair<AgentLogField, object> keyValuePair in agentLogData)
			{
				if (keyValuePair.Value != null)
				{
					logRowFormatter[(int)keyValuePair.Key] = keyValuePair.Value;
				}
			}
			logRowFormatter[11] = eventTopic;
			logRowFormatter[10] = agentName;
			logRowFormatter[12] = action;
			AgentLog.AddRecipientsData(logRowFormatter, recipients);
			AgentLog.AddEventArgsData(logRowFormatter, eventArgs);
			AgentLog.AddSmtpResponseData(logRowFormatter, smtpResponse);
			AgentLog.AddLogEntryData(logRowFormatter, logEntry);
			this.log.Append(logRowFormatter, 0);
			AgentLog.PublishSystemProbeNotification(agentName, action, systemProbeId, internetMessageId);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002824 File Offset: 0x00000A24
		private static void AddRecipientsData(LogRowFormatter row, IEnumerable<RoutingAddress> recipients)
		{
			if (recipients == null)
			{
				recipients = new List<RoutingAddress>();
			}
			string[] array = (from recipient in recipients
			select AgentLog.ReplaceNewLinesWithSpace(recipient.ToString())).ToArray<string>();
			row[9] = array.Length;
			row[8] = array;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000287C File Offset: 0x00000A7C
		private static void AddLogEntryData(LogRowFormatter row, LogEntry logEntry)
		{
			if (logEntry != null)
			{
				if (!string.IsNullOrEmpty(logEntry.Reason))
				{
					string text = AgentLog.ReplaceNewLinesWithSpace(logEntry.Reason);
					row[14] = ((text.Length <= 256) ? text : text.Substring(0, 256));
				}
				if (!string.IsNullOrEmpty(logEntry.ReasonData))
				{
					string text2 = AgentLog.ReplaceNewLinesWithSpace(logEntry.ReasonData);
					row[15] = ((text2.Length <= 256) ? text2 : text2.Substring(0, 256));
				}
				if (!string.IsNullOrEmpty(logEntry.Diagnostics))
				{
					string text3 = AgentLog.ReplaceNewLinesWithSpace(logEntry.Diagnostics);
					row[16] = ((text3.Length <= 256) ? text3 : text3.Substring(0, 256));
				}
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002948 File Offset: 0x00000B48
		private static void AddSmtpResponseData(LogRowFormatter row, SmtpResponse smtpResponse)
		{
			if (!SmtpResponse.Empty.Equals(smtpResponse))
			{
				string text = AgentLog.ReplaceNewLinesWithSpace(smtpResponse.ToString());
				row[13] = ((text.Length <= 256) ? text : text.Substring(0, 256));
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000299C File Offset: 0x00000B9C
		private static void AddEventArgsData(LogRowFormatter row, EventArgs eventArgs)
		{
			if (eventArgs != null)
			{
				if (eventArgs is MailCommandEventArgs)
				{
					MailCommandEventArgs mailCommandEventArgs = eventArgs as MailCommandEventArgs;
					row[6] = AgentLog.ReplaceNewLinesWithSpace(mailCommandEventArgs.FromAddress.ToString());
					return;
				}
				if (eventArgs is RcptCommandEventArgs)
				{
					RcptCommandEventArgs rcptCommandEventArgs = eventArgs as RcptCommandEventArgs;
					row[9] = 1;
					row[8] = AgentLog.ReplaceNewLinesWithSpace(rcptCommandEventArgs.RecipientAddress.ToString());
					return;
				}
				if (eventArgs is EndOfHeadersEventArgs)
				{
					EndOfHeadersEventArgs endOfHeadersEventArgs = eventArgs as EndOfHeadersEventArgs;
					row[7] = AgentLog.ReplaceNewLinesWithSpace(AgentLog.GetP2From(endOfHeadersEventArgs.Headers));
				}
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A44 File Offset: 0x00000C44
		private static void PublishSystemProbeNotification(string agentName, AgentAction action, Guid systemProbeId, string internetMessageId)
		{
			if (systemProbeId != Guid.Empty && !string.IsNullOrEmpty(internetMessageId))
			{
				EventNotificationItem eventNotificationItem = new EventNotificationItem(ExchangeComponent.Transport.Name, "MessageTracking", systemProbeId.ToString(), ResultSeverityLevel.Verbose);
				string value = string.Format("Agent={0};Action={1}", agentName, action);
				eventNotificationItem.AddCustomProperty("StateAttribute1", internetMessageId);
				eventNotificationItem.AddCustomProperty("StateAttribute2", "AGENTINFO");
				eventNotificationItem.AddCustomProperty("StateAttribute3", value);
				eventNotificationItem.Publish(false);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002ACC File Offset: 0x00000CCC
		private static string GetP2From(MailItem mailItem)
		{
			if (mailItem.Message.MimeDocument.RootPart == null || mailItem.Message.MimeDocument.RootPart.Headers == null)
			{
				return string.Empty;
			}
			return AgentLog.GetP2From(mailItem.Message.MimeDocument.RootPart.Headers);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002B24 File Offset: 0x00000D24
		private static string GetP2From(HeaderList headers)
		{
			Header[] array = headers.FindAll(HeaderId.From);
			List<string> list = new List<string>(array.Length);
			foreach (Header header in array)
			{
				foreach (AddressItem addressItem in ((AddressHeader)header))
				{
					MimeRecipient mimeRecipient = addressItem as MimeRecipient;
					if (mimeRecipient != null)
					{
						string email = mimeRecipient.Email;
						if (!string.IsNullOrEmpty(email))
						{
							list.Add(email);
						}
					}
					else
					{
						MimeGroup mimeGroup = addressItem as MimeGroup;
						if (mimeGroup != null)
						{
							foreach (MimeRecipient mimeRecipient2 in mimeGroup)
							{
								string email = mimeRecipient2.Email;
								if (!string.IsNullOrEmpty(email))
								{
									list.Add(email);
								}
							}
						}
					}
				}
				if (list.Count >= 10)
				{
					break;
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			while (num < list.Count && num < 10)
			{
				stringBuilder.Append(list[num]);
				stringBuilder.Append(';');
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002C84 File Offset: 0x00000E84
		private static string ReplaceNewLinesWithSpace(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return s;
			}
			StringBuilder stringBuilder = new StringBuilder(s.Length);
			bool flag = false;
			bool flag2 = false;
			foreach (char c in s)
			{
				char c2 = c;
				if (c2 == '\n' || c2 == '\r')
				{
					if (!flag)
					{
						stringBuilder.Append(' ');
					}
					flag = true;
					flag2 = true;
				}
				else
				{
					stringBuilder.Append(c);
					flag = false;
				}
			}
			if (!flag2)
			{
				return s;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002D04 File Offset: 0x00000F04
		private void Configure(TransportServerConfiguration server)
		{
			Server transportServer = server.TransportServer;
			ProcessTransportRole processTransportRole = AgentLog.GetProcessTransportRole();
			if (processTransportRole == ProcessTransportRole.MailboxDelivery)
			{
				this.Configure(transportServer.MailboxDeliveryAgentLogEnabled, transportServer.MailboxDeliveryAgentLogPath, transportServer.MailboxDeliveryAgentLogMaxAge, transportServer.MailboxDeliveryAgentLogMaxDirectorySize, transportServer.MailboxDeliveryAgentLogMaxFileSize);
				return;
			}
			if (processTransportRole == ProcessTransportRole.MailboxSubmission)
			{
				this.Configure(transportServer.MailboxSubmissionAgentLogEnabled, transportServer.MailboxSubmissionAgentLogPath, transportServer.MailboxSubmissionAgentLogMaxAge, transportServer.MailboxSubmissionAgentLogMaxDirectorySize, transportServer.MailboxSubmissionAgentLogMaxFileSize);
				return;
			}
			this.Configure(transportServer.AgentLogEnabled, transportServer.AgentLogPath, transportServer.AgentLogMaxAge, transportServer.AgentLogMaxDirectorySize, transportServer.AgentLogMaxFileSize);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002D94 File Offset: 0x00000F94
		private void Configure(bool enabled, LocalLongFullPath path, EnhancedTimeSpan maxAge, Unlimited<ByteQuantifiedSize> maxDirectorySize, Unlimited<ByteQuantifiedSize> maxFileSize)
		{
			if (!enabled)
			{
				this.enabled = false;
				return;
			}
			if (path == null || string.IsNullOrEmpty(path.PathName))
			{
				this.enabled = false;
				ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Agent Log path was set to null, Agent Log is disabled");
				Components.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_AgentLogPathIsNull, null, new object[0]);
				return;
			}
			this.log.Configure(path.PathName, maxAge, (long)(maxDirectorySize.IsUnlimited ? 0UL : maxDirectorySize.Value.ToBytes()), (long)(maxFileSize.IsUnlimited ? 0UL : maxFileSize.Value.ToBytes()));
			this.enabled = true;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002E4C File Offset: 0x0000104C
		private string GetDirectionalityString(MailItem item)
		{
			TransportMailItemWrapper transportMailItemWrapper = item as TransportMailItemWrapper;
			if (transportMailItemWrapper != null && transportMailItemWrapper.TransportMailItem != null)
			{
				return transportMailItemWrapper.TransportMailItem.Directionality.ToString();
			}
			return MailDirectionality.Undefined.ToString();
		}

		// Token: 0x04000004 RID: 4
		private const int MaxFromHeaders = 10;

		// Token: 0x04000005 RID: 5
		private const int MaxCustomStringLength = 256;

		// Token: 0x04000006 RID: 6
		private const string LogComponentName = "AgentLogs";

		// Token: 0x04000007 RID: 7
		private static readonly object agentLogCreationLock = new object();

		// Token: 0x04000008 RID: 8
		private static readonly TimeSpan DefaultMaxAge = TimeSpan.FromDays(30.0);

		// Token: 0x04000009 RID: 9
		private static IAgentLog instance;

		// Token: 0x0400000A RID: 10
		private Log log;

		// Token: 0x0400000B RID: 11
		private LogSchema agentLogSchema;

		// Token: 0x0400000C RID: 12
		private bool enabled;
	}
}
