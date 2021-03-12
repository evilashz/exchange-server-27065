using System;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000022 RID: 34
	public interface IJobExecutionTracker : IProgress<short>
	{
		// Token: 0x060000B3 RID: 179
		void OnCorruptionDetected(Corruption corruption);
	}
}
