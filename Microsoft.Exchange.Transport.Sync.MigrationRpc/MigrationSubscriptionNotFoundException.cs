using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x0200000A RID: 10
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationSubscriptionNotFoundException : MigrationServiceRpcException
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002371 File Offset: 0x00000571
		internal MigrationSubscriptionNotFoundException(MigrationServiceRpcResultCode resultCode, string message) : base(resultCode, message)
		{
		}
	}
}
