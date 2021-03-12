using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Search.Core.AsyncTask
{
	// Token: 0x02000049 RID: 73
	internal abstract class AsyncTask
	{
		// Token: 0x06000164 RID: 356 RVA: 0x00002BFA File Offset: 0x00000DFA
		internal AsyncTask()
		{
			this.diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession("AsyncTask", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.CoreGeneralTracer, (long)this.GetHashCode());
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00002C28 File Offset: 0x00000E28
		internal ComponentException Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00002C30 File Offset: 0x00000E30
		internal bool Running
		{
			get
			{
				return this.isRunning != 0;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00002C3E File Offset: 0x00000E3E
		internal bool Cancelled
		{
			get
			{
				return this.isCancelled != 0;
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00002C4C File Offset: 0x00000E4C
		internal void Execute(TaskCompleteCallback callback)
		{
			this.diagnosticsSession.TraceDebug<AsyncTask>("AsyncTask::Execute: {0}", this);
			if (Interlocked.CompareExchange(ref this.isRunning, 1, 0) != 0)
			{
				throw new InvalidOperationException("Cannot invoke a task that is running.");
			}
			this.callback = callback;
			if (this.Cancelled)
			{
				this.Complete(null);
				return;
			}
			this.InternalExecute();
		}

		// Token: 0x06000169 RID: 361
		internal abstract void InternalExecute();

		// Token: 0x0600016A RID: 362 RVA: 0x00002CA1 File Offset: 0x00000EA1
		internal void Cancel()
		{
			this.diagnosticsSession.TraceDebug<AsyncTask>("AsyncTask::Cancel: {0}", this);
			if (Interlocked.CompareExchange(ref this.isCancelled, 1, 0) != 0)
			{
				throw new InvalidOperationException("Cannot cancel a task that has been cancelled.");
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00002CD0 File Offset: 0x00000ED0
		protected void Complete(ComponentException exception)
		{
			this.diagnosticsSession.TraceDebug<AsyncTask, object>("AsyncTask::Complete: {0}. Exception = {1}", this, (exception == null) ? "none" : exception);
			this.exception = exception;
			try
			{
				if (this.callback != null)
				{
					this.callback(this);
				}
			}
			finally
			{
				this.isRunning = 0;
			}
		}

		// Token: 0x0400008E RID: 142
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x0400008F RID: 143
		private TaskCompleteCallback callback;

		// Token: 0x04000090 RID: 144
		private ComponentException exception;

		// Token: 0x04000091 RID: 145
		private int isRunning;

		// Token: 0x04000092 RID: 146
		private int isCancelled;
	}
}
