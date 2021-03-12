using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ContentTypes.Tnef;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Transport.LoggingCommon;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport.Logging.MessageTracking
{
	// Token: 0x02000087 RID: 135
	internal class MessageTrackingLog
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00010B92 File Offset: 0x0000ED92
		internal static LogSchema MsgTrackingSchema
		{
			get
			{
				return MessageTrackingLog.msgTrackingSchema;
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00010B99 File Offset: 0x0000ED99
		public static void Start()
		{
			MessageTrackingLog.Start("MSGTRK");
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00010BA8 File Offset: 0x0000EDA8
		public static void Start(string logFilePrefix)
		{
			MessageTrackingLog.msgTrackingSchema = new LogSchema("Microsoft Exchange Server", "15.00.1497.010", "Message Tracking Log", MessageTrackingLog.Fields);
			MessageTrackingLog.log = new Log(logFilePrefix, new LogHeaderFormatter(MessageTrackingLog.msgTrackingSchema), "MessageTrackingLogs");
			if (MessageTrackingLog.log.LogDirectory != null)
			{
				MessageTrackingLog.log.LogDirectory.OnDirSizeQuotaExceeded += MessageTrackingLog.MessageTrackingLogDirSizeQuotaExceeded;
			}
			MessageTrackingLog.hostName = Dns.GetHostName();
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00010C1E File Offset: 0x0000EE1E
		public static void Stop()
		{
			if (MessageTrackingLog.log != null)
			{
				if (MessageTrackingLog.log.LogDirectory != null)
				{
					MessageTrackingLog.log.LogDirectory.OnDirSizeQuotaExceeded -= MessageTrackingLog.MessageTrackingLogDirSizeQuotaExceeded;
				}
				MessageTrackingLog.log.Close();
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00010C58 File Offset: 0x0000EE58
		public static void TrackLoadedMessage(MessageTrackingSource trackingSource, MessageTrackingEvent trackingEvent, TransportMailItem mailItem)
		{
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			logRowFormatter[8] = trackingEvent;
			logRowFormatter[7] = trackingSource;
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, mailItem, false);
			logRowFormatter[15] = mailItem.Recipients.Count;
			logRowFormatter[12] = mailItem.Recipients;
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00010CC8 File Offset: 0x0000EEC8
		public static void TrackPoisonMessage(MessageTrackingSource source, IReadOnlyMailItem mailItem)
		{
			MessageTrackingLog.WriteLamEventNotification(mailItem, MessageTrackingEvent.POISONMESSAGE);
			MessageTrackingLog.WriteLamPoisonEventNotification(mailItem);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			logRowFormatter[8] = MessageTrackingEvent.POISONMESSAGE;
			logRowFormatter[7] = source;
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, mailItem, true);
			logRowFormatter[15] = mailItem.Recipients.Count;
			logRowFormatter[12] = mailItem.Recipients;
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00010D48 File Offset: 0x0000EF48
		public static void TrackPoisonMessage(MessageTrackingSource source, MsgTrackPoisonInfo msgTrackingPoisonInfo)
		{
			MessageTrackingLog.WriteLamPoisonEventNotification(source, msgTrackingPoisonInfo);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			logRowFormatter[8] = MessageTrackingEvent.POISONMESSAGE;
			logRowFormatter[7] = source;
			logRowFormatter[1] = msgTrackingPoisonInfo.ClientIPAddress;
			logRowFormatter[2] = msgTrackingPoisonInfo.ClientHostName;
			logRowFormatter[3] = msgTrackingPoisonInfo.ServerIPAddress;
			logRowFormatter[5] = msgTrackingPoisonInfo.SourceContext;
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00010DC4 File Offset: 0x0000EFC4
		public static void TrackPoisonMessage(MessageTrackingSource source, TransportMailItem mailItem, string messageId, MsgTrackReceiveInfo msgTrackInfo)
		{
			MessageTrackingLog.WriteLamEventNotification(mailItem, MessageTrackingEvent.POISONMESSAGE);
			MessageTrackingLog.WriteLamPoisonEventNotification(mailItem);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			List<KeyValuePair<string, object>> list = null;
			if (!string.IsNullOrEmpty(msgTrackInfo.MailboxDatabaseGuid))
			{
				MessageTrackingLog.AddCustomData("MailboxDatabaseGuid", msgTrackInfo.MailboxDatabaseGuid, ref list);
			}
			if (msgTrackInfo.EntryId != null)
			{
				string value = BitConverter.ToString(msgTrackInfo.EntryId);
				MessageTrackingLog.AddCustomData("ItemEntryId", value, ref list);
			}
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, mailItem, ref list, true);
			MessageTrackingLog.SetSubmittingSender(logRowFormatter, mailItem.MimeSender, msgTrackInfo.SubmittingMailboxSmtpAddress);
			logRowFormatter[8] = MessageTrackingEvent.POISONMESSAGE;
			logRowFormatter[22] = MailDirectionality.Originating;
			logRowFormatter[7] = source;
			logRowFormatter[4] = MessageTrackingLog.hostName;
			logRowFormatter[5] = msgTrackInfo.SourceContext;
			logRowFormatter[1] = msgTrackInfo.ClientIPAddress;
			logRowFormatter[3] = msgTrackInfo.ServerIPAddress;
			logRowFormatter[2] = msgTrackInfo.ClientHostname;
			if (!string.IsNullOrEmpty(messageId))
			{
				logRowFormatter[10] = messageId;
			}
			else if (mailItem.Message != null && mailItem.Message.MessageId != null)
			{
				logRowFormatter[10] = mailItem.Message.MessageId;
			}
			logRowFormatter[15] = mailItem.Recipients.Count;
			logRowFormatter[12] = mailItem.Recipients.AllUnprocessed;
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00010F28 File Offset: 0x0000F128
		public static void TrackReceiveForApprovalRelease(TransportMailItem mailItem, string approver, string initiationMessageId)
		{
			MsgTrackReceiveInfo msgTrackInfo = new MsgTrackReceiveInfo(null, approver, initiationMessageId, null, null);
			MessageTrackingLog.TrackReceive(MessageTrackingSource.APPROVAL, mailItem, null, msgTrackInfo);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00010F4C File Offset: 0x0000F14C
		public static void TrackReceiveByAgent(ITransportMailItemFacade mailItem, string sourceContext, string connectorId, long? relatedMailItemId)
		{
			MsgTrackReceiveInfo msgTrackInfo = new MsgTrackReceiveInfo(null, null, null, sourceContext, connectorId, relatedMailItemId);
			MessageTrackingLog.TrackReceive(MessageTrackingSource.AGENT, (TransportMailItem)mailItem, null, msgTrackInfo);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00010F73 File Offset: 0x0000F173
		public static void TrackReceive(MessageTrackingSource source, TransportMailItem mailItem, MsgTrackReceiveInfo msgTrackInfo)
		{
			MessageTrackingLog.TrackReceive(source, mailItem, null, msgTrackInfo);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00010F80 File Offset: 0x0000F180
		public static void TrackReceive(MessageTrackingSource source, TransportMailItem mailItem, string messageId, MsgTrackReceiveInfo msgTrackInfo)
		{
			MessageTrackingLog.WriteLamEventNotification(mailItem, MessageTrackingEvent.RECEIVE);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			List<KeyValuePair<string, object>> list = null;
			if (!string.IsNullOrEmpty(msgTrackInfo.MailboxDatabaseGuid))
			{
				MessageTrackingLog.AddCustomData("MailboxDatabaseGuid", msgTrackInfo.MailboxDatabaseGuid, ref list);
			}
			if (msgTrackInfo.EntryId != null)
			{
				string value = BitConverter.ToString(msgTrackInfo.EntryId);
				MessageTrackingLog.AddCustomData("ItemEntryId", value, ref list);
			}
			if (msgTrackInfo.InvalidRecipients != null)
			{
				foreach (string value2 in msgTrackInfo.InvalidRecipients)
				{
					MessageTrackingLog.AddCustomData("InvalidRecipient", value2, ref list);
				}
			}
			Header header = mailItem.RootPart.Headers.FindFirst("X-MS-Exchange-Forest-ArrivalHubServer");
			if (header != null && !string.IsNullOrEmpty(header.Value))
			{
				MessageTrackingLog.AddCustomData("FirstForestHop", header.Value, ref list);
			}
			header = mailItem.RootPart.Headers.FindFirst("X-MS-Exchange-Organization-FFO-ServiceTag");
			if (header != null && !string.IsNullOrWhiteSpace(header.Value))
			{
				MessageTrackingLog.AddCustomData("ServiceTag", header.Value, ref list);
			}
			MessageTrackingLog.AddOriginatorOrganization(mailItem, ref list);
			MessageTrackingLog.AddProxiedClientDetails(msgTrackInfo.ProxiedClientIPAddress, msgTrackInfo.ProxiedClientHostname, ref list);
			if (msgTrackInfo.ReceivedHeaders != null)
			{
				int num = 1;
				for (int i = msgTrackInfo.ReceivedHeaders.Count - 1; i >= 0; i--)
				{
					ReceivedHeader receivedHeader = msgTrackInfo.ReceivedHeaders[i] as ReceivedHeader;
					if (receivedHeader != null && string.Equals(receivedHeader.Via, DataBdatHelpers.ViaFrontEndTransport, StringComparison.OrdinalIgnoreCase))
					{
						MessageTrackingLog.AddCustomData(string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[]
						{
							"ProxyHop",
							num
						}), string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[]
						{
							receivedHeader.By,
							receivedHeader.ByTcpInfo
						}), ref list);
						num++;
					}
				}
			}
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, mailItem, ref list, false);
			MessageTrackingLog.SetSubmittingSender(logRowFormatter, mailItem.MimeSender, msgTrackInfo.SubmittingMailboxSmtpAddress);
			MessageTrackingLog.SetPurportedSender(logRowFormatter, mailItem, logRowFormatter[19].ToString());
			MessageTrackingLog.SetSenderMailboxGuid(logRowFormatter, msgTrackInfo.AuthUserMailboxGuid);
			logRowFormatter[8] = MessageTrackingEvent.RECEIVE;
			logRowFormatter[7] = source;
			logRowFormatter[4] = MessageTrackingLog.hostName;
			logRowFormatter[5] = msgTrackInfo.SourceContext;
			logRowFormatter[1] = msgTrackInfo.ClientIPAddress;
			logRowFormatter[3] = msgTrackInfo.ServerIPAddress;
			logRowFormatter[2] = msgTrackInfo.ClientHostname;
			logRowFormatter[6] = msgTrackInfo.ConnectorId;
			List<string> list2 = new List<string>();
			list2.Add(msgTrackInfo.SecurityInfo);
			if (source == MessageTrackingSource.SMTP && !string.IsNullOrEmpty(msgTrackInfo.RelatedMessageInfo))
			{
				list2.Add(msgTrackInfo.RelatedMessageInfo);
				if (string.IsNullOrEmpty(msgTrackInfo.ClientHostname) && !msgTrackInfo.RelatedMessageInfo.Contains("NTS"))
				{
					logRowFormatter[2] = msgTrackInfo.ClientIPAddress;
				}
			}
			logRowFormatter[21] = MessageTrackingLog.FormatValues(list2.ToArray());
			MessageTrackingLog.SetOriginalIP(mailItem, logRowFormatter);
			if (msgTrackInfo.RelatedMailItemId != null)
			{
				logRowFormatter[17] = msgTrackInfo.RelatedMailItemId;
			}
			if (msgTrackInfo.RelatedMessageId != null)
			{
				logRowFormatter[17] = msgTrackInfo.RelatedMessageId;
			}
			if (!string.IsNullOrEmpty(messageId))
			{
				logRowFormatter[10] = messageId;
			}
			else if (mailItem.Message != null && mailItem.Message.MessageId != null)
			{
				logRowFormatter[10] = mailItem.Message.MessageId;
			}
			int num2 = (msgTrackInfo.InvalidRecipients != null) ? msgTrackInfo.InvalidRecipients.Count : 0;
			logRowFormatter[15] = mailItem.Recipients.Count + num2;
			logRowFormatter[12] = mailItem.Recipients.AllUnprocessed;
			if (source != MessageTrackingSource.SMTP)
			{
				List<string> list3 = new List<string>();
				foreach (MailRecipient mailRecipient in mailItem.Recipients.AllUnprocessed)
				{
					int num3;
					if (!mailRecipient.ExtendedProperties.TryGetValue<int>("Microsoft.Exchange.Transport.RecipientP2Type", out num3) || num3 >= MessageTrackingLog.recipientTypeStringValues.Length)
					{
						ExTraceGlobals.MessageTrackingTracer.TraceError<RoutingAddress, int>(0L, "Message recipient {0} had an unsupported RecipientP2TypeProperty of {1}.  Logging as \"Unknown\"", mailRecipient.Email, num3);
						num3 = 0;
					}
					list3.Add(MessageTrackingLog.recipientTypeStringValues[num3]);
				}
				logRowFormatter[13] = list3.ToArray();
			}
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00011418 File Offset: 0x0000F618
		public static void TrackNotify(MsgTrackMapiSubmitInfo msgTrackInfo, bool isShadowSubmission)
		{
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			logRowFormatter[7] = MessageTrackingSource.STOREDRIVER;
			logRowFormatter[21] = msgTrackInfo.LatencyData;
			List<KeyValuePair<string, object>> properties = null;
			MessageTrackingLog.TrackCommonMsgTrackMapiSubmitInfo(msgTrackInfo, logRowFormatter, ref properties);
			if (!string.IsNullOrEmpty(msgTrackInfo.DiagnosticInfo))
			{
				MessageTrackingLog.AddCustomData("DiagnosticInfo", msgTrackInfo.DiagnosticInfo, ref properties);
			}
			MessageTrackingLog.AppendCustomDataList(logRowFormatter, properties);
			logRowFormatter[8] = (isShadowSubmission ? MessageTrackingEvent.NOTIFYSHADOW : MessageTrackingEvent.NOTIFYMAPI);
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000114A0 File Offset: 0x0000F6A0
		public static void TrackMapiSubmit(MsgTrackMapiSubmitInfo msgTrackInfo)
		{
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			MessageTrackingEvent messageTrackingEvent;
			if (msgTrackInfo.Failed)
			{
				if (msgTrackInfo.IsPermanentFailure)
				{
					messageTrackingEvent = (msgTrackInfo.IsRegularSubmission ? MessageTrackingEvent.SUBMITFAIL : MessageTrackingEvent.RESUBMITFAIL);
				}
				else
				{
					messageTrackingEvent = (msgTrackInfo.IsRegularSubmission ? MessageTrackingEvent.SUBMITDEFER : MessageTrackingEvent.RESUBMITDEFER);
				}
			}
			else
			{
				messageTrackingEvent = (msgTrackInfo.IsRegularSubmission ? MessageTrackingEvent.SUBMIT : MessageTrackingEvent.RESUBMIT);
			}
			logRowFormatter[8] = messageTrackingEvent;
			logRowFormatter[7] = MessageTrackingSource.STOREDRIVER;
			List<KeyValuePair<string, object>> list = null;
			MessageTrackingLog.TrackCommonMsgTrackMapiSubmitInfo(msgTrackInfo, logRowFormatter, ref list);
			if (!string.IsNullOrEmpty(msgTrackInfo.DiagnosticInfo))
			{
				MessageTrackingLog.AddCustomData("DiagnosticInfo", msgTrackInfo.DiagnosticInfo, ref list);
			}
			if (!string.IsNullOrEmpty(msgTrackInfo.Sender))
			{
				logRowFormatter[19] = msgTrackInfo.Sender;
			}
			if (!string.IsNullOrEmpty(msgTrackInfo.MessageId))
			{
				logRowFormatter[10] = msgTrackInfo.MessageId;
			}
			if (!string.IsNullOrEmpty(msgTrackInfo.LatencyData))
			{
				logRowFormatter[21] = msgTrackInfo.LatencyData;
			}
			if (!msgTrackInfo.Failed)
			{
				logRowFormatter[18] = msgTrackInfo.Subject;
				if (!string.IsNullOrEmpty(msgTrackInfo.From))
				{
					MessageTrackingLog.SetPurportedSender(logRowFormatter, msgTrackInfo.From, msgTrackInfo.Sender);
				}
			}
			if (msgTrackInfo.DirectionIsSet)
			{
				logRowFormatter[22] = msgTrackInfo.Direction;
			}
			if (list != null && list.Count > 0)
			{
				MessageTrackingLog.AppendCustomDataList(logRowFormatter, list);
			}
			string value;
			logRowFormatter[23] = MessageTrackingLog.GetExternalOrganizationIdToLog(msgTrackInfo.ExternalOrganizationId, msgTrackInfo.OrganizationId, out value);
			MessageTrackingLog.AppendCustomData(logRowFormatter, "ExternalOrgIdNotSetReason", value);
			if (msgTrackInfo.RecipientAddresses != null && msgTrackInfo.RecipientAddresses.Length > 0)
			{
				logRowFormatter[15] = msgTrackInfo.RecipientAddresses.Length;
				logRowFormatter[12] = msgTrackInfo.RecipientAddresses;
			}
			if (msgTrackInfo.NetworkMessageId != Guid.Empty)
			{
				logRowFormatter[11] = msgTrackInfo.NetworkMessageId;
			}
			logRowFormatter[24] = msgTrackInfo.OriginalClientIPAddress;
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00011690 File Offset: 0x0000F890
		public static void TrackResubmitCancelled(MessageTrackingSource source, IReadOnlyMailItem mailItem, ICollection recipients, SmtpResponse response, LatencyFormatter latencyFormatter)
		{
			MessageTrackingLog.WriteLamEventNotification(mailItem, MessageTrackingEvent.SUPPRESSED);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			logRowFormatter[8] = MessageTrackingEvent.SUPPRESSED;
			logRowFormatter[7] = source;
			logRowFormatter[2] = MessageTrackingLog.hostName;
			logRowFormatter[15] = recipients.Count;
			logRowFormatter[12] = recipients;
			logRowFormatter[5] = MessageTrackingLog.GetMessageTrackingString(response);
			List<KeyValuePair<string, object>> list = null;
			MessageTrackingLog.SetLatencyFormatter(logRowFormatter, latencyFormatter, ref list);
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, mailItem, ref list, false);
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00011724 File Offset: 0x0000F924
		public static void TrackDuplicateDelivery(MessageTrackingSource source, IReadOnlyMailItem mailItem, ICollection recipients, ICollection recipientsData, string clientHostname, string serverName, LatencyFormatter latencyFormatter, string sourceContext, List<KeyValuePair<string, string>> extraEventData)
		{
			MessageTrackingLog.TrackDeliveryEvent(source, MessageTrackingEvent.DUPLICATEDELIVER, mailItem, recipients, recipientsData, clientHostname, serverName, latencyFormatter, sourceContext, extraEventData);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00011748 File Offset: 0x0000F948
		public static void TrackDelivered(MessageTrackingSource source, IReadOnlyMailItem mailItem, ICollection recipients, ICollection recipientsData, string clientHostname, string serverName, LatencyFormatter latencyFormatter, string sourceContext, List<KeyValuePair<string, string>> extraEventData)
		{
			MessageTrackingLog.TrackDeliveryEvent(source, MessageTrackingEvent.DELIVER, mailItem, recipients, recipientsData, clientHostname, serverName, latencyFormatter, sourceContext, extraEventData);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001176C File Offset: 0x0000F96C
		public static void TrackExpiredMessageDropped(MessageTrackingSource source, IReadOnlyMailItem mailItem, ICollection recipients, SmtpResponse response)
		{
			MessageTrackingLog.WriteLamEventNotification(mailItem, MessageTrackingEvent.FAIL);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, mailItem, false);
			logRowFormatter[8] = MessageTrackingEvent.FAIL;
			logRowFormatter[7] = source;
			logRowFormatter[2] = MessageTrackingLog.hostName;
			logRowFormatter[15] = recipients.Count;
			logRowFormatter[12] = recipients;
			logRowFormatter[5] = MessageTrackingLog.GetMessageTrackingString(response);
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000117F0 File Offset: 0x0000F9F0
		public static void TrackProcessed(MessageTrackingSource source, IReadOnlyMailItem mailItem, ICollection recipients, ICollection recipientsSource, LatencyFormatter latencyFormatter, string sourceContext, List<KeyValuePair<string, string>> extraEventData)
		{
			MessageTrackingLog.WriteLamEventNotification(mailItem, MessageTrackingEvent.PROCESS);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			List<KeyValuePair<string, object>> list = null;
			MessageTrackingLog.AddCustomDataList(extraEventData, ref list);
			MessageTrackingLog.SetLatencyFormatter(logRowFormatter, latencyFormatter, ref list);
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, mailItem, ref list, false, false);
			logRowFormatter[8] = MessageTrackingEvent.PROCESS;
			logRowFormatter[7] = source;
			logRowFormatter[2] = MessageTrackingLog.hostName;
			logRowFormatter[15] = recipients.Count;
			logRowFormatter[12] = recipients;
			logRowFormatter[13] = recipientsSource;
			logRowFormatter[5] = sourceContext;
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00011894 File Offset: 0x0000FA94
		public static void TrackRecipientAdd(MessageTrackingSource source, TransportMailItem mailItem, RoutingAddress recipEmail, RecipientP2Type? recipientType, string agentName)
		{
			List<KeyValuePair<string, object>> extraEventData = null;
			if (recipientType != null)
			{
				int num = (int)recipientType.Value;
				if (num > MessageTrackingLog.recipientTypeStringValues.Length)
				{
					ExTraceGlobals.MessageTrackingTracer.TraceError<RoutingAddress, int>(0L, "TrackRecipientAdd is adding '{0}' with an unsupported RecipientP2Type of {1}.  Logging as \"Unknown\"", recipEmail, num);
					num = 0;
				}
				MessageTrackingLog.AddCustomData("RecipientType", MessageTrackingLog.recipientTypeStringValues[num], ref extraEventData);
			}
			MessageTrackingLog.TrackRecipient(source, mailItem, recipEmail, null, MessageTrackingEvent.RECEIVE, agentName, extraEventData, null);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000118FC File Offset: 0x0000FAFC
		public static void TrackRecipientAddByAgent(ITransportMailItemFacade mailItem, string recipEmail, RecipientP2Type recipientType, string agentName)
		{
			MessageTrackingLog.TrackRecipientAdd(MessageTrackingSource.AGENT, (TransportMailItem)mailItem, new RoutingAddress(recipEmail), new RecipientP2Type?(recipientType), agentName);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00011918 File Offset: 0x0000FB18
		public static void TrackFailedRecipients(MessageTrackingSource source, string sourceContext, IReadOnlyMailItem mailItem, string relatedRecipientAddress, ICollection<MailRecipient> recipients, SmtpResponse smtpResponse, LatencyFormatter latencyFormatter)
		{
			MessageTrackingLog.WriteLamEventNotification(mailItem, MessageTrackingEvent.FAIL);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			logRowFormatter[8] = MessageTrackingEvent.FAIL;
			logRowFormatter[7] = source;
			logRowFormatter[5] = sourceContext;
			logRowFormatter[4] = MessageTrackingLog.hostName;
			logRowFormatter[16] = relatedRecipientAddress;
			List<KeyValuePair<string, object>> list = null;
			MessageTrackingLog.SetLatencyFormatter(logRowFormatter, latencyFormatter, ref list);
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, mailItem, ref list, false);
			if (recipients == null || recipients.Count == 0)
			{
				logRowFormatter[15] = 0;
				MessageTrackingLog.Append(logRowFormatter);
				return;
			}
			logRowFormatter[15] = recipients.Count;
			logRowFormatter[12] = recipients;
			string text = MessageTrackingLog.RemoveCRLFs(smtpResponse.ToString());
			string[] array = new string[recipients.Count];
			for (int i = 0; i < recipients.Count; i++)
			{
				array[i] = text;
			}
			logRowFormatter[13] = array;
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00011A16 File Offset: 0x0000FC16
		public static void TrackRecipientFail(MessageTrackingSource source, TransportMailItem mailItem, RoutingAddress recipEmail, SmtpResponse smtpResponse, string sourceContext, LatencyFormatter latencyFormatter)
		{
			MessageTrackingLog.TrackRecipient(source, mailItem, recipEmail, new SmtpResponse?(smtpResponse), MessageTrackingEvent.FAIL, sourceContext, null, latencyFormatter);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00011A2C File Offset: 0x0000FC2C
		public static void TrackRecipientDrop(MessageTrackingSource source, TransportMailItem mailItem, RoutingAddress recipEmail, SmtpResponse smtpResponse, string sourceContext, LatencyFormatter latencyFormatter)
		{
			MessageTrackingLog.TrackRecipient(source, mailItem, recipEmail, new SmtpResponse?(smtpResponse), MessageTrackingEvent.DROP, sourceContext, null, latencyFormatter);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00011A44 File Offset: 0x0000FC44
		public static void TrackAgentInfo(TransportMailItem mailItem)
		{
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			if (mailItem == null)
			{
				return;
			}
			List<List<KeyValuePair<string, string>>> list = mailItem.ClaimAgentInfo();
			if (list == null)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			List<KeyValuePair<string, object>> agentInfoString = LoggingFormatter.GetAgentInfoString(list);
			MessageTrackingLog.WriteLamAgentInfoEventNotification(mailItem, agentInfoString);
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, mailItem, ref agentInfoString, false);
			logRowFormatter[2] = MessageTrackingLog.hostName;
			logRowFormatter[7] = MessageTrackingSource.AGENT;
			logRowFormatter[8] = MessageTrackingEvent.AGENTINFO;
			logRowFormatter[15] = mailItem.Recipients.Count;
			logRowFormatter[12] = mailItem.Recipients;
			MessageTrackingLog.SetOriginalIP(mailItem, logRowFormatter);
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00011AE8 File Offset: 0x0000FCE8
		public static void TrackPoisonMessageDeleted(MessageTrackingSource source, string sourceContext, IReadOnlyMailItem item)
		{
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, item, false);
			logRowFormatter[7] = source;
			logRowFormatter[2] = MessageTrackingLog.hostName;
			if (sourceContext != null)
			{
				logRowFormatter[5] = sourceContext;
			}
			List<KeyValuePair<string, object>> list = null;
			MessageTrackingLog.TrackRecipientStatus(logRowFormatter, source, item, null, item.Recipients, SmtpResponse.Empty, null, ref list);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00011B50 File Offset: 0x0000FD50
		public static void TrackRejectCommand(MessageTrackingSource source, string sourceContext, AckDetails ackDetails, SmtpResponse smtpResponse)
		{
			if (!MessageTrackingLog.enabled || ackDetails == null)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			List<KeyValuePair<string, object>> list = null;
			if (sourceContext == null)
			{
				sourceContext = ackDetails.SessionId;
			}
			logRowFormatter[1] = ackDetails.SourceIPAddress;
			if (ackDetails.RemoteEndPoint != null)
			{
				logRowFormatter[3] = ackDetails.RemoteEndPoint.Address;
			}
			logRowFormatter[4] = ackDetails.RemoteHostName;
			logRowFormatter[6] = ackDetails.ConnectorId;
			if (ackDetails.ExtraEventData != null)
			{
				MessageTrackingLog.AddCustomDataList(ackDetails.ExtraEventData, ref list);
				MessageTrackingLog.UpdateLogFormatter(logRowFormatter, null, ref list, false, true);
			}
			logRowFormatter[7] = source;
			logRowFormatter[8] = MessageTrackingEvent.FAIL;
			logRowFormatter[2] = MessageTrackingLog.hostName;
			logRowFormatter[5] = sourceContext;
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00011C15 File Offset: 0x0000FE15
		public static void TrackRelayedAndFailed(MessageTrackingSource source, IReadOnlyMailItem mailItem, IEnumerable<MailRecipient> recipients, AckDetails ackDetails)
		{
			MessageTrackingLog.TrackRelayedAndFailed(source, null, mailItem, recipients, ackDetails, SmtpResponse.Empty, null);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00011C28 File Offset: 0x0000FE28
		public static void TrackRelayedAndFailed(MessageTrackingSource source, string sourceContext, IReadOnlyMailItem mailItem, IEnumerable<MailRecipient> recipients, AckDetails ackDetails, SmtpResponse smtpResponse, LatencyFormatter latencyFormatter)
		{
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			string sourceContext2 = null;
			List<KeyValuePair<string, object>> list = null;
			if (ackDetails != null)
			{
				sourceContext2 = ackDetails.SessionId;
				logRowFormatter[1] = ackDetails.SourceIPAddress;
				if (ackDetails.RemoteEndPoint != null)
				{
					logRowFormatter[3] = ackDetails.RemoteEndPoint.Address;
				}
				logRowFormatter[4] = ackDetails.RemoteHostName;
				logRowFormatter[6] = ackDetails.ConnectorId;
				MessageTrackingLog.AddCustomDataList(ackDetails.ExtraEventData, ref list);
			}
			if (sourceContext != null)
			{
				sourceContext2 = sourceContext;
			}
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, mailItem, ref list, false, true);
			logRowFormatter[7] = source;
			logRowFormatter[2] = MessageTrackingLog.hostName;
			MessageTrackingLog.TrackRecipientStatus(logRowFormatter, source, mailItem, sourceContext2, recipients, smtpResponse, latencyFormatter, ref list);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00011CE8 File Offset: 0x0000FEE8
		public static void TrackDSN(TransportMailItem dsnMailItem, MsgTrackDSNInfo dsnInfo)
		{
			MessageTrackingLog.WriteLamEventNotification(dsnMailItem, MessageTrackingEvent.DSN);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			logRowFormatter[7] = MessageTrackingSource.DSN;
			logRowFormatter[4] = MessageTrackingLog.hostName;
			logRowFormatter[8] = MessageTrackingEvent.DSN;
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, dsnMailItem, false);
			logRowFormatter[15] = dsnMailItem.Recipients.Count;
			logRowFormatter[17] = dsnInfo.OrigMessageID;
			logRowFormatter[5] = dsnInfo.DsnType;
			logRowFormatter[12] = dsnMailItem.Recipients.AllUnprocessed.ToArray<MailRecipient>();
			if (!string.IsNullOrEmpty(dsnInfo.OriginalDsnSender))
			{
				MessageTrackingLog.AppendCustomData(logRowFormatter, "OriginalDsnSender", dsnInfo.OriginalDsnSender);
			}
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00011DB4 File Offset: 0x0000FFB4
		public static void TrackAgentGeneratedMessageRejected(MessageTrackingSource source, bool loopDetectionEnabled, IReadOnlyMailItem mailItem)
		{
			MessageTrackingLog.WriteLamEventNotification(mailItem, MessageTrackingEvent.BADMAIL);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			logRowFormatter[8] = (loopDetectionEnabled ? MessageTrackingEvent.BADMAIL : MessageTrackingEvent.AGENTINFO);
			logRowFormatter[7] = source;
			logRowFormatter[5] = "agent generated message loop detected";
			logRowFormatter[4] = MessageTrackingLog.hostName;
			Header header = mailItem.RootPart.Headers.FindFirst("X-MS-Exchange-Parent-Message-Id");
			string value = (header == null) ? string.Empty : header.Value;
			Header header2 = mailItem.RootPart.Headers.FindFirst("X-MS-Exchange-Generated-Message-Source");
			string value2 = (header2 == null) ? string.Empty : header2.Value;
			List<KeyValuePair<string, object>> list = null;
			MessageTrackingLog.AddCustomData("GeneratedMessageParentMessageId", value, ref list);
			MessageTrackingLog.AddCustomData("GeneratedMessageSource", value2, ref list);
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, mailItem, ref list, false);
			if (mailItem != null)
			{
				logRowFormatter[15] = mailItem.Recipients.Count;
				logRowFormatter[12] = mailItem.Recipients;
				logRowFormatter[13] = SmtpResponse.AgentGeneratedMessageDepthExceeded;
			}
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00011ECC File Offset: 0x000100CC
		public static void TrackBadmail(MessageTrackingSource source, MsgTrackReceiveInfo msgTrackInfo, IReadOnlyMailItem mailItem, string badmailReason)
		{
			MessageTrackingLog.WriteLamEventNotification(mailItem, MessageTrackingEvent.BADMAIL);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			logRowFormatter[8] = MessageTrackingEvent.BADMAIL;
			logRowFormatter[7] = source;
			logRowFormatter[4] = MessageTrackingLog.hostName;
			if (mailItem != null)
			{
				MessageTrackingLog.UpdateLogFormatter(logRowFormatter, mailItem, false);
				logRowFormatter[15] = mailItem.Recipients.Count;
				logRowFormatter[12] = mailItem.Recipients;
			}
			if (msgTrackInfo != null)
			{
				logRowFormatter[5] = msgTrackInfo.SourceContext;
				logRowFormatter[1] = msgTrackInfo.ClientIPAddress;
				logRowFormatter[3] = msgTrackInfo.ServerIPAddress;
				logRowFormatter[2] = msgTrackInfo.ClientHostname;
				logRowFormatter[6] = msgTrackInfo.ConnectorId;
			}
			if (!string.IsNullOrEmpty(badmailReason))
			{
				List<KeyValuePair<string, object>> properties = null;
				MessageTrackingLog.AddCustomData("BadmailReason", badmailReason, ref properties);
				MessageTrackingLog.AppendCustomDataList(logRowFormatter, properties);
			}
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00011FB2 File Offset: 0x000101B2
		public static void TrackDefer(MessageTrackingSource source, IReadOnlyMailItem mailItem, string sourceContext)
		{
			MessageTrackingLog.TrackDefer(source, sourceContext, mailItem, null, null);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00011FC8 File Offset: 0x000101C8
		public static void TrackDefer(MessageTrackingSource source, string messageTrackingSourceContext, IReadOnlyMailItem mailItem, IEnumerable<MailRecipient> recipientsToTrack, AckDetails ackDetails)
		{
			MessageTrackingLog.WriteLamEventNotification(mailItem, MessageTrackingEvent.DEFER);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			List<KeyValuePair<string, object>> list = null;
			if (ackDetails != null)
			{
				logRowFormatter[1] = ackDetails.SourceIPAddress;
				if (ackDetails.RemoteEndPoint != null)
				{
					logRowFormatter[3] = ackDetails.RemoteEndPoint.Address;
				}
				logRowFormatter[4] = ackDetails.RemoteHostName;
				logRowFormatter[6] = ackDetails.ConnectorId;
				MessageTrackingLog.AddCustomDataList(ackDetails.ExtraEventData, ref list);
			}
			logRowFormatter[8] = MessageTrackingEvent.DEFER;
			logRowFormatter[7] = source;
			logRowFormatter[2] = MessageTrackingLog.hostName;
			bool flag;
			if (source == MessageTrackingSource.SMTP && recipientsToTrack != null)
			{
				flag = recipientsToTrack.All((MailRecipient r) => r.SmtpDeferLogged);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (flag2)
			{
				MessageTrackingLog.AddCustomData("RecipientsAlreadySmtpDeferLogged", "true", ref list);
			}
			if (mailItem != null)
			{
				IEnumerable<MailRecipient> enumerable;
				if (recipientsToTrack != null)
				{
					enumerable = recipientsToTrack;
				}
				else
				{
					List<MailRecipient> list2 = new List<MailRecipient>();
					foreach (MailRecipient mailRecipient in mailItem.Recipients)
					{
						if (!mailRecipient.IsProcessed)
						{
							list2.Add(mailRecipient);
						}
					}
					enumerable = list2;
				}
				List<string> list3 = new List<string>();
				List<string> list4 = new List<string>();
				List<string> list5 = new List<string>();
				foreach (MailRecipient mailRecipient2 in enumerable)
				{
					list4.Add(mailRecipient2.Email.ToString());
					string text = mailRecipient2.SmtpResponse.ToString();
					string text2 = null;
					if (mailRecipient2.ExtendedProperties.TryGetValue<string>("Microsoft.Exchange.Transport.RecipientDiagnosticInfo", out text2))
					{
						text = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
						{
							text,
							text2
						});
					}
					list3.Add(MessageTrackingLog.RemoveCRLFs(text));
					if (source == MessageTrackingSource.SMTP || source == MessageTrackingSource.QUEUE)
					{
						MessageTrackingLog.AddTenantConnectorInfo(mailRecipient2.ExtendedProperties, ref list5);
					}
				}
				logRowFormatter[15] = list4.Count;
				logRowFormatter[12] = list4.ToArray();
				logRowFormatter[13] = list3.ToArray();
				if (list5.Any<string>())
				{
					MessageTrackingLog.SetTenantConnectorUsage(ref list, list5);
				}
			}
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, mailItem, ref list, false);
			if (messageTrackingSourceContext != null)
			{
				logRowFormatter[5] = messageTrackingSourceContext;
			}
			else if (mailItem.DeferReason != DeferReason.None)
			{
				logRowFormatter[5] = mailItem.DeferReason.ToString();
			}
			logRowFormatter[21] = ((IQueueItem)mailItem).DeferUntil.ToUniversalTime().ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffZ", DateTimeFormatInfo.InvariantInfo);
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000122B4 File Offset: 0x000104B4
		public static void TrackThrottle(MessageTrackingSource source, MsgTrackMapiSubmitInfo msgTrackInfo, string senderAddress, string messageId)
		{
			MessageTrackingLog.TrackThrottleHelper(source, msgTrackInfo, senderAddress, messageId, null);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000122D3 File Offset: 0x000104D3
		public static void TrackThrottle(MessageTrackingSource source, MsgTrackMapiSubmitInfo msgTrackInfo, string senderAddress, string messageId, MailDirectionality direction)
		{
			MessageTrackingLog.TrackThrottleHelper(source, msgTrackInfo, senderAddress, messageId, new MailDirectionality?(direction));
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000122E8 File Offset: 0x000104E8
		public static void TrackThrottle(MessageTrackingSource source, IReadOnlyMailItem mailItem, IPAddress serverIPAddress, string sourceContext, string reference, ProxyAddress recipient, string recipientData)
		{
			MessageTrackingLog.WriteLamEventNotification(mailItem, MessageTrackingEvent.THROTTLE);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			logRowFormatter[8] = MessageTrackingEvent.THROTTLE;
			logRowFormatter[7] = source;
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, mailItem, false);
			logRowFormatter[3] = serverIPAddress;
			logRowFormatter[4] = MessageTrackingLog.hostName;
			logRowFormatter[5] = sourceContext;
			logRowFormatter[17] = reference;
			logRowFormatter[12] = recipient;
			logRowFormatter[13] = recipientData;
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00012374 File Offset: 0x00010574
		public static void TrackResolve(MessageTrackingSource source, IReadOnlyMailItem mailItem, MsgTrackResolveInfo msgTrackInfo)
		{
			MessageTrackingLog.WriteLamEventNotification(mailItem, MessageTrackingEvent.RESOLVE);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			logRowFormatter[8] = MessageTrackingEvent.RESOLVE;
			logRowFormatter[7] = source;
			logRowFormatter[4] = MessageTrackingLog.hostName;
			logRowFormatter[5] = msgTrackInfo.SourceContext;
			logRowFormatter[12] = msgTrackInfo.ResolvedAddress;
			logRowFormatter[16] = msgTrackInfo.OriginalAddress;
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, mailItem, false);
			logRowFormatter[15] = 1;
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00012410 File Offset: 0x00010610
		public static void TrackExpand<T>(MessageTrackingSource source, IReadOnlyMailItem mailItem, MsgTrackExpandInfo msgTrackInfo, ICollection<T> recipients)
		{
			MessageTrackingLog.TrackExpandEvent<T>(source, mailItem, msgTrackInfo, recipients, MessageTrackingEvent.EXPAND);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0001241C File Offset: 0x0001061C
		public static void TrackExpandEvent<T>(MessageTrackingSource source, IReadOnlyMailItem mailItem, MsgTrackExpandInfo msgTrackInfo, ICollection<T> recipients, MessageTrackingEvent trackingEvent)
		{
			MessageTrackingLog.WriteLamEventNotification(mailItem, trackingEvent);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			logRowFormatter[8] = trackingEvent;
			logRowFormatter[7] = source;
			logRowFormatter[4] = MessageTrackingLog.hostName;
			logRowFormatter[5] = msgTrackInfo.SourceContext;
			logRowFormatter[16] = msgTrackInfo.GroupAddress;
			if (msgTrackInfo.RelatedMailItemId != null)
			{
				logRowFormatter[17] = msgTrackInfo.RelatedMailItemId;
			}
			string[] value = new string[]
			{
				MessageTrackingLog.RemoveCRLFs(msgTrackInfo.StatusText)
			};
			logRowFormatter[13] = value;
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, mailItem, false);
			if (recipients == null || recipients.Count == 0)
			{
				logRowFormatter[15] = 0;
				MessageTrackingLog.Append(logRowFormatter);
				return;
			}
			logRowFormatter[15] = recipients.Count;
			logRowFormatter[12] = recipients;
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00012518 File Offset: 0x00010718
		public static void TrackRedirect(MessageTrackingSource source, IReadOnlyMailItem mailItem, MsgTrackRedirectInfo msgTrackInfo)
		{
			MessageTrackingLog.TrackRedirectEvent(source, mailItem, msgTrackInfo, MessageTrackingEvent.REDIRECT);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00012524 File Offset: 0x00010724
		public static void TrackRedirectEvent(MessageTrackingSource source, IReadOnlyMailItem mailItem, MsgTrackRedirectInfo msgTrackInfo, MessageTrackingEvent redirectEvent)
		{
			MessageTrackingLog.WriteLamEventNotification(mailItem, redirectEvent);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			logRowFormatter[8] = redirectEvent;
			logRowFormatter[7] = source;
			logRowFormatter[4] = MessageTrackingLog.hostName;
			logRowFormatter[5] = msgTrackInfo.SourceContext;
			logRowFormatter[12] = msgTrackInfo.OriginalAddress;
			logRowFormatter[16] = msgTrackInfo.RedirectedAddress;
			if (msgTrackInfo.RelatedMailItemId != null)
			{
				logRowFormatter[17] = msgTrackInfo.RelatedMailItemId;
			}
			if (msgTrackInfo.Response != null)
			{
				logRowFormatter[13] = msgTrackInfo.Response.ToString();
			}
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, mailItem, false);
			logRowFormatter[15] = 1;
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00012610 File Offset: 0x00010810
		public static void TrackRedirectToDomain(MessageTrackingSource source, IReadOnlyMailItem mailItem, MsgTrackRedirectInfo msgTrackInfo, MailRecipient mailRecipient)
		{
			MessageTrackingLog.WriteLamEventNotification(mailItem, MessageTrackingEvent.REDIRECT);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			logRowFormatter[8] = MessageTrackingEvent.REDIRECT;
			logRowFormatter[7] = source;
			logRowFormatter[4] = MessageTrackingLog.hostName;
			logRowFormatter[5] = msgTrackInfo.SourceContext;
			logRowFormatter[12] = msgTrackInfo.OriginalAddress;
			if (msgTrackInfo.RelatedMailItemId != null)
			{
				logRowFormatter[17] = msgTrackInfo.RelatedMailItemId;
			}
			if (msgTrackInfo.Response != null)
			{
				logRowFormatter[13] = msgTrackInfo.Response.ToString();
			}
			logRowFormatter[15] = 1;
			List<KeyValuePair<string, object>> list = null;
			MessageTrackingLog.AddCustomData("ConnectorRedirect", string.Concat(new object[]
			{
				msgTrackInfo.RedirectedConnectorDomain,
				"(",
				msgTrackInfo.RedirectedDeliveryDestination,
				")"
			}), ref list);
			string value;
			if (mailRecipient != null && MessageTrackingLog.TryGetTenantOutboundConnectorProperties(mailRecipient.ExtendedProperties, out value))
			{
				MessageTrackingLog.AddCustomData("Microsoft.Exchange.Hygiene.TenantOutboundConnectorCustomData", value, ref list);
			}
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, mailItem, ref list, false);
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00012754 File Offset: 0x00010954
		public static void TrackTransfer(MessageTrackingSource source, IReadOnlyMailItem mailItem, long relatedMailItemId, string sourceContext)
		{
			MessageTrackingLog.WriteLamEventNotification(mailItem, MessageTrackingEvent.TRANSFER);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			logRowFormatter[8] = MessageTrackingEvent.TRANSFER;
			logRowFormatter[7] = source;
			logRowFormatter[4] = MessageTrackingLog.hostName;
			logRowFormatter[5] = sourceContext;
			logRowFormatter[17] = relatedMailItemId;
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, mailItem, false);
			logRowFormatter[15] = mailItem.Recipients.Count;
			logRowFormatter[12] = mailItem.Recipients;
			string[] array = new string[mailItem.Recipients.Count];
			int num = 0;
			foreach (MailRecipient mailRecipient in mailItem.Recipients)
			{
				array[num++] = MessageTrackingLog.RemoveCRLFs(mailRecipient.SmtpResponse.ToString());
			}
			logRowFormatter[13] = array;
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00012868 File Offset: 0x00010A68
		public static void TrackHighAvailabilityRedirect(MessageTrackingSource source, IReadOnlyMailItem mailItem, ICollection<MailRecipient> redirectedRecipients, string sourceContext)
		{
			MessageTrackingLog.WriteLamEventNotification(mailItem, MessageTrackingEvent.HAREDIRECT);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = MessageTrackingLog.CreateDefaultLogRow(MessageTrackingEvent.HAREDIRECT, source, mailItem, false);
			logRowFormatter[5] = sourceContext;
			logRowFormatter[15] = redirectedRecipients.Count;
			logRowFormatter[12] = redirectedRecipients;
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000128BC File Offset: 0x00010ABC
		public static void TrackHighAvailabilityRedirect(MessageTrackingSource source, IReadOnlyMailItem mailItem, string sourceContext)
		{
			MessageTrackingLog.WriteLamEventNotification(mailItem, MessageTrackingEvent.HAREDIRECT);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = MessageTrackingLog.CreateDefaultLogRow(MessageTrackingEvent.HAREDIRECT, source, mailItem, true);
			logRowFormatter[5] = sourceContext;
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000128F4 File Offset: 0x00010AF4
		public static void TrackHighAvailabilityRedirectFail(MessageTrackingSource source, IReadOnlyMailItem mailItem, string sourceContext)
		{
			MessageTrackingLog.WriteLamEventNotification(mailItem, MessageTrackingEvent.HAREDIRECTFAIL);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = MessageTrackingLog.CreateDefaultLogRow(MessageTrackingEvent.HAREDIRECTFAIL, source, mailItem, true);
			logRowFormatter[5] = sourceContext;
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001292C File Offset: 0x00010B2C
		public static void TrackHighAvailabilityReceive(MessageTrackingSource source, string primaryServerFqdn, IReadOnlyMailItem mailItem)
		{
			MessageTrackingLog.WriteLamEventNotification(mailItem, MessageTrackingEvent.HARECEIVE);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = MessageTrackingLog.CreateDefaultLogRow(MessageTrackingEvent.HARECEIVE, source, mailItem, true);
			logRowFormatter[2] = primaryServerFqdn;
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00012964 File Offset: 0x00010B64
		public static void TrackHighAvailabilityDiscard(MessageTrackingSource source, IReadOnlyMailItem mailItem, string reason)
		{
			MessageTrackingLog.WriteLamEventNotification(mailItem, MessageTrackingEvent.HADISCARD);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = MessageTrackingLog.CreateDefaultLogRow(MessageTrackingEvent.HADISCARD, source, mailItem, true);
			logRowFormatter[5] = reason;
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000129A4 File Offset: 0x00010BA4
		public static void TrackResubmit(MessageTrackingSource source, TransportMailItem newMailItem, IReadOnlyMailItem originalMailItem, string sourceContext)
		{
			MessageTrackingLog.WriteLamEventNotification(newMailItem, MessageTrackingEvent.RESUBMIT);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			logRowFormatter[8] = MessageTrackingEvent.RESUBMIT;
			logRowFormatter[7] = source;
			logRowFormatter[4] = MessageTrackingLog.hostName;
			logRowFormatter[9] = newMailItem.RecordId;
			logRowFormatter[10] = originalMailItem.InternetMessageId;
			logRowFormatter[11] = originalMailItem.NetworkMessageId;
			logRowFormatter[20] = newMailItem.From;
			logRowFormatter[18] = MessageTrackingLog.GetSubjectToLog(newMailItem.Subject);
			logRowFormatter[19] = newMailItem.MimeSender;
			logRowFormatter[14] = newMailItem.MimeSize;
			logRowFormatter[5] = sourceContext;
			logRowFormatter[17] = originalMailItem.RecordId;
			logRowFormatter[12] = newMailItem.Recipients;
			logRowFormatter[15] = newMailItem.Recipients.Count;
			logRowFormatter[13] = from r in newMailItem.Recipients
			select r.SmtpResponse;
			string value;
			logRowFormatter[23] = MessageTrackingLog.GetExternalOrganizationIdToLog(newMailItem.ExternalOrganizationId, newMailItem.OrganizationId, out value);
			MessageTrackingLog.AppendCustomData(logRowFormatter, "ExternalOrgIdNotSetReason", value);
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00012B14 File Offset: 0x00010D14
		public static void TrackInitMessageCreated(MessageTrackingSource source, ICollection<MailRecipient> moderatedRecipients, IReadOnlyMailItem originalMailItem, TransportMailItem initiationMailItem, string initiationMessageIdentifier, string sourceContext)
		{
			MessageTrackingLog.WriteLamEventNotification(initiationMailItem, MessageTrackingEvent.INITMESSAGECREATED);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, originalMailItem, false);
			logRowFormatter[8] = MessageTrackingEvent.INITMESSAGECREATED;
			logRowFormatter[7] = source;
			logRowFormatter[5] = sourceContext;
			logRowFormatter[17] = ((!string.IsNullOrEmpty(initiationMessageIdentifier)) ? new string[]
			{
				initiationMessageIdentifier,
				initiationMailItem.InternetMessageId
			} : new string[]
			{
				initiationMailItem.InternetMessageId
			});
			logRowFormatter[15] = moderatedRecipients.Count;
			logRowFormatter[12] = moderatedRecipients;
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00012BC4 File Offset: 0x00010DC4
		public static void TrackModeratorsAllNdr(MessageTrackingSource source, string initiationMessageId, string originalMessageId, string originalSenderAddress, ICollection<string> recipientAddresses, OrganizationId organizationId)
		{
			MessageTrackingLog.TrackModeratorDecisionNdrExpired(source, initiationMessageId, originalMessageId, originalSenderAddress, recipientAddresses, MessageTrackingEvent.MODERATORSALLNDR, organizationId, MessageTrackingLog.GetExternalOrganizationId(organizationId), false);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00012BE8 File Offset: 0x00010DE8
		public static void TrackModeratorExpired(MessageTrackingSource source, string initiationMessageId, string originalMessageId, string originalSenderAddress, ICollection<string> recipientAddresses, OrganizationId organizationId, bool isNotificationSent)
		{
			MessageTrackingLog.TrackModeratorDecisionNdrExpired(source, initiationMessageId, originalMessageId, originalSenderAddress, recipientAddresses, MessageTrackingEvent.MODERATIONEXPIRE, organizationId, MessageTrackingLog.GetExternalOrganizationId(organizationId), isNotificationSent);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00012C10 File Offset: 0x00010E10
		public static void TrackModeratorDecision(MessageTrackingSource source, string initiationMessageId, string originalMessageId, string originalSenderAddress, ICollection<string> recipientAddresses, bool isApproved, OrganizationId organizationId)
		{
			MessageTrackingEvent messageTrackingEvent;
			if (isApproved)
			{
				messageTrackingEvent = MessageTrackingEvent.MODERATORAPPROVE;
			}
			else
			{
				messageTrackingEvent = MessageTrackingEvent.MODERATORREJECT;
			}
			MessageTrackingLog.TrackModeratorDecisionNdrExpired(source, initiationMessageId, originalMessageId, originalSenderAddress, recipientAddresses, messageTrackingEvent, organizationId, MessageTrackingLog.GetExternalOrganizationId(organizationId), false);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00012C40 File Offset: 0x00010E40
		public static void TrackMeetingMessage(string internetMessageID, string clientName, OrganizationId organizationID, List<KeyValuePair<string, string>> extraEventData)
		{
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			logRowFormatter[7] = MessageTrackingSource.MEETINGMESSAGEPROCESSOR;
			logRowFormatter[4] = MessageTrackingLog.hostName;
			logRowFormatter[8] = MessageTrackingEvent.PROCESSMEETINGMESSAGE;
			logRowFormatter[9] = internetMessageID;
			logRowFormatter[2] = clientName;
			logRowFormatter[23] = MessageTrackingLog.GetOrganizationIdToLog(organizationID);
			List<KeyValuePair<string, object>> properties = null;
			MessageTrackingLog.AddCustomDataList(extraEventData, ref properties);
			MessageTrackingLog.AppendCustomDataList(logRowFormatter, properties);
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00012CC0 File Offset: 0x00010EC0
		public static void Configure(Server serverConfig)
		{
			if (serverConfig == null)
			{
				return;
			}
			MessageTrackingLog.enabled = serverConfig.MessageTrackingLogEnabled;
			if (MessageTrackingLog.enabled)
			{
				if (serverConfig.MessageTrackingLogPath == null || string.IsNullOrEmpty(serverConfig.MessageTrackingLogPath.PathName))
				{
					MessageTrackingLog.enabled = false;
					ExTraceGlobals.MessageTrackingTracer.TraceDebug(0L, "Message tracking was supposed to be on but the output path was null");
					Components.EventLogger.LogEvent(TransportEventLogConstants.Tuple_MessageTrackingLogPathIsNull, null, new object[0]);
					return;
				}
				MessageTrackingLog.log.Configure(serverConfig.MessageTrackingLogPath.PathName, serverConfig.MessageTrackingLogMaxAge, (long)(serverConfig.MessageTrackingLogMaxDirectorySize.IsUnlimited ? 0UL : serverConfig.MessageTrackingLogMaxDirectorySize.Value.ToBytes()), (long)serverConfig.MessageTrackingLogMaxFileSize.ToBytes(), true, Components.TransportAppConfig.Logging.MsgTrkLogBufferSize, Components.TransportAppConfig.Logging.MsgTrkLogFlushInterval);
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00012DAC File Offset: 0x00010FAC
		public static void FlushBuffer()
		{
			if (MessageTrackingLog.log != null)
			{
				MessageTrackingLog.log.Flush();
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00012DC0 File Offset: 0x00010FC0
		internal static string GetMessageTrackingString(SmtpResponse smtpResponse)
		{
			string[] statusText = smtpResponse.StatusText;
			string statusCode = smtpResponse.StatusCode;
			string enhancedStatusCode = smtpResponse.EnhancedStatusCode;
			string result;
			if (statusText != null && statusText.Length > 0)
			{
				string text;
				if (!string.IsNullOrEmpty(enhancedStatusCode))
				{
					text = statusCode + " " + enhancedStatusCode + " ";
				}
				else
				{
					text = statusCode + " ";
				}
				string text2 = statusText[0].Replace(';', ' ');
				int capacity = text.Length + text2.Length;
				StringBuilder stringBuilder = new StringBuilder(capacity);
				stringBuilder.Append(text);
				stringBuilder.Append(text2);
				if (stringBuilder.Length > 512)
				{
					stringBuilder.Length = 512;
				}
				result = stringBuilder.ToString();
			}
			else if (!string.IsNullOrEmpty(enhancedStatusCode))
			{
				result = statusCode + " " + enhancedStatusCode;
			}
			else if (!string.IsNullOrEmpty(statusCode))
			{
				result = statusCode;
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00012EA8 File Offset: 0x000110A8
		internal static void UpdateLogFormatter(LogRowFormatter row, IReadOnlyMailItem mailItem, bool poisonMessage)
		{
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>(2);
			MessageTrackingLog.UpdateLogFormatter(row, mailItem, ref list, poisonMessage);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00012EC6 File Offset: 0x000110C6
		internal static void UpdateLogFormatter(LogRowFormatter row, IReadOnlyMailItem mailItem, ref List<KeyValuePair<string, object>> properties, bool poisonMessage)
		{
			MessageTrackingLog.UpdateLogFormatter(row, mailItem, ref properties, poisonMessage, false);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00012ED4 File Offset: 0x000110D4
		internal static void UpdateLogFormatter(LogRowFormatter row, IReadOnlyMailItem mailItem, ref List<KeyValuePair<string, object>> properties, bool poisonMessage, bool shouldAddOorg)
		{
			if (mailItem != null)
			{
				row[9] = mailItem.RecordId;
				row[10] = mailItem.InternetMessageId;
				row[11] = mailItem.NetworkMessageId;
				row[20] = mailItem.From;
				row[18] = MessageTrackingLog.GetSubjectToLog(mailItem.Subject);
				row[19] = mailItem.MimeSender;
				row[14] = mailItem.MimeSize;
				if (!poisonMessage)
				{
					row[22] = mailItem.Directionality;
				}
				MessageTrackingLog.BuildCommonCustomData(mailItem, ref properties, shouldAddOorg);
				string value;
				row[23] = MessageTrackingLog.GetExternalOrganizationIdToLog(mailItem.ExternalOrganizationId, mailItem.OrganizationId, out value);
				if (!string.IsNullOrEmpty(value))
				{
					MessageTrackingLog.AddCustomData("ExternalOrgIdNotSetReason", value, ref properties);
				}
			}
			MessageTrackingLog.AppendCustomDataList(row, properties);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00012FC0 File Offset: 0x000111C0
		internal static int GetRecipsCount(MessageTrackingSource source, IEnumerable<MailRecipient> recipients, out int failedRecipCount)
		{
			int num = 0;
			failedRecipCount = 0;
			if (recipients == null)
			{
				return 0;
			}
			foreach (MailRecipient mailRecipient in recipients)
			{
				if (source != MessageTrackingSource.STOREDRIVER && source != MessageTrackingSource.ROUTING && (mailRecipient.AckStatus == AckStatus.Success || mailRecipient.AckStatus == AckStatus.SuccessNoDsn || mailRecipient.AckStatus == AckStatus.Relay))
				{
					num++;
				}
				else if (mailRecipient.AckStatus == AckStatus.Fail)
				{
					failedRecipCount++;
				}
			}
			return num;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00013044 File Offset: 0x00011244
		internal static string RemoveCRLFs(string str)
		{
			string result = str;
			if (!string.IsNullOrEmpty(str))
			{
				result = str.Replace("\r\n", " ");
			}
			return result;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00013070 File Offset: 0x00011270
		internal static void Append(LogRowFormatter row)
		{
			try
			{
				MessageTrackingLog.log.Append(row, 0);
			}
			catch (ObjectDisposedException)
			{
				ExTraceGlobals.MessageTrackingTracer.TraceDebug(0L, "Message tracking append failed with ObjectDisposedException");
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000130B0 File Offset: 0x000112B0
		private static string[] InitializeFields()
		{
			string[] array = new string[Enum.GetValues(typeof(MsgTrackField)).Length];
			array[0] = "date-time";
			array[1] = "client-ip";
			array[2] = "client-hostname";
			array[3] = "server-ip";
			array[4] = "server-hostname";
			array[5] = "source-context";
			array[6] = "connector-id";
			array[7] = "source";
			array[8] = "event-id";
			array[9] = "internal-message-id";
			array[10] = "message-id";
			array[11] = "network-message-id";
			array[12] = "recipient-address";
			array[13] = "recipient-status";
			array[14] = "total-bytes";
			array[15] = "recipient-count";
			array[16] = "related-recipient-address";
			array[17] = "reference";
			array[18] = "message-subject";
			array[19] = "sender-address";
			array[20] = "return-path";
			array[21] = "message-info";
			array[22] = "directionality";
			array[23] = "tenant-id";
			array[24] = "original-client-ip";
			array[25] = "original-server-ip";
			array[26] = "custom-data";
			return array;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000131C4 File Offset: 0x000113C4
		private static void TrackThrottleHelper(MessageTrackingSource source, MsgTrackMapiSubmitInfo msgTrackInfo, string senderAddress, string messageId, MailDirectionality? direction)
		{
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			logRowFormatter[8] = MessageTrackingEvent.THROTTLE;
			logRowFormatter[7] = source;
			List<KeyValuePair<string, object>> list = null;
			MessageTrackingLog.TrackCommonMsgTrackMapiSubmitInfo(msgTrackInfo, logRowFormatter, ref list);
			logRowFormatter[19] = senderAddress;
			logRowFormatter[10] = messageId;
			logRowFormatter[21] = msgTrackInfo.DiagnosticInfo;
			string value;
			logRowFormatter[23] = MessageTrackingLog.GetExternalOrganizationIdToLog(msgTrackInfo.ExternalOrganizationId, msgTrackInfo.OrganizationId, out value);
			if (!string.IsNullOrEmpty(value))
			{
				MessageTrackingLog.AddCustomData("ExternalOrgIdNotSetReason", value, ref list);
			}
			if (list != null)
			{
				MessageTrackingLog.AppendCustomDataList(logRowFormatter, list);
			}
			if (direction != null && direction != null)
			{
				logRowFormatter[22] = direction.Value;
			}
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00013290 File Offset: 0x00011490
		private static void TrackRecipient(MessageTrackingSource source, IReadOnlyMailItem mailItem, RoutingAddress recipEmail, SmtpResponse? smtpResponse, MessageTrackingEvent trackingEvent, string sourceContext, List<KeyValuePair<string, object>> extraEventData, LatencyFormatter latencyFormatter)
		{
			MessageTrackingLog.WriteLamEventNotification(mailItem, trackingEvent);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			MessageTrackingLog.SetLatencyFormatter(logRowFormatter, latencyFormatter, ref extraEventData);
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, mailItem, ref extraEventData, trackingEvent == MessageTrackingEvent.POISONMESSAGE);
			logRowFormatter[2] = MessageTrackingLog.hostName;
			logRowFormatter[7] = source;
			logRowFormatter[5] = sourceContext;
			logRowFormatter[8] = trackingEvent;
			logRowFormatter[15] = 1;
			logRowFormatter[12] = recipEmail;
			if (smtpResponse != null && !string.IsNullOrEmpty(smtpResponse.Value.ToString()))
			{
				logRowFormatter[13] = new string[]
				{
					MessageTrackingLog.RemoveCRLFs(new LastError(null, null, null, smtpResponse.Value).ToString())
				};
			}
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0001337C File Offset: 0x0001157C
		private static void TrackDeliveryEvent(MessageTrackingSource source, MessageTrackingEvent eventId, IReadOnlyMailItem mailItem, ICollection recipients, ICollection recipientsData, string clientHostname, string serverName, LatencyFormatter latencyFormatter, string sourceContext, List<KeyValuePair<string, string>> extraEventData)
		{
			if (latencyFormatter == null)
			{
				throw new ArgumentNullException("latencyFormatter");
			}
			MessageTrackingLog.WriteLamEventNotification(mailItem, eventId);
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			List<KeyValuePair<string, object>> list = null;
			MessageTrackingLog.AddCustomDataList(extraEventData, ref list);
			MessageTrackingLog.AddResubmitSourceCustomData(mailItem, ref list);
			if (eventId == MessageTrackingEvent.DELIVER)
			{
				MessageTrackingLog.SetLatencyFormatter(logRowFormatter, latencyFormatter, ref list);
			}
			else
			{
				string value = latencyFormatter.FormatOrgArrivalTime();
				if (!string.IsNullOrEmpty(value))
				{
					logRowFormatter[21] = value;
				}
			}
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, mailItem, ref list, false);
			logRowFormatter[8] = eventId;
			logRowFormatter[7] = source;
			logRowFormatter[4] = serverName;
			logRowFormatter[2] = clientHostname;
			logRowFormatter[15] = recipients.Count;
			logRowFormatter[12] = recipients;
			logRowFormatter[13] = recipientsData;
			logRowFormatter[5] = sourceContext;
			MessageTrackingLog.SetOriginalIP(mailItem, logRowFormatter);
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00013460 File Offset: 0x00011660
		private static void TrackCommonMsgTrackMapiSubmitInfo(MsgTrackMapiSubmitInfo msgTrackInfo, LogRowFormatter row, ref List<KeyValuePair<string, object>> properties)
		{
			row[3] = msgTrackInfo.BridgeheadServerIPAddress;
			row[4] = msgTrackInfo.BridgeheadServerName;
			row[1] = msgTrackInfo.MailboxServerIPAddress;
			row[2] = msgTrackInfo.MailboxServerName;
			row[5] = msgTrackInfo.MapiEventInfo;
			string value = BitConverter.ToString(msgTrackInfo.ItemEntryId);
			MessageTrackingLog.AddCustomData("ItemEntryId", value, ref properties);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x000134C8 File Offset: 0x000116C8
		private static void TrackModeratorDecisionNdrExpired(MessageTrackingSource source, string initiationMessageId, string originalMessageId, string originalSenderAddress, ICollection<string> recipientAddresses, MessageTrackingEvent messageTrackingEvent, OrganizationId organizationId, Guid externalOrganizationId, bool isExpiryNotificationSent)
		{
			if (!MessageTrackingLog.enabled)
			{
				return;
			}
			if (messageTrackingEvent != MessageTrackingEvent.MODERATORAPPROVE && messageTrackingEvent != MessageTrackingEvent.MODERATORREJECT && messageTrackingEvent != MessageTrackingEvent.MODERATORSALLNDR && messageTrackingEvent != MessageTrackingEvent.MODERATIONEXPIRE)
			{
				throw new ArgumentException("Unexpected messageTrackingEvent");
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			logRowFormatter[8] = messageTrackingEvent;
			logRowFormatter[7] = source;
			logRowFormatter[10] = initiationMessageId;
			logRowFormatter[17] = new string[]
			{
				originalMessageId
			};
			logRowFormatter[9] = 0;
			logRowFormatter[15] = recipientAddresses.Count;
			logRowFormatter[12] = recipientAddresses;
			logRowFormatter[19] = originalSenderAddress;
			string value;
			logRowFormatter[23] = MessageTrackingLog.GetExternalOrganizationIdToLog(externalOrganizationId, organizationId, out value);
			MessageTrackingLog.AppendCustomData(logRowFormatter, "ExternalOrgIdNotSetReason", value);
			if (messageTrackingEvent == MessageTrackingEvent.MODERATIONEXPIRE)
			{
				MessageTrackingLog.AppendCustomData(logRowFormatter, "ExpiryNotificationSent", isExpiryNotificationSent.ToString());
			}
			MessageTrackingLog.Append(logRowFormatter);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000135B8 File Offset: 0x000117B8
		private static void TrackRecipientStatus(LogRowFormatter row, MessageTrackingSource source, IReadOnlyMailItem mailItem, string sourceContext, IEnumerable<MailRecipient> recipients, SmtpResponse smtpResponse, LatencyFormatter latencyFormatter, ref List<KeyValuePair<string, object>> extraEventData)
		{
			int num = 0;
			int recipsCount = MessageTrackingLog.GetRecipsCount(source, recipients, out num);
			if (recipsCount > 0 || num > 0)
			{
				string[] array = new string[recipsCount];
				string[] array2 = new string[recipsCount];
				string[] array3 = new string[recipsCount];
				List<string> connectorCustomDataList = new List<string>();
				string[] array4 = new string[num];
				string[] array5 = new string[num];
				string[] array6 = new string[num];
				List<string> connectorCustomDataList2 = new List<string>();
				int num2 = 0;
				int num3 = 0;
				foreach (MailRecipient mailRecipient in recipients)
				{
					if (mailRecipient.IsActive)
					{
						if (source != MessageTrackingSource.STOREDRIVER && source != MessageTrackingSource.ROUTING && (mailRecipient.AckStatus == AckStatus.Success || mailRecipient.AckStatus == AckStatus.SuccessNoDsn || mailRecipient.AckStatus == AckStatus.Relay))
						{
							array[num2] = mailRecipient.ToString();
							array2[num2] = MessageTrackingLog.RemoveCRLFs(mailRecipient.SmtpResponse.ToString());
							array3[num2] = mailRecipient.DsnMessageId;
							MessageTrackingLog.AddTenantConnectorInfo(mailRecipient.ExtendedProperties, ref connectorCustomDataList);
							num2++;
						}
						else if (mailRecipient.AckStatus == AckStatus.Fail)
						{
							array4[num3] = mailRecipient.ToString();
							string text = MessageTrackingLog.RemoveCRLFs(mailRecipient.GetLastErrorDetails());
							string text2 = null;
							if (mailRecipient.ExtendedProperties.TryGetValue<string>("Microsoft.Exchange.Transport.RecipientDiagnosticInfo", out text2))
							{
								array5[num3] = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
								{
									text,
									text2
								});
							}
							else
							{
								array5[num3] = text;
							}
							array6[num3] = mailRecipient.DsnMessageId;
							MessageTrackingLog.AddTenantConnectorInfo(mailRecipient.ExtendedProperties, ref connectorCustomDataList2);
							num3++;
						}
					}
				}
				MessageTrackingLog.SetLatencyFormatter(row, latencyFormatter, ref extraEventData);
				if (recipsCount > 0)
				{
					row[8] = MessageTrackingEvent.SEND;
					row[15] = recipsCount;
					row[12] = array;
					row[13] = array2;
					row[17] = array3;
					List<string> list = new List<string>();
					if (sourceContext != null)
					{
						list.Add(sourceContext);
					}
					if (smtpResponse.SmtpResponseType == SmtpResponseType.Success)
					{
						string messageTrackingString = MessageTrackingLog.GetMessageTrackingString(smtpResponse);
						list.Add(messageTrackingString);
					}
					string text3 = "ClientSubmitTime:";
					DateTime dateTime;
					mailItem.Message.TryGetMapiProperty<DateTime>(TnefPropertyTag.ClientSubmitTime, out dateTime);
					if (dateTime != DateTime.MinValue)
					{
						text3 = string.Format(CultureInfo.InvariantCulture, "{0}{1:yyyy-MM-ddTHH\\:mm\\:ss.fffZ}", new object[]
						{
							text3,
							dateTime
						});
					}
					list.Add(text3);
					string value = string.Join(";", list);
					row[5] = value;
					MessageTrackingLog.SetTenantConnectorUsage(ref extraEventData, connectorCustomDataList);
					MessageTrackingLog.AddResubmitSourceCustomData(mailItem, ref extraEventData);
					MessageTrackingLog.AppendCustomDataList(row, extraEventData);
					MessageTrackingLog.Append(row);
				}
				if (num > 0)
				{
					row[8] = MessageTrackingEvent.FAIL;
					row[15] = num;
					row[12] = array4;
					row[13] = array5;
					row[17] = array6;
					row[5] = sourceContext;
					MessageTrackingLog.SetTenantConnectorUsage(ref extraEventData, connectorCustomDataList2);
					MessageTrackingLog.AppendCustomDataList(row, extraEventData);
					MessageTrackingLog.Append(row);
				}
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x000138E8 File Offset: 0x00011AE8
		private static string GetOrganizationIdToLog(OrganizationId organizationId)
		{
			if (organizationId == null || organizationId.OrganizationalUnit == null)
			{
				return string.Empty;
			}
			return organizationId.OrganizationalUnit.ObjectGuid.ToString();
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00013928 File Offset: 0x00011B28
		private static string GetExternalOrganizationIdToLog(Guid externalOrganizationId, OrganizationId organizationId, out string customDataToAppend)
		{
			customDataToAppend = string.Empty;
			if (externalOrganizationId == Guid.Empty)
			{
				if (organizationId == null)
				{
					customDataToAppend = "OrgId_Not_Known";
				}
				else if (OrganizationId.ForestWideOrgId == null)
				{
					customDataToAppend = "FirstOrgId_Not_Known";
				}
				else if (!OrganizationId.ForestWideOrgId.Equals(organizationId))
				{
					customDataToAppend = "Not_FirstOrg_But_ExternalOrgId_Is_Empty";
				}
				return string.Empty;
			}
			return externalOrganizationId.ToString();
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00013998 File Offset: 0x00011B98
		private static string GetSubjectToLog(string subject)
		{
			Server transportServer = Components.Configuration.LocalServer.TransportServer;
			if (!transportServer.MessageTrackingLogSubjectLoggingEnabled)
			{
				return "[Undisclosed]";
			}
			if (string.IsNullOrEmpty(subject))
			{
				return string.Empty;
			}
			return subject.Substring(0, Math.Min(subject.Length, 255));
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x000139E8 File Offset: 0x00011BE8
		private static string FormatValues(params string[] values)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in values)
			{
				if (!string.IsNullOrEmpty(text))
				{
					stringBuilder.Append(text);
					if (!text.EndsWith(':'.ToString()))
					{
						stringBuilder.Append(':');
					}
					stringBuilder.Append(' ');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00013A4C File Offset: 0x00011C4C
		private static void AddCustomDataList(List<KeyValuePair<string, string>> extraEventData, ref List<KeyValuePair<string, object>> properties)
		{
			if (extraEventData != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in extraEventData)
				{
					MessageTrackingLog.AddCustomData(keyValuePair.Key, keyValuePair.Value, ref properties);
				}
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00013AAC File Offset: 0x00011CAC
		private static void AddCustomData(string name, object value, ref List<KeyValuePair<string, object>> properties)
		{
			if (properties == null)
			{
				properties = new List<KeyValuePair<string, object>>();
			}
			properties.Add(new KeyValuePair<string, object>(name, value));
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00013AC8 File Offset: 0x00011CC8
		private static void AddResubmitSourceCustomData(IReadOnlyMailItem mailItem, ref List<KeyValuePair<string, object>> properties)
		{
			bool flag = false;
			bool flag2 = false;
			for (ReceivedHeader receivedHeader = mailItem.RootPart.Headers.FindFirst(HeaderId.Received) as ReceivedHeader; receivedHeader != null; receivedHeader = (mailItem.RootPart.Headers.FindNext(receivedHeader) as ReceivedHeader))
			{
				if (receivedHeader.With == "MailboxResubmission")
				{
					flag = true;
				}
				if (receivedHeader.With == "ShadowRedundancy")
				{
					flag2 = true;
				}
				if (flag && flag2)
				{
					break;
				}
			}
			if (flag)
			{
				MessageTrackingLog.AddCustomData("Resubmit", "SafetyNet", ref properties);
			}
			if (flag2)
			{
				MessageTrackingLog.AddCustomData("Resubmit", "ShadowRedundancy", ref properties);
			}
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00013B60 File Offset: 0x00011D60
		private static void BuildCommonCustomData(IReadOnlyMailItem mailItem, ref List<KeyValuePair<string, object>> extraEventData, bool shouldAddOorg)
		{
			MessageTrackingLog.AddCustomData("DeliveryPriority", mailItem.Priority.ToString(), ref extraEventData);
			if (!string.IsNullOrEmpty(mailItem.PrioritizationReason))
			{
				MessageTrackingLog.AddCustomData("PrioritizationReason", mailItem.PrioritizationReason, ref extraEventData);
			}
			if (mailItem.LockReasonHistory != null)
			{
				MessageTrackingLog.AddCustomData("LockReasons", MessageTrackingLog.CombineStrings(mailItem.LockReasonHistory), ref extraEventData);
			}
			if (shouldAddOorg)
			{
				MessageTrackingLog.AddOriginatorOrganization(mailItem, ref extraEventData);
			}
			if (!RoutingAddress.IsEmpty(mailItem.OriginalFrom) && (!mailItem.OriginalFrom.Equals(mailItem.From) || !mailItem.OriginalFrom.Equals(mailItem.MimeSender)))
			{
				MessageTrackingLog.AddCustomData("OriginalFromAddress", mailItem.OriginalFrom.ToString(), ref extraEventData);
			}
			if (!string.IsNullOrEmpty(mailItem.ExoAccountForest))
			{
				MessageTrackingLog.AddCustomData("AccountForest", mailItem.ExoAccountForest, ref extraEventData);
			}
			if (mailItem.IsProbe)
			{
				MessageTrackingLog.AddCustomData("IsProbe", "true", ref extraEventData);
				MessageTrackingLog.AddCustomData("PersistProbeTrace", mailItem.PersistProbeTrace.ToString(), ref extraEventData);
				if (!string.IsNullOrEmpty(mailItem.ProbeName))
				{
					MessageTrackingLog.AddCustomData("ProbeType", mailItem.ProbeName, ref extraEventData);
				}
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00013C94 File Offset: 0x00011E94
		private static void SetLatencyFormatter(LogRowFormatter row, LatencyFormatter latencyFormatter, ref List<KeyValuePair<string, object>> extraEventData)
		{
			if (latencyFormatter != null)
			{
				DateTime utcNow = DateTime.UtcNow;
				row[21] = latencyFormatter.FormatAndUpdatePerfCounters();
				string format = "F3";
				if (latencyFormatter.EndToEndLatency != TimeSpan.MinValue)
				{
					MessageTrackingLog.AddCustomData("E2ELatency", latencyFormatter.EndToEndLatency.TotalSeconds.ToString(format, CultureInfo.InvariantCulture), ref extraEventData);
					if (latencyFormatter.ExternalSendLatency != TimeSpan.MinValue)
					{
						MessageTrackingLog.AddCustomData("ExternalSendLatency", latencyFormatter.ExternalSendLatency.TotalSeconds.ToString(format, CultureInfo.InvariantCulture), ref extraEventData);
					}
				}
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00013D34 File Offset: 0x00011F34
		private static bool TryGetTenantOutboundConnectorProperties(IExtendedPropertyCollection mailRecipientExtendedProperties, out string connectorCustomData)
		{
			return mailRecipientExtendedProperties.TryGetValue<string>("Microsoft.Exchange.Hygiene.TenantOutboundConnectorCustomData", out connectorCustomData) && connectorCustomData != null;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00013D50 File Offset: 0x00011F50
		private static void AddTenantConnectorInfo(IExtendedPropertyCollection mailRecipientExtendedProperties, ref List<string> tenantConnectorCustomDataList)
		{
			string text;
			if (mailRecipientExtendedProperties != null && MessageTrackingLog.TryGetTenantOutboundConnectorProperties(mailRecipientExtendedProperties, out text) && !tenantConnectorCustomDataList.Contains(text, StringComparer.OrdinalIgnoreCase))
			{
				tenantConnectorCustomDataList.Add(text);
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00013D94 File Offset: 0x00011F94
		private static void SetTenantConnectorUsage(ref List<KeyValuePair<string, object>> properties, IEnumerable<string> connectorCustomDataList)
		{
			if (properties != null)
			{
				properties.RemoveAll((KeyValuePair<string, object> kvp) => kvp.Key == "Microsoft.Exchange.Hygiene.TenantOutboundConnectorCustomData");
			}
			if (connectorCustomDataList.Any<string>())
			{
				MessageTrackingLog.AddCustomData("Microsoft.Exchange.Hygiene.TenantOutboundConnectorCustomData", MessageTrackingLog.CombineStrings(connectorCustomDataList), ref properties);
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00013DE4 File Offset: 0x00011FE4
		private static void AddOriginatorOrganization(IReadOnlyMailItem mailItem, ref List<KeyValuePair<string, object>> properties)
		{
			string oorg = mailItem.Oorg;
			if (!string.IsNullOrEmpty(oorg))
			{
				MessageTrackingLog.AddCustomData("Oorg", oorg, ref properties);
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00013E0C File Offset: 0x0001200C
		private static void AddProxiedClientDetails(IPAddress proxiedClientIPAddress, string proxiedClientHostname, ref List<KeyValuePair<string, object>> properties)
		{
			if (proxiedClientIPAddress != null)
			{
				MessageTrackingLog.AddCustomData("ProxiedClientIPAddress", proxiedClientIPAddress.ToString(), ref properties);
			}
			if (!string.IsNullOrEmpty(proxiedClientHostname))
			{
				MessageTrackingLog.AddCustomData("ProxiedClientHostname", proxiedClientHostname, ref properties);
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00013E36 File Offset: 0x00012036
		private static string CombineStrings(IEnumerable<string> strings)
		{
			return string.Join("| ", strings);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00013E44 File Offset: 0x00012044
		private static LogRowFormatter CreateDefaultLogRow(MessageTrackingEvent messageTrackingEvent, MessageTrackingSource messageTrackingSource, IReadOnlyMailItem mailItem, bool setRecipientFields = true)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(MessageTrackingLog.msgTrackingSchema);
			MessageTrackingLog.UpdateLogFormatter(logRowFormatter, mailItem, false);
			logRowFormatter[8] = messageTrackingEvent;
			logRowFormatter[7] = messageTrackingSource;
			logRowFormatter[4] = MessageTrackingLog.hostName;
			if (setRecipientFields)
			{
				logRowFormatter[15] = mailItem.Recipients.Count;
				logRowFormatter[12] = mailItem.Recipients;
			}
			return logRowFormatter;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00013EB4 File Offset: 0x000120B4
		private static void WriteLamEventNotification(IReadOnlyMailItem mailItem, MessageTrackingEvent eventId)
		{
			if (mailItem != null && mailItem.SystemProbeId != Guid.Empty)
			{
				EventNotificationItem eventNotificationItem = new EventNotificationItem(ExchangeComponent.Transport.Name, "MessageTracking", mailItem.ProbeName, ResultSeverityLevel.Verbose);
				eventNotificationItem.AddCustomProperty("StateAttribute1", (mailItem.InternetMessageId != null) ? mailItem.InternetMessageId : string.Empty);
				eventNotificationItem.AddCustomProperty("StateAttribute2", "MTRT");
				eventNotificationItem.AddCustomProperty("StateAttribute3", eventId);
				eventNotificationItem.StateAttribute4 = mailItem.SystemProbeId.ToString();
				eventNotificationItem.Publish(false);
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00013F58 File Offset: 0x00012158
		private static void WriteLamPoisonEventNotification(IReadOnlyMailItem mailItem)
		{
			new EventNotificationItem(ExchangeComponent.Transport.Name, "PoisonMessageNotification", "PoisonMessageWithMessageId", ResultSeverityLevel.Error)
			{
				StateAttribute1 = (mailItem.InternetMessageId ?? string.Empty),
				StateAttribute2 = ((mailItem.ExternalOrganizationId != Guid.Empty) ? mailItem.ExternalOrganizationId.ToString() : string.Empty)
			}.Publish(false);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00013FD0 File Offset: 0x000121D0
		private static void WriteLamPoisonEventNotification(MessageTrackingSource source, MsgTrackPoisonInfo msgTrackingPoisonInfo)
		{
			new EventNotificationItem(ExchangeComponent.Transport.Name, "PoisonMessageNotification", "PoisonMessageEncounteredStoreSubmission", ResultSeverityLevel.Error)
			{
				StateAttribute1 = source.ToString(),
				StateAttribute2 = msgTrackingPoisonInfo.ClientIPAddress.ToString(),
				StateAttribute3 = (msgTrackingPoisonInfo.ClientHostName ?? string.Empty),
				StateAttribute4 = msgTrackingPoisonInfo.ServerIPAddress.ToString(),
				StateAttribute5 = (msgTrackingPoisonInfo.SourceContext ?? string.Empty)
			}.Publish(false);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001405C File Offset: 0x0001225C
		private static void WriteLamAgentInfoEventNotification(IReadOnlyMailItem mailItem, List<KeyValuePair<string, object>> logLine)
		{
			if (mailItem != null && mailItem.SystemProbeId != Guid.Empty && mailItem.InternetMessageId != null && mailItem.ProbeName != null)
			{
				EventNotificationItem eventNotificationItem = new EventNotificationItem(ExchangeComponent.Transport.Name, "MessageTracking", mailItem.ProbeName, ResultSeverityLevel.Verbose);
				StringBuilder stringBuilder = new StringBuilder();
				foreach (KeyValuePair<string, object> keyValuePair in logLine)
				{
					if (keyValuePair.Key != null && keyValuePair.Value != null)
					{
						stringBuilder.AppendFormat("{0}[{1}],", keyValuePair.Key, keyValuePair.Value);
					}
				}
				eventNotificationItem.AddCustomProperty("StateAttribute1", mailItem.InternetMessageId);
				eventNotificationItem.AddCustomProperty("StateAttribute2", "AGENTINFO");
				eventNotificationItem.AddCustomProperty("StateAttribute3", stringBuilder.ToString());
				eventNotificationItem.StateAttribute4 = mailItem.SystemProbeId.ToString();
				eventNotificationItem.Publish(false);
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00014178 File Offset: 0x00012378
		private static void AppendCustomData(LogRowFormatter row, string key, string value)
		{
			if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
			{
				List<KeyValuePair<string, object>> value2 = (List<KeyValuePair<string, object>>)row[26];
				MessageTrackingLog.AddCustomData(key, value, ref value2);
				row[26] = value2;
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000141B8 File Offset: 0x000123B8
		private static void AppendCustomDataList(LogRowFormatter row, List<KeyValuePair<string, object>> properties)
		{
			List<KeyValuePair<string, object>> list = (List<KeyValuePair<string, object>>)row[26];
			if (list == null && properties != null)
			{
				list = new List<KeyValuePair<string, object>>(properties.Count);
			}
			if (properties != null)
			{
				list.InsertRange(0, properties);
				properties.Clear();
				row[26] = list;
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000141FF File Offset: 0x000123FF
		private static Guid GetExternalOrganizationId(OrganizationId organizationId)
		{
			if (organizationId == null)
			{
				return Guid.Empty;
			}
			if (!(organizationId == OrganizationId.ForestWideOrgId))
			{
				return ADAccountPartitionLocator.GetExternalDirectoryOrganizationIdByTenantName(organizationId.OrganizationalUnit.Name, organizationId.PartitionId);
			}
			return TenantPartitionHint.ExternalDirectoryOrganizationIdForRootOrg;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001423C File Offset: 0x0001243C
		private static void SetOriginalIP(IReadOnlyMailItem mailItem, LogRowFormatter row)
		{
			Header header = mailItem.RootPart.Headers.FindFirst("X-MS-Exchange-Organization-OriginalClientIPAddress");
			if (header != null)
			{
				row[24] = header.Value;
			}
			header = mailItem.RootPart.Headers.FindFirst("X-MS-Exchange-Organization-OriginalServerIPAddress");
			if (header != null)
			{
				row[25] = header.Value;
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00014298 File Offset: 0x00012498
		private static void SetSubmittingSender(LogRowFormatter row, RoutingAddress sender, string submittingMailbox)
		{
			if (!string.IsNullOrEmpty(submittingMailbox) && !string.Equals(submittingMailbox, sender.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				sender = new RoutingAddress(submittingMailbox);
			}
			if (row[19] == null || !string.Equals(sender.ToString(), row[19].ToString(), StringComparison.OrdinalIgnoreCase))
			{
				row[19] = sender;
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00014308 File Offset: 0x00012508
		private static void SetPurportedSender(LogRowFormatter row, IReadOnlyMailItem mailItem, string actualSenderAddress)
		{
			AddressHeader addressHeader = AddressHeader.Parse("From", mailItem.MimeFrom, AddressParserFlags.None);
			MimeRecipient mimeRecipient = addressHeader.FirstChild as MimeRecipient;
			string purportedSenderAddress;
			if (mimeRecipient != null)
			{
				purportedSenderAddress = mimeRecipient.Email;
			}
			else
			{
				purportedSenderAddress = mailItem.From.ToString();
			}
			MessageTrackingLog.SetPurportedSender(row, purportedSenderAddress, actualSenderAddress);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0001435C File Offset: 0x0001255C
		private static void SetPurportedSender(LogRowFormatter row, string purportedSenderAddress, string actualSenderAddress)
		{
			if (!string.IsNullOrEmpty(purportedSenderAddress) && !string.Equals(purportedSenderAddress, actualSenderAddress, StringComparison.OrdinalIgnoreCase))
			{
				MessageTrackingLog.AppendCustomData(row, "PurportedSender", purportedSenderAddress);
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0001437C File Offset: 0x0001257C
		private static void SetSenderMailboxGuid(LogRowFormatter row, Guid mailboxGuid)
		{
			if (mailboxGuid != Guid.Empty)
			{
				MessageTrackingLog.AppendCustomData(row, "Mailbox", mailboxGuid.ToString());
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000143A4 File Offset: 0x000125A4
		private static void MessageTrackingLogDirSizeQuotaExceeded(string component, string directory, long maxDirectorySize, int trimmed)
		{
			new EventNotificationItem(ExchangeComponent.Transport.Name, "MessageTrackingLogs", null, "Message Tracking Log directory has reached the directory quota", ResultSeverityLevel.Warning)
			{
				StateAttribute1 = component,
				StateAttribute2 = directory,
				StateAttribute3 = maxDirectorySize.ToString(),
				StateAttribute4 = trimmed.ToString()
			}.Publish(false);
		}

		// Token: 0x0400023B RID: 571
		public const string SubjectNotDisclosed = "[Undisclosed]";

		// Token: 0x0400023C RID: 572
		public const string InvalidRecipientPropertyName = "InvalidRecipient";

		// Token: 0x0400023D RID: 573
		public const string ItemEntryIdPropertyName = "ItemEntryId";

		// Token: 0x0400023E RID: 574
		public const string MailboxDatabaseGuidPropertyName = "MailboxDatabaseGuid";

		// Token: 0x0400023F RID: 575
		public const string FirstForestHopPropertyName = "FirstForestHop";

		// Token: 0x04000240 RID: 576
		public const string ServiceTagPropertyName = "ServiceTag";

		// Token: 0x04000241 RID: 577
		public const string VersionPropertyName = "Version";

		// Token: 0x04000242 RID: 578
		private const string DeliveryPriorityPropertyName = "DeliveryPriority";

		// Token: 0x04000243 RID: 579
		private const string PrioritizationReasonPropertyName = "PrioritizationReason";

		// Token: 0x04000244 RID: 580
		private const string DiagnosticInfoPropertyName = "DiagnosticInfo";

		// Token: 0x04000245 RID: 581
		private const string PurportedSenderPropertyName = "PurportedSender";

		// Token: 0x04000246 RID: 582
		private const string MailboxGuidPropertyName = "Mailbox";

		// Token: 0x04000247 RID: 583
		private const string EndToEndLatencyPropertyName = "E2ELatency";

		// Token: 0x04000248 RID: 584
		private const string ExternalSendLatencyPropertyName = "ExternalSendLatency";

		// Token: 0x04000249 RID: 585
		private const string ExternalOrgIdNotSetReason = "ExternalOrgIdNotSetReason";

		// Token: 0x0400024A RID: 586
		private const string OriginalFromAddress = "OriginalFromAddress";

		// Token: 0x0400024B RID: 587
		private const string AccountForest = "AccountForest";

		// Token: 0x0400024C RID: 588
		private const string OriginatorOrganizationPropertyName = "Oorg";

		// Token: 0x0400024D RID: 589
		private const string LockReasonPropertyName = "LockReasons";

		// Token: 0x0400024E RID: 590
		private const string ProxiedClientIPAddressPropertyName = "ProxiedClientIPAddress";

		// Token: 0x0400024F RID: 591
		private const string ProxiedClientHostnamePropertyName = "ProxiedClientHostname";

		// Token: 0x04000250 RID: 592
		private const string OriginalDsnSenderPropertyName = "OriginalDsnSender";

		// Token: 0x04000251 RID: 593
		private const string ProxyHopPropertyName = "ProxyHop";

		// Token: 0x04000252 RID: 594
		private const string LogComponentName = "MessageTrackingLogs";

		// Token: 0x04000253 RID: 595
		private const string RecipientTypePropertyName = "RecipientType";

		// Token: 0x04000254 RID: 596
		private const string PoisonMessageComponentId = "PoisonMessageNotification";

		// Token: 0x04000255 RID: 597
		private const string BadmailReasonPropertyName = "BadmailReason";

		// Token: 0x04000256 RID: 598
		private static readonly string[] Fields = MessageTrackingLog.InitializeFields();

		// Token: 0x04000257 RID: 599
		private static readonly string[] recipientTypeStringValues = new string[]
		{
			"Unknown",
			"To",
			"Cc",
			"Bcc",
			"Redirect"
		};

		// Token: 0x04000258 RID: 600
		private static LogSchema msgTrackingSchema;

		// Token: 0x04000259 RID: 601
		private static Log log;

		// Token: 0x0400025A RID: 602
		private static bool enabled;

		// Token: 0x0400025B RID: 603
		private static string hostName;

		// Token: 0x02000088 RID: 136
		public class PropertyBag : Dictionary<string, object>
		{
			// Token: 0x06000471 RID: 1137 RVA: 0x00014450 File Offset: 0x00012650
			public PropertyBag(int capacity) : base(capacity)
			{
			}
		}
	}
}
