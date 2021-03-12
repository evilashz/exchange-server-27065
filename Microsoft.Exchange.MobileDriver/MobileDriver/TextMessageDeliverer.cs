using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Configuration.SQM;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.TextMessaging;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.VersionedXml;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.MobileTransport;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.TextMessaging.MobileDriver.Resources;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000043 RID: 67
	internal class TextMessageDeliverer
	{
		// Token: 0x0600016E RID: 366 RVA: 0x000080BF File Offset: 0x000062BF
		private static AckStatusAndResponse ExceptionToAckStatusAndResponse(Exception e)
		{
			return TextMessageDeliverer.AckStatusAndResponseUnableToRoute;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000080C6 File Offset: 0x000062C6
		public TextMessageDeliverer(TextMessageDeliveryContext context)
		{
			this.Context = context;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000080D5 File Offset: 0x000062D5
		// (set) Token: 0x06000171 RID: 369 RVA: 0x000080DD File Offset: 0x000062DD
		public TextMessageDeliveryContext Context { get; private set; }

		// Token: 0x06000172 RID: 370 RVA: 0x000080E8 File Offset: 0x000062E8
		public void Deliver()
		{
			try
			{
				if (this.Stage0Dispatch())
				{
					this.Stage1Translate();
					this.Stage2Compose();
					this.Stage3Deliver();
					this.Stage4Report();
				}
			}
			catch (LocalizedException e)
			{
				this.GenerateDsn(e);
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00008134 File Offset: 0x00006334
		private void SetRecipientDsnParam(IEnumerable<EnvelopeRecipient> recipients, string mapiCls, string dpType, string body, string number, string carrier, object exception)
		{
			if (recipients == null)
			{
				throw new ArgumentNullException("recipients");
			}
			foreach (EnvelopeRecipient recipient in recipients)
			{
				this.SetRecipientDsnParam(recipient, mapiCls, dpType, body, number, carrier, exception);
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00008194 File Offset: 0x00006394
		private void SetRecipientDsnParam(EnvelopeRecipient recipient, string mapiCls, string dpType, string body, string number, string carrier, object exception)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			if (mapiCls != null)
			{
				this.Context.AgentWrapper.AddDsnParameters(recipient, "MapiMessageClass", mapiCls);
			}
			else if (!this.Context.AgentWrapper.DoDsnParametersExist(recipient, "MapiMessageClass"))
			{
				this.Context.AgentWrapper.AddDsnParameters(recipient, "MapiMessageClass", string.Empty);
			}
			if (dpType != null)
			{
				this.Context.AgentWrapper.AddDsnParameters(recipient, "TextMessagingDeliveryPointType", dpType);
			}
			else if (!this.Context.AgentWrapper.DoDsnParametersExist(recipient, "TextMessagingDeliveryPointType"))
			{
				this.Context.AgentWrapper.AddDsnParameters(recipient, "TextMessagingDeliveryPointType", string.Empty);
			}
			if (body != null)
			{
				this.Context.AgentWrapper.AddDsnParameters(recipient, "TextMessagingBodyText", body);
			}
			else if (!this.Context.AgentWrapper.DoDsnParametersExist(recipient, "TextMessagingBodyText"))
			{
				this.Context.AgentWrapper.AddDsnParameters(recipient, "TextMessagingBodyText", string.Empty);
			}
			if (number != null)
			{
				this.Context.AgentWrapper.AddDsnParameters(recipient, "TextMessagingRecipientPhoneNumber", number);
			}
			else if (!this.Context.AgentWrapper.DoDsnParametersExist(recipient, "TextMessagingRecipientPhoneNumber"))
			{
				this.Context.AgentWrapper.AddDsnParameters(recipient, "TextMessagingRecipientPhoneNumber", string.Empty);
			}
			if (carrier != null)
			{
				this.Context.AgentWrapper.AddDsnParameters(recipient, "TextMessagingRecipientCarrier", carrier);
			}
			else if (!this.Context.AgentWrapper.DoDsnParametersExist(recipient, "TextMessagingRecipientCarrier"))
			{
				this.Context.AgentWrapper.AddDsnParameters(recipient, "TextMessagingRecipientCarrier", string.Empty);
			}
			object obj = null;
			if (!this.Context.AgentWrapper.TryGetDsnParameters(recipient, "TextMessagingRecipientExceptions", out obj))
			{
				this.Context.AgentWrapper.AddDsnParameters(recipient, "TextMessagingRecipientExceptions", obj = new List<Exception>());
			}
			if (exception is Exception)
			{
				((List<Exception>)obj).Add((Exception)exception);
				return;
			}
			if (exception is IList<Exception>)
			{
				((List<Exception>)obj).AddRange((IList<Exception>)exception);
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000083B0 File Offset: 0x000065B0
		internal bool Stage0Dispatch()
		{
			if (this.Context.MobileService != null)
			{
				return true;
			}
			if (this.Context.AgentWrapper == null)
			{
				throw new MobileDriverStateException(Strings.ErrorInvalidState("AgentWrapper", Strings.ConstNull));
			}
			this.Context.AgentWrapper.Resume();
			this.Context.MapiMessageClass = this.Context.AgentWrapper.GetMapiMessageClass();
			this.Context.AgentWrapper.AddDsnParameters("MapiMessageClass", this.Context.MapiMessageClass);
			Dictionary<MobileRecipient, EnvelopeRecipient> dictionary = new Dictionary<MobileRecipient, EnvelopeRecipient>(this.Context.AgentWrapper.ReadOnlyRecipients.Count);
			List<EnvelopeRecipient> list = new List<EnvelopeRecipient>(this.Context.AgentWrapper.ReadOnlyRecipients.Count);
			foreach (EnvelopeRecipient envelopeRecipient in this.Context.AgentWrapper.ReadOnlyRecipients)
			{
				this.SetRecipientDsnParam(envelopeRecipient, this.Context.MapiMessageClass, null, null, null, null, null);
				MobileRecipient mobileRecipientFromImceaAddress = EmailMessageHelper.GetMobileRecipientFromImceaAddress((string)envelopeRecipient.Address);
				if (mobileRecipientFromImceaAddress == null)
				{
					this.SetRecipientDsnParam(envelopeRecipient, null, null, null, null, null, new MobileServicePermanentException(Strings.ErrorInvalidPhoneNumber));
					this.AckRecipientWithSqmReport(envelopeRecipient, TextMessageDeliverer.AckStatusAndResponseInvalidRecipientAddress.AckStatus, TextMessageDeliverer.AckStatusAndResponseInvalidRecipientAddress.SmtpResponse);
				}
				else
				{
					this.SetRecipientDsnParam(envelopeRecipient, null, null, null, MobileRecipient.GetNumberString(mobileRecipientFromImceaAddress), null, null);
					if (dictionary.ContainsKey(mobileRecipientFromImceaAddress))
					{
						list.Add(envelopeRecipient);
					}
					else
					{
						dictionary[mobileRecipientFromImceaAddress] = envelopeRecipient;
					}
				}
			}
			if (!this.Context.AgentWrapper.IsInDeliveryAgent)
			{
				this.Context.AgentWrapper.RemoveRecipients(list);
			}
			this.Context.Recipients = dictionary;
			EmailRecipient sender = EmailMessageHelper.GetSender(this.Context.AgentWrapper.EmailMessage);
			this.Context.SetPrincipalFromProxyAddress(ProxyAddress.Parse(sender.NativeAddressType, sender.NativeAddress).ProxyAddressString);
			bool flag = this.Context.IsUndercurrentMessage || this.Context.IsAlertMessage;
			this.Context.EcpLinkUrl = TextMessagingHelper.GetEcpUrl(this.Context.Principal);
			if (ObjectClass.IsOfClass(this.Context.MapiMessageClass, "IPM.Note.Mobile.MMS"))
			{
				throw new MobileServiceCapabilityException(Strings.ErrorNotSupportMultimediaMessage);
			}
			string b = null;
			if (!this.Context.AgentWrapper.TryGetTextHeader("X-MS-Exchange-Organization-Text-Messaging-Originator", out b))
			{
				throw new MobileDriverStateException(Strings.ErrorInvalidState("X-MS-Exchange-Organization-Text-Messaging-Originator", Strings.ConstNull));
			}
			bool flag2 = string.Equals("Agent:TextMessagingInternalDelivery-86DB88E6-E880-4564-B1EC-25C9797FEBBE", b);
			if (flag2)
			{
				this.SetFirstHopTimestamp(this.Context.AgentWrapper);
			}
			if (flag2 && this.Context.IsUndercurrentMessage)
			{
				this.Context.AgentWrapper.SetDsnFormat(DsnFormat.Headers);
			}
			this.Context.Settings = (this.Context.AgentWrapper.GetCachedTextMessagingSettings() as TextMessagingSettingsVersion1Point0);
			string name = string.Empty;
			if (!this.Context.AgentWrapper.TryGetTextHeader("X-MS-Exchange-Organization-Text-Messaging-Notification-PreferredCulture", out name))
			{
				name = "en-US";
			}
			this.Context.NotificationPreferredCulture = new CultureInfo(name);
			if (this.Context.Settings == null)
			{
				using (MailboxSession mailboxSession = MailboxSession.OpenAsTransport(this.Context.Principal, "Client=HUB"))
				{
					using (VersionedXmlDataProvider versionedXmlDataProvider = new VersionedXmlDataProvider(mailboxSession))
					{
						TextMessagingAccount textMessagingAccount = (TextMessagingAccount)versionedXmlDataProvider.Read<TextMessagingAccount>(this.Context.Principal.ObjectId);
						this.Context.Settings = textMessagingAccount.TextMessagingSettings;
						if (textMessagingAccount.NotificationPreferredCulture != null)
						{
							this.Context.NotificationPreferredCulture = textMessagingAccount.NotificationPreferredCulture;
						}
						if (!this.Context.AgentWrapper.IsInDeliveryAgent)
						{
							this.Context.AgentWrapper.SetCachedTextMessagingSettings(this.Context.Settings);
							this.Context.AgentWrapper.SetTextHeader("X-MS-Exchange-Organization-Text-Messaging-Notification-PreferredCulture", this.Context.NotificationPreferredCulture.ToString());
						}
					}
				}
			}
			TextMessagingAccount textMessagingAccount2 = new TextMessagingAccount();
			textMessagingAccount2.TextMessagingSettings = this.Context.Settings;
			if (this.Context.IsRuleAlertMessage && textMessagingAccount2.EasEnabled)
			{
				this.SetRecipientDsnParam(this.Context.AgentWrapper.ReadOnlyRecipients, null, DeliveryPointType.ExchangeActiveSync.ToString(), null, null, null, null);
				throw new MobileDriverStateException(Strings.ErrorNotSupportM2pWhenEasEnabled(this.Context.EcpInboxRuleSlab));
			}
			IList<DeliveryPoint> list2 = null;
			if (flag)
			{
				list2 = this.Context.Settings.MachineToPersonPreferences;
			}
			else
			{
				list2 = this.Context.Settings.PersonToPersonPreferences;
			}
			if (list2.Count == 0)
			{
				if (flag)
				{
					throw new MobileDriverStateException(Strings.ErrorNoM2pDeliveryPoint(this.Context.EcpEditNotificatonWizard));
				}
				throw new MobileDriverStateException(Strings.ErrorNoP2pDeliveryPoint(this.Context.EcpTextMessagingSlab));
			}
			else
			{
				if (flag2 && DeliveryPointType.ExchangeActiveSync != list2[0].Type)
				{
					this.Context.PartnerDelivery = true;
					return false;
				}
				List<KeyValuePair<MobileRecipient, EnvelopeRecipient>> list3 = new List<KeyValuePair<MobileRecipient, EnvelopeRecipient>>(this.Context.AgentWrapper.ReadOnlyRecipients.Count);
				if (flag)
				{
					foreach (KeyValuePair<MobileRecipient, EnvelopeRecipient> item in this.Context.Recipients)
					{
						PossibleRecipient mathed = PossibleRecipient.GetMathed(this.Context.Settings.MachineToPersonMessagingPolicies.EffectivePossibleRecipients, item.Key.E164Number, true);
						if (mathed != null && !flag2)
						{
							TextMessagingHostingDataCarriersCarrier carrier = TextMessagingHostingDataCache.Instance.GetCarrier(mathed.Carrier);
							TextMessagingHostingDataCarriersCarrierLocalizedInfo[] array = (carrier == null) ? null : carrier.LocalizedInfo;
							string carrier2 = (array == null || array.Length == 0) ? null : array[0].DisplayName;
							this.SetRecipientDsnParam(item.Value, null, null, null, null, carrier2, null);
						}
						if (mathed == null || (!mathed.Acknowledged && !this.Context.IsUndercurrentMessage))
						{
							bool flag3 = 0 < this.Context.Settings.MachineToPersonMessagingPolicies.EffectivePossibleRecipients.Count;
							this.SetRecipientDsnParam(item.Value, null, null, null, null, null, new MobileServicePermanentException((flag3 && this.Context.IsRuleAlertMessage) ? Strings.ErrorNotAcknowledged(MobileRecipient.GetNumberString(item.Key), this.Context.EcpEditNotificatonWizard) : Strings.ErrorNoM2pDeliveryPointForEmailAlert(this.Context.EcpInboxRuleSlab)));
							list3.Add(item);
						}
						else
						{
							try
							{
								item.Key.Region = new RegionInfo(mathed.Region);
							}
							catch (ArgumentException)
							{
								ExTraceGlobals.XsoTracer.TraceDebug<string, MobileRecipient>((long)this.GetHashCode(), "region '{0}' is not valid for recipient '{1}'", mathed.Region, item.Key);
							}
							int carrier3 = -1;
							if (int.TryParse(mathed.Carrier, out carrier3))
							{
								item.Key.Carrier = carrier3;
							}
							else
							{
								ExTraceGlobals.XsoTracer.TraceDebug<string, MobileRecipient>((long)this.GetHashCode(), "carrier '{0}' is not valid for recipient '{1}'", mathed.Region, item.Key);
							}
						}
					}
				}
				foreach (KeyValuePair<MobileRecipient, EnvelopeRecipient> keyValuePair in list3)
				{
					this.AckRecipientWithSqmReport(keyValuePair.Value, TextMessageDeliverer.AckStatusAndResponseInvalidRecipientAddress.AckStatus, TextMessageDeliverer.AckStatusAndResponseInvalidRecipientAddress.SmtpResponse);
					this.Context.Recipients.Remove(keyValuePair.Key);
				}
				string dpType = null;
				foreach (DeliveryPoint deliveryPoint in list2)
				{
					dpType = deliveryPoint.Type.ToString();
					IMobileService mobileService = null;
					try
					{
						mobileService = MobileServiceCreator.Create(this.Context.Principal, deliveryPoint);
					}
					catch (LocalizedException ex)
					{
						foreach (KeyValuePair<MobileRecipient, EnvelopeRecipient> keyValuePair2 in this.Context.Recipients)
						{
							this.SetRecipientDsnParam(keyValuePair2.Value, null, null, null, null, null, ex);
							this.AckTransportMailItemRecipient(keyValuePair2.Value, new Exception[]
							{
								ex
							});
						}
						this.Context.Recipients.Clear();
						return true;
					}
					if (mobileService.Manager.CapabilityPerRecipientSupported)
					{
						bool flag4 = true;
						foreach (MobileRecipient recipient in this.Context.Recipients.Keys)
						{
							if (mobileService.Manager.GetCapabilityForRecipient(recipient) == null)
							{
								flag4 = false;
								break;
							}
						}
						if (!flag4)
						{
							dpType = null;
							mobileService = null;
						}
					}
					if (mobileService != null)
					{
						this.Context.MobileService = mobileService;
						break;
					}
				}
				try
				{
					if (this.Context.MobileService == null)
					{
						EmailMessageHelper.ThrowErrorNoProviderForNotificationNDR(this.Context.EcpTextMessagingSlab, this.Context.EcpEditNotificatonWizard);
					}
				}
				catch (LocalizedException ex2)
				{
					foreach (KeyValuePair<MobileRecipient, EnvelopeRecipient> keyValuePair3 in this.Context.Recipients)
					{
						this.SetRecipientDsnParam(keyValuePair3.Value, null, null, null, null, null, ex2);
						this.AckTransportMailItemRecipient(keyValuePair3.Value, new Exception[]
						{
							ex2
						});
					}
					this.Context.Recipients.Clear();
					return true;
				}
				this.SetRecipientDsnParam(this.Context.AgentWrapper.ReadOnlyRecipients, null, dpType, null, null, null, null);
				return true;
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00008E68 File Offset: 0x00007068
		internal void Stage1Translate()
		{
			if (this.Context.Message != null)
			{
				return;
			}
			if (this.Context.AgentWrapper == null)
			{
				throw new MobileDriverStateException(Strings.ErrorInvalidState("AgentWrapper", Strings.ConstNull));
			}
			this.Context.AgentWrapper.Resume();
			if (this.Context.AgentWrapper.ReadOnlyRecipients.Count == 0)
			{
				return;
			}
			CalendarNotificationType calNotifTypeHint = CalendarNotificationType.Uninteresting;
			this.Context.Message = new EmailMessageToMessageItem().Convert(this.Context.AgentWrapper.ADSessionSettings, this.Context.AgentWrapper.EmailMessage, this.Context.MapiMessageClass, this.Context.Recipients.Keys, this.Context.NotificationPreferredCulture, out calNotifTypeHint);
			this.Context.CalNotifTypeHint = calNotifTypeHint;
			this.Context.IsFromOutlook = (this.Context.AgentWrapper.EmailMessage.Attachments.Count > 0);
			this.SetRecipientDsnParam(this.Context.AgentWrapper.ReadOnlyRecipients, null, null, this.Context.Message.Message.OriginalText, null, null, null);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00008F98 File Offset: 0x00007198
		internal void Stage2Compose()
		{
			if (this.Context.AgentWrapper != null)
			{
				this.Context.AgentWrapper.Resume();
			}
			if (this.Context.TextSendingPackages != null)
			{
				return;
			}
			if (this.Context.Recipients != null && this.Context.Recipients.Count == 0)
			{
				return;
			}
			if (this.Context.Message == null)
			{
				throw new MobileDriverStateException(Strings.ErrorInvalidState("Message", Strings.ConstNull));
			}
			if (this.Context.MobileService == null)
			{
				throw new MobileDriverStateException(Strings.ErrorInvalidState("MobileSerivce", Strings.ConstNull));
			}
			int messageCount = 0;
			bool hasUnicode = false;
			this.Context.TextSendingPackages = new MessageItemToTextSendingPackages(this.Context.MobileService.Manager).Convert(this.Context.Message, this.Context.CalNotifTypeHint, out messageCount, out hasUnicode);
			if (this.Context.TextSendingPackages != null && 0 < this.Context.TextSendingPackages.Count && 0 < this.Context.TextSendingPackages[0].BookmarkRetriever.Parts.Count)
			{
				this.SetRecipientDsnParam(this.Context.AgentWrapper.ReadOnlyRecipients, null, null, this.Context.TextSendingPackages[0].BookmarkRetriever.Parts[0].FullText, null, null, null);
			}
			if (new TextMessagingAccount
			{
				TextMessagingSettings = this.Context.Settings
			}.EasEnabled)
			{
				SmsSqmDataPointHelper.AddEasMessageDataPoint(SmsSqmSession.Instance, this.Context.Principal.ObjectId, this.Context.Principal.LegacyDn, messageCount, hasUnicode, this.Context.Recipients.Keys.Count, this.Context.IsFromOutlook);
				return;
			}
			SmsSqmDataPointHelper.AddNotificationMessageDataPoint(SmsSqmSession.Instance, this.Context.Principal.ObjectId, this.Context.Principal.LegacyDn, SmsSqmDataPointHelper.TranslateEnumForSqm<CalendarNotificationType>(this.Context.CalNotifTypeHint), this.Context.IsVoicemailMessage, this.Context.IsRuleAlertMessage, this.Context.IsUndercurrentMessage || this.Context.IsAlertInfoMessage, messageCount, this.Context.Settings.MachineToPersonMessagingPolicies.PossibleRecipients[0].Carrier, this.Context.Settings.MachineToPersonMessagingPolicies.PossibleRecipients[0].Region);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00009220 File Offset: 0x00007420
		internal void Stage3Deliver()
		{
			if (this.Context.AgentWrapper != null)
			{
				this.Context.AgentWrapper.Resume();
			}
			if (this.Context.Recipients != null && this.Context.Recipients.Count == 0)
			{
				return;
			}
			if (this.Context.TextSendingPackages == null)
			{
				throw new MobileDriverStateException(Strings.ErrorInvalidState("TextSendingPackage", Strings.ConstNull));
			}
			if (this.Context.Message == null)
			{
				throw new MobileDriverStateException(Strings.ErrorInvalidState("Message", Strings.ConstNull));
			}
			if (this.Context.MobileService == null)
			{
				throw new MobileDriverStateException(Strings.ErrorInvalidState("MobileSerivce", Strings.ConstNull));
			}
			if (this.Context.AgentWrapper != null && this.Context.MobileService is SmtpToSmsGateway)
			{
				((SmtpToSmsGateway)this.Context.MobileService).Send(this.Context.TextSendingPackages, this.Context.Message.Message, this.Context.Message.Sender, this.Context.IsUndercurrentMessage ? DsnFormat.Headers : DsnFormat.Default, this.Context.AgentWrapper.MailItemDsnParametersCopy, this.Context.Recipients);
			}
			else if (this.Context.AgentWrapper != null && this.Context.MobileService is Eas)
			{
				((Eas)this.Context.MobileService).Send(this.Context.TextSendingPackages, this.Context.Message.Message, this.Context.Message.Sender, this.Context.AgentWrapper.EmailMessage.MessageId);
			}
			else
			{
				this.Context.MobileService.Send(this.Context.TextSendingPackages, this.Context.Message.Message, this.Context.Message.Sender);
			}
			ExSmsCounters.NumberOfTextMessagesSent.Increment();
			this.UpdateAverageDeliveryLatency(this.Context.AgentWrapper);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000943F File Offset: 0x0000763F
		internal void Stage4Report()
		{
			if (this.Context.AgentWrapper != null)
			{
				this.Context.AgentWrapper.Resume();
			}
			this.GenerateDsn(null);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00009468 File Offset: 0x00007668
		internal void GenerateDsn(Exception e)
		{
			if (this.Context.PartnerDelivery)
			{
				return;
			}
			if (e != null)
			{
				if (this.Context.AgentWrapper == null)
				{
					throw e;
				}
				if (e is MobileDriverEmailNotificationDeadLoopException)
				{
					foreach (EnvelopeRecipient envelopeRecipient in this.Context.Recipients.Values)
					{
						ExTraceGlobals.XsoTracer.TraceDebug<RoutingAddress>((long)this.GetHashCode(), "Acking recipient {0} as fail for MobileDriverEmailNotificationDeadLoopException. No NDR will be generated.", envelopeRecipient.Address);
						envelopeRecipient.RequestedReports = DsnTypeRequested.Never;
						this.AckRecipientWithSqmReport(envelopeRecipient, AckStatus.Fail, TextMessageDeliverer.SmtpResponseDeadLoop);
					}
					return;
				}
				foreach (KeyValuePair<MobileRecipient, EnvelopeRecipient> keyValuePair in this.Context.Recipients)
				{
					MobileRecipient key = keyValuePair.Key;
					EnvelopeRecipient value = keyValuePair.Value;
					TextMessagingHostingDataCarriersCarrier carrier = TextMessagingHostingDataCache.Instance.GetCarrier(key.Carrier.ToString("00000"));
					TextMessagingHostingDataCarriersCarrierLocalizedInfo[] array = (carrier == null) ? null : carrier.LocalizedInfo;
					string carrier2 = (array == null || array.Length == 0) ? null : array[0].DisplayName;
					this.SetRecipientDsnParam(value, null, null, null, null, carrier2, e);
					this.AckTransportMailItemRecipient(value, new Exception[]
					{
						e
					});
				}
				this.Context.Recipients.Clear();
			}
			else if (this.Context.TextSendingPackages != null)
			{
				foreach (TextSendingPackage textSendingPackage in this.Context.TextSendingPackages)
				{
					foreach (MobileRecipient mobileRecipient in textSendingPackage.Recipients)
					{
						if (mobileRecipient.Exceptions != null && 0 < mobileRecipient.Exceptions.Count)
						{
							if (this.Context.AgentWrapper == null)
							{
								throw mobileRecipient.Exceptions[0];
							}
							if (this.Context.Recipients.ContainsKey(mobileRecipient))
							{
								EnvelopeRecipient recipient = this.Context.Recipients[mobileRecipient];
								TextMessagingHostingDataCarriersCarrier carrier3 = TextMessagingHostingDataCache.Instance.GetCarrier(mobileRecipient.Carrier.ToString("00000"));
								TextMessagingHostingDataCarriersCarrierLocalizedInfo[] array2 = (carrier3 == null) ? null : carrier3.LocalizedInfo;
								string carrier4 = (array2 == null || array2.Length == 0) ? null : array2[0].DisplayName;
								this.SetRecipientDsnParam(recipient, null, null, null, null, carrier4, mobileRecipient.Exceptions);
								this.AckTransportMailItemRecipient(recipient, mobileRecipient.Exceptions);
								this.Context.Recipients.Remove(mobileRecipient);
							}
						}
					}
				}
			}
			if (this.Context.AgentWrapper != null)
			{
				foreach (EnvelopeRecipient recipient2 in this.Context.Recipients.Values)
				{
					this.AckRecipientWithSqmReport(recipient2, TextMessageDeliverer.AckStatusAndResponseNoopOk.AckStatus, TextMessageDeliverer.AckStatusAndResponseNoopOk.SmtpResponse);
				}
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00009818 File Offset: 0x00007A18
		private void AckTransportMailItemRecipient(EnvelopeRecipient recipient, IList<Exception> exceptions)
		{
			if (exceptions == null || exceptions.Count == 0)
			{
				return;
			}
			AckStatusAndResponse ackStatusAndResponse = TextMessageDeliverer.ExceptionToAckStatusAndResponse(exceptions[0]);
			this.AckRecipientWithSqmReport(recipient, ackStatusAndResponse.AckStatus, ackStatusAndResponse.SmtpResponse);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00009854 File Offset: 0x00007A54
		private void AckRecipientWithSqmReport(EnvelopeRecipient recipient, AckStatus ackStatus, SmtpResponse smtpResponse)
		{
			this.Context.AgentWrapper.AckRecipient(recipient, ackStatus, smtpResponse);
			if (ackStatus == AckStatus.Fail)
			{
				bool isEASMessage = true;
				if (this.Context.MapiMessageClass.StartsWith("IPM.Note.Mobile.SMS.", StringComparison.OrdinalIgnoreCase))
				{
					isEASMessage = false;
				}
				SmsSqmDataPointHelper.AddNdrDataPoint(SmsSqmSession.Instance, SmsSqmDataPointHelper.TranslateEnumForSqm<CalendarNotificationType>(this.Context.CalNotifTypeHint), isEASMessage, this.Context.IsVoicemailMessage, this.Context.IsRuleAlertMessage, this.Context.IsUndercurrentMessage || this.Context.IsAlertInfoMessage, smtpResponse.StatusText[0]);
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000098EC File Offset: 0x00007AEC
		private void SetFirstHopTimestamp(TransportAgentWrapper textmessage)
		{
			string text = ((DateTime)ExDateTime.UtcNow).ToString("yyyyMMddhhmmssfff");
			textmessage.SetTextHeader("X-MS-Exchange-Organization-Text-Messaging-Timestamp", text);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00009920 File Offset: 0x00007B20
		private void UpdateAverageDeliveryLatency(TransportAgentWrapper textmessage)
		{
			string s;
			if (!textmessage.TryGetTextHeader("X-MS-Exchange-Organization-Text-Messaging-Timestamp", out s))
			{
				return;
			}
			ExDateTime dt = ExDateTime.ParseExact(s, "yyyyMMddhhmmssfff", null);
			int milliseconds = (ExDateTime.UtcNow - dt).Milliseconds;
			lock (TextMessageDeliverer.deliveryLatencySamples)
			{
				int[] array = TextMessageDeliverer.deliveryLatencySamples;
				int num = TextMessageDeliverer.insertionIndex;
				int num2 = TextMessageDeliverer.numSamples;
				int num3 = array[num];
				array[num] = milliseconds;
				TextMessageDeliverer.insertionIndex = (num + 1) % array.Length;
				if (num2 < array.Length)
				{
					num2 = ++TextMessageDeliverer.numSamples;
				}
				TextMessageDeliverer.latencySum += milliseconds;
				TextMessageDeliverer.latencySum -= num3;
				ExSmsCounters.AverageDeliveryLatency.RawValue = (long)(TextMessageDeliverer.latencySum / num2);
			}
		}

		// Token: 0x040000E1 RID: 225
		private static int[] deliveryLatencySamples = new int[1024];

		// Token: 0x040000E2 RID: 226
		private static int insertionIndex;

		// Token: 0x040000E3 RID: 227
		private static int latencySum;

		// Token: 0x040000E4 RID: 228
		private static int numSamples;

		// Token: 0x040000E5 RID: 229
		internal static readonly AckStatusAndResponse AckStatusAndResponseNoopOk = new AckStatusAndResponse(AckStatus.Success, SmtpResponse.NoopOk);

		// Token: 0x040000E6 RID: 230
		internal static readonly AckStatusAndResponse AckStatusAndResponseInvalidRecipientAddress = new AckStatusAndResponse(AckStatus.Fail, SmtpResponse.InvalidRecipientAddress);

		// Token: 0x040000E7 RID: 231
		internal static readonly AckStatusAndResponse AckStatusAndResponseUnableToRoute = new AckStatusAndResponse(AckStatus.Fail, SmtpResponse.UnableToRoute);

		// Token: 0x040000E8 RID: 232
		internal static readonly SmtpResponse SmtpResponseDeadLoop = new SmtpResponse("554", "5.4.6", new string[]
		{
			"Email Notification dead loop detected."
		});
	}
}
