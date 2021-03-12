using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.FaultInjection;
using Microsoft.Exchange.UM.UMCommon.MessageContent;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002C8 RID: 712
	internal abstract class VoiceMessagePipelineContextBase : PipelineContext, IUMCompressAudio, IUMTranscribeAudio
	{
		// Token: 0x06001594 RID: 5524 RVA: 0x0005BE54 File Offset: 0x0005A054
		protected VoiceMessagePipelineContextBase(AudioCodecEnum audioCodec, SubmissionHelper helper, string attachmentName, int duration) : base(helper)
		{
			this.tempAttachmentPath = attachmentName;
			this.audioCodec = audioCodec;
			this.duration = TimeSpan.FromSeconds((double)duration);
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x0005BE80 File Offset: 0x0005A080
		protected VoiceMessagePipelineContextBase(SubmissionHelper helper) : base(helper)
		{
			bool flag = false;
			try
			{
				if (!helper.CustomHeaders.Contains("Duration"))
				{
					throw new HeaderFileArgumentInvalidException("Duration");
				}
				int num = int.Parse((string)helper.CustomHeaders["Duration"], CultureInfo.InvariantCulture);
				this.duration = TimeSpan.FromSeconds((double)num);
				if (!helper.CustomHeaders.Contains("AttachmentPath"))
				{
					throw new HeaderFileArgumentInvalidException("AttachmentPath");
				}
				if (this.duration.TotalSeconds > 0.0)
				{
					this.attachmentPath = (string)helper.CustomHeaders["AttachmentPath"];
					if (!string.Equals(Path.GetExtension(this.attachmentPath), ".wav", StringComparison.InvariantCultureIgnoreCase))
					{
						CallIdTracer.TraceError(ExTraceGlobals.VoiceMailTracer, 0, "Attachment name is not a wav file  {0}", new object[]
						{
							this.attachmentPath
						});
						throw new HeaderFileArgumentInvalidException(string.Format(CultureInfo.InvariantCulture, "WAV {0}: {1}", new object[]
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
				else
				{
					this.attachmentPath = null;
				}
				if (!helper.CustomHeaders.Contains("Codec"))
				{
					throw new HeaderFileArgumentInvalidException("Codec");
				}
				this.audioCodec = (AudioCodecEnum)Enum.Parse(typeof(AudioCodecEnum), (string)helper.CustomHeaders["Codec"]);
				if (helper.CustomHeaders.Contains("TranscriptionData"))
				{
					XmlDocument xmlDocument = new SafeXmlDocument();
					try
					{
						xmlDocument.LoadXml((string)helper.CustomHeaders["TranscriptionData"]);
					}
					catch (XmlException innerException)
					{
						throw new HeaderFileArgumentInvalidException("TranscriptionData", innerException);
					}
					this.TranscriptionData = new PartnerTranscriptionData(xmlDocument);
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

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x0005C100 File Offset: 0x0005A300
		public AudioCodecEnum AudioCodec
		{
			get
			{
				return this.audioCodec;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001597 RID: 5527 RVA: 0x0005C108 File Offset: 0x0005A308
		// (set) Token: 0x06001598 RID: 5528 RVA: 0x0005C110 File Offset: 0x0005A310
		public ITempFile CompressedAudioFile
		{
			get
			{
				return this.compressedAudioFile;
			}
			set
			{
				this.compressedAudioFile = value;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x0005C119 File Offset: 0x0005A319
		public string FileToCompressPath
		{
			get
			{
				return this.attachmentPath;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x0005C121 File Offset: 0x0005A321
		public string AttachmentPath
		{
			get
			{
				return this.attachmentPath;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x0005C129 File Offset: 0x0005A329
		// (set) Token: 0x0600159C RID: 5532 RVA: 0x0005C131 File Offset: 0x0005A331
		public ITranscriptionData TranscriptionData
		{
			get
			{
				return this.transcriptionData;
			}
			set
			{
				this.transcriptionData = value;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x0005C13A File Offset: 0x0005A33A
		// (set) Token: 0x0600159E RID: 5534 RVA: 0x0005C142 File Offset: 0x0005A342
		public UMSubscriber TranscriptionUser { get; protected set; }

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x0005C14B File Offset: 0x0005A34B
		public TimeSpan Duration
		{
			get
			{
				return this.duration;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x0005C153 File Offset: 0x0005A353
		public virtual bool EnableTopNGrammar
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x0005C158 File Offset: 0x0005A358
		public override void PostCompletion()
		{
			if (this.attachmentPath != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "VoiceMessagePipelineContextBase.PostCompletion - Deleting attachment file '{0}'", new object[]
				{
					this.attachmentPath
				});
				Util.TryDeleteFile(this.attachmentPath);
			}
			if (this.compressedAudioFile != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "VoiceMessagePipelineContextBase.PostCompletion - Deleting compressed audio file '{0}'", new object[]
				{
					this.compressedAudioFile.FilePath
				});
				this.compressedAudioFile.Dispose();
			}
			base.PostCompletion();
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x0005C1E4 File Offset: 0x0005A3E4
		internal override void SaveMessage()
		{
			if (this.tempAttachmentPath != null)
			{
				this.attachmentPath = Path.Combine(Path.GetDirectoryName(base.HeaderFileName), Path.GetFileNameWithoutExtension(base.HeaderFileName) + ".wav");
				File.Copy(this.tempAttachmentPath, this.attachmentPath);
				this.tempAttachmentPath = null;
			}
			base.SaveMessage();
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0005C244 File Offset: 0x0005A444
		internal override void WriteCustomHeaderFields(StreamWriter headerStream)
		{
			string str = this.attachmentPath ?? "null";
			headerStream.WriteLine("AttachmentPath : " + str);
			headerStream.WriteLine("Duration : " + (int)this.duration.TotalSeconds);
			headerStream.WriteLine("Codec : " + this.audioCodec.ToString("g"));
			if (this.TranscriptionData != null)
			{
				headerStream.WriteLine("TranscriptionData : " + this.TranscriptionData.TranscriptionXml.OuterXml);
			}
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x0005C2E0 File Offset: 0x0005A4E0
		protected void CreateRightsManagedItem(LocalizedString? outerMessageBody, UMRecipient user, string contentClass)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "VoiceMessagePipelineContextBase:CreateRightsManagedItem.", new object[0]);
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				FaultInjectionUtils.FaultInjectException(3034983741U);
				RightsManagedMessageItem rightsManagedMessageItem = RightsManagedMessageItem.Create(base.MessageToSubmit, XsoUtil.GetOutboundConversionOptions(user));
				disposeGuard.Add<RightsManagedMessageItem>(rightsManagedMessageItem);
				rightsManagedMessageItem.SetRestriction(RmsTemplate.DoNotForward);
				if (this.TranscriptionData != null)
				{
					this.AddASRDataAttachment(rightsManagedMessageItem);
				}
				rightsManagedMessageItem.SetDefaultEnvelopeBody(outerMessageBody);
				rightsManagedMessageItem.ClassName = contentClass;
				rightsManagedMessageItem[StoreObjectSchema.ContentClass] = contentClass;
				rightsManagedMessageItem.Save(SaveMode.FailOnAnyConflict);
				disposeGuard.Success();
				base.MessageToSubmit = rightsManagedMessageItem;
			}
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x0005C3A4 File Offset: 0x0005A5A4
		protected void AddASRDataAttachment(RightsManagedMessageItem message)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "VoiceMessagePipelineContextBase:AddASRDataAttachment.", new object[0]);
			using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(this.TranscriptionData.TranscriptionXml.OuterXml)))
			{
				XsoUtil.AddHiddenAttachment(message.ProtectedAttachmentCollection, memoryStream, "voicemail.umrmasr", "text");
			}
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x0005C424 File Offset: 0x0005A624
		protected void SetNDRProperties(MessageContentBuilder content)
		{
			base.MessageToSubmit.AutoResponseSuppress = AutoResponseSuppress.All;
			using (MemoryStream memoryStream = content.ToStream())
			{
				using (Stream stream = base.MessageToSubmit.Body.OpenWriteStream(new BodyWriteConfiguration(Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml, Charset.UTF8.Name)))
				{
					memoryStream.WriteTo(stream);
				}
			}
			base.MessageToSubmit.ClassName = "IPM.Note";
			base.MessageToSubmit.Importance = Importance.High;
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x0005C4BC File Offset: 0x0005A6BC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<VoiceMessagePipelineContextBase>(this);
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0005C4C4 File Offset: 0x0005A6C4
		protected LocalizedString? GetCustomDRMText(UMMailboxPolicy policy)
		{
			LocalizedString? result = null;
			if (policy != null)
			{
				if (string.IsNullOrEmpty(policy.ProtectedVoiceMailText))
				{
					result = new LocalizedString?(Strings.UMBodyDownload);
				}
				else
				{
					result = new LocalizedString?(new LocalizedString(policy.ProtectedVoiceMailText));
				}
			}
			return result;
		}

		// Token: 0x04000CDF RID: 3295
		private const string ASRAttachmentNameForDRMVoicemails = "voicemail.umrmasr";

		// Token: 0x04000CE0 RID: 3296
		private string attachmentPath;

		// Token: 0x04000CE1 RID: 3297
		private string tempAttachmentPath;

		// Token: 0x04000CE2 RID: 3298
		private ITempFile compressedAudioFile;

		// Token: 0x04000CE3 RID: 3299
		private TimeSpan duration;

		// Token: 0x04000CE4 RID: 3300
		private AudioCodecEnum audioCodec;

		// Token: 0x04000CE5 RID: 3301
		private ITranscriptionData transcriptionData;

		// Token: 0x04000CE6 RID: 3302
		protected int temporaryMessageRetentionDays = 7;
	}
}
