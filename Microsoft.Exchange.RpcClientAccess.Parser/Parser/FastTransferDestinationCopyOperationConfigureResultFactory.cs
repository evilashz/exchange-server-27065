using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000CB RID: 203
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FastTransferDestinationCopyOperationConfigureResultFactory : StandardResultFactory
	{
		// Token: 0x06000484 RID: 1156 RVA: 0x0000EEA4 File Offset: 0x0000D0A4
		internal FastTransferDestinationCopyOperationConfigureResultFactory() : base(RopId.FastTransferDestinationCopyOperationConfigure)
		{
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000EEAE File Offset: 0x0000D0AE
		public RopResult CreateSuccessfulResult(IServerObject serverObject)
		{
			return new SuccessfulFastTransferDestinationCopyOperationConfigureResult(serverObject);
		}
	}
}
