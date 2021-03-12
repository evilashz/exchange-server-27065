using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000083 RID: 131
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ReplyCreation : ReplyForwardCommon
	{
		// Token: 0x06000987 RID: 2439 RVA: 0x00044CB2 File Offset: 0x00042EB2
		internal ReplyCreation(Item originalItem, Item newItem, ReplyForwardConfiguration parameters, bool isReplyAll, bool shouldUseSender, bool decodeSmime) : base(originalItem, newItem, parameters, decodeSmime)
		{
			this.Initialize(originalItem, isReplyAll, shouldUseSender);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00044CCC File Offset: 0x00042ECC
		protected override void BuildSubject()
		{
			if (this.parameters.SubjectPrefix != null)
			{
				this.newItem[InternalSchema.SubjectPrefix] = this.parameters.SubjectPrefix;
			}
			else
			{
				this.newItem[InternalSchema.SubjectPrefix] = ClientStrings.ItemReply.ToString(base.Culture);
			}
			this.newItem[InternalSchema.NormalizedSubject] = this.originalItem.GetValueOrDefault<string>(InternalSchema.NormalizedSubjectInternal, string.Empty);
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00044D4C File Offset: 0x00042F4C
		protected override void UpdateNewItemProperties()
		{
			base.UpdateNewItemProperties();
			if (this.originalItem is CalendarItemBase || this.originalItem is MeetingRequest || this.originalItem is MeetingCancellation)
			{
				this.newItem.SafeSetProperty(InternalSchema.IsReplyRequested, false);
				this.newItem.SafeSetProperty(InternalSchema.IsResponseRequested, false);
				ReplyCreation.UpdateMeetingReplyIconIndex(this.newItem);
			}
			if (!(this.newItem is PostItem))
			{
				this.BuildRecipients();
			}
			this.newItem.SafeSetProperty(InternalSchema.Importance, Importance.Normal);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00044DE6 File Offset: 0x00042FE6
		protected override void BuildAttachments(BodyConversionCallbacks callbacks, InboundConversionOptions optionsForSmime)
		{
			base.CopyAttachments(callbacks, this.originalItem.AttachmentCollection, this.newItem.AttachmentCollection, true, this.parameters.TargetFormat == BodyFormat.TextPlain, optionsForSmime);
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00044E18 File Offset: 0x00043018
		private static void UpdateMeetingReplyIconIndex(Item item)
		{
			IconIndex valueOrDefault = item.GetValueOrDefault<IconIndex>(InternalSchema.IconIndex, IconIndex.Default);
			if ((valueOrDefault & IconIndex.BaseAppointment) > (IconIndex)0)
			{
				item[InternalSchema.IconIndex] = IconIndex.Default;
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00044E50 File Offset: 0x00043050
		private void Initialize(Item originalItem, bool isReplyAll, bool shouldUseSender)
		{
			this.isReplyAll = isReplyAll;
			this.shouldUseSender = shouldUseSender;
			LastAction lastAction;
			if (isReplyAll)
			{
				lastAction = LastAction.ReplyToAll;
			}
			else
			{
				lastAction = LastAction.ReplyToSender;
			}
			IconIndex iconIndex = IconIndex.MailReplied;
			if (this.originalItemSigned)
			{
				iconIndex = IconIndex.MailSmimeSignedReplied;
			}
			else if (this.originalItemEncrypted)
			{
				iconIndex = IconIndex.MailEncryptedReplied;
			}
			else if (this.originalItemIrm)
			{
				iconIndex = IconIndex.MailIrmReplied;
			}
			if (originalItem.Id != null && originalItem.Id.ObjectId != null && !originalItem.Id.ObjectId.IsFakeId && !(originalItem is PostItem))
			{
				this.newItem.SafeSetProperty(InternalSchema.ReplyForwardStatus, ReplyForwardUtils.EncodeReplyForwardStatus(lastAction, iconIndex, originalItem.Id));
			}
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00044EF8 File Offset: 0x000430F8
		private void BuildRecipients()
		{
			bool useReplyTo = true;
			if (this is OofReplyCreation || this is RuleReplyCreation)
			{
				useReplyTo = false;
			}
			if (this.originalItem is MessageItem)
			{
				ReplyForwardCommon.BuildReplyRecipientsFromMessage(this.newItem as MessageItem, this.originalItem as MessageItem, this.isReplyAll, this.shouldUseSender, useReplyTo);
				return;
			}
			if (this.originalItem is CalendarItemBase)
			{
				this.BuildRecipientsFromCalendarItem();
				return;
			}
			if (this.originalItem is PostItem)
			{
				this.BuildRecipientsFromPostItem();
			}
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00044F78 File Offset: 0x00043178
		private void BuildRecipientsFromCalendarItem()
		{
			MessageItem messageItem = (MessageItem)this.newItem;
			CalendarItemBase calendarItemBase = (CalendarItemBase)this.originalItem;
			if (calendarItemBase.Organizer != null)
			{
				messageItem.Recipients.Add(calendarItemBase.Organizer, RecipientItemType.To);
			}
			if (!this.isReplyAll)
			{
				return;
			}
			MailboxSession mailboxSession = calendarItemBase.Session as MailboxSession;
			foreach (Attendee attendee in calendarItemBase.AttendeeCollection)
			{
				if (!messageItem.Recipients.Contains(attendee.Participant) && (mailboxSession == null || !Participant.HasSameEmail(attendee.Participant, new Participant(mailboxSession.MailboxOwner))))
				{
					if (attendee.AttendeeType == AttendeeType.Required)
					{
						messageItem.Recipients.Add(attendee.Participant, RecipientItemType.To);
					}
					else if (attendee.AttendeeType == AttendeeType.Optional)
					{
						messageItem.Recipients.Add(attendee.Participant, RecipientItemType.Cc);
					}
				}
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0004507C File Offset: 0x0004327C
		private void BuildRecipientsFromPostItem()
		{
			MessageItem messageItem = (MessageItem)this.newItem;
			PostItem postItem = (PostItem)this.originalItem;
			if (postItem.From != null)
			{
				messageItem.Recipients.Add(postItem.From, RecipientItemType.To);
			}
		}

		// Token: 0x0400027A RID: 634
		private bool isReplyAll;

		// Token: 0x0400027B RID: 635
		private bool shouldUseSender;
	}
}
