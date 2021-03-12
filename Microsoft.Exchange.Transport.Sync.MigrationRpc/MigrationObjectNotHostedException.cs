using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x02000009 RID: 9
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationObjectNotHostedException : MigrationServiceRpcException
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002367 File Offset: 0x00000567
		internal MigrationObjectNotHostedException(MigrationServiceRpcResultCode resultCode, string message) : base(resultCode, message)
		{
		}
	}
}
