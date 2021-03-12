using System;
using System.IO;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002C2 RID: 706
	internal class MissedCallPipelineContext : PipelineContext, IUMCAMessage, IUMResolveCaller
	{
		// Token: 0x06001560 RID: 5472 RVA: 0x0005B790 File Offset: 0x00059990
		internal MissedCallPipelineContext(SubmissionHelper helper) : base(helper)
		{
			bool flag = false;
			try
			{
				base.MessageType = "MissedCall";
				if (helper.CustomHeaders.Contains("Important"))
				{
					this.important = bool.Parse((string)helper.CustomHeaders["Important"]);
				}
				if (helper.CustomHeaders.Contains("Subject"))
				{
					this.subject = (string)helper.CustomHeaders["Subject"];
				}
				this.recipient = base.CreateRecipientFromObjectGuid(helper.RecipientObjectGuid, helper.TenantGuid);
				this.recipientDialPlan = base.InitializeCallerIdAndTryGetDialPlan(this.recipient);
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

		// Token: 0x06001561 RID: 5473 RVA: 0x0005B858 File Offset: 0x00059A58
		internal MissedCallPipelineContext(SubmissionHelper helper, bool important, string subject, UMRecipient recipient) : base(helper)
		{
			base.MessageType = "MissedCall";
			this.important = important;
			this.subject = subject;
			this.recipient = recipient;
			this.recipient.AddReference();
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0005B890 File Offset: 0x00059A90
		internal MissedCallPipelineContext(SubmissionHelper helper, bool important, string subject, UMRecipient recipient, string messageID, ExDateTime sentTime) : base(helper)
		{
			base.MessageType = "MissedCall";
			this.important = important;
			this.subject = subject;
			this.recipient = recipient;
			this.recipient.AddReference();
			base.MessageID = messageID;
			base.SentTime = sentTime;
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x0005B8E0 File Offset: 0x00059AE0
		public UMRecipient CAMessageRecipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x0005B8E8 File Offset: 0x00059AE8
		public bool CollectMessageForAnalysis
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x0005B8EB File Offset: 0x00059AEB
		// (set) Token: 0x06001566 RID: 5478 RVA: 0x0005B8F3 File Offset: 0x00059AF3
		public ContactInfo ContactInfo
		{
			get
			{
				return this.contactInfo;
			}
			set
			{
				this.contactInfo = value;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x0005B8FC File Offset: 0x00059AFC
		internal override Pipeline Pipeline
		{
			get
			{
				return TextPipeline.Instance;
			}
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x0005B903 File Offset: 0x00059B03
		public override void PostCompletion()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "MissedCallPipelineContext.PostCompletion - Incrementing missed calls counter", new object[0]);
			Util.IncrementCounter(CallAnswerCounters.CallAnsweringMissedCalls);
			base.PostCompletion();
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0005B935 File Offset: 0x00059B35
		public override string GetMailboxServerId()
		{
			return base.GetMailboxServerIdHelper();
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0005B93D File Offset: 0x00059B3D
		public override string GetRecipientIdForThrottling()
		{
			return base.GetRecipientIdHelper();
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x0005B945 File Offset: 0x00059B45
		internal override void WriteCustomHeaderFields(StreamWriter headerStream)
		{
			if (this.subject != null)
			{
				headerStream.WriteLine("Subject : " + this.subject);
			}
			headerStream.WriteLine("Important : " + this.important.ToString());
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x0005B980 File Offset: 0x00059B80
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MissedCallPipelineContext>(this);
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0005B988 File Offset: 0x00059B88
		protected override void SetMessageProperties()
		{
			base.SetMessageProperties();
			MessageContentBuilder messageContentBuilder = MessageContentBuilder.Create(base.CultureInfo, this.recipientDialPlan);
			base.MessageToSubmit.Subject = messageContentBuilder.GetMissedCallSubject(this.subject);
			messageContentBuilder.AddMissedCallBody(base.CallerId, this.ContactInfo);
			using (MemoryStream memoryStream = messageContentBuilder.ToStream())
			{
				using (Stream stream = base.MessageToSubmit.Body.OpenWriteStream(new BodyWriteConfiguration(BodyFormat.TextHtml, Charset.UTF8.Name)))
				{
					memoryStream.WriteTo(stream);
				}
			}
			if (this.important)
			{
				base.MessageToSubmit.Importance = Importance.High;
			}
			base.MessageToSubmit.ClassName = "IPM.Note.Microsoft.Missed.Voice";
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0005BA60 File Offset: 0x00059C60
		protected override void InternalDispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "MissedCallPipelineContext.Dispose() called", new object[0]);
					if (this.recipient != null)
					{
						this.recipient.ReleaseReference();
					}
				}
			}
			finally
			{
				base.InternalDispose(disposing);
			}
		}

		// Token: 0x04000CD5 RID: 3285
		private bool important;

		// Token: 0x04000CD6 RID: 3286
		private string subject;

		// Token: 0x04000CD7 RID: 3287
		private UMRecipient recipient;

		// Token: 0x04000CD8 RID: 3288
		private UMDialPlan recipientDialPlan;

		// Token: 0x04000CD9 RID: 3289
		private ContactInfo contactInfo;
	}
}
