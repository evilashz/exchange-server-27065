using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006A9 RID: 1705
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IDocument : IDocumentLibraryItem, IReadOnlyPropertyBag
	{
		// Token: 0x1700141B RID: 5147
		// (get) Token: 0x0600452C RID: 17708
		long Size { get; }

		// Token: 0x0600452D RID: 17709
		Stream GetDocument();
	}
}
