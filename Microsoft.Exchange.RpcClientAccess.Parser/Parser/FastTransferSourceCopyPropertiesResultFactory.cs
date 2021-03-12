using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000D2 RID: 210
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FastTransferSourceCopyPropertiesResultFactory : StandardResultFactory
	{
		// Token: 0x06000496 RID: 1174 RVA: 0x0000EF68 File Offset: 0x0000D168
		internal FastTransferSourceCopyPropertiesResultFactory() : base(RopId.FastTransferSourceCopyProperties)
		{
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000EF72 File Offset: 0x0000D172
		public SuccessfulFastTransferSourceCopyPropertiesResult CreateSuccessfulResult(IServerObject serverObject)
		{
			return new SuccessfulFastTransferSourceCopyPropertiesResult(serverObject);
		}
	}
}
