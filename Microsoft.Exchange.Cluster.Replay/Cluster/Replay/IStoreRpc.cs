using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200031E RID: 798
	internal interface IStoreRpc : IListMDBStatus, IStoreMountDismount, IDisposable
	{
		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x060020C5 RID: 8389
		TimeSpan ConnectivityTimeout { get; }

		// Token: 0x060020C6 RID: 8390
		void ForceNewLog(Guid guidMdb, long numLogsToRoll);

		// Token: 0x060020C7 RID: 8391
		void LogReplayRequest(Guid guidMdb, uint ulgenLogReplayMax, uint flags, out uint ulgenLogReplayNext, out JET_DBINFOMISC dbinfo, out IPagePatchReply pagePatchReply, out uint[] corruptedPages);

		// Token: 0x060020C8 RID: 8392
		void StartBlockModeReplicationToPassive(Guid guidMdb, string passiveName, uint ulFirstGenToSend);

		// Token: 0x060020C9 RID: 8393
		bool TestStoreConnectivity(TimeSpan timeout, out LocalizedException ex);

		// Token: 0x060020CA RID: 8394
		void SnapshotPrepare(Guid dbGuid, uint flags);

		// Token: 0x060020CB RID: 8395
		void SnapshotFreeze(Guid dbGuid, uint flags);

		// Token: 0x060020CC RID: 8396
		void SnapshotThaw(Guid dbGuid, uint flags);

		// Token: 0x060020CD RID: 8397
		void SnapshotTruncateLogInstance(Guid dbGuid, uint flags);

		// Token: 0x060020CE RID: 8398
		void SnapshotStop(Guid dbGuid, uint flags);

		// Token: 0x060020CF RID: 8399
		void GetDatabaseInformation(Guid guidMdb, out JET_DBINFOMISC databaseInformation);

		// Token: 0x060020D0 RID: 8400
		void GetDatabaseProcessInfo(Guid guidMdb, out int workerProcessId, out int minVersion, out int maxVersion, out int requestedVersion);

		// Token: 0x060020D1 RID: 8401
		void Close();
	}
}
