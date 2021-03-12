using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000062 RID: 98
	public class MeetingRequestRecipientWell : ItemRecipientWell
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x00018525 File Offset: 0x00016725
		internal MeetingRequestRecipientWell(UserContext userContext, MeetingRequest meetingRequest) : base(userContext)
		{
			this.meetingRequest = meetingRequest;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x000186D4 File Offset: 0x000168D4
		internal override IEnumerator<Participant> GetRecipientCollection(RecipientWellType type)
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
			foreach (BlobRecipient recipient in this.meetingRequest.GetMergedRecipientList())
			{
				if (MapiUtil.MapiRecipientTypeToRecipientItemType((RecipientType)recipient[InternalSchema.RecipientType]) == recipientItemType)
				{
					yield return recipient.Participant;
				}
			}
			yield break;
		}

		// Token: 0x0400020B RID: 523
		private MeetingRequest meetingRequest;
	}
}
