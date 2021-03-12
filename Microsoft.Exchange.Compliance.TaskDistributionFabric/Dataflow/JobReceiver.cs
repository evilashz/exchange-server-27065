using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;
using Microsoft.Exchange.Compliance.TaskDistributionFabric.Blocks;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Dataflow
{
	// Token: 0x02000010 RID: 16
	internal class JobReceiver : MessageReceiverBase
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00003203 File Offset: 0x00001403
		private JobReceiver()
		{
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003221 File Offset: 0x00001421
		public static JobReceiver Instance
		{
			get
			{
				return JobReceiver.instance;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003228 File Offset: 0x00001428
		protected override Task ReceiveMessageInternal(ComplianceMessage message)
		{
			TransformBlock<ComplianceMessage, IEnumerable<ComplianceMessage>> dataflowBlock = this.distributeBlock.GetDataflowBlock(null);
			TransformBlock<IEnumerable<ComplianceMessage>, object> dataflowBlock2 = this.sendBlock.GetDataflowBlock(null);
			dataflowBlock.LinkTo(dataflowBlock2, BlockBase<ComplianceMessage, IEnumerable<ComplianceMessage>>.DefaultLinkOptions);
			if (DataflowBlock.Post<ComplianceMessage>(dataflowBlock, message))
			{
				dataflowBlock.Complete();
				return dataflowBlock2.Completion;
			}
			return null;
		}

		// Token: 0x0400001C RID: 28
		private static JobReceiver instance = new JobReceiver();

		// Token: 0x0400001D RID: 29
		private DistributeBlock distributeBlock = new DistributeBlock();

		// Token: 0x0400001E RID: 30
		private SendBlock sendBlock = new SendBlock();
	}
}
