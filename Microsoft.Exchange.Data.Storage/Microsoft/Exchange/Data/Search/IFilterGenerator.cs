using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Search
{
	// Token: 0x02000CE7 RID: 3303
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IFilterGenerator
	{
		// Token: 0x06007227 RID: 29223
		QueryFilter Execute(string searchString, bool isContentIndexingEnabled, Folder folder, SearchScope searchScope);
	}
}
