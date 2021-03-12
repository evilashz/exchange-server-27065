using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002B3 RID: 691
	internal interface ILogReader
	{
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001362 RID: 4962
		string Server { get; }

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001363 RID: 4963
		MtrSchemaVersion MtrSchemaVersion { get; }

		// Token: 0x06001364 RID: 4964
		List<MessageTrackingLogEntry> ReadLogs(RpcReason rpcReason, string logFile, string messageId, DateTime startTime, DateTime endTime, TrackingEventBudget eventBudget);

		// Token: 0x06001365 RID: 4965
		List<MessageTrackingLogEntry> ReadLogs(RpcReason rpcReason, string logFilePrefix, ProxyAddressCollection senderProxyAddresses, DateTime startTime, DateTime endTime, TrackingEventBudget eventBudget);
	}
}
