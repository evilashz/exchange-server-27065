using System;
using System.Threading.Tasks.Dataflow;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Blocks
{
	// Token: 0x02000007 RID: 7
	internal abstract class FinalBlockBase<TIn> : BlockBase<TIn, object>
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002970 File Offset: 0x00000B70
		public override object Process(TIn input)
		{
			this.ProcessFinal(input);
			return null;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000297C File Offset: 0x00000B7C
		public override TransformBlock<TIn, object> GetDataflowBlock(ExecutionDataflowBlockOptions options = null)
		{
			TransformBlock<TIn, object> dataflowBlock = base.GetDataflowBlock(options);
			ITargetBlock<object> targetBlock = new ActionBlock<object>(delegate(object o)
			{
			});
			DataflowBlock.LinkTo<object>(dataflowBlock, targetBlock);
			return dataflowBlock;
		}

		// Token: 0x0600002C RID: 44
		protected abstract void ProcessFinal(TIn input);
	}
}
