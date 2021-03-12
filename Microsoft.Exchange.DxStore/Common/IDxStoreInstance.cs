using System;
using System.ServiceModel;
using System.Threading.Tasks;
using FUSE.Paxos;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000052 RID: 82
	[ServiceContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	public interface IDxStoreInstance
	{
		// Token: 0x060002CA RID: 714
		[OperationContract]
		void Stop(bool isFlush = true);

		// Token: 0x060002CB RID: 715
		[OperationContract]
		void Flush();

		// Token: 0x060002CC RID: 716
		[OperationContract]
		void Reconfigure(InstanceGroupMemberConfig[] members);

		// Token: 0x060002CD RID: 717
		[OperationContract]
		InstanceStatusInfo GetStatus();

		// Token: 0x060002CE RID: 718
		[OperationContract]
		InstanceSnapshotInfo AcquireSnapshot(string fullKeyName = null, bool isCompress = true);

		// Token: 0x060002CF RID: 719
		[OperationContract]
		void ApplySnapshot(InstanceSnapshotInfo snapshot, bool isForce = false);

		// Token: 0x060002D0 RID: 720
		[OperationContract]
		void TryBecomeLeader();

		// Token: 0x060002D1 RID: 721
		[OperationContract]
		void NotifyInitiator(Guid commandId, string sender, int instanceNumber, bool isSucceeded, string errorMessage);

		// Token: 0x060002D2 RID: 722
		[OperationContract]
		Task PaxosMessageAsync(string sender, Message message);
	}
}
