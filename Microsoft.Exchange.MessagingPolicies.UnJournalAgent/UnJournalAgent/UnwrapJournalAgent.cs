using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.MessagingPolicies.Journaling;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Exchange.Transport.Internal;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.MessagingPolicies.UnJournalAgent
{
	// Token: 0x0200001D RID: 29
	internal class UnwrapJournalAgent : RoutingAgent
	{
		// Token: 0x06000062 RID: 98 RVA: 0x0000418C File Offset: 0x0000238C
		public UnwrapJournalAgent(bool isEnabled, JournalPerfCountersWrapper perfCountersWrapper)
		{
			this.isEHAJournalingEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Ipaed.EHAJournaling.Enabled;
			this.isEnabled = isEnabled;
			base.OnSubmittedMessage += this.SubmitMessage;
			this.perfCountersWrapper = perfCountersWrapper;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000041E7 File Offset: 0x000023E7
		private static void CommitTrackAndEnqueue(TransportMailItem journalMailItem, TransportMailItem transportMailItem, string agentName)
		{
			transportMailItem.CommitLazy();
			SubmitHelper.TrackAndEnqueue(journalMailItem, transportMailItem, Components.Configuration.LocalServer.TransportServer.Fqdn, Components.Configuration.LocalServer.TransportServer.AdminDisplayVersion, agentName);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004224 File Offset: 0x00002424
		private static TransportMailItem CreateNewTransportMailItem(IReadOnlyMailItem originalMailItem, EmailMessage emailMessage, OrganizationId organizationId, string agentName, string perfAttribution, RoutingAddress p2FromAddress)
		{
			TransportMailItem transportMailItem = TransportMailItem.NewSideEffectMailItem(originalMailItem, organizationId, LatencyComponent.Agent, MailDirectionality.Incoming, default(Guid));
			if (transportMailItem != null)
			{
				transportMailItem.PerfCounterAttribution = perfAttribution;
				transportMailItem.MimeDocument = emailMessage.MimeDocument;
				transportMailItem.ReceiveConnectorName = "Agent:" + agentName;
				transportMailItem.From = p2FromAddress;
				SubmitHelper.PatchHeaders(transportMailItem, Components.Configuration.LocalServer.TransportServer.Fqdn, Components.Configuration.LocalServer.TransportServer.AdminDisplayVersion);
				TransportFacades.EnsureSecurityAttributes(transportMailItem);
				SubmitHelper.StampOriginalMessageSize(transportMailItem);
			}
			return transportMailItem;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000042B5 File Offset: 0x000024B5
		private static void SetPrioritization(ITransportMailItemFacade tmi, bool isSourceEha)
		{
			if (isSourceEha)
			{
				tmi.PrioritizationReason = "eha legacy archive journaling";
			}
			else
			{
				tmi.PrioritizationReason = "Live archive journaling";
			}
			tmi.Priority = DeliveryPriority.None;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000042DC File Offset: 0x000024DC
		private static void SetAutoResponseSuppress(TransportMailItem mailItem, AutoResponseSuppress suppress)
		{
			if (mailItem != null && mailItem.RootPart != null && mailItem.RootPart.Headers != null)
			{
				Header[] array = mailItem.RootPart.Headers.FindAll("X-Auto-Response-Suppress");
				if (array.Length == 0)
				{
					Header newChild = new AsciiTextHeader("X-Auto-Response-Suppress", ResolverMessage.FormatAutoResponseSuppressHeaderValue(suppress));
					mailItem.RootPart.Headers.AppendChild(newChild);
					return;
				}
				array[0].Value = ResolverMessage.FormatAutoResponseSuppressHeaderValue(suppress);
				for (int i = 1; i < array.Length; i++)
				{
					mailItem.RootPart.Headers.RemoveChild(array[i]);
				}
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004374 File Offset: 0x00002574
		private static void AddRecipientsToTransportMailItem(List<RoutingAddress> targetRecipients, TransportMailItem unwrappedMailItem)
		{
			if (unwrappedMailItem.Recipients != null)
			{
				unwrappedMailItem.Recipients.Clear();
				foreach (RoutingAddress routingAddress in targetRecipients)
				{
					unwrappedMailItem.Recipients.Add(routingAddress.ToString());
				}
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000043E8 File Offset: 0x000025E8
		private static List<AddressInfo> ConvertAndGetAddressInfoList(List<RoutingAddress> list)
		{
			List<AddressInfo> list2 = new List<AddressInfo>(list.Count);
			foreach (RoutingAddress address in list)
			{
				AddressInfo item = new AddressInfo(address);
				list2.Add(item);
			}
			return list2;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000444C File Offset: 0x0000264C
		private static void ApplyHeaderFirewall(HeaderList headers)
		{
			HeaderFirewall.Filter(headers, ~RestrictedHeaderSet.MTA);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004458 File Offset: 0x00002658
		private static void NdrAllRecipients(MailItem mailItem, SmtpResponse smtpResponse)
		{
			int count = mailItem.Recipients.Count;
			EnvelopeRecipientCollection recipients = mailItem.Recipients;
			for (int i = count - 1; i >= 0; i--)
			{
				recipients.Remove(recipients[i], DsnType.Failure, smtpResponse);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004498 File Offset: 0x00002698
		private static UnjournalRecipientType GetRecipientType(MailItem mailItem, string email)
		{
			IADRecipientCache iadrecipientCache = (IADRecipientCache)((ITransportMailItemWrapperFacade)mailItem).TransportMailItem.ADRecipientCacheAsObject;
			if (ProxyAddressBase.IsAddressStringValid(email) && RoutingAddress.IsValidAddress(email))
			{
				SmtpProxyAddress proxyAddress = new SmtpProxyAddress(email, true);
				Result<ADRawEntry> result = default(Result<ADRawEntry>);
				result = iadrecipientCache.FindAndCacheRecipient(proxyAddress);
				if (result.Data == null || result.Error == ProviderError.NotFound)
				{
					return UnjournalRecipientType.External;
				}
				object obj;
				if (result.Data.TryGetValueWithoutDefault(ADRecipientSchema.RecipientType, out obj))
				{
					if (obj != null && obj is Microsoft.Exchange.Data.Directory.Recipient.RecipientType)
					{
						Microsoft.Exchange.Data.Directory.Recipient.RecipientType recipientType = (Microsoft.Exchange.Data.Directory.Recipient.RecipientType)obj;
						if (recipientType == Microsoft.Exchange.Data.Directory.Recipient.RecipientType.UserMailbox)
						{
							return UnjournalRecipientType.Mailbox;
						}
						if (recipientType == Microsoft.Exchange.Data.Directory.Recipient.RecipientType.MailNonUniversalGroup || recipientType == Microsoft.Exchange.Data.Directory.Recipient.RecipientType.MailUniversalDistributionGroup || recipientType == Microsoft.Exchange.Data.Directory.Recipient.RecipientType.MailUniversalSecurityGroup || recipientType == Microsoft.Exchange.Data.Directory.Recipient.RecipientType.DynamicDistributionGroup)
						{
							return UnjournalRecipientType.DistributionGroup;
						}
					}
					return UnjournalRecipientType.ResolvedOther;
				}
			}
			return UnjournalRecipientType.External;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000454C File Offset: 0x0000274C
		private static RoutingAddress GetJournalArchiveAddress(MailItem mailItem, string email)
		{
			ITransportMailItemFacade transportMailItem = ((ITransportMailItemWrapperFacade)mailItem).TransportMailItem;
			TransportMailItem transportMailItem2 = transportMailItem as TransportMailItem;
			string arg = (transportMailItem2 == null) ? string.Empty : transportMailItem2.MsgId.ToString();
			string text = string.Empty;
			IADRecipientCache iadrecipientCache = (IADRecipientCache)((ITransportMailItemWrapperFacade)mailItem).TransportMailItem.ADRecipientCacheAsObject;
			if (ProxyAddressBase.IsAddressStringValid(email) && RoutingAddress.IsValidAddress(email))
			{
				SmtpProxyAddress proxyAddress = new SmtpProxyAddress(email, true);
				Result<ADRawEntry> result = default(Result<ADRawEntry>);
				result = iadrecipientCache.FindAndCacheRecipient(proxyAddress);
				if (result.Data == null || result.Error == ProviderError.NotFound)
				{
					text = string.Format("UnWrapJournalAgent: GetJournalArchiveAddress, message id = {0} , ADlookup failed or found zero results for email address of {1} ", arg, email);
					ExTraceGlobals.JournalingTracer.TraceDebug(0L, text);
					UnwrapJournalAgent.PublishMonitoringResults(mailItem, email, text);
					return RoutingAddress.Empty;
				}
				ADPropertyDefinition[] extraProperties = new ADPropertyDefinition[]
				{
					MiniRecipientSchema.JournalArchiveAddress
				};
				object obj;
				if (iadrecipientCache.ReloadRecipient(proxyAddress, extraProperties).Data.TryGetValueWithoutDefault(MiniRecipientSchema.JournalArchiveAddress, out obj))
				{
					if (obj != null && obj is SmtpAddress)
					{
						SmtpAddress smtpAddress = (SmtpAddress)obj;
						if (smtpAddress != SmtpAddress.Empty && smtpAddress.IsValidAddress && smtpAddress != SmtpAddress.NullReversePath)
						{
							return new RoutingAddress(smtpAddress.ToString());
						}
						text = string.Format("For message id = {0} , Loaded journalarchiveaddress property of {1} from transport cache after AD lookup completed, value returned is empty/invalid/nullreverseaddress: {2}", arg, email, smtpAddress);
						ExTraceGlobals.JournalingTracer.TraceDebug(0L, text);
					}
					else
					{
						text = string.Format("For message id = {0} , Loaded journalarchiveaddress property of {1} from transport cache after AD lookup completed, but value returned is null or datatype mismatches : {2}", arg, email, obj);
						ExTraceGlobals.JournalingTracer.TraceDebug(0L, text);
					}
				}
				else
				{
					text = string.Format("For message id = {0} , Failed to load journalarchiveaddress property of {1} from transport cache after AD lookup completed", arg, email);
					ExTraceGlobals.JournalingTracer.TraceDebug(0L, text);
				}
			}
			UnwrapJournalAgent.PublishMonitoringResults(mailItem, email, text);
			return RoutingAddress.Empty;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004704 File Offset: 0x00002904
		private static void AddToListWithDuplicateCheck(List<RoutingAddress> destinationList, RoutingAddress address)
		{
			if (!destinationList.Contains(address))
			{
				destinationList.Add(address);
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004718 File Offset: 0x00002918
		private static List<AddressInfo> GetJournalArchiveAddresses(EnvelopeJournalReport journalReport, MailItem mailItem, int hashCode)
		{
			List<RoutingAddress> list = new List<RoutingAddress>();
			ITransportMailItemFacade transportMailItem = ((ITransportMailItemWrapperFacade)mailItem).TransportMailItem;
			TransportMailItem transportMailItem2 = transportMailItem as TransportMailItem;
			string arg = (transportMailItem2 == null) ? string.Empty : transportMailItem2.MsgId.ToString();
			ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)hashCode, "UnJournalAgent: GetJournalArchiveAddresses, message id = {0} ", arg);
			foreach (AddressInfo addressInfo in journalReport.Recipients)
			{
				UnwrapJournalAgent.CheckJournalArchiveAddressAndInsert((string)addressInfo.Address, mailItem, list);
			}
			RoutingAddress journalArchiveAddress = UnwrapJournalAgent.GetJournalArchiveAddress(mailItem, (string)journalReport.EnvelopeSender.Address);
			if (journalArchiveAddress != RoutingAddress.Empty && journalArchiveAddress != RoutingAddress.NullReversePath)
			{
				journalReport.SenderJournalArchiveAddress = new AddressInfo(journalArchiveAddress);
			}
			else
			{
				journalReport.SenderJournalArchiveAddress = new AddressInfo(RoutingAddress.NullReversePath);
			}
			return UnwrapJournalAgent.ConvertAndGetAddressInfoList(list);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004818 File Offset: 0x00002A18
		private static void CheckJournalArchiveAddressAndInsert(string recipientAddress, MailItem mailItem, List<RoutingAddress> journalArchiveAddresses)
		{
			RoutingAddress journalArchiveAddress = UnwrapJournalAgent.GetJournalArchiveAddress(mailItem, recipientAddress);
			if (journalArchiveAddress != RoutingAddress.Empty && journalArchiveAddress != RoutingAddress.NullReversePath)
			{
				UnwrapJournalAgent.AddToListWithDuplicateCheck(journalArchiveAddresses, journalArchiveAddress);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004850 File Offset: 0x00002A50
		private static string GetPrimarySmtpAddress(MailItem mailItem, RoutingAddress email)
		{
			ADRecipientCache<TransportMiniRecipient> adrecipientCache = (ADRecipientCache<TransportMiniRecipient>)((ITransportMailItemWrapperFacade)mailItem).TransportMailItem.ADRecipientCacheAsObject;
			if (email.IsValid)
			{
				ProxyAddress proxyAddress = new SmtpProxyAddress(email.ToString(), true);
				Result<TransportMiniRecipient> result = adrecipientCache.FindAndCacheRecipient(proxyAddress);
				if (result.Data != null && result.Error != ProviderError.NotFound)
				{
					string primarySmtpAddress = DirectoryItem.GetPrimarySmtpAddress(result.Data);
					if (!string.IsNullOrEmpty(primarySmtpAddress))
					{
						return primarySmtpAddress;
					}
				}
			}
			return null;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000048C8 File Offset: 0x00002AC8
		private static List<RoutingAddress> FindUnprovisionedRecipients(EnvelopeJournalReport journalReport, MailItem mailItem)
		{
			List<RoutingAddress> list = new List<RoutingAddress>();
			if (journalReport == null || journalReport.ExternalOrUnprovisionedRecipients == null)
			{
				throw new ArgumentNullException("Must specify the list of unprovisioned and external recipients ");
			}
			if (journalReport.ExternalOrUnprovisionedRecipients.Count > 0)
			{
				foreach (RoutingAddress routingAddress in journalReport.ExternalOrUnprovisionedRecipients)
				{
					if (UnwrapJournalAgent.IsUserInOrg(mailItem, routingAddress))
					{
						list.Add(routingAddress);
					}
				}
			}
			if (!journalReport.IsSenderInternal && journalReport.Sender.RecipientType != UnjournalRecipientType.DistributionGroup && UnwrapJournalAgent.IsUserInOrg(mailItem, journalReport.Sender.Address))
			{
				list.Add(journalReport.Sender.Address);
			}
			return list;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000498C File Offset: 0x00002B8C
		private static bool IsUserInOrg(MailItem mailItem, RoutingAddress targetAddress)
		{
			ITransportMailItemWrapperFacade transportMailItemWrapperFacade = (ITransportMailItemWrapperFacade)mailItem;
			ITransportMailItemFacade transportMailItem = transportMailItemWrapperFacade.TransportMailItem;
			OrganizationId orgId = (OrganizationId)transportMailItem.OrganizationIdAsObject;
			TransportMailItem transportMailItem2 = transportMailItem as TransportMailItem;
			string arg = (transportMailItem2 == null) ? string.Empty : transportMailItem2.MsgId.ToString();
			PerTenantAcceptedDomainTable acceptedDomainTable = Components.Configuration.GetAcceptedDomainTable(orgId);
			bool result = false;
			if (targetAddress != RoutingAddress.NullReversePath)
			{
				result = (acceptedDomainTable.AcceptedDomainTable.GetDomainEntry(SmtpDomain.Parse(targetAddress.DomainPart)) != null);
			}
			ExTraceGlobals.JournalingTracer.TraceDebug<string, string, string>(0L, "UnJournalAgent: MessageId: {0}, TargetAddress {1}. IsUserInOrg returns {2}. Target org compared with current org.", arg, targetAddress.ToString(), result.ToString());
			return result;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004A3C File Offset: 0x00002C3C
		private static StringBuilder GetEHAInformation(MailItem mailItem)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(",SND=<");
			stringBuilder.Append(mailItem.FromAddress);
			stringBuilder.Append(">");
			string value = string.Empty;
			if (1 == mailItem.Recipients.Count)
			{
				value = mailItem.Recipients[0].Address.ToString();
			}
			else
			{
				foreach (EnvelopeRecipient envelopeRecipient in mailItem.Recipients)
				{
					MailRecipientWrapper mailRecipientWrapper = (MailRecipientWrapper)envelopeRecipient;
					if (UnwrapJournalAgent.IsUserInOrg(mailItem, mailRecipientWrapper.Address))
					{
						value = mailRecipientWrapper.Address.ToString();
						break;
					}
				}
			}
			stringBuilder.Append(",RCP=<");
			stringBuilder.Append(value);
			stringBuilder.Append(">");
			HeaderList headerList = null;
			if (mailItem != null && mailItem.Message != null && mailItem.Message.RootPart != null)
			{
				headerList = mailItem.Message.RootPart.Headers;
			}
			stringBuilder.Append(",RBS=<");
			stringBuilder.Append(EnvelopeJournalUtility.ReadHeaderValueWithDefault<int>(headerList, "X-MS-EHA-ConfirmBatchSize", 1000, new EnvelopeJournalUtility.Parser<int>(int.TryParse)));
			stringBuilder.Append(">");
			stringBuilder.Append(",RTO=<");
			stringBuilder.Append(EnvelopeJournalUtility.ReadHeaderValueWithDefault<int>(headerList, "X-MS-EHA-ConfirmTimeout", 3600, new EnvelopeJournalUtility.Parser<int>(int.TryParse)));
			stringBuilder.Append(">");
			stringBuilder.Append(",RXD=<");
			stringBuilder.Append(EnvelopeJournalUtility.ReadHeaderValueWithDefault<DateTime>(headerList, "X-MS-EHA-MessageExpiryDate", MessageConstants.EHAJournalHeaderDefaults.DefaultRetainUntilDate, new EnvelopeJournalUtility.Parser<DateTime>(DateTime.TryParse)));
			stringBuilder.Append(">");
			stringBuilder.Append(",RID=<");
			stringBuilder.Append(UnwrapJournalAgent.GetEhaMessageIdHeader(mailItem));
			stringBuilder.Append(">");
			stringBuilder.Append(",ExtOrgId=<");
			stringBuilder.Append(UnwrapJournalAgent.GetExternalOrganizationId(mailItem));
			stringBuilder.Append(">");
			return stringBuilder;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004C68 File Offset: 0x00002E68
		private static string GetExternalOrganizationId(MailItem mailItem)
		{
			IReadOnlyMailItem readOnlyMailItem = (IReadOnlyMailItem)((ITransportMailItemWrapperFacade)mailItem).TransportMailItem;
			return readOnlyMailItem.ExternalOrganizationId.ToString();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004C9C File Offset: 0x00002E9C
		private static string GetEhaMessageIdHeader(MailItem mailItem)
		{
			string text = string.Empty;
			ITransportMailItemFacade transportMailItem = ((ITransportMailItemWrapperFacade)mailItem).TransportMailItem;
			TransportMailItem transportMailItem2 = transportMailItem as TransportMailItem;
			string arg = (transportMailItem2 == null) ? string.Empty : transportMailItem2.MsgId.ToString();
			if (mailItem.Message != null && mailItem.Message.RootPart != null)
			{
				HeaderList headers = mailItem.Message.RootPart.Headers;
				Header header = headers.FindFirst("X-MS-EHAMessageID");
				if (header != null)
				{
					text = ((header.Value == null) ? string.Empty : header.Value);
					ExTraceGlobals.JournalingTracer.TraceDebug<string, string>(0L, "UnJournalAgent: MessageId: {0} This journal message originated from EHA migration. It has EHA message id header of {1} on it.", arg, text);
				}
				else
				{
					ExTraceGlobals.JournalingTracer.TraceDebug<string>(0L, "UnJournalAgent: MessageId: {0} This journal message originated from live journal EHA traffic. EHA message id header not found.", arg);
				}
			}
			return text;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004D58 File Offset: 0x00002F58
		private static void PublishMonitoringResults(MailItem mailItem, string email, string errorMessage)
		{
			ITransportMailItemWrapperFacade transportMailItemWrapperFacade = (ITransportMailItemWrapperFacade)mailItem;
			ITransportMailItemFacade transportMailItem = transportMailItemWrapperFacade.TransportMailItem;
			OrganizationId organizationId = (OrganizationId)transportMailItem.OrganizationIdAsObject;
			string str = string.Empty;
			if (organizationId != null && organizationId.OrganizationalUnit != null)
			{
				str = organizationId.OrganizationalUnit.Name;
			}
			Guid guid = Guid.Empty;
			TransportMailItem transportMailItem2 = transportMailItem as TransportMailItem;
			if (transportMailItem2 != null && !transportMailItem2.IsRowDeleted)
			{
				guid = transportMailItem2.ExternalOrganizationId;
			}
			new EventNotificationItem(ExchangeComponent.JournalArchive.Name, "JournalArchiveComponent", string.Empty, ResultSeverityLevel.Error)
			{
				StateAttribute1 = guid.ToString() + " " + str,
				StateAttribute2 = (string.IsNullOrEmpty(errorMessage) ? string.Empty : errorMessage)
			}.Publish(false);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004E21 File Offset: 0x00003021
		private void SetPrioritization(ITransportMailItemFacade tmi, MailItem mailItem)
		{
			if (this.IsMessageSourceEhaMigration(mailItem))
			{
				UnwrapJournalAgent.SetPrioritization(tmi, true);
				return;
			}
			UnwrapJournalAgent.SetPrioritization(tmi, false);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004E3C File Offset: 0x0000303C
		private void SubmitMessage(SubmittedMessageEventSource source, QueuedMessageEventArgs args)
		{
			if (source == null || args == null)
			{
				throw new ArgumentNullException("internal transport error");
			}
			if (!this.isEnabled)
			{
				return;
			}
			if (!this.isEHAJournalingEnabled)
			{
				ExTraceGlobals.JournalingTracer.TraceDebug((long)this.GetHashCode(), "UnJournalAgent: This is not DC, agent is not enabled.");
				return;
			}
			this.timer.Reset();
			this.timer.Start();
			MailItem mailItem = args.MailItem;
			ITransportMailItemWrapperFacade transportMailItemWrapperFacade = (ITransportMailItemWrapperFacade)mailItem;
			ITransportMailItemFacade transportMailItem = transportMailItemWrapperFacade.TransportMailItem;
			TransportMailItem transportMailItem2 = transportMailItem as TransportMailItem;
			string text = (transportMailItem2 == null) ? string.Empty : transportMailItem2.MsgId.ToString();
			ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} SubmitMessage Invoked.", text);
			using (JournalLogContext journalLogContext = new JournalLogContext("UJA", "OnSubmittedMessage", mailItem.Message.MessageId, mailItem))
			{
				OrganizationId organizationId = (OrganizationId)transportMailItem.OrganizationIdAsObject;
				this.configuration = ArchiveJournalTenantConfiguration.GetTenantConfig(organizationId);
				if (this.configuration == null)
				{
					ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} Unable to retrieve archive journaling configuration.", text);
					journalLogContext.LogAsSkipped("Cfg", new object[0]);
				}
				else
				{
					ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} Retrieved archive journaling configuration.", text);
					ProcessingStatus processingStatus;
					if (this.configuration.LegacyArchiveJournalingEnabled || this.configuration.LegacyArchiveLiveJournalingEnabled || this.configuration.JournalArchivingEnabled)
					{
						ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} archive journaling enabled.", text);
						if (!this.IfJournalAlreadyProcessed(mailItem))
						{
							processingStatus = this.ProcessJournalMessage(source, mailItem, organizationId, journalLogContext);
							ExTraceGlobals.JournalingTracer.TraceDebug<string, string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} Message processed , status = {1}.", text, processingStatus.ToString());
						}
						else
						{
							processingStatus = ProcessingStatus.AlreadyProcessed;
							journalLogContext.LogAsSkipped("APed", new object[0]);
							ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} Already processed message.", text);
						}
					}
					else
					{
						ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} Legacy archive journaling not enabled.", text);
						journalLogContext.LogAsSkipped("LAJDis", new object[0]);
						processingStatus = ProcessingStatus.LegacyArchiveJournallingDisabled;
					}
					ExTraceGlobals.JournalingTracer.TraceDebug<string, string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} Final ProcessingStatus {1}", text, processingStatus.ToString());
					switch (processingStatus)
					{
					case ProcessingStatus.PermanentError:
						this.MarkProcessingDoneForJournalNdr(mailItem);
						this.UpdateProcessingTime();
						break;
					case ProcessingStatus.TransientError:
					case ProcessingStatus.LegacyArchiveJournallingDisabled:
						this.UpdateProcessingTime();
						break;
					case ProcessingStatus.UnwrapProcessSuccess:
					case ProcessingStatus.DropJournalReportWithoutNdr:
					case ProcessingStatus.NoUsersResolved:
						this.DropJournalEnvelop(source, text);
						this.UpdateProcessingTime();
						break;
					case ProcessingStatus.NdrProcessSuccess:
						this.MarkProcessingDoneForJournalNdr(mailItem);
						this.UpdateProcessingTime();
						break;
					case ProcessingStatus.NonJournalMsgFromLegacyArchiveCustomer:
					case ProcessingStatus.AlreadyProcessed:
						this.UpdateProcessingTime();
						break;
					case ProcessingStatus.NdrJournalReport:
						UnwrapJournalAgent.NdrAllRecipients(mailItem, SmtpResponse.InvalidContent);
						this.UpdateProcessingTime();
						break;
					}
					this.UpdatePerformanceCounters(processingStatus);
				}
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00005128 File Offset: 0x00003328
		private void DropJournalEnvelop(SubmittedMessageEventSource source, string messageId)
		{
			ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} Delete the journal message.", messageId);
			source.Delete();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00005148 File Offset: 0x00003348
		private void LogEHAResults(StringBuilder logResponse, ProcessingStatus processingStatus, int unprovisionedCount, int distributionGroupCount, bool permanentError, bool noUsersResolved)
		{
			if (ProcessingStatus.UnwrapProcessSuccess == processingStatus || ProcessingStatus.NdrProcessSuccess == processingStatus || ProcessingStatus.NoUsersResolved == processingStatus || ProcessingStatus.DropJournalReportWithoutNdr == processingStatus || ProcessingStatus.PermanentError == processingStatus)
			{
				logResponse.Append(",status=<");
				logResponse.Append(processingStatus.ToString());
				logResponse.Append(">");
				if (permanentError)
				{
					logResponse.Append(",<permanenterror>");
				}
				else if (noUsersResolved)
				{
					logResponse.Append(",<nousersresolved>");
				}
				else if (unprovisionedCount > 0 && distributionGroupCount > 0)
				{
					logResponse.Append(",<unprovisionedanddistributionlistusers>");
				}
				else if (unprovisionedCount > 0)
				{
					logResponse.Append(",<unprovisionedusers>");
				}
				else if (distributionGroupCount > 0)
				{
					logResponse.Append(",<distributiongroup>");
				}
				lock (UnwrapJournalAgent.syncObject)
				{
					if (UnwrapJournalAgent.logger == null)
					{
						UnwrapJournalAgent.logger = new UnwrapJournalAgent.EHAUnwrapJournalAgentLog();
					}
				}
				UnwrapJournalAgent.logger.Append(logResponse);
				logResponse.Length = 0;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00005248 File Offset: 0x00003448
		private bool IsMessageSourceEhaMigration(MailItem mailItem)
		{
			ITransportMailItemFacade transportMailItem = ((ITransportMailItemWrapperFacade)mailItem).TransportMailItem;
			TransportMailItem transportMailItem2 = transportMailItem as TransportMailItem;
			string arg = (transportMailItem2 == null) ? string.Empty : transportMailItem2.MsgId.ToString();
			if (mailItem.Message == null || mailItem.Message.RootPart == null)
			{
				return false;
			}
			HeaderList headers = mailItem.Message.RootPart.Headers;
			Header header = headers.FindFirst("X-MS-EHA-MessageExpiryDate");
			if (header != null)
			{
				ExTraceGlobals.JournalingTracer.TraceDebug<string, string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} This journal message originated from EHA migration. It has EHA header {1} on it.", arg, "X-MS-EHA-MessageExpiryDate".ToString());
				return true;
			}
			ExTraceGlobals.JournalingTracer.TraceDebug<string, string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} This journal message originated from live journal EHA traffic. EHA header {1} not found.", arg, "X-MS-EHA-MessageExpiryDate".ToString());
			return false;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00005300 File Offset: 0x00003500
		private void StampReceiveAndExpiryTimes(TransportMailItem mailItem, string receiveTime, string expiryTime)
		{
			if (!string.IsNullOrEmpty(receiveTime))
			{
				this.AddHeaderToMessage(mailItem, "X-MS-Exchange-Organization-Unjournal-OriginalReceiveDate", receiveTime);
			}
			if (!string.IsNullOrEmpty(expiryTime))
			{
				this.AddHeaderToMessage(mailItem, "X-MS-Exchange-Organization-Unjournal-OriginalExpiryDate", expiryTime);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000532C File Offset: 0x0000352C
		private string GetExpiryTime(TransportMailItem mailItem)
		{
			string arg = (mailItem == null) ? string.Empty : mailItem.MsgId.ToString();
			if (mailItem.Message != null && mailItem.Message.RootPart != null)
			{
				HeaderList headers = mailItem.Message.RootPart.Headers;
				Header header = headers.FindFirst("X-MS-EHA-MessageExpiryDate");
				ExDateTime exDateTime;
				if (header != null && !string.IsNullOrEmpty(header.Value) && ExDateTime.TryParse(header.Value, out exDateTime))
				{
					ExTraceGlobals.JournalingTracer.TraceDebug<string, string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} Expiry date found {1}.", arg, exDateTime.ToString());
					return exDateTime.ToString();
				}
			}
			return null;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000053D8 File Offset: 0x000035D8
		private string GetOriginalReceiveTime(TransportMailItem mailItem)
		{
			string arg = (mailItem == null) ? string.Empty : mailItem.MsgId.ToString();
			if (mailItem.Message != null && mailItem.Message.RootPart != null)
			{
				HeaderList headers = mailItem.Message.RootPart.Headers;
				Header header = headers.FindFirst("X-MS-EHA-MessageDate");
				ExDateTime exDateTime;
				if (header != null && !string.IsNullOrEmpty(header.Value) && ExDateTime.TryParse(header.Value, out exDateTime))
				{
					ExTraceGlobals.JournalingTracer.TraceDebug<string, string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} Original Recieved date found {1}.", arg, exDateTime.ToString());
					return exDateTime.ToString();
				}
			}
			return null;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00005484 File Offset: 0x00003684
		private void MarkProcessingDone(TransportMailItem transportItem)
		{
			string arg = (transportItem == null) ? string.Empty : transportItem.MsgId.ToString();
			this.perfCountersWrapper.Increment(PerfCounters.MessagesUnjournaled, 1L);
			this.AddHeaderToMessage(transportItem, "X-MS-Exchange-Organization-Unjournal-Processed", string.Empty);
			ExTraceGlobals.JournalingTracer.TraceDebug<string, string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} MarkProcessingDone with {1}.", arg, "Microsoft.Exchange.MessagingPolicies.UnJournalAgent.ProcessedOnSubmitted");
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000054EC File Offset: 0x000036EC
		private void MarkProcessingDoneForJournalNdr(MailItem mailItem)
		{
			ITransportMailItemFacade transportMailItem = ((ITransportMailItemWrapperFacade)mailItem).TransportMailItem;
			TransportMailItem transportMailItem2 = transportMailItem as TransportMailItem;
			string arg = (transportMailItem2 == null) ? string.Empty : transportMailItem2.MsgId.ToString();
			this.SetPrioritization(transportMailItem, mailItem);
			mailItem.Properties["Microsoft.Exchange.MessagingPolicies.UnJournalAgent.ProcessedOnSubmittedForJournalNdr"] = true;
			this.AddHeaderToMessage(transportMailItem2, "X-MS-Exchange-Organization-Unjournal-ProcessedNdr", string.Empty);
			ExTraceGlobals.JournalingTracer.TraceDebug<string, string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} MarkProcessingDoneForJournalNdr with {1}.", arg, "Microsoft.Exchange.MessagingPolicies.UnJournalAgent.ProcessedOnSubmittedForJournalNdr");
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00005570 File Offset: 0x00003770
		private void UpdateProcessingTime()
		{
			this.timer.Stop();
			long elapsedMilliseconds = this.timer.ElapsedMilliseconds;
			ExTraceGlobals.JournalingTracer.TraceDebug<long>((long)this.GetHashCode(), "UnJournalAgent{0} ms to journal message", elapsedMilliseconds);
			PerfCounters.ProcessingTime.IncrementBy(elapsedMilliseconds);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000055B8 File Offset: 0x000037B8
		private bool IfJournalAlreadyProcessed(MailItem mailItem)
		{
			ITransportMailItemFacade transportMailItem = ((ITransportMailItemWrapperFacade)mailItem).TransportMailItem;
			TransportMailItem transportMailItem2 = transportMailItem as TransportMailItem;
			string arg = (transportMailItem2 == null) ? string.Empty : transportMailItem2.MsgId.ToString();
			if (this.HeaderExists(mailItem, "X-MS-Exchange-Organization-Unjournal-Processed"))
			{
				ExTraceGlobals.JournalingTracer.TraceDebug<string, string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} Message was already processed OnSubmitted, skipping: {1}", arg, mailItem.Message.MessageId);
				return true;
			}
			return false;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00005624 File Offset: 0x00003824
		private bool IfJournalAlreadyProcessedForNDR(MailItem mailItem)
		{
			ITransportMailItemFacade transportMailItem = ((ITransportMailItemWrapperFacade)mailItem).TransportMailItem;
			TransportMailItem transportMailItem2 = transportMailItem as TransportMailItem;
			string arg = (transportMailItem2 == null) ? string.Empty : transportMailItem2.MsgId.ToString();
			if (this.HeaderExists(mailItem, "X-MS-Exchange-Organization-Unjournal-ProcessedNdr"))
			{
				ExTraceGlobals.JournalingTracer.TraceDebug<string, string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} Message was already processed OnSubmitted, skipping: {1}", arg, mailItem.Message.MessageId);
				return true;
			}
			return false;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005690 File Offset: 0x00003890
		private bool HeaderExists(MailItem mailItem, string headerName)
		{
			return mailItem.Message.MimeDocument.RootPart.Headers.FindFirst(headerName) != null;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000056C0 File Offset: 0x000038C0
		private bool IfInternalJournalReport(MailItem mailItem)
		{
			ITransportMailItemWrapperFacade transportMailItemWrapperFacade = (ITransportMailItemWrapperFacade)mailItem;
			ITransportMailItemFacade transportMailItem = transportMailItemWrapperFacade.TransportMailItem;
			TransportMailItem transportMailItem2 = transportMailItem as TransportMailItem;
			if (transportMailItem2 == null)
			{
				return false;
			}
			if (this.HeaderExists(mailItem, "X-MS-InternalJournal"))
			{
				ExTraceGlobals.JournalingTracer.TraceDebug<long, string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} This is an internal journal report generated by the journal agent in DC for an EHA customer, this should not be unjournaled: {1}", transportMailItem2.MsgId, mailItem.Message.MessageId);
				return true;
			}
			return false;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005730 File Offset: 0x00003930
		private ProcessingStatus ProcessJournalMessage(SubmittedMessageEventSource source, MailItem mailItem, OrganizationId orgId, JournalLogContext logContext)
		{
			ProcessingStatus processingStatus = ProcessingStatus.NotDone;
			List<EnvelopeRecipient> recipientList = mailItem.Recipients.ToList<EnvelopeRecipient>();
			RoutingAddress fromAddress = mailItem.FromAddress;
			UnwrapJournalAgent.OriginalMailItemInfo originalMailItem = new UnwrapJournalAgent.OriginalMailItemInfo(recipientList, fromAddress);
			Exception ex = null;
			Exception ex2 = null;
			FailureMessageType failureMessageType = FailureMessageType.Unknown;
			ITransportMailItemFacade transportMailItem = ((ITransportMailItemWrapperFacade)mailItem).TransportMailItem;
			TransportMailItem transportMailItem2 = transportMailItem as TransportMailItem;
			string text = (transportMailItem2 == null) ? string.Empty : transportMailItem2.MsgId.ToString();
			StringBuilder stringBuilder = null;
			int unprovisionedCount = 0;
			int distributionGroupCount = 0;
			try
			{
				EnvelopeJournalVersion envelopeJournalVersion = EnvelopeJournalUtility.CheckEnvelopeJournalVersion(mailItem.Message);
				if (envelopeJournalVersion == EnvelopeJournalVersion.None)
				{
					ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} This is not a journal report.", text);
					processingStatus = ProcessingStatus.NonJournalMsgFromLegacyArchiveCustomer;
					logContext.LogAsSkipped("NJMFLAC", new object[0]);
				}
				else if (this.isEHAJournalingEnabled && this.IfInternalJournalReport(mailItem))
				{
					ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} This is an internal journal report, hence we dont unwrap it.", text);
					processingStatus = ProcessingStatus.AlreadyProcessed;
					logContext.LogAsSkipped("APed", new object[]
					{
						"InJR"
					});
				}
				else if (this.IfJournalAlreadyProcessedForNDR(mailItem))
				{
					ExTraceGlobals.JournalingTracer.TraceDebug<string, string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} This is already processed for {1} , hence we will not process it again.", text, "Microsoft.Exchange.MessagingPolicies.UnJournalAgent.ProcessedOnSubmittedForJournalNdr".ToString());
					processingStatus = ProcessingStatus.AlreadyProcessed;
					logContext.LogAsSkipped("APed", new object[]
					{
						"NDR"
					});
				}
				else
				{
					this.perfCountersWrapper.Increment(PerfCounters.MessagesProcessed, 1L);
					ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} This is journal report which we should unwrap.", text);
					if (this.IsMessageSourceEhaMigration(mailItem))
					{
						stringBuilder = UnwrapJournalAgent.GetEHAInformation(mailItem);
						this.SetEhaMigrationMailbox(mailItem);
					}
					EnvelopeJournalReport envelopeJournalReport = EnvelopeJournalUtility.ExtractEnvelopeJournalMessage(mailItem.Message, envelopeJournalVersion);
					if (envelopeJournalReport.Defective)
					{
						ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} This is a defective journal report.", text);
						this.perfCountersWrapper.Increment(PerfCounters.DefectiveJournals, 1L);
						logContext.AddParameter("Def", new object[]
						{
							envelopeJournalReport.Defective
						});
					}
					ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} Updating recipients of the journal report.", text);
					List<RoutingAddress> list = this.ProcessRecipients(mailItem, envelopeJournalReport);
					this.SetPrioritization(transportMailItem, mailItem);
					if (list.Count > 0)
					{
						this.ProcessEmbeddedMessageInJournal(source, mailItem, envelopeJournalReport, list, logContext, out unprovisionedCount, out distributionGroupCount, out processingStatus);
						string key = "UnRec";
						object[] array = new object[1];
						array[0] = from x in list
						select x.ToString();
						logContext.AddParameter(key, array);
						ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} The journal report was unwrapped successfully.", text);
					}
					else
					{
						this.ProcessCauseOfFailureAndSendJournalReportAsIs(source, mailItem, envelopeJournalReport, logContext, out unprovisionedCount, out distributionGroupCount, out processingStatus);
						ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} There are no mail recipients, the journal report was sent to journal ndr mailbox.", text);
					}
					logContext.AddParameter("PSt", new object[]
					{
						processingStatus
					});
				}
			}
			catch (InvalidEnvelopeJournalMessageException ex3)
			{
				ex2 = ex3;
				failureMessageType = FailureMessageType.UnexpectedJournalMessageFormatMsg;
			}
			catch (ADTransientException ex4)
			{
				ex = ex4;
			}
			catch (TransientException ex5)
			{
				ex = ex5;
			}
			catch (DataValidationException ex6)
			{
				ex2 = ex6;
				failureMessageType = FailureMessageType.PermanentErrorOther;
			}
			catch (ExchangeDataException ex7)
			{
				ex2 = ex7;
				failureMessageType = FailureMessageType.PermanentErrorOther;
			}
			if (ex != null)
			{
				ExTraceGlobals.JournalingTracer.TraceError<string>((long)this.GetHashCode(), "Putting this message into retry, as there was an error during unjournaling: {0}.", ex.ToString());
				UnwrapJournalAgent.OriginalMailItemInfo.ResetSenderRecipientInMailItem(mailItem, originalMailItem);
				source.Defer(UnwrapJournalGlobals.RetryIntervalOnError);
				processingStatus = ProcessingStatus.TransientError;
				UnwrapJournalGlobals.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_UnJournalingTransientError, null, new object[]
				{
					text,
					orgId,
					ex2
				});
				logContext.LogAsRetriableError(new object[]
				{
					processingStatus,
					ex
				});
			}
			if (ex2 != null)
			{
				ExTraceGlobals.JournalingTracer.TraceError<string>((long)this.GetHashCode(), "Journal report could not be unjournaled. Got an exception during processing: {0}", ex2.ToString());
				if (this.configuration.DropJournalsWithPermanentErrors)
				{
					if (this.IsMessageSourceEhaMigration(mailItem))
					{
						processingStatus = ProcessingStatus.DropJournalReportWithoutNdr;
					}
					else
					{
						processingStatus = ProcessingStatus.NdrJournalReport;
					}
				}
				else
				{
					processingStatus = ProcessingStatus.PermanentError;
					this.SendReportAsIsForPermanentError(mailItem, failureMessageType);
				}
				logContext.LogAsFatalError(new object[]
				{
					processingStatus,
					failureMessageType,
					ex2
				});
				UnwrapJournalGlobals.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_UnJournalingPermanentError, null, new object[]
				{
					text,
					orgId,
					ex2
				});
			}
			if (stringBuilder != null)
			{
				this.LogEHAResults(stringBuilder, processingStatus, unprovisionedCount, distributionGroupCount, ex2 != null, processingStatus == ProcessingStatus.NoUsersResolved);
			}
			return processingStatus;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005C10 File Offset: 0x00003E10
		private void SetEhaMigrationMailbox(MailItem mailItem)
		{
			RoutingAddress routingAddress = RoutingAddress.NullReversePath;
			if (1 == mailItem.Recipients.Count)
			{
				routingAddress = mailItem.Recipients[0].Address;
			}
			else
			{
				foreach (EnvelopeRecipient envelopeRecipient in mailItem.Recipients)
				{
					MailRecipientWrapper mailRecipientWrapper = (MailRecipientWrapper)envelopeRecipient;
					if (mailRecipientWrapper.Address.LocalPart.ToLower().Contains("ehamigrationmailbox"))
					{
						routingAddress = mailRecipientWrapper.Address;
						break;
					}
				}
			}
			if (routingAddress.IsValid && routingAddress != RoutingAddress.NullReversePath)
			{
				this.configuration.EhaMigrationMailboxAddress = routingAddress;
				ExTraceGlobals.JournalingTracer.TraceError<string>((long)this.GetHashCode(), "Eha migration mailbox address found : {0}", routingAddress.ToString());
				return;
			}
			this.configuration.EhaMigrationMailboxAddress = this.configuration.MSExchangeRecipient;
			ExTraceGlobals.JournalingTracer.TraceError<string>((long)this.GetHashCode(), "Eha migration mailbox address NOT found, reseting migration mailbox to MSExchangeRecipientaddress : {0}", this.configuration.MSExchangeRecipient.ToString());
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005D40 File Offset: 0x00003F40
		private void SendReportAsIsForPermanentError(MailItem mailItem, FailureMessageType messageType)
		{
			this.SetJournalNdrAddress(mailItem);
			this.PrepareDeliveryMessageForNdrReport(mailItem, messageType, new List<RoutingAddress>(), new List<RoutingAddress>());
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005D84 File Offset: 0x00003F84
		private void ProcessEmbeddedMessageInJournal(SubmittedMessageEventSource source, MailItem mailItem, EnvelopeJournalReport journalReport, List<RoutingAddress> targetRecipients, JournalLogContext logContext, out int unprovisionedCount, out int distributionGroupsCount, out ProcessingStatus processingStatus)
		{
			processingStatus = ProcessingStatus.NotDone;
			unprovisionedCount = 0;
			distributionGroupsCount = 0;
			ITransportMailItemFacade transportMailItem = ((ITransportMailItemWrapperFacade)mailItem).TransportMailItem;
			TransportMailItem transportMailItem2 = transportMailItem as TransportMailItem;
			string text = (transportMailItem2 == null) ? string.Empty : transportMailItem2.MsgId.ToString();
			bool flag = this.IsMessageSourceEhaMigration(mailItem);
			ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} Processing embedded message in the journal report.", text);
			if (flag)
			{
				this.ProcessEhaMessageIDHeader(journalReport, transportMailItem2);
			}
			if (journalReport.Defective)
			{
				if (this.configuration.DropJournalsWithPermanentErrors)
				{
					processingStatus = ProcessingStatus.NdrJournalReport;
					logContext.AddParameter("Def", new object[]
					{
						"DropJR"
					});
				}
				else
				{
					this.SetJournalNdrAddress(mailItem);
					List<RoutingAddress> list = new List<RoutingAddress>();
					list = (from x in journalReport.Recipients
					select x.Address).ToList<RoutingAddress>();
					if (journalReport.Sender != null)
					{
						list.Add(journalReport.Sender.Address);
					}
					ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} Preparing deliver message for defective journal to ndr report.", text);
					this.PrepareDeliveryMessageForNdrReport(mailItem, FailureMessageType.DefectiveJournalWithRecipientsMsg, list, new List<RoutingAddress>());
					ExTraceGlobals.JournalingTracer.TraceDebug<string, int>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} Forking a copy to ndr mailbox. Original Recipients Count = {1}.", text, targetRecipients.Count);
					processingStatus = ProcessingStatus.NdrProcessSuccess;
					logContext.AddParameter("Ndr", new object[]
					{
						FailureMessageType.DefectiveJournalWithRecipientsMsg
					});
				}
			}
			else
			{
				List<RoutingAddress> list2;
				List<RoutingAddress> list3;
				this.GetUnProvisionedUsersAndDLsForDelivery(journalReport, mailItem, out list2, out list3);
				if (list2.Count > 0 || list3.Count > 0)
				{
					ExTraceGlobals.JournalingTracer.TraceDebug<string, int, int>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} Found unprovisioned users count {1} or DLs. Count = {2}.", text, list2.Count, list3.Count);
					unprovisionedCount = list2.Count;
					distributionGroupsCount = list3.Count;
					this.SetJournalNdrAddress(mailItem);
					ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} Preparing deliver message for ndr report.", text);
					this.PrepareDeliveryMessageForNdrReport(mailItem, FailureMessageType.UnProvisionedRecipientsMsg, list2, list3);
					ExTraceGlobals.JournalingTracer.TraceDebug<string, int>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} Forking a copy to ndr mailbox. Original Recipients Count = {1}.", text, targetRecipients.Count);
					processingStatus = ProcessingStatus.NdrProcessSuccess;
					string key = "Ndr";
					object[] array = new object[5];
					array[0] = FailureMessageType.UnProvisionedRecipientsMsg;
					array[1] = unprovisionedCount;
					array[2] = from x in list2
					select x.ToString();
					array[3] = distributionGroupsCount;
					array[4] = from x in list3
					select x.ToString();
					logContext.AddParameter(key, array);
				}
			}
			if (this.CreateEmbeddedMessageAndResubmit(targetRecipients, transportMailItem2, journalReport, flag, text))
			{
				if (processingStatus == ProcessingStatus.NotDone)
				{
					processingStatus = ProcessingStatus.UnwrapProcessSuccess;
				}
				this.TrackAgentInfo(source, targetRecipients);
				return;
			}
			throw new InvalidEnvelopeJournalMessageException(AgentStrings.InvalidEnvelopeJournalMessageAttachment(text));
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000605C File Offset: 0x0000425C
		private void GetUnProvisionedUsersAndDLsForDelivery(EnvelopeJournalReport journalReport, MailItem mailItem, out List<RoutingAddress> unprovisionedUsersList, out List<RoutingAddress> distributionGroups)
		{
			unprovisionedUsersList = new List<RoutingAddress>();
			distributionGroups = new List<RoutingAddress>();
			if (!this.configuration.DropUnprovisionedUsersMessages)
			{
				List<RoutingAddress> list = UnwrapJournalAgent.FindUnprovisionedRecipients(journalReport, mailItem);
				if (list != null && list.Count > 0)
				{
					unprovisionedUsersList.AddRange(list);
				}
			}
			if (this.configuration.RedirectDistributionListMessages)
			{
				if (journalReport.DistributionLists != null && journalReport.DistributionLists.Count > 0)
				{
					distributionGroups.AddRange(journalReport.DistributionLists);
				}
				if (journalReport.Sender.RecipientType == UnjournalRecipientType.DistributionGroup)
				{
					distributionGroups.Add(journalReport.Sender.Address);
				}
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000060F4 File Offset: 0x000042F4
		private bool CreateEmbeddedMessageAndResubmit(List<RoutingAddress> targetRecipients, TransportMailItem journalMailItem, EnvelopeJournalReport journalReport, bool isSourceEha, string messageId)
		{
			bool result = false;
			string ehaExpiryTime = null;
			string ehaReceiveTime = null;
			if (isSourceEha)
			{
				ehaExpiryTime = this.GetExpiryTime(journalMailItem);
				ehaReceiveTime = this.GetOriginalReceiveTime(journalMailItem);
			}
			EmailMessage emailMessage = this.ConvertAttachementToEmailMessage(journalReport.EmbeddedMessageAttachment);
			if (emailMessage != null && emailMessage.MimeDocument != null)
			{
				ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} Extracted embedded message.", messageId);
				emailMessage.Normalize(NormalizeOptions.NormalizeMessageId | NormalizeOptions.MergeAddressHeaders | NormalizeOptions.RemoveDuplicateHeaders, false);
				UnwrapJournalAgent.ApplyHeaderFirewall(emailMessage.MimeDocument.RootPart.Headers);
				RoutingAddress from;
				if (isSourceEha)
				{
					from = this.configuration.EhaMigrationMailboxAddress;
				}
				else
				{
					from = journalMailItem.From;
				}
				TransportMailItem transportMailItem = UnwrapJournalAgent.CreateNewTransportMailItem(journalMailItem, emailMessage, journalMailItem.OrganizationId, "Unwrap Journal Agent", "submit", journalReport.EnvelopeSender.Address);
				this.UpdateTransportMailItemWithCustomPropertiesHeaders(from, targetRecipients, transportMailItem, journalMailItem, journalReport, ehaReceiveTime, ehaExpiryTime, isSourceEha, messageId);
				UnwrapJournalAgent.CommitTrackAndEnqueue(journalMailItem, transportMailItem, "Unwrap Journal Agent");
				ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} Unwrapped mailitem submitted in the queue successfully.", messageId);
				result = true;
				PerfCounters.TotalMessagesUnjournaledSize.IncrementBy(transportMailItem.MimeSize);
				this.perfCountersWrapper.Increment(PerfCounters.UsersUnjournaled, (long)targetRecipients.Count);
			}
			else
			{
				ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} Unwrapped mailitem or embedded email message is null.", messageId);
			}
			return result;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000622C File Offset: 0x0000442C
		private void UpdateTransportMailItemWithCustomPropertiesHeaders(RoutingAddress from, List<RoutingAddress> recipients, TransportMailItem newTransportMailItem, TransportMailItem parentMailItem, EnvelopeJournalReport journalReport, string ehaReceiveTime, string ehaExpiryTime, bool isSourceEha, string messageId)
		{
			newTransportMailItem.From = from;
			UnwrapJournalAgent.AddRecipientsToTransportMailItem(recipients, newTransportMailItem);
			UnwrapJournalAgent.SetPrioritization(newTransportMailItem, isSourceEha);
			this.ApplyUnjournalHeaders(newTransportMailItem, journalReport, isSourceEha, ehaReceiveTime, ehaExpiryTime);
			this.MarkProcessingDone(newTransportMailItem);
			ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} Unwrapped mailitem applied header firewall and applied unjournal headers.", messageId);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00006280 File Offset: 0x00004480
		private void ApplyUnjournalHeaders(TransportMailItem unwrappedMailItem, EnvelopeJournalReport journalReport, bool isSourceEha, string ehaReceiveTime, string ehaExpiryTime)
		{
			this.AddExchangeOrganizationBccHeader(journalReport, unwrappedMailItem);
			this.ProcessSender(journalReport, unwrappedMailItem);
			this.ProcessIsSenderARecipient(journalReport, unwrappedMailItem);
			this.RemoveDispositionNotificationHeaderAndExpiryDates(unwrappedMailItem);
			this.CheckAndAddFromHeaderIfMissing(journalReport, unwrappedMailItem);
			this.AddSuppressAutoResponseHeader(unwrappedMailItem);
			if (isSourceEha)
			{
				this.StampReceiveAndExpiryTimes(unwrappedMailItem, ehaReceiveTime, ehaExpiryTime);
				this.ProcessEhaMessageIDHeader(journalReport, unwrappedMailItem);
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000062D4 File Offset: 0x000044D4
		private void AddSuppressAutoResponseHeader(TransportMailItem mailItem)
		{
			AutoResponseSuppress suppress = AutoResponseSuppress.DR | AutoResponseSuppress.RN | AutoResponseSuppress.NRN | AutoResponseSuppress.OOF | AutoResponseSuppress.AutoReply;
			UnwrapJournalAgent.SetAutoResponseSuppress(mailItem, suppress);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000062EC File Offset: 0x000044EC
		private void ProcessSender(EnvelopeJournalReport journalReport, TransportMailItem mailItem)
		{
			string headerValue;
			if (string.IsNullOrEmpty(journalReport.Sender.PrimarySmtpAddress))
			{
				headerValue = journalReport.Sender.Address.ToString();
			}
			else
			{
				headerValue = journalReport.Sender.PrimarySmtpAddress;
			}
			this.AddHeaderToMessage(mailItem, "X-MS-Exchange-Organization-Unjournal-SenderAddress", headerValue);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00006340 File Offset: 0x00004540
		private void CheckAndAddFromHeaderIfMissing(EnvelopeJournalReport journalReport, TransportMailItem transportItem)
		{
			if (transportItem.MimeDocument.RootPart.Headers.FindFirst(HeaderId.From) == null)
			{
				string email = string.Empty;
				string displayName = string.Empty;
				if (string.IsNullOrEmpty(journalReport.EnvelopeSender.PrimarySmtpAddress))
				{
					email = journalReport.EnvelopeSender.Address.ToString();
				}
				else
				{
					email = journalReport.EnvelopeSender.PrimarySmtpAddress;
				}
				displayName = ((journalReport.EnvelopeSender.FriendlyName == null) ? string.Empty : journalReport.EnvelopeSender.FriendlyName);
				HeaderList headers = transportItem.MimeDocument.RootPart.Headers;
				AddressHeader addressHeader = new AddressHeader(HeaderId.From.ToString());
				MimeRecipient newChild = new MimeRecipient(displayName, email);
				addressHeader.AppendChild(newChild);
				headers.AppendChild(addressHeader);
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00006410 File Offset: 0x00004610
		private void ProcessEhaMessageIDHeader(EnvelopeJournalReport journalReport, TransportMailItem mailItem)
		{
			this.AddHeaderToMessage(mailItem, "X-MS-EHAMessageID", journalReport.MessageId);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00006424 File Offset: 0x00004624
		private void ProcessIsSenderARecipient(EnvelopeJournalReport journalReport, TransportMailItem transportItem)
		{
			string arg = (transportItem == null) ? string.Empty : transportItem.MsgId.ToString();
			string strB;
			if (string.IsNullOrEmpty(journalReport.Sender.PrimarySmtpAddress) || string.Compare(RoutingAddress.NullReversePath.ToString(), journalReport.Sender.PrimarySmtpAddress.ToString(), StringComparison.OrdinalIgnoreCase) == 0)
			{
				strB = journalReport.Sender.Address.ToString();
			}
			else
			{
				strB = journalReport.Sender.PrimarySmtpAddress;
			}
			foreach (AddressInfo addressInfo in journalReport.Recipients)
			{
				if (!string.IsNullOrEmpty(addressInfo.PrimarySmtpAddress) && string.Compare(RoutingAddress.NullReversePath.ToString(), addressInfo.PrimarySmtpAddress.ToString(), StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(addressInfo.PrimarySmtpAddress, strB, StringComparison.OrdinalIgnoreCase) == 0)
				{
					ExTraceGlobals.JournalingTracer.TraceDebug<string, string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} Sender is also a recipient {1}.", arg, addressInfo.ToString());
					this.AddHeaderToMessage(transportItem, "X-MS-Exchange-Organization-Unjournal-SenderIsRecipient", string.Empty);
				}
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00006568 File Offset: 0x00004768
		private void RemoveDispositionNotificationHeaderAndExpiryDates(TransportMailItem transportItem)
		{
			if (transportItem != null)
			{
				transportItem.MsgId.ToString();
			}
			else
			{
				string empty = string.Empty;
			}
			if (transportItem.MimeDocument.RootPart != null && transportItem.Message.MimeDocument.RootPart.Headers != null)
			{
				transportItem.Message.MimeDocument.RootPart.Headers.RemoveAll(HeaderId.DispositionNotificationTo);
				transportItem.Message.MimeDocument.RootPart.Headers.RemoveAll(HeaderId.ReturnReceiptTo);
				transportItem.Message.MimeDocument.RootPart.Headers.RemoveAll(HeaderId.ExpiryDate);
				transportItem.Message.MimeDocument.RootPart.Headers.RemoveAll(HeaderId.Expires);
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00006628 File Offset: 0x00004828
		private void AddHeaderToMessage(TransportMailItem transportItem, string headerName, string headerValue)
		{
			Header header = Header.Create(headerName);
			header.Value = headerValue;
			HeaderList headers = transportItem.MimeDocument.RootPart.Headers;
			headers.AppendChild(header);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000665C File Offset: 0x0000485C
		private void AddExchangeOrganizationBccHeader(EnvelopeJournalReport journalReport, TransportMailItem transportItem)
		{
			string arg = (transportItem == null) ? string.Empty : transportItem.MsgId.ToString();
			AddressHeader addressHeader = new AddressHeader("X-MS-Exchange-Organization-BCC");
			foreach (AddressInfo addressInfo in journalReport.Recipients)
			{
				if (addressInfo.IncludedInBcc)
				{
					ExTraceGlobals.JournalingTracer.TraceDebug<string, string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} BCC recipients found, header will be included {1}.", arg, addressInfo.ToString());
					MimeRecipient newChild = new MimeRecipient(addressInfo.FriendlyName, addressInfo.Address.ToString());
					addressHeader.AppendChild(newChild);
				}
			}
			HeaderList headers = transportItem.MimeDocument.RootPart.Headers;
			headers.AppendChild(addressHeader);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00006738 File Offset: 0x00004938
		private void SetJournalNdrAddress(MailItem mailItem)
		{
			mailItem.Recipients.Clear();
			if (this.IsMessageSourceEhaMigration(mailItem))
			{
				mailItem.Recipients.Add(this.configuration.EhaMigrationMailboxAddress);
				return;
			}
			mailItem.Recipients.Add(this.configuration.JournalReportNdrTo);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000067AC File Offset: 0x000049AC
		private void ProcessCauseOfFailureAndSendJournalReportAsIs(SubmittedMessageEventSource source, MailItem mailItem, EnvelopeJournalReport journalReport, JournalLogContext logContext, out int unprovisionedCount, out int distributionGroupsCount, out ProcessingStatus status)
		{
			ITransportMailItemFacade transportMailItem = ((ITransportMailItemWrapperFacade)mailItem).TransportMailItem;
			TransportMailItem mailItem2 = transportMailItem as TransportMailItem;
			bool flag = this.IsMessageSourceEhaMigration(mailItem);
			unprovisionedCount = 0;
			distributionGroupsCount = 0;
			status = ProcessingStatus.NotDone;
			if (journalReport != null)
			{
				if (journalReport.Defective)
				{
					if (this.configuration.DropJournalsWithPermanentErrors)
					{
						status = ProcessingStatus.NdrJournalReport;
						logContext.AddParameter("Def", new object[]
						{
							"DropJR"
						});
						return;
					}
					this.SetJournalNdrAddress(mailItem);
					List<RoutingAddress> list = new List<RoutingAddress>();
					list = (from x in journalReport.Recipients
					select x.Address).ToList<RoutingAddress>();
					if (journalReport.EnvelopeSender != null)
					{
						list.Add(journalReport.EnvelopeSender.Address);
					}
					this.PrepareDeliveryMessageForNdrReport(mailItem, FailureMessageType.DefectiveJournalNoRecipientsMsg, list, new List<RoutingAddress>());
					status = ProcessingStatus.NdrProcessSuccess;
					logContext.AddParameter("Ndr", new object[]
					{
						FailureMessageType.DefectiveJournalNoRecipientsMsg
					});
					return;
				}
				else
				{
					List<RoutingAddress> list2;
					List<RoutingAddress> list3;
					this.GetUnProvisionedUsersAndDLsForDelivery(journalReport, mailItem, out list2, out list3);
					if (list2.Count > 0 || list3.Count > 0)
					{
						this.SetJournalNdrAddress(mailItem);
						unprovisionedCount = list2.Count;
						distributionGroupsCount = list3.Count;
						FailureMessageType failureType = FailureMessageType.UnProvisionedRecipientsMsg;
						status = ProcessingStatus.NdrProcessSuccess;
						this.PrepareDeliveryMessageForNdrReport(mailItem, failureType, list2, list3);
						if (flag)
						{
							this.ProcessEhaMessageIDHeader(journalReport, mailItem2);
						}
						string key = "Ndr";
						object[] array = new object[5];
						array[0] = FailureMessageType.UnProvisionedRecipientsMsg;
						array[1] = unprovisionedCount;
						array[2] = from x in list2
						select x.ToString();
						array[3] = distributionGroupsCount;
						array[4] = from x in list3
						select x.ToString();
						logContext.AddParameter(key, array);
						return;
					}
					status = ProcessingStatus.NoUsersResolved;
				}
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00006998 File Offset: 0x00004B98
		private List<RoutingAddress> ProcessRecipients(MailItem mailItem, EnvelopeJournalReport journalReport)
		{
			List<RoutingAddress> list = new List<RoutingAddress>();
			List<AddressInfo> recipientAddressesToProcess = new List<AddressInfo>();
			if (this.configuration.JournalArchivingEnabled && !this.IsMessageSourceEhaMigration(mailItem))
			{
				recipientAddressesToProcess = UnwrapJournalAgent.GetJournalArchiveAddresses(journalReport, mailItem, this.GetHashCode());
			}
			else
			{
				recipientAddressesToProcess = journalReport.Recipients;
			}
			return this.CategorizeJournalRecipients(journalReport, mailItem, recipientAddressesToProcess);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000069EC File Offset: 0x00004BEC
		private List<RoutingAddress> CategorizeJournalRecipients(EnvelopeJournalReport journalReport, MailItem mailItem, List<AddressInfo> recipientAddressesToProcess)
		{
			List<RoutingAddress> list = new List<RoutingAddress>();
			List<RoutingAddress> list2 = new List<RoutingAddress>();
			List<RoutingAddress> list3 = new List<RoutingAddress>();
			List<RoutingAddress> list4 = new List<RoutingAddress>();
			foreach (AddressInfo addressInfo in recipientAddressesToProcess)
			{
				UnjournalRecipientType recipientType = UnwrapJournalAgent.GetRecipientType(mailItem, (string)addressInfo.Address);
				addressInfo.RecipientType = recipientType;
				switch (recipientType)
				{
				case UnjournalRecipientType.Mailbox:
					addressInfo.PrimarySmtpAddress = UnwrapJournalAgent.GetPrimarySmtpAddress(mailItem, addressInfo.Address);
					UnwrapJournalAgent.AddToListWithDuplicateCheck(list2, addressInfo.Address);
					break;
				case UnjournalRecipientType.DistributionGroup:
					UnwrapJournalAgent.AddToListWithDuplicateCheck(list4, addressInfo.Address);
					break;
				case UnjournalRecipientType.ResolvedOther:
				case UnjournalRecipientType.External:
					UnwrapJournalAgent.AddToListWithDuplicateCheck(list3, addressInfo.Address);
					break;
				}
			}
			if (!this.configuration.RedirectDistributionListMessages && list4.Count > 0)
			{
				list.AddRange(list4);
			}
			if (list2.Count > 0)
			{
				list.AddRange(list2);
			}
			journalReport.ExternalOrUnprovisionedRecipients = list3;
			journalReport.DistributionLists = list4;
			this.ProcessJournalReportSender(journalReport, mailItem, list);
			this.ProcessJournalReportRecipients(journalReport, mailItem);
			return list;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00006B1C File Offset: 0x00004D1C
		private void ProcessJournalReportRecipients(EnvelopeJournalReport journalReport, MailItem mailItem)
		{
			if (this.configuration.JournalArchivingEnabled && !this.IsMessageSourceEhaMigration(mailItem))
			{
				foreach (AddressInfo addressInfo in journalReport.Recipients)
				{
					RoutingAddress journalArchiveAddress = UnwrapJournalAgent.GetJournalArchiveAddress(mailItem, (string)addressInfo.Address);
					if (journalArchiveAddress != RoutingAddress.Empty && journalArchiveAddress != RoutingAddress.NullReversePath)
					{
						addressInfo.PrimarySmtpAddress = journalArchiveAddress.ToString();
					}
				}
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00006BC0 File Offset: 0x00004DC0
		private void ProcessJournalReportSender(EnvelopeJournalReport journalReport, MailItem mailItem, List<RoutingAddress> targetRecipients)
		{
			string primarySmtpAddress = UnwrapJournalAgent.GetPrimarySmtpAddress(mailItem, journalReport.EnvelopeSender.Address);
			journalReport.EnvelopeSender.PrimarySmtpAddress = primarySmtpAddress;
			UnjournalRecipientType unjournalRecipientType;
			string text;
			if (this.configuration.JournalArchivingEnabled && !this.IsMessageSourceEhaMigration(mailItem))
			{
				if (journalReport.SenderJournalArchiveAddressIsValid)
				{
					unjournalRecipientType = UnwrapJournalAgent.GetRecipientType(mailItem, journalReport.Sender.Address.ToString());
					text = UnwrapJournalAgent.GetPrimarySmtpAddress(mailItem, journalReport.Sender.Address);
				}
				else
				{
					unjournalRecipientType = UnjournalRecipientType.Empty;
					text = null;
				}
			}
			else
			{
				unjournalRecipientType = UnwrapJournalAgent.GetRecipientType(mailItem, journalReport.Sender.Address.ToString());
				text = primarySmtpAddress;
			}
			journalReport.Sender.RecipientType = unjournalRecipientType;
			journalReport.IsSenderInternal = (unjournalRecipientType == UnjournalRecipientType.Mailbox);
			journalReport.Sender.PrimarySmtpAddress = ((text == null) ? string.Empty : text);
			if (journalReport.IsSenderInternal && !mailItem.Recipients.Contains(journalReport.Sender.Address))
			{
				targetRecipients.Add(journalReport.Sender.Address);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00006CC4 File Offset: 0x00004EC4
		private EmailMessage ConvertAttachementToEmailMessage(Attachment messageatt)
		{
			Stream contentReadStream = messageatt.GetContentReadStream();
			return EmailMessage.Create(contentReadStream);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00006CE0 File Offset: 0x00004EE0
		private void UpdateJournalBodyWithNdrMessage(MailItem mailItem, LocalizedString locMessage)
		{
			Body body = mailItem.Message.Body;
			Encoding encoding = Charset.GetEncoding(body.CharsetName);
			TextConverter textConverter;
			switch (mailItem.Message.Body.BodyFormat)
			{
			default:
				textConverter = new TextToText
				{
					InputEncoding = encoding,
					HeaderFooterFormat = HeaderFooterFormat.Text,
					Footer = "\n" + locMessage.ToString()
				};
				break;
			case BodyFormat.Html:
				textConverter = new HtmlToHtml
				{
					InputEncoding = encoding,
					HeaderFooterFormat = HeaderFooterFormat.Html,
					Footer = "<br/>" + locMessage.ToString()
				};
				break;
			}
			using (Stream contentReadStream = body.GetContentReadStream())
			{
				using (Stream contentWriteStream = body.GetContentWriteStream())
				{
					textConverter.Convert(contentReadStream, contentWriteStream);
				}
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00006DEC File Offset: 0x00004FEC
		private void PrepareDeliveryMessageForNdrReport(MailItem mailItem, FailureMessageType failureType, List<RoutingAddress> recipients, List<RoutingAddress> distributionGroups)
		{
			ITransportMailItemFacade transportMailItem = ((ITransportMailItemWrapperFacade)mailItem).TransportMailItem;
			TransportMailItem transportMailItem2 = transportMailItem as TransportMailItem;
			string arg = (transportMailItem2 == null) ? string.Empty : transportMailItem2.MsgId.ToString();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("UserListStart;");
			if (recipients != null && recipients.Count > 0)
			{
				foreach (RoutingAddress routingAddress in recipients)
				{
					stringBuilder.AppendLine(routingAddress.ToString() + "; <Unprovisioned>");
				}
			}
			if (distributionGroups != null && distributionGroups.Count > 0)
			{
				foreach (RoutingAddress routingAddress2 in distributionGroups)
				{
					stringBuilder.AppendLine(routingAddress2.ToString() + ";<DistributionGroup>");
				}
			}
			stringBuilder.AppendLine("UserListEnd;");
			LocalizedString locMessage = LocalizedString.Empty;
			switch (failureType)
			{
			case FailureMessageType.DefectiveJournalNoRecipientsMsg:
				locMessage = AgentStrings.DefectiveJournalNoRecipients(stringBuilder.ToString());
				break;
			case FailureMessageType.DefectiveJournalWithRecipientsMsg:
				locMessage = AgentStrings.DefectiveJournalWithRecipients(stringBuilder.ToString());
				break;
			case FailureMessageType.UnProvisionedRecipientsMsg:
				locMessage = AgentStrings.UnProvisionedRecipientsMsg(stringBuilder.ToString());
				break;
			case FailureMessageType.NoRecipientsResolvedMsg:
				locMessage = AgentStrings.NoRecipientsResolvedMsg;
				break;
			case FailureMessageType.UnexpectedJournalMessageFormatMsg:
				locMessage = AgentStrings.UnexpectedJournalMessageFormatMsg;
				break;
			case FailureMessageType.PermanentErrorOther:
				locMessage = AgentStrings.PermanentErrorOther;
				break;
			}
			if (!locMessage.IsEmpty)
			{
				ExTraceGlobals.JournalingTracer.TraceDebug<string, string>((long)this.GetHashCode(), "UnJournalAgent: MessageId: {0} About to update journal body with ndr message {1}.", arg, locMessage.ToString());
				this.UpdateJournalBodyWithNdrMessage(mailItem, locMessage);
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00006FC0 File Offset: 0x000051C0
		private void TrackAgentInfo(SubmittedMessageEventSource source, List<RoutingAddress> targetRecipients)
		{
			int num = 0;
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			if (targetRecipients != null && targetRecipients.Count > 0)
			{
				this.VerifyAndAddAgentInfoDataItem("destNum", targetRecipients.Count.ToString(), list, ref num);
				foreach (RoutingAddress address in targetRecipients)
				{
					string text = (string)address;
					if (!this.VerifyAndAddAgentInfoDataItem("dest", text.ToString(), list, ref num))
					{
						break;
					}
				}
			}
			if (list.Count > 0)
			{
				source.TrackAgentInfo("UJA", "UM", list);
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00007070 File Offset: 0x00005270
		private bool VerifyAndAddAgentInfoDataItem(string key, string value, List<KeyValuePair<string, string>> data, ref int dataLength)
		{
			if (dataLength < 0 || dataLength >= UnwrapJournalAgent.maxMsgTrkAgenInfoLength)
			{
				return false;
			}
			KeyValuePair<string, string> item = new KeyValuePair<string, string>(key, value);
			int num = item.Key.Length + item.Value.Length;
			if (num <= 0 || num > UnwrapJournalAgent.maxMsgTrkAgenInfoLength - dataLength)
			{
				return false;
			}
			dataLength += num;
			data.Add(item);
			return true;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000070D4 File Offset: 0x000052D4
		private void UpdatePerformanceCounters(ProcessingStatus processingStatus)
		{
			switch (processingStatus)
			{
			case ProcessingStatus.PermanentError:
				this.perfCountersWrapper.Increment(PerfCounters.PermanentError, 1L);
				return;
			case ProcessingStatus.TransientError:
				this.perfCountersWrapper.Increment(PerfCounters.TransientError, 1L);
				return;
			case ProcessingStatus.UnwrapProcessSuccess:
				break;
			case ProcessingStatus.NdrProcessSuccess:
				this.perfCountersWrapper.Increment(PerfCounters.NdrProcessSuccess, 1L);
				return;
			case ProcessingStatus.LegacyArchiveJournallingDisabled:
				this.perfCountersWrapper.Increment(PerfCounters.LegacyArchiveJournallingDisabled, 1L);
				return;
			case ProcessingStatus.NonJournalMsgFromLegacyArchiveCustomer:
				this.perfCountersWrapper.Increment(PerfCounters.NonJournalMsgFromLegacyArchiveCustomer, 1L);
				return;
			case ProcessingStatus.AlreadyProcessed:
				this.perfCountersWrapper.Increment(PerfCounters.AlreadyProcessed, 1L);
				return;
			case ProcessingStatus.DropJournalReportWithoutNdr:
				this.perfCountersWrapper.Increment(PerfCounters.DropJournalReportWithoutNdr, 1L);
				return;
			case ProcessingStatus.NoUsersResolved:
				this.perfCountersWrapper.Increment(PerfCounters.NoUsersResolved, 1L);
				return;
			case ProcessingStatus.NdrJournalReport:
				this.perfCountersWrapper.Increment(PerfCounters.NdrJournalReport, 1L);
				break;
			default:
				return;
			}
		}

		// Token: 0x040000D9 RID: 217
		private static readonly object syncObject = new object();

		// Token: 0x040000DA RID: 218
		private static UnwrapJournalAgent.EHAUnwrapJournalAgentLog logger = null;

		// Token: 0x040000DB RID: 219
		private static AutoResponseSuppressFormatter autoResponseSuppressFormatter = new AutoResponseSuppressFormatter();

		// Token: 0x040000DC RID: 220
		private static int maxMsgTrkAgenInfoLength = Math.Min(512, Components.TransportAppConfig.Logging.MaxMsgTrkAgenInfoSize);

		// Token: 0x040000DD RID: 221
		private readonly bool isEHAJournalingEnabled;

		// Token: 0x040000DE RID: 222
		private readonly bool isEnabled;

		// Token: 0x040000DF RID: 223
		private readonly JournalPerfCountersWrapper perfCountersWrapper;

		// Token: 0x040000E0 RID: 224
		private Stopwatch timer = new Stopwatch();

		// Token: 0x040000E1 RID: 225
		private ArchiveJournalTenantConfiguration configuration;

		// Token: 0x0200001E RID: 30
		internal class OriginalMailItemInfo
		{
			// Token: 0x060000AA RID: 170 RVA: 0x000071F8 File Offset: 0x000053F8
			internal OriginalMailItemInfo(List<EnvelopeRecipient> recipientList, RoutingAddress fromAddress)
			{
				this.senderAddress = RoutingAddress.Parse(fromAddress.ToString());
				this.recipientList = new List<RoutingAddress>();
				foreach (EnvelopeRecipient envelopeRecipient in recipientList)
				{
					RoutingAddress address = envelopeRecipient.Address;
					this.recipientList.Add(envelopeRecipient.Address);
				}
			}

			// Token: 0x060000AB RID: 171 RVA: 0x00007280 File Offset: 0x00005480
			internal static void ResetSenderRecipientInMailItem(MailItem mailItem, UnwrapJournalAgent.OriginalMailItemInfo originalMailItem)
			{
				mailItem.Recipients.Clear();
				foreach (RoutingAddress address in originalMailItem.recipientList)
				{
					mailItem.Recipients.Add(address);
				}
				mailItem.FromAddress = originalMailItem.senderAddress;
			}

			// Token: 0x040000E9 RID: 233
			private RoutingAddress senderAddress;

			// Token: 0x040000EA RID: 234
			private List<RoutingAddress> recipientList;
		}

		// Token: 0x0200001F RID: 31
		internal class EHAUnwrapJournalAgentLog : IDisposable
		{
			// Token: 0x060000AC RID: 172 RVA: 0x000072F0 File Offset: 0x000054F0
			internal EHAUnwrapJournalAgentLog()
			{
				ExTraceGlobals.JournalingTracer.TraceDebug((long)this.GetHashCode(), "static EHAUnwrapJournalAgentLog created");
				string[] array = new string[2];
				for (int i = 0; i < 2; i++)
				{
					array[i] = ((UnwrapJournalAgent.EHAUnwrapJournalAgentLog.Field)i).ToString();
				}
				if (!Directory.Exists(UnwrapJournalAgent.EHAUnwrapJournalAgentLog.LogDirectory))
				{
					Directory.CreateDirectory(UnwrapJournalAgent.EHAUnwrapJournalAgentLog.LogDirectory);
				}
				this.logSchema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "EHA Migration Log", array);
				this.log = new Log("EHAMigrationLog", new LogHeaderFormatter(this.logSchema), "EHAMigrationLogs");
				this.log.Configure(UnwrapJournalAgent.EHAUnwrapJournalAgentLog.LogDirectory, UnwrapJournalAgent.EHAUnwrapJournalAgentLog.MaxAge, 262144000L, 10485760L);
			}

			// Token: 0x060000AD RID: 173 RVA: 0x000073BC File Offset: 0x000055BC
			public void Dispose()
			{
				ExTraceGlobals.JournalingTracer.TraceDebug((long)this.GetHashCode(), "EHAUnwrapJournalAgentLog Dispose() called");
				lock (this)
				{
					if (this.log != null)
					{
						this.log.Close();
						this.log = null;
					}
				}
			}

			// Token: 0x060000AE RID: 174 RVA: 0x00007424 File Offset: 0x00005624
			internal StringBuilder Append(StringBuilder sb)
			{
				LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema);
				logRowFormatter[1] = sb.ToString();
				if (this.log != null)
				{
					this.log.Append(logRowFormatter, 0);
				}
				return sb;
			}

			// Token: 0x040000EB RID: 235
			private const string LogComponentName = "EHAMigrationLogs";

			// Token: 0x040000EC RID: 236
			private const string FileNamePrefix = "EHAMigrationLog";

			// Token: 0x040000ED RID: 237
			private const long MaxFileSize = 10485760L;

			// Token: 0x040000EE RID: 238
			private const long MaxDirSize = 262144000L;

			// Token: 0x040000EF RID: 239
			public static readonly string LogDirectory = Path.Combine(Configuration.TransportServer.InstallPath.PathName, "TransportRoles\\Logs\\EHAConfirmations\\");

			// Token: 0x040000F0 RID: 240
			private static readonly TimeSpan MaxAge = TimeSpan.FromDays(30.0);

			// Token: 0x040000F1 RID: 241
			private Log log;

			// Token: 0x040000F2 RID: 242
			private LogSchema logSchema;

			// Token: 0x02000020 RID: 32
			private enum Field
			{
				// Token: 0x040000F4 RID: 244
				Timestamp,
				// Token: 0x040000F5 RID: 245
				LogString,
				// Token: 0x040000F6 RID: 246
				NumFields
			}
		}
	}
}
