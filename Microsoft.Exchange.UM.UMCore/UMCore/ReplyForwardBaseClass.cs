using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001C7 RID: 455
	internal abstract class ReplyForwardBaseClass : XsoRecordedMessage
	{
		// Token: 0x06000D40 RID: 3392 RVA: 0x0003A8A5 File Offset: 0x00038AA5
		internal ReplyForwardBaseClass(BaseUMCallSession vo, UMSubscriber user, RetrieveVoicemailManager context) : base(vo, user, context)
		{
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x0003A8B0 File Offset: 0x00038AB0
		protected override bool IsPureVoiceMessage
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0003A8B4 File Offset: 0x00038AB4
		protected override MessageItem GenerateProtectedMessage(MailboxSession session)
		{
			MessageItem messageItem = this.GenerateMessage(session);
			bool flag = false;
			foreach (AttachmentHandle handle in messageItem.AttachmentCollection)
			{
				using (Attachment attachment = messageItem.AttachmentCollection.Open(handle))
				{
					if (XsoUtil.IsValidAudioAttachment(attachment))
					{
						attachment.FileName = DRMUtils.GetProtectedUMFileNameToUse(attachment.FileName);
						attachment.Save();
						flag = true;
					}
				}
			}
			if (flag)
			{
				messageItem[MessageItemSchema.VoiceMessageAttachmentOrder] = DRMUtils.GetProtectedUMVoiceMessageAttachmentOrder(XsoUtil.GetAttachmentOrderString(messageItem));
			}
			return messageItem;
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0003A96C File Offset: 0x00038B6C
		protected override void AddMessageHeader(Item originalMessage, MessageContentBuilder content)
		{
			content.AddEmailHeader((MessageItem)originalMessage);
		}
	}
}
