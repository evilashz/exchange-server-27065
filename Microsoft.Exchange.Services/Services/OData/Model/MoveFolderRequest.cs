using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EE0 RID: 3808
	[AllowedOAuthGrant("Mail.Write")]
	internal class MoveFolderRequest : CopyOrMoveEntityRequest<Folder>
	{
		// Token: 0x06006273 RID: 25203 RVA: 0x00133F8A File Offset: 0x0013218A
		public MoveFolderRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x06006274 RID: 25204 RVA: 0x00133F93 File Offset: 0x00132193
		public override ODataCommand GetODataCommand()
		{
			return new MoveFolderCommand(this);
		}
	}
}
