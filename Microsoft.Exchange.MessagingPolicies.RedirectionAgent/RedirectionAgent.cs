using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Data.ThrottlingService.Client;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Logging.MessageTracking;

namespace Microsoft.Exchange.MessagingPolicies.Redirection
{
	// Token: 0x02000003 RID: 3
	internal class RedirectionAgent : RoutingAgent
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020DF File Offset: 0x000002DF
		public RedirectionAgent()
		{
			base.OnRoutedMessage += this.OnRoutedMessageHandler;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020FC File Offset: 0x000002FC
		public void OnRoutedMessageHandler(RoutedMessageEventSource source, QueuedMessageEventArgs args)
		{
			MailItem mailItem = args.MailItem;
			if (RedirectionUtil.WasProcessedByRedirectionAgent(mailItem))
			{
				return;
			}
			string redirectionAddress = RedirectionUtil.GetRedirectionAddress(mailItem);
			if (redirectionAddress != null)
			{
				this.ProcessRedirection(source, mailItem, redirectionAddress);
				return;
			}
			this.ProcessNewMails(source, mailItem);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002138 File Offset: 0x00000338
		private void ProcessRedirection(RoutedMessageEventSource source, MailItem mailItem, string redirectionAddress)
		{
			if (mailItem.Recipients.Count == 1)
			{
				List<RecipientExpansionInfo> list = new List<RecipientExpansionInfo>();
				EnvelopeRecipient envelopeRecipient = mailItem.Recipients[0];
				string originalAddress = envelopeRecipient.Address.ToString();
				RoutingAddress routingAddress = new RoutingAddress(redirectionAddress);
				List<RoutingAddress> list2 = new List<RoutingAddress>();
				list2.Add(routingAddress);
				if (RedirectionUtil.GetDeliverAndForward(envelopeRecipient))
				{
					list2.Add(envelopeRecipient.Address);
				}
				RecipientExpansionInfo item = new RecipientExpansionInfo(envelopeRecipient, list2.ToArray());
				list.Add(item);
				MsgTrackRedirectInfo msgTrackInfo = new MsgTrackRedirectInfo("Redirection Agent", envelopeRecipient.Address, routingAddress, null);
				MessageTrackingLog.TrackRedirect(MessageTrackingSource.AGENT, (IReadOnlyMailItem)((ITransportMailItemWrapperFacade)mailItem).TransportMailItem, msgTrackInfo);
				source.ExpandRecipients(list);
				this.PatchHeaders(mailItem, originalAddress, redirectionAddress);
				source.Defer(TimeSpan.Zero, SmtpResponse.RecipientAddressExpandedByRedirectionAgent);
				return;
			}
			throw new InvalidOperationException(string.Format("There should be only 1 recipient left to remove, but there are {0} recipients. They are: {1}", mailItem.Recipients.Count, mailItem.Recipients));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002240 File Offset: 0x00000440
		private void ProcessNewMails(RoutedMessageEventSource source, MailItem mailItem)
		{
			Dictionary<EnvelopeRecipient, string> dictionary = new Dictionary<EnvelopeRecipient, string>();
			IADRecipientCache recipientCache = (IADRecipientCache)((ITransportMailItemWrapperFacade)mailItem).TransportMailItem.ADRecipientCacheAsObject;
			HeaderList headers = mailItem.Message.MimeDocument.RootPart.Headers;
			foreach (EnvelopeRecipient envelopeRecipient in mailItem.Recipients)
			{
				string value = null;
				if (this.IsToRedirect(mailItem, headers, recipientCache, envelopeRecipient, out value))
				{
					dictionary.Add(envelopeRecipient, value);
				}
			}
			if (dictionary.Count > 0)
			{
				this.SpawnNewMails(source, mailItem, dictionary);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022EC File Offset: 0x000004EC
		private bool IsToRedirect(MailItem mailItem, HeaderList headerList, IADRecipientCache recipientCache, EnvelopeRecipient recipient, out string redirectionAddress)
		{
			ProxyAddress forwardingSmtpAddress = RedirectionUtil.GetForwardingSmtpAddress(recipient);
			redirectionAddress = null;
			if (forwardingSmtpAddress != null)
			{
				string text = recipient.Address.ToString();
				if (forwardingSmtpAddress is InvalidProxyAddress)
				{
					RedirectionUtil.LogErrorWithMessageId(MessagingPoliciesEventLogConstants.Tuple_InvalidForwardingSmtpAddressError, mailItem.InternalMessageId, new object[]
					{
						text,
						forwardingSmtpAddress
					});
				}
				else if (!this.DetectLoop(headerList, text))
				{
					ADRawEntry entry;
					string text2;
					if (RedirectionUtil.TryResolveForwardingSmtpAddress(recipientCache, forwardingSmtpAddress, out entry))
					{
						text2 = (RedirectionUtil.GetPrimarySmtpAddress(entry) ?? forwardingSmtpAddress.AddressString);
					}
					else
					{
						text2 = forwardingSmtpAddress.AddressString;
					}
					RoutingAddress address = new RoutingAddress(text2);
					if (!mailItem.Recipients.Contains(address))
					{
						OrganizationId orgId = (OrganizationId)((ITransportMailItemWrapperFacade)mailItem).TransportMailItem.OrganizationIdAsObject;
						if (this.IsWithinThrottlingLimit(orgId, recipient))
						{
							redirectionAddress = text2;
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000023C8 File Offset: 0x000005C8
		private void SpawnNewMails(RoutedMessageEventSource source, MailItem mailItem, Dictionary<EnvelopeRecipient, string> redirectionDictionary)
		{
			if (redirectionDictionary.Count > 0)
			{
				List<EnvelopeRecipient> list = redirectionDictionary.Keys.ToList<EnvelopeRecipient>();
				RedirectionUtil.MarkProcessedByRedirectionAgent(mailItem);
				source.Fork(list);
				RedirectionUtil.ClearProcessedByRedirectionAgent(mailItem);
				foreach (KeyValuePair<EnvelopeRecipient, string> keyValuePair in redirectionDictionary)
				{
					if (list.Count == 1)
					{
						this.ProcessRedirection(source, mailItem, keyValuePair.Value);
					}
					else if (list.Count > 1)
					{
						list.Remove(keyValuePair.Key);
						RedirectionUtil.SetRedirectionAddress(mailItem, keyValuePair.Value);
						source.Fork(list);
						RedirectionUtil.RemoveRedirectionAddress(mailItem);
					}
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002488 File Offset: 0x00000688
		private void PatchHeaders(MailItem mailItem, string originalAddress, string redirectionAddress)
		{
			HeaderList headers = mailItem.Message.MimeDocument.RootPart.Headers;
			this.AddLoopDetectionHeaders(headers, originalAddress);
			this.HandleResentFrom(mailItem, originalAddress, redirectionAddress);
			this.RemoveRuleHeaders(headers);
			mailItem.Properties.Clear();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000024CE File Offset: 0x000006CE
		private void RemoveRuleHeaders(HeaderList headerList)
		{
			headerList.RemoveAll("X-MS-Exchange-Forest-RulesExecuted");
			headerList.RemoveAll("X-MS-Exchange-Organization-Rules-Execution-History");
			headerList.RemoveAll("X-MS-Exchange-Organization-Processed-By-Journaling");
			headerList.RemoveAll("X-MS-Exchange-Organization-Processed-By-Gcc-Journaling");
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000024FC File Offset: 0x000006FC
		private bool DetectLoop(HeaderList headerList, string smtpAddress)
		{
			Header[] array = headerList.FindAll("X-MS-Exchange-Inbox-Rules-Loop");
			if (array.Length <= 0)
			{
				return false;
			}
			if (array.Length >= 3)
			{
				return true;
			}
			Header[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				Header header = array2[i];
				bool result;
				if (header.Value.Length >= 1000)
				{
					result = true;
				}
				else
				{
					if (!header.Value.Equals(smtpAddress, StringComparison.InvariantCultureIgnoreCase))
					{
						i++;
						continue;
					}
					result = true;
				}
				return result;
			}
			return false;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000256C File Offset: 0x0000076C
		private bool IsWithinThrottlingLimit(OrganizationId orgId, EnvelopeRecipient recipient)
		{
			Guid? mailboxGuid = RedirectionUtil.GetMailboxGuid(recipient);
			Guid? mailboxDatabaseGuid = RedirectionUtil.GetMailboxDatabaseGuid(recipient);
			IThrottlingPolicy throttlingPolicy = RedirectionUtil.GetThrottlingPolicy(recipient, orgId);
			if (mailboxGuid == null || mailboxDatabaseGuid == null || throttlingPolicy == null)
			{
				return true;
			}
			DatabaseLocationInfo databaseLocationInfo;
			ActiveManagerOperationResult activeManagerOperationResult = ActiveManager.TryGetCachedServerForDatabaseBasic(mailboxDatabaseGuid.Value, out databaseLocationInfo);
			if (activeManagerOperationResult.Succeeded)
			{
				Unlimited<uint> totalTokenCount = throttlingPolicy.RecipientRateLimit * 4;
				return MailboxThrottle.Instance.ObtainRuleSubmissionTokens(databaseLocationInfo.ServerFqdn, databaseLocationInfo.ServerVersion, mailboxGuid.Value, 1, totalTokenCount, base.Name);
			}
			return true;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000025FC File Offset: 0x000007FC
		private void AddLoopDetectionHeaders(HeaderList headerList, string smtpAddress)
		{
			Header header = Header.Create("X-MS-Exchange-Inbox-Rules-Loop");
			header.Value = smtpAddress;
			headerList.AppendChild(header);
			if (headerList.FindFirst("X-MS-Exchange-Organization-AutoForwarded") == null)
			{
				header = Header.Create("X-MS-Exchange-Organization-AutoForwarded");
				header.Value = "true";
				headerList.AppendChild(header);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002650 File Offset: 0x00000850
		private void HandleResentFrom(MailItem mailItem, string resentFromAddress, string redirectionAddress)
		{
			OrganizationId orgId = (OrganizationId)((ITransportMailItemWrapperFacade)mailItem).TransportMailItem.OrganizationIdAsObject;
			if (!OneOffItem.IsAuthoritativeOrInternalRelaySmtpAddress(new RoutingAddress(redirectionAddress), orgId))
			{
				HeaderList headers = mailItem.Message.RootPart.Headers;
				RoutingAddress purportedResponsibleAddress = Sender.GetPurportedResponsibleAddress(headers);
				if (purportedResponsibleAddress == Sender.NoPRA || !purportedResponsibleAddress.IsValid || !OneOffItem.IsAuthoritativeOrInternalRelaySmtpAddress(purportedResponsibleAddress, orgId))
				{
					if (mailItem.FromAddress != RoutingAddress.NullReversePath)
					{
						mailItem.FromAddress = new RoutingAddress(resentFromAddress);
					}
					headers.PrependChild(AddressHeader.Parse("Resent-From", resentFromAddress, AddressParserFlags.None));
				}
			}
		}
	}
}
