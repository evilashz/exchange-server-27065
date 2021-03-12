using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000ED3 RID: 3795
	internal class FindFoldersCommand : ExchangeServiceCommand<FindFoldersRequest, FindFoldersResponse>
	{
		// Token: 0x06006259 RID: 25177 RVA: 0x00133BF0 File Offset: 0x00131DF0
		public FindFoldersCommand(FindFoldersRequest request) : base(request)
		{
		}

		// Token: 0x0600625A RID: 25178 RVA: 0x00133BFC File Offset: 0x00131DFC
		protected override FindFoldersResponse InternalExecute()
		{
			FolderProvider folderProvider = new FolderProvider(base.ExchangeService);
			IFindEntitiesResult<Folder> findEntitiesResult = folderProvider.Find(base.Request.ParentFolderId, new FolderQueryAdapter(FolderSchema.SchemaInstance, base.Request.ODataQueryOptions));
			if (base.Request.ODataQueryOptions.Expands(FolderSchema.ChildFolders.Name))
			{
				foreach (Folder folder in findEntitiesResult)
				{
					folder.ChildFolders = folderProvider.Find(folder.Id, null);
				}
			}
			if (base.Request.ODataQueryOptions.Expands(FolderSchema.Messages.Name))
			{
				MessageProvider messageProvider = new MessageProvider(base.ExchangeService);
				foreach (Folder folder2 in findEntitiesResult)
				{
					folder2.Messages = messageProvider.Find(folder2.Id, null);
				}
			}
			return new FindFoldersResponse(base.Request)
			{
				Result = findEntitiesResult
			};
		}
	}
}
