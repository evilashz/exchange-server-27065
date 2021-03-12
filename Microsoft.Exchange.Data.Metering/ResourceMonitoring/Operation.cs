using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x02000024 RID: 36
	internal class Operation
	{
		// Token: 0x0600017A RID: 378 RVA: 0x000079B8 File Offset: 0x00005BB8
		public Operation(string debugText, Action action, IExecutionInfo executionInfo, int maxTransientExceptions = 5)
		{
			Operation <>4__this = this;
			ArgumentValidator.ThrowIfNullOrEmpty("debugText", debugText);
			ArgumentValidator.ThrowIfNull("action", action);
			ArgumentValidator.ThrowIfNull("executionInfo", executionInfo);
			ArgumentValidator.ThrowIfZeroOrNegative("maxTransientExceptions", maxTransientExceptions);
			this.debugText = debugText;
			this.executionInfo = executionInfo;
			this.maxTransientExceptions = maxTransientExceptions;
			this.action = delegate()
			{
				<>4__this.ActionWrapper(action);
			};
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00007A4B File Offset: 0x00005C4B
		public IExecutionInfo ExecutionInfo
		{
			get
			{
				return this.executionInfo;
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007CF8 File Offset: 0x00005EF8
		public static async Task InvokeOperationsAsync(IEnumerable<Operation> operations, TimeSpan timeout)
		{
			ArgumentValidator.ThrowIfNull("operations", operations);
			ArgumentValidator.ThrowIfInvalidValue<TimeSpan>("timeout", timeout, (TimeSpan timeoutPeriod) => timeoutPeriod > TimeSpan.Zero);
			foreach (Operation operation3 in operations)
			{
				if (operation3 != null)
				{
					operation3.Start();
				}
			}
			Task allOperationsTask = Task.WhenAll(from operation in operations
			where operation != null && operation.task != null
			select operation into op
			select op.task);
			Task timeoutTask = Task.Delay(timeout);
			await Task.WhenAny(new Task[]
			{
				allOperationsTask,
				timeoutTask
			});
			List<Exception> exceptions = new List<Exception>();
			foreach (Operation operation2 in operations)
			{
				if (operation2 != null)
				{
					Exception ex = operation2.End();
					if (ex != null)
					{
						exceptions.Add(ex);
					}
				}
			}
			if (exceptions.Any<Exception>())
			{
				throw new AggregateException(exceptions);
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007D48 File Offset: 0x00005F48
		private void ActionWrapper(Action originalAction)
		{
			this.executionInfo.OnStart();
			try
			{
				originalAction();
			}
			catch (TransientException ex)
			{
				this.exceptionList.Add(ex);
				this.executionInfo.OnException(ex);
			}
			finally
			{
				this.executionInfo.OnFinish();
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007DB0 File Offset: 0x00005FB0
		private void Start()
		{
			if (this.ShouldInvoke())
			{
				this.task = Task.Run(this.action);
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007DCC File Offset: 0x00005FCC
		private Exception End()
		{
			if (this.task == null)
			{
				return null;
			}
			if (this.task.Status == TaskStatus.Running)
			{
				throw new TimeoutException(this.debugText);
			}
			if (this.exceptionList.Count >= this.maxTransientExceptions)
			{
				throw new AggregateException("Too many transient exceptions were thrown. Debug info:" + this.debugText, this.exceptionList);
			}
			return this.task.Exception;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007E37 File Offset: 0x00006037
		private bool ShouldInvoke()
		{
			return this.task == null || this.task.IsCompleted;
		}

		// Token: 0x040000BE RID: 190
		private readonly int maxTransientExceptions;

		// Token: 0x040000BF RID: 191
		private readonly List<Exception> exceptionList = new List<Exception>();

		// Token: 0x040000C0 RID: 192
		private readonly Action action;

		// Token: 0x040000C1 RID: 193
		private readonly IExecutionInfo executionInfo;

		// Token: 0x040000C2 RID: 194
		private readonly string debugText;

		// Token: 0x040000C3 RID: 195
		private Task task;
	}
}
