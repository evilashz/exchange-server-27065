using System;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000011 RID: 17
	public interface IJobProgressTracker
	{
		// Token: 0x06000054 RID: 84
		void Report(ProgressInfo progress);
	}
}
