using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DBD RID: 3517
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class UnableToGenerateEncryptedSharedFolderDataException : StoragePermanentException
	{
		// Token: 0x060078D9 RID: 30937 RVA: 0x00215F20 File Offset: 0x00214120
		public UnableToGenerateEncryptedSharedFolderDataException(Exception innerException) : base(ServerStrings.SharingUnableToGenerateEncryptedSharedFolderData, innerException)
		{
		}
	}
}
