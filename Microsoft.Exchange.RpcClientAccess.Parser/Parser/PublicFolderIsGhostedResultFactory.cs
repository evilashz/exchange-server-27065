using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200010C RID: 268
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderIsGhostedResultFactory : StandardResultFactory
	{
		// Token: 0x06000577 RID: 1399 RVA: 0x000103E8 File Offset: 0x0000E5E8
		internal PublicFolderIsGhostedResultFactory() : base(RopId.PublicFolderIsGhosted)
		{
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000103F2 File Offset: 0x0000E5F2
		public RopResult CreateSuccessfulResult(ReplicaServerInfo? replicaInfo)
		{
			return new SuccessfulPublicFolderIsGhostedResult(replicaInfo);
		}
	}
}
