using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;
using Microsoft.Exchange.Compliance.TaskDistributionFabric.Blocks;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Dataflow
{
	// Token: 0x0200000F RID: 15
	internal class ResultReceiver : MessageReceiverBase
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00003185 File Offset: 0x00001385
		private ResultReceiver()
		{
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000031A3 File Offset: 0x000013A3
		public static ResultReceiver Instance
		{
			get
			{
				return ResultReceiver.instance;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000031AC File Offset: 0x000013AC
		protected override Task ReceiveMessageInternal(ComplianceMessage message)
		{
			TransformBlock<ComplianceMessage, ComplianceMessage> dataflowBlock = this.recordBlock.GetDataflowBlock(null);
			TransformBlock<ComplianceMessage, object> dataflowBlock2 = this.returnBlock.GetDataflowBlock(null);
			dataflowBlock.LinkTo(dataflowBlock2, BlockBase<ComplianceMessage, ComplianceMessage>.DefaultLinkOptions);
			if (DataflowBlock.Post<ComplianceMessage>(dataflowBlock, message))
			{
				dataflowBlock.Complete();
				return dataflowBlock2.Completion;
			}
			return null;
		}

		// Token: 0x04000019 RID: 25
		private static ResultReceiver instance = new ResultReceiver();

		// Token: 0x0400001A RID: 26
		private RecordBlock recordBlock = new RecordBlock();

		// Token: 0x0400001B RID: 27
		private ReturnBlock returnBlock = new ReturnBlock();
	}
}
