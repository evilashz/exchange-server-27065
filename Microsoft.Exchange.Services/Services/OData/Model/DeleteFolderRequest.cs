using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000ED7 RID: 3799
	[AllowedOAuthGrant("Mail.Write")]
	internal class DeleteFolderRequest : DeleteEntityRequest<Folder>
	{
		// Token: 0x06006264 RID: 25188 RVA: 0x00133E52 File Offset: 0x00132052
		public DeleteFolderRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x06006265 RID: 25189 RVA: 0x00133E5B File Offset: 0x0013205B
		public override ODataCommand GetODataCommand()
		{
			return new DeleteFolderCommand(this);
		}
	}
}
