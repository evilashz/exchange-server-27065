using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200004D RID: 77
	internal abstract class XsoRecordedMessage : IRecordedMessage
	{
		// Token: 0x0600032A RID: 810 RVA: 0x0000F508 File Offset: 0x0000D708
		internal XsoRecordedMessage(BaseUMCallSession vo, UMSubscriber user, ActivityManager manager, bool autoPostSubmit)
		{
			this.session = vo;
			this.user = user;
			this.manager = manager;
			this.autoPostSubmit = autoPostSubmit;
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = user.CreateSessionLock())
			{
				if (XsoUtil.IsOverSendQuota(mailboxSessionLock.Session.Mailbox, 0UL))
				{
					throw new QuotaExceededException(LocalizedString.Empty);
				}
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000F57C File Offset: 0x0000D77C
		internal XsoRecordedMessage(BaseUMCallSession vo, UMSubscriber user, ActivityManager manager) : this(vo, user, manager, true)
		{
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000F588 File Offset: 0x0000D788
		protected virtual bool IsPureVoiceMessage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000F58B File Offset: 0x0000D78B
		protected virtual bool IsReplyToAProtectedMessage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000F58E File Offset: 0x0000D78E
		protected BaseUMCallSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000F596 File Offset: 0x0000D796
		protected ActivityManager Manager
		{
			get
			{
				return this.manager;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000F59E File Offset: 0x0000D79E
		protected UMSubscriber User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000F5A6 File Offset: 0x0000D7A6
		protected bool AutoPostSubmit
		{
			get
			{
				return this.autoPostSubmit;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000F5AE File Offset: 0x0000D7AE
		protected string AttachName
		{
			get
			{
				return this.attachName;
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000F5B6 File Offset: 0x0000D7B6
		public void DoSubmit(Importance importance)
		{
			this.DoSubmit(importance, false, null);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000F5C4 File Offset: 0x0000D7C4
		public virtual void DoSubmit(Importance importance, bool markPrivate, Stack<Participant> recips)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "RecordedMessage::Submit.", new object[0]);
			ExAssert.RetailAssert(!markPrivate || (markPrivate && this.IsPureVoiceMessage), "Only Pure voice messages can be marked private");
			RecordContext recordContext = this.Manager.RecordContext;
			this.DetermineIfMessageNeedsToBeProtected(markPrivate);
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
			{
				using (MessageItem messageItem = this.GenerateNewMessage(mailboxSessionLock.Session))
				{
					int num = 10000;
					if (recips != null)
					{
						while (recips.Count > 0 && --num > 0)
						{
							Participant participant = recips.Pop();
							messageItem.Recipients.Add(participant, RecipientItemType.To);
						}
					}
					messageItem.ExpandRecipientTable();
					messageItem[MessageItemSchema.VoiceMessageDuration] = recordContext.TotalSeconds;
					XsoUtil.SetSubscriberAccessSenderProperties(messageItem, this.User);
					messageItem.Importance = importance;
					if (markPrivate)
					{
						messageItem.Sensitivity = Sensitivity.Private;
					}
					string waveFilePath = null;
					if (recordContext.Recording != null && recordContext.TotalSeconds > 0)
					{
						waveFilePath = recordContext.Recording.FilePath;
					}
					UMMessageSubmission.SubmitXSOVoiceMail(this.session.CallId, this.session.CurrentCallContext.CallerId, this.User, waveFilePath, recordContext.TotalSeconds, this.User.DialPlan.AudioCodec, this.AttachName, this.User.TelephonyCulture, messageItem, null, this.session.CurrentCallContext.CallerIdDisplayName, this.session.CurrentCallContext.TenantGuid);
					this.Session.SetCounter(SubscriberAccessCounters.AverageSentVoiceMessageSize, XsoRecordedMessage.sizeAverage.Update((long)recordContext.TotalSeconds));
					this.Session.SetCounter(SubscriberAccessCounters.AverageRecentSentVoiceMessageSize, XsoRecordedMessage.sizeMovingAverage.Update((long)recordContext.TotalSeconds));
					if (this.AutoPostSubmit)
					{
						this.DoPostSubmit();
					}
				}
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000F7CC File Offset: 0x0000D9CC
		public virtual void DoPostSubmit()
		{
			this.Manager.RecordContext.Reset();
			this.Session.IncrementCounter(SubscriberAccessCounters.VoiceMessagesSent);
		}

		// Token: 0x06000336 RID: 822
		protected abstract MessageItem GenerateMessage(MailboxSession session);

		// Token: 0x06000337 RID: 823 RVA: 0x0000F7EE File Offset: 0x0000D9EE
		protected virtual MessageItem GenerateProtectedMessage(MailboxSession session)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000F7F5 File Offset: 0x0000D9F5
		protected virtual void AddRecordedMessageText(MessageContentBuilder content)
		{
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000F7F7 File Offset: 0x0000D9F7
		protected virtual void AddMessageHeader(Item originalMessage, MessageContentBuilder content)
		{
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000F7FC File Offset: 0x0000D9FC
		protected void SetAttachmentName(AttachmentCollection attachmentCollection)
		{
			this.attachName = Util.BuildAttachmentName(this.User.Extension, this.Manager.RecordContext.TotalSeconds, attachmentCollection, this.User.TelephonyCulture, this.User.DialPlan.AudioCodec, this.messageIsToBeProtected);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000F854 File Offset: 0x0000DA54
		protected string PrepareMessageBodyPrefix(Item originalMessage)
		{
			MessageContentBuilder messageContentBuilder = MessageContentBuilder.Create(this.User.TelephonyCulture);
			if (!string.IsNullOrEmpty(this.AttachName))
			{
				this.AddRecordedMessageText(messageContentBuilder);
			}
			this.AddMessageHeader(originalMessage, messageContentBuilder);
			return messageContentBuilder.ToString();
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000F894 File Offset: 0x0000DA94
		private MessageItem GenerateNewMessage(MailboxSession session)
		{
			if (this.messageIsToBeProtected)
			{
				return this.GenerateProtectedMessage(session);
			}
			return this.GenerateMessage(session);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000F8B0 File Offset: 0x0000DAB0
		private void DetermineIfMessageNeedsToBeProtected(bool markPrivate)
		{
			if (this.IsReplyToAProtectedMessage)
			{
				this.messageIsToBeProtected = true;
				return;
			}
			if (!this.IsPureVoiceMessage)
			{
				this.messageIsToBeProtected = false;
				return;
			}
			if ((markPrivate && this.User.DRMPolicyForInterpersonal == DRMProtectionOptions.Private) || this.User.DRMPolicyForInterpersonal == DRMProtectionOptions.All)
			{
				this.messageIsToBeProtected = true;
				return;
			}
			this.messageIsToBeProtected = false;
		}

		// Token: 0x040000FC RID: 252
		private static MovingAverage sizeMovingAverage = new MovingAverage(50);

		// Token: 0x040000FD RID: 253
		private static Average sizeAverage = new Average();

		// Token: 0x040000FE RID: 254
		private BaseUMCallSession session;

		// Token: 0x040000FF RID: 255
		private ActivityManager manager;

		// Token: 0x04000100 RID: 256
		private UMSubscriber user;

		// Token: 0x04000101 RID: 257
		private bool autoPostSubmit;

		// Token: 0x04000102 RID: 258
		private string attachName;

		// Token: 0x04000103 RID: 259
		private bool messageIsToBeProtected;
	}
}
