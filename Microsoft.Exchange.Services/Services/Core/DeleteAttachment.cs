using System;
using System.Collections.Generic;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002CB RID: 715
	internal sealed class DeleteAttachment : MultiStepServiceCommand<DeleteAttachmentRequest, RootItemIdType>
	{
		// Token: 0x060013D7 RID: 5079 RVA: 0x00063494 File Offset: 0x00061694
		public DeleteAttachment(CallContext callContext, DeleteAttachmentRequest request) : base(callContext, request)
		{
			this.attachmentIds = base.Request.AttachmentIds;
			ServiceCommandBase.ThrowIfNullOrEmpty<AttachmentIdType>(this.attachmentIds, "attachmentIds", "DeleteAttachment::Execute");
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x000634D0 File Offset: 0x000616D0
		internal override IExchangeWebMethodResponse GetResponse()
		{
			foreach (ServiceResult<RootItemIdType> serviceResult in base.Results)
			{
				if (serviceResult != null && serviceResult.Code == ServiceResultCode.Success)
				{
					serviceResult.Value.RootItemChangeKey = this.rootItemIds[serviceResult.Value.RootItemId];
				}
			}
			DeleteAttachmentResponse deleteAttachmentResponse = new DeleteAttachmentResponse();
			deleteAttachmentResponse.BuildForResults<RootItemIdType>(base.Results);
			return deleteAttachmentResponse;
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x00063536 File Offset: 0x00061736
		internal override int StepCount
		{
			get
			{
				return this.attachmentIds.Length;
			}
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x00063540 File Offset: 0x00061740
		internal override ServiceResult<RootItemIdType> Execute()
		{
			IdAndSession idAndSession = base.IdConverter.ConvertAttachmentIdToIdAndSessionReadOnly(this.attachmentIds[base.CurrentStep]);
			RootItemIdType rootItemIdType = new RootItemIdType();
			using (AttachmentHierarchy attachmentHierarchy = new AttachmentHierarchy(idAndSession, true, base.Request.ClientSupportsIrm))
			{
				if (attachmentHierarchy.Last.Attachment.IsContactPhoto)
				{
					attachmentHierarchy.RootItem[ContactSchema.HasPicturePropertyDef] = false;
				}
				attachmentHierarchy.DeleteLast();
				attachmentHierarchy.SaveAll();
				attachmentHierarchy.RootItem.Load();
				ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(attachmentHierarchy.RootItem.Id, idAndSession, null);
				this.rootItemIds[concatenatedId.Id] = concatenatedId.ChangeKey;
				rootItemIdType.RootItemId = concatenatedId.Id;
			}
			return new ServiceResult<RootItemIdType>(rootItemIdType);
		}

		// Token: 0x04000D74 RID: 3444
		private AttachmentIdType[] attachmentIds;

		// Token: 0x04000D75 RID: 3445
		private Dictionary<string, string> rootItemIds = new Dictionary<string, string>();
	}
}
