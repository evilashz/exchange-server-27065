using System;
using System.Diagnostics;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000027 RID: 39
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class TimedOperationRunner
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x00006ACA File Offset: 0x00004CCA
		public TimedOperationRunner(ILogger logger, TimeSpan slowOperationThreshold)
		{
			this.logger = logger;
			this.slowOperationThreshold = slowOperationThreshold;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00006AE0 File Offset: 0x00004CE0
		public TResult RunOperation<TResult>(Func<TResult> operation, object debugInfo)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			TResult result;
			try
			{
				result = operation();
			}
			finally
			{
				stopwatch.Stop();
				if (this.slowOperationThreshold < stopwatch.Elapsed)
				{
					this.logger.Log(MigrationEventType.Error, "SLOW Operation: took {0}s using '{1}' stack trace {2}", new object[]
					{
						stopwatch.Elapsed.Seconds,
						debugInfo,
						AnchorUtil.GetCurrentStackTrace()
					});
				}
				else
				{
					this.logger.Log(MigrationEventType.Instrumentation, "Operation: took {0} using '{1}'", new object[]
					{
						stopwatch.Elapsed,
						debugInfo
					});
				}
			}
			return result;
		}

		// Token: 0x04000081 RID: 129
		private readonly ILogger logger;

		// Token: 0x04000082 RID: 130
		private readonly TimeSpan slowOperationThreshold;
	}
}
