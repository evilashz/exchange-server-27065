using System;
using System.Diagnostics;
using System.Threading.Tasks.Dataflow;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Instrumentation;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Blocks
{
	// Token: 0x02000004 RID: 4
	internal abstract class BlockBase<TIn, TOut>
	{
		// Token: 0x0600001C RID: 28 RVA: 0x0000224C File Offset: 0x0000044C
		static BlockBase()
		{
			BlockBase<TIn, TOut>.watch.Start();
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000229E File Offset: 0x0000049E
		public static ExecutionDataflowBlockOptions DefaultExecutionOptions
		{
			get
			{
				return BlockBase<TIn, TOut>.defaultExecutionOptions;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000022A5 File Offset: 0x000004A5
		public static DataflowLinkOptions DefaultLinkOptions
		{
			get
			{
				return BlockBase<TIn, TOut>.defaultLinkOptions;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000022AC File Offset: 0x000004AC
		public virtual string BlockType
		{
			get
			{
				if (string.IsNullOrEmpty(this.blockType))
				{
					this.blockType = base.GetType().Name;
				}
				return this.blockType;
			}
		}

		// Token: 0x06000020 RID: 32
		public abstract TOut Process(TIn input);

		// Token: 0x06000021 RID: 33 RVA: 0x000022E4 File Offset: 0x000004E4
		public virtual TransformBlock<TIn, TOut> GetDataflowBlock(ExecutionDataflowBlockOptions options = null)
		{
			if (options != null)
			{
				return new TransformBlock<TIn, TOut>((TIn input) => this.MeterBlockProcessing(input), options);
			}
			return new TransformBlock<TIn, TOut>((TIn input) => this.MeterBlockProcessing(input), BlockBase<TIn, TOut>.DefaultExecutionOptions);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000232C File Offset: 0x0000052C
		protected virtual TOut MeterBlockProcessing(TIn input)
		{
			long elapsedMilliseconds = BlockBase<TIn, TOut>.watch.ElapsedMilliseconds;
			ComplianceMessage complianceMessage = input as ComplianceMessage;
			TOut result;
			try
			{
				if (complianceMessage != null)
				{
					MessageLogger.Instance.LogMessageBlockProcessing(complianceMessage, this.BlockType);
				}
				if (input != null)
				{
					result = this.Process(input);
				}
				else
				{
					result = default(TOut);
				}
			}
			finally
			{
				long elapsedMilliseconds2 = BlockBase<TIn, TOut>.watch.ElapsedMilliseconds;
				if (complianceMessage != null)
				{
					MessageLogger.Instance.LogMessageBlockProcessed(complianceMessage, this.BlockType, elapsedMilliseconds2 - elapsedMilliseconds);
				}
			}
			return result;
		}

		// Token: 0x04000014 RID: 20
		private static Stopwatch watch = new Stopwatch();

		// Token: 0x04000015 RID: 21
		private static DataflowLinkOptions defaultLinkOptions = new DataflowLinkOptions
		{
			PropagateCompletion = true
		};

		// Token: 0x04000016 RID: 22
		private static ExecutionDataflowBlockOptions defaultExecutionOptions = new ExecutionDataflowBlockOptions
		{
			MaxDegreeOfParallelism = -1,
			BoundedCapacity = TaskDistributionSettings.MaxQueuePerBlock
		};

		// Token: 0x04000017 RID: 23
		private string blockType;
	}
}
