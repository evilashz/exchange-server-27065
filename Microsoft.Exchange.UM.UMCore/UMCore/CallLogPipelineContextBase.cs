using System;
using System.IO;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002B5 RID: 693
	internal abstract class CallLogPipelineContextBase : PipelineContext, IUMCAMessage, IUMResolveCaller
	{
		// Token: 0x06001500 RID: 5376 RVA: 0x0005A790 File Offset: 0x00058990
		internal CallLogPipelineContextBase(SubmissionHelper helper) : base(helper)
		{
			bool flag = false;
			try
			{
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

		// Token: 0x06001501 RID: 5377 RVA: 0x0005A7F0 File Offset: 0x000589F0
		internal CallLogPipelineContextBase(SubmissionHelper helper, UMRecipient recipient) : base(helper)
		{
			this.recipient = recipient;
			this.recipient.AddReference();
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x0005A80B File Offset: 0x00058A0B
		public UMRecipient CAMessageRecipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x0005A813 File Offset: 0x00058A13
		public bool CollectMessageForAnalysis
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x0005A816 File Offset: 0x00058A16
		// (set) Token: 0x06001505 RID: 5381 RVA: 0x0005A81E File Offset: 0x00058A1E
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

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x0005A827 File Offset: 0x00058A27
		internal override Pipeline Pipeline
		{
			get
			{
				return TextPipeline.Instance;
			}
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x0005A82E File Offset: 0x00058A2E
		internal override void WriteCustomHeaderFields(StreamWriter headerStream)
		{
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x0005A830 File Offset: 0x00058A30
		public override string GetMailboxServerId()
		{
			return base.GetMailboxServerIdHelper();
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x0005A838 File Offset: 0x00058A38
		public override string GetRecipientIdForThrottling()
		{
			return base.GetRecipientIdHelper();
		}

		// Token: 0x0600150A RID: 5386
		protected abstract string GetMessageSubject(MessageContentBuilder contentBuilder);

		// Token: 0x0600150B RID: 5387
		protected abstract void AddMessageBody(MessageContentBuilder contentBuilder);

		// Token: 0x0600150C RID: 5388 RVA: 0x0005A840 File Offset: 0x00058A40
		protected override void SetMessageProperties()
		{
			base.SetMessageProperties();
			MessageContentBuilder messageContentBuilder = MessageContentBuilder.Create(base.CultureInfo, this.recipientDialPlan);
			base.MessageToSubmit.Subject = this.GetMessageSubject(messageContentBuilder);
			this.AddMessageBody(messageContentBuilder);
			using (MemoryStream memoryStream = messageContentBuilder.ToStream())
			{
				using (Stream stream = base.MessageToSubmit.Body.OpenWriteStream(new BodyWriteConfiguration(BodyFormat.TextHtml, Charset.UTF8.Name)))
				{
					memoryStream.WriteTo(stream);
				}
			}
			base.MessageToSubmit.ClassName = "IPM.Note.Microsoft.Conversation.Voice";
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0005A8F4 File Offset: 0x00058AF4
		protected override void InternalDispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "CallLogPipelineContextBase.Dispose() called", new object[0]);
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

		// Token: 0x04000CBF RID: 3263
		private UMRecipient recipient;

		// Token: 0x04000CC0 RID: 3264
		private UMDialPlan recipientDialPlan;

		// Token: 0x04000CC1 RID: 3265
		private ContactInfo contactInfo;
	}
}
