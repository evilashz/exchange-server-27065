using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EDC RID: 3804
	internal class UpdateFolderCommand : ExchangeServiceCommand<UpdateFolderRequest, UpdateFolderResponse>
	{
		// Token: 0x0600626C RID: 25196 RVA: 0x00133EC7 File Offset: 0x001320C7
		public UpdateFolderCommand(UpdateFolderRequest request) : base(request)
		{
		}

		// Token: 0x0600626D RID: 25197 RVA: 0x00133ED0 File Offset: 0x001320D0
		protected override UpdateFolderResponse InternalExecute()
		{
			FolderProvider folderProvider = new FolderProvider(base.ExchangeService);
			Folder result = folderProvider.Update(base.Request.Id, base.Request.Change);
			return new UpdateFolderResponse(base.Request)
			{
				Result = result
			};
		}
	}
}
