using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EE2 RID: 3810
	internal class MoveFolderCommand : ExchangeServiceCommand<MoveFolderRequest, MoveFolderResponse>
	{
		// Token: 0x06006276 RID: 25206 RVA: 0x00133FA4 File Offset: 0x001321A4
		public MoveFolderCommand(MoveFolderRequest request) : base(request)
		{
		}

		// Token: 0x06006277 RID: 25207 RVA: 0x00133FB0 File Offset: 0x001321B0
		protected override MoveFolderResponse InternalExecute()
		{
			FolderProvider folderProvider = new FolderProvider(base.ExchangeService);
			Folder result = folderProvider.Move(base.Request.Id, base.Request.DestinationId);
			return new MoveFolderResponse(base.Request)
			{
				Result = result
			};
		}
	}
}
