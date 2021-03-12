using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003B0 RID: 944
	public class MeetingRequestRecipientWell : ItemRecipientWell
	{
		// Token: 0x06002385 RID: 9093 RVA: 0x000CC405 File Offset: 0x000CA605
		internal MeetingRequestRecipientWell(MeetingRequest meetingRequest)
		{
			this.meetingRequest = meetingRequest;
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x000CC5B0 File Offset: 0x000CA7B0
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
			foreach (BlobRecipient recipient in this.meetingRequest.GetMergedRecipientList())
			{
				if (MapiUtil.MapiRecipientTypeToRecipientItemType((RecipientType)recipient[InternalSchema.RecipientType]) == recipientItemType)
				{
					yield return recipient.Participant;
				}
			}
			yield break;
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x000CC5D4 File Offset: 0x000CA7D4
		internal override void RenderContents(TextWriter writer, UserContext userContext, RecipientWellType type, RecipientWellNode.RenderFlags flags, RenderRecipientWellNode wellNode)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (this.meetingRequest.IsDelegated())
			{
				Utilities.SanitizeHtmlEncode(CalendarUtilities.GetDisplayAttendees(this.meetingRequest, type), writer);
				return;
			}
			base.RenderContents(writer, userContext, type, flags, wellNode);
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x000CC62A File Offset: 0x000CA82A
		public override bool HasRecipients(RecipientWellType type)
		{
			if (this.meetingRequest.IsDelegated())
			{
				return !string.IsNullOrEmpty(CalendarUtilities.GetDisplayAttendees(this.meetingRequest, type));
			}
			return base.HasRecipients(type);
		}

		// Token: 0x040018BF RID: 6335
		private MeetingRequest meetingRequest;
	}
}
