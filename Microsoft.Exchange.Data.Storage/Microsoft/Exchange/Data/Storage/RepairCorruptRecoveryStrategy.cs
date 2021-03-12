using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000667 RID: 1639
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class RepairCorruptRecoveryStrategy : CorruptDataRecoveryStrategy
	{
		// Token: 0x060043C4 RID: 17348 RVA: 0x0011F2A8 File Offset: 0x0011D4A8
		internal override void Recover(DefaultFolder defaultFolder, Exception e, ref DefaultFolderData defaultFolderData)
		{
			if (defaultFolderData.FolderId != null)
			{
				using (Folder folder = Folder.Bind(defaultFolder.Session, defaultFolderData.FolderId))
				{
					defaultFolder.SetProperties(folder);
				}
			}
		}
	}
}
