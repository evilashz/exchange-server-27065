using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001CC RID: 460
	internal class RestrictedItem : DirectoryItem
	{
		// Token: 0x060014FF RID: 5375 RVA: 0x00054335 File Offset: 0x00052535
		public RestrictedItem(MailRecipient recipient) : base(recipient)
		{
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x0005433E File Offset: 0x0005253E
		public IList<ADObjectId> AcceptMessagesOnlyFrom
		{
			get
			{
				return base.GetListProperty<ADObjectId>("Microsoft.Exchange.Transport.DirectoryData.AcceptMessagesOnlyFrom");
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x0005434B File Offset: 0x0005254B
		public IList<ADObjectId> AcceptMessagesOnlyFromDLMembers
		{
			get
			{
				return base.GetListProperty<ADObjectId>("Microsoft.Exchange.Transport.DirectoryData.AcceptMessagesOnlyFromDLMembers");
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x00054358 File Offset: 0x00052558
		public IList<ADObjectId> RejectMessagesFrom
		{
			get
			{
				return base.GetListProperty<ADObjectId>("Microsoft.Exchange.Transport.DirectoryData.RejectMessagesFrom");
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x00054365 File Offset: 0x00052565
		public IList<ADObjectId> RejectMessagesFromDLMembers
		{
			get
			{
				return base.GetListProperty<ADObjectId>("Microsoft.Exchange.Transport.DirectoryData.RejectMessagesFromDLMembers");
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x00054372 File Offset: 0x00052572
		public bool RequireAllSendersAreAuthenticated
		{
			get
			{
				return base.GetProperty<bool>("Microsoft.Exchange.Transport.DirectoryData.RequireAllSendersAreAuthenticated", false);
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x00054380 File Offset: 0x00052580
		public bool ModerationEnabled
		{
			get
			{
				return base.GetProperty<bool>("Microsoft.Exchange.Transport.DirectoryData.ModerationEnabled", false);
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x0005438E File Offset: 0x0005258E
		public IList<ADObjectId> ModeratedBy
		{
			get
			{
				return base.GetListProperty<ADObjectId>("Microsoft.Exchange.Transport.DirectoryData.ModeratedBy");
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x0005439B File Offset: 0x0005259B
		public IList<ADObjectId> BypassModerationFrom
		{
			get
			{
				return base.GetListProperty<ADObjectId>("Microsoft.Exchange.Transport.DirectoryData.BypassModerationFrom");
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001508 RID: 5384 RVA: 0x000543A8 File Offset: 0x000525A8
		public IList<ADObjectId> BypassModerationFromDLMembers
		{
			get
			{
				return base.GetListProperty<ADObjectId>("Microsoft.Exchange.Transport.DirectoryData.BypassModerationFromDLMembers");
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x000543B5 File Offset: 0x000525B5
		public ADObjectId ArbitrationMailbox
		{
			get
			{
				return base.GetProperty<ADObjectId>("Microsoft.Exchange.Transport.DirectoryData.ArbitrationMailbox");
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x000543C2 File Offset: 0x000525C2
		public TransportModerationNotificationFlags SendModerationNotifications
		{
			get
			{
				return (TransportModerationNotificationFlags)base.GetProperty<int>("Microsoft.Exchange.Transport.DirectoryData.SendModerationNotifications", 3);
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x000543D0 File Offset: 0x000525D0
		public bool BypassNestedModerationEnabled
		{
			get
			{
				return base.GetProperty<bool>("Microsoft.Exchange.Transport.DirectoryData.BypassNestedModerationEnabled", false);
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x000543E0 File Offset: 0x000525E0
		public Unlimited<ByteQuantifiedSize> MaxReceiveSize
		{
			get
			{
				ulong bytesValue;
				if (base.Recipient.ExtendedProperties.TryGetValue<ulong>("Microsoft.Exchange.Transport.DirectoryData.MaxReceiveSize", out bytesValue))
				{
					ByteQuantifiedSize limitedValue = ByteQuantifiedSize.FromBytes(bytesValue);
					return new Unlimited<ByteQuantifiedSize>(limitedValue);
				}
				return Unlimited<ByteQuantifiedSize>.UnlimitedValue;
			}
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x00054419 File Offset: 0x00052619
		public override void PreProcess(Expansion expansion)
		{
			if (this.CheckDeliveryRestrictions(expansion))
			{
				this.Allow(expansion);
			}
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0005442B File Offset: 0x0005262B
		public virtual void Allow(Expansion expansion)
		{
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x00054430 File Offset: 0x00052630
		protected override bool CheckDeliveryRestrictions(Expansion expansion)
		{
			long num;
			RestrictionCheckResult restrictionCheckResult = DeliveryRestriction.CheckRestriction(this, expansion.Sender, expansion.Resolver.IsAuthenticated, expansion.Resolver.MailItem.IsJournalReport(), expansion.Message.OriginalMessageSize, expansion.Configuration.MaxReceiveSize, expansion.Configuration.PrivilegedSenders, expansion.Resolver.ResolverCache, expansion.MailItem.OrganizationId, out num);
			ExTraceGlobals.ResolverTracer.TraceDebug((long)this.GetHashCode(), "Restriction Check returns {0}: recipient {1} sender {2} authenticated {3} stream size {4}", new object[]
			{
				(int)restrictionCheckResult,
				base.Recipient,
				expansion.Sender,
				expansion.Resolver.IsAuthenticated,
				expansion.Message.OriginalMessageSize
			});
			if (ADRecipientRestriction.Failed(restrictionCheckResult))
			{
				if (restrictionCheckResult == (RestrictionCheckResult)2147483649U)
				{
					base.Recipient.AddDsnParameters("MaxRecipMessageSizeInKB", num);
					base.Recipient.AddDsnParameters("CurrentMessageSizeInKB", expansion.Message.OriginalMessageSize >> 10);
				}
				base.FailRecipient(DeliveryRestriction.GetResponseForResult(restrictionCheckResult));
				return false;
			}
			if (ADRecipientRestriction.Moderated(restrictionCheckResult) && !this.ShouldSkipModerationBasedOnMessage(expansion))
			{
				ExTraceGlobals.ResolverTracer.TraceDebug<MailRecipient>((long)this.GetHashCode(), "Recipient {0} requires moderation", base.Recipient);
				this.ModerateRecipient(expansion);
				return false;
			}
			return true;
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0005458B File Offset: 0x0005278B
		public bool IsDeliveryToGroupRestricted()
		{
			return this.ModerationEnabled || !RestrictedItem.IsNullOrEmptyCollection<ADObjectId>(this.AcceptMessagesOnlyFrom) || !RestrictedItem.IsNullOrEmptyCollection<ADObjectId>(this.AcceptMessagesOnlyFromDLMembers);
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x000545B2 File Offset: 0x000527B2
		private static bool IsNullOrEmptyCollection<T>(ICollection<T> collection)
		{
			return collection == null || collection.Count == 0;
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x000545C4 File Offset: 0x000527C4
		protected void ModerateRecipient(Expansion expansion)
		{
			TransportMailItem transportMailItem = TransportMailItem.NewSideEffectMailItem(expansion.MailItem, expansion.MailItem.ADRecipientCache.OrganizationId, LatencyComponent.Categorizer, MailDirectionality.Originating, expansion.MailItem.ExternalOrganizationId);
			ADRecipientCache<TransportMiniRecipient> adrecipientCache = transportMailItem.ADRecipientCache;
			RoutingAddress moderatedRecipientArbitrationMailbox = this.GetModeratedRecipientArbitrationMailbox(adrecipientCache);
			string value = null;
			if (expansion.Sender.ObjectId != null)
			{
				TransportMiniRecipient data = adrecipientCache.FindAndCacheRecipient(expansion.Sender.ObjectId).Data;
				if (data != null)
				{
					value = data.DisplayName;
				}
			}
			if (string.IsNullOrEmpty(value) && expansion.Sender.P2Address != null)
			{
				value = expansion.Sender.P2Address.AddressString;
			}
			if (string.IsNullOrEmpty(value))
			{
				value = (string)expansion.MailItem.From;
			}
			string text;
			if (!this.TryGetFormattedModeratorAddresses(adrecipientCache, out text))
			{
				ExTraceGlobals.ResolverTracer.TraceError((long)this.GetHashCode(), "No moderator addresses.");
				base.FailRecipient(AckReason.NoModeratorAddresses);
				return;
			}
			try
			{
				bool flag = this.SendModerationNotifications == TransportModerationNotificationFlags.Always || (this.SendModerationNotifications == TransportModerationNotificationFlags.Internal && expansion.Configuration.AcceptedDomains.CheckInternal(SmtpDomain.GetDomainPart(expansion.MailItem.From)));
				if (flag && expansion.Message != null && expansion.Message.AutoResponseSuppress != (AutoResponseSuppress)0)
				{
					flag = false;
				}
				ExTraceGlobals.ResolverTracer.TraceDebug((long)this.GetHashCode(), "Create initiation message. arbMbx='{0}'; recipient='{1}'; sender='{2}'; moderators='{3}'; notification={4}", new object[]
				{
					moderatedRecipientArbitrationMailbox,
					base.Recipient,
					expansion.MailItem.From,
					text,
					flag
				});
				ApprovalInitiation.CreateAndSubmitApprovalInitiation(transportMailItem, expansion.MailItem, base.Recipient, (string)expansion.MailItem.From, text, moderatedRecipientArbitrationMailbox, flag);
				base.Recipient.Ack(AckStatus.SuccessNoDsn, AckReason.ModerationStarted);
			}
			catch (ApprovalInitiation.ApprovalInitiationFailedException ex)
			{
				ExTraceGlobals.ResolverTracer.TraceError<ApprovalInitiation.ApprovalInitiationFailedException>((long)this.GetHashCode(), "Failing recipient because generating approval initiation caused an exception {0}", ex);
				if (ex.ExceptionType == ApprovalInitiation.ApprovalInitiationFailedException.FailureType.ModerationLoop)
				{
					base.Recipient.DsnRequested = DsnRequestedFlags.Never;
				}
				base.FailRecipient(ex.ExceptionSmtpResponse);
			}
			catch (ExchangeDataException arg)
			{
				ExTraceGlobals.ResolverTracer.TraceError<ExchangeDataException>((long)this.GetHashCode(), "Failing recipient because generating approval initiation caused an exception {0}", arg);
				base.FailRecipient(AckReason.ModerationInitFailed);
			}
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x00054820 File Offset: 0x00052A20
		protected bool ShouldSkipModerationBasedOnMessage(Expansion expansion)
		{
			if (expansion.BypassChildModeration || expansion.Message.BypassChildModeration)
			{
				ExTraceGlobals.ResolverTracer.TraceDebug<string>((long)this.GetHashCode(), "Skipping child moderation with BypassChildModeration: id={0}", expansion.MailItem.InternetMessageId);
				return true;
			}
			HeaderList headers = expansion.MailItem.RootPart.Headers;
			Header header = headers.FindFirst("X-MS-Exchange-Organization-Approval-Approved");
			string address;
			if (header != null && header.TryGetValue(out address) && base.Recipient.Email.Equals((RoutingAddress)address))
			{
				ExTraceGlobals.ResolverTracer.TraceDebug<string>((long)this.GetHashCode(), "Already approved: id={0}", expansion.MailItem.InternetMessageId);
				return true;
			}
			Header header2 = headers.FindFirst("X-MS-Exchange-Organization-Unjournal-Processed");
			if (header2 != null)
			{
				ExTraceGlobals.ResolverTracer.TraceDebug<string>((long)this.GetHashCode(), "By pass moderation for traffic going to DLs via eha migration or live journaling: id={0}", expansion.MailItem.InternetMessageId);
				return true;
			}
			if (ObjectClass.IsOfClass(expansion.MailItem.Message.MapiMessageClass, "IPM.Note.Microsoft.Approval.Request") && headers.FindFirst("X-MS-Exchange-Organization-Mapi-Admin-Submission") != null)
			{
				ExTraceGlobals.ResolverTracer.TraceDebug<string>((long)this.GetHashCode(), "Not moderating an approval request id={0}", expansion.MailItem.InternetMessageId);
				return true;
			}
			return false;
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x00054950 File Offset: 0x00052B50
		private RoutingAddress GetModeratedRecipientArbitrationMailbox(ADRecipientCache<TransportMiniRecipient> cache)
		{
			if (VariantConfiguration.InvariantNoFlightingSnapshot.Transport.IgnoreArbitrationMailboxForModeratedRecipient.Enabled || this.ArbitrationMailbox == null)
			{
				return RoutingAddress.Empty;
			}
			if (this.ArbitrationMailbox.IsDeleted)
			{
				ExEventLog exEventLog = new ExEventLog(ExTraceGlobals.ResolverTracer.Category, TransportEventLog.GetEventSource());
				exEventLog.LogEvent(TransportEventLogConstants.Tuple_RecipientStampedWithDeletedArbitrationMailbox, null, new object[]
				{
					base.Recipient,
					this.ArbitrationMailbox.Name
				});
				return RoutingAddress.Empty;
			}
			TransportMiniRecipient data = cache.FindAndCacheRecipient(this.ArbitrationMailbox).Data;
			if (data == null)
			{
				return RoutingAddress.Empty;
			}
			return (RoutingAddress)data.PrimarySmtpAddress.ToString();
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00054A14 File Offset: 0x00052C14
		private bool TryGetFormattedModeratorAddresses(ADRecipientCache<TransportMiniRecipient> cache, out string moderators)
		{
			moderators = null;
			IList<ADObjectId> list;
			if (this.ModeratedBy == null || this.ModeratedBy.Count == 0)
			{
				GroupItem groupItem = this as GroupItem;
				if (groupItem == null || groupItem.ManagedBy == null || groupItem.ManagedBy.Count == 0)
				{
					return false;
				}
				int num = Math.Min(groupItem.ManagedBy.Count, 25);
				list = new List<ADObjectId>(num);
				for (int i = 0; i < num; i++)
				{
					list.Add(groupItem.ManagedBy[i]);
				}
			}
			else
			{
				list = this.ModeratedBy;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ADObjectId objectId in list)
			{
				TransportMiniRecipient data = cache.FindAndCacheRecipient(objectId).Data;
				if (data != null)
				{
					SmtpAddress primarySmtpAddress = data.PrimarySmtpAddress;
					if (primarySmtpAddress.IsValidAddress && !primarySmtpAddress.Equals(SmtpAddress.NullReversePath))
					{
						stringBuilder.Append((string)primarySmtpAddress);
						stringBuilder.Append(';');
					}
				}
			}
			moderators = stringBuilder.ToString();
			return !string.IsNullOrEmpty(moderators);
		}
	}
}
