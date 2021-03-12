using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.PublicFolder
{
	// Token: 0x0200093D RID: 2365
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IFavoritePublicFolder
	{
		// Token: 0x1700187A RID: 6266
		// (get) Token: 0x06005835 RID: 22581
		StoreObjectId FolderId { get; }

		// Token: 0x1700187B RID: 6267
		// (get) Token: 0x06005836 RID: 22582
		string DisplayName { get; }
	}
}
