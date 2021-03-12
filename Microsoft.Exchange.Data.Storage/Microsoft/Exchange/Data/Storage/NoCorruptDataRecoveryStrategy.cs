using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000664 RID: 1636
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class NoCorruptDataRecoveryStrategy : CorruptDataRecoveryStrategy
	{
		// Token: 0x060043BE RID: 17342 RVA: 0x0011F223 File Offset: 0x0011D423
		internal override void Recover(DefaultFolder defaultFolder, Exception e, ref DefaultFolderData defaultFolderData)
		{
			defaultFolder.RemoveForRecover(ref defaultFolderData);
		}
	}
}
