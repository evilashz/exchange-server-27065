using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Office.CompliancePolicy.Exchange.Dar.Diagnostics
{
	// Token: 0x02000016 RID: 22
	internal class ExceptionHandler
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00004911 File Offset: 0x00002B11
		protected ExceptionHandler(Action code, ExceptionGroupHandler groupHandler, ExceptionHandlingOptions options)
		{
			this.code = code;
			this.groupHandler = groupHandler;
			this.options = options;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000493C File Offset: 0x00002B3C
		public static void Handle(Action code, ExceptionGroupHandler groupHandler, ExceptionHandlingOptions options)
		{
			try
			{
				new ExceptionHandler(code, groupHandler, options).Execute();
			}
			catch (AggregateException)
			{
				throw;
			}
			catch (Exception ex)
			{
				LogItem.Publish(options.ClientId, string.Format("{0}{1}", options.Operation, "UnhandledException"), ex.ToString(), options.CorrelationId, ResultSeverityLevel.Error);
				throw;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000049A8 File Offset: 0x00002BA8
		private void Execute()
		{
			this.operationStartTime = DateTime.UtcNow;
			try
			{
				IL_0B:
				this.retryCount++;
				this.code();
			}
			catch (Exception ex)
			{
				this.exceptions.Add(ex);
				TimeSpan timeSpan = DateTime.UtcNow - this.operationStartTime;
				if (this.groupHandler(ex) == ExceptionAction.RetryWait && this.ShouldRetry())
				{
					TimeSpan timeSpan2 = this.options.RetrySchedule[Math.Min(this.retryCount, this.options.RetrySchedule.Length - 1)];
					bool flag = !this.options.IsTimeoutEnabled || timeSpan2 + timeSpan < this.options.OperationDuration;
					if (flag)
					{
						LogItem.Publish(this.options.ClientId, string.Format("{0}{1}", this.options.Operation, "ErrorHandling"), string.Format("Operation failed, retrying {0}/{1} in {2}, duration: {3}/{4}, error: {5}", new object[]
						{
							this.retryCount,
							this.options.MaxRetries,
							timeSpan2,
							timeSpan,
							this.options.OperationDuration,
							ex.ToString()
						}), this.options.CorrelationId, ResultSeverityLevel.Informational);
						Thread.Sleep(timeSpan2);
						goto IL_0B;
					}
					this.exceptions.Add(new TimeoutException("Operation would took longer than expected with next retry: " + this.options.OperationDuration));
				}
				LogItem.Publish(this.options.ClientId, string.Format("{0}{1}", this.options.Operation, "FatalError"), string.Format("Operation failed, retried {0}/{1}, duration: {2}/{3}, error: {4}", new object[]
				{
					this.retryCount,
					this.options.MaxRetries,
					timeSpan,
					this.options.OperationDuration,
					ex.ToString()
				}), this.options.CorrelationId, ResultSeverityLevel.Error);
				throw new AggregateException(this.exceptions);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004C0C File Offset: 0x00002E0C
		private bool ShouldRetry()
		{
			return this.options.IsRetryEnabled && this.options.RetrySchedule != null && this.options.RetrySchedule.Length > 0 && this.retryCount < this.options.MaxRetries;
		}

		// Token: 0x0400003C RID: 60
		private readonly Action code;

		// Token: 0x0400003D RID: 61
		private readonly ExceptionGroupHandler groupHandler;

		// Token: 0x0400003E RID: 62
		private readonly ExceptionHandlingOptions options;

		// Token: 0x0400003F RID: 63
		private readonly List<Exception> exceptions = new List<Exception>();

		// Token: 0x04000040 RID: 64
		private DateTime operationStartTime;

		// Token: 0x04000041 RID: 65
		private int retryCount;
	}
}
