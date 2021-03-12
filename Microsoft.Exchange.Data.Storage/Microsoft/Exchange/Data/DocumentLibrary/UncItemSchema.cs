using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006D5 RID: 1749
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UncItemSchema : Schema
	{
		// Token: 0x04002627 RID: 9767
		public static readonly DocumentLibraryPropertyDefinition Uri = DocumentLibraryItemSchema.Uri;

		// Token: 0x04002628 RID: 9768
		public static readonly DocumentLibraryPropertyDefinition CreationDate = DocumentLibraryItemSchema.CreationTime;

		// Token: 0x04002629 RID: 9769
		public static readonly DocumentLibraryPropertyDefinition DisplayName = DocumentLibraryItemSchema.DisplayName;

		// Token: 0x0400262A RID: 9770
		public static readonly DocumentLibraryPropertyDefinition LastModifiedDate = DocumentLibraryItemSchema.LastModifiedDate;

		// Token: 0x0400262B RID: 9771
		public static readonly DocumentLibraryPropertyDefinition IsFolder = DocumentLibraryItemSchema.IsFolder;

		// Token: 0x0400262C RID: 9772
		public static readonly DocumentLibraryPropertyDefinition IsHidden = DocumentLibraryItemSchema.IsHidden;

		// Token: 0x0400262D RID: 9773
		public static readonly DocumentLibraryPropertyDefinition Id = DocumentLibraryItemSchema.Id;
	}
}
