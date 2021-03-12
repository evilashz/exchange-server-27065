using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000261 RID: 609
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class OperationRetryManagerBase : IOperationRetryManager
	{
		// Token: 0x06001478 RID: 5240 RVA: 0x0004077A File Offset: 0x0003E97A
		public OperationRetryManagerBase(int maxRetryCount) : this(maxRetryCount, TimeSpan.FromSeconds(1.0), true)
		{
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x00040792 File Offset: 0x0003E992
		public OperationRetryManagerBase(int maxRetryCount, TimeSpan retryInterval, bool multiplyDurationByRetryIteration)
		{
			if (maxRetryCount < 0)
			{
				throw new ArgumentOutOfRangeException("maxRetryCount", "maxRetryCount must be greater than or equal to 0.");
			}
			this.maxRetryCount = maxRetryCount;
			this.retryInterval = retryInterval;
			this.multiplyDurationByRetryIteration = multiplyDurationByRetryIteration;
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x000407C4 File Offset: 0x0003E9C4
		public void Run(Action operation)
		{
			if (operation == null)
			{
				throw new ArgumentNullException("operation");
			}
			for (int i = 0; i <= this.maxRetryCount; i++)
			{
				if (this.InternalRun(operation, i == this.maxRetryCount))
				{
					return;
				}
				TimeSpan duration;
				if (this.multiplyDurationByRetryIteration)
				{
					duration = TimeSpan.FromMilliseconds((double)(i * (int)this.retryInterval.TotalMilliseconds));
				}
				else
				{
					duration = ((i == 0) ? TimeSpan.Zero : this.retryInterval);
				}
				this.Sleep(duration);
			}
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0004083D File Offset: 0x0003EA3D
		public OperationRetryManagerResult TryRun(Action operation)
		{
			if (operation == null)
			{
				throw new ArgumentNullException("operation");
			}
			return this.InternalTryRun(operation);
		}

		// Token: 0x0600147C RID: 5244
		protected abstract bool InternalRun(Action operation, bool maxRetryReached);

		// Token: 0x0600147D RID: 5245
		protected abstract OperationRetryManagerResult InternalTryRun(Action operation);

		// Token: 0x0600147E RID: 5246 RVA: 0x00040854 File Offset: 0x0003EA54
		protected virtual void Sleep(TimeSpan duration)
		{
			Thread.Sleep(duration);
		}

		// Token: 0x04000C01 RID: 3073
		private readonly TimeSpan retryInterval;

		// Token: 0x04000C02 RID: 3074
		private readonly int maxRetryCount;

		// Token: 0x04000C03 RID: 3075
		private readonly bool multiplyDurationByRetryIteration;
	}
}
