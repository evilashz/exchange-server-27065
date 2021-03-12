using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000014 RID: 20
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ABOperationRetryManager : OperationRetryManagerBase
	{
		// Token: 0x0600009E RID: 158 RVA: 0x0000402F File Offset: 0x0000222F
		public ABOperationRetryManager(int maxRetryCount) : base(maxRetryCount, TimeSpan.FromSeconds(1.0), true)
		{
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004047 File Offset: 0x00002247
		public ABOperationRetryManager(int maxRetryCount, TimeSpan retryInterval, bool multiplyDurationByRetryIteration) : base(maxRetryCount, retryInterval, multiplyDurationByRetryIteration)
		{
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004054 File Offset: 0x00002254
		protected override bool InternalRun(Action operation, bool maxRetryReached)
		{
			try
			{
				operation();
				return true;
			}
			catch (TransientException arg)
			{
				if (maxRetryReached)
				{
					ExTraceGlobals.RetryManagerTracer.TraceError<TransientException>(0L, "{0}: Max retry count reached, will re-throw.", arg);
					throw;
				}
			}
			return false;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004098 File Offset: 0x00002298
		protected override OperationRetryManagerResult InternalTryRun(Action operation)
		{
			try
			{
				base.Run(operation);
			}
			catch (TransientException ex)
			{
				ExTraceGlobals.RetryManagerTracer.TraceError<TransientException>(0L, "AB operation failed with exception: {0}", ex);
				return new OperationRetryManagerResult(OperationRetryManagerResultCode.RetryableError, ex);
			}
			catch (DataSourceOperationException ex2)
			{
				ExTraceGlobals.RetryManagerTracer.TraceError<DataSourceOperationException>(0L, "AB operation failed with exception: {0}", ex2);
				return new OperationRetryManagerResult(OperationRetryManagerResultCode.PermanentError, ex2);
			}
			catch (DataValidationException ex3)
			{
				ExTraceGlobals.RetryManagerTracer.TraceError<DataValidationException>(0L, "AB operation failed with exception: {0}", ex3);
				return new OperationRetryManagerResult(OperationRetryManagerResultCode.PermanentError, ex3);
			}
			return OperationRetryManagerResult.Success;
		}
	}
}
