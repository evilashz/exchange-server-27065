using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x02000013 RID: 19
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationTargetInvocationException : MigrationServiceRpcException
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002BB0 File Offset: 0x00000DB0
		internal MigrationTargetInvocationException(MigrationServiceRpcResultCode resultCode, string message) : base(resultCode, message)
		{
		}
	}
}
