using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x0200069A RID: 1690
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DocumentLibraryItemSchema : Schema
	{
		// Token: 0x0400258E RID: 9614
		public static readonly DocumentLibraryPropertyDefinition Uri = new DocumentLibraryPropertyDefinition("Uri", typeof(Uri), null, DocumentLibraryPropertyId.Uri);

		// Token: 0x0400258F RID: 9615
		public static readonly DocumentLibraryPropertyDefinition CreationTime = new DocumentLibraryPropertyDefinition("CreationTime", typeof(ExDateTime), null, DocumentLibraryPropertyId.CreationTime);

		// Token: 0x04002590 RID: 9616
		public static readonly DocumentLibraryPropertyDefinition DisplayName = new DocumentLibraryPropertyDefinition("Display Name", typeof(string), string.Empty, DocumentLibraryPropertyId.Title);

		// Token: 0x04002591 RID: 9617
		public static readonly DocumentLibraryPropertyDefinition LastModifiedDate = new DocumentLibraryPropertyDefinition("LastModifiedDate", typeof(ExDateTime), null, DocumentLibraryPropertyId.LastModifiedTime);

		// Token: 0x04002592 RID: 9618
		public static readonly DocumentLibraryPropertyDefinition IsFolder = new DocumentLibraryPropertyDefinition("IsFolder", typeof(bool), null, DocumentLibraryPropertyId.IsFolder);

		// Token: 0x04002593 RID: 9619
		public static readonly DocumentLibraryPropertyDefinition IsHidden = new DocumentLibraryPropertyDefinition("IsHidden", typeof(bool), null, DocumentLibraryPropertyId.IsHidden);

		// Token: 0x04002594 RID: 9620
		public static readonly DocumentLibraryPropertyDefinition Id = new DocumentLibraryPropertyDefinition("Id", typeof(DocumentLibraryObjectId), null, DocumentLibraryPropertyId.Id);
	}
}
