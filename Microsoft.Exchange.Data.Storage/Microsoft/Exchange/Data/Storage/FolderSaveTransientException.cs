using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000732 RID: 1842
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FolderSaveTransientException : StorageTransientException
	{
		// Token: 0x060047DF RID: 18399 RVA: 0x001306B0 File Offset: 0x0012E8B0
		internal FolderSaveTransientException(LocalizedString message, FolderSaveResult folderSaveResult) : base(message, folderSaveResult.Exception)
		{
			this.folderSaveResult = folderSaveResult;
		}

		// Token: 0x170014DB RID: 5339
		// (get) Token: 0x060047E0 RID: 18400 RVA: 0x001306C6 File Offset: 0x0012E8C6
		public FolderSaveResult FolderSaveResult
		{
			get
			{
				return this.folderSaveResult;
			}
		}

		// Token: 0x04002736 RID: 10038
		private readonly FolderSaveResult folderSaveResult;
	}
}
