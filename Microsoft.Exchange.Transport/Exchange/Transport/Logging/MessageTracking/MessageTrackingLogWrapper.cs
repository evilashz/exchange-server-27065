using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Logging.MessageTracking
{
	// Token: 0x02000089 RID: 137
	internal sealed class MessageTrackingLogWrapper : IMessageTrackingLog
	{
		// Token: 0x06000472 RID: 1138 RVA: 0x00014459 File Offset: 0x00012659
		public void Start()
		{
			MessageTrackingLog.Start();
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00014460 File Offset: 0x00012660
		public void Start(string logFilePrefix)
		{
			MessageTrackingLog.Start(logFilePrefix);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00014468 File Offset: 0x00012668
		public void Stop()
		{
			MessageTrackingLog.Stop();
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001446F File Offset: 0x0001266F
		public void TrackLoadedMessage(MessageTrackingSource trackingSource, MessageTrackingEvent trackingEvent, TransportMailItem mailItem)
		{
			MessageTrackingLog.TrackLoadedMessage(trackingSource, trackingEvent, mailItem);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00014479 File Offset: 0x00012679
		public void TrackPoisonMessage(MessageTrackingSource source, IReadOnlyMailItem mailItem)
		{
			MessageTrackingLog.TrackPoisonMessage(source, mailItem);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00014482 File Offset: 0x00012682
		public void TrackPoisonMessage(MessageTrackingSource source, MsgTrackPoisonInfo msgTrackingPoisonInfo)
		{
			MessageTrackingLog.TrackPoisonMessage(source, msgTrackingPoisonInfo);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001448B File Offset: 0x0001268B
		public void TrackPoisonMessage(MessageTrackingSource source, TransportMailItem mailItem, string messageId, MsgTrackReceiveInfo msgTrackInfo)
		{
			MessageTrackingLog.TrackPoisonMessage(source, mailItem, messageId, msgTrackInfo);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00014497 File Offset: 0x00012697
		public void TrackReceiveForApprovalRelease(TransportMailItem mailItem, string approver, string initiationMessageId)
		{
			MessageTrackingLog.TrackReceiveForApprovalRelease(mailItem, approver, initiationMessageId);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000144A1 File Offset: 0x000126A1
		public void TrackReceiveByAgent(ITransportMailItemFacade mailItem, string sourceContext, string connectorId, long? relatedMailItemId)
		{
			MessageTrackingLog.TrackReceiveByAgent(mailItem, sourceContext, connectorId, relatedMailItemId);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000144AD File Offset: 0x000126AD
		public void TrackReceive(MessageTrackingSource source, TransportMailItem mailItem, MsgTrackReceiveInfo msgTrackInfo)
		{
			MessageTrackingLog.TrackReceive(source, mailItem, msgTrackInfo);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000144B7 File Offset: 0x000126B7
		public void TrackReceive(MessageTrackingSource source, TransportMailItem mailItem, string messageId, MsgTrackReceiveInfo msgTrackInfo)
		{
			MessageTrackingLog.TrackReceive(source, mailItem, messageId, msgTrackInfo);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000144C3 File Offset: 0x000126C3
		public void TrackNotify(MsgTrackMapiSubmitInfo msgTrackInfo, bool isShadowSubmission)
		{
			MessageTrackingLog.TrackNotify(msgTrackInfo, isShadowSubmission);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000144CC File Offset: 0x000126CC
		public void TrackMapiSubmit(MsgTrackMapiSubmitInfo msgTrackInfo)
		{
			MessageTrackingLog.TrackMapiSubmit(msgTrackInfo);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x000144D4 File Offset: 0x000126D4
		public void TrackResubmitCancelled(MessageTrackingSource source, IReadOnlyMailItem mailItem, ICollection recipients, SmtpResponse response, LatencyFormatter latencyFormatter)
		{
			MessageTrackingLog.TrackResubmitCancelled(source, mailItem, recipients, response, latencyFormatter);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000144E4 File Offset: 0x000126E4
		public void TrackDuplicateDelivery(MessageTrackingSource source, IReadOnlyMailItem mailItem, ICollection recipients, ICollection recipientsData, string clientHostname, string serverName, LatencyFormatter latencyFormatter, string sourceContext, List<KeyValuePair<string, string>> extraEventData)
		{
			MessageTrackingLog.TrackDuplicateDelivery(source, mailItem, recipients, recipientsData, clientHostname, serverName, latencyFormatter, sourceContext, extraEventData);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00014508 File Offset: 0x00012708
		public void TrackDelivered(MessageTrackingSource source, IReadOnlyMailItem mailItem, ICollection recipients, ICollection recipientsData, string clientHostname, string serverName, LatencyFormatter latencyFormatter, string sourceContext, List<KeyValuePair<string, string>> extraEventData)
		{
			MessageTrackingLog.TrackDelivered(source, mailItem, recipients, recipientsData, clientHostname, serverName, latencyFormatter, sourceContext, extraEventData);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00014529 File Offset: 0x00012729
		public void TrackExpiredMessageDropped(MessageTrackingSource source, IReadOnlyMailItem mailItem, ICollection recipients, SmtpResponse response)
		{
			MessageTrackingLog.TrackExpiredMessageDropped(source, mailItem, recipients, response);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00014535 File Offset: 0x00012735
		public void TrackProcessed(MessageTrackingSource source, IReadOnlyMailItem mailItem, ICollection recipients, ICollection recipientsSource, LatencyFormatter latencyFormatter, string sourceContext, List<KeyValuePair<string, string>> extraEventData)
		{
			MessageTrackingLog.TrackProcessed(source, mailItem, recipients, recipientsSource, latencyFormatter, sourceContext, extraEventData);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00014547 File Offset: 0x00012747
		public void TrackRecipientAdd(MessageTrackingSource source, TransportMailItem mailItem, RoutingAddress recipEmail, RecipientP2Type? recipientType, string agentName)
		{
			MessageTrackingLog.TrackRecipientAdd(source, mailItem, recipEmail, recipientType, agentName);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00014555 File Offset: 0x00012755
		public void TrackRecipientAddByAgent(ITransportMailItemFacade mailItem, string recipEmail, RecipientP2Type recipientType, string agentName)
		{
			MessageTrackingLog.TrackRecipientAddByAgent(mailItem, recipEmail, recipientType, agentName);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00014561 File Offset: 0x00012761
		public void TrackFailedRecipients(MessageTrackingSource source, string sourceContext, IReadOnlyMailItem mailItem, string relatedRecipientAddress, ICollection<MailRecipient> recipients, SmtpResponse smtpResponse, LatencyFormatter latencyFormatter)
		{
			MessageTrackingLog.TrackFailedRecipients(source, sourceContext, mailItem, relatedRecipientAddress, recipients, smtpResponse, latencyFormatter);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00014573 File Offset: 0x00012773
		public void TrackRecipientFail(MessageTrackingSource source, TransportMailItem mailItem, RoutingAddress recipEmail, SmtpResponse smtpResponse, string sourceContext, LatencyFormatter latencyFormatter)
		{
			MessageTrackingLog.TrackRecipientFail(source, mailItem, recipEmail, smtpResponse, sourceContext, latencyFormatter);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00014583 File Offset: 0x00012783
		public void TrackRecipientDrop(MessageTrackingSource source, TransportMailItem mailItem, RoutingAddress recipEmail, SmtpResponse smtpResponse, string sourceContext, LatencyFormatter latencyFormatter)
		{
			MessageTrackingLog.TrackRecipientDrop(source, mailItem, recipEmail, smtpResponse, sourceContext, latencyFormatter);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00014593 File Offset: 0x00012793
		public void TrackAgentInfo(TransportMailItem mailItem)
		{
			MessageTrackingLog.TrackAgentInfo(mailItem);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0001459B File Offset: 0x0001279B
		public void TrackPoisonMessageDeleted(MessageTrackingSource source, string sourceContext, IReadOnlyMailItem item)
		{
			MessageTrackingLog.TrackPoisonMessageDeleted(source, sourceContext, item);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000145A5 File Offset: 0x000127A5
		public void TrackRejectCommand(MessageTrackingSource source, string sourceContext, AckDetails ackDetails, SmtpResponse smtpResponse)
		{
			MessageTrackingLog.TrackRejectCommand(source, sourceContext, ackDetails, smtpResponse);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x000145B1 File Offset: 0x000127B1
		public void TrackRelayedAndFailed(MessageTrackingSource source, IReadOnlyMailItem mailItem, IEnumerable<MailRecipient> recipients, AckDetails ackDetails)
		{
			MessageTrackingLog.TrackRelayedAndFailed(source, mailItem, recipients, ackDetails);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000145BD File Offset: 0x000127BD
		public void TrackRelayedAndFailed(MessageTrackingSource source, string sourceContext, IReadOnlyMailItem mailItem, IEnumerable<MailRecipient> recipients, AckDetails ackDetails, SmtpResponse smtpResponse, LatencyFormatter latencyFormatter)
		{
			MessageTrackingLog.TrackRelayedAndFailed(source, sourceContext, mailItem, recipients, ackDetails, smtpResponse, latencyFormatter);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x000145CF File Offset: 0x000127CF
		public void TrackDSN(TransportMailItem dsnMailItem, MsgTrackDSNInfo dsnInfo)
		{
			MessageTrackingLog.TrackDSN(dsnMailItem, dsnInfo);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x000145D8 File Offset: 0x000127D8
		public void TrackAgentGeneratedMessageRejected(MessageTrackingSource source, bool loopDetectionEnabled, IReadOnlyMailItem mailItem)
		{
			MessageTrackingLog.TrackAgentGeneratedMessageRejected(source, loopDetectionEnabled, mailItem);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x000145E2 File Offset: 0x000127E2
		public void TrackBadmail(MessageTrackingSource source, MsgTrackReceiveInfo msgTrackInfo, IReadOnlyMailItem mailItem, string badmailReason)
		{
			MessageTrackingLog.TrackBadmail(source, msgTrackInfo, mailItem, badmailReason);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000145EE File Offset: 0x000127EE
		public void TrackDefer(MessageTrackingSource source, IReadOnlyMailItem mailItem, string sourceContext)
		{
			MessageTrackingLog.TrackDefer(source, mailItem, sourceContext);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x000145F8 File Offset: 0x000127F8
		public void TrackDefer(MessageTrackingSource source, string messageTrackingSourceContext, IReadOnlyMailItem mailItem, IEnumerable<MailRecipient> recipientsToTrack, AckDetails ackDetails)
		{
			MessageTrackingLog.TrackDefer(source, messageTrackingSourceContext, mailItem, recipientsToTrack, ackDetails);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00014606 File Offset: 0x00012806
		public void TrackThrottle(MessageTrackingSource source, MsgTrackMapiSubmitInfo msgTrackInfo, string senderAddress, string messageId)
		{
			MessageTrackingLog.TrackThrottle(source, msgTrackInfo, senderAddress, messageId);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00014612 File Offset: 0x00012812
		public void TrackThrottle(MessageTrackingSource source, MsgTrackMapiSubmitInfo msgTrackInfo, string senderAddress, string messageId, MailDirectionality direction)
		{
			MessageTrackingLog.TrackThrottle(source, msgTrackInfo, senderAddress, messageId, direction);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00014620 File Offset: 0x00012820
		public void TrackThrottle(MessageTrackingSource source, IReadOnlyMailItem mailItem, IPAddress serverIPAddress, string sourceContext, string reference, ProxyAddress recipient, string recipientData)
		{
			MessageTrackingLog.TrackThrottle(source, mailItem, serverIPAddress, sourceContext, reference, recipient, recipientData);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00014632 File Offset: 0x00012832
		public void TrackResolve(MessageTrackingSource source, IReadOnlyMailItem mailItem, MsgTrackResolveInfo msgTrackInfo)
		{
			MessageTrackingLog.TrackResolve(source, mailItem, msgTrackInfo);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001463C File Offset: 0x0001283C
		public void TrackExpand<T>(MessageTrackingSource source, IReadOnlyMailItem mailItem, MsgTrackExpandInfo msgTrackInfo, ICollection<T> recipients)
		{
			MessageTrackingLog.TrackExpand<T>(source, mailItem, msgTrackInfo, recipients);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00014648 File Offset: 0x00012848
		public void TrackExpandEvent<T>(MessageTrackingSource source, IReadOnlyMailItem mailItem, MsgTrackExpandInfo msgTrackInfo, ICollection<T> recipients, MessageTrackingEvent trackingEvent)
		{
			MessageTrackingLog.TrackExpandEvent<T>(source, mailItem, msgTrackInfo, recipients, trackingEvent);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00014656 File Offset: 0x00012856
		public void TrackRedirect(MessageTrackingSource source, IReadOnlyMailItem mailItem, MsgTrackRedirectInfo msgTrackInfo)
		{
			MessageTrackingLog.TrackRedirect(source, mailItem, msgTrackInfo);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00014660 File Offset: 0x00012860
		public void TrackRedirectEvent(MessageTrackingSource source, IReadOnlyMailItem mailItem, MsgTrackRedirectInfo msgTrackInfo, MessageTrackingEvent redirectEvent)
		{
			MessageTrackingLog.TrackRedirectEvent(source, mailItem, msgTrackInfo, redirectEvent);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0001466C File Offset: 0x0001286C
		public void TrackRedirectToDomain(MessageTrackingSource source, IReadOnlyMailItem mailItem, MsgTrackRedirectInfo msgTrackInfo, MailRecipient mailRecipient)
		{
			MessageTrackingLog.TrackRedirectToDomain(source, mailItem, msgTrackInfo, mailRecipient);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00014678 File Offset: 0x00012878
		public void TrackTransfer(MessageTrackingSource source, IReadOnlyMailItem mailItem, long relatedMailItemId, string sourceContext)
		{
			MessageTrackingLog.TrackTransfer(source, mailItem, relatedMailItemId, sourceContext);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00014684 File Offset: 0x00012884
		public void TrackHighAvailabilityRedirect(MessageTrackingSource source, IReadOnlyMailItem mailItem, ICollection<MailRecipient> redirectedRecipients, string sourceContext)
		{
			MessageTrackingLog.TrackHighAvailabilityRedirect(source, mailItem, redirectedRecipients, sourceContext);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00014690 File Offset: 0x00012890
		public void TrackHighAvailabilityRedirect(MessageTrackingSource source, IReadOnlyMailItem mailItem, string sourceContext)
		{
			MessageTrackingLog.TrackHighAvailabilityRedirect(source, mailItem, sourceContext);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001469A File Offset: 0x0001289A
		public void TrackHighAvailabilityRedirectFail(MessageTrackingSource source, IReadOnlyMailItem mailItem, string sourceContext)
		{
			MessageTrackingLog.TrackHighAvailabilityRedirectFail(source, mailItem, sourceContext);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000146A4 File Offset: 0x000128A4
		public void TrackHighAvailabilityReceive(MessageTrackingSource source, string primaryServerFqdn, IReadOnlyMailItem mailItem)
		{
			MessageTrackingLog.TrackHighAvailabilityReceive(source, primaryServerFqdn, mailItem);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000146AE File Offset: 0x000128AE
		public void TrackHighAvailabilityDiscard(MessageTrackingSource source, IReadOnlyMailItem mailItem, string reason)
		{
			MessageTrackingLog.TrackHighAvailabilityDiscard(source, mailItem, reason);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000146B8 File Offset: 0x000128B8
		public void TrackResubmit(MessageTrackingSource source, TransportMailItem newMailItem, IReadOnlyMailItem originalMailItem, string sourceContext)
		{
			MessageTrackingLog.TrackResubmit(source, newMailItem, originalMailItem, sourceContext);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x000146C4 File Offset: 0x000128C4
		public void TrackInitMessageCreated(MessageTrackingSource source, ICollection<MailRecipient> moderatedRecipients, IReadOnlyMailItem originalMailItem, TransportMailItem initiationMailItem, string initiationMessageIdentifier, string sourceContext)
		{
			MessageTrackingLog.TrackInitMessageCreated(source, moderatedRecipients, originalMailItem, initiationMailItem, initiationMessageIdentifier, sourceContext);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000146D4 File Offset: 0x000128D4
		public void TrackModeratorsAllNdr(MessageTrackingSource source, string initiationMessageId, string originalMessageId, string originalSenderAddress, ICollection<string> recipientAddresses, OrganizationId organizationId)
		{
			MessageTrackingLog.TrackModeratorsAllNdr(source, initiationMessageId, originalMessageId, originalSenderAddress, recipientAddresses, organizationId);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x000146E4 File Offset: 0x000128E4
		public void TrackModeratorExpired(MessageTrackingSource source, string initiationMessageId, string originalMessageId, string originalSenderAddress, ICollection<string> recipientAddresses, OrganizationId organizationId, bool isNotificationSent)
		{
			MessageTrackingLog.TrackModeratorExpired(source, initiationMessageId, originalMessageId, originalSenderAddress, recipientAddresses, organizationId, isNotificationSent);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000146F6 File Offset: 0x000128F6
		public void TrackModeratorDecision(MessageTrackingSource source, string initiationMessageId, string originalMessageId, string originalSenderAddress, ICollection<string> recipientAddresses, bool isApproved, OrganizationId organizationId)
		{
			MessageTrackingLog.TrackModeratorDecision(source, initiationMessageId, originalMessageId, originalSenderAddress, recipientAddresses, isApproved, organizationId);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00014708 File Offset: 0x00012908
		public void TrackMeetingMessage(string internetMessageID, string clientName, OrganizationId organizationID, List<KeyValuePair<string, string>> extraEventData)
		{
			MessageTrackingLog.TrackMeetingMessage(internetMessageID, clientName, organizationID, extraEventData);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00014714 File Offset: 0x00012914
		public void Configure(Server serverConfig)
		{
			MessageTrackingLog.Configure(serverConfig);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001471C File Offset: 0x0001291C
		public void FlushBuffer()
		{
			MessageTrackingLog.FlushBuffer();
		}
	}
}
