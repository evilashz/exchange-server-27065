using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.PublicFolder
{
	// Token: 0x0200093E RID: 2366
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IFavoritePublicFoldersManager
	{
		// Token: 0x06005837 RID: 22583
		IEnumerable<IFavoritePublicFolder> EnumerateCalendarFolders();

		// Token: 0x06005838 RID: 22584
		IEnumerable<IFavoritePublicFolder> EnumerateContactsFolders();

		// Token: 0x06005839 RID: 22585
		IEnumerable<IFavoritePublicFolder> EnumerateMailAndPostsFolders();
	}
}
