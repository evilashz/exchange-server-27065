using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000238 RID: 568
	internal class VoicemailForward : ReplyForwardBaseClass
	{
		// Token: 0x060010A0 RID: 4256 RVA: 0x0004AACF File Offset: 0x00048CCF
		internal VoicemailForward(BaseUMCallSession vo, StoreObjectId originalId, UMSubscriber user, RetrieveVoicemailManager context) : base(vo, user, context)
		{
			this.originalId = originalId;
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0004AAE2 File Offset: 0x00048CE2
		public override void DoPostSubmit()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "VoicemailForward::DoPostSubmit", new object[0]);
			base.Session.IncrementCounter(SubscriberAccessCounters.ForwardMessagesSent);
			base.DoPostSubmit();
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0004AB10 File Offset: 0x00048D10
		protected override MessageItem GenerateMessage(MailboxSession session)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "VoicemailForward::GenerateMessage.", new object[0]);
			MessageItem result;
			using (MessageItem messageItem = MessageItem.Bind(session, this.originalId))
			{
				base.SetAttachmentName(messageItem.AttachmentCollection);
				ReplyForwardConfiguration replyForwardConfiguration = new ReplyForwardConfiguration(BodyFormat.TextHtml);
				replyForwardConfiguration.AddBodyPrefix(base.PrepareMessageBodyPrefix(messageItem));
				IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(base.User.ADUser.OrganizationId);
				AcceptedDomain defaultAcceptedDomain = iadsystemConfigurationLookup.GetDefaultAcceptedDomain();
				replyForwardConfiguration.ConversionOptionsForSmime = new InboundConversionOptions(defaultAcceptedDomain.DomainName.ToString());
				replyForwardConfiguration.ConversionOptionsForSmime.UserADSession = ADRecipientLookupFactory.CreateFromUmUser(base.User).ScopedRecipientSession;
				MessageItem messageItem2 = messageItem.CreateForward(XsoUtil.GetDraftsFolderId(session), replyForwardConfiguration);
				messageItem2.ClassName = "IPM.Note.Microsoft.Voicemail.UM";
				messageItem2[MessageItemSchema.VoiceMessageAttachmentOrder] = XsoUtil.GetAttachmentOrderString(messageItem);
				result = messageItem2;
			}
			return result;
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0004AC00 File Offset: 0x00048E00
		protected override void AddRecordedMessageText(MessageContentBuilder content)
		{
			content.AddRecordedForwardText(base.User.DisplayName);
		}

		// Token: 0x04000B99 RID: 2969
		private StoreObjectId originalId;
	}
}
