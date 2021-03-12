using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006D6 RID: 1750
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UncDocumentSchema : UncItemSchema
	{
		// Token: 0x17001440 RID: 5184
		// (get) Token: 0x060045C9 RID: 17865 RVA: 0x00128E7F File Offset: 0x0012707F
		public new static UncDocumentSchema Instance
		{
			get
			{
				if (UncDocumentSchema.instance == null)
				{
					UncDocumentSchema.instance = new UncDocumentSchema();
				}
				return UncDocumentSchema.instance;
			}
		}

		// Token: 0x0400262E RID: 9774
		private static UncDocumentSchema instance = null;

		// Token: 0x0400262F RID: 9775
		public static readonly DocumentLibraryPropertyDefinition FileType = DocumentSchema.FileType;

		// Token: 0x04002630 RID: 9776
		public static readonly DocumentLibraryPropertyDefinition FileSize = DocumentSchema.FileSize;
	}
}
