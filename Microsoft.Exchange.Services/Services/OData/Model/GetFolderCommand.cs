using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000ED0 RID: 3792
	internal class GetFolderCommand : ExchangeServiceCommand<GetFolderRequest, GetFolderResponse>
	{
		// Token: 0x06006250 RID: 25168 RVA: 0x00133A67 File Offset: 0x00131C67
		public GetFolderCommand(GetFolderRequest request) : base(request)
		{
		}

		// Token: 0x06006251 RID: 25169 RVA: 0x00133A70 File Offset: 0x00131C70
		protected override GetFolderResponse InternalExecute()
		{
			FolderProvider folderProvider = new FolderProvider(base.ExchangeService);
			Folder folder = folderProvider.Read(base.Request.Id, new FolderQueryAdapter(FolderSchema.SchemaInstance, base.Request.ODataQueryOptions));
			if (base.Request.ODataQueryOptions.Expands(FolderSchema.ChildFolders.Name))
			{
				folder.ChildFolders = folderProvider.Find(folder.Id, null);
			}
			if (base.Request.ODataQueryOptions.Expands(FolderSchema.Messages.Name))
			{
				MessageProvider messageProvider = new MessageProvider(base.ExchangeService);
				folder.Messages = messageProvider.Find(folder.Id, null);
			}
			return new GetFolderResponse(base.Request)
			{
				Result = folder
			};
		}
	}
}
