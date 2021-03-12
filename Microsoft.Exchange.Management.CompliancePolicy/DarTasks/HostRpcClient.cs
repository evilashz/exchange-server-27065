using System;
using Microsoft.Exchange.Data.Storage.Compliance.DAR;
using Microsoft.Exchange.Management.CompliancePolicy.DarTasks.Protocol;
using Microsoft.Exchange.Rpc.Dar;

namespace Microsoft.Exchange.Management.CompliancePolicy.DarTasks
{
	// Token: 0x0200000F RID: 15
	internal class HostRpcClient : ExDarHostRpcClient
	{
		// Token: 0x06000079 RID: 121 RVA: 0x000033E4 File Offset: 0x000015E4
		public HostRpcClient(string machineName) : base(machineName)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000033ED File Offset: 0x000015ED
		public void NotifyTaskStoreChange(byte[] tenantId)
		{
			base.SendHostRequest(0, 0, tenantId);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000033F9 File Offset: 0x000015F9
		public void EnsureTenantMonitoring(byte[] tenantId)
		{
			base.SendHostRequest(0, 1, tenantId);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003408 File Offset: 0x00001608
		public DarTaskResult SendDarHostRequest(int version, int type, byte[] inputParameterBytes)
		{
			byte[] data = base.SendHostRequest(version, type, inputParameterBytes);
			return DarTaskResult.GetResultObject(data);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003428 File Offset: 0x00001628
		public TaskStoreObject[] GetDarTask(DarTaskParams darParams)
		{
			DarTaskResult darTaskResult = this.SendDarHostRequest(0, 2, darParams.ToBytes());
			return darTaskResult.DarTasks;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000344C File Offset: 0x0000164C
		public void SetDarTask(TaskStoreObject darTaskObject)
		{
			byte[] inputParameterBytes = DarTaskResult.ObjectToBytes<TaskStoreObject>(darTaskObject);
			this.SendDarHostRequest(0, 3, inputParameterBytes);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000346C File Offset: 0x0000166C
		public TaskAggregateStoreObject[] GetDarTaskAggregate(DarTaskAggregateParams darTaskAggregateParams)
		{
			DarTaskResult darTaskResult = this.SendDarHostRequest(0, 4, darTaskAggregateParams.ToBytes());
			return darTaskResult.DarTaskAggregates;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003490 File Offset: 0x00001690
		public void SetDarTaskAggregate(TaskAggregateStoreObject darTaskObject)
		{
			byte[] inputParameterBytes = DarTaskResult.ObjectToBytes<TaskAggregateStoreObject>(darTaskObject);
			this.SendDarHostRequest(0, 5, inputParameterBytes);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000034AE File Offset: 0x000016AE
		public void RemoveCompletedDarTasks(DarTaskParams darParams)
		{
			this.SendDarHostRequest(0, 6, darParams.ToBytes());
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000034BF File Offset: 0x000016BF
		public void RemoveDarTaskAggregate(DarTaskAggregateParams darTaskAggregateParams)
		{
			this.SendDarHostRequest(0, 7, darTaskAggregateParams.ToBytes());
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000034D0 File Offset: 0x000016D0
		public string GetDarInfo()
		{
			int version = 0;
			int type = 8;
			byte[] inputParameterBytes = new byte[1];
			DarTaskResult darTaskResult = this.SendDarHostRequest(version, type, inputParameterBytes);
			return darTaskResult.LocalizedInformation;
		}
	}
}
