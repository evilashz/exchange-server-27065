using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006A0 RID: 1696
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DocumentSchema : DocumentLibraryItemSchema
	{
		// Token: 0x17001414 RID: 5140
		// (get) Token: 0x06004521 RID: 17697 RVA: 0x00126110 File Offset: 0x00124310
		public new static DocumentSchema Instance
		{
			get
			{
				if (DocumentSchema.instance == null)
				{
					DocumentSchema.instance = new DocumentSchema();
				}
				return DocumentSchema.instance;
			}
		}

		// Token: 0x040025B4 RID: 9652
		private static DocumentSchema instance = null;

		// Token: 0x040025B5 RID: 9653
		public static readonly DocumentLibraryPropertyDefinition FileSize = new DocumentLibraryPropertyDefinition("Size", typeof(int), null, DocumentLibraryPropertyId.FileSize);

		// Token: 0x040025B6 RID: 9654
		public static readonly DocumentLibraryPropertyDefinition FileType = new DocumentLibraryPropertyDefinition("ContentType", typeof(string), string.Empty, DocumentLibraryPropertyId.FileType);
	}
}
