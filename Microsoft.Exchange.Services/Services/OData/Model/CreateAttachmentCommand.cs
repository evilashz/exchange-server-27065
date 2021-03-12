using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F10 RID: 3856
	internal class CreateAttachmentCommand : ExchangeServiceCommand<CreateAttachmentRequest, CreateAttachmentResponse>
	{
		// Token: 0x060062D7 RID: 25303 RVA: 0x0013496C File Offset: 0x00132B6C
		public CreateAttachmentCommand(CreateAttachmentRequest request) : base(request)
		{
		}

		// Token: 0x060062D8 RID: 25304 RVA: 0x00134978 File Offset: 0x00132B78
		protected override CreateAttachmentResponse InternalExecute()
		{
			AttachmentProvider attachmentProvider = new AttachmentProvider(base.ExchangeService);
			Attachment result = attachmentProvider.Create(base.Request.RootItemId, base.Request.Template);
			return new CreateAttachmentResponse(base.Request)
			{
				Result = result
			};
		}
	}
}
