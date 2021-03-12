using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000B6 RID: 182
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IProgressResultFactory
	{
		// Token: 0x0600042F RID: 1071
		RopResult CreateProgressResult(uint completedTaskCount, uint totalTaskCount);
	}
}
