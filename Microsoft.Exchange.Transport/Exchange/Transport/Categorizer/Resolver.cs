using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001EE RID: 494
	internal class Resolver
	{
		// Token: 0x060015EA RID: 5610 RVA: 0x00059115 File Offset: 0x00057315
		public Resolver(TransportMailItem mailItem, TaskContext taskContext, Resolver.DeferBifurcatedDelegate deferHandler) : this(mailItem, taskContext, deferHandler, new ResolverCache())
		{
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00059128 File Offset: 0x00057328
		public Resolver(TransportMailItem mailItem, TaskContext taskContext, Resolver.DeferBifurcatedDelegate deferHandler, ResolverCache cache)
		{
			this.mailItem = mailItem;
			this.taskContext = taskContext;
			this.deferHandler = deferHandler;
			this.configuration = new ResolverConfiguration(mailItem.OrganizationId, mailItem.TransportSettings);
			this.message = new ResolverMessage(this.mailItem.Message, this.mailItem.MimeSize);
			this.backupMailItem = new LimitedMailItem(this, taskContext);
			this.isAuthenticated = MultilevelAuth.IsAuthenticated(this.mailItem);
			this.resolverCache = cache;
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x000591B9 File Offset: 0x000573B9
		public static ExEventLog EventLogger
		{
			get
			{
				return Resolver.eventLogger;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x000591C0 File Offset: 0x000573C0
		public static ResolverPerfCountersInstance PerfCounters
		{
			get
			{
				return Resolver.perfCounters;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x000591C7 File Offset: 0x000573C7
		public ResolverConfiguration Configuration
		{
			get
			{
				return this.configuration;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x000591CF File Offset: 0x000573CF
		public TransportMailItem MailItem
		{
			get
			{
				return this.mailItem;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x000591D7 File Offset: 0x000573D7
		public TaskContext TaskContext
		{
			get
			{
				return this.taskContext;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x000591DF File Offset: 0x000573DF
		public ResolverMessage Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x000591E7 File Offset: 0x000573E7
		public Stack<RecipientItem> RecipientStack
		{
			get
			{
				return this.recipientStack;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x000591EF File Offset: 0x000573EF
		public Sender Sender
		{
			get
			{
				return this.sender;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x000591F7 File Offset: 0x000573F7
		public bool IsAuthenticated
		{
			get
			{
				return this.isAuthenticated;
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x000591FF File Offset: 0x000573FF
		public ResolverCache ResolverCache
		{
			get
			{
				return this.resolverCache;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x00059207 File Offset: 0x00057407
		public int PendingChipItemsCount
		{
			get
			{
				return this.backupMailItem.PendingChipItemsCount;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x00059214 File Offset: 0x00057414
		public RecipientItem CurrentTopLevelRecipientInProcess
		{
			get
			{
				return this.currentTopLevelRecipientInProcess;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x0005921C File Offset: 0x0005741C
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x00059224 File Offset: 0x00057424
		public GroupItem TopLevelGroupItem
		{
			get
			{
				return this.topLevelGroupItem;
			}
			set
			{
				this.topLevelGroupItem = value;
			}
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x0005922D File Offset: 0x0005742D
		public static bool IsResolved(MailRecipient recipient)
		{
			return recipient.ExtendedProperties.GetValue<bool>("Microsoft.Exchange.Transport.Resolved", false);
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x00059240 File Offset: 0x00057440
		public static void SetResolved(MailRecipient recipient)
		{
			recipient.ExtendedProperties.SetValue<bool>("Microsoft.Exchange.Transport.Resolved", true);
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00059254 File Offset: 0x00057454
		public static void ClearResolverProperties(MailRecipient recipient)
		{
			recipient.ExtendedProperties.Remove("Microsoft.Exchange.Transport.Resolved");
			recipient.ExtendedProperties.Remove("Microsoft.Exchange.Transport.Processed");
			foreach (CachedProperty cachedProperty in DirectoryItem.AllCachedProperties)
			{
				recipient.ExtendedProperties.Remove(cachedProperty.ExtendedProperty);
			}
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x000592B8 File Offset: 0x000574B8
		public static void ClearResolverAndTransportSettings(TransportMailItem mailItem)
		{
			if (mailItem.ADRecipientCache != null)
			{
				mailItem.ADRecipientCache.Clear();
			}
			mailItem.ExtendedProperties.Remove("Microsoft.Exchange.Transport.Sender.Resolved");
			foreach (CachedProperty cachedProperty in SenderSchema.CachedProperties)
			{
				mailItem.ExtendedProperties.Remove(cachedProperty.ExtendedProperty);
			}
			mailItem.ClearTransportSettings();
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00059324 File Offset: 0x00057524
		public static void InitializePerfCounters()
		{
			try
			{
				Resolver.perfCounters = ResolverPerfCounters.GetInstance("_total");
			}
			catch (InvalidOperationException ex)
			{
				ExTraceGlobals.ResolverTracer.TraceError<InvalidOperationException>(0L, "Failed to initialize performance counters: {0}", ex);
				Resolver.EventLogger.LogEvent(TransportEventLogConstants.Tuple_ResolverPerfCountersLoadFailure, null, new object[]
				{
					ex.ToString()
				});
			}
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x0005938C File Offset: 0x0005758C
		public static bool TryEncapsulate(ProxyAddress address, out RoutingAddress outer)
		{
			outer = RoutingAddress.Empty;
			SmtpProxyAddress smtpProxyAddress = address as SmtpProxyAddress;
			if (smtpProxyAddress == null && !SmtpProxyAddress.TryEncapsulate(address, Components.Configuration.FirstOrgAcceptedDomainTable.DefaultDomainName, out smtpProxyAddress))
			{
				return false;
			}
			outer = new RoutingAddress(smtpProxyAddress.AddressString);
			return true;
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x000593E1 File Offset: 0x000575E1
		public static bool TryDeencapsulate(RoutingAddress address, out ProxyAddress inner)
		{
			return Resolver.TryDeencapsulate(address, Components.Configuration.FirstOrgAcceptedDomainTable.DefaultDomainName, out inner);
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x000593F9 File Offset: 0x000575F9
		public static bool TryDeencapsulate(RoutingAddress address, string firstOrgDefaultDomainName, out ProxyAddress inner)
		{
			inner = null;
			return string.Equals(address.DomainPart, firstOrgDefaultDomainName, StringComparison.OrdinalIgnoreCase) && SmtpProxyAddress.TryDeencapsulate((string)address, out inner);
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x0005941C File Offset: 0x0005761C
		public static bool IsEncapsulatedAddress(RoutingAddress address)
		{
			return Resolver.IsEncapsulatedAddress(address, Components.Configuration.FirstOrgAcceptedDomainTable.DefaultDomainName);
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00059433 File Offset: 0x00057633
		public void BifurcateAndDeferAmbigousRecipients(List<MailRecipient> recipientsToDefer, TransportMailItem mailItem, TaskContext taskContext, SmtpResponse ackReason)
		{
			this.BifurcateAndDeferRecipients(recipientsToDefer, mailItem, taskContext, ackReason, DeferReason.AmbiguousRecipient, Components.TransportAppConfig.Resolver.DeferralTimeForAmbiguousRecipients);
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00059451 File Offset: 0x00057651
		public void BifurcateAndDeferRecipients(List<MailRecipient> recipientsToDefer, TransportMailItem mailItem, TaskContext taskContext, SmtpResponse ackReason)
		{
			this.BifurcateAndDeferRecipients(recipientsToDefer, mailItem, taskContext, ackReason, DeferReason.TransientFailure, TimeSpan.FromMinutes(Components.TransportAppConfig.Resolver.ResolverRetryInterval));
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x00059474 File Offset: 0x00057674
		private void BifurcateAndDeferRecipients(List<MailRecipient> recipientsToDefer, TransportMailItem mailItem, TaskContext taskContext, SmtpResponse ackReason, DeferReason deferReason, TimeSpan deferTime)
		{
			TransportMailItem transportMailItem = Resolver.ForkMailItem(recipientsToDefer, mailItem, deferReason);
			foreach (MailRecipient mailRecipient in transportMailItem.Recipients)
			{
				mailRecipient.Ack(AckStatus.Retry, ackReason);
				Resolver.ClearResolverProperties(mailRecipient);
			}
			this.deferHandler(transportMailItem, taskContext, deferReason, deferTime);
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x000594E4 File Offset: 0x000576E4
		public bool RewriteEmail(MailRecipient recipient, ProxyAddress email, MessageTrackingSource messageTrackingSource)
		{
			RoutingAddress routingAddress;
			if (!Resolver.TryEncapsulate(email, out routingAddress))
			{
				return false;
			}
			if (recipient.Email != routingAddress)
			{
				ExTraceGlobals.ResolverTracer.TraceDebug<RoutingAddress>(0L, "recipient address {0} to be rewritten", recipient.Email);
				string value = recipient.ExtendedProperties.GetValue<string>("Microsoft.Exchange.Transport.DirectoryData.LegacyExchangeDN", null);
				MsgTrackResolveInfo msgTrackInfo = new MsgTrackResolveInfo(value, recipient.Email, routingAddress);
				MessageTrackingLog.TrackResolve(messageTrackingSource, this.mailItem, msgTrackInfo);
				ProxyAddress proxyAddress;
				if (recipient.ORcpt == null && (!(email.Prefix == ProxyAddressPrefix.Smtp) || !Resolver.TryDeencapsulate(recipient.Email, out proxyAddress)))
				{
					recipient.ORcpt = "rfc822;" + recipient.Email;
				}
				recipient.Email = routingAddress;
				ExTraceGlobals.ResolverTracer.TraceDebug<RoutingAddress, string>(0L, "recipient address rewritten to {0}, ORCPT = {1}", recipient.Email, recipient.ORcpt ?? "<null>");
			}
			return true;
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x000595C4 File Offset: 0x000577C4
		public void ResolveSenderAndTopLevelRecipients()
		{
			this.sender = new Sender(this.mailItem);
			this.LookupRecipientsAndSender();
			Sender.Resolve(this.GetADRawEntryFromCache(this.sender.P1Address), this.GetADRawEntryFromCache(this.sender.P2Address), this.mailItem);
			foreach (MailRecipient resolved in this.mailItem.Recipients.AllUnprocessed)
			{
				Resolver.SetResolved(resolved);
			}
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x00059660 File Offset: 0x00057860
		public void ResolveAll()
		{
			this.ResolveSenderAndTopLevelRecipients();
			if (!this.CheckSenderSizeRestriction())
			{
				return;
			}
			this.recipientStack = new Stack<RecipientItem>();
			Expansion expansion = new Expansion(this);
			foreach (MailRecipient mailRecipient in this.mailItem.Recipients.AllUnprocessed)
			{
				if (!mailRecipient.IsProcessed && !mailRecipient.ExtendedProperties.GetValue<bool>("Microsoft.Exchange.Transport.Processed", false))
				{
					RecipientItem recipientItem = RecipientItem.Create(mailRecipient, true);
					recipientItem.AddItemVisited(expansion);
					expansion.Add(recipientItem, true);
				}
			}
			if (!this.CheckRecipientLimit())
			{
				return;
			}
			while (this.recipientStack.Count > 0)
			{
				RecipientItem recipientItem2 = this.recipientStack.Pop();
				ExTraceGlobals.ResolverTracer.TracePfd<int, RoutingAddress, long>(0L, "PFD CAT {0} Resolving recipient {1} (msgId={2})", 17314, recipientItem2.Recipient.Email, this.mailItem.RecordId);
				Expansion expansion2 = Expansion.Resume(recipientItem2.Recipient, this);
				if (recipientItem2.TopLevelRecipient)
				{
					this.CheckDLLimitsAndAckRecipientIfPending();
					this.CommitPendingChips();
					this.ResetStateForNextTopLevelRecipient(recipientItem2);
				}
				try
				{
					recipientItem2.Process(expansion2);
					if (!recipientItem2.TopLevelRecipient || !this.ackPending)
					{
						recipientItem2.Recipient.ExtendedProperties.SetValue<bool>("Microsoft.Exchange.Transport.Processed", true);
					}
				}
				catch (UnresolvedRecipientBifurcatedTransientException)
				{
					ExTraceGlobals.ResolverTracer.TraceWarning(0L, "Caught UnresolvedRecipientBifurcatedTransientException; leaving recipient unprocessed");
				}
			}
			this.CheckDLLimitsAndAckRecipientIfPending();
			this.CommitBackupMailItem();
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x000597E4 File Offset: 0x000579E4
		public void CommitBackupMailItem()
		{
			this.CommitPendingChips();
			this.backupMailItem.CommitLogAndSubmit();
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x000597F8 File Offset: 0x000579F8
		public MailRecipient AddRecipient(string primarySmtpAddress, TransportMiniRecipient entry, DsnRequestedFlags dsnRequested, string orcpt, bool commitIfChipIsFull, out bool addedToPrimary)
		{
			TransportMailItem transportMailItem = this.mailItem;
			bool flag = Resolver.IsExpandableEntry(entry);
			MailRecipient mailRecipient;
			if (this.mailItem.Recipients.Count > ResolverConfiguration.ExpansionSizeLimit && !flag)
			{
				mailRecipient = this.backupMailItem.AddRecipient(primarySmtpAddress, commitIfChipIsFull, out transportMailItem);
				addedToPrimary = false;
			}
			else
			{
				addedToPrimary = true;
				mailRecipient = this.mailItem.Recipients.Add(primarySmtpAddress);
			}
			mailRecipient.ORcpt = orcpt;
			mailRecipient.DsnRequested = dsnRequested;
			DirectoryItem.Set(entry, mailRecipient);
			Resolver.SetResolved(mailRecipient);
			if (!this.encounteredMemoryPressure)
			{
				if (!this.ShouldShrinkMemory())
				{
					transportMailItem.ADRecipientCache.AddCacheEntry(new SmtpProxyAddress(primarySmtpAddress, true), new Result<TransportMiniRecipient>(entry, null));
				}
				else
				{
					this.ClearADRecipientCache();
					this.encounteredMemoryPressure = true;
				}
			}
			if (!flag)
			{
				this.numRecipientsAddedForCurrentExpansion++;
			}
			return mailRecipient;
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x000598C0 File Offset: 0x00057AC0
		protected virtual bool ShouldShrinkMemory()
		{
			return Components.ResourceManager.ShouldShrinkDownMemoryCaches;
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x000598CC File Offset: 0x00057ACC
		public void ClearAllPendingChipsStartingAtIndex(int index)
		{
			int num = this.backupMailItem.ClearAllPendingChipsStartingAtIndex(index, null);
			if (num > 0)
			{
				if (this.numRecipientsAddedForCurrentExpansion < num * ResolverConfiguration.ExpansionSizeLimit)
				{
					throw new InvalidOperationException("Trying to remove more recipients than was originally added");
				}
				this.numRecipientsAddedForCurrentExpansion -= num * ResolverConfiguration.ExpansionSizeLimit;
			}
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00059919 File Offset: 0x00057B19
		public void ParkCurrentTopLevelRecipientAck(AckStatusAndResponse ackStatusAndResponse)
		{
			if (this.ackPending)
			{
				throw new InvalidOperationException("Ack already pending for the top level recipient");
			}
			this.ackPending = true;
			this.pendingAckStatusAndResponse = ackStatusAndResponse;
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x0005993C File Offset: 0x00057B3C
		internal static bool IsEncapsulatedAddress(RoutingAddress address, string defaultDomainName)
		{
			return string.Equals(address.DomainPart, defaultDomainName, StringComparison.OrdinalIgnoreCase) && SmtpProxyAddress.IsEncapsulatedAddress((string)address);
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x0005995C File Offset: 0x00057B5C
		internal static RoutingAddress GetPrimarySmtpAddress(ADRawEntry entry)
		{
			ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)entry[ADRecipientSchema.EmailAddresses];
			ProxyAddress proxyAddress = proxyAddressCollection.FindPrimary(ProxyAddressPrefix.Smtp);
			string address = (null == proxyAddress) ? SmtpAddress.Empty.ToString() : proxyAddress.AddressString;
			return new RoutingAddress(address);
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x000599B1 File Offset: 0x00057BB1
		internal static string GetLegacyExchangeDN(ADRawEntry entry)
		{
			return (string)entry[ADRecipientSchema.LegacyExchangeDN];
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x000599C3 File Offset: 0x00057BC3
		internal static bool IsSmtpAddress(ProxyAddress address)
		{
			return address.Prefix.Equals(ProxyAddressPrefix.Smtp);
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x000599D5 File Offset: 0x00057BD5
		internal static bool IsX500Address(ProxyAddress address)
		{
			return address.Prefix.Equals(ProxyAddressPrefix.X500);
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x000599E7 File Offset: 0x00057BE7
		internal static bool IsExAddress(ProxyAddress address)
		{
			return address.Prefix.Equals(ProxyAddressPrefix.LegacyDN);
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x000599F9 File Offset: 0x00057BF9
		internal static bool IsInvalidAddress(ProxyAddress address)
		{
			return address.Prefix.Equals(ProxyAddressPrefix.Invalid);
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x00059A0B File Offset: 0x00057C0B
		internal static void FailRecipient(MailRecipient recipient, SmtpResponse response)
		{
			recipient.Ack(AckStatus.Fail, response);
			if (Resolver.PerfCounters != null)
			{
				Resolver.PerfCounters.FailedRecipientsTotal.Increment();
			}
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x00059A2C File Offset: 0x00057C2C
		[Conditional("DEBUG")]
		internal void LogMailItem(string title, TransportMailItem mailItem)
		{
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x00059A30 File Offset: 0x00057C30
		private static bool TryGetRecipientResolveAddress(MailRecipient recipient, out ProxyAddress resolveAddress)
		{
			resolveAddress = null;
			ExTraceGlobals.ResolverTracer.TraceDebug<RoutingAddress>(0L, "Get ProxyAddress for {0}", recipient.Email);
			if (recipient.IsProcessed)
			{
				ExTraceGlobals.ResolverTracer.TraceDebug<AckStatus, SmtpResponse>(0L, "skipping acked recipient: AckStatus = {0}, SmtpResponse = {1}", recipient.AckStatus, recipient.SmtpResponse);
				return false;
			}
			if (Resolver.IsResolved(recipient))
			{
				ExTraceGlobals.ResolverTracer.TraceDebug<RoutingAddress>(0L, "skipping previously resolved recipient {0}", recipient.Email);
				return false;
			}
			if (Resolver.TryDeencapsulate(recipient.Email, out resolveAddress))
			{
				ExTraceGlobals.ResolverTracer.TraceDebug<ProxyAddress>(0L, "inner address = {0}", resolveAddress);
				if (Resolver.IsSmtpAddress(resolveAddress))
				{
					ExTraceGlobals.ResolverTracer.TraceDebug(0L, "encapsulated SMTP address");
					Resolver.FailRecipient(recipient, AckReason.EncapsulatedSmtpAddress);
					return false;
				}
				if (Resolver.IsX500Address(resolveAddress))
				{
					ExTraceGlobals.ResolverTracer.TraceDebug(0L, "encapsulated X.500 address");
					Resolver.FailRecipient(recipient, AckReason.EncapsulatedX500Address);
					return false;
				}
				if (Resolver.IsInvalidAddress(resolveAddress))
				{
					ExTraceGlobals.ResolverTracer.TraceDebug(0L, "encapsulated INVALID address");
					Resolver.FailRecipient(recipient, AckReason.EncapsulatedInvalidAddress);
					return false;
				}
			}
			else
			{
				resolveAddress = new SmtpProxyAddress((string)recipient.Email, false);
			}
			return true;
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x00059B4C File Offset: 0x00057D4C
		private static TransportMailItem ForkMailItem(List<MailRecipient> recipientsToDefer, TransportMailItem mailItem, DeferReason deferReason)
		{
			TransportMailItem transportMailItem = mailItem.NewCloneWithoutRecipients();
			foreach (MailRecipient mailRecipient in recipientsToDefer)
			{
				mailRecipient.MoveTo(transportMailItem);
				if (deferReason == DeferReason.AmbiguousRecipient)
				{
					string text = mailRecipient.Email.ToString();
					Resolver.EventLogger.LogEvent(mailItem.OrganizationId, TransportEventLogConstants.Tuple_AmbiguousRecipient, text, text);
					string notificationReason = string.Format("More than one Active Directory object is configured with the recipient address {0}.", text);
					EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "A3C4E4B6-490E-40e2-89D6-7FBF03ADCFB5", null, notificationReason, ResultSeverityLevel.Warning, false);
				}
			}
			transportMailItem.CommitLazy();
			return transportMailItem;
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00059C00 File Offset: 0x00057E00
		private static bool IsExpandableEntry(ADRawEntry entry)
		{
			if (entry[ADRecipientSchema.ForwardingAddress] != null)
			{
				return true;
			}
			Microsoft.Exchange.Data.Directory.Recipient.RecipientType recipientType = (Microsoft.Exchange.Data.Directory.Recipient.RecipientType)entry[ADRecipientSchema.RecipientType];
			return recipientType == Microsoft.Exchange.Data.Directory.Recipient.RecipientType.Group || recipientType == Microsoft.Exchange.Data.Directory.Recipient.RecipientType.MailUniversalDistributionGroup || recipientType == Microsoft.Exchange.Data.Directory.Recipient.RecipientType.MailUniversalSecurityGroup || recipientType == Microsoft.Exchange.Data.Directory.Recipient.RecipientType.MailNonUniversalGroup || recipientType == Microsoft.Exchange.Data.Directory.Recipient.RecipientType.DynamicDistributionGroup;
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00059C48 File Offset: 0x00057E48
		private static void AddP2RecipientsToPrefetch(EmailRecipientCollection recipientCollection, List<ProxyAddress> lookupAddressList, HashSet<ProxyAddress> uniqueProxyAddresses, List<ProxyAddress> senderAndP2Recipients)
		{
			if (recipientCollection.Count > ADRecipientObjectSession.ReadMultipleMaxBatchSize * 5)
			{
				return;
			}
			foreach (EmailRecipient emailRecipient in recipientCollection)
			{
				if (SmtpAddress.IsValidSmtpAddress(emailRecipient.SmtpAddress))
				{
					ProxyAddress item = new SmtpProxyAddress(emailRecipient.SmtpAddress, false);
					if (uniqueProxyAddresses.Add(item))
					{
						lookupAddressList.Add(item);
						senderAndP2Recipients.Add(item);
					}
				}
			}
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x00059CD0 File Offset: 0x00057ED0
		private TransportMiniRecipient GetADRawEntryFromCache(ProxyAddress proxyAddress)
		{
			if (proxyAddress == null)
			{
				return null;
			}
			Result<TransportMiniRecipient> result;
			if (this.mailItem.ADRecipientCache.TryGetValue(proxyAddress, out result))
			{
				ExTraceGlobals.ResolverTracer.TraceDebug<ProxyAddress>(0L, "Find ADRawEntry for ProxyAddress {0} in the cache", proxyAddress);
				return result.Data;
			}
			ExTraceGlobals.ResolverTracer.TraceDebug<ProxyAddress>(0L, "Doesn't find ADRawEntry for ProxyAddress {0} in the cache", proxyAddress);
			return null;
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x00059D2C File Offset: 0x00057F2C
		private void LookupRecipientsAndSender()
		{
			List<ProxyAddress> list = new List<ProxyAddress>(this.mailItem.Recipients.Count + 2);
			List<MailRecipient> list2 = new List<MailRecipient>(this.mailItem.Recipients.Count);
			ExTraceGlobals.FaultInjectionTracer.TraceTest(2951097661U);
			foreach (MailRecipient mailRecipient in this.mailItem.Recipients.AllUnprocessed)
			{
				ProxyAddress item;
				if (Resolver.TryGetRecipientResolveAddress(mailRecipient, out item))
				{
					list.Add(item);
					list2.Add(mailRecipient);
				}
			}
			List<ProxyAddress> list3 = new List<ProxyAddress>();
			if (this.sender.P1Address != null)
			{
				list.Add(this.sender.P1Address);
				list3.Add(this.sender.P1Address);
			}
			if (this.sender.P2Address != null)
			{
				list.Add(this.sender.P2Address);
				list3.Add(this.sender.P2Address);
			}
			int count = list.Count;
			HashSet<ProxyAddress> uniqueProxyAddresses = new HashSet<ProxyAddress>(list);
			Resolver.AddP2RecipientsToPrefetch(this.mailItem.Message.To, list, uniqueProxyAddresses, list3);
			Resolver.AddP2RecipientsToPrefetch(this.mailItem.Message.Cc, list, uniqueProxyAddresses, list3);
			Resolver.AddP2RecipientsToPrefetch(this.mailItem.Message.Bcc, list, uniqueProxyAddresses, list3);
			IList<Result<TransportMiniRecipient>> list4 = this.mailItem.ADRecipientCache.FindAndCacheRecipients(list);
			List<MailRecipient> list5 = null;
			for (int i = 0; i < list4.Count; i++)
			{
				if (i < list2.Count)
				{
					bool flag;
					this.CopyADAttributesToRecipient(list4[i], list2[i], out flag);
					if (flag)
					{
						if (list5 == null)
						{
							list5 = new List<MailRecipient>();
						}
						list5.Add(list2[i]);
					}
				}
				else if (i < count)
				{
					this.UpdateSenderPerfCounters(list4[i], list[i]);
				}
			}
			if (list5 != null && list5.Count > 0)
			{
				if (Components.TransportAppConfig.Resolver.NDRForAmbiguousRecipients)
				{
					ExTraceGlobals.ResolverTracer.TraceError(0L, "NDRing for ambiguous recipients");
					using (List<MailRecipient>.Enumerator enumerator2 = list5.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							MailRecipient recipient = enumerator2.Current;
							Resolver.FailRecipient(recipient, AckReason.AmbiguousAddressPermanent);
						}
						goto IL_283;
					}
				}
				ExTraceGlobals.ResolverTracer.TraceDebug(0L, "Deferring for ambiguous recipients");
				this.BifurcateAndDeferAmbigousRecipients(list5, this.mailItem, this.taskContext, AckReason.AmbiguousAddressTransient);
			}
			IL_283:
			this.MarkSenderAndP2Recipients(this.mailItem.ADRecipientCache, list3);
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x00059FEC File Offset: 0x000581EC
		private void MarkSenderAndP2Recipients(ADRecipientCache<TransportMiniRecipient> adRecipientCache, List<ProxyAddress> senderAndP2Recipients)
		{
			foreach (ProxyAddress proxyAddress in senderAndP2Recipients)
			{
				Result<TransportMiniRecipient> result;
				if (adRecipientCache.TryGetValue(proxyAddress, out result) && result.Data != null)
				{
					result.Data.SetSenderOrP2RecipientEntry();
				}
			}
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x0005A054 File Offset: 0x00058254
		private void CopyADAttributesToRecipient(Result<TransportMiniRecipient> result, MailRecipient recipient, out bool isAmbiguousRecipient)
		{
			string arg = recipient.Email.ToString();
			isAmbiguousRecipient = false;
			ExTraceGlobals.ResolverTracer.TraceDebug<string, ProviderError>(0L, "Lookup result for recipient {0} is {1}", arg, result.Error);
			if (result.Error == null)
			{
				TransportMiniRecipient data = result.Data;
				if (data != null)
				{
					string primarySmtpAddress = DirectoryItem.GetPrimarySmtpAddress(data);
					if (primarySmtpAddress == null)
					{
						ExTraceGlobals.ResolverTracer.TraceError(0L, "invalid or missing primary SMTP address");
						Resolver.FailRecipient(recipient, AckReason.BadOrMissingPrimarySmtpAddress);
						return;
					}
					DirectoryItem.Set(data, recipient);
					this.RewriteEmail(recipient, new SmtpProxyAddress(primarySmtpAddress, false), MessageTrackingSource.ROUTING);
					return;
				}
			}
			else if (result.Error is NonUniqueAddressError)
			{
				ExTraceGlobals.ResolverTracer.TraceError(0L, "ambiguous address");
				isAmbiguousRecipient = true;
				if (Resolver.PerfCounters != null)
				{
					Resolver.PerfCounters.AmbiguousRecipientsTotal.Increment();
					return;
				}
			}
			else if (result.Error is ObjectValidationError)
			{
				ExTraceGlobals.ResolverTracer.TraceError(0L, "Invalid data");
				Resolver.FailRecipient(recipient, AckReason.InvalidObjectOnSearch);
			}
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x0005A14C File Offset: 0x0005834C
		private void UpdateSenderPerfCounters(Result<TransportMiniRecipient> result, ProxyAddress senderAddress)
		{
			if (Resolver.PerfCounters == null || result.Error == null)
			{
				return;
			}
			if (OneOffItem.IsLocalAddress(senderAddress, this.configuration.AcceptedDomains))
			{
				Resolver.PerfCounters.UnresolvedOrgSendersTotal.Increment();
			}
			if (result.Error is NonUniqueAddressError)
			{
				string text = senderAddress.ToString();
				ExTraceGlobals.ResolverTracer.TraceDebug<string>(0L, "Ambiguous sender {0}", text);
				Resolver.EventLogger.LogEvent(TransportEventLogConstants.Tuple_AmbiguousSender, text, new object[]
				{
					text
				});
				Resolver.PerfCounters.AmbiguousSendersTotal.Increment();
			}
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x0005A1E4 File Offset: 0x000583E4
		private bool CheckRecipientLimit()
		{
			if (!this.Message.RecipientLimitVerified)
			{
				if (!DeliveryRestriction.IsPrivilegedSender(this.sender, this.isAuthenticated, this.configuration.PrivilegedSenders) && !this.sender.EffectiveRecipientLimit.IsUnlimited && this.recipientStack.Count > this.sender.EffectiveRecipientLimit.Value)
				{
					ExTraceGlobals.ResolverTracer.TraceDebug<int>(0L, "Exceeded sender recipient limit {0}", this.sender.EffectiveRecipientLimit.Value);
					this.mailItem.AddDsnParameters("MaxRecipientCount", this.sender.EffectiveRecipientLimit.Value);
					this.mailItem.AddDsnParameters("CurrentRecipientCount", this.recipientStack.Count);
					this.FailAllRecipients(AckReason.RecipientLimitExceeded);
					return false;
				}
				this.Message.RecipientLimitVerified = true;
			}
			return true;
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x0005A2E4 File Offset: 0x000584E4
		private void FailAllRecipients(SmtpResponse response)
		{
			foreach (MailRecipient recipient in this.mailItem.Recipients.AllUnprocessed)
			{
				Resolver.FailRecipient(recipient, response);
			}
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x0005A33C File Offset: 0x0005853C
		private bool CheckSenderSizeRestriction()
		{
			long num;
			long num2;
			RestrictionCheckResult restrictionCheckResult = DeliveryRestriction.CheckSenderSizeRestriction(this.sender, this.message.OriginalMessageSize, this.isAuthenticated, this.mailItem.IsJournalReport(), this.configuration.MaxSendSize, this.configuration.PrivilegedSenders, out num, out num2);
			ExTraceGlobals.ResolverTracer.TraceDebug(0L, "Sender Size Restriction Check returns {0}: sender {1} stream size {2} authenticated {3}", new object[]
			{
				(int)restrictionCheckResult,
				this.sender.PrimarySmtpAddress,
				this.message.OriginalMessageSize,
				this.isAuthenticated
			});
			if (ADRecipientRestriction.Failed(restrictionCheckResult))
			{
				if (restrictionCheckResult == (RestrictionCheckResult)2147483650U || restrictionCheckResult == (RestrictionCheckResult)2147483651U)
				{
					this.mailItem.AddDsnParameters("MaxMessageSizeInKB", num);
					this.mailItem.AddDsnParameters("CurrentMessageSizeInKB", num2);
				}
				this.FailAllRecipients(DeliveryRestriction.GetResponseForResult(restrictionCheckResult));
				return false;
			}
			return true;
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x0005A430 File Offset: 0x00058630
		private ResolverLogLevel GetEffectiveLogLevel()
		{
			ResolverLogLevel val = ResolverLogLevel.Disabled;
			int num;
			if (this.mailItem.ExtendedProperties.TryGetValue<int>("Microsoft.Exchange.Transport.ResolverLogLevel", out num))
			{
				val = (ResolverLogLevel)num;
			}
			return (ResolverLogLevel)Math.Max((int)ResolverConfiguration.ResolverLogLevel, (int)val);
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x0005A465 File Offset: 0x00058665
		private void CommitPendingChips()
		{
			this.backupMailItem.CommitPendingChips();
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x0005A472 File Offset: 0x00058672
		private void ClearADRecipientCache()
		{
			this.backupMailItem.ClearADRecipientCache();
			if (this.mailItem.ADRecipientCache != null)
			{
				this.mailItem.ADRecipientCache.Clear();
			}
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x0005A49C File Offset: 0x0005869C
		private void CheckDLLimitsAndAckRecipientIfPending()
		{
			if (this.CheckDLLimitsRestrictions())
			{
				if (this.ackPending)
				{
					this.currentTopLevelRecipientInProcess.Recipient.ExtendedProperties.SetValue<bool>("Microsoft.Exchange.Transport.Processed", true);
					ExTraceGlobals.ResolverTracer.TraceDebug<RoutingAddress, AckStatus, SmtpResponse>((long)this.GetHashCode(), "Acking top level recipient {0}. AckStatus {1}, AckResponse {2}", this.currentTopLevelRecipientInProcess.Recipient.Email, this.pendingAckStatusAndResponse.AckStatus, this.pendingAckStatusAndResponse.SmtpResponse);
					this.currentTopLevelRecipientInProcess.Recipient.Ack(this.pendingAckStatusAndResponse.AckStatus, this.pendingAckStatusAndResponse.SmtpResponse);
				}
				this.resolverCache.MergeResultsFromCurrentExpansion();
			}
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x0005A544 File Offset: 0x00058744
		private void ResetStateForNextTopLevelRecipient(RecipientItem recipientItem)
		{
			this.currentTopLevelRecipientInProcess = recipientItem;
			this.numRecipientsAddedForCurrentExpansion = 0;
			this.topLevelGroupItem = null;
			this.ackPending = false;
			this.pendingAckStatusAndResponse = Resolver.NoopAckStatusAndResponse;
			this.originalMailItemIndex = this.mailItem.Recipients.Count;
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x0005A584 File Offset: 0x00058784
		private bool CheckDLLimitsRestrictions()
		{
			if (!ResolverConfiguration.LargeDGLimitEnforcementEnabled)
			{
				return true;
			}
			if (this.currentTopLevelRecipientInProcess == null)
			{
				return true;
			}
			if (ResolverConfiguration.LargeDGGroupCount > 0 && this.message.OriginalMessageSize > (long)ResolverConfiguration.LargeDGMaxMessageSize.ToBytes() && this.numRecipientsAddedForCurrentExpansion > ResolverConfiguration.LargeDGGroupCount)
			{
				ExTraceGlobals.ResolverTracer.TraceError((long)this.GetHashCode(), "Message size ({0}) exceeds the allowed size ({1})for large DG. Num recipients expanded {2}, Configured LargeDGGroupCount {3}. Failing DL expansion for {4}", new object[]
				{
					this.message.OriginalMessageSize,
					ResolverConfiguration.LargeDGMaxMessageSize,
					this.numRecipientsAddedForCurrentExpansion,
					ResolverConfiguration.LargeDGGroupCount,
					this.currentTopLevelRecipientInProcess.Recipient.Email
				});
				this.currentTopLevelRecipientInProcess.Recipient.AddDsnParameters("MaxRecipMessageSizeInKB", (long)ResolverConfiguration.LargeDGMaxMessageSize.ToKB());
				this.currentTopLevelRecipientInProcess.Recipient.AddDsnParameters("CurrentMessageSizeInKB", this.message.OriginalMessageSize >> 10);
				Resolver.FailRecipient(this.currentTopLevelRecipientInProcess.Recipient, AckReason.MessageTooLargeForDistributionList);
				this.DiscardRecipientsAddedForCurrentExpansion(AckReason.MessageTooLargeForDistributionList);
				return false;
			}
			RestrictedItem restrictedItem = this.currentTopLevelRecipientInProcess as RestrictedItem;
			if (restrictedItem != null && ResolverConfiguration.LargeDGGroupCountForUnRestrictedDG > 0 && this.numRecipientsAddedForCurrentExpansion > ResolverConfiguration.LargeDGGroupCountForUnRestrictedDG && !restrictedItem.IsDeliveryToGroupRestricted())
			{
				ExTraceGlobals.ResolverTracer.TraceError<RoutingAddress, int, int>((long)this.GetHashCode(), "Total number of recipients expanded for the distribution list {0} is ({1}) and that exceeds the configured limits for the recipient count for an UnRestrictedDL ({2}). Failing DL expansion.", this.currentTopLevelRecipientInProcess.Recipient.Email, this.numRecipientsAddedForCurrentExpansion, ResolverConfiguration.LargeDGGroupCountForUnRestrictedDG);
				Resolver.FailRecipient(this.currentTopLevelRecipientInProcess.Recipient, AckReason.DLExpansionBlockedNeedsSenderRestrictions);
				Resolver.EventLogger.LogEvent(this.mailItem.OrganizationId, TransportEventLogConstants.Tuple_NDRForUnrestrictedLargeDL, this.currentTopLevelRecipientInProcess.Recipient.ToString(), this.currentTopLevelRecipientInProcess.Recipient.ToString());
				string notificationReason = string.Format("Messages to the Distribution group {0} could not be delivered because of security policies.", this.currentTopLevelRecipientInProcess.Recipient.ToString());
				EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "NDRForUnrestrictedLargeDL", null, notificationReason, ResultSeverityLevel.Warning, false);
				this.DiscardRecipientsAddedForCurrentExpansion(AckReason.DLExpansionBlockedNeedsSenderRestrictions);
				return false;
			}
			return true;
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x0005A7BC File Offset: 0x000589BC
		private void DiscardRecipientsAddedForCurrentExpansion(SmtpResponse ackReason)
		{
			this.resolverCache.DiscardResultsFromCurrentExpansion();
			ExTraceGlobals.ResolverTracer.TraceDebug<int>((long)this.GetHashCode(), "Discarding recipients from the original mailItem. BeginIdx {0}", this.originalMailItemIndex);
			List<MailRecipient> list = new List<MailRecipient>();
			for (int i = this.originalMailItemIndex; i < this.mailItem.Recipients.Count; i++)
			{
				MailRecipient mailRecipient = this.mailItem.Recipients[i];
				mailRecipient.Ack(AckStatus.SuccessNoDsn, AckReason.RecipientDiscarded);
				list.Add(mailRecipient);
			}
			this.backupMailItem.DiscardChipsForCurrentExpansion(list);
			LatencyFormatter latencyFormatter = new LatencyFormatter(this.mailItem, Components.Configuration.LocalServer.TransportServer.Fqdn, true);
			MessageTrackingLog.TrackFailedRecipients(MessageTrackingSource.ROUTING, "resolver DL limit restrictions", this.mailItem, (string)this.currentTopLevelRecipientInProcess.Recipient.Email, list, ackReason, latencyFormatter);
		}

		// Token: 0x04000AE3 RID: 2787
		public const string Resolved = "Microsoft.Exchange.Transport.Resolved";

		// Token: 0x04000AE4 RID: 2788
		public const string Processed = "Microsoft.Exchange.Transport.Processed";

		// Token: 0x04000AE5 RID: 2789
		public const string ResolverLogLevelOverride = "Microsoft.Exchange.Transport.ResolverLogLevel";

		// Token: 0x04000AE6 RID: 2790
		private const string PerfInstanceName = "_total";

		// Token: 0x04000AE7 RID: 2791
		public static IComparer<MailRecipient> RecipientHomeMDBComparer = new RecipientHomeMDBComparer();

		// Token: 0x04000AE8 RID: 2792
		private static readonly AckStatusAndResponse NoopAckStatusAndResponse = new AckStatusAndResponse(AckStatus.Pending, SmtpResponse.Empty);

		// Token: 0x04000AE9 RID: 2793
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.ResolverTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x04000AEA RID: 2794
		private static ResolverPerfCountersInstance perfCounters;

		// Token: 0x04000AEB RID: 2795
		private ResolverConfiguration configuration;

		// Token: 0x04000AEC RID: 2796
		private TransportMailItem mailItem;

		// Token: 0x04000AED RID: 2797
		private TaskContext taskContext;

		// Token: 0x04000AEE RID: 2798
		private ResolverMessage message;

		// Token: 0x04000AEF RID: 2799
		private Stack<RecipientItem> recipientStack;

		// Token: 0x04000AF0 RID: 2800
		private Sender sender;

		// Token: 0x04000AF1 RID: 2801
		private bool isAuthenticated;

		// Token: 0x04000AF2 RID: 2802
		private bool encounteredMemoryPressure;

		// Token: 0x04000AF3 RID: 2803
		private LimitedMailItem backupMailItem;

		// Token: 0x04000AF4 RID: 2804
		private ResolverCache resolverCache;

		// Token: 0x04000AF5 RID: 2805
		private RecipientItem currentTopLevelRecipientInProcess;

		// Token: 0x04000AF6 RID: 2806
		private GroupItem topLevelGroupItem;

		// Token: 0x04000AF7 RID: 2807
		private int originalMailItemIndex;

		// Token: 0x04000AF8 RID: 2808
		private bool ackPending;

		// Token: 0x04000AF9 RID: 2809
		private AckStatusAndResponse pendingAckStatusAndResponse = Resolver.NoopAckStatusAndResponse;

		// Token: 0x04000AFA RID: 2810
		private int numRecipientsAddedForCurrentExpansion;

		// Token: 0x04000AFB RID: 2811
		private Resolver.DeferBifurcatedDelegate deferHandler;

		// Token: 0x020001EF RID: 495
		// (Invoke) Token: 0x0600162C RID: 5676
		public delegate void DeferBifurcatedDelegate(TransportMailItem mailItem, TaskContext taskContext, DeferReason reason, TimeSpan deferTime);
	}
}
