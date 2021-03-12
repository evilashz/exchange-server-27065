using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.MultiMailboxSearch;

namespace Microsoft.Exchange.Server.Storage.MultiMailboxSearch
{
	// Token: 0x02000008 RID: 8
	internal sealed class TimeoutHandler
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00003D28 File Offset: 0x00001F28
		private TimeoutHandler(Action actionToExecute, TimeSpan timeout, Action abortAction)
		{
			TimeoutHandler.TraceFunction("Entering TimeoutHandler.ctor");
			this.actionToExecute = actionToExecute;
			this.timeout = timeout;
			this.abortAction = abortAction;
			this.operationState = 0;
			if (ExTraceGlobals.SearchTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				ExTraceGlobals.SearchTracer.TraceInformation<string, string, double>(52368, 0L, "Creating TimeoutHandler to execute action {0} and abort action {1} for the timeout interval of {2} ms.", this.actionToExecute.Method.Name, this.abortAction.Method.Name, this.timeout.TotalMilliseconds);
			}
			TimeoutHandler.TraceFunction("Exiting TimeoutHandler.ctor");
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003DBC File Offset: 0x00001FBC
		public static void Execute(Action actionToExecute, TimeSpan timeout, Action abortAction)
		{
			TimeoutHandler.TraceFunction("Entering static TimeoutHandler.Execute");
			TimeoutHandler timeoutHandler = new TimeoutHandler(actionToExecute, timeout, abortAction);
			timeoutHandler.Execute();
			TimeoutHandler.TraceFunction("Exiting static TimeoutHandler.Execute");
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003DEC File Offset: 0x00001FEC
		private static void TraceFunction(string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				return;
			}
			if (ExTraceGlobals.SearchTracer.IsTraceEnabled(TraceType.FunctionTrace))
			{
				ExTraceGlobals.SearchTracer.TraceFunction(48272, 0L, message);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003E18 File Offset: 0x00002018
		private void Execute()
		{
			TimeoutHandler.TraceFunction("Entering TimeoutHandler.Execute");
			try
			{
				using (Timer timer = new Timer(new TimerCallback(this.OnTimeoutCallback), this, this.timeout, TimeoutHandler.runOnceTimeSpan))
				{
					if (this.actionToExecute != null)
					{
						this.actionToExecute();
					}
					if (Interlocked.CompareExchange(ref this.operationState, 2, 0) == 0)
					{
						timer.Change(-1, -1);
						if (ExTraceGlobals.SearchTracer.IsTraceEnabled(TraceType.InfoTrace))
						{
							ExTraceGlobals.SearchTracer.TraceInformation(46224, 0L, "TimeoutHandler:Execution completed before the timeout interval, disabling the timer");
						}
					}
					else if (this.operationState != 1 && ExTraceGlobals.SearchTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.SearchTracer.TraceError<int, string, double>(62608, 0L, "TimeoutHandler found invalid operation state {0}, while executing action:{1} with a timeout interval of {2} ms.", this.operationState, this.actionToExecute.Method.Name, this.timeout.TotalMilliseconds);
					}
				}
			}
			finally
			{
				TimeoutHandler.TraceFunction("Exiting static TimeoutHandler.Execute");
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003F24 File Offset: 0x00002124
		private void OnTimeoutCallback(object state)
		{
			TimeoutHandler.TraceFunction("Entering static TimeoutHandler.OnTimeoutCallback");
			if (ExTraceGlobals.SearchTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				ExTraceGlobals.SearchTracer.TraceError(38032, 0L, "TimeoutHandler:Execution timed out.");
			}
			if (Interlocked.CompareExchange(ref this.operationState, 1, 0) == 0 && this.abortAction != null)
			{
				if (ExTraceGlobals.SearchTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.SearchTracer.TraceError(54416, 0L, "TimeoutHandler:Execution timed out, invoking the Abort action delegate");
				}
				this.abortAction();
			}
			TimeoutHandler.TraceFunction("Exiting static TimeoutHandler.OnTimeoutCallback");
		}

		// Token: 0x0400001A RID: 26
		private static TimeSpan runOnceTimeSpan = TimeSpan.FromMilliseconds(-1.0);

		// Token: 0x0400001B RID: 27
		private readonly Action actionToExecute;

		// Token: 0x0400001C RID: 28
		private readonly TimeSpan timeout;

		// Token: 0x0400001D RID: 29
		private readonly Action abortAction;

		// Token: 0x0400001E RID: 30
		private int operationState;

		// Token: 0x02000009 RID: 9
		private enum OperationState
		{
			// Token: 0x04000020 RID: 32
			InitialState,
			// Token: 0x04000021 RID: 33
			TimeoutHappened,
			// Token: 0x04000022 RID: 34
			OperationCompleted
		}
	}
}
