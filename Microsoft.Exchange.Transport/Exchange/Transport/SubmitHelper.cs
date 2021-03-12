using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200052A RID: 1322
	internal class SubmitHelper
	{
		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x06003DA4 RID: 15780 RVA: 0x00102F19 File Offset: 0x00101119
		public static SlidingTotalCounter AgentMessageCounter
		{
			get
			{
				return SubmitHelper.agentMessageSlidingCounter;
			}
		}

		// Token: 0x06003DA5 RID: 15781 RVA: 0x00102F20 File Offset: 0x00101120
		internal static TransportMailItem CreateTransportMailItem(IReadOnlyMailItem currentItem, EmailMessage message, string serverName, Version version, string agentName, RoutingAddress? returnPath, IEnumerable<RoutingAddress> p1Recipients, OrganizationId organizationId, Guid externalOrgId = default(Guid), bool cloneMimeDoc = false)
		{
			TransportMailItem transportMailItem = SubmitHelper.CreateSideEffectMailItem(currentItem, organizationId, externalOrgId);
			SubmitHelper.FixMailItem(transportMailItem, message, serverName, version, agentName, returnPath, p1Recipients, cloneMimeDoc);
			return transportMailItem;
		}

		// Token: 0x06003DA6 RID: 15782 RVA: 0x00102F4A File Offset: 0x0010114A
		internal static void CreateTransportMailItemAndSubmit(IReadOnlyMailItem currentItem, EmailMessage message, string serverName, Version version, string agentName)
		{
			SubmitHelper.InternalCreateTransportMailItemAndSubmit(currentItem, message, serverName, version, agentName, false);
		}

		// Token: 0x06003DA7 RID: 15783 RVA: 0x00102F58 File Offset: 0x00101158
		internal static void CreateTransportMailItemAndSubmit(IReadOnlyMailItem currentItem, EmailMessage message, string serverName, Version version, string agentName, OrganizationId organizationId, Guid externalOrgId = default(Guid), bool cloneMimeDoc = false)
		{
			SubmitHelper.InternalCreateTransportMailItemAndSubmit(currentItem, message, serverName, version, agentName, false, null, organizationId, externalOrgId, cloneMimeDoc);
		}

		// Token: 0x06003DA8 RID: 15784 RVA: 0x00102F80 File Offset: 0x00101180
		internal static void CreateTransportMailItemAndSubmitWithoutDSNs(IReadOnlyMailItem currentItem, EmailMessage message, string serverName, Version version, string agentName)
		{
			SubmitHelper.InternalCreateTransportMailItemAndSubmit(currentItem, message, serverName, version, agentName, true);
		}

		// Token: 0x06003DA9 RID: 15785 RVA: 0x00102F8E File Offset: 0x0010118E
		internal static void CreateTransportMailItemWithNullReversePathAndSubmitWithoutDSNs(IReadOnlyMailItem currentItem, EmailMessage message, string serverName, Version version, string agentName)
		{
			SubmitHelper.InternalCreateTransportMailItemAndSubmit(currentItem, message, serverName, version, agentName, true, new RoutingAddress?(RoutingAddress.NullReversePath));
		}

		// Token: 0x06003DAA RID: 15786 RVA: 0x00102FA8 File Offset: 0x001011A8
		internal static void CreateNewTransportMailItemAndSubmit(EmailMessage message, string serverName, Version version, string agentName, Guid externalOrgId = default(Guid), RoutingAddress? returnPath = null, IEnumerable<RoutingAddress> p1Recipients = null, bool cloneMimeDoc = false)
		{
			TransportMailItem transportMailItem = SubmitHelper.CreateNewMailItem(externalOrgId);
			SubmitHelper.FixMailItem(transportMailItem, message, serverName, version, agentName, returnPath, p1Recipients, cloneMimeDoc);
			SubmitHelper.InternalSubmitTransportMailItem(null, transportMailItem, serverName, version, agentName, false);
		}

		// Token: 0x06003DAB RID: 15787 RVA: 0x00102FD8 File Offset: 0x001011D8
		internal static void TrackAndEnqueue(IReadOnlyMailItem currentItem, TransportMailItem transportMailItem, string serverName, Version version, string agentName)
		{
			MsgTrackReceiveInfo msgTrackInfo = new MsgTrackReceiveInfo(null, null, null, agentName, "SmtpServer.SubmitMessage", new long?((currentItem != null) ? currentItem.RecordId : 0L), transportMailItem.MessageTrackingSecurityInfo);
			MessageTrackingLog.TrackReceive(MessageTrackingSource.AGENT, transportMailItem, msgTrackInfo);
			if (currentItem == null)
			{
				Components.CategorizerComponent.EnqueueSubmittedMessage(transportMailItem);
				return;
			}
			Components.CategorizerComponent.EnqueueSideEffectMessage(currentItem, transportMailItem, agentName);
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x00103034 File Offset: 0x00101234
		internal static void StripHeaders(TransportMailItem transportMailItem, HeaderId[] headerIds)
		{
			HeaderList headers = transportMailItem.RootPart.Headers;
			for (int i = 0; i < headerIds.Length; i++)
			{
				headers.RemoveAll(headerIds[i]);
			}
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x00103064 File Offset: 0x00101264
		internal static SubmitHelper.SenderPromotionError GetSender(EmailMessage message, out RoutingAddress senderAddress)
		{
			HeaderList headers = message.MimeDocument.RootPart.Headers;
			int num = 0;
			Header[] array = headers.FindAll(HeaderId.Sender);
			if (1 == array.Length)
			{
				using (MimeNode.Enumerator<AddressItem> enumerator = ((AddressHeader)array[0]).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						num++;
						if (1 < num)
						{
							return SubmitHelper.SenderPromotionError.MultipleSenders;
						}
					}
					goto IL_62;
				}
			}
			if (1 < array.Length)
			{
				return SubmitHelper.SenderPromotionError.MultipleSenders;
			}
			IL_62:
			if (num == 0)
			{
				Header[] array2 = headers.FindAll(HeaderId.From);
				if (array2.Length == 0 || array2[0].FirstChild == null)
				{
					return SubmitHelper.SenderPromotionError.NoSender;
				}
				if (array2[0].FirstChild is MimeGroup && array2[0].FirstChild.FirstChild == null)
				{
					return SubmitHelper.SenderPromotionError.NoSender;
				}
				if (1 < array2.Length)
				{
					return SubmitHelper.SenderPromotionError.MultipleFromNoSender;
				}
				if (array2[0].FirstChild.NextSibling != null)
				{
					return SubmitHelper.SenderPromotionError.MultipleFromNoSender;
				}
			}
			Header header = headers.FindFirst(HeaderId.ReturnPath);
			string text = (header != null) ? header.Value : null;
			if (!string.IsNullOrEmpty(text) && text.Length > 4 && text[0] == '<' && text[text.Length - 1] == '>')
			{
				RoutingAddress routingAddress = (RoutingAddress)text.Substring(1, text.Length - 2);
				if (Util.IsValidAddress(routingAddress))
				{
					senderAddress = routingAddress;
					return SubmitHelper.SenderPromotionError.None;
				}
			}
			EmailRecipient sender = message.Sender;
			if (sender == null)
			{
				throw new InvalidOperationException("sender is null");
			}
			senderAddress = (RoutingAddress)sender.SmtpAddress;
			if (Util.IsValidAddress(senderAddress) && senderAddress != RoutingAddress.NullReversePath)
			{
				return SubmitHelper.SenderPromotionError.None;
			}
			senderAddress = RoutingAddress.Empty;
			return SubmitHelper.SenderPromotionError.InvalidAddress;
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x00103210 File Offset: 0x00101410
		internal static void StampOriginalMessageSize(TransportMailItem transportMailItem)
		{
			HeaderList headers = transportMailItem.RootPart.Headers;
			if (headers.FindFirst("X-MS-Exchange-Organization-OriginalSize") == null)
			{
				transportMailItem.RefreshMimeSize();
				headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-OriginalSize", transportMailItem.MimeSize.ToString(NumberFormatInfo.InvariantInfo)));
			}
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x00103263 File Offset: 0x00101463
		internal static void SubmitTransportMailItem(IReadOnlyMailItem currentItem, TransportMailItem transportMailItem, string serverName, Version version, string agentName)
		{
			SubmitHelper.InternalSubmitTransportMailItem(currentItem, transportMailItem, serverName, version, agentName, false);
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x00103274 File Offset: 0x00101474
		internal static void PatchHeaders(TransportMailItem transportMailItem, string serverName, Version version)
		{
			SubmitHelper.StripHeaders(transportMailItem, SubmitHelper.SubmitHeaderTypesToStrip);
			DateHeader dateHeader = new DateHeader("Date", transportMailItem.DateReceived);
			string value = dateHeader.Value;
			ReceivedHeader receivedHeader = new ReceivedHeader("SmtpServer.Submit", null, serverName, null, null, "Microsoft SMTP Server", version.ToString(), null, value);
			string text;
			Util.PatchHeaders(transportMailItem.RootPart.Headers, receivedHeader, transportMailItem.From, transportMailItem.DateReceived, Components.Configuration.LocalServer.TransportServer.Fqdn, Components.Configuration.LocalServer.TransportServer.IsHubTransportServer, out text);
			MultilevelAuth.EnsureSecurityAttributes(transportMailItem, SubmitAuthCategory.Internal, MultilevelAuthMechanism.SecureInternalSubmit, null);
			SubmitHelper.StampOriginalMessageSize(transportMailItem);
		}

		// Token: 0x06003DB1 RID: 15793 RVA: 0x0010331C File Offset: 0x0010151C
		private static void InternalCreateTransportMailItemAndSubmit(IReadOnlyMailItem currentItem, EmailMessage message, string serverName, Version version, string agentName, bool suppressDSNs)
		{
			SubmitHelper.InternalCreateTransportMailItemAndSubmit(currentItem, message, serverName, version, agentName, suppressDSNs, null);
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x00103340 File Offset: 0x00101540
		private static void InternalCreateTransportMailItemAndSubmit(IReadOnlyMailItem currentItem, EmailMessage message, string serverName, Version version, string agentName, bool suppressDSNs, RoutingAddress? returnPath)
		{
			SubmitHelper.InternalCreateTransportMailItemAndSubmit(currentItem, message, serverName, version, agentName, false, returnPath, null, Guid.Empty, false);
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x00103364 File Offset: 0x00101564
		private static void InternalCreateTransportMailItemAndSubmit(IReadOnlyMailItem currentItem, EmailMessage message, string serverName, Version version, string agentName, bool suppressDSNs, RoutingAddress? returnPath, OrganizationId organizationId, Guid externalOrgId, bool cloneMimeDoc = false)
		{
			TransportMailItem transportMailItem = SubmitHelper.CreateTransportMailItem(currentItem, message, serverName, version, agentName, returnPath, null, organizationId, externalOrgId, cloneMimeDoc);
			SubmitHelper.InternalSubmitTransportMailItem(currentItem, transportMailItem, serverName, version, agentName, suppressDSNs);
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x00103394 File Offset: 0x00101594
		private static void InternalSubmitTransportMailItem(IReadOnlyMailItem currentItem, TransportMailItem transportMailItem, string serverName, Version version, string agentName, bool suppressDSNs)
		{
			if (currentItem != null && currentItem.IsShadowed())
			{
				ShadowRedundancyManager shadowRedundancyManager = Components.ShadowRedundancyComponent.ShadowRedundancyManager;
				shadowRedundancyManager.LinkSideEffectMailItem(currentItem, transportMailItem);
			}
			if (suppressDSNs)
			{
				foreach (MailRecipient mailRecipient in transportMailItem.Recipients)
				{
					mailRecipient.DsnRequested = DsnRequestedFlags.Never;
				}
			}
			transportMailItem.CommitLazy();
			SubmitHelper.TrackAndEnqueue(currentItem, transportMailItem, serverName, version, agentName);
			SubmitHelper.AgentMessageCounter.AddValue(1L);
		}

		// Token: 0x06003DB5 RID: 15797 RVA: 0x00103424 File Offset: 0x00101624
		private void UpdateAllCounters()
		{
			SubmitHelperPerfCounters.AgentSubmitted.RawValue = SubmitHelper.agentMessageSlidingCounter.Sum;
		}

		// Token: 0x06003DB6 RID: 15798 RVA: 0x0010343C File Offset: 0x0010163C
		private static void FixMailItem(TransportMailItem transportMailItem, EmailMessage message, string serverName, Version version, string agentName, RoutingAddress? returnPath, IEnumerable<RoutingAddress> p1Recipients, bool cloneMimeDoc)
		{
			transportMailItem.PerfCounterAttribution = "submit";
			if (returnPath != null)
			{
				transportMailItem.From = returnPath.Value;
			}
			else
			{
				RoutingAddress from;
				SubmitHelper.SenderPromotionError sender = SubmitHelper.GetSender(message, out from);
				SubmitHelper.ThrowOnSenderErrorIfNecessary(sender);
				transportMailItem.From = from;
			}
			if (p1Recipients != null)
			{
				if (SubmitHelper.AddRecipients(transportMailItem, p1Recipients) == 0)
				{
					throw new InvalidOperationException("There is no recipient.");
				}
			}
			else
			{
				SubmitHelper.PromoteRecipients(transportMailItem, message);
			}
			message.Normalize(NormalizeOptions.NormalizeMessageId | NormalizeOptions.MergeAddressHeaders | NormalizeOptions.RemoveDuplicateHeaders, false);
			if (cloneMimeDoc)
			{
				transportMailItem.MimeDocument = message.MimeDocument.Clone();
			}
			else
			{
				transportMailItem.MimeDocument = message.MimeDocument;
			}
			SubmitHelper.PatchHeaders(transportMailItem, serverName, version);
			transportMailItem.ReceiveConnectorName = "Agent:" + (agentName ?? "Unknown");
		}

		// Token: 0x06003DB7 RID: 15799 RVA: 0x001034F4 File Offset: 0x001016F4
		private static TransportMailItem CreateSideEffectMailItem(IReadOnlyMailItem currentItem, OrganizationId organizationId, Guid externalOrgId)
		{
			TransportMailItem result;
			if (organizationId != null)
			{
				if (externalOrgId == Guid.Empty)
				{
					ADOperationResult adoperationResult = MultiTenantTransport.TryGetExternalOrgId(organizationId, out externalOrgId);
					if (!adoperationResult.Succeeded)
					{
						externalOrgId = MultiTenantTransport.SafeTenantId;
						organizationId = OrganizationId.ForestWideOrgId;
					}
				}
				result = TransportMailItem.NewSideEffectMailItem(currentItem, organizationId, LatencyComponent.Agent, MailDirectionality.Originating, externalOrgId);
			}
			else
			{
				result = TransportMailItem.NewSideEffectMailItem(currentItem);
			}
			return result;
		}

		// Token: 0x06003DB8 RID: 15800 RVA: 0x0010354C File Offset: 0x0010174C
		private static TransportMailItem CreateNewMailItem(Guid externalOrgId)
		{
			return TransportMailItem.NewMailItem(null, LatencyComponent.Agent, MailDirectionality.Originating, externalOrgId);
		}

		// Token: 0x06003DB9 RID: 15801 RVA: 0x00103558 File Offset: 0x00101758
		private static void ThrowOnSenderErrorIfNecessary(SubmitHelper.SenderPromotionError senderAddressError)
		{
			switch (senderAddressError)
			{
			case SubmitHelper.SenderPromotionError.MultipleSenders:
				throw new InvalidOperationException("More than one sender is specified by Sender.");
			case SubmitHelper.SenderPromotionError.MultipleFromNoSender:
				throw new InvalidOperationException("There are multiple addresses specified by From header, but no address is specified by Sender header.");
			case SubmitHelper.SenderPromotionError.NoSender:
				throw new InvalidOperationException("There is no sender.");
			case SubmitHelper.SenderPromotionError.InvalidAddress:
				throw new InvalidOperationException("The sender address is not a valid smtp address");
			default:
				return;
			}
		}

		// Token: 0x06003DBA RID: 15802 RVA: 0x001035D4 File Offset: 0x001017D4
		private static void PromoteRecipients(TransportMailItem transportMailItem, EmailMessage message)
		{
			int num = 0;
			num += SubmitHelper.AddRecipients(transportMailItem, from recipient in message.To
			select new RoutingAddress(recipient.SmtpAddress));
			num += SubmitHelper.AddRecipients(transportMailItem, from recipient in message.Cc
			select new RoutingAddress(recipient.SmtpAddress));
			if (num + SubmitHelper.AddRecipients(transportMailItem, from recipient in message.Bcc
			select new RoutingAddress(recipient.SmtpAddress)) == 0)
			{
				throw new InvalidOperationException("There is no recipient.");
			}
		}

		// Token: 0x06003DBB RID: 15803 RVA: 0x00103684 File Offset: 0x00101884
		private static int AddRecipients(TransportMailItem transportMailItem, IEnumerable<RoutingAddress> recipients)
		{
			int num = 0;
			foreach (RoutingAddress routingAddress in recipients)
			{
				if (routingAddress == RoutingAddress.Empty || routingAddress == RoutingAddress.NullReversePath || !Util.IsValidAddress(routingAddress))
				{
					throw new InvalidOperationException("An invalid recipient smtp address is found: " + routingAddress.ToString());
				}
				transportMailItem.Recipients.Add(routingAddress.ToString());
				num++;
			}
			return num;
		}

		// Token: 0x04001F7C RID: 8060
		private const string NoSenderExceptionText = "There is no sender.";

		// Token: 0x04001F7D RID: 8061
		private const string MultipleSenderExceptionText = "More than one sender is specified by Sender.";

		// Token: 0x04001F7E RID: 8062
		private const string MultipleFromNoSenderExceptionText = "There are multiple addresses specified by From header, but no address is specified by Sender header.";

		// Token: 0x04001F7F RID: 8063
		private const string InvalidSenderAddressExceptionText = "The sender address is not a valid smtp address";

		// Token: 0x04001F80 RID: 8064
		private const string InvalidRecipietAddressExceptionText = "An invalid recipient smtp address is found: ";

		// Token: 0x04001F81 RID: 8065
		private const string NoRecipientExceptionText = "There is no recipient.";

		// Token: 0x04001F82 RID: 8066
		private const string SmtpServerSubmitText = "SmtpServer.SubmitMessage";

		// Token: 0x04001F83 RID: 8067
		private static readonly HeaderId[] SubmitHeaderTypesToStrip = new HeaderId[]
		{
			HeaderId.Bcc
		};

		// Token: 0x04001F84 RID: 8068
		private static SlidingTotalCounter agentMessageSlidingCounter = new SlidingTotalCounter(TimeSpan.FromHours(1.0), TimeSpan.FromMinutes(10.0));

		// Token: 0x0200052B RID: 1323
		internal enum SenderPromotionError
		{
			// Token: 0x04001F89 RID: 8073
			None,
			// Token: 0x04001F8A RID: 8074
			MultipleSenders,
			// Token: 0x04001F8B RID: 8075
			MultipleFromNoSender,
			// Token: 0x04001F8C RID: 8076
			NoSender,
			// Token: 0x04001F8D RID: 8077
			InvalidAddress
		}
	}
}
