using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000ED9 RID: 3801
	internal class DeleteFolderCommand : ExchangeServiceCommand<DeleteFolderRequest, DeleteFolderResponse>
	{
		// Token: 0x06006267 RID: 25191 RVA: 0x00133E6C File Offset: 0x0013206C
		public DeleteFolderCommand(DeleteFolderRequest request) : base(request)
		{
		}

		// Token: 0x06006268 RID: 25192 RVA: 0x00133E78 File Offset: 0x00132078
		protected override DeleteFolderResponse InternalExecute()
		{
			FolderProvider folderProvider = new FolderProvider(base.ExchangeService);
			folderProvider.Delete(base.Request.Id);
			return new DeleteFolderResponse(base.Request);
		}
	}
}
