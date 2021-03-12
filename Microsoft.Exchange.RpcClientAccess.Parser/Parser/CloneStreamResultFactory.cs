using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000B3 RID: 179
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CloneStreamResultFactory : StandardResultFactory
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x0000E7CB File Offset: 0x0000C9CB
		internal CloneStreamResultFactory() : base(RopId.CloneStream)
		{
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000E7D5 File Offset: 0x0000C9D5
		public RopResult CreateSuccessfulResult(IServerObject serverObject)
		{
			return new SuccessfulCloneStreamResult(serverObject);
		}
	}
}
