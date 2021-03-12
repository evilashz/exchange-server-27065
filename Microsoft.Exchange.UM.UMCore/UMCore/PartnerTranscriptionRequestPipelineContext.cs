using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002C9 RID: 713
	internal class PartnerTranscriptionRequestPipelineContext : VoiceMessagePipelineContextBase, IUMCAMessage
	{
		// Token: 0x060015A9 RID: 5545 RVA: 0x0005C50C File Offset: 0x0005A70C
		internal PartnerTranscriptionRequestPipelineContext(SubmissionHelper helper) : base(helper)
		{
			bool flag = false;
			try
			{
				base.MessageType = "PartnerTranscriptionRequest";
				if (!helper.CustomHeaders.Contains("PartnerTranscriptionContext"))
				{
					throw new HeaderFileArgumentInvalidException("PartnerTranscriptionContext");
				}
				string base64Data = helper.CustomHeaders["PartnerTranscriptionContext"] as string;
				this.partnerContext = UMPartnerContext.Parse<UMPartnerTranscriptionContext>(base64Data);
				if (!helper.CustomHeaders.Contains("SenderObjectGuid"))
				{
					throw new HeaderFileArgumentInvalidException("SenderObjectGuid");
				}
				Guid objectGuid = new Guid(helper.CustomHeaders["SenderObjectGuid"] as string);
				this.subscriber = (UMSubscriber)base.CreateRecipientFromObjectGuid(objectGuid, helper.TenantGuid);
				if (helper.CustomHeaders.Contains("MessageFilePath"))
				{
					this.interpersonalMessagePath = (helper.CustomHeaders["MessageFilePath"] as string);
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.Dispose();
				}
			}
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x0005C608 File Offset: 0x0005A808
		internal PartnerTranscriptionRequestPipelineContext(SubmissionHelper helper, UMSubscriber subscriber, UMSubscriber caller, string waveFilePath, MessageItem ipmMessage, string ipmAttachName, bool isCallAnswering, AudioCodecEnum codec, bool isImportant, string subject, int duration) : base(AudioCodecEnum.G711, helper, waveFilePath, duration)
		{
			FileInfo fileInfo = new FileInfo(waveFilePath);
			base.MessageType = "PartnerTranscriptionRequest";
			this.subscriber = subscriber;
			this.subscriber.AddReference();
			this.interpersonalMessage = ipmMessage;
			UMMailboxPolicy ummailboxPolicy = this.subscriber.UMMailboxPolicy;
			this.partnerContext = UMPartnerContext.Create<UMPartnerTranscriptionContext>();
			this.partnerContext.IpmAttachmentName = ipmAttachName;
			this.partnerContext.IsCallAnsweringMessage = isCallAnswering;
			this.partnerContext.IsImportant = isImportant;
			this.partnerContext.CallId = helper.CallId;
			this.partnerContext.SessionId = helper.CallId;
			this.partnerContext.Subject = subject;
			this.partnerContext.Duration = duration;
			this.partnerContext.Culture = new CultureInfo(helper.CultureInfo);
			this.partnerContext.CreationTime = new ExDateTime(ExTimeZone.UtcTimeZone, fileInfo.CreationTimeUtc);
			this.partnerContext.CallingParty = helper.CallerId.ToDial;
			this.partnerContext.AudioCodec = codec.ToString();
			this.partnerContext.PartnerAddress = ummailboxPolicy.VoiceMailPreviewPartnerAddress.Value;
			this.partnerContext.PartnerMaxDeliveryDelay = ummailboxPolicy.VoiceMailPreviewPartnerMaxDeliveryDelay;
			this.partnerContext.CallerGuid = ((caller != null) ? caller.ADRecipient.Guid : Guid.Empty);
			this.partnerContext.CallerName = ((caller != null) ? caller.DisplayName : string.Empty);
			this.partnerContext.CallerIdDisplayName = helper.CallerIdDisplayName;
			this.partnerContext.UMDialPlanLanguage = this.subscriber.DialPlan.DefaultLanguage.Culture.Name;
			this.partnerContext.CallerInformedOfAnalysis = (ummailboxPolicy.InformCallerOfVoiceMailAnalysis ? "true" : "false");
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x0005C7E0 File Offset: 0x0005A9E0
		public UMRecipient CAMessageRecipient
		{
			get
			{
				return this.subscriber;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x0005C7E8 File Offset: 0x0005A9E8
		public bool CollectMessageForAnalysis
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x0005C7EB File Offset: 0x0005A9EB
		internal override Pipeline Pipeline
		{
			get
			{
				return PartnerTranscriptionRequestPipeline.Instance;
			}
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x0005C7F4 File Offset: 0x0005A9F4
		public override void PostCompletion()
		{
			if (!string.IsNullOrEmpty(this.interpersonalMessagePath))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "PartnerTranscriptionRequestPipelineContext.PostCompletion - Removing msg file '{0}'", new object[]
				{
					this.interpersonalMessagePath
				});
				Util.TryDeleteFile(this.interpersonalMessagePath);
				this.interpersonalMessagePath = null;
			}
			base.PostCompletion();
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x0005C852 File Offset: 0x0005AA52
		public override string GetMailboxServerId()
		{
			return base.GetMailboxServerIdHelper();
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x0005C85A File Offset: 0x0005AA5A
		public override string GetRecipientIdForThrottling()
		{
			return base.GetRecipientIdHelper();
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x0005C864 File Offset: 0x0005AA64
		internal override void WriteCustomHeaderFields(StreamWriter headerStream)
		{
			base.WriteCustomHeaderFields(headerStream);
			headerStream.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", new object[]
			{
				"PartnerTranscriptionContext",
				" : ",
				this.partnerContext
			}));
			headerStream.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", new object[]
			{
				"SenderObjectGuid",
				" : ",
				this.subscriber.ADRecipient.Guid
			}));
			if (!string.IsNullOrEmpty(this.interpersonalMessagePath))
			{
				headerStream.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", new object[]
				{
					"MessageFilePath",
					" : ",
					this.interpersonalMessagePath
				}));
			}
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x0005C938 File Offset: 0x0005AB38
		internal override void SaveMessage()
		{
			if (this.interpersonalMessage != null)
			{
				this.interpersonalMessagePath = Path.Combine(Path.GetDirectoryName(base.HeaderFileName), Path.GetFileNameWithoutExtension(base.HeaderFileName) + ".msg");
				XSOVoiceMessagePipelineContext.SaveAndDeleteMessageItem(this.interpersonalMessage, this.interpersonalMessagePath, this.subscriber);
				this.interpersonalMessage = null;
			}
			base.SaveMessage();
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x0005C99C File Offset: 0x0005AB9C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PartnerTranscriptionRequestPipelineContext>(this);
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x0005C9A4 File Offset: 0x0005ABA4
		protected override void SetMessageProperties()
		{
			base.SetMessageProperties();
			this.partnerContext.PcmAudioAttachmentName = new FileInfo(base.FileToCompressPath).Name;
			this.partnerContext.PartnerAudioAttachmentName = new FileInfo(base.CompressedAudioFile.FilePath).Name;
			using (FileStream fileStream = new FileStream(base.CompressedAudioFile.FilePath, FileMode.Open, FileAccess.Read))
			{
				XsoUtil.AddAttachment(base.MessageToSubmit.AttachmentCollection, fileStream, this.partnerContext.PartnerAudioAttachmentName, "audio/wav");
			}
			using (FileStream fileStream2 = new FileStream(base.FileToCompressPath, FileMode.Open, FileAccess.Read))
			{
				XsoUtil.AddAttachment(base.MessageToSubmit.AttachmentCollection, fileStream2, this.partnerContext.PcmAudioAttachmentName, "audio/wav");
			}
			if (this.interpersonalMessagePath != null)
			{
				using (FileStream fileStream3 = new FileStream(this.interpersonalMessagePath, FileMode.Open, FileAccess.Read))
				{
					XsoUtil.AddAttachment(base.MessageToSubmit.AttachmentCollection, fileStream3, this.partnerContext.IpmAttachmentName, "application/octet-stream");
				}
			}
			UMMailboxPolicy ummailboxPolicy = this.subscriber.UMMailboxPolicy;
			base.MessageToSubmit.ClassName = "IPM.Note.Microsoft.Partner.UM.TranscriptionRequest";
			base.MessageToSubmit.Subject = base.MessageToSubmit.ClassName;
			base.MessageToSubmit[MessageItemSchema.XMsExchangeUMPartnerContext] = this.partnerContext.ToString();
			base.MessageToSubmit[MessageItemSchema.XMsExchangeUMPartnerContent] = "voice";
			base.MessageToSubmit[MessageItemSchema.XMsExchangeUMPartnerAssignedID] = ummailboxPolicy.VoiceMailPreviewPartnerAssignedID;
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x0005CB50 File Offset: 0x0005AD50
		protected override void InternalDispose(bool disposing)
		{
			try
			{
				if (disposing && this.subscriber != null)
				{
					this.subscriber.ReleaseReference();
				}
			}
			finally
			{
				base.InternalDispose(disposing);
			}
		}

		// Token: 0x04000CE8 RID: 3304
		private UMPartnerTranscriptionContext partnerContext;

		// Token: 0x04000CE9 RID: 3305
		private UMSubscriber subscriber;

		// Token: 0x04000CEA RID: 3306
		private MessageItem interpersonalMessage;

		// Token: 0x04000CEB RID: 3307
		private string interpersonalMessagePath;
	}
}
