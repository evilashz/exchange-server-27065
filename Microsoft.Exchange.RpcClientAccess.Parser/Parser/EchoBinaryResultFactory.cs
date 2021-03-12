using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000C6 RID: 198
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EchoBinaryResultFactory : StandardResultFactory
	{
		// Token: 0x06000472 RID: 1138 RVA: 0x0000ED47 File Offset: 0x0000CF47
		internal EchoBinaryResultFactory() : base(RopId.EchoBinary)
		{
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000ED54 File Offset: 0x0000CF54
		public RopResult CreateSuccessfulResult(int returnValue, byte[] outParameter)
		{
			return new SuccessfulEchoBinaryResult(returnValue, outParameter);
		}
	}
}
