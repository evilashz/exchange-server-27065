using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.MessageContent;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001EB RID: 491
	internal class SubmitMessageManager : SendMessageManager
	{
		// Token: 0x06000E7E RID: 3710 RVA: 0x0004101E File Offset: 0x0003F21E
		internal SubmitMessageManager(ActivityManager manager, SubmitMessageManager.ConfigClass config) : base(manager, config)
		{
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x00041028 File Offset: 0x0003F228
		internal bool DrmIsEnabled
		{
			get
			{
				return this.drmIsEnabled;
			}
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00041030 File Offset: 0x0003F230
		internal override void Start(BaseUMCallSession vo, string refInfo)
		{
			this.drmIsEnabled = (vo.CurrentCallContext.CallerInfo != null && vo.CurrentCallContext.CallerInfo.DRMPolicyForInterpersonal != DRMProtectionOptions.None);
			base.Start(vo, refInfo);
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00041068 File Offset: 0x0003F268
		internal override TransitionBase ExecuteAction(string action, BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "SubmitMessageManager asked to do action: {0}.", new object[]
			{
				action
			});
			return base.ExecuteAction(action, vo);
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0004109C File Offset: 0x0003F29C
		internal override void DropCall(BaseUMCallSession vo, DropCallReason reason)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "SubmitMessageManager::DropCall.", new object[0]);
			if (base.CurrentActivity is Record)
			{
				UMSubscriber callerInfo = vo.CurrentCallContext.CallerInfo;
				if (base.RecordContext.TotalSeconds > 0)
				{
					this.SendMessage(vo);
				}
			}
			base.DropCall(vo, reason);
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x000410F6 File Offset: 0x0003F2F6
		protected override string SendMessage(BaseUMCallSession vo)
		{
			base.SendMsg = new SubmitMessageManager.SearchRecordedMessage(vo, vo.CurrentCallContext.CallerInfo, this);
			return base.SendMessage(vo);
		}

		// Token: 0x04000AF6 RID: 2806
		private bool drmIsEnabled;

		// Token: 0x020001EC RID: 492
		internal class ConfigClass : ActivityManagerConfig
		{
			// Token: 0x06000E84 RID: 3716 RVA: 0x00041117 File Offset: 0x0003F317
			public ConfigClass(ActivityManagerConfig manager) : base(manager)
			{
			}

			// Token: 0x06000E85 RID: 3717 RVA: 0x00041120 File Offset: 0x0003F320
			internal override ActivityManager CreateActivityManager(ActivityManager manager)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Constructing Submit Message activity manager.", new object[0]);
				return new SubmitMessageManager(manager, this);
			}
		}

		// Token: 0x020001ED RID: 493
		internal class SearchRecordedMessage : XsoRecordedMessage
		{
			// Token: 0x06000E86 RID: 3718 RVA: 0x0004113F File Offset: 0x0003F33F
			internal SearchRecordedMessage(BaseUMCallSession vo, UMSubscriber user, ActivityManager manager) : base(vo, user, manager, true)
			{
			}

			// Token: 0x17000390 RID: 912
			// (get) Token: 0x06000E87 RID: 3719 RVA: 0x0004114B File Offset: 0x0003F34B
			protected override bool IsPureVoiceMessage
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000E88 RID: 3720 RVA: 0x00041150 File Offset: 0x0003F350
			protected override MessageItem GenerateMessage(MailboxSession session)
			{
				MessageItem result;
				using (Folder folder = Folder.Bind(session, XsoUtil.GetDraftsFolderId(session)))
				{
					string text = Strings.VoiceMessageSubject(Util.BuildDurationString(base.Manager.RecordContext.TotalSeconds, base.User.TelephonyCulture)).ToString(base.User.TelephonyCulture);
					CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "RecordedMessage::GenerateMessage creating message with subject={0}.", new object[]
					{
						text
					});
					MessageItem messageItem = MessageItem.Create(session, folder.Id);
					messageItem.Subject = text;
					base.SetAttachmentName(null);
					messageItem.ClassName = "IPM.Note.Microsoft.Voicemail.UM";
					string s = this.PrepareMessageBody();
					using (Stream stream = messageItem.Body.OpenWriteStream(new BodyWriteConfiguration(BodyFormat.TextHtml, Charset.UTF8.Name)))
					{
						byte[] bytes = Encoding.UTF8.GetBytes(s);
						stream.Write(bytes, 0, bytes.Length);
					}
					result = messageItem;
				}
				return result;
			}

			// Token: 0x06000E89 RID: 3721 RVA: 0x00041264 File Offset: 0x0003F464
			protected override MessageItem GenerateProtectedMessage(MailboxSession session)
			{
				return this.GenerateMessage(session);
			}

			// Token: 0x06000E8A RID: 3722 RVA: 0x00041270 File Offset: 0x0003F470
			private string PrepareMessageBody()
			{
				MessageContentBuilder messageContentBuilder = MessageContentBuilder.Create(base.User.TelephonyCulture);
				messageContentBuilder.AddVoicemailBody(new ADContactInfo((IADOrgPerson)base.User.ADRecipient, FoundByType.NotSpecified), null);
				return messageContentBuilder.ToString();
			}
		}
	}
}
