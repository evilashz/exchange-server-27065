using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001D0 RID: 464
	internal struct DeliveryRestriction
	{
		// Token: 0x06001529 RID: 5417 RVA: 0x0005518C File Offset: 0x0005338C
		private DeliveryRestriction(RestrictedItem recipient, Sender sender, bool isAuthenticated, bool isJournalReport, long messageSize, Unlimited<ByteQuantifiedSize> defaultMaxReceiveSize, IList<RoutingAddress> privilegedSenders)
		{
			this.recipient = recipient;
			this.sender = sender;
			this.isAuthenticated = isAuthenticated;
			this.isJournalReport = isJournalReport;
			this.messageSize = messageSize;
			this.defaultMaxReceiveSize = defaultMaxReceiveSize;
			this.privilegedSenders = privilegedSenders;
			this.recipientSession = null;
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x000551CC File Offset: 0x000533CC
		public static RestrictionCheckResult CheckRestriction(RestrictedItem recipient, Sender sender, bool isAuthenticated, bool isJournalReport, long messageSize, Unlimited<ByteQuantifiedSize> defaultMaxReceiveSize, IList<RoutingAddress> privilegedSenders, ISimpleCache<ADObjectId, bool> memberOfGroupCache, OrganizationId orgId, out long maxRecipientMessageSize)
		{
			DeliveryRestriction deliveryRestriction = new DeliveryRestriction(recipient, sender, isAuthenticated, isJournalReport, messageSize, defaultMaxReceiveSize, privilegedSenders);
			return deliveryRestriction.CheckRestriction(orgId, memberOfGroupCache, out maxRecipientMessageSize);
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x000551F8 File Offset: 0x000533F8
		public static RestrictionCheckResult CheckSenderSizeRestriction(Sender sender, long messageSize, bool isAuthenticatedSender, bool isJournalReport, Unlimited<ByteQuantifiedSize> defaultSendLimit, IList<RoutingAddress> privilegedSenders, out long maxMessageSizeSendLimitInKB, out long currentMessageSizeInKB)
		{
			maxMessageSizeSendLimitInKB = 0L;
			currentMessageSizeInKB = 0L;
			if (isJournalReport)
			{
				return RestrictionCheckResult.AcceptedJournalReport;
			}
			if (DeliveryRestriction.IsPrivilegedSender(sender, isAuthenticatedSender, privilegedSenders))
			{
				return RestrictionCheckResult.AcceptedSizeOK;
			}
			bool flag = false;
			Unlimited<ByteQuantifiedSize> unlimited;
			if (!isAuthenticatedSender || sender.MaxSendSize.IsUnlimited)
			{
				unlimited = defaultSendLimit;
				flag = true;
			}
			else
			{
				unlimited = sender.MaxSendSize;
			}
			if (!unlimited.IsUnlimited)
			{
				currentMessageSizeInKB = DeliveryRestriction.GetSizeInKiloBytes(messageSize);
				maxMessageSizeSendLimitInKB = (long)unlimited.Value.ToKB();
				if (currentMessageSizeInKB > maxMessageSizeSendLimitInKB)
				{
					if (flag)
					{
						return (RestrictionCheckResult)2147483651U;
					}
					return (RestrictionCheckResult)2147483650U;
				}
			}
			return RestrictionCheckResult.AcceptedSizeOK;
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x00055284 File Offset: 0x00053484
		public static SmtpResponse GetResponseForResult(RestrictionCheckResult result)
		{
			switch (result)
			{
			case (RestrictionCheckResult)2147483649U:
				return AckReason.MessageTooLargeForReceiver;
			case (RestrictionCheckResult)2147483650U:
				return AckReason.MessageTooLargeForSender;
			case (RestrictionCheckResult)2147483651U:
				return AckReason.MessageTooLargeForOrganization;
			case (RestrictionCheckResult)2147483652U:
			case (RestrictionCheckResult)2147483653U:
			case (RestrictionCheckResult)2147483654U:
				return AckReason.RecipientPermissionRestricted;
			case (RestrictionCheckResult)2147483655U:
				return AckReason.NotAuthenticated;
			case (RestrictionCheckResult)2147483656U:
				return AckReason.InvalidDirectoryObjectForRestrictionCheck;
			case (RestrictionCheckResult)2147483657U:
				return AckReason.RecipientPermissionRestrictedToGroup;
			default:
				throw new InvalidOperationException("Should only call this method for rejected sender!");
			}
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x000552FC File Offset: 0x000534FC
		public static bool IsPrivilegedSender(Sender sender, bool isAuthenticatedSender, IList<RoutingAddress> privilegedSenders)
		{
			if (!isAuthenticatedSender)
			{
				return false;
			}
			if (sender.RecipientType != null)
			{
				if (sender.RecipientType.Value == Microsoft.Exchange.Data.Directory.Recipient.RecipientType.PublicDatabase)
				{
					return true;
				}
				if (sender.RecipientType.Value == Microsoft.Exchange.Data.Directory.Recipient.RecipientType.MicrosoftExchange)
				{
					return true;
				}
			}
			return sender.EmailAddress != null && privilegedSenders.IndexOf(sender.EmailAddress.Value) != -1;
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x00055373 File Offset: 0x00053573
		private static long GetSizeInKiloBytes(long size)
		{
			return (size + 1023L) / 1024L;
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x000553C8 File Offset: 0x000535C8
		private RestrictionCheckResult CheckRestriction(OrganizationId orgId, ISimpleCache<ADObjectId, bool> memberOfGroupCache, out long maxRecipientMessageSize)
		{
			DeliveryRestriction.<>c__DisplayClass1 CS$<>8__locals1 = new DeliveryRestriction.<>c__DisplayClass1();
			CS$<>8__locals1.orgId = orgId;
			maxRecipientMessageSize = 0L;
			if (this.isJournalReport)
			{
				return RestrictionCheckResult.AcceptedJournalReport;
			}
			if (DeliveryRestriction.IsPrivilegedSender(this.sender, this.isAuthenticated, this.privilegedSenders))
			{
				return RestrictionCheckResult.AcceptedPrivilegedSender;
			}
			if (this.IsMessageTooBig(out maxRecipientMessageSize))
			{
				return (RestrictionCheckResult)2147483649U;
			}
			if (this.recipient == null)
			{
				return ADRecipientRestriction.CheckDeliveryRestrictionForOneOffRecipient(this.sender.ObjectId, this.isAuthenticated);
			}
			GroupItem groupItem = this.recipient as GroupItem;
			IRecipientSession session = null;
			if (this.recipientSession == null)
			{
				ADNotificationAdapter.RunADOperation(delegate()
				{
					session = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(CS$<>8__locals1.orgId), 369, "CheckRestriction", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\Categorizer\\Resolver\\DeliveryRestriction.cs");
				}, 0);
				this.recipientSession = session;
			}
			return ADRecipientRestriction.CheckDeliveryRestriction(this.sender.ObjectId, this.isAuthenticated, this.recipient.RejectMessagesFrom, this.recipient.RejectMessagesFromDLMembers, this.recipient.AcceptMessagesOnlyFrom, this.recipient.AcceptMessagesOnlyFromDLMembers, this.recipient.BypassModerationFrom, this.recipient.BypassModerationFromDLMembers, this.recipient.ModeratedBy, (groupItem != null) ? groupItem.ManagedBy : null, this.recipient.RequireAllSendersAreAuthenticated, this.recipient.ModerationEnabled, this.recipient.RecipientType, this.recipientSession, memberOfGroupCache);
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x00055520 File Offset: 0x00053720
		private bool IsMessageTooBig(out long maxRecipientMessageSize)
		{
			maxRecipientMessageSize = 0L;
			Unlimited<ByteQuantifiedSize> maxReceiveSize;
			if (this.recipient == null || this.recipient.MaxReceiveSize.IsUnlimited)
			{
				maxReceiveSize = this.defaultMaxReceiveSize;
			}
			else
			{
				maxReceiveSize = this.recipient.MaxReceiveSize;
			}
			if (!maxReceiveSize.IsUnlimited)
			{
				maxRecipientMessageSize = (long)maxReceiveSize.Value.ToKB();
				return DeliveryRestriction.GetSizeInKiloBytes(this.messageSize) > (long)maxReceiveSize.Value.ToKB();
			}
			return false;
		}

		// Token: 0x04000A8D RID: 2701
		private RestrictedItem recipient;

		// Token: 0x04000A8E RID: 2702
		private Sender sender;

		// Token: 0x04000A8F RID: 2703
		private bool isAuthenticated;

		// Token: 0x04000A90 RID: 2704
		private bool isJournalReport;

		// Token: 0x04000A91 RID: 2705
		private long messageSize;

		// Token: 0x04000A92 RID: 2706
		private Unlimited<ByteQuantifiedSize> defaultMaxReceiveSize;

		// Token: 0x04000A93 RID: 2707
		private IList<RoutingAddress> privilegedSenders;

		// Token: 0x04000A94 RID: 2708
		private IRecipientSession recipientSession;
	}
}
