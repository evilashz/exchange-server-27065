using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Transport.Logging.MessageTracking;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000192 RID: 402
	internal sealed class OrarGenerator : IOrarGeneratorComponent, ITransportComponent
	{
		// Token: 0x0600119A RID: 4506 RVA: 0x00047878 File Offset: 0x00045A78
		public static bool TryGetOrarAddress(MailRecipient recipient, out RoutingAddress orarAddress)
		{
			string text;
			if (recipient.ExtendedProperties.TryGetValue<string>("Microsoft.Exchange.Transport.Orar", out text) && !string.IsNullOrEmpty(text))
			{
				orarAddress = new RoutingAddress(text);
				return true;
			}
			orarAddress = RoutingAddress.Empty;
			return false;
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x000478BC File Offset: 0x00045ABC
		public static bool TryGetOrarBlob(MailRecipient recipient, out byte[] orar)
		{
			orar = null;
			RoutingAddress routingAddress;
			if (!OrarGenerator.TryGetOrarAddress(recipient, out routingAddress))
			{
				return false;
			}
			ProxyAddress proxyAddress;
			string emailAddress;
			string routingType;
			if (SmtpProxyAddress.TryDeencapsulate(routingAddress.ToString(), out proxyAddress) && proxyAddress.Prefix != ProxyAddressPrefix.LegacyDN)
			{
				emailAddress = proxyAddress.AddressString;
				routingType = proxyAddress.Prefix.ToString();
			}
			else
			{
				emailAddress = routingAddress.ToString();
				routingType = "SMTP";
			}
			Participant participant = new Participant.Builder
			{
				RoutingType = routingType,
				EmailAddress = emailAddress
			}.ToParticipant();
			ParticipantEntryId participantEntryId = ParticipantEntryId.FromParticipant(participant, ParticipantEntryIdConsumer.SupportsNone);
			orar = participantEntryId.ToByteArray();
			return orar != null && orar.Length > 0;
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00047969 File Offset: 0x00045B69
		public static void SetOrarAddress(MailRecipient recipient, RoutingAddress orarAddress)
		{
			if (orarAddress == RoutingAddress.Empty)
			{
				recipient.ExtendedProperties.Remove("Microsoft.Exchange.Transport.Orar");
				return;
			}
			recipient.ExtendedProperties.SetValue<string>("Microsoft.Exchange.Transport.Orar", orarAddress.ToString());
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x000479A8 File Offset: 0x00045BA8
		public static void SetOrarBlob(MailRecipient recipient, byte[] orar)
		{
			RoutingAddress orarAddress;
			if (OrarGenerator.TryParseOrar(orar, recipient.Email.ToString(), out orarAddress))
			{
				OrarGenerator.SetOrarAddress(recipient, orarAddress);
			}
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x000479DC File Offset: 0x00045BDC
		public static bool ContainsOrar(MailRecipient recipient)
		{
			string value;
			return recipient.ExtendedProperties.TryGetValue<string>("Microsoft.Exchange.Transport.Orar", out value) && !string.IsNullOrEmpty(value);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00047A08 File Offset: 0x00045C08
		public static void CopyOrar(MailRecipient from, MailRecipient to)
		{
			RoutingAddress orarAddress;
			if (OrarGenerator.TryGetOrarAddress(from, out orarAddress))
			{
				OrarGenerator.SetOrarAddress(to, orarAddress);
			}
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00047A28 File Offset: 0x00045C28
		public static bool TryParseOrar(byte[] orar, string diagRecipientAddress, out RoutingAddress orarAddress)
		{
			string text;
			string text2;
			string text3;
			return OrarGenerator.TryParseOrar(orar, diagRecipientAddress, out orarAddress, out text, out text2, out text3);
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00047A44 File Offset: 0x00045C44
		public static bool TryParseOrar(byte[] orar, string diagRecipientAddress, out RoutingAddress orarAddress, out string addressType, out string addressValue, out string displayName)
		{
			orarAddress = RoutingAddress.Empty;
			displayName = string.Empty;
			Participant.Builder builder = new Participant.Builder();
			builder.SetPropertiesFrom(ParticipantEntryId.TryFromEntryId(orar));
			addressValue = OrarGenerator.StripSingleQuotes(builder.EmailAddress);
			addressType = builder.RoutingType;
			if (string.IsNullOrEmpty(addressValue) || string.IsNullOrEmpty(addressType))
			{
				OrarGenerator.diag.TraceDebug<string>(0L, "No valid orar email address or type for recipient {0}, no ORAR for it", diagRecipientAddress);
				OrarGenerator.eventLogger.LogEvent(TransportEventLogConstants.Tuple_FailParseOrarBlob, null, new object[]
				{
					diagRecipientAddress
				});
				string empty;
				addressType = (empty = string.Empty);
				addressValue = empty;
				return false;
			}
			if (!OrarGenerator.TryGetRoutingAddress(addressType, addressValue, out orarAddress))
			{
				OrarGenerator.diag.TraceDebug<string>(0L, "No valid orar routingAddress for recipient {0}, no ORAR for it", diagRecipientAddress);
				OrarGenerator.eventLogger.LogEvent(TransportEventLogConstants.Tuple_FailGetRoutingAddress, null, new object[]
				{
					diagRecipientAddress
				});
				string empty2;
				addressType = (empty2 = string.Empty);
				addressValue = empty2;
				return false;
			}
			displayName = (string.IsNullOrEmpty(builder.DisplayName) ? string.Empty : builder.DisplayName);
			return true;
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00047B48 File Offset: 0x00045D48
		public void Load()
		{
			this.Config();
			this.RegisterConfigurationChangeHandlers();
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00047B56 File Offset: 0x00045D56
		public void Unload()
		{
			this.UnRegisterConfigurationChangeHandlers();
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00047B5E File Offset: 0x00045D5E
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00047B61 File Offset: 0x00045D61
		public void GenerateOrarMessage(IReadOnlyMailItem mailItem)
		{
			this.GenerateOrarMessage(mailItem, false);
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00047B6C File Offset: 0x00045D6C
		public void GenerateOrarMessage(IReadOnlyMailItem mailItem, bool resetTime)
		{
			List<MailRecipient> list = new List<MailRecipient>();
			List<RoutingAddress> list2 = new List<RoutingAddress>();
			foreach (MailRecipient mailRecipient in mailItem.Recipients)
			{
				RoutingAddress item;
				if (this.ShouldRedirectToOrar(mailRecipient, out item))
				{
					list.Add(mailRecipient);
					list2.Add(item);
				}
			}
			if (list.Count != 0)
			{
				OrarGenerator.GenerateOrar(mailItem, list, list2, resetTime);
			}
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00047BF0 File Offset: 0x00045DF0
		private static string StripSingleQuotes(string emailAddress)
		{
			if (emailAddress == null || emailAddress.Length < 3)
			{
				return emailAddress;
			}
			if (emailAddress[0] == '\'' && emailAddress[emailAddress.Length - 1] == '\'')
			{
				return emailAddress.Substring(1, emailAddress.Length - 2);
			}
			return emailAddress;
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00047C30 File Offset: 0x00045E30
		private static bool TryGetRoutingAddress(string type, string address, out RoutingAddress result)
		{
			result = RoutingAddress.Empty;
			OrarGenerator.diag.TraceDebug<string, string>(0L, "Try to get routing address for {0}:{1}.", type, address);
			if (type.Equals("SMTP", StringComparison.OrdinalIgnoreCase))
			{
				result = new RoutingAddress(address);
				if (result.IsValid)
				{
					return true;
				}
				if (Components.IsBridgehead)
				{
					result = new RoutingAddress(address, Components.Configuration.FirstOrgAcceptedDomainTable.DefaultDomainName);
					if (result.IsValid)
					{
						return true;
					}
				}
				result = RoutingAddress.Empty;
				OrarGenerator.diag.TraceError<string>(0L, "ORAR Smtp Address is invalid {0}", address);
				return false;
			}
			else
			{
				if (!Components.IsBridgehead)
				{
					OrarGenerator.diag.TraceError<string, string>(0L, "Cannot encapsulate ORAR address {0}:{1} on Edge", type, address);
					return false;
				}
				SmtpProxyAddress smtpProxyAddress;
				if (SmtpProxyAddress.TryEncapsulate(type, address, Components.Configuration.FirstOrgAcceptedDomainTable.DefaultDomainName, out smtpProxyAddress))
				{
					result = (RoutingAddress)smtpProxyAddress.SmtpAddress;
					return true;
				}
				OrarGenerator.diag.TraceDebug<string, string>(0L, "Couldn't encapsulate address {0}:{1}.", type, address);
				return false;
			}
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00047D28 File Offset: 0x00045F28
		private static void GenerateOrar(IReadOnlyMailItem mailItem, List<MailRecipient> recipientList, List<RoutingAddress> routingAddresses, bool resetTime)
		{
			TransportMailItem transportMailItem = mailItem.NewCloneWithoutRecipients(false);
			List<RoutingAddress> list = new List<RoutingAddress>();
			List<RoutingAddress> list2 = new List<RoutingAddress>();
			List<SmtpResponse> list3 = new List<SmtpResponse>();
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < recipientList.Count; i++)
			{
				MailRecipient mailRecipient = transportMailItem.Recipients.Add(routingAddresses[i].ToString());
				OrarGenerator.CopyRecipientProperties(recipientList[i], mailRecipient);
				if (!string.IsNullOrEmpty(recipientList[i].ORcpt))
				{
					mailRecipient.ORcpt = recipientList[i].ORcpt;
				}
				RedirectionHistory.SetRedirectionHistoryOnRecipient(mailRecipient, recipientList[i].Email.ToString());
				recipientList[i].DsnNeeded &= ~DsnFlags.Failure;
				list.Add(recipientList[i].Email);
				list2.Add(routingAddresses[i]);
				list3.Add(recipientList[i].SmtpResponse);
				stringBuilder.Append(routingAddresses[i].ToString());
				stringBuilder.Append(' ');
			}
			if (resetTime)
			{
				OrarGenerator.diag.TraceDebug<string>(0L, "Resetting time on ORAR message {0}", transportMailItem.InternetMessageId);
				transportMailItem.DateReceived = DateTime.UtcNow;
			}
			transportMailItem.RootPart.Headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-Classification", "3f4cc40b-2a9f-4be5-8a55-0e3fdacddd43"));
			MultilevelAuth.EnsureSecurityAttributes(transportMailItem, SubmitAuthCategory.Internal, MultilevelAuthMechanism.SecureInternalSubmit, null);
			transportMailItem.CommitLazy();
			for (int j = 0; j < list.Count; j++)
			{
				MsgTrackRedirectInfo msgTrackInfo = new MsgTrackRedirectInfo(list[j], list2[j], null, new SmtpResponse?(list3[j]));
				MessageTrackingLog.TrackRedirect(MessageTrackingSource.ORAR, transportMailItem, msgTrackInfo);
			}
			transportMailItem.PerfCounterAttribution = "Orar";
			Components.CategorizerComponent.EnqueueSubmittedMessage(transportMailItem);
			OrarGenerator.eventLogger.LogEvent(TransportEventLogConstants.Tuple_ORARMessageSubmitted, null, new object[]
			{
				stringBuilder.ToString()
			});
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00047F44 File Offset: 0x00046144
		private static void CopyRecipientProperties(MailRecipient original, MailRecipient alternate)
		{
			alternate.DsnRequested = original.DsnRequested;
			int value;
			if (original.ExtendedProperties.TryGetValue<int>("Microsoft.Exchange.Transport.RecipientP2Type", out value))
			{
				alternate.ExtendedProperties.SetValue<int>("Microsoft.Exchange.Transport.RecipientP2Type", value);
			}
			int value2;
			if (original.ExtendedProperties.TryGetValue<int>("Microsoft.Exchange.Transport.ClientRequestedInternetEncoding", out value2))
			{
				alternate.ExtendedProperties.SetValue<int>("Microsoft.Exchange.Transport.ClientRequestedInternetEncoding", value2);
			}
			bool value3;
			if (original.ExtendedProperties.TryGetValue<bool>("Microsoft.Exchange.Transport.ClientRequestedSendRichInfo", out value3))
			{
				alternate.ExtendedProperties.SetValue<bool>("Microsoft.Exchange.Transport.ClientRequestedSendRichInfo", value3);
			}
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x00047FCC File Offset: 0x000461CC
		private void Config()
		{
			OrarGenerator.diag.TraceDebug(0L, "OrarGenerator Config");
			QuarantineConfig quarantineConfig = new QuarantineConfig();
			if (quarantineConfig.Load())
			{
				this.quarantineConfig = quarantineConfig;
			}
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00048000 File Offset: 0x00046200
		private bool QuarantineCheck(MailRecipient recipient)
		{
			if ((recipient.DsnNeeded & DsnFlags.Quarantine) != DsnFlags.None && (this.quarantineConfig == null || string.IsNullOrEmpty(this.quarantineConfig.Mailbox)))
			{
				OrarGenerator.diag.TraceDebug<RoutingAddress>(0L, "Quarantine DSN needed for recipient {0}, but there is no quarantine configed", recipient.Email);
				if (recipient.AckStatus == AckStatus.Quarantine)
				{
					recipient.Ack(AckStatus.Fail, AckReason.QuarantineDisabled);
				}
				recipient.DsnNeeded &= ~DsnFlags.Quarantine;
				return true;
			}
			return (recipient.DsnNeeded & DsnFlags.Failure) != DsnFlags.None;
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x0004807C File Offset: 0x0004627C
		private bool ShouldRedirectToOrar(MailRecipient recipient, out RoutingAddress orarAddress)
		{
			orarAddress = RoutingAddress.Empty;
			if (!recipient.IsActive)
			{
				OrarGenerator.diag.TraceDebug<RoutingAddress>(0L, "recipient {0} is mark as deleted, no ORAR for it", recipient.Email);
				return false;
			}
			DsnFlags dsnFlags = DsnFlags.Failure | DsnFlags.Quarantine;
			if ((recipient.DsnNeeded & dsnFlags) == DsnFlags.None)
			{
				OrarGenerator.diag.TraceDebug<RoutingAddress>(0L, "No need generate DSN for recipient {0}, no ORAR for it", recipient.Email);
				return false;
			}
			if (!OrarGenerator.TryGetOrarAddress(recipient, out orarAddress))
			{
				OrarGenerator.diag.TraceDebug<RoutingAddress>(0L, "No orar blob exist for recipient {0} , no ORAR for it", recipient.Email);
				return false;
			}
			return this.QuarantineCheck(recipient);
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00048124 File Offset: 0x00046324
		private void RegisterConfigurationChangeHandlers()
		{
			OrarGenerator.diag.TraceDebug((long)this.GetHashCode(), "OrarGenerator RegisterconfigurationChangeHandlers");
			ADObjectId quarantineConfigObjectId = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				quarantineConfigObjectId = QuarantineConfig.GetConfigObjectId();
			});
			if (!adoperationResult.Succeeded)
			{
				throw new TransportComponentLoadFailedException(Strings.ReadOrgContainerFailed, adoperationResult.Exception);
			}
			try
			{
				this.quarantineConfigNotificationCookie = ADNotificationAdapter.RegisterChangeNotification<ContentFilterConfig>(quarantineConfigObjectId, delegate(ADNotificationEventArgs param0)
				{
					this.Config();
				});
			}
			catch (ADTransientException arg)
			{
				OrarGenerator.diag.TraceError<ADTransientException>((long)this.GetHashCode(), "OrarGenerator failed to register for AD notification due to {0}", arg);
			}
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x000481DC File Offset: 0x000463DC
		private void UnRegisterConfigurationChangeHandlers()
		{
			if (this.quarantineConfigNotificationCookie != null)
			{
				OrarGenerator.diag.TraceDebug((long)this.GetHashCode(), "OrarGenerator UnRegisterConfigurationChangeHandlers");
				try
				{
					ADNotificationAdapter.UnregisterChangeNotification(this.quarantineConfigNotificationCookie);
				}
				catch (ADTransientException arg)
				{
					OrarGenerator.diag.TraceError<ADTransientException>((long)this.GetHashCode(), "OrarGenerator failed to un-register for AD notification due to {0}", arg);
				}
			}
		}

		// Token: 0x0400095B RID: 2395
		private const string OrginatorRequestedAlternateRecipient = "Microsoft.Exchange.Transport.Orar";

		// Token: 0x0400095C RID: 2396
		private static readonly Trace diag = ExTraceGlobals.OrarTracer;

		// Token: 0x0400095D RID: 2397
		private static ExEventLog eventLogger = new ExEventLog(OrarGenerator.diag.Category, TransportEventLog.GetEventSource());

		// Token: 0x0400095E RID: 2398
		private QuarantineConfig quarantineConfig;

		// Token: 0x0400095F RID: 2399
		private ADNotificationRequestCookie quarantineConfigNotificationCookie;
	}
}
