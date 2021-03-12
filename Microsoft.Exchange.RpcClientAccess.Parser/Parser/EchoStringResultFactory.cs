using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000C8 RID: 200
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EchoStringResultFactory : StandardResultFactory
	{
		// Token: 0x06000476 RID: 1142 RVA: 0x0000ED73 File Offset: 0x0000CF73
		internal EchoStringResultFactory() : base(RopId.EchoString)
		{
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000ED80 File Offset: 0x0000CF80
		public RopResult CreateSuccessfulResult(string returnValue, string outParameter)
		{
			return new SuccessfulEchoStringResult(returnValue, outParameter);
		}
	}
}
