using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006A8 RID: 1704
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IDocumentLibraryItem : IReadOnlyPropertyBag
	{
		// Token: 0x17001415 RID: 5141
		// (get) Token: 0x06004524 RID: 17700
		string DisplayName { get; }

		// Token: 0x17001416 RID: 5142
		// (get) Token: 0x06004525 RID: 17701
		Uri Uri { get; }

		// Token: 0x17001417 RID: 5143
		// (get) Token: 0x06004526 RID: 17702
		ObjectId Id { get; }

		// Token: 0x17001418 RID: 5144
		// (get) Token: 0x06004527 RID: 17703
		bool IsFolder { get; }

		// Token: 0x17001419 RID: 5145
		// (get) Token: 0x06004528 RID: 17704
		IDocumentLibraryFolder Parent { get; }

		// Token: 0x1700141A RID: 5146
		// (get) Token: 0x06004529 RID: 17705
		IDocumentLibrary Library { get; }

		// Token: 0x0600452A RID: 17706
		object TryGetProperty(PropertyDefinition propertyDefinition);

		// Token: 0x0600452B RID: 17707
		List<KeyValuePair<string, Uri>> GetHierarchy();
	}
}
