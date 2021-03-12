using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EDF RID: 3807
	internal class CopyFolderCommand : ExchangeServiceCommand<CopyFolderRequest, CopyFolderResponse>
	{
		// Token: 0x06006271 RID: 25201 RVA: 0x00133F34 File Offset: 0x00132134
		public CopyFolderCommand(CopyFolderRequest request) : base(request)
		{
		}

		// Token: 0x06006272 RID: 25202 RVA: 0x00133F40 File Offset: 0x00132140
		protected override CopyFolderResponse InternalExecute()
		{
			FolderProvider folderProvider = new FolderProvider(base.ExchangeService);
			Folder result = folderProvider.Copy(base.Request.Id, base.Request.DestinationId);
			return new CopyFolderResponse(base.Request)
			{
				Result = result
			};
		}
	}
}
