using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;
using Microsoft.Exchange.Compliance.TaskDistributionFabric.Blocks;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Dataflow
{
	// Token: 0x02000012 RID: 18
	internal class DriverProcessor : MessageProcessorBase
	{
		// Token: 0x0600004D RID: 77 RVA: 0x000032C8 File Offset: 0x000014C8
		private DriverProcessor()
		{
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000032F1 File Offset: 0x000014F1
		public static DriverProcessor Instance
		{
			get
			{
				return DriverProcessor.instance;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000350C File Offset: 0x0000170C
		protected override async Task<ComplianceMessage> ProcessMessageInternal(ComplianceMessage message)
		{
			TransformBlock<ComplianceMessage, ComplianceMessage> transformer = null;
			switch (message.ComplianceMessageType)
			{
			case ComplianceMessageType.RecordResult:
				transformer = this.storeBlock.GetDataflowBlock(null);
				goto IL_B1;
			case ComplianceMessageType.RetrieveRequest:
				transformer = this.retrievePayloadBlock.GetDataflowBlock(null);
				goto IL_B1;
			case ComplianceMessageType.EchoRequest:
				transformer = this.echoBlock.GetDataflowBlock(null);
				goto IL_B1;
			}
			throw new ArgumentException(string.Format("MessageType:{0} is not supported", message.ComplianceMessageType));
			IL_B1:
			await DataflowBlock.SendAsync<ComplianceMessage>(transformer, message);
			ComplianceMessage response = await DataflowBlock.ReceiveAsync<ComplianceMessage>(transformer);
			transformer.Complete();
			return response;
		}

		// Token: 0x0400001F RID: 31
		private static DriverProcessor instance = new DriverProcessor();

		// Token: 0x04000020 RID: 32
		private EchoRequestBlock echoBlock = new EchoRequestBlock();

		// Token: 0x04000021 RID: 33
		private RetrievePayloadBlock retrievePayloadBlock = new RetrievePayloadBlock();

		// Token: 0x04000022 RID: 34
		private StoreResultsBlock storeBlock = new StoreResultsBlock();
	}
}
