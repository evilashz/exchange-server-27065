using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003B5 RID: 949
	public class MessageRecipientWell : ItemRecipientWell
	{
		// Token: 0x060023AB RID: 9131 RVA: 0x000CD260 File Offset: 0x000CB460
		internal MessageRecipientWell(MessageItem message)
		{
			this.message = message;
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x000CD26F File Offset: 0x000CB46F
		public MessageRecipientWell() : this(null)
		{
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x000CD534 File Offset: 0x000CB734
		internal override IEnumerator<Participant> GetRecipientsCollection(RecipientWellType type)
		{
			if (this.message != null)
			{
				RecipientItemType recipientItemType;
				switch (type)
				{
				case RecipientWellType.To:
					recipientItemType = RecipientItemType.To;
					break;
				case RecipientWellType.Cc:
					recipientItemType = RecipientItemType.Cc;
					break;
				case RecipientWellType.Bcc:
					recipientItemType = RecipientItemType.Bcc;
					break;
				default:
					recipientItemType = RecipientItemType.Unknown;
					break;
				}
				if (recipientItemType == RecipientItemType.To && this.message is ReportMessage)
				{
					Participant participant = ((ReportMessage)this.message).ReceivedRepresenting;
					if (null != participant)
					{
						yield return participant;
					}
				}
				else if (type == RecipientWellType.From)
				{
					Participant participant2 = this.message.From;
					if (null != participant2)
					{
						yield return participant2;
					}
				}
				else
				{
					foreach (Recipient recipient in this.message.Recipients)
					{
						if (recipient.RecipientItemType == recipientItemType)
						{
							object isResentMessage = this.message.TryGetProperty(MessageItemSchema.IsResend);
							if (!(isResentMessage is bool) || !(bool)isResentMessage || !recipient.Submitted)
							{
								yield return recipient.Participant;
							}
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x040018CF RID: 6351
		private MessageItem message;
	}
}
