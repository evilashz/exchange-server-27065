using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000676 RID: 1654
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FolderNoCreator : DefaultFolderCreator
	{
		// Token: 0x06004444 RID: 17476 RVA: 0x001232A6 File Offset: 0x001214A6
		internal FolderNoCreator() : base(DefaultFolderType.None, StoreObjectType.Folder, true)
		{
		}

		// Token: 0x06004445 RID: 17477 RVA: 0x001232B1 File Offset: 0x001214B1
		internal override Folder Create(DefaultFolderContext context, string folderName, out bool hasCreatedNew)
		{
			throw new NotSupportedException("The defaultFolder does not support Create.");
		}

		// Token: 0x06004446 RID: 17478 RVA: 0x001232BD File Offset: 0x001214BD
		internal override AggregateOperationResult Delete(DefaultFolderContext context, DeleteItemFlags deleteItemFlags, StoreObjectId id)
		{
			throw new NotSupportedException("The defaultFolder does not support deletion.");
		}
	}
}
