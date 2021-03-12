using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage.VersionedXml;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Delivery;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MobileTransport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.TextMessaging.MobileDriver.Resources;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Delivery;
using Microsoft.Exchange.Transport.Logging.MessageTracking;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x0200004F RID: 79
	internal class TransportAgentWrapper
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x0000A864 File Offset: 0x00008A64
		public TransportAgentWrapper(AgentAsyncContext asyncContext, SubmittedMessageEventSource src, QueuedMessageEventArgs e) : this(asyncContext)
		{
			if (src == null)
			{
				throw new ArgumentNullException("src");
			}
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			this.SubmittedMessageEventSource = src;
			this.QueuedMessageEventArgs = e;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000A897 File Offset: 0x00008A97
		public TransportAgentWrapper(AgentAsyncContext asyncContext, DeliverMailItemEventSource src, DeliverMailItemEventArgs e) : this(asyncContext)
		{
			if (src == null)
			{
				throw new ArgumentNullException("src");
			}
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			this.DeliverMailItemEventSource = src;
			this.DeliverMailItemEventArgs = e;
			this.IsInDeliveryAgent = true;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000A8D1 File Offset: 0x00008AD1
		private TransportAgentWrapper(AgentAsyncContext asyncContext)
		{
			ExSmsCounters.PendingDelivery.Increment();
			this.AgentAsyncContext = asyncContext;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000A8EB File Offset: 0x00008AEB
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0000A8F3 File Offset: 0x00008AF3
		public bool IsInDeliveryAgent { get; private set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000A8FC File Offset: 0x00008AFC
		// (set) Token: 0x060001EF RID: 495 RVA: 0x0000A904 File Offset: 0x00008B04
		public bool PartnerDelivery { get; internal set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000A90D File Offset: 0x00008B0D
		public ReadOnlyEnvelopeRecipientCollection ReadOnlyRecipients
		{
			get
			{
				if (this.readOnlyRecipients == null)
				{
					this.readOnlyRecipients = (this.IsInDeliveryAgent ? this.DeliverMailItemEventArgs.DeliverableMailItem.Recipients : this.QueuedMessageEventArgs.MailItem.Recipients);
				}
				return this.readOnlyRecipients;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000A94D File Offset: 0x00008B4D
		public EmailMessage EmailMessage
		{
			get
			{
				if (this.emailMessage == null)
				{
					this.emailMessage = (this.IsInDeliveryAgent ? this.DeliverMailItemEventArgs.DeliverableMailItem.Message : this.QueuedMessageEventArgs.MailItem.Message);
				}
				return this.emailMessage;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000A98D File Offset: 0x00008B8D
		public DsnParameters MailItemDsnParametersCopy
		{
			get
			{
				if (this.mailItemDsnParametersCopy == null)
				{
					this.mailItemDsnParametersCopy = new DsnParameters();
				}
				return this.mailItemDsnParametersCopy;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000A9A8 File Offset: 0x00008BA8
		internal ADSessionSettings ADSessionSettings
		{
			get
			{
				if (this.adSessionSettings == null)
				{
					OrganizationId organizationId = null;
					if (this.IsInDeliveryAgent)
					{
						RoutedMailItemWrapper routedMailItemWrapper = this.DeliverMailItemEventArgs.DeliverableMailItem as RoutedMailItemWrapper;
						if (routedMailItemWrapper != null && routedMailItemWrapper.RoutedMailItem != null)
						{
							organizationId = routedMailItemWrapper.RoutedMailItem.OrganizationId;
						}
					}
					else
					{
						TransportMailItemWrapper transportMailItemWrapper = this.QueuedMessageEventArgs.MailItem as TransportMailItemWrapper;
						if (transportMailItemWrapper != null && transportMailItemWrapper.TransportMailItem != null)
						{
							organizationId = transportMailItemWrapper.TransportMailItem.OrganizationId;
						}
					}
					this.adSessionSettings = ((null == organizationId) ? ADSessionSettings.FromRootOrgScopeSet() : ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId));
				}
				return this.adSessionSettings;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000AA3B File Offset: 0x00008C3B
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x0000AA43 File Offset: 0x00008C43
		private AgentAsyncContext AgentAsyncContext { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000AA4C File Offset: 0x00008C4C
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x0000AA54 File Offset: 0x00008C54
		private DeliverMailItemEventSource DeliverMailItemEventSource { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000AA5D File Offset: 0x00008C5D
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x0000AA65 File Offset: 0x00008C65
		private DeliverMailItemEventArgs DeliverMailItemEventArgs { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000AA6E File Offset: 0x00008C6E
		// (set) Token: 0x060001FB RID: 507 RVA: 0x0000AA76 File Offset: 0x00008C76
		private SubmittedMessageEventSource SubmittedMessageEventSource { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000AA7F File Offset: 0x00008C7F
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0000AA87 File Offset: 0x00008C87
		private QueuedMessageEventArgs QueuedMessageEventArgs { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000AA90 File Offset: 0x00008C90
		private TransportMailItem TransportMailItem
		{
			get
			{
				if (!this.IsInDeliveryAgent && this.transportMailItem == null)
				{
					this.transportMailItem = ((TransportMailItemWrapper)this.QueuedMessageEventArgs.MailItem).TransportMailItem;
				}
				return this.transportMailItem;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000AAC4 File Offset: 0x00008CC4
		private MimePart RootPart
		{
			get
			{
				if (this.rootPart == null)
				{
					if (this.IsInDeliveryAgent)
					{
						this.rootPart = this.DeliverMailItemEventArgs.DeliverableMailItem.Message.RootPart;
					}
					else if (this.QueuedMessageEventArgs.MailItem.MimeDocument == null)
					{
						this.rootPart = this.QueuedMessageEventArgs.MailItem.Message.RootPart;
					}
					else
					{
						try
						{
							this.rootPart = this.QueuedMessageEventArgs.MailItem.MimeDocument.RootPart;
						}
						catch (ObjectDisposedException arg)
						{
							this.rootPart = this.QueuedMessageEventArgs.MailItem.Message.RootPart;
							ExTraceGlobals.XsoTracer.TraceError<ObjectDisposedException>((long)this.GetHashCode(), "MimeDocument has been disposed. Raises Exception: {0}", arg);
						}
					}
				}
				return this.rootPart;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000AB9C File Offset: 0x00008D9C
		private EnvelopeRecipientCollection Recipients
		{
			get
			{
				return this.ReadOnlyRecipients as EnvelopeRecipientCollection;
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000ABAC File Offset: 0x00008DAC
		public static DsnParameters CloneDsnParameters(DsnParameters src)
		{
			if (src == null)
			{
				return null;
			}
			DsnParameters dsnParameters = new DsnParameters();
			foreach (KeyValuePair<string, object> keyValuePair in src)
			{
				dsnParameters[keyValuePair.Key] = keyValuePair.Value;
			}
			return dsnParameters;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000AC14 File Offset: 0x00008E14
		public static void AddDsnParameters(MailRecipient recipient, DsnParameters dsnParam)
		{
			if (dsnParam == null)
			{
				return;
			}
			foreach (KeyValuePair<string, object> keyValuePair in dsnParam)
			{
				recipient.AddDsnParameters(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000AC74 File Offset: 0x00008E74
		public static MailRecipient CastEnvelopeRecipientToMailRecipient(EnvelopeRecipient envelopeRecipient)
		{
			return ((MailRecipientWrapper)envelopeRecipient).MailRecipient;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000AC81 File Offset: 0x00008E81
		public bool TrySetRecipientDsnTypeRequested(EnvelopeRecipient recipient, DsnTypeRequested requested)
		{
			if (this.IsInDeliveryAgent)
			{
				return false;
			}
			recipient.RequestedReports = requested;
			return true;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000AC98 File Offset: 0x00008E98
		public void AckRecipient(EnvelopeRecipient recipient, AckStatus ackStatus, SmtpResponse smtpResponse)
		{
			if (!this.IsInDeliveryAgent)
			{
				TransportAgentWrapper.CastEnvelopeRecipientToMailRecipient(recipient).Ack(ackStatus, smtpResponse);
				return;
			}
			switch (ackStatus)
			{
			case AckStatus.Success:
			case AckStatus.Expand:
			case AckStatus.Relay:
			case AckStatus.SuccessNoDsn:
				this.DeliverMailItemEventSource.AckRecipientSuccess(recipient, smtpResponse);
				return;
			case AckStatus.Retry:
				this.DeliverMailItemEventSource.AckRecipientDefer(recipient, smtpResponse);
				return;
			case AckStatus.Fail:
				this.DeliverMailItemEventSource.AckRecipientFail(recipient, smtpResponse);
				return;
			default:
				this.DeliverMailItemEventSource.AckRecipientFail(recipient, smtpResponse);
				return;
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000AD18 File Offset: 0x00008F18
		public void LogTrackingForTransportMailItem()
		{
			if (this.IsInDeliveryAgent)
			{
				throw new MobileDriverStateException(Strings.ErrorInvalidState("IsInDeliveryAgent", true.ToString()));
			}
			if (!this.PartnerDelivery)
			{
				MessageTrackingLog.TrackRelayedAndFailed(MessageTrackingSource.AGENT, this.TransportMailItem, this.TransportMailItem.Recipients, null);
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000AD68 File Offset: 0x00008F68
		public void RemoveRecipients(IList<EnvelopeRecipient> recipients)
		{
			if (this.IsInDeliveryAgent)
			{
				throw new MobileDriverStateException(Strings.ErrorInvalidState("IsInDeliveryAgent", true.ToString()));
			}
			foreach (EnvelopeRecipient recipient in recipients)
			{
				this.Recipients.Remove(recipient);
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000ADD8 File Offset: 0x00008FD8
		public void AddDsnParameters(string key, object value)
		{
			this.MailItemDsnParametersCopy[key] = value;
			if (this.IsInDeliveryAgent)
			{
				this.DeliverMailItemEventSource.AddDsnParameters(key, value);
				return;
			}
			this.TransportMailItem.AddDsnParameters(key, value);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000AE0A File Offset: 0x0000900A
		public void AddDsnParameters(EnvelopeRecipient recipient, string key, object value)
		{
			if (this.IsInDeliveryAgent)
			{
				this.DeliverMailItemEventSource.AddDsnParameters(recipient, key, value);
				return;
			}
			TransportAgentWrapper.CastEnvelopeRecipientToMailRecipient(recipient).AddDsnParameters(key, value);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000AE30 File Offset: 0x00009030
		public bool DoDsnParametersExist(string key)
		{
			if (this.IsInDeliveryAgent)
			{
				object obj = null;
				return this.DeliverMailItemEventSource.TryGetDsnParameters(key, out obj);
			}
			return this.TransportMailItem.DsnParameters != null && this.TransportMailItem.DsnParameters.ContainsKey(key);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000AE76 File Offset: 0x00009076
		public bool TryGetDsnParameters(string key, out object value)
		{
			value = null;
			if (this.IsInDeliveryAgent)
			{
				return this.DeliverMailItemEventSource.TryGetDsnParameters(key, out value);
			}
			return this.TransportMailItem.DsnParameters != null && this.TransportMailItem.DsnParameters.TryGetValue(key, out value);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000AEB4 File Offset: 0x000090B4
		public bool DoDsnParametersExist(EnvelopeRecipient recipient, string key)
		{
			if (this.IsInDeliveryAgent)
			{
				object obj = null;
				return this.DeliverMailItemEventSource.TryGetDsnParameters(recipient, key, out obj);
			}
			MailRecipient mailRecipient = TransportAgentWrapper.CastEnvelopeRecipientToMailRecipient(recipient);
			return mailRecipient.DsnParameters != null && mailRecipient.DsnParameters.ContainsKey(key);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000AEF8 File Offset: 0x000090F8
		public bool TryGetDsnParameters(EnvelopeRecipient recipient, string key, out object value)
		{
			value = null;
			if (this.IsInDeliveryAgent)
			{
				return this.DeliverMailItemEventSource.TryGetDsnParameters(recipient, key, out value);
			}
			MailRecipient mailRecipient = TransportAgentWrapper.CastEnvelopeRecipientToMailRecipient(recipient);
			return mailRecipient.DsnParameters != null && mailRecipient.DsnParameters.TryGetValue(key, out value);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000AF3D File Offset: 0x0000913D
		public void CompleteAsyncEvent()
		{
			ExSmsCounters.PendingDelivery.Decrement();
			this.AgentAsyncContext.Complete();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000AF55 File Offset: 0x00009155
		public void Resume()
		{
			this.AgentAsyncContext.Resume();
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000AF64 File Offset: 0x00009164
		public void SetCachedTextMessagingSettings(TextMessagingSettingsBase settings)
		{
			if (this.IsInDeliveryAgent)
			{
				throw new MobileDriverStateException(Strings.ErrorInvalidState("IsInDeliveryAgent", true.ToString()));
			}
			StringBuilder stringBuilder = new StringBuilder(settings.ToBase64String());
			int num = 0;
			int num2 = 1024 - "X-MS-Exchange-Organization-Text-Messaging-Settings-Segment-".Length - 1;
			if (0 >= num2)
			{
				throw new MobileDriverStateException(Strings.ErrorInvalidState("EffectiveMaxTextValueBytesPerValueLimits", num2.ToString()));
			}
			int num3 = 0;
			int num4 = Math.Min(num2, stringBuilder.Length - num3);
			while (stringBuilder.Length > num3)
			{
				this.SetTextHeader("X-MS-Exchange-Organization-Text-Messaging-Settings-Segment-" + num++.ToString(), stringBuilder.ToString(num3, num4));
				num3 += num4;
				num4 = Math.Min(num2, stringBuilder.Length - num3);
			}
			if (0 < num)
			{
				this.SetTextHeader("X-MS-Exchange-Organization-Text-Messaging-Count-Of-Settings-Segments", num.ToString());
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000B040 File Offset: 0x00009240
		public TextMessagingSettingsBase GetCachedTextMessagingSettings()
		{
			string s = null;
			if (!this.TryGetTextHeader("X-MS-Exchange-Organization-Text-Messaging-Count-Of-Settings-Segments", out s))
			{
				return null;
			}
			int i = 0;
			if (!int.TryParse(s, out i))
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(i * 1024);
			int num = 0;
			while (i > num)
			{
				string value = null;
				if (!this.TryGetTextHeader("X-MS-Exchange-Organization-Text-Messaging-Settings-Segment-" + num.ToString(), out value))
				{
					return null;
				}
				if (string.IsNullOrEmpty(value))
				{
					return null;
				}
				stringBuilder.Append(value);
				num++;
			}
			TextMessagingSettingsBase result;
			try
			{
				result = (TextMessagingSettingsBase)VersionedXmlBase.ParseFromBase64(stringBuilder.ToString());
			}
			catch (XmlException ex)
			{
				throw new MobileDriverStateException(Strings.ErrorCannotParseSettings(ex.ToString()));
			}
			catch (InvalidOperationException ex2)
			{
				throw new MobileDriverStateException(Strings.ErrorCannotParseSettings(ex2.ToString()));
			}
			return result;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000B118 File Offset: 0x00009318
		public bool TryGetTextHeader(string name, out string text)
		{
			text = null;
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (this.RootPart == null)
			{
				return false;
			}
			Header header = this.RootPart.Headers.FindFirst(name);
			if (header == null)
			{
				return false;
			}
			text = header.Value;
			return true;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000B168 File Offset: 0x00009368
		public void SetTextHeader(string name, string text)
		{
			if (this.IsInDeliveryAgent)
			{
				throw new MobileDriverStateException(Strings.ErrorInvalidState("IsInDeliveryAgent", true.ToString()));
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentNullException("text");
			}
			if (this.RootPart == null)
			{
				throw new MobileDriverStateException(Strings.ErrorInvalidState("RootPart", Strings.ConstNull));
			}
			Header header = this.RootPart.Headers.FindFirst(name);
			if (header == null)
			{
				header = new TextHeader(name, text);
				this.RootPart.Headers.AppendChild(header);
				return;
			}
			header.Value = text;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000B218 File Offset: 0x00009418
		public string GetMapiMessageClass()
		{
			string result = null;
			if (!this.TryGetTextHeader("X-MS-Exchange-Organization-Text-Messaging-Mapi-Class", out result))
			{
				result = this.EmailMessage.MapiMessageClass;
			}
			return result;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000B244 File Offset: 0x00009444
		public void SetOnceMapiMessageClassToMimeHeader()
		{
			if (this.IsInDeliveryAgent)
			{
				throw new MobileDriverStateException(Strings.ErrorInvalidState("IsInDeliveryAgent", true.ToString()));
			}
			string text = null;
			if (!this.TryGetTextHeader("X-MS-Exchange-Organization-Text-Messaging-Mapi-Class", out text))
			{
				string mapiMessageClass = this.EmailMessage.MapiMessageClass;
				this.SetTextHeader("X-MS-Exchange-Organization-Text-Messaging-Mapi-Class", mapiMessageClass);
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000B29C File Offset: 0x0000949C
		public void ForkSubmission(IList<EnvelopeRecipient> recipients)
		{
			if (this.IsInDeliveryAgent)
			{
				throw new MobileDriverStateException(Strings.ErrorInvalidState("IsInDeliveryAgent", true.ToString()));
			}
			this.SubmittedMessageEventSource.Fork(recipients);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000B2D6 File Offset: 0x000094D6
		public void SetAgentAsyncContext(AgentAsyncContext asyncContext)
		{
			if (this.AgentAsyncContext != null)
			{
				throw new MobileDriverStateException(Strings.ErrorInvalidState("AgentAsyncContext", this.AgentAsyncContext.ToString()));
			}
			this.AgentAsyncContext = asyncContext;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000B304 File Offset: 0x00009504
		public void SetDsnFormat(DsnFormat dsnFormat)
		{
			if (this.IsInDeliveryAgent)
			{
				throw new MobileDriverStateException(Strings.ErrorInvalidState("IsInDeliveryAgent", true.ToString()));
			}
			this.TransportMailItem.DsnFormat = dsnFormat;
		}

		// Token: 0x0400010D RID: 269
		private const int OneK = 1024;

		// Token: 0x0400010E RID: 270
		private MimePart rootPart;

		// Token: 0x0400010F RID: 271
		private EmailMessage emailMessage;

		// Token: 0x04000110 RID: 272
		private ReadOnlyEnvelopeRecipientCollection readOnlyRecipients;

		// Token: 0x04000111 RID: 273
		private TransportMailItem transportMailItem;

		// Token: 0x04000112 RID: 274
		private DsnParameters mailItemDsnParametersCopy;

		// Token: 0x04000113 RID: 275
		private ADSessionSettings adSessionSettings;
	}
}
