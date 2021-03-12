using System;
using Microsoft.Exchange.Data.Storage.Optics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000895 RID: 2197
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ConversationAggregationPerformanceTracker : PerformanceTrackerBase, IMailboxPerformanceTracker, IPerformanceTracker
	{
		// Token: 0x0600521D RID: 21021 RVA: 0x0015779C File Offset: 0x0015599C
		public ConversationAggregationPerformanceTracker(IMailboxSession mailboxSession) : base(mailboxSession)
		{
		}

		// Token: 0x0600521E RID: 21022 RVA: 0x001577A8 File Offset: 0x001559A8
		public ILogEvent GetLogEvent(string operationName)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("operationName", operationName);
			base.EnforceInternalState(PerformanceTrackerBase.InternalState.Stopped, "GetLogEvent");
			return new SchemaBasedLogEvent<ConversationAggregationLogSchema.OperationEnd>
			{
				{
					ConversationAggregationLogSchema.OperationEnd.OperationName,
					operationName
				},
				{
					ConversationAggregationLogSchema.OperationEnd.Elapsed,
					base.ElapsedTime.TotalMilliseconds
				},
				{
					ConversationAggregationLogSchema.OperationEnd.CPU,
					base.CpuTime.TotalMilliseconds
				},
				{
					ConversationAggregationLogSchema.OperationEnd.RPCCount,
					base.StoreRpcCount
				},
				{
					ConversationAggregationLogSchema.OperationEnd.RPCLatency,
					base.StoreRpcLatency.TotalMilliseconds
				},
				{
					ConversationAggregationLogSchema.OperationEnd.DirectoryCount,
					base.DirectoryCount
				},
				{
					ConversationAggregationLogSchema.OperationEnd.DirectoryLatency,
					base.DirectoryLatency.TotalMilliseconds
				},
				{
					ConversationAggregationLogSchema.OperationEnd.StoreTimeInServer,
					base.StoreTimeInServer.TotalMilliseconds
				},
				{
					ConversationAggregationLogSchema.OperationEnd.StoreTimeInCPU,
					base.StoreTimeInCPU.TotalMilliseconds
				},
				{
					ConversationAggregationLogSchema.OperationEnd.StorePagesRead,
					base.StorePagesRead
				},
				{
					ConversationAggregationLogSchema.OperationEnd.StorePagesPreRead,
					base.StorePagesPreread
				},
				{
					ConversationAggregationLogSchema.OperationEnd.StoreLogRecords,
					base.StoreLogRecords
				},
				{
					ConversationAggregationLogSchema.OperationEnd.StoreLogBytes,
					base.StoreLogBytes
				}
			};
		}
	}
}
