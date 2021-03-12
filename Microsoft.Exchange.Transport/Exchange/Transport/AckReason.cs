using System;
using System.Collections.Generic;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000140 RID: 320
	internal static class AckReason
	{
		// Token: 0x06000E39 RID: 3641 RVA: 0x00034EC8 File Offset: 0x000330C8
		public static SmtpResponse MimeToMimeInvalidContent(string errorDetails)
		{
			return new SmtpResponse("550", "5.6.0", new string[]
			{
				errorDetails
			});
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00034EF0 File Offset: 0x000330F0
		public static SmtpResponse MimeToMimeStorageError(string errorDetails)
		{
			return new SmtpResponse("550", "5.6.0", new string[]
			{
				errorDetails
			});
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00034F18 File Offset: 0x00033118
		public static SmtpResponse UnreachableMessageExpired(string unreachableReason)
		{
			return new SmtpResponse("550", "4.4.7", new string[]
			{
				"QUEUE.Expired; message expired in unreachable destination queue. Reason: " + unreachableReason
			});
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x00034F4C File Offset: 0x0003314C
		public static SmtpResponse InboundPoisonMessage(int crashCount)
		{
			return new SmtpResponse("550", "5.7.0", new string[]
			{
				string.Format("STOREDRV.Deliver; message is treated as poison. Crash count = '{0}'", crashCount)
			});
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00034F83 File Offset: 0x00033183
		public static bool IsMailboxTransportDeliveryPoisonMessageResponse(SmtpResponse smtpResponse)
		{
			return string.Equals(smtpResponse.StatusCode, "550", StringComparison.Ordinal) && string.Equals(smtpResponse.EnhancedStatusCode, "5.7.0", StringComparison.Ordinal);
		}

		// Token: 0x04000622 RID: 1570
		public const string MailboxTransportPoisonResponseStatusCode = "550";

		// Token: 0x04000623 RID: 1571
		public const string InboundPoisonMessageEnhancedStatusCode = "5.7.0";

		// Token: 0x04000624 RID: 1572
		public static readonly SmtpResponse PickupInvalidAddress = new SmtpResponse("550", "5.1.3", new string[]
		{
			"PICKUP.InvalidAddress; invalid address"
		});

		// Token: 0x04000625 RID: 1573
		public static readonly SmtpResponse PickupMessageTooLarge = new SmtpResponse("550", "5.3.4", new string[]
		{
			"PICKUP.MessageSize; message size exceeds a fixed maximum size limit"
		});

		// Token: 0x04000626 RID: 1574
		public static readonly SmtpResponse PickupHeaderTooLarge = new SmtpResponse("550", "5.3.4", new string[]
		{
			"PICKUP.HeaderSize; header size exceeds a fixed maximum size limit"
		});

		// Token: 0x04000627 RID: 1575
		public static readonly SmtpResponse PickupTooManyRecipients = new SmtpResponse("550", "5.5.3", new string[]
		{
			"PICKUP.RecipLimit; too many recipients"
		});

		// Token: 0x04000628 RID: 1576
		public static readonly SmtpResponse MessageDelayedDeleteByAdmin = new SmtpResponse("550", "4.3.2", new string[]
		{
			"QUEUE.DDAdmin; message deleted by administrative action"
		});

		// Token: 0x04000629 RID: 1577
		public static readonly SmtpResponse MessageDeletedByAdmin = new SmtpResponse("550", "4.3.2", new string[]
		{
			"QUEUE.Admin; message deleted by administrative action"
		});

		// Token: 0x0400062A RID: 1578
		public static readonly SmtpResponse MessageDeletedByTransportAgent = new SmtpResponse("550", "4.3.2", new string[]
		{
			"QUEUE.TransportAgent; message deleted by transport agent"
		});

		// Token: 0x0400062B RID: 1579
		public static readonly SmtpResponse PoisonMessageDeletedByAdmin = new SmtpResponse("550", "4.3.2", new string[]
		{
			"QUEUE.PoisonAdmin; message deleted by administrative action"
		});

		// Token: 0x0400062C RID: 1580
		public static readonly SmtpResponse MessageExpired = new SmtpResponse("550", "4.4.7", new string[]
		{
			"QUEUE.Expired; message expired"
		});

		// Token: 0x0400062D RID: 1581
		public static readonly SmtpResponse MessageTooLargeForHighPriority = new SmtpResponse("550", "5.2.3", new string[]
		{
			"QUEUE.Priority; message too large to be sent with high priority"
		});

		// Token: 0x0400062E RID: 1582
		public static readonly SmtpResponse ReplayMessageTooLarge = new SmtpResponse("550", "5.3.4", new string[]
		{
			"REPLAY.MessageSize; message size exceeds a fixed maximum size limit"
		});

		// Token: 0x0400062F RID: 1583
		public static readonly SmtpResponse AmbiguousAddressPermanent = new SmtpResponse("550", "5.1.4", new string[]
		{
			"RESOLVER.ADR.Ambiguous; ambiguous address"
		});

		// Token: 0x04000630 RID: 1584
		public static readonly SmtpResponse AmbiguousAddressTransient = new SmtpResponse("420", "4.2.0", new string[]
		{
			"RESOLVER.ADR.Ambiguous; ambiguous address"
		});

		// Token: 0x04000631 RID: 1585
		public static readonly SmtpResponse BadOrMissingPrimarySmtpAddress = new SmtpResponse("550", "5.2.0", new string[]
		{
			"RESOLVER.ADR.BadPrimary; recipient primary SMTP address is missing or invalid"
		});

		// Token: 0x04000632 RID: 1586
		public static readonly SmtpResponse EncapsulatedSmtpAddress = new SmtpResponse("550", "5.1.0", new string[]
		{
			"RESOLVER.ADR.SmtpInSmtp; encapsulated SMTP address inside an SMTP address (IMCEASMTP-)"
		});

		// Token: 0x04000633 RID: 1587
		public static readonly SmtpResponse EncapsulatedX500Address = new SmtpResponse("550", "5.1.0", new string[]
		{
			"RESOLVER.ADR.X500InSmtp; encapsulated X.500 address inside an SMTP address (IMCEAX500-)"
		});

		// Token: 0x04000634 RID: 1588
		public static readonly SmtpResponse EncapsulatedInvalidAddress = new SmtpResponse("550", "5.1.0", new string[]
		{
			"RESOLVER.ADR.InvalidInSmtp; encapsulated INVALID address inside an SMTP address (IMCEAINVALID-)"
		});

		// Token: 0x04000635 RID: 1589
		public static readonly SmtpResponse InvalidDirectoryObject = new SmtpResponse("550", "5.2.0", new string[]
		{
			"RESOLVER.ADR.Invalid; the recipient's directory object is misconfigured"
		});

		// Token: 0x04000636 RID: 1590
		public static readonly SmtpResponse InvalidObjectOnSearch = new SmtpResponse("550", "5.2.0", new string[]
		{
			"RESOLVER.ADR.InvalidResult; the recipient's directory object is misconfigured"
		});

		// Token: 0x04000637 RID: 1591
		public static readonly SmtpResponse LocalRecipientAddressUnknown = new SmtpResponse("550", "5.1.1", new string[]
		{
			"RESOLVER.ADR.RecipNotFound; not found"
		});

		// Token: 0x04000638 RID: 1592
		public static readonly SmtpResponse LocalRecipientExAddressUnknown = new SmtpResponse("550", "5.1.1", new string[]
		{
			"RESOLVER.ADR.ExRecipNotFound; not found"
		});

		// Token: 0x04000639 RID: 1593
		public static readonly SmtpResponse LocalRecipientX400AddressUnknown = new SmtpResponse("550", "5.1.1", new string[]
		{
			"RESOLVER.ADR.X400RecipNotFound; not found"
		});

		// Token: 0x0400063A RID: 1594
		public static readonly SmtpResponse UnencapsulatableTargetAddress = new SmtpResponse("550", "5.2.0", new string[]
		{
			"RESOLVER.CON.UnencapTarget; contact's configured external address is unroutable"
		});

		// Token: 0x0400063B RID: 1595
		public static readonly SmtpResponse ContactChainNotMailEnabled = new SmtpResponse("550", "5.2.0", new string[]
		{
			"RESOLVER.CON.ChainDisabled; misconfigured forwarded contact"
		});

		// Token: 0x0400063C RID: 1596
		public static readonly SmtpResponse ContactChainHandled = new SmtpResponse("250", "2.1.5", new string[]
		{
			"RESOLVER.CON.Chained; contact forwarded"
		});

		// Token: 0x0400063D RID: 1597
		public static readonly SmtpResponse ContactChainAmbiguousPermanent = new SmtpResponse("550", "5.2.0", new string[]
		{
			"RESOLVER.CON.Ambiguous; contact has an ambiguous forwarding address"
		});

		// Token: 0x0400063E RID: 1598
		public static readonly SmtpResponse ContactChainAmbiguousTransient = new SmtpResponse("420", "4.2.0", new string[]
		{
			"RESOLVER.CON.Ambiguous; contact has an ambiguous forwarding address"
		});

		// Token: 0x0400063F RID: 1599
		public static readonly SmtpResponse ContactChainInvalid = new SmtpResponse("550", "5.2.0", new string[]
		{
			"RESOLVER.CON.ChainInvalid; contact forwards to an invalid object"
		});

		// Token: 0x04000640 RID: 1600
		public static readonly SmtpResponse ContactInvalidTargetAddress = new SmtpResponse("550", "5.2.0", new string[]
		{
			"RESOLVER.CON.InvalidTarget; contact's target address is invalid"
		});

		// Token: 0x04000641 RID: 1601
		public static readonly SmtpResponse ContactMissingTargetAddress = new SmtpResponse("550", "5.2.0", new string[]
		{
			"RESOLVER.CON.NoTarget; contact's external address is missing"
		});

		// Token: 0x04000642 RID: 1602
		public static readonly SmtpResponse ForwardedToAlternateRecipient = new SmtpResponse("250", "2.1.5", new string[]
		{
			"RESOLVER.FWD.Forwarded; recipient forwarded"
		});

		// Token: 0x04000643 RID: 1603
		public static readonly SmtpResponse AlternateRecipientInvalid = new SmtpResponse("550", "5.2.0", new string[]
		{
			"RESOLVER.FWD.Invalid; misconfigured forwarding address"
		});

		// Token: 0x04000644 RID: 1604
		public static readonly SmtpResponse AlternateRecipientNotFound = new SmtpResponse("550", "5.2.0", new string[]
		{
			"RESOLVER.FWD.NotFound; misconfigured forwarding address"
		});

		// Token: 0x04000645 RID: 1605
		public static readonly SmtpResponse MigratedPublicFolderInvalidTargetAddress = new SmtpResponse("550", "5.2.0", new string[]
		{
			"RESOLVER.PF.InvalidTarget; migrated public folder's external address is invalid"
		});

		// Token: 0x04000646 RID: 1606
		public static readonly SmtpResponse PublicFolderMailboxesInTransit = new SmtpResponse("550", "5.2.0", new string[]
		{
			"RESOLVER.PF.InTransit; public folders unavailable due to ongoing migration"
		});

		// Token: 0x04000647 RID: 1607
		public static readonly SmtpResponse ContentMailboxInvalid = new SmtpResponse("550", "5.2.0", new string[]
		{
			"RESOLVER.PF.Invalid; misconfigured public folder mailbox"
		});

		// Token: 0x04000648 RID: 1608
		public static readonly SmtpResponse ContentMailboxRecipientNotFound = new SmtpResponse("550", "5.2.0", new string[]
		{
			"RESOLVER.PF.NotFound; public folder mailbox recipient not found"
		});

		// Token: 0x04000649 RID: 1609
		public static readonly SmtpResponse AlternateRecipientBlockedBySender = new SmtpResponse("550", "5.7.300", new string[]
		{
			"RESOLVER.FWD.Blocked; the sender prohibited alt recipient redirection on this message"
		});

		// Token: 0x0400064A RID: 1610
		public static readonly SmtpResponse DDLMisconfiguration = new SmtpResponse("550", "5.2.4", new string[]
		{
			"RESOLVER.GRP.DDLQuery; the dynamic distribution list has a misconfigured query"
		});

		// Token: 0x0400064B RID: 1611
		public static readonly SmtpResponse DLBlockedMessageType = new SmtpResponse("250", "2.1.5", new string[]
		{
			"RESOLVER.GRP.BlockedMessage; messages of this type are not delivered to groups"
		});

		// Token: 0x0400064C RID: 1612
		public static readonly SmtpResponse DLBlockedRedirectableMessageType = new SmtpResponse("250", "2.1.5", new string[]
		{
			"RESOLVER.GRP.NotRedirected; messages of this type are not delivered to groups"
		});

		// Token: 0x0400064D RID: 1613
		public static readonly SmtpResponse DLExpansionBlockedBySender = new SmtpResponse("550", "5.7.100", new string[]
		{
			"RESOLVER.GRP.Blocked; the sender prohibited DL expansion on this message"
		});

		// Token: 0x0400064E RID: 1614
		public static readonly SmtpResponse DLExpansionBlockedNeedsSenderRestrictions = new SmtpResponse("550", "5.7.1", new string[]
		{
			"RESOLVER.GRP.Blocked.NeedsSenderRestrictions; DL expansion needs sender restrictions or message approval configured"
		});

		// Token: 0x0400064F RID: 1615
		public static readonly SmtpResponse DLExpanded = new SmtpResponse("250", "2.1.5", new string[]
		{
			"RESOLVER.GRP.Expanded; distribution list expanded"
		});

		// Token: 0x04000650 RID: 1616
		public static readonly SmtpResponse DLExpandedSilently = new SmtpResponse("250", "2.1.5", new string[]
		{
			"RESOLVER.GRP.Expanded; distribution list expanded"
		});

		// Token: 0x04000651 RID: 1617
		public static readonly SmtpResponse DLRedirectedToManager = new SmtpResponse("250", "2.1.5", new string[]
		{
			"RESOLVER.GRP.ToOwner; message redirected to group owner"
		});

		// Token: 0x04000652 RID: 1618
		public static readonly SmtpResponse DLRedirectManagerNotFound = new SmtpResponse("250", "2.1.5", new string[]
		{
			"RESOLVER.GRP.NoOwner; no group owner to redirect to"
		});

		// Token: 0x04000653 RID: 1619
		public static readonly SmtpResponse DLRedirectManagerNotValid = new SmtpResponse("250", "2.1.5", new string[]
		{
			"RESOLVER.GRP.OwnerInvalid; group owner to redirect to is misconfigured"
		});

		// Token: 0x04000654 RID: 1620
		public static readonly SmtpResponse InvalidGroupForExpansion = new SmtpResponse("550", "5.2.0", new string[]
		{
			"RESOLVER.GRP.InvalidObject; the group's directory object is misconfigured"
		});

		// Token: 0x04000655 RID: 1621
		public static readonly SmtpResponse ExpansionLoopDetected = new SmtpResponse("550", "5.4.6", new string[]
		{
			"RESOLVER.FWD.Loop; there is a forwarding loop configured in the directory"
		});

		// Token: 0x04000656 RID: 1622
		public static readonly SmtpResponse SilentExpansionLoopDetected = new SmtpResponse("250", "2.1.5", new string[]
		{
			"RESOLVER.ADR.AlreadyExpanded; already expanded this recipient"
		});

		// Token: 0x04000657 RID: 1623
		public static readonly SmtpResponse DuplicateRecipient = new SmtpResponse("250", "2.1.5", new string[]
		{
			"CAT.DuplicateRecipient; recipient already present"
		});

		// Token: 0x04000658 RID: 1624
		public static readonly SmtpResponse BlockExternalOofToInternalUser = new SmtpResponse("250", "2.1.5", new string[]
		{
			"RESOLVER.OOF.ExtToInt; handled external OOF addressed to internal recipient"
		});

		// Token: 0x04000659 RID: 1625
		public static readonly SmtpResponse BlockLegacyOofToExternalUser = new SmtpResponse("250", "2.1.5", new string[]
		{
			"RESOLVER.OOF.LegacyToExt; handled legacy OOF addressed to external recipient"
		});

		// Token: 0x0400065A RID: 1626
		public static readonly SmtpResponse BlockInternalOofToExternalUser = new SmtpResponse("250", "2.1.5", new string[]
		{
			"RESOLVER.OOF.IntToExt; handled internal OOF addressed to external recipient"
		});

		// Token: 0x0400065B RID: 1627
		public static readonly SmtpResponse BlockInternalOofToInternalOpenDomainUser = new SmtpResponse("250", "2.1.5", new string[]
		{
			"RESOLVER.OOF.ExtToOpenDomainInt; handled external OOF addressed to internal recipient of an open domain"
		});

		// Token: 0x0400065C RID: 1628
		public static readonly SmtpResponse BlockExternalOofToExternalUser = new SmtpResponse("250", "2.1.5", new string[]
		{
			"RESOLVER.OOF.SuppressExternal; external OOFs are suppressed"
		});

		// Token: 0x0400065D RID: 1629
		public static readonly SmtpResponse BlockDRToExternalUser = new SmtpResponse("250", "2.1.5", new string[]
		{
			"RESOLVER.MSGTYPE.DR; handled DR addressed to external recipient"
		});

		// Token: 0x0400065E RID: 1630
		public static readonly SmtpResponse BlockNDRToExternalUser = new SmtpResponse("250", "2.1.5", new string[]
		{
			"RESOLVER.MSGTYPE.NDR; handled NDR addressed to external recipient"
		});

		// Token: 0x0400065F RID: 1631
		public static readonly SmtpResponse BlockMFNToExternalUser = new SmtpResponse("250", "2.1.5", new string[]
		{
			"RESOLVER.MSGTYPE.NDR; handled MFN addressed to external recipient"
		});

		// Token: 0x04000660 RID: 1632
		public static readonly SmtpResponse BlockARToExternalUser = new SmtpResponse("250", "2.1.5", new string[]
		{
			"RESOLVER.MSGTYPE.AR; handled AutoReply addressed to external recipient"
		});

		// Token: 0x04000661 RID: 1633
		public static readonly SmtpResponse BlockAFToExternalUser = new SmtpResponse("250", "2.1.5", new string[]
		{
			"RESOLVER.MSGTYPE.AF; handled AutoForward addressed to external recipient"
		});

		// Token: 0x04000662 RID: 1634
		public static readonly SmtpResponse RecipientPermissionRestricted = new SmtpResponse("550", "5.7.1", new string[]
		{
			"RESOLVER.RST.NotAuthorized; not authorized"
		});

		// Token: 0x04000663 RID: 1635
		public static readonly SmtpResponse RecipientPermissionRestrictedToGroup = new SmtpResponse("550", "5.7.1", new string[]
		{
			"RESOLVER.RST.NotAuthorizedToGroup; not authorized to send to the distribution list"
		});

		// Token: 0x04000664 RID: 1636
		public static readonly SmtpResponse JournalReportFromUnauthorizedSender = new SmtpResponse("550", "5.7.1", new string[]
		{
			"RESOLVER.RST.JournalReportFromUnauthorizedSender; not authorized to send journal reports"
		});

		// Token: 0x04000665 RID: 1637
		public static readonly SmtpResponse MessageTooLargeForReceiver = new SmtpResponse("550", "5.2.3", new string[]
		{
			"RESOLVER.RST.RecipSizeLimit; message too large for this recipient"
		});

		// Token: 0x04000666 RID: 1638
		public static readonly SmtpResponse MessageTooLargeForSender = new SmtpResponse("550", "5.2.3", new string[]
		{
			"RESOLVER.RST.SendSizeLimit.Sender; message too large for this sender"
		});

		// Token: 0x04000667 RID: 1639
		public static readonly SmtpResponse MessageTooLargeForOrganization = new SmtpResponse("550", "5.2.3", new string[]
		{
			"RESOLVER.RST.SendSizeLimit.Org; message too large for this organization"
		});

		// Token: 0x04000668 RID: 1640
		public static readonly SmtpResponse MessageTooLargeForDistributionList = new SmtpResponse("550", "5.2.3", new string[]
		{
			"RESOLVER.RST.RecipSizeLimit.DL; message length exceeds administrative limit"
		});

		// Token: 0x04000669 RID: 1641
		public static readonly SmtpResponse RecipientDiscarded = new SmtpResponse("250", "2.1.5", new string[]
		{
			"RESOLVER.RST.RecipientDiscarded; recipient discarded due to failed distribution group limits check"
		});

		// Token: 0x0400066A RID: 1642
		public static readonly SmtpResponse RecipientLimitExceeded = new SmtpResponse("550", "5.5.3", new string[]
		{
			"RESOLVER.ADR.RecipLimit; too many recipients"
		});

		// Token: 0x0400066B RID: 1643
		public static readonly SmtpResponse RevertDueToRecipientLimit = new SmtpResponse("250", "2.1.5", new string[]
		{
			"RESOLVER.ADR.RecipOverLimit; too many recipients"
		});

		// Token: 0x0400066C RID: 1644
		public static readonly SmtpResponse NotAuthenticated = new SmtpResponse("550", "5.7.1", new string[]
		{
			"RESOLVER.RST.AuthRequired; authentication required"
		});

		// Token: 0x0400066D RID: 1645
		public static readonly SmtpResponse InvalidDirectoryObjectForRestrictionCheck = new SmtpResponse("550", "5.2.0", new string[]
		{
			"RESOLVER.RST.InvalidObjectForRestrictionCheck; unable to perform restriction check because a directory object is misconfigured"
		});

		// Token: 0x0400066E RID: 1646
		public static readonly SmtpResponse MicrosoftExchangeRecipientSuppressed = new SmtpResponse("250", "2.1.5", new string[]
		{
			"RESOLVER.MER.Suppressed; recipient suppressed"
		});

		// Token: 0x0400066F RID: 1647
		public static readonly SmtpResponse ModerationStarted = new SmtpResponse("250", "2.1.5", new string[]
		{
			"Resolver.MT.StartModeration; recipient is moderated"
		});

		// Token: 0x04000670 RID: 1648
		public static readonly SmtpResponse ModerationInitFailed = new SmtpResponse("554", "5.6.0", new string[]
		{
			"Resolver.MT.CannotStartModeration; recipient needs moderation, but moderation initiation failed due to content"
		});

		// Token: 0x04000671 RID: 1649
		public static readonly SmtpResponse ModerationReencrptionFailed = new SmtpResponse("554", "5.6.0", new string[]
		{
			"Resolver.MT.ReencryptionFailed; Re-encryption failed during the moderation of a protected message."
		});

		// Token: 0x04000672 RID: 1650
		public static readonly SmtpResponse ModerationNoArbitrationAddress = new SmtpResponse("550", "5.2.0", new string[]
		{
			"Resolver.MT.NoArbitrationAddress; No arbitration address for approval process"
		});

		// Token: 0x04000673 RID: 1651
		public static readonly SmtpResponse NoModeratorAddresses = new SmtpResponse("550", "5.2.0", new string[]
		{
			"Resolver.MT.NoModeratorAddresses; No moderator addresses for approval process"
		});

		// Token: 0x04000674 RID: 1652
		public static readonly SmtpResponse ModerationLoop = new SmtpResponse("550", "5.2.0", new string[]
		{
			"Resolver.MT.ModerationLoop; Loop in approval process"
		});

		// Token: 0x04000675 RID: 1653
		public static readonly SmtpResponse MessageTooLargeForRoute = new SmtpResponse("550", "5.3.4", new string[]
		{
			"ROUTING.SizeLimit; message size exceeds fixed maximum size for route"
		});

		// Token: 0x04000676 RID: 1654
		public static readonly SmtpResponse InvalidAddressForRouting = new SmtpResponse("550", "5.1.2", new string[]
		{
			"ROUTING.InvalidAddress; invalid address domain"
		});

		// Token: 0x04000677 RID: 1655
		public static readonly SmtpResponse InvalidX400AddressForRouting = new SmtpResponse("550", "5.1.2", new string[]
		{
			"ROUTING.InvalidX400Address; invalid x400 address"
		});

		// Token: 0x04000678 RID: 1656
		public static readonly SmtpResponse NoNextHop = new SmtpResponse("550", "5.4.4", new string[]
		{
			"ROUTING.NoNextHop; unable to route"
		});

		// Token: 0x04000679 RID: 1657
		public static readonly SmtpResponse NoConnectorForAddressType = new SmtpResponse("550", "5.4.4", new string[]
		{
			"ROUTING.NoConnectorForAddressType; unable to route for address type"
		});

		// Token: 0x0400067A RID: 1658
		public static readonly SmtpResponse QuarantineDisabled = new SmtpResponse("550", "5.2.1", new string[]
		{
			"DSNGENERATION.Quarantine; unable to quarantine"
		});

		// Token: 0x0400067B RID: 1659
		public static readonly SmtpResponse ProbeMessageDropped = new SmtpResponse("250", "2.1.6", new string[]
		{
			"ROUTING.ProbeMessageDropped; probe message dropped"
		});

		// Token: 0x0400067C RID: 1660
		public static readonly SmtpResponse MessageIsPoisonForRemoteServer = new SmtpResponse("550", "5.5.0", new string[]
		{
			"SMTPSEND.PoisonForRemote; message might be crashing the remote server"
		});

		// Token: 0x0400067D RID: 1661
		public static readonly SmtpResponse SuspiciousRemoteServerError = new SmtpResponse("451", "4.4.0", new string[]
		{
			"SMTPSEND.SuspiciousRemoteServerError; remote server disconnected abruptly; retry will be delayed"
		});

		// Token: 0x0400067E RID: 1662
		public static readonly SmtpResponse BareLinefeedsAreIllegal = new SmtpResponse("550", "5.6.2", new string[]
		{
			"SMTPSEND.BareLinefeedsAreIllegal; message contains bare linefeeds, which cannot be sent via DATA"
		});

		// Token: 0x0400067F RID: 1663
		public static readonly SmtpResponse BdatOverAdvertisedSizeLimit = new SmtpResponse("550", "5.3.4", new string[]
		{
			"SMTPSEND.BDATOverAdvertisedSize; message size exceeds fixed maximum size"
		});

		// Token: 0x04000680 RID: 1664
		public static readonly SmtpResponse DataOverAdvertisedSizeLimit = new SmtpResponse("550", "5.3.4", new string[]
		{
			"SMTPSEND.DATAOverAdvertisedSize; message size exceeds fixed maximum size"
		});

		// Token: 0x04000681 RID: 1665
		public static readonly SmtpResponse OverAdvertisedSizeLimit = new SmtpResponse("550", "5.3.4", new string[]
		{
			"SMTPSEND.OverAdvertisedSize; message size exceeds fixed maximum size"
		});

		// Token: 0x04000682 RID: 1666
		public static readonly SmtpResponse SmtpSendLongRecipientAddress = new SmtpResponse("550", "5.1.2", new string[]
		{
			"SMTPSEND.LongRecipientAddress; long recipient address"
		});

		// Token: 0x04000683 RID: 1667
		public static readonly SmtpResponse SmtpSendInvalidLongRecipientAddress = new SmtpResponse("550", "5.1.2", new string[]
		{
			"SMTPSEND.InvalidLongRecipientAddress; invalid long recipient address"
		});

		// Token: 0x04000684 RID: 1668
		public static readonly SmtpResponse SmtpSendLongSenderAddress = new SmtpResponse("550", "5.1.7", new string[]
		{
			"SMTPSEND.LongSenderAddress; long sender address"
		});

		// Token: 0x04000685 RID: 1669
		public static readonly SmtpResponse SmtpSendInvalidLongSenderAddress = new SmtpResponse("550", "5.1.7", new string[]
		{
			"SMTPSEND.InvalidLongSenderAddress; invalid long sender address"
		});

		// Token: 0x04000686 RID: 1670
		public static readonly SmtpResponse SmtpSendUtf8RecipientAddress = new SmtpResponse("550", "5.1.8", new string[]
		{
			"SMTPSEND.Utf8RecipientAddress; UTF-8 recipient address not supported."
		});

		// Token: 0x04000687 RID: 1671
		public static readonly SmtpResponse SmtpSendUtf8SenderAddress = new SmtpResponse("550", "5.1.9", new string[]
		{
			"SMTPSEND.Utf8SenderAddress; UTF-8 sender address not supported."
		});

		// Token: 0x04000688 RID: 1672
		public static readonly SmtpResponse SmtpSendOrarNotTransmittable = new SmtpResponse("550", "5.6.0", new string[]
		{
			"SMTPSEND.SmtpSendOrarNotTransmittable; unable to transmit ORAR"
		});

		// Token: 0x04000689 RID: 1673
		public static readonly SmtpResponse SmtpSendLongOrarNotTransmittable = new SmtpResponse("550", "5.6.0", new string[]
		{
			"SMTPSEND.SmtpSendLongOrarNotTransmittable; unable to transmit long ORAR"
		});

		// Token: 0x0400068A RID: 1674
		public static readonly SmtpResponse SmtpSendRDstNotTransmittable = new SmtpResponse("550", "5.6.0", new string[]
		{
			"SMTPSEND.SmtpSendRDstNotTransmittable; unable to transmit RDST"
		});

		// Token: 0x0400068B RID: 1675
		public static readonly SmtpResponse DnsNonExistentDomain = new SmtpResponse("554", "5.4.4", new string[]
		{
			"SMTPSEND.DNS.NonExistentDomain; nonexistent domain"
		});

		// Token: 0x0400068C RID: 1676
		public static readonly SmtpResponse DnsMxLoopback = new SmtpResponse("554", "5.4.4", new string[]
		{
			"SMTPSEND.DNS.MxLoopback; DNS records for this domain are configured in a loop"
		});

		// Token: 0x0400068D RID: 1677
		public static readonly SmtpResponse DnsInvalidData = new SmtpResponse("554", "5.4.4", new string[]
		{
			"SMTPSEND.DNS.InvalidData; DNS returned an invalid response for this domain"
		});

		// Token: 0x0400068E RID: 1678
		public static readonly SmtpResponse NoOutboundFrontendServers = new SmtpResponse("451", "4.4.0", new string[]
		{
			"SMTPSEND.DNS.NoOutboundFrontendServers; no outbound frontend servers to proxy through"
		});

		// Token: 0x0400068F RID: 1679
		public static readonly SmtpResponse DnsNonExistentDomainForOutboundFrontend = new SmtpResponse("554", "5.4.4", new string[]
		{
			"SMTPSEND.DNS.NonExistentDomain; nonexistent domain while resolving the outbound proxy frontend server fqdn"
		});

		// Token: 0x04000690 RID: 1680
		public static readonly SmtpResponse DnsMxLoopbackForOutboundFrontend = new SmtpResponse("554", "5.4.4", new string[]
		{
			"SMTPSEND.DNS.MxLoopback; DNS records for this domain are configured in a loop for the outbound proxy frontend servers fqdn"
		});

		// Token: 0x04000691 RID: 1681
		public static readonly SmtpResponse DnsInvalidDataOutboundFrontend = new SmtpResponse("554", "5.4.4", new string[]
		{
			"SMTPSEND.DNS.InvalidData; DNS returned an invalid response nonexistent domain while resolving the outbound proxy frontend server fqdn"
		});

		// Token: 0x04000692 RID: 1682
		public static readonly SmtpResponse SendingError = new SmtpResponse("451", "4.4.0", new string[]
		{
			"SMTPSEND.SendingError; Error while sending message"
		});

		// Token: 0x04000693 RID: 1683
		public static readonly SmtpResponse UnexpectedException = new SmtpResponse("444", "4.4.4", new string[]
		{
			"NONSMTPGW.UnexpectedException Unexpected exception occurred opening a new non SMTP gateway connection."
		});

		// Token: 0x04000694 RID: 1684
		public static readonly SmtpResponse GWInvalidSourceBH = new SmtpResponse("444", "4.4.4", new string[]
		{
			"NONSMTPGW.InvalidSourceBH This BH is not a source for this connector."
		});

		// Token: 0x04000695 RID: 1685
		public static readonly SmtpResponse GWConnectorDeleted = new SmtpResponse("444", "4.4.4", new string[]
		{
			"NONSMTPGW.ConnectorDeleted The Connector has been deleted or disabled."
		});

		// Token: 0x04000696 RID: 1686
		public static readonly SmtpResponse GWConnectorInvalid = new SmtpResponse("444", "4.4.4", new string[]
		{
			"NONSMTPGW.ConnectorInvalid The Connector object is invalid."
		});

		// Token: 0x04000697 RID: 1687
		public static readonly SmtpResponse GWPathTooLongException = new SmtpResponse("444", "4.4.4", new string[]
		{
			"NONSMTPGW.PathTooLongException writing to the Foreign Connector Drop Directory."
		});

		// Token: 0x04000698 RID: 1688
		public static readonly SmtpResponse GWIOException = new SmtpResponse("444", "4.4.4", new string[]
		{
			"NONSMTPGW.IOException writing to the Foreign Connector Drop Directory."
		});

		// Token: 0x04000699 RID: 1689
		public static readonly SmtpResponse GWUnauthorizedAccess = new SmtpResponse("444", "4.4.4", new string[]
		{
			"NONSMTPGW.UnauthorizedAccess The access requested is not permitted by the OS for the specified path."
		});

		// Token: 0x0400069A RID: 1690
		public static readonly SmtpResponse GWNoDropDirectory = new SmtpResponse("444", "4.4.4", new string[]
		{
			"NONSMTPGW.NoDropDirectory The Drop Directory does not exist or has invalid ACLs."
		});

		// Token: 0x0400069B RID: 1691
		public static readonly SmtpResponse GWQuotaExceeded = new SmtpResponse("444", "4.4.4", new string[]
		{
			"NONSMTPGW.QuotaExceeded Drop Directory Quota is exceeded."
		});

		// Token: 0x0400069C RID: 1692
		public static readonly SmtpResponse DeliveryAgentInvalidSourceBH = new SmtpResponse("444", "4.4.4", new string[]
		{
			"DELIVERYAGENT.InvalidSourceBH This BH is not a source for this connector."
		});

		// Token: 0x0400069D RID: 1693
		public static readonly SmtpResponse DeliveryAgentConnectorDeleted = new SmtpResponse("444", "4.4.4", new string[]
		{
			"DELIVERYAGENT.ConnectorDeleted The Connector has been deleted or disabled."
		});

		// Token: 0x0400069E RID: 1694
		public static readonly SmtpResponse DeliveryAgentConnectorInvalid = new SmtpResponse("444", "4.4.4", new string[]
		{
			"DELIVERYAGENT.ConnectorInvalid The Connector object is invalid."
		});

		// Token: 0x0400069F RID: 1695
		public static readonly SmtpResponse InboundInvalidDirectoryData = new SmtpResponse("550", "5.2.0", new string[]
		{
			"STOREDRV.Deliver; directory misconfiguration."
		});

		// Token: 0x040006A0 RID: 1696
		public static readonly SmtpResponse OutboundInvalidDirectoryData = new SmtpResponse("550", "5.2.0", new string[]
		{
			"STOREDRV.Submit; directory misconfiguration."
		});

		// Token: 0x040006A1 RID: 1697
		public static readonly SmtpResponse PublicFolderReplicaServerMisconfiguration = new SmtpResponse("550", "5.2.0", new string[]
		{
			"STOREDRV.Deliver; PF replica server misconfiguration"
		});

		// Token: 0x040006A2 RID: 1698
		public static readonly SmtpResponse PublicFolderRoute = new SmtpResponse("420", "4.2.0", new string[]
		{
			"STOREDRV.Deliver; No local replica for public folder"
		});

		// Token: 0x040006A3 RID: 1699
		public static readonly SmtpResponse RecipientMailboxIsRemote = new SmtpResponse("420", "4.2.0", new string[]
		{
			"STOREDRV.Deliver; recipient mailbox is remote"
		});

		// Token: 0x040006A4 RID: 1700
		public static readonly SmtpResponse RecipientMailboxLocationInfoNotAvailable = new SmtpResponse("420", "4.2.0", new string[]
		{
			"STOREDRV.Deliver; recipient mailbox location information is not available"
		});

		// Token: 0x040006A5 RID: 1701
		public static readonly SmtpResponse SubmissionCancelled = new SmtpResponse("420", "4.2.0", new string[]
		{
			"STOREDRV.Submit; Submission is cancelled"
		});

		// Token: 0x040006A6 RID: 1702
		public static readonly SmtpResponse SubmissionCancelledRecipientMdbNotTargetted = new SmtpResponse("420", "4.2.0", new string[]
		{
			"STOREDRV.Submit; Submission is cancelled; recipient mailbox mdb not targetted for resubmission"
		});

		// Token: 0x040006A7 RID: 1703
		public static readonly SmtpResponse SubmissionCancelledProbeRequest = new SmtpResponse("420", "4.2.0", new string[]
		{
			"STOREDRV.Submit; Submission is cancelled; probe resubmit request"
		});

		// Token: 0x040006A8 RID: 1704
		public static readonly SmtpResponse OutboundInvalidAddress = new SmtpResponse("550", "5.1.3", new string[]
		{
			"STOREDRV.Submit; invalid recipient address"
		});

		// Token: 0x040006A9 RID: 1705
		public static readonly SmtpResponse InboundInvalidContent = new SmtpResponse("554", "5.6.0", new string[]
		{
			"STOREDRV.Deliver; invalid message content"
		});

		// Token: 0x040006AA RID: 1706
		public static readonly SmtpResponse OutboundInvalidContent = new SmtpResponse("554", "5.6.0", new string[]
		{
			"STOREDRV.Submit; invalid message content"
		});

		// Token: 0x040006AB RID: 1707
		public static readonly SmtpResponse MailboxDiskFull = new SmtpResponse("431", "4.3.1", new string[]
		{
			"STOREDRV; mailbox disk is full"
		});

		// Token: 0x040006AC RID: 1708
		public static readonly SmtpResponse DiskFull = new SmtpResponse("431", "4.3.1", new string[]
		{
			"STOREDRV; disk is full"
		});

		// Token: 0x040006AD RID: 1709
		public static readonly SmtpResponse MailboxIOError = new SmtpResponse("430", "4.3.0", new string[]
		{
			"STOREDRV; mailbox database IO error"
		});

		// Token: 0x040006AE RID: 1710
		public static readonly SmtpResponse MailboxServerOffline = new SmtpResponse("432", "4.3.2", new string[]
		{
			"STOREDRV; mailbox server is offline"
		});

		// Token: 0x040006AF RID: 1711
		public static readonly SmtpResponse MDBOffline = new SmtpResponse("432", "4.3.2", new string[]
		{
			"STOREDRV; mailbox database is offline"
		});

		// Token: 0x040006B0 RID: 1712
		public static readonly SmtpResponse MapiNoAccessFailure = new SmtpResponse("432", "4.3.2", new string[]
		{
			"STOREDRV.Deliver; retryable mailbox access denied failure"
		});

		// Token: 0x040006B1 RID: 1713
		public static readonly SmtpResponse MailboxServerTooBusy = new SmtpResponse("432", "4.3.2", new string[]
		{
			"STOREDRV.Storage; mailbox server is too busy"
		});

		// Token: 0x040006B2 RID: 1714
		public static readonly SmtpResponse MailboxMapiSessionLimit = new SmtpResponse("432", "4.3.2", new string[]
		{
			"STOREDRV.Storage; mailbox server mapi session limit exceeded"
		});

		// Token: 0x040006B3 RID: 1715
		public static readonly SmtpResponse MailboxServerNotEnoughMemory = new SmtpResponse("432", "4.3.2", new string[]
		{
			"STOREDRV.Storage; not enough memory for mapi operation"
		});

		// Token: 0x040006B4 RID: 1716
		public static readonly SmtpResponse MailboxServerMaxThreadsPerMdbExceeded = new SmtpResponse("432", "4.3.2", new string[]
		{
			"STOREDRV.Storage; max threads per mdb exceeded"
		});

		// Token: 0x040006B5 RID: 1717
		public static readonly SmtpResponse MapiExceptionMaxThreadsPerSCTExceeded = new SmtpResponse("432", "4.3.2", new string[]
		{
			"STOREDRV.Storage; max threads per sct exceeded"
		});

		// Token: 0x040006B6 RID: 1718
		public static readonly SmtpResponse MailboxServerThreadLimitExceeded = new SmtpResponse("432", "4.3.2", new string[]
		{
			"STOREDRV.Deliver; mailbox server thread limit exceeded"
		});

		// Token: 0x040006B7 RID: 1719
		public static readonly SmtpResponse MailboxDatabaseThreadLimitExceeded = new SmtpResponse("432", "4.3.2", new string[]
		{
			"STOREDRV.Deliver; mailbox database thread limit exceeded"
		});

		// Token: 0x040006B8 RID: 1720
		public static readonly SmtpResponse RecipientThreadLimitExceeded = new SmtpResponse("432", "4.3.2", new string[]
		{
			"STOREDRV.Deliver; recipient thread limit exceeded"
		});

		// Token: 0x040006B9 RID: 1721
		public static readonly SmtpResponse DeliverySourceThreadLimitExceeded = new SmtpResponse("432", "4.3.2", new string[]
		{
			"STOREDRV.Deliver; delivery source thread limit exceeded"
		});

		// Token: 0x040006BA RID: 1722
		public static readonly SmtpResponse DynamicMailboxDatabaseThrottlingLimitExceeded = new SmtpResponse("432", "4.3.2", new string[]
		{
			"STOREDRV.Deliver; dynamic mailbox database throttling limit exceeded."
		});

		// Token: 0x040006BB RID: 1723
		public static readonly SmtpResponse MaxConcurrentMessageSizeLimitExceeded = new SmtpResponse("432", "4.3.2", new string[]
		{
			"STOREDRV.Deliver; max concurrent message size limit exceeded."
		});

		// Token: 0x040006BC RID: 1724
		public static readonly SmtpResponse LogonFailure = new SmtpResponse("430", "4.2.0", new string[]
		{
			"STOREDRV; mailbox logon failure"
		});

		// Token: 0x040006BD RID: 1725
		public static readonly SmtpResponse UnrecognizedClassification = new SmtpResponse("550", "5.7.3", new string[]
		{
			"Message classification was not recognized"
		});

		// Token: 0x040006BE RID: 1726
		public static readonly SmtpResponse NoLegacyDN = new SmtpResponse("420", "4.2.0", new string[]
		{
			"STOREDRV.Deliver; legacyExchangeDN attribute value is missing for recipient"
		});

		// Token: 0x040006BF RID: 1727
		public static readonly SmtpResponse SubscriptionNotFound = new SmtpResponse("550", "5.6.2", new string[]
		{
			"STOREDRV.Submit; subscription not found"
		});

		// Token: 0x040006C0 RID: 1728
		public static readonly SmtpResponse SubscriptionDisabled = new SmtpResponse("550", "5.6.2", new string[]
		{
			"STOREDRV.Submit; subscription is disabled"
		});

		// Token: 0x040006C1 RID: 1729
		public static readonly SmtpResponse SubscriptionNotEnabledForSendAs = new SmtpResponse("550", "5.6.2", new string[]
		{
			"STOREDRV.Submit; subscription not enabled for send as"
		});

		// Token: 0x040006C2 RID: 1730
		public static readonly SmtpResponse AmbiguousSubscription = new SmtpResponse("550", "5.6.2", new string[]
		{
			"STOREDRV.Submit; ambiguous send as subscription found"
		});

		// Token: 0x040006C3 RID: 1731
		public static readonly SmtpResponse InvalidSendAsProperties = new SmtpResponse("550", "5.6.2", new string[]
		{
			"STOREDRV.Submit; invalid send as properties"
		});

		// Token: 0x040006C4 RID: 1732
		public static readonly SmtpResponse UnrecognizedSendAsMessage = new SmtpResponse("550", "5.6.2", new string[]
		{
			"STOREDRV.Submit; unrecognized send as message"
		});

		// Token: 0x040006C5 RID: 1733
		public static readonly SmtpResponse AbortResubmissionDueToContentChange = new SmtpResponse("550", "5.2.0", new string[]
		{
			"STOREDRV.Submit; shadow re-submission aborted due to content change"
		});

		// Token: 0x040006C6 RID: 1734
		public static readonly SmtpResponse UnableToDetermineTargetPublicFolderMailbox = new SmtpResponse("550", "5.8.1", new string[]
		{
			"STOREDRV.Deliver; unable to determine target mailbox for public folder recipient"
		});

		// Token: 0x040006C7 RID: 1735
		public static readonly SmtpResponse PublicFolderMailboxNotFound = new SmtpResponse("420", "4.8.1", new string[]
		{
			"STOREDRV.Deliver; public folder mailbox not found"
		});

		// Token: 0x040006C8 RID: 1736
		public static readonly SmtpResponse MissingMdbProperties = new SmtpResponse("532", "5.3.2", new string[]
		{
			"STOREDRV.Deliver; Missing or bad StoreDriver MDB properties"
		});

		// Token: 0x040006C9 RID: 1737
		public static readonly SmtpResponse NotResolvedRecipient = new SmtpResponse("432", "4.3.3", new string[]
		{
			"STOREDRV.Deliver; Recipient could not be resolved"
		});

		// Token: 0x040006CA RID: 1738
		public static readonly SmtpResponse ExtendedPropertiesNotAvailable = new SmtpResponse("432", "4.3.3", new string[]
		{
			"STOREDRV.Deliver; Extended Properties not available"
		});

		// Token: 0x040006CB RID: 1739
		public static readonly SmtpResponse DeliverAgentTransientFailure = new SmtpResponse("432", "4.2.0", new string[]
		{
			"STOREDRV.Deliver; Agent transient failure during message resubmission"
		});

		// Token: 0x040006CC RID: 1740
		public static readonly SmtpResponse OutboundPoisonMessage = new SmtpResponse("550", "5.6.0", new string[]
		{
			"STOREDRV.Submit; message is treated as poison."
		});

		// Token: 0x040006CD RID: 1741
		public static readonly SmtpResponse RecipientMailboxQuarantined = new SmtpResponse("550", "5.7.9", new string[]
		{
			"STOREDRV.Deliver; Recipient mailbox quarantined."
		});

		// Token: 0x040006CE RID: 1742
		public static readonly SmtpResponse SenderMailboxQuarantined = new SmtpResponse("550", "5.7.10", new string[]
		{
			"STOREDRV.Submit; Sender mailbox quarantined."
		});

		// Token: 0x040006CF RID: 1743
		public static readonly SmtpResponse PublicFolderSenderValidationFailed = new SmtpResponse("550", "5.7.1", new string[]
		{
			"STOREDRV.Deliver; Unable to determine the sender's permission on public folder"
		});

		// Token: 0x040006D0 RID: 1744
		public static readonly SmtpResponse ApprovalInvalidMessage = new SmtpResponse("550", "5.7.1", new string[]
		{
			"APPROVAL.InvalidContent; Invalid content."
		});

		// Token: 0x040006D1 RID: 1745
		public static readonly SmtpResponse ApprovalCannotReadExpiryPolicy = new SmtpResponse("550", "5.6.0", new string[]
		{
			"APPROVAL.InvalidExpiry; Cannot read expiry policy."
		});

		// Token: 0x040006D2 RID: 1746
		public static readonly SmtpResponse ApprovalUnAuthorizedMessage = new SmtpResponse("550", "5.7.1", new string[]
		{
			"APPROVAL.NotAuthorized; message cannot be delivered."
		});

		// Token: 0x040006D3 RID: 1747
		public static readonly SmtpResponse ApprovalDuplicateInitiation = new SmtpResponse("250", "2.1.5", new string[]
		{
			"APPROVAL.DuplicationInitiation; duplicate initiation ignored."
		});

		// Token: 0x040006D4 RID: 1748
		public static readonly SmtpResponse ApprovalDecisionSuccsess = new SmtpResponse("250", "2.1.5", new string[]
		{
			"APPROVAL.DecisionMarked; decision marked OK."
		});

		// Token: 0x040006D5 RID: 1749
		public static readonly SmtpResponse ApprovalUpdateSuccess = new SmtpResponse("250", "2.1.5", new string[]
		{
			"APPROVAL.ApprovalRequestUpdated; approval request updated successfully"
		});

		// Token: 0x040006D6 RID: 1750
		public static readonly SmtpResponse ApprovalNdrOofUpdateSuccess = new SmtpResponse("250", "2.1.5", new string[]
		{
			"APPROVAL.NdrOofUpdated; NDR or OOF update successfully"
		});

		// Token: 0x040006D7 RID: 1751
		public static readonly SmtpResponse ApprovalResubmitSuccess = new SmtpResponse("500", "5.0.0", new string[]
		{
			"APPROVAL.ModeratedMessageResubmitted; message resubmitted successfully"
		});

		// Token: 0x040006D8 RID: 1752
		public static readonly SmtpResponse MeetingMessageParkedSuccess = new SmtpResponse("250", "2.1.5", new string[]
		{
			"PARKINGLOT.MeetingMessageParked; Message was parked successfully."
		});

		// Token: 0x040006D9 RID: 1753
		public static readonly SmtpResponse UMPartnerMessageInvalidHeaders = new SmtpResponse("554", "5.6.5", new string[]
		{
			"UMPMSG.Deliver; missing or invalid x-ms-exchange-um-* headers."
		});

		// Token: 0x040006DA RID: 1754
		public static readonly SmtpResponse UMPartnerMessageInvalidAttachments = new SmtpResponse("554", "5.6.5", new string[]
		{
			"UMPMSG.Deliver; missing or invalid attachments. The message must have 2 attachments of type audio/wav and text/xml."
		});

		// Token: 0x040006DB RID: 1755
		public static readonly SmtpResponse UMFaxPartnerMessageInvalidAttachments = new SmtpResponse("554", "5.6.5", new string[]
		{
			"UMPMSG.Deliver; missing or invalid Fax attachments. The message must have only 1 attachment of type image/tiff."
		});

		// Token: 0x040006DC RID: 1756
		public static readonly SmtpResponse UMPartnerMessageInvalidTranscriptionDocument = new SmtpResponse("554", "5.6.5", new string[]
		{
			"UMPMSG.Deliver; invalid transcription document. The transcription document failed schema validation."
		});

		// Token: 0x040006DD RID: 1757
		public static readonly SmtpResponse UMPartnerMessageMessageArrivedTooLate = new SmtpResponse("554", "5.6.5", new string[]
		{
			"UMPMSG.Deliver; message arrived too late. A copy without transcription has already been delivered to the user."
		});

		// Token: 0x040006DE RID: 1758
		public static readonly SmtpResponse UMPartnerMessageMessageSenderIdCheckFailed = new SmtpResponse("554", "5.6.5", new string[]
		{
			"UMPMSG.Deliver; sender ID check failed. Anonymous x-ms-exchange-um-partner messages must pass the Sender ID check."
		});

		// Token: 0x040006DF RID: 1759
		public static readonly SmtpResponse UMPartnerMessageMessageInvalidPartnerDomain = new SmtpResponse("554", "5.6.5", new string[]
		{
			"UMPMSG.Deliver; unrecognized partner domain. The recipient's UMMailboxPolicy does not allow the sender domain."
		});

		// Token: 0x040006E0 RID: 1760
		public static readonly SmtpResponse UMPartnerMessageMessageCannotReadPolicy = new SmtpResponse("554", "5.6.5", new string[]
		{
			"UMPMSG.Deliver; cannot read the recipient's UMMailboxPolicy."
		});

		// Token: 0x040006E1 RID: 1761
		public static readonly SmtpResponse UMPartnerMessageMessageCannotReadRecipient = new SmtpResponse("554", "5.6.5", new string[]
		{
			"UMPMSG.Deliver; cannot read the recipient object."
		});

		// Token: 0x040006E2 RID: 1762
		public static readonly SmtpResponse UMTranscriptionRequestSuccess = new SmtpResponse("250", "2.1.5", new string[]
		{
			"UMPMSG.Deliver; transcription request processed succesfully."
		});

		// Token: 0x040006E3 RID: 1763
		public static readonly SmtpResponse TransportRulesAgentDeferCountExceeded = new SmtpResponse("550", "5.7.1", new string[]
		{
			"TRA: Policy processing failed: transport rule evaluation timed out."
		});

		// Token: 0x040006E4 RID: 1764
		public static readonly SmtpResponse TransportRuleReject = new SmtpResponse("550", "5.7.1", new string[]
		{
			"TRANSPORT.RULES.RejectMessage; the message was rejected by organization policy"
		});

		// Token: 0x040006E5 RID: 1765
		public static readonly SmtpResponse SenderRecipientThrottlingMessageRejected = new SmtpResponse("450", "4.5.0", new string[]
		{
			"Message rejected.  Excessive message rate from sender to recipient."
		});

		// Token: 0x040006E6 RID: 1766
		public static readonly SmtpResponse MessageTooOld = new SmtpResponse("550", "4.4.7", new string[]
		{
			"Message dropped. Message too old"
		});

		// Token: 0x040006E7 RID: 1767
		public static readonly SmtpResponse MessageNotActive = new SmtpResponse("550", "4.4.7", new string[]
		{
			"Message dropped. Message not active"
		});

		// Token: 0x040006E8 RID: 1768
		public static readonly SmtpResponse PoisonMessageExpired = new SmtpResponse("550", "4.4.7", new string[]
		{
			"Message dropped. Poison message too old"
		});

		// Token: 0x040006E9 RID: 1769
		public static readonly Dictionary<SmtpResponse, LocalizedString> EnhancedTextGetter = new Dictionary<SmtpResponse, LocalizedString>(new AckReason.SmtpResponseComparer())
		{
			{
				AckReason.MessageTooLargeForHighPriority,
				SystemMessages.DSNEnhanced_5_2_3_QUEUE_Priority
			},
			{
				AckReason.MessageTooLargeForReceiver,
				SystemMessages.DSNEnhanced_5_2_3_RESOLVER_RST_RecipSizeLimit
			},
			{
				AckReason.MessageTooLargeForSender,
				SystemMessages.DSNEnhanced_5_2_3_RESOLVER_RST_SendSizeLimit_Sender
			},
			{
				AckReason.MessageTooLargeForOrganization,
				SystemMessages.DSNEnhanced_5_2_3_RESOLVER_RST_SendSizeLimit_Org
			},
			{
				AckReason.MessageTooLargeForDistributionList,
				SystemMessages.DSNEnhanced_5_2_3_RESOLVER_RST_RecipSizeLimit_DL
			},
			{
				AckReason.NoNextHop,
				SystemMessages.DSNEnhanced_5_4_4_ROUTING_NoNextHop
			},
			{
				AckReason.DnsNonExistentDomain,
				SystemMessages.DSNEnhanced_5_4_4_SMTPSEND_DNS_NonExistentDomain
			},
			{
				AckReason.ApprovalUnAuthorizedMessage,
				SystemMessages.DSNEnhanced_5_7_1_APPROVAL_NotAuthorized
			},
			{
				AckReason.NotAuthenticated,
				SystemMessages.DSNEnhanced_5_7_1_RESOLVER_RST_AuthRequired
			},
			{
				AckReason.RecipientPermissionRestricted,
				SystemMessages.DSNEnhanced_5_7_1_RESOLVER_RST_NotAuthorized
			},
			{
				AckReason.RecipientPermissionRestrictedToGroup,
				SystemMessages.DSNEnhanced_5_7_1_RESOLVER_RST_NotAuthorizedToGroup
			},
			{
				AckReason.ModerationReencrptionFailed,
				SystemMessages.DSNEnhanced_5_6_0_RESOLVER_MT_ModerationReencrptionFailed
			},
			{
				AckReason.DLExpansionBlockedNeedsSenderRestrictions,
				SystemMessages.DSNEnhanced_5_7_1_RESOLVER_RST_DLNeedsSenderRestrictions
			},
			{
				AckReason.TransportRuleReject,
				SystemMessages.DSNEnhanced_5_7_1_TRANSPORT_RULES_RejectMessage
			}
		};

		// Token: 0x040006EA RID: 1770
		internal static readonly SmtpResponse MailboxDatabaseMoved = new SmtpResponse("421", "4.2.1", new string[]
		{
			"STOREDRV.Deliver; mailbox database has moved."
		});

		// Token: 0x02000141 RID: 321
		internal class SmtpResponseComparer : IEqualityComparer<SmtpResponse>
		{
			// Token: 0x06000E3F RID: 3647 RVA: 0x00036D34 File Offset: 0x00034F34
			public bool Equals(SmtpResponse x, SmtpResponse y)
			{
				if (string.Equals(x.StatusCode, y.StatusCode, StringComparison.Ordinal) && string.Equals(x.EnhancedStatusCode, y.EnhancedStatusCode, StringComparison.Ordinal) && x.StatusText != null && y.StatusText != null && x.StatusText.Length > 0 && y.StatusText.Length > 0 && !string.IsNullOrEmpty(x.StatusText[0]) && !string.IsNullOrEmpty(y.StatusText[0]))
				{
					int num = x.StatusText[0].IndexOf(';');
					int num2 = y.StatusText[0].IndexOf(';');
					return num == num2 && num > 0 && string.Equals(x.StatusText[0].Substring(0, num), y.StatusText[0].Substring(0, num2), StringComparison.Ordinal);
				}
				return false;
			}

			// Token: 0x06000E40 RID: 3648 RVA: 0x00036E18 File Offset: 0x00035018
			public int GetHashCode(SmtpResponse smtpResponse)
			{
				int num = smtpResponse.GetHashCode();
				if (smtpResponse.StatusText != null && smtpResponse.StatusText.Length > 0 && !string.IsNullOrEmpty(smtpResponse.StatusText[0]))
				{
					int num2 = smtpResponse.StatusText[0].IndexOf(';');
					if (num2 > 0)
					{
						num ^= smtpResponse.StatusText[0].Substring(0, num2).GetHashCode();
					}
				}
				return num;
			}

			// Token: 0x040006EB RID: 1771
			private const char SemiColon = ';';
		}
	}
}
