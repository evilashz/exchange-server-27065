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
	// Token: 0x02000086 RID: 134
	internal interface IMessageTrackingLog
	{
		// Token: 0x060003D0 RID: 976
		void Start();

		// Token: 0x060003D1 RID: 977
		void Start(string logFilePrefix);

		// Token: 0x060003D2 RID: 978
		void Stop();

		// Token: 0x060003D3 RID: 979
		void TrackLoadedMessage(MessageTrackingSource trackingSource, MessageTrackingEvent trackingEvent, TransportMailItem mailItem);

		// Token: 0x060003D4 RID: 980
		void TrackPoisonMessage(MessageTrackingSource source, IReadOnlyMailItem mailItem);

		// Token: 0x060003D5 RID: 981
		void TrackPoisonMessage(MessageTrackingSource source, MsgTrackPoisonInfo msgTrackingPoisonInfo);

		// Token: 0x060003D6 RID: 982
		void TrackPoisonMessage(MessageTrackingSource source, TransportMailItem mailItem, string messageId, MsgTrackReceiveInfo msgTrackInfo);

		// Token: 0x060003D7 RID: 983
		void TrackReceiveForApprovalRelease(TransportMailItem mailItem, string approver, string initiationMessageId);

		// Token: 0x060003D8 RID: 984
		void TrackReceiveByAgent(ITransportMailItemFacade mailItem, string sourceContext, string connectorId, long? relatedMailItemId);

		// Token: 0x060003D9 RID: 985
		void TrackReceive(MessageTrackingSource source, TransportMailItem mailItem, MsgTrackReceiveInfo msgTrackInfo);

		// Token: 0x060003DA RID: 986
		void TrackReceive(MessageTrackingSource source, TransportMailItem mailItem, string messageId, MsgTrackReceiveInfo msgTrackInfo);

		// Token: 0x060003DB RID: 987
		void TrackNotify(MsgTrackMapiSubmitInfo msgTrackInfo, bool isShadowSubmission);

		// Token: 0x060003DC RID: 988
		void TrackMapiSubmit(MsgTrackMapiSubmitInfo msgTrackInfo);

		// Token: 0x060003DD RID: 989
		void TrackResubmitCancelled(MessageTrackingSource source, IReadOnlyMailItem mailItem, ICollection recipients, SmtpResponse response, LatencyFormatter latencyFormatter);

		// Token: 0x060003DE RID: 990
		void TrackDuplicateDelivery(MessageTrackingSource source, IReadOnlyMailItem mailItem, ICollection recipients, ICollection recipientsData, string clientHostname, string serverName, LatencyFormatter latencyFormatter, string sourceContext, List<KeyValuePair<string, string>> extraEventData);

		// Token: 0x060003DF RID: 991
		void TrackDelivered(MessageTrackingSource source, IReadOnlyMailItem mailItem, ICollection recipients, ICollection recipientsData, string clientHostname, string serverName, LatencyFormatter latencyFormatter, string sourceContext, List<KeyValuePair<string, string>> extraEventData);

		// Token: 0x060003E0 RID: 992
		void TrackExpiredMessageDropped(MessageTrackingSource source, IReadOnlyMailItem mailItem, ICollection recipients, SmtpResponse response);

		// Token: 0x060003E1 RID: 993
		void TrackProcessed(MessageTrackingSource source, IReadOnlyMailItem mailItem, ICollection recipients, ICollection recipientsSource, LatencyFormatter latencyFormatter, string sourceContext, List<KeyValuePair<string, string>> extraEventData);

		// Token: 0x060003E2 RID: 994
		void TrackRecipientAdd(MessageTrackingSource source, TransportMailItem mailItem, RoutingAddress recipEmail, RecipientP2Type? recipientType, string agentName);

		// Token: 0x060003E3 RID: 995
		void TrackRecipientAddByAgent(ITransportMailItemFacade mailItem, string recipEmail, RecipientP2Type recipientType, string agentName);

		// Token: 0x060003E4 RID: 996
		void TrackFailedRecipients(MessageTrackingSource source, string sourceContext, IReadOnlyMailItem mailItem, string relatedRecipientAddress, ICollection<MailRecipient> recipients, SmtpResponse smtpResponse, LatencyFormatter latencyFormatter);

		// Token: 0x060003E5 RID: 997
		void TrackRecipientFail(MessageTrackingSource source, TransportMailItem mailItem, RoutingAddress recipEmail, SmtpResponse smtpResponse, string sourceContext, LatencyFormatter latencyFormatter);

		// Token: 0x060003E6 RID: 998
		void TrackRecipientDrop(MessageTrackingSource source, TransportMailItem mailItem, RoutingAddress recipEmail, SmtpResponse smtpResponse, string sourceContext, LatencyFormatter latencyFormatter);

		// Token: 0x060003E7 RID: 999
		void TrackAgentInfo(TransportMailItem mailItem);

		// Token: 0x060003E8 RID: 1000
		void TrackPoisonMessageDeleted(MessageTrackingSource source, string sourceContext, IReadOnlyMailItem item);

		// Token: 0x060003E9 RID: 1001
		void TrackRejectCommand(MessageTrackingSource source, string sourceContext, AckDetails ackDetails, SmtpResponse smtpResponse);

		// Token: 0x060003EA RID: 1002
		void TrackRelayedAndFailed(MessageTrackingSource source, IReadOnlyMailItem mailItem, IEnumerable<MailRecipient> recipients, AckDetails ackDetails);

		// Token: 0x060003EB RID: 1003
		void TrackRelayedAndFailed(MessageTrackingSource source, string sourceContext, IReadOnlyMailItem mailItem, IEnumerable<MailRecipient> recipients, AckDetails ackDetails, SmtpResponse smtpResponse, LatencyFormatter latencyFormatter);

		// Token: 0x060003EC RID: 1004
		void TrackDSN(TransportMailItem dsnMailItem, MsgTrackDSNInfo dsnInfo);

		// Token: 0x060003ED RID: 1005
		void TrackAgentGeneratedMessageRejected(MessageTrackingSource source, bool loopDetectionEnabled, IReadOnlyMailItem mailItem);

		// Token: 0x060003EE RID: 1006
		void TrackBadmail(MessageTrackingSource source, MsgTrackReceiveInfo msgTrackInfo, IReadOnlyMailItem mailItem, string badmailReason);

		// Token: 0x060003EF RID: 1007
		void TrackDefer(MessageTrackingSource source, IReadOnlyMailItem mailItem, string sourceContext);

		// Token: 0x060003F0 RID: 1008
		void TrackDefer(MessageTrackingSource source, string messageTrackingSourceContext, IReadOnlyMailItem mailItem, IEnumerable<MailRecipient> recipientsToTrack, AckDetails ackDetails);

		// Token: 0x060003F1 RID: 1009
		void TrackThrottle(MessageTrackingSource source, MsgTrackMapiSubmitInfo msgTrackInfo, string senderAddress, string messageId);

		// Token: 0x060003F2 RID: 1010
		void TrackThrottle(MessageTrackingSource source, MsgTrackMapiSubmitInfo msgTrackInfo, string senderAddress, string messageId, MailDirectionality direction);

		// Token: 0x060003F3 RID: 1011
		void TrackThrottle(MessageTrackingSource source, IReadOnlyMailItem mailItem, IPAddress serverIPAddress, string sourceContext, string reference, ProxyAddress recipient, string recipientData);

		// Token: 0x060003F4 RID: 1012
		void TrackResolve(MessageTrackingSource source, IReadOnlyMailItem mailItem, MsgTrackResolveInfo msgTrackInfo);

		// Token: 0x060003F5 RID: 1013
		void TrackExpand<T>(MessageTrackingSource source, IReadOnlyMailItem mailItem, MsgTrackExpandInfo msgTrackInfo, ICollection<T> recipients);

		// Token: 0x060003F6 RID: 1014
		void TrackExpandEvent<T>(MessageTrackingSource source, IReadOnlyMailItem mailItem, MsgTrackExpandInfo msgTrackInfo, ICollection<T> recipients, MessageTrackingEvent trackingEvent);

		// Token: 0x060003F7 RID: 1015
		void TrackRedirect(MessageTrackingSource source, IReadOnlyMailItem mailItem, MsgTrackRedirectInfo msgTrackInfo);

		// Token: 0x060003F8 RID: 1016
		void TrackRedirectEvent(MessageTrackingSource source, IReadOnlyMailItem mailItem, MsgTrackRedirectInfo msgTrackInfo, MessageTrackingEvent redirectEvent);

		// Token: 0x060003F9 RID: 1017
		void TrackRedirectToDomain(MessageTrackingSource source, IReadOnlyMailItem mailItem, MsgTrackRedirectInfo msgTrackInfo, MailRecipient mailRecipient);

		// Token: 0x060003FA RID: 1018
		void TrackTransfer(MessageTrackingSource source, IReadOnlyMailItem mailItem, long relatedMailItemId, string sourceContext);

		// Token: 0x060003FB RID: 1019
		void TrackHighAvailabilityRedirect(MessageTrackingSource source, IReadOnlyMailItem mailItem, ICollection<MailRecipient> redirectedRecipients, string sourceContext);

		// Token: 0x060003FC RID: 1020
		void TrackHighAvailabilityRedirect(MessageTrackingSource source, IReadOnlyMailItem mailItem, string sourceContext);

		// Token: 0x060003FD RID: 1021
		void TrackHighAvailabilityRedirectFail(MessageTrackingSource source, IReadOnlyMailItem mailItem, string sourceContext);

		// Token: 0x060003FE RID: 1022
		void TrackHighAvailabilityReceive(MessageTrackingSource source, string primaryServerFqdn, IReadOnlyMailItem mailItem);

		// Token: 0x060003FF RID: 1023
		void TrackHighAvailabilityDiscard(MessageTrackingSource source, IReadOnlyMailItem mailItem, string reason);

		// Token: 0x06000400 RID: 1024
		void TrackResubmit(MessageTrackingSource source, TransportMailItem newMailItem, IReadOnlyMailItem originalMailItem, string sourceContext);

		// Token: 0x06000401 RID: 1025
		void TrackInitMessageCreated(MessageTrackingSource source, ICollection<MailRecipient> moderatedRecipients, IReadOnlyMailItem originalMailItem, TransportMailItem initiationMailItem, string initiationMessageIdentifier, string sourceContext);

		// Token: 0x06000402 RID: 1026
		void TrackModeratorsAllNdr(MessageTrackingSource source, string initiationMessageId, string originalMessageId, string originalSenderAddress, ICollection<string> recipientAddresses, OrganizationId organizationId);

		// Token: 0x06000403 RID: 1027
		void TrackModeratorExpired(MessageTrackingSource source, string initiationMessageId, string originalMessageId, string originalSenderAddress, ICollection<string> recipientAddresses, OrganizationId organizationId, bool isNotificationSent);

		// Token: 0x06000404 RID: 1028
		void TrackModeratorDecision(MessageTrackingSource source, string initiationMessageId, string originalMessageId, string originalSenderAddress, ICollection<string> recipientAddresses, bool isApproved, OrganizationId organizationId);

		// Token: 0x06000405 RID: 1029
		void TrackMeetingMessage(string internetMessageID, string clientName, OrganizationId organizationID, List<KeyValuePair<string, string>> extraEventData);

		// Token: 0x06000406 RID: 1030
		void Configure(Server serverConfig);

		// Token: 0x06000407 RID: 1031
		void FlushBuffer();
	}
}
