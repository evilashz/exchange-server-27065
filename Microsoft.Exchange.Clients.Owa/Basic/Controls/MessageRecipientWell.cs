using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000069 RID: 105
	public class MessageRecipientWell : ItemRecipientWell
	{
		// Token: 0x060002E5 RID: 741 RVA: 0x00018F1C File Offset: 0x0001711C
		internal MessageRecipientWell(UserContext userContext, MessageItem message) : base(userContext)
		{
			this.message = message;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000191A0 File Offset: 0x000173A0
		internal override IEnumerator<Participant> GetRecipientCollection(RecipientWellType type)
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
						yield return ((ReportMessage)this.message).ReceivedRepresenting;
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

		// Token: 0x04000219 RID: 537
		private MessageItem message;
	}
}
