using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006AA RID: 1706
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IDocumentLibrary : IReadOnlyPropertyBag
	{
		// Token: 0x1700141C RID: 5148
		// (get) Token: 0x0600452E RID: 17710
		ObjectId Id { get; }

		// Token: 0x1700141D RID: 5149
		// (get) Token: 0x0600452F RID: 17711
		string Title { get; }

		// Token: 0x1700141E RID: 5150
		// (get) Token: 0x06004530 RID: 17712
		string Description { get; }

		// Token: 0x1700141F RID: 5151
		// (get) Token: 0x06004531 RID: 17713
		Uri Uri { get; }

		// Token: 0x06004532 RID: 17714
		List<KeyValuePair<string, Uri>> GetHierarchy();

		// Token: 0x06004533 RID: 17715
		IDocumentLibraryItem Read(ObjectId objectId, params PropertyDefinition[] propsToReturn);

		// Token: 0x06004534 RID: 17716
		ITableView GetView(QueryFilter query, SortBy[] sortBy, DocumentLibraryQueryOptions queryOptions, params PropertyDefinition[] propsToReturn);
	}
}
