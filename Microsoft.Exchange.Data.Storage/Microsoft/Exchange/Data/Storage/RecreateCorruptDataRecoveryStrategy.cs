using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000666 RID: 1638
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class RecreateCorruptDataRecoveryStrategy : CorruptDataRecoveryStrategy
	{
		// Token: 0x060043C2 RID: 17346 RVA: 0x0011F240 File Offset: 0x0011D440
		internal override void Recover(DefaultFolder defaultFolder, Exception e, ref DefaultFolderData defaultFolderData)
		{
			try
			{
				defaultFolder.RemoveForRecover(ref defaultFolderData);
				defaultFolder.CreateInternal(ref defaultFolderData);
			}
			catch (StoragePermanentException innerException)
			{
				throw new CorruptDataException(ServerStrings.ExCorruptDataRecoverError(defaultFolder.ToString()), innerException);
			}
			catch (StorageTransientException innerException2)
			{
				throw new CorruptDataException(ServerStrings.ExCorruptDataRecoverError(defaultFolder.ToString()), innerException2);
			}
		}
	}
}
