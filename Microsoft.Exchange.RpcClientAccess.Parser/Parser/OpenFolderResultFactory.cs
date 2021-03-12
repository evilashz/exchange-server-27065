using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000106 RID: 262
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OpenFolderResultFactory : StandardResultFactory
	{
		// Token: 0x0600054B RID: 1355 RVA: 0x0000FD75 File Offset: 0x0000DF75
		internal OpenFolderResultFactory() : base(RopId.OpenFolder)
		{
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0000FD7E File Offset: 0x0000DF7E
		public RopResult CreateSuccessfulResult(IServerObject folder, bool hasRules, ReplicaServerInfo? replicaInfo)
		{
			return new SuccessfulOpenFolderResult(folder, hasRules, replicaInfo);
		}
	}
}
