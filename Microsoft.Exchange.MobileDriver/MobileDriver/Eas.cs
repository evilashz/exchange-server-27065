using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.TextMessaging.MobileDriver.Resources;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000019 RID: 25
	internal class Eas : IMobileService
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00003DB8 File Offset: 0x00001FB8
		public Eas(EasSelector selector)
		{
			this.Manager = new EasManager(selector);
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003DCC File Offset: 0x00001FCC
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00003DD4 File Offset: 0x00001FD4
		public EasManager Manager { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003DDD File Offset: 0x00001FDD
		IMobileServiceManager IMobileService.Manager
		{
			get
			{
				return this.Manager;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003DE5 File Offset: 0x00001FE5
		public void Send(IList<TextSendingPackage> textPackages, Message message, MobileRecipient sender)
		{
			this.Send(textPackages, message, sender, null);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003DF4 File Offset: 0x00001FF4
		public void Send(IList<TextSendingPackage> textPackages, Message message, MobileRecipient sender, string internetMessageId)
		{
			ExSmsCounters.NumberOfTextMessagesSentViaEas.Increment();
			using (MailboxSession mailboxSession = MailboxSession.OpenAsTransport(this.Manager.Selector.Principal, OpenTransportSessionFlags.OpenForSpecialMessageDelivery))
			{
				StoreObjectId storeObjectId = null;
				try
				{
					storeObjectId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Outbox);
					if (storeObjectId == null)
					{
						throw new MobileServicePermanentException(Strings.ErrorObjectNotFound(DefaultFolderType.Outbox.ToString()));
					}
				}
				catch (ObjectNotFoundException ex)
				{
					throw new MobileServicePermanentException(ex.LocalizedString, ex);
				}
				foreach (TextSendingPackage textSendingPackage in textPackages)
				{
					foreach (Bookmark bookmark in textSendingPackage.BookmarkRetriever.Parts)
					{
						using (MessageItem messageItem = MessageItem.Create(mailboxSession, storeObjectId))
						{
							messageItem.ClassName = "IPM.Note.Mobile.SMS";
							messageItem.Sender = new Participant(this.Manager.Selector.Principal);
							messageItem.From = new Participant(this.Manager.Selector.Principal.MailboxInfo.DisplayName, this.Manager.Selector.Number.Number, "MOBILE");
							foreach (MobileRecipient recipient in textSendingPackage.Recipients)
							{
								Participant participant = new Participant(null, MobileRecipient.GetNumberString(recipient), "MOBILE");
								Recipient recipient2 = messageItem.Recipients.Add(participant, RecipientItemType.To);
								recipient2[ItemSchema.Responsibility] = true;
							}
							using (TextWriter textWriter = messageItem.Body.OpenTextWriter(new BodyWriteConfiguration(BodyFormat.TextPlain, Charset.Unicode.Name)))
							{
								textWriter.Write(bookmark.ToString());
							}
							if (!string.IsNullOrEmpty(internetMessageId))
							{
								messageItem.SetProperties(Eas.propertyInternetMessageId, new object[]
								{
									internetMessageId
								});
							}
							messageItem.SetProperties(Eas.propertyTextMessageDeliveryStatus, Eas.propertyValueTextMessageDeliveryStatus);
							mailboxSession.Deliver(messageItem, null, RecipientItemType.Unknown);
						}
					}
				}
			}
		}

		// Token: 0x04000034 RID: 52
		private static readonly PropertyDefinition[] propertyTextMessageDeliveryStatus = new PropertyDefinition[]
		{
			MessageItemSchema.TextMessageDeliveryStatus
		};

		// Token: 0x04000035 RID: 53
		private static readonly object[] propertyValueTextMessageDeliveryStatus = new object[]
		{
			25
		};

		// Token: 0x04000036 RID: 54
		private static readonly PropertyDefinition[] propertyInternetMessageId = new PropertyDefinition[]
		{
			ItemSchema.InternetMessageId
		};
	}
}
