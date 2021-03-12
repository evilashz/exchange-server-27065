using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006AB RID: 1707
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IDocumentLibraryFolder : IDocumentLibraryItem, IReadOnlyPropertyBag
	{
		// Token: 0x06004535 RID: 17717
		ITableView GetView(QueryFilter query, SortBy[] sortBy, DocumentLibraryQueryOptions queryOptions, params PropertyDefinition[] propsToReturn);
	}
}
