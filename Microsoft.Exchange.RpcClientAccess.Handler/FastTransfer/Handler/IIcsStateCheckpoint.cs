using System;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x0200006A RID: 106
	internal interface IIcsStateCheckpoint
	{
		// Token: 0x0600045E RID: 1118
		IFastTransferProcessor<FastTransferDownloadContext> CreateIcsStateCheckpointFastTransferObject();
	}
}
