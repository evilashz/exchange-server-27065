using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000397 RID: 919
	public class ItemPartRecipientWell : ItemRecipientWell
	{
		// Token: 0x060022C3 RID: 8899 RVA: 0x000C6D20 File Offset: 0x000C4F20
		internal ItemPartRecipientWell(ItemPart itemPart)
		{
			this.itemPart = itemPart;
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x000C6EB8 File Offset: 0x000C50B8
		internal override IEnumerator<Participant> GetRecipientsCollection(RecipientWellType type)
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
			foreach (IParticipant participant2 in this.itemPart.Recipients[recipientItemType])
			{
				Participant participant = (Participant)participant2;
				yield return participant;
			}
			yield break;
		}

		// Token: 0x04001879 RID: 6265
		private ItemPart itemPart;
	}
}
