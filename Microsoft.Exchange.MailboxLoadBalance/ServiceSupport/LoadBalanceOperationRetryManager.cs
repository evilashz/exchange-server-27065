using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalance.ServiceSupport
{
	// Token: 0x020000F5 RID: 245
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadBalanceOperationRetryManager : OperationRetryManagerBase
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x00014DD5 File Offset: 0x00012FD5
		private LoadBalanceOperationRetryManager(int retryCount, TimeSpan retryInterval, ILogger logger) : base(retryCount, retryInterval, true)
		{
			this.logger = logger;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00014DE7 File Offset: 0x00012FE7
		public static IOperationRetryManager Create(int retryCount, TimeSpan retryInterval, ILogger logger)
		{
			return new LoadBalanceOperationRetryManager(retryCount, retryInterval, logger);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00014DF4 File Offset: 0x00012FF4
		public static IOperationRetryManager Create(ILogger logger)
		{
			ILoadBalanceSettings value = LoadBalanceADSettings.Instance.Value;
			return LoadBalanceOperationRetryManager.Create(value.TransientFailureMaxRetryCount, value.TransientFailureRetryDelay, logger);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00014E90 File Offset: 0x00013090
		protected override bool InternalRun(Action operation, bool maxRetryReached)
		{
			bool success = true;
			CommonUtils.ProcessKnownExceptionsWithoutTracing(operation, delegate(Exception exception)
			{
				this.logger.Log(MigrationEventType.Verbose, exception, "Error running operation {0}. Last retry: {1}.", new object[]
				{
					operation.Method.Name,
					maxRetryReached
				});
				if (!CommonUtils.IsTransientException(exception))
				{
					return false;
				}
				success = false;
				return !maxRetryReached;
			});
			return success;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00014F54 File Offset: 0x00013154
		protected override OperationRetryManagerResult InternalTryRun(Action operation)
		{
			OperationRetryManagerResult result = OperationRetryManagerResult.Success;
			Action operationToRun = operation;
			CommonUtils.ProcessKnownExceptionsWithoutTracing(delegate
			{
				this.Run(operationToRun);
			}, delegate(Exception exception)
			{
				this.logger.Log(MigrationEventType.Verbose, exception, "Error running operation {0}.", new object[]
				{
					operation.Method.Name
				});
				OperationRetryManagerResultCode resultCode = CommonUtils.IsTransientException(exception) ? OperationRetryManagerResultCode.RetryableError : OperationRetryManagerResultCode.PermanentError;
				result = new OperationRetryManagerResult(resultCode, exception);
				return true;
			});
			return result;
		}

		// Token: 0x040002DB RID: 731
		private readonly ILogger logger;
	}
}
