using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.ActiveManager;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000105 RID: 261
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IReplicaInstanceManager
	{
		// Token: 0x06000A34 RID: 2612
		bool TryWaitForFirstFullConfigUpdater();

		// Token: 0x06000A35 RID: 2613
		bool TryGetReplicaInstance(Guid guid, out ReplicaInstance instance);

		// Token: 0x06000A36 RID: 2614
		bool TryGetReplicaInstanceContainer(Guid guid, out ReplicaInstanceContainer instance);

		// Token: 0x06000A37 RID: 2615
		List<ReplicaInstance> GetAllReplicaInstances();

		// Token: 0x06000A38 RID: 2616
		List<ReplicaInstanceContainer> GetAllReplicaInstanceContainers();

		// Token: 0x06000A39 RID: 2617
		ISetVolumeInfo GetVolumeInfoCallback(Guid instanceGuid, bool activePassiveAgnostic);

		// Token: 0x06000A3A RID: 2618
		ISetSeeding GetSeederStatusCallback(Guid instanceGuid);

		// Token: 0x06000A3B RID: 2619
		IGetStatus GetStatusRetrieverCallback(Guid instanceGuid);

		// Token: 0x06000A3C RID: 2620
		bool GetRunningInstanceSignatureAndCheckpoint(Guid instanceGuid, out JET_SIGNATURE? logfileSignature, out long lowestGenerationRequired, out long highestGenerationRequired, out long lastGenerationBackedUp);

		// Token: 0x06000A3D RID: 2621
		CopyStatusEnum GetCopyStatus(Guid instanceGuid);

		// Token: 0x06000A3E RID: 2622
		ReplayState GetRunningInstanceState(Guid instanceGuid);

		// Token: 0x06000A3F RID: 2623
		bool CreateTempLogFileForRunningInstance(Guid instanceGuid);

		// Token: 0x06000A40 RID: 2624
		AmAcllReturnStatus AmAttemptCopyLastLogsCallback(Guid mdbGuid, AmAcllArgs acllArgs);

		// Token: 0x06000A41 RID: 2625
		void AmPreMountCallback(Guid mdbGuid, ref int storeMountFlags, AmMountFlags amMountFlags, MountDirectPerformanceTracker mountPerf, out LogStreamResetOnMount logReset, out ReplicaInstanceContext instanceContext);

		// Token: 0x06000A42 RID: 2626
		void RequestSuspend(Guid guid, string suspendComment, DatabaseCopyActionFlags flags, ActionInitiatorType initiator);

		// Token: 0x06000A43 RID: 2627
		void RequestResume(Guid guid, DatabaseCopyActionFlags flags);

		// Token: 0x06000A44 RID: 2628
		void SyncSuspendResumeState(Guid guid);

		// Token: 0x06000A45 RID: 2629
		void DisableReplayLag(Guid guid, ActionInitiatorType actionInitiator, string reason);

		// Token: 0x06000A46 RID: 2630
		void EnableReplayLag(Guid guid, ActionInitiatorType actionInitiator);

		// Token: 0x06000A47 RID: 2631
		ISetPassiveSeeding GetPassiveSeederStatusCallback(Guid instanceGuid);

		// Token: 0x06000A48 RID: 2632
		ISetActiveSeeding GetActiveSeederStatusCallback(Guid instanceGuid);

		// Token: 0x06000A49 RID: 2633
		ISetGeneration GetSetGenerationCallback(Guid instanceGuid);
	}
}
