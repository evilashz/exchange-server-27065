using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000668 RID: 1640
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class LegalHoldRecreateCorruptRecoveryStrategy : CorruptDataRecoveryStrategy
	{
		// Token: 0x060043C6 RID: 17350 RVA: 0x0011F2FC File Offset: 0x0011D4FC
		internal override void Recover(DefaultFolder defaultFolder, Exception e, ref DefaultFolderData defaultFolderData)
		{
			if (e is DefaultFolderPropertyValidationException && defaultFolderData.FolderId != null)
			{
				using (Folder folder = Folder.Bind(defaultFolder.Session, defaultFolderData.FolderId))
				{
					defaultFolder.SetProperties(folder);
				}
				return;
			}
			COWSettings cowsettings = new COWSettings(defaultFolder.Session);
			if (cowsettings.HoldEnabled())
			{
				CorruptDataRecoveryStrategy.Throw.Recover(defaultFolder, e, ref defaultFolderData);
				return;
			}
			CorruptDataRecoveryStrategy.Recreate.Recover(defaultFolder, e, ref defaultFolderData);
		}
	}
}
