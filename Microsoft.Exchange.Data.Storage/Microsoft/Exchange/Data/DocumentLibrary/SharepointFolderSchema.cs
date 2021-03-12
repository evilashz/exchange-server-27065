using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006E4 RID: 1764
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SharepointFolderSchema : SharepointDocumentLibraryItemSchema
	{
		// Token: 0x1700146F RID: 5231
		// (get) Token: 0x06004628 RID: 17960 RVA: 0x0012ADDB File Offset: 0x00128FDB
		public new static SharepointFolderSchema Instance
		{
			get
			{
				if (SharepointFolderSchema.instance == null)
				{
					SharepointFolderSchema.instance = new SharepointFolderSchema();
				}
				return SharepointFolderSchema.instance;
			}
		}

		// Token: 0x04002658 RID: 9816
		private static SharepointFolderSchema instance;
	}
}
