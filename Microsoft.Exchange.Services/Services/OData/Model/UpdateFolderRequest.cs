using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EDA RID: 3802
	[AllowedOAuthGrant("Mail.Write")]
	internal class UpdateFolderRequest : UpdateEntityRequest<Folder>
	{
		// Token: 0x06006269 RID: 25193 RVA: 0x00133EAD File Offset: 0x001320AD
		public UpdateFolderRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x0600626A RID: 25194 RVA: 0x00133EB6 File Offset: 0x001320B6
		public override ODataCommand GetODataCommand()
		{
			return new UpdateFolderCommand(this);
		}
	}
}
