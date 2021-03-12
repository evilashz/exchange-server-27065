using System;
using System.Threading;
using Microsoft.Exchange.LogUploaderProxy;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000014 RID: 20
	internal interface ILogMonitorHelper<T> where T : LogDataBatch
	{
		// Token: 0x060000EC RID: 236
		CsvTable GetLogSchema(Version version);

		// Token: 0x060000ED RID: 237
		T CreateBatch(int batchSizeInBytes, long batchBeginOffset, string fullLogName, string logPrefix);

		// Token: 0x060000EE RID: 238
		DatabaseWriter<T> CreateDBWriter(ThreadSafeQueue<T> queue, int id, ConfigInstance config, string instanceName);

		// Token: 0x060000EF RID: 239
		string GetDefaultLogVersion();

		// Token: 0x060000F0 RID: 240
		bool ShouldProcessLogFile(string logPrefix, string fileName);

		// Token: 0x060000F1 RID: 241
		void Initialize(CancellationToken cancellationToken);
	}
}
