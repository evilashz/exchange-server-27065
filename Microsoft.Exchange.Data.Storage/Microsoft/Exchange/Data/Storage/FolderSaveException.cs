using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000731 RID: 1841
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FolderSaveException : StoragePermanentException
	{
		// Token: 0x060047DD RID: 18397 RVA: 0x00130692 File Offset: 0x0012E892
		public FolderSaveException(LocalizedString message, FolderSaveResult folderSaveResult) : base(message, folderSaveResult.Exception)
		{
			this.folderSaveResult = folderSaveResult;
		}

		// Token: 0x170014DA RID: 5338
		// (get) Token: 0x060047DE RID: 18398 RVA: 0x001306A8 File Offset: 0x0012E8A8
		public FolderSaveResult FolderSaveResult
		{
			get
			{
				return this.folderSaveResult;
			}
		}

		// Token: 0x04002735 RID: 10037
		private readonly FolderSaveResult folderSaveResult;
	}
}
