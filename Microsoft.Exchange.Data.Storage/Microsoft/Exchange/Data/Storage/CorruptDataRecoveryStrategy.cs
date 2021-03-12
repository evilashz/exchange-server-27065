using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000663 RID: 1635
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class CorruptDataRecoveryStrategy
	{
		// Token: 0x060043BB RID: 17339 RVA: 0x0011F1E7 File Offset: 0x0011D3E7
		internal CorruptDataRecoveryStrategy()
		{
		}

		// Token: 0x060043BC RID: 17340
		internal abstract void Recover(DefaultFolder defaultFolder, Exception e, ref DefaultFolderData defaultFolderData);

		// Token: 0x040024D8 RID: 9432
		internal static NoCorruptDataRecoveryStrategy DoNothing = new NoCorruptDataRecoveryStrategy();

		// Token: 0x040024D9 RID: 9433
		internal static RecreateCorruptDataRecoveryStrategy Recreate = new RecreateCorruptDataRecoveryStrategy();

		// Token: 0x040024DA RID: 9434
		internal static ThrowCorruptDataRecoveryStrategy Throw = new ThrowCorruptDataRecoveryStrategy();

		// Token: 0x040024DB RID: 9435
		internal static LegalHoldRecreateCorruptRecoveryStrategy LegalHold = new LegalHoldRecreateCorruptRecoveryStrategy();

		// Token: 0x040024DC RID: 9436
		internal static RepairCorruptRecoveryStrategy Repair = new RepairCorruptRecoveryStrategy();
	}
}
