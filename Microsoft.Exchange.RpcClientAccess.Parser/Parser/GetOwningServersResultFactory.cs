using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000E3 RID: 227
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetOwningServersResultFactory : StandardResultFactory
	{
		// Token: 0x060004CD RID: 1229 RVA: 0x0000F235 File Offset: 0x0000D435
		internal GetOwningServersResultFactory() : base(RopId.GetOwningServers)
		{
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0000F23F File Offset: 0x0000D43F
		public RopResult CreateSuccessfulResult(ReplicaServerInfo replicaInfo)
		{
			return new SuccessfulGetOwningServersResult(replicaInfo);
		}
	}
}
