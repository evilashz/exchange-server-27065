using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F2E RID: 3886
	internal class FindContactFoldersCommand : ExchangeServiceCommand<FindContactFoldersRequest, FindContactFoldersResponse>
	{
		// Token: 0x06006323 RID: 25379 RVA: 0x001351E0 File Offset: 0x001333E0
		public FindContactFoldersCommand(FindContactFoldersRequest request) : base(request)
		{
		}

		// Token: 0x06006324 RID: 25380 RVA: 0x001351EC File Offset: 0x001333EC
		protected override FindContactFoldersResponse InternalExecute()
		{
			ContactFolderProvider contactFolderProvider = new ContactFolderProvider(base.ExchangeService);
			IFindEntitiesResult<ContactFolder> findEntitiesResult = contactFolderProvider.Find(base.Request.ParentFolderId, new ContactFolderQueryAdapter(ContactFolderSchema.SchemaInstance, base.Request.ODataQueryOptions));
			if (base.Request.ODataQueryOptions.Expands(ContactFolderSchema.ChildFolders.Name))
			{
				foreach (ContactFolder contactFolder in findEntitiesResult)
				{
					contactFolder.ChildFolders = contactFolderProvider.Find(contactFolder.Id, null);
				}
			}
			if (base.Request.ODataQueryOptions.Expands(ContactFolderSchema.Contacts.Name))
			{
				ContactProvider contactProvider = new ContactProvider(base.ExchangeService);
				foreach (ContactFolder contactFolder2 in findEntitiesResult)
				{
					contactFolder2.Contacts = contactProvider.Find(contactFolder2.Id, null);
				}
			}
			return new FindContactFoldersResponse(base.Request)
			{
				Result = findEntitiesResult
			};
		}
	}
}
