using System;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002CF RID: 719
	internal interface IFileChecker
	{
		// Token: 0x06001C1D RID: 7197
		bool RunChecks();

		// Token: 0x06001C1E RID: 7198
		bool RecalculateRequiredGenerations(ref JET_DBINFOMISC dbinfo);

		// Token: 0x06001C1F RID: 7199
		bool RecalculateRequiredGenerations();

		// Token: 0x06001C20 RID: 7200
		bool CheckRequiredLogfilesForPassiveOrInconsistentDatabase(bool checkForReplay);

		// Token: 0x06001C21 RID: 7201
		void CheckCheckpoint();

		// Token: 0x06001C22 RID: 7202
		void PrepareToStop();

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001C23 RID: 7203
		FileState FileState { get; }
	}
}
