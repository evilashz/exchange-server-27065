using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000046 RID: 70
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class WelcomeToGroupMessageComposer : BaseGroupMessageComposer
	{
		// Token: 0x06000211 RID: 529 RVA: 0x0000E180 File Offset: 0x0000C380
		public WelcomeToGroupMessageComposer(WelcomeToGroupMessageTemplate template, ADUser recipient, ADUser group)
		{
			ArgumentValidator.ThrowIfNull("template", template);
			ArgumentValidator.ThrowIfNull("recipient", recipient);
			this.template = template;
			this.recipient = recipient;
			this.preferredCulture = BaseGroupMessageComposer.GetPreferredCulture(new ADUser[]
			{
				recipient,
				group
			});
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000E1D4 File Offset: 0x0000C3D4
		protected override ADUser[] Recipients
		{
			get
			{
				return new ADUser[]
				{
					this.recipient
				};
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000E1F2 File Offset: 0x0000C3F2
		protected override Participant FromParticipant
		{
			get
			{
				return this.template.EmailFrom;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000E1FF File Offset: 0x0000C3FF
		protected override string Subject
		{
			get
			{
				return this.GetSubject();
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000E207 File Offset: 0x0000C407
		private bool AddedByRecipient
		{
			get
			{
				return this.template.ExecutingUser == null || this.template.ExecutingUser.Id.Equals(this.recipient.Id);
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000E238 File Offset: 0x0000C438
		protected override void SetAdditionalMessageProperties(MessageItem message)
		{
			ArgumentValidator.ThrowIfNull("message", message);
			message[StoreObjectSchema.ItemClass] = "IPM.Note.GroupMailbox.WelcomeEmail";
			if (this.template.EmailSender != null)
			{
				BaseGroupMessageComposer.Tracer.TraceDebug<string>((long)this.GetHashCode(), "WelcomeToGroupMessageComposer.SetAdditionalMessageProperties: Setting message sender to: {0}", this.template.EmailSender.DisplayName);
				message.Sender = this.template.EmailSender;
				return;
			}
			BaseGroupMessageComposer.Tracer.TraceDebug((long)this.GetHashCode(), "WelcomeToGroupMessageComposer.SetAdditionalMessageProperties: Skipping setting email sender.");
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000E2C4 File Offset: 0x0000C4C4
		protected override void WriteMessageBody(StreamWriter streamWriter)
		{
			ArgumentValidator.ThrowIfNull("streamWriter", streamWriter);
			WelcomeMessageBodyData data = new WelcomeMessageBodyData(this.template, this.GetJoinedByMessage(), this.recipient.RecipientType == RecipientType.MailUser, !this.AddedByRecipient, this.preferredCulture);
			WelcomeMessageBodyWriter.WriteTemplate(streamWriter, data);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000E314 File Offset: 0x0000C514
		protected override void AddAttachments(MessageItem message)
		{
			ArgumentValidator.ThrowIfNull("message", message);
			WelcomeMessageBodyData.WelcomeConversationsIcon.AddImageAsAttachment(message);
			WelcomeMessageBodyData.BlankGifImage.AddImageAsAttachment(message);
			if (!this.template.GroupIsAutoSubscribe)
			{
				if (this.preferredCulture.TextInfo.IsRightToLeft)
				{
					WelcomeMessageBodyData.WelcomeArrowFlippedIcon.AddImageAsAttachment(message);
				}
				else
				{
					WelcomeMessageBodyData.WelcomeArrowIcon.AddImageAsAttachment(message);
				}
			}
			if (this.recipient.RecipientType != RecipientType.MailUser)
			{
				WelcomeMessageBodyData.WelcomeO365Icon.AddImageAsAttachment(message);
			}
			if (!string.IsNullOrEmpty(this.template.GroupSharePointUrl))
			{
				WelcomeMessageBodyData.WelcomeDocumentIcon.AddImageAsAttachment(message);
			}
			if (this.template.GroupHasPhoto)
			{
				this.template.GroupPhoto.AddImageAsAttachment(message);
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000E3D0 File Offset: 0x0000C5D0
		private string GetJoinedByMessage()
		{
			LocalizedString localizedString = LocalizedString.Empty;
			if (this.AddedByRecipient)
			{
				BaseGroupMessageComposer.Tracer.TraceDebug((long)this.GetHashCode(), "WelcomeToGroupMessageComposer.GetJoinedByMessage: executingUser is unknown or by user himself.");
				localizedString = ClientStrings.GroupMailboxWelcomeEmailSecondaryHeaderYouJoined(this.template.EncodedGroupDisplayName);
			}
			else
			{
				BaseGroupMessageComposer.Tracer.TraceDebug<ADObjectId, ADObjectId>((long)this.GetHashCode(), "WelcomeToGroupMessageComposer.GetJoinedByMessage: executingUser is different than the one joining the group, returning message header for added member. ExecutingUser.AdObjectId: {0}, NewMember.AdObjectId: {1}.", this.template.ExecutingUser.Id, this.recipient.Id);
				localizedString = ClientStrings.GroupMailboxWelcomeEmailSecondaryHeaderAddedBy(this.template.EncodedExecutingUserDisplayName, this.template.EncodedGroupDisplayName);
			}
			return localizedString.ToString(this.preferredCulture);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000E470 File Offset: 0x0000C670
		private string GetSubject()
		{
			LocalizedString localizedString;
			if (this.template.ExecutingUser == null)
			{
				BaseGroupMessageComposer.Tracer.TraceDebug((long)this.GetHashCode(), "WelcomeToGroupMessageComposer.GetSubject: executingUser is unknown, returning subject without JoinedBy.");
				localizedString = ClientStrings.GroupMailboxAddedMemberNoJoinedBySubject(this.template.Group.DisplayName);
			}
			else if (this.template.ExecutingUser.Id.Equals(this.recipient.Id))
			{
				BaseGroupMessageComposer.Tracer.TraceDebug<ADObjectId, ADObjectId>((long)this.GetHashCode(), "WelcomeToGroupMessageComposer.GetSubject: executingUser is same as user joining the group, returning subject for self-join. ExecutingUser.AdObjectId: {0}, NewMember.AdObjectId: {1}.", this.template.ExecutingUser.Id, this.recipient.Id);
				localizedString = ClientStrings.GroupMailboxAddedSelfMessageSubject(this.template.Group.DisplayName);
			}
			else
			{
				BaseGroupMessageComposer.Tracer.TraceDebug<ADObjectId, ADObjectId>((long)this.GetHashCode(), "WelcomeToGroupMessageComposer.GetSubject: executingUser is different than the one joining the group, returning subject for added member. ExecutingUser.AdObjectId: {0}, NewMember.AdObjectId: {1}.", this.template.ExecutingUser.Id, this.recipient.Id);
				localizedString = ClientStrings.GroupMailboxAddedMemberMessageSubject(this.template.Group.DisplayName);
			}
			return localizedString.ToString(this.preferredCulture);
		}

		// Token: 0x04000125 RID: 293
		private readonly WelcomeToGroupMessageTemplate template;

		// Token: 0x04000126 RID: 294
		private readonly ADUser recipient;

		// Token: 0x04000127 RID: 295
		private readonly CultureInfo preferredCulture;
	}
}
