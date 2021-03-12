using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001AC RID: 428
	internal interface IConnectionHandler : IDisposable
	{
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000882 RID: 2178
		IRopHandler RopHandler { get; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000883 RID: 2179
		INotificationHandler NotificationHandler { get; }

		// Token: 0x06000884 RID: 2180
		void BeginRopProcessing(AuxiliaryData auxiliaryData);

		// Token: 0x06000885 RID: 2181
		void EndRopProcessing(AuxiliaryData auxiliaryData);

		// Token: 0x06000886 RID: 2182
		void LogInputRops(IEnumerable<RopId> rops);

		// Token: 0x06000887 RID: 2183
		void LogPrepareForRop(RopId ropId);

		// Token: 0x06000888 RID: 2184
		void LogCompletedRop(RopId ropId, ErrorCode errorCode);
	}
}
