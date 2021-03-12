using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002BC RID: 700
	internal sealed class CreateAttachment : MultiStepServiceCommand<CreateAttachmentRequest, AttachmentType>, IDisposeTrackable, IDisposable
	{
		// Token: 0x060012E9 RID: 4841 RVA: 0x0005C350 File Offset: 0x0005A550
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<CreateAttachment>(this);
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0005C358 File Offset: 0x0005A558
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x0005C370 File Offset: 0x0005A570
		public CreateAttachment(CallContext callContext, CreateAttachmentRequest request) : base(callContext, request)
		{
			ExTraceGlobals.CreateItemCallTracer.TraceDebug((long)this.GetHashCode(), "CreateAttachment.Execute called");
			this.parentItemId = request.ParentItemId;
			this.attachmentTypes = request.Attachments;
			ServiceCommandBase.ThrowIfNull(this.parentItemId, "parentItemId", "CreateAttachment::ctor");
			ServiceCommandBase.ThrowIfNullOrEmpty<AttachmentType>(this.attachmentTypes, "attachment", "CreateAttachment::ctor");
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x0005C3E9 File Offset: 0x0005A5E9
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0005C40C File Offset: 0x0005A60C
		internal override IExchangeWebMethodResponse GetResponse()
		{
			CreateAttachmentResponse createAttachmentResponse = new CreateAttachmentResponse();
			createAttachmentResponse.AddResponses(base.Results);
			return createAttachmentResponse;
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0005C42C File Offset: 0x0005A62C
		internal override void PreExecuteCommand()
		{
			this.parentIdAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(this.parentItemId, BasicTypes.ItemOrAttachment);
			this.attachments = new AttachmentHierarchy(this.parentIdAndSession, true, base.Request.ClientSupportsIrm);
			this.builder = new AttachmentBuilder(this.attachments, this.attachmentTypes, base.IdConverter, base.Request.ClientSupportsIrm);
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x0005C498 File Offset: 0x0005A698
		internal override void PostExecuteCommand()
		{
			this.attachments.SaveAll();
			this.attachments.RootItem.Load();
			foreach (ServiceResult<AttachmentType> serviceResult in base.Results)
			{
				if (serviceResult.Value != null)
				{
					ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(this.attachments.RootItem.Id, this.parentIdAndSession, null);
					serviceResult.Value.AttachmentId.RootItemId = concatenatedId.Id;
					serviceResult.Value.AttachmentId.RootItemChangeKey = concatenatedId.ChangeKey;
				}
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x0005C52C File Offset: 0x0005A72C
		internal override int StepCount
		{
			get
			{
				return this.attachmentTypes.Length;
			}
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x0005C538 File Offset: 0x0005A738
		internal override ServiceResult<AttachmentType> Execute()
		{
			AttachmentType attachmentType = this.attachmentTypes[base.CurrentStep];
			if (attachmentType is ReferenceAttachmentType && !(ExchangeVersion.Current > ExchangeVersion.ExchangeV2_4))
			{
				throw new ServiceArgumentException((CoreResources.IDs)3784063568U);
			}
			if (base.Request.RequireImageType)
			{
				if (!CreateAttachment.IsInlineImage(attachmentType.ContentType))
				{
					throw new ServiceInvalidOperationException((CoreResources.IDs)4077357270U);
				}
				this.GenerateContentId(attachmentType);
			}
			ServiceError serviceError = null;
			AttachmentType value = null;
			using (Attachment attachment = this.builder.CreateAttachment(attachmentType, out serviceError))
			{
				value = this.CreateAttachmentResult(attachment, attachmentType);
			}
			if (serviceError == null)
			{
				return new ServiceResult<AttachmentType>(value);
			}
			return new ServiceResult<AttachmentType>(value, serviceError);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0005C5F8 File Offset: 0x0005A7F8
		private static bool IsInlineImage(string contentType)
		{
			return contentType != null && CreateAttachment.inlineImageContentTypes.Contains(contentType.ToLowerInvariant());
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0005C610 File Offset: 0x0005A810
		private void GenerateContentId(AttachmentType attachmentType)
		{
			attachmentType.ContentId = Guid.NewGuid().ToString();
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x0005C638 File Offset: 0x0005A838
		private AttachmentType CreateAttachmentResult(Attachment attachment, AttachmentType attachmentType)
		{
			AttachmentType attachmentType2;
			if (attachment is StreamAttachment)
			{
				attachmentType2 = new FileAttachmentType();
			}
			else if (attachment is ReferenceAttachment)
			{
				attachmentType2 = new ReferenceAttachmentType();
			}
			else
			{
				attachmentType2 = new ItemAttachmentType();
			}
			IdAndSession idAndSession = this.parentIdAndSession.Clone();
			attachment.Load();
			idAndSession.AttachmentIds.Add(attachment.Id);
			attachmentType2.AttachmentId = new AttachmentIdType(idAndSession.GetConcatenatedId().Id);
			if (base.Request.IncludeContentIdInResponse)
			{
				attachmentType2.ContentId = attachment.ContentId;
			}
			if (attachmentType is ItemIdAttachmentType)
			{
				attachmentType2.Size = ((attachment.Size > 2147483647L) ? int.MaxValue : ((int)attachment.Size));
			}
			return attachmentType2;
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0005C6EB File Offset: 0x0005A8EB
		private void Dispose(bool fromDispose)
		{
			if (this.builder != null)
			{
				this.builder.Dispose();
				this.builder = null;
			}
		}

		// Token: 0x04000D1A RID: 3354
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04000D1B RID: 3355
		private static HashSet<string> inlineImageContentTypes = new HashSet<string>
		{
			"image/jpeg",
			"image/pjpeg",
			"image/gif",
			"image/bmp",
			"image/png",
			"image/x-png"
		};

		// Token: 0x04000D1C RID: 3356
		private ItemId parentItemId;

		// Token: 0x04000D1D RID: 3357
		private AttachmentType[] attachmentTypes;

		// Token: 0x04000D1E RID: 3358
		private IdAndSession parentIdAndSession;

		// Token: 0x04000D1F RID: 3359
		private AttachmentHierarchy attachments;

		// Token: 0x04000D20 RID: 3360
		private AttachmentBuilder builder;
	}
}
