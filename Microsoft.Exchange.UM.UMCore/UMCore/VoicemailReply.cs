using System;
using System.IO;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000239 RID: 569
	internal class VoicemailReply : ReplyForwardBaseClass
	{
		// Token: 0x060010A4 RID: 4260 RVA: 0x0004AC14 File Offset: 0x00048E14
		internal VoicemailReply(BaseUMCallSession vo, StoreObjectId originalId, UMSubscriber user, ContactInfo sender, RetrieveVoicemailManager context, bool isOriginalMessageIsProtected) : base(vo, user, context)
		{
			this.originalId = originalId;
			this.originalMessageProtected = isOriginalMessageIsProtected;
			if (sender.ADOrgPerson == null || string.IsNullOrEmpty(sender.ADOrgPerson.LegacyExchangeDN))
			{
				this.sender = new Participant(sender.DisplayName, sender.EMailAddress, null);
				return;
			}
			this.sender = new Participant(sender.DisplayName, sender.ADOrgPerson.LegacyExchangeDN, "EX");
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x0004AC94 File Offset: 0x00048E94
		protected override bool IsReplyToAProtectedMessage
		{
			get
			{
				return this.originalMessageProtected;
			}
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0004AC9C File Offset: 0x00048E9C
		public override void DoPostSubmit()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "VoicemailReply::DoPostSubmit.", new object[0]);
			base.Session.IncrementCounter(SubscriberAccessCounters.ReplyMessagesSent);
			base.DoPostSubmit();
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0004ACCC File Offset: 0x00048ECC
		protected virtual MessageItem InternalCreateReply(MessageItem original, MailboxSession session, ReplyForwardConfiguration replyConfiguration)
		{
			MessageItem result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				MessageItem messageItem = original.CreateReply(XsoUtil.GetDraftsFolderId(session), replyConfiguration);
				disposeGuard.Add<MessageItem>(messageItem);
				messageItem.Recipients.Clear();
				messageItem.Recipients.Add(this.sender);
				disposeGuard.Success();
				result = messageItem;
			}
			return result;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0004AD40 File Offset: 0x00048F40
		protected override MessageItem GenerateMessage(MailboxSession session)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "VoicemailReply::GenerateMessage.", new object[0]);
			MessageItem result;
			using (MessageItem messageItem = MessageItem.Bind(session, this.originalId))
			{
				base.SetAttachmentName(messageItem.AttachmentCollection);
				MessageItem messageItem2 = this.CreateReply(messageItem, session);
				this.CopyOverAttachments(messageItem2.AttachmentCollection, messageItem.AttachmentCollection);
				result = messageItem2;
			}
			return result;
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0004ADB8 File Offset: 0x00048FB8
		protected override MessageItem GenerateProtectedMessage(MailboxSession session)
		{
			if (this.IsReplyToAProtectedMessage)
			{
				using (RightsManagedMessageItem rightsManagedMessageItem = RightsManagedMessageItem.Bind(session, this.originalId, XsoUtil.GetOutboundConversionOptions(base.User)))
				{
					base.SetAttachmentName(rightsManagedMessageItem.ProtectedAttachmentCollection);
					RightsManagedMessageItem rightsManagedMessageItem2 = (RightsManagedMessageItem)this.CreateReply(rightsManagedMessageItem, session);
					this.CopyOverAttachments(rightsManagedMessageItem2.ProtectedAttachmentCollection, rightsManagedMessageItem.ProtectedAttachmentCollection);
					return rightsManagedMessageItem2;
				}
			}
			return base.GenerateProtectedMessage(session);
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0004AE38 File Offset: 0x00049038
		protected override void AddRecordedMessageText(MessageContentBuilder content)
		{
			content.AddRecordedReplyText(base.User.DisplayName);
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0004AE4C File Offset: 0x0004904C
		private void CopyOverAttachments(AttachmentCollection destAttachCollection, AttachmentCollection originalAttachCollection)
		{
			foreach (AttachmentHandle handle in originalAttachCollection)
			{
				using (Attachment attachment = originalAttachCollection.Open(handle))
				{
					StreamAttachment streamAttachment = attachment as StreamAttachment;
					if (streamAttachment != null)
					{
						Stream stream;
						if (XsoUtil.IsValidProtectedAudioAttachment(streamAttachment))
						{
							stream = DRMUtils.OpenProtectedAttachment(streamAttachment, base.User.ADUser.OrganizationId);
						}
						else
						{
							if (!XsoUtil.IsValidAudioAttachment(streamAttachment))
							{
								continue;
							}
							stream = streamAttachment.GetContentStream();
						}
						using (StreamAttachment streamAttachment2 = (StreamAttachment)destAttachCollection.Create(AttachmentType.Stream))
						{
							using (Stream contentStream = streamAttachment2.GetContentStream())
							{
								using (DisposeGuard disposeGuard = default(DisposeGuard))
								{
									disposeGuard.Add<Stream>(stream);
									CommonUtil.CopyStream(stream, contentStream);
									streamAttachment2.FileName = streamAttachment.FileName;
									streamAttachment2.ContentType = streamAttachment.ContentType;
									streamAttachment2.Save();
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0004AFA0 File Offset: 0x000491A0
		private MessageItem CreateReply(MessageItem original, MailboxSession session)
		{
			ReplyForwardConfiguration replyForwardConfiguration = new ReplyForwardConfiguration(BodyFormat.TextHtml);
			replyForwardConfiguration.AddBodyPrefix(base.PrepareMessageBodyPrefix(original));
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(base.User.ADUser.OrganizationId);
			AcceptedDomain defaultAcceptedDomain = iadsystemConfigurationLookup.GetDefaultAcceptedDomain();
			replyForwardConfiguration.ConversionOptionsForSmime = new InboundConversionOptions(defaultAcceptedDomain.DomainName.ToString());
			replyForwardConfiguration.ConversionOptionsForSmime.UserADSession = ADRecipientLookupFactory.CreateFromUmUser(base.User).ScopedRecipientSession;
			MessageItem result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				MessageItem messageItem = this.InternalCreateReply(original, session, replyForwardConfiguration);
				disposeGuard.Add<MessageItem>(messageItem);
				messageItem.ClassName = "IPM.Note.Microsoft.Voicemail.UM";
				messageItem[MessageItemSchema.VoiceMessageAttachmentOrder] = XsoUtil.GetAttachmentOrderString(original);
				disposeGuard.Success();
				result = messageItem;
			}
			return result;
		}

		// Token: 0x04000B9A RID: 2970
		private StoreObjectId originalId;

		// Token: 0x04000B9B RID: 2971
		private Participant sender;

		// Token: 0x04000B9C RID: 2972
		private bool originalMessageProtected;
	}
}
