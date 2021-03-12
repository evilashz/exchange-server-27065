using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EFE RID: 3838
	internal class FindAttachmentsCommand : ExchangeServiceCommand<FindAttachmentsRequest, FindAttachmentsResponse>
	{
		// Token: 0x060062AF RID: 25263 RVA: 0x0013461B File Offset: 0x0013281B
		public FindAttachmentsCommand(FindAttachmentsRequest request) : base(request)
		{
		}

		// Token: 0x060062B0 RID: 25264 RVA: 0x00134624 File Offset: 0x00132824
		protected override FindAttachmentsResponse InternalExecute()
		{
			AttachmentProvider attachmentProvider = new AttachmentProvider(base.ExchangeService);
			AttachmentSchema entitySchema = base.Request.ODataContext.EntityType.GetSchema() as AttachmentSchema;
			IFindEntitiesResult<Attachment> result = attachmentProvider.Find(base.Request.RootItemId, new AttachmentQueryAdapter(entitySchema, base.Request.ODataQueryOptions));
			return new FindAttachmentsResponse(base.Request)
			{
				Result = result
			};
		}
	}
}
