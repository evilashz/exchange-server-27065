using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x0200031E RID: 798
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct CopyInfo
	{
		// Token: 0x04001531 RID: 5425
		public DatabaseCopy Copy;

		// Token: 0x04001532 RID: 5426
		public RpcDatabaseCopyStatus2 Status;
	}
}
