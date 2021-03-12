using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport.Logging.MessageTracking;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001CD RID: 461
	internal class ContactItem : RestrictedItem
	{
		// Token: 0x06001516 RID: 5398 RVA: 0x00054B40 File Offset: 0x00052D40
		public ContactItem(MailRecipient recipient) : base(recipient)
		{
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x00054B49 File Offset: 0x00052D49
		public string ExternalEmailAddress
		{
			get
			{
				return base.GetProperty<string>("Microsoft.Exchange.Transport.DirectoryData.ExternalEmailAddress");
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x00054B56 File Offset: 0x00052D56
		public IList<string> EmailAddresses
		{
			get
			{
				return base.GetListProperty<string>("Microsoft.Exchange.Transport.DirectoryData.EmailAddresses");
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x00054B64 File Offset: 0x00052D64
		public int? InternetEncoding
		{
			get
			{
				return base.GetProperty<int?>("Microsoft.Exchange.Transport.DirectoryData.InternetEncoding", null);
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x00054B88 File Offset: 0x00052D88
		public bool? UseMapiRichTextFormat
		{
			get
			{
				return base.GetProperty<bool?>("Microsoft.Exchange.Transport.DirectoryData.UseMapiRichTextFormat", null);
			}
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x00054BA9 File Offset: 0x00052DA9
		public static bool TryGetTargetAddress(MailRecipient recipient, out string targetAddress)
		{
			return recipient.ExtendedProperties.TryGetValue<string>("Microsoft.Exchange.Transport.DirectoryData.ExternalEmailAddress", out targetAddress) && !string.IsNullOrEmpty(targetAddress);
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x00054BCC File Offset: 0x00052DCC
		public override void Allow(Expansion expansion)
		{
			if (this.ExternalEmailAddress == null)
			{
				base.FailRecipient(AckReason.ContactMissingTargetAddress);
				return;
			}
			ProxyAddress proxyAddress = ProxyAddress.Parse(this.ExternalEmailAddress);
			if (string.IsNullOrEmpty(proxyAddress.PrefixString) || string.IsNullOrEmpty(proxyAddress.AddressString) || proxyAddress.GetType() == typeof(InvalidProxyAddress))
			{
				base.FailRecipient(AckReason.ContactInvalidTargetAddress);
				ExTraceGlobals.ResolverTracer.TraceDebug<ProxyAddress, RoutingAddress>((long)this.GetHashCode(), "Invalid target address '{0}' for recipient '{1}'", proxyAddress, base.Recipient.Email);
				return;
			}
			if (!this.TargetIsProxy())
			{
				ExTraceGlobals.ResolverTracer.TraceDebug<ProxyAddress>((long)this.GetHashCode(), "possible chain to {0}", proxyAddress);
				ProxyAddress proxyAddress2 = ProxyAddress.Parse(this.ExternalEmailAddress);
				Result<TransportMiniRecipient> result = expansion.MailItem.ADRecipientCache.FindAndCacheRecipient(proxyAddress2);
				if (result.Error is NonUniqueAddressError)
				{
					if (Components.TransportAppConfig.Resolver.NDRForAmbiguousRecipients)
					{
						ExTraceGlobals.ResolverTracer.TraceError(0L, "NDRing for ambiguous recipient");
						base.FailRecipient(AckReason.ContactChainAmbiguousPermanent);
					}
					else
					{
						ExTraceGlobals.ResolverTracer.TraceDebug(0L, "Deferring for ambiguous recipient");
						List<MailRecipient> list = new List<MailRecipient>(1);
						list.Add(base.Recipient);
						expansion.Resolver.BifurcateAndDeferAmbigousRecipients(list, expansion.MailItem, expansion.TaskContext, AckReason.ContactChainAmbiguousTransient);
					}
					if (Resolver.PerfCounters != null)
					{
						Resolver.PerfCounters.AmbiguousRecipientsTotal.Increment();
					}
				}
				else if (result.Error is ObjectValidationError)
				{
					ExTraceGlobals.ResolverTracer.TraceDebug((long)this.GetHashCode(), "chain points to an invalid object");
					base.FailRecipient(AckReason.ContactChainInvalid);
					return;
				}
				TransportMiniRecipient data = result.Data;
				if (data != null)
				{
					ExTraceGlobals.ResolverTracer.TraceDebug<string>((long)this.GetHashCode(), "contact chained to {0}", this.ExternalEmailAddress);
					Expansion expansion2 = base.Expand(expansion, HistoryType.Forwarded);
					if (expansion2 == null)
					{
						ExTraceGlobals.ResolverTracer.TraceDebug((long)this.GetHashCode(), "loop in contact chain");
						return;
					}
					string orcpt = base.Recipient.ORcpt ?? ("rfc822;" + base.Recipient.Email);
					MailRecipient mailRecipient = expansion2.Add(data, base.Recipient.DsnRequested, orcpt);
					if (mailRecipient == null)
					{
						ExTraceGlobals.ResolverTracer.TraceDebug((long)this.GetHashCode(), "contact chain not mail enabled");
						base.FailRecipient(AckReason.ContactChainNotMailEnabled);
						return;
					}
					OrarGenerator.CopyOrar(base.Recipient, mailRecipient);
					ExTraceGlobals.ResolverTracer.TraceDebug((long)this.GetHashCode(), "contact chain handled");
					base.Recipient.Ack(AckStatus.SuccessNoDsn, AckReason.ContactChainHandled);
					MsgTrackRedirectInfo msgTrackInfo = new MsgTrackRedirectInfo(base.Recipient.Email, mailRecipient.Email, null);
					MessageTrackingLog.TrackRedirect(MessageTrackingSource.ROUTING, expansion.MailItem, msgTrackInfo);
					return;
				}
				else
				{
					ExTraceGlobals.ResolverTracer.TraceDebug((long)this.GetHashCode(), "possible chain address was not in the directory");
				}
			}
			if (!expansion.Resolver.RewriteEmail(base.Recipient, proxyAddress, MessageTrackingSource.ROUTING))
			{
				base.FailRecipient(AckReason.UnencapsulatableTargetAddress);
				return;
			}
			RoutingAddress purportedResponsibleAddress = expansion.Sender.GetPurportedResponsibleAddress();
			if (purportedResponsibleAddress == Sender.NoPRA || !purportedResponsibleAddress.IsValid || !OneOffItem.IsAuthoritativeOrInternalRelaySmtpAddress(purportedResponsibleAddress, expansion.MailItem.OrganizationId))
			{
				History history = History.ReadFrom(base.Recipient);
				if (history != null && history.Records != null)
				{
					for (int i = history.Records.Count - 1; i >= 0; i--)
					{
						HistoryRecord historyRecord = history.Records[history.Records.Count - 1];
						if (historyRecord.Address.IsValid && OneOffItem.IsLocalSmtpAddress(historyRecord.Address, expansion.Configuration.AcceptedDomains))
						{
							MessageTemplate messageTemplate = MessageTemplate.ReadFrom(base.Recipient);
							string reversePath = null;
							if (expansion.MailItem.From != RoutingAddress.NullReversePath && messageTemplate.ReversePath == null)
							{
								reversePath = historyRecord.Address.ToString();
							}
							MessageTemplate template = new MessageTemplate(reversePath, (AutoResponseSuppress)0, historyRecord.Address.ToString(), false, false, false, false);
							base.ApplyTemplate(template);
							return;
						}
					}
				}
			}
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x00054FFC File Offset: 0x000531FC
		public override void PostProcess(Expansion expansion)
		{
			ExTraceGlobals.ResolverTracer.TraceDebug<ResolverMessageType, RoutingAddress>(0L, "ContactItem:Msg Type={0}, recipientAddress={1}", expansion.Message.Type, base.Recipient.Email);
			OofRestriction.ExternalUserOofCheck(expansion, base.Recipient);
			MsgTypeRestriction.ExternalRecipientMessageTypeCheck(expansion, base.Recipient);
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x00055048 File Offset: 0x00053248
		public override void AddItemVisited(Expansion expansion)
		{
			ExTraceGlobals.ResolverTracer.TraceDebug<RoutingAddress>(0L, "Adding contact item '{0}' to the visited list", base.Email);
			expansion.Resolver.ResolverCache.AddToResolvedRecipientCache(base.ObjectGuid);
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x00055078 File Offset: 0x00053278
		private bool TargetIsProxy()
		{
			string externalEmailAddress = this.ExternalEmailAddress;
			foreach (string a in this.EmailAddresses)
			{
				if (string.Equals(a, externalEmailAddress, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}
	}
}
