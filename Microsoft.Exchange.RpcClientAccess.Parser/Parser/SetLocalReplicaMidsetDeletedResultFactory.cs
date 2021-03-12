using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000124 RID: 292
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SetLocalReplicaMidsetDeletedResultFactory : StandardResultFactory
	{
		// Token: 0x060005C1 RID: 1473 RVA: 0x00010CD4 File Offset: 0x0000EED4
		internal SetLocalReplicaMidsetDeletedResultFactory() : base(RopId.SetLocalReplicaMidsetDeleted)
		{
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00010CE1 File Offset: 0x0000EEE1
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.SetLocalReplicaMidsetDeleted, ErrorCode.None);
		}
	}
}
