using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200003F RID: 63
	public interface IExecutionTrackingData<TOperationData> where TOperationData : class
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000478 RID: 1144
		int Count { get; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000479 RID: 1145
		TimeSpan TotalTime { get; }

		// Token: 0x0600047A RID: 1146
		void Aggregate(TOperationData dataToAggregate);

		// Token: 0x0600047B RID: 1147
		void AppendToTraceContentBuilder(TraceContentBuilder cb);

		// Token: 0x0600047C RID: 1148
		void AppendDetailsToTraceContentBuilder(TraceContentBuilder cb, int indentLevel);
	}
}
