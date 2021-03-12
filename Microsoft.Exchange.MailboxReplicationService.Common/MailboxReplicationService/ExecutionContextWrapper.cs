using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000F3 RID: 243
	internal class ExecutionContextWrapper
	{
		// Token: 0x0600091C RID: 2332 RVA: 0x00012438 File Offset: 0x00010638
		public ExecutionContextWrapper(CommonUtils.UpdateDuration updateDuration, string callName, params DataContext[] additionalContexts)
		{
			this.updateDuration = updateDuration;
			this.contexts.Add(new OperationDataContext(callName, OperationType.None));
			if (additionalContexts != null)
			{
				this.contexts.AddRange(additionalContexts);
			}
			this.callName = callName;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00012488 File Offset: 0x00010688
		public void Execute(Action operation, bool measure = true)
		{
			this.wrappedContext = ExecutionContext.Create(this.contexts.ToArray());
			if (measure)
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				try
				{
					this.wrappedContext.Execute(operation);
					return;
				}
				finally
				{
					this.updateDuration(this.callName, stopwatch.Elapsed);
					stopwatch.Stop();
				}
			}
			this.wrappedContext.Execute(operation);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x000124FC File Offset: 0x000106FC
		public ExecutionContext Create()
		{
			this.wrappedContext = ExecutionContext.Create(this.contexts.ToArray());
			return this.wrappedContext;
		}

		// Token: 0x04000556 RID: 1366
		private ExecutionContext wrappedContext;

		// Token: 0x04000557 RID: 1367
		private List<DataContext> contexts = new List<DataContext>();

		// Token: 0x04000558 RID: 1368
		private readonly string callName;

		// Token: 0x04000559 RID: 1369
		private CommonUtils.UpdateDuration updateDuration;
	}
}
