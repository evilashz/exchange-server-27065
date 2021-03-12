using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F0A RID: 3850
	internal class DeleteAttachmentCommand : ExchangeServiceCommand<DeleteAttachmentRequest, DeleteAttachmentResponse>
	{
		// Token: 0x060062C7 RID: 25287 RVA: 0x00134835 File Offset: 0x00132A35
		public DeleteAttachmentCommand(DeleteAttachmentRequest request) : base(request)
		{
		}

		// Token: 0x060062C8 RID: 25288 RVA: 0x00134840 File Offset: 0x00132A40
		protected override DeleteAttachmentResponse InternalExecute()
		{
			AttachmentProvider attachmentProvider = new AttachmentProvider(base.ExchangeService);
			attachmentProvider.Delete(base.Request.RootItemId, base.Request.Id);
			return new DeleteAttachmentResponse(base.Request);
		}
	}
}
