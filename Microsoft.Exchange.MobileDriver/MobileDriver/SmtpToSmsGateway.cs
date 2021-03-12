using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.TextMessaging;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.TextMessaging.MobileDriver.Resources;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.MailboxTransport.StoreDriver;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x0200003F RID: 63
	internal class SmtpToSmsGateway : IMobileService
	{
		// Token: 0x0600014F RID: 335 RVA: 0x00007768 File Offset: 0x00005968
		public SmtpToSmsGateway(SmtpToSmsGatewaySelector selector)
		{
			this.Manager = new SmtpToSmsGatewayManager(selector);
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000777C File Offset: 0x0000597C
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00007784 File Offset: 0x00005984
		public SmtpToSmsGatewayManager Manager { get; private set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000778D File Offset: 0x0000598D
		IMobileServiceManager IMobileService.Manager
		{
			get
			{
				return this.Manager;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007795 File Offset: 0x00005995
		public void Send(IList<TextSendingPackage> textPackages, Message message, MobileRecipient sender)
		{
			this.Send(textPackages, message, sender, DsnFormat.Default, null, null);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000077DC File Offset: 0x000059DC
		internal void Send(IList<TextSendingPackage> textPackages, Message message, MobileRecipient sender, DsnFormat dsnFormat, DsnParameters msgDsnParam, IDictionary<MobileRecipient, EnvelopeRecipient> recipientMap)
		{
			ExSmsCounters.NumberOfTextMessagesSentViaSmtp.Increment();
			bool needUpdateDsnParameter = msgDsnParam != null && null != recipientMap;
			Dictionary<string, MobileRecipient> addressMap = null;
			if (needUpdateDsnParameter)
			{
				addressMap = new Dictionary<string, MobileRecipient>(recipientMap.Count);
			}
			foreach (TextSendingPackage textSendingPackage in textPackages)
			{
				TextMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRenderingContainer textMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRenderingContainer = TextMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRenderingContainer.Body;
				List<string> list = new List<string>(textSendingPackage.Recipients.Count);
				foreach (MobileRecipient mobileRecipient in textSendingPackage.Recipients)
				{
					TextMessagingHostingDataServicesServiceSmtpToSmsGateway parameters = this.Manager.GetParameters(mobileRecipient);
					if (parameters == null)
					{
						mobileRecipient.Exceptions.Add(new MobileServiceTransientException(Strings.ErrorUnableDeliverForSmtpToSmsGateway(MobileRecipient.GetNumberString(mobileRecipient))));
					}
					else
					{
						textMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRenderingContainer = parameters.MessageRendering.Container;
						string text = null;
						if (!this.TryBuildSmtpAddress(parameters.RecipientAddressing.SmtpAddress, mobileRecipient.E164Number, out text))
						{
							mobileRecipient.Exceptions.Add(new MobileServiceTransientException(Strings.ErrorUnableDeliverForSmtpToSmsGateway(MobileRecipient.GetNumberString(mobileRecipient))));
						}
						else
						{
							if (needUpdateDsnParameter)
							{
								addressMap[text] = mobileRecipient;
							}
							list.Add(text);
						}
					}
				}
				if (list.Count != 0)
				{
					foreach (Bookmark bookmark in textSendingPackage.BookmarkRetriever.Parts)
					{
						using (MessageItem messageItem = MessageItem.CreateInMemory(StoreObjectSchema.ContentConversionProperties))
						{
							messageItem.ClassName = "IPM.Note.Mobile.SMS";
							messageItem.Sender = new Participant(this.Manager.Selector.Principal);
							messageItem.From = new Participant(this.Manager.Selector.Principal);
							foreach (string emailAddress in list)
							{
								Participant participant = new Participant(null, emailAddress, ProxyAddressPrefix.Smtp.PrimaryPrefix);
								Recipient recipient = messageItem.Recipients.Add(participant, RecipientItemType.To);
								recipient[ItemSchema.Responsibility] = true;
							}
							if (textMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRenderingContainer == TextMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRenderingContainer.Body)
							{
								using (TextWriter textWriter = messageItem.Body.OpenTextWriter(new BodyWriteConfiguration(BodyFormat.TextPlain, Charset.Unicode.Name)))
								{
									textWriter.WriteLine(bookmark.ToString());
									goto IL_2DA;
								}
							}
							messageItem.Subject = bookmark.ToString();
							using (TextWriter textWriter2 = messageItem.Body.OpenTextWriter(new BodyWriteConfiguration(BodyFormat.TextPlain, Charset.Unicode.Name)))
							{
								textWriter2.Write("\r\n");
							}
							IL_2DA:
							messageItem.Save(SaveMode.NoConflictResolution);
							using (MemorySubmissionItem memorySubmissionItem = new MemorySubmissionItem(messageItem, this.Manager.Selector.Principal.MailboxInfo.OrganizationId))
							{
								memorySubmissionItem.Submit(MessageTrackingSource.AGENT, delegate(TransportMailItem mailItem, bool isValid)
								{
									mailItem.DsnFormat = dsnFormat;
									if (needUpdateDsnParameter)
									{
										SmtpToSmsGateway.UpdateDsnParameters(mailItem, msgDsnParam, recipientMap, addressMap);
									}
									return true;
								}, null);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00007C24 File Offset: 0x00005E24
		private static void UpdateDsnParameters(TransportMailItem tmi, DsnParameters msgDsnParam, IDictionary<MobileRecipient, EnvelopeRecipient> mobileRecipToTransportRecip, IDictionary<string, MobileRecipient> addressToMobileRecip)
		{
			tmi.DsnParameters = TransportAgentWrapper.CloneDsnParameters(msgDsnParam);
			foreach (MailRecipient mailRecipient in tmi.Recipients)
			{
				MobileRecipient key = addressToMobileRecip[(string)mailRecipient.Email];
				if (mobileRecipToTransportRecip.ContainsKey(key))
				{
					EnvelopeRecipient envelopeRecipient = mobileRecipToTransportRecip[key];
					MailRecipient mailRecipient2 = TransportAgentWrapper.CastEnvelopeRecipientToMailRecipient(envelopeRecipient);
					mailRecipient.DsnRequested = mailRecipient2.DsnRequested;
					TransportAgentWrapper.AddDsnParameters(mailRecipient, mailRecipient2.DsnParameters);
				}
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00007CC0 File Offset: 0x00005EC0
		private bool TryBuildSmtpAddress(string template, E164Number number, out string address)
		{
			if (string.IsNullOrEmpty(template))
			{
				throw new ArgumentNullException("template");
			}
			if (null == number)
			{
				throw new ArgumentNullException("number");
			}
			address = null;
			StringBuilder stringBuilder = new StringBuilder(template.Length + number.Number.Length);
			bool flag = true;
			int num = 0;
			while (flag && template.Length > num)
			{
				char c = template[num];
				if ('%' != c)
				{
					stringBuilder.Append(c);
				}
				else
				{
					if (template.Length - 1 == num)
					{
						flag = false;
						break;
					}
					char c2 = template[1 + num];
					if (c2 <= 'C')
					{
						if (c2 != '%')
						{
							if (c2 != 'C')
							{
								goto IL_CD;
							}
							goto IL_AF;
						}
						else
						{
							stringBuilder.Append('%');
						}
					}
					else
					{
						if (c2 != 'N')
						{
							if (c2 == 'c')
							{
								goto IL_AF;
							}
							if (c2 != 'n')
							{
								goto IL_CD;
							}
						}
						stringBuilder.Append(number.SignificantNumber);
					}
					IL_CF:
					num++;
					goto IL_D3;
					IL_AF:
					stringBuilder.Append(number.CountryCode);
					goto IL_CF;
					IL_CD:
					flag = false;
					goto IL_CF;
				}
				IL_D3:
				num++;
			}
			string text = stringBuilder.ToString();
			if (flag)
			{
				flag = SmtpAddress.IsValidSmtpAddress(text);
			}
			if (flag)
			{
				address = text;
			}
			return flag;
		}
	}
}
