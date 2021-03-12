using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F04 RID: 3844
	internal class GetAttachmentCommand : ExchangeServiceCommand<GetAttachmentRequest, GetAttachmentResponse>
	{
		// Token: 0x060062BB RID: 25275 RVA: 0x00134722 File Offset: 0x00132922
		public GetAttachmentCommand(GetAttachmentRequest request) : base(request)
		{
		}

		// Token: 0x060062BC RID: 25276 RVA: 0x0013472C File Offset: 0x0013292C
		protected override GetAttachmentResponse InternalExecute()
		{
			AttachmentProvider attachmentProvider = new AttachmentProvider(base.ExchangeService);
			AttachmentSchema entitySchema = base.Request.ODataContext.EntityType.GetSchema() as AttachmentSchema;
			Attachment result = attachmentProvider.Read(base.Request.RootItemId, base.Request.Id, new AttachmentQueryAdapter(entitySchema, base.Request.ODataQueryOptions));
			return new GetAttachmentResponse(base.Request)
			{
				Result = result
			};
		}
	}
}
