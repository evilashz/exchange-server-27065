using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Extensibility;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;
using Microsoft.Exchange.Compliance.TaskDistributionFabric.Blocks;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Dataflow
{
	// Token: 0x02000013 RID: 19
	internal class WorkReceiver : MessageReceiverBase
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00003566 File Offset: 0x00001766
		private WorkReceiver()
		{
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000052 RID: 82 RVA: 0x0000358F File Offset: 0x0000178F
		public static WorkReceiver Instance
		{
			get
			{
				return WorkReceiver.instance;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003598 File Offset: 0x00001798
		protected override Task ReceiveMessageInternal(ComplianceMessage message)
		{
			TaskScheduler @default = TaskScheduler.Default;
			FaultDefinition faultDefinition;
			Registry.Instance.TryGetInstance<TaskScheduler>(RegistryComponent.TaskDistribution, TaskDistributionComponent.ResourceScheduler, out @default, out faultDefinition, "ReceiveMessageInternal", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Dataflow\\WorkReceiver.cs", 77);
			TransformBlock<ComplianceMessage, ComplianceMessage> dataflowBlock = this.workBlock.GetDataflowBlock(new ExecutionDataflowBlockOptions
			{
				TaskScheduler = @default,
				MaxDegreeOfParallelism = -1
			});
			TransformBlock<ComplianceMessage, ComplianceMessage> dataflowBlock2 = this.recordBlock.GetDataflowBlock(null);
			TransformBlock<ComplianceMessage, object> dataflowBlock3 = this.returnBlock.GetDataflowBlock(null);
			dataflowBlock.LinkTo(dataflowBlock2, BlockBase<ComplianceMessage, ComplianceMessage>.DefaultLinkOptions);
			dataflowBlock2.LinkTo(dataflowBlock3, BlockBase<ComplianceMessage, ComplianceMessage>.DefaultLinkOptions);
			if (DataflowBlock.Post<ComplianceMessage>(dataflowBlock, message))
			{
				dataflowBlock.Complete();
				return dataflowBlock3.Completion;
			}
			return null;
		}

		// Token: 0x04000023 RID: 35
		private static WorkReceiver instance = new WorkReceiver();

		// Token: 0x04000024 RID: 36
		private WorkBlock workBlock = new WorkBlock();

		// Token: 0x04000025 RID: 37
		private RecordBlock recordBlock = new RecordBlock();

		// Token: 0x04000026 RID: 38
		private ReturnBlock returnBlock = new ReturnBlock();
	}
}
