using System;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000010 RID: 16
	public interface IJobStateTracker
	{
		// Token: 0x06000053 RID: 83
		void MoveToState(JobState state);
	}
}
