using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000665 RID: 1637
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ThrowCorruptDataRecoveryStrategy : CorruptDataRecoveryStrategy
	{
		// Token: 0x060043C0 RID: 17344 RVA: 0x0011F234 File Offset: 0x0011D434
		internal override void Recover(DefaultFolder defaultFolder, Exception e, ref DefaultFolderData defaultFolderData)
		{
			throw e;
		}
	}
}
