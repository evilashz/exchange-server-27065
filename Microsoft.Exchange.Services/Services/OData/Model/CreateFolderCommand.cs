using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000ED6 RID: 3798
	internal class CreateFolderCommand : ExchangeServiceCommand<CreateFolderRequest, CreateFolderResponse>
	{
		// Token: 0x06006262 RID: 25186 RVA: 0x00133DFF File Offset: 0x00131FFF
		public CreateFolderCommand(CreateFolderRequest request) : base(request)
		{
		}

		// Token: 0x06006263 RID: 25187 RVA: 0x00133E08 File Offset: 0x00132008
		protected override CreateFolderResponse InternalExecute()
		{
			FolderProvider folderProvider = new FolderProvider(base.ExchangeService);
			Folder result = folderProvider.Create(base.Request.ParentFolderId, base.Request.Template);
			return new CreateFolderResponse(base.Request)
			{
				Result = result
			};
		}
	}
}
