using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006D7 RID: 1751
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UncFolderSchema : UncItemSchema
	{
		// Token: 0x17001441 RID: 5185
		// (get) Token: 0x060045CC RID: 17868 RVA: 0x00128EBB File Offset: 0x001270BB
		public new static UncFolderSchema Instance
		{
			get
			{
				if (UncFolderSchema.instance == null)
				{
					UncFolderSchema.instance = new UncFolderSchema();
				}
				return UncFolderSchema.instance;
			}
		}

		// Token: 0x04002631 RID: 9777
		private static UncFolderSchema instance;
	}
}
