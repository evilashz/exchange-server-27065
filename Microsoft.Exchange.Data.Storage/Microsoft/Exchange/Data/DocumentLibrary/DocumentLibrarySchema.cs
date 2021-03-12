using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x0200069F RID: 1695
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DocumentLibrarySchema
	{
		// Token: 0x040025B0 RID: 9648
		public static DocumentLibraryPropertyDefinition Id = DocumentLibraryItemSchema.Id;

		// Token: 0x040025B1 RID: 9649
		public static DocumentLibraryPropertyDefinition Uri = DocumentLibraryItemSchema.Uri;

		// Token: 0x040025B2 RID: 9650
		public static DocumentLibraryPropertyDefinition Title = new DocumentLibraryPropertyDefinition("Title", typeof(string), null, DocumentLibraryPropertyId.Title);

		// Token: 0x040025B3 RID: 9651
		public static DocumentLibraryPropertyDefinition Description = new DocumentLibraryPropertyDefinition("Description", typeof(string), null, DocumentLibraryPropertyId.Description);
	}
}
