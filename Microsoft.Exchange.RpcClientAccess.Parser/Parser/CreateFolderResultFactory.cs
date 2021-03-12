using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000BE RID: 190
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CreateFolderResultFactory : StandardResultFactory
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x0000EBD6 File Offset: 0x0000CDD6
		internal CreateFolderResultFactory() : base(RopId.CreateFolder)
		{
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000EBE0 File Offset: 0x0000CDE0
		public RopResult CreateSuccessfulResult(IServerObject serverObject, StoreId folderId, bool existed, bool hasRules, ReplicaServerInfo? replicaInfo)
		{
			return new SuccessfulCreateFolderResult(serverObject, folderId, existed, hasRules, replicaInfo);
		}
	}
}
