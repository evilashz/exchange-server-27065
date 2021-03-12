using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.MessageContent;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002BC RID: 700
	internal class FaxPipelineContext : PipelineContext, IUMCAMessage, IUMResolveCaller
	{
		// Token: 0x06001531 RID: 5425 RVA: 0x0005ADCC File Offset: 0x00058FCC
		internal FaxPipelineContext(SubmissionHelper helper, string attachmentName, uint numberOfPages, bool incomplete, UMRecipient recipient, string messageID, ExDateTime sentTime) : base(helper)
		{
			this.tempAttachmentPath = attachmentName;
			this.numberOfPages = numberOfPages;
			this.incomplete = incomplete;
			base.MessageType = "Fax";
			this.recipient = recipient;
			this.recipient.AddReference();
			base.MessageID = messageID;
			base.SentTime = sentTime;
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0005AE24 File Offset: 0x00059024
		internal FaxPipelineContext(SubmissionHelper helper) : base(helper)
		{
			bool flag = false;
			try
			{
				this.recipient = base.CreateRecipientFromObjectGuid(helper.RecipientObjectGuid, helper.TenantGuid);
				this.recipientDialPlan = base.InitializeCallerIdAndTryGetDialPlan(this.recipient);
				if (helper.CustomHeaders.Contains("AttachmentPath"))
				{
					this.attachmentPath = (string)helper.CustomHeaders["AttachmentPath"];
					if (Path.GetExtension(this.attachmentPath) != ".tif")
					{
						CallIdTracer.TraceError(ExTraceGlobals.VoiceMailTracer, 0, "Attachment name is not a Tif file  {0}", new object[]
						{
							this.attachmentPath
						});
						throw new HeaderFileArgumentInvalidException(string.Format(CultureInfo.InvariantCulture, "TIFF {0}: {1}", new object[]
						{
							"AttachmentPath",
							this.attachmentPath
						}));
					}
					if (!File.Exists(this.attachmentPath))
					{
						CallIdTracer.TraceError(ExTraceGlobals.VoiceMailTracer, 0, "Attachment file {0} does not exist", new object[]
						{
							this.attachmentPath
						});
						throw new HeaderFileArgumentInvalidException(string.Format(CultureInfo.InvariantCulture, "{0}: {1}", new object[]
						{
							"AttachmentPath",
							this.attachmentPath
						}));
					}
				}
				if (!helper.CustomHeaders.Contains("NumberOfPages"))
				{
					throw new HeaderFileArgumentInvalidException("NumberOfPages");
				}
				this.numberOfPages = uint.Parse((string)helper.CustomHeaders["NumberOfPages"], CultureInfo.InvariantCulture);
				if (helper.CustomHeaders.Contains("InComplete"))
				{
					this.incomplete = bool.Parse((string)helper.CustomHeaders["InComplete"]);
				}
				base.MessageType = "Fax";
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

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x0005B010 File Offset: 0x00059210
		public UMRecipient CAMessageRecipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x0005B018 File Offset: 0x00059218
		public bool CollectMessageForAnalysis
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x0005B01B File Offset: 0x0005921B
		// (set) Token: 0x06001536 RID: 5430 RVA: 0x0005B023 File Offset: 0x00059223
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

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x0005B02C File Offset: 0x0005922C
		internal override Pipeline Pipeline
		{
			get
			{
				return TextPipeline.Instance;
			}
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x0005B034 File Offset: 0x00059234
		public override void PostCompletion()
		{
			if (this.attachmentPath != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "FaxPipelineContext.PostCompletion - Deleting attachment file '{0}'", new object[]
				{
					this.attachmentPath
				});
				Util.TryDeleteFile(this.attachmentPath);
			}
			base.PostCompletion();
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0005B081 File Offset: 0x00059281
		public override string GetMailboxServerId()
		{
			return base.GetMailboxServerIdHelper();
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0005B089 File Offset: 0x00059289
		public override string GetRecipientIdForThrottling()
		{
			return base.GetRecipientIdHelper();
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0005B094 File Offset: 0x00059294
		internal override void SaveMessage()
		{
			if (this.tempAttachmentPath != null)
			{
				this.attachmentPath = Path.Combine(Path.GetDirectoryName(base.HeaderFileName), Path.GetFileNameWithoutExtension(base.HeaderFileName) + ".tif");
				File.Copy(this.tempAttachmentPath, this.attachmentPath);
				this.tempAttachmentPath = null;
			}
			base.SaveMessage();
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0005B0F4 File Offset: 0x000592F4
		internal override void WriteCustomHeaderFields(StreamWriter headerStream)
		{
			headerStream.WriteLine("AttachmentPath : " + this.attachmentPath);
			headerStream.WriteLine("InComplete : " + this.incomplete);
			headerStream.WriteLine("NumberOfPages : " + this.numberOfPages);
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0005B14D File Offset: 0x0005934D
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<FaxPipelineContext>(this);
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0005B158 File Offset: 0x00059358
		protected override void SetMessageProperties()
		{
			base.SetMessageProperties();
			MessageContentBuilder messageContentBuilder = MessageContentBuilder.Create(base.CultureInfo, this.recipientDialPlan);
			base.MessageToSubmit.Subject = messageContentBuilder.GetFaxSubject(this.ContactInfo, base.CallerId, this.numberOfPages, this.incomplete);
			base.MessageToSubmit[MessageItemSchema.FaxNumberOfPages] = (int)this.numberOfPages;
			string additionalText = null;
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromADRecipient(this.CAMessageRecipient.ADRecipient);
			UMMailboxPolicy policyFromRecipient = iadsystemConfigurationLookup.GetPolicyFromRecipient(this.CAMessageRecipient.ADRecipient);
			if (policyFromRecipient != null)
			{
				additionalText = policyFromRecipient.FaxMessageText;
			}
			messageContentBuilder.AddFaxBody(base.CallerId, this.ContactInfo, additionalText);
			using (MemoryStream memoryStream = messageContentBuilder.ToStream())
			{
				using (Stream stream = base.MessageToSubmit.Body.OpenWriteStream(new BodyWriteConfiguration(BodyFormat.TextHtml, Charset.UTF8.Name)))
				{
					memoryStream.WriteTo(stream);
				}
			}
			if (this.attachmentPath != null)
			{
				string attachmentName;
				if (this.numberOfPages > 1U)
				{
					attachmentName = Strings.FaxAttachmentInPages(base.CallerId.ToDisplay, this.numberOfPages, ".tif").ToString(base.CultureInfo);
				}
				else
				{
					attachmentName = Strings.FaxAttachmentInPage(base.CallerId.ToDisplay, ".tif").ToString(base.CultureInfo);
				}
				using (FileStream fileStream = File.Open(this.attachmentPath, FileMode.Open, FileAccess.Read))
				{
					XsoUtil.AddAttachment(base.MessageToSubmit.AttachmentCollection, fileStream, attachmentName, "image/tiff");
					CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "Successfully attached recorded message.", new object[0]);
				}
			}
			base.MessageToSubmit.ClassName = "IPM.Note.Microsoft.Fax.CA";
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0005B340 File Offset: 0x00059540
		protected override void InternalDispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "FaxPipelineContext.Dispose() called", new object[0]);
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

		// Token: 0x04000CC7 RID: 3271
		private string attachmentPath;

		// Token: 0x04000CC8 RID: 3272
		private string tempAttachmentPath;

		// Token: 0x04000CC9 RID: 3273
		private uint numberOfPages;

		// Token: 0x04000CCA RID: 3274
		private bool incomplete;

		// Token: 0x04000CCB RID: 3275
		private UMRecipient recipient;

		// Token: 0x04000CCC RID: 3276
		private UMDialPlan recipientDialPlan;

		// Token: 0x04000CCD RID: 3277
		private ContactInfo contactInfo;
	}
}
