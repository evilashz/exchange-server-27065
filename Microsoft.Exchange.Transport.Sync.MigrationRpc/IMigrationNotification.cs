using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMigrationNotification
	{
		// Token: 0x0600000A RID: 10
		UpdateMigrationRequestResult UpdateMigrationRequest(UpdateMigrationRequestArgs args);

		// Token: 0x0600000B RID: 11
		RegisterMigrationBatchResult RegisterMigrationBatch(RegisterMigrationBatchArgs args);
	}
}
