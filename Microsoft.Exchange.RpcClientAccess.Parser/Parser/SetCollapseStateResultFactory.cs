using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000122 RID: 290
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SetCollapseStateResultFactory : StandardResultFactory
	{
		// Token: 0x060005BD RID: 1469 RVA: 0x00010CB0 File Offset: 0x0000EEB0
		internal SetCollapseStateResultFactory() : base(RopId.SetCollapseState)
		{
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00010CBA File Offset: 0x0000EEBA
		public RopResult CreateSuccessfulResult(byte[] bookmark)
		{
			return new SuccessfulSetCollapseStateResult(bookmark);
		}
	}
}
