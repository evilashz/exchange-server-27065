using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x02000007 RID: 7
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationCommunicationException : MigrationServiceRpcException
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002140 File Offset: 0x00000340
		internal MigrationCommunicationException(MigrationServiceRpcResultCode resultCode, string message) : base(resultCode, message)
		{
		}
	}
}
