using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200000F RID: 15
	internal abstract class DispatchTask : EasyCancelableAsyncResult
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00004286 File Offset: 0x00002486
		public DispatchTask(string description, CancelableAsyncCallback asyncCallback, object asyncState) : base(asyncCallback, asyncState)
		{
			if (ExTraceGlobals.DispatchTaskTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.DispatchTaskTracer.TraceDebug<string>(0, 0L, "{0} created.", description);
			}
			this.description = description;
			this.taskCompleted = 0;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000042C0 File Offset: 0x000024C0
		public void Execute()
		{
			if (ExTraceGlobals.DispatchTaskTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.DispatchTaskTracer.TraceDebug<string, long>(0, 0L, "{0} execution started. ContextHandle={1:X}.", this.description, this.ContextHandle.ToInt64());
			}
			Exception ex = null;
			try
			{
				if (!RpcClientAccessService.IsShuttingDown)
				{
					int? num = this.InternalExecute();
					if (num != null)
					{
						this.Completion(null, num.Value);
					}
				}
				else
				{
					this.Completion(new ServerUnavailableException("Shutting down"), 0);
				}
			}
			catch (FailRpcException ex2)
			{
				this.Completion(null, ex2.ErrorCode);
			}
			catch (RpcException ex3)
			{
				this.Completion(ex3, 0);
			}
			catch (RpcServiceException ex4)
			{
				this.Completion(new RpcException(ex4.Message, ex4.RpcStatus, ex4), 0);
			}
			catch (RpcServerException ex5)
			{
				this.Completion(null, (int)ex5.StoreError);
			}
			catch (BufferParseException)
			{
				this.Completion(null, 1206);
			}
			catch (OutOfMemoryException)
			{
				this.Completion(null, 1008);
			}
			catch (Exception ex6)
			{
				ex = ex6;
				this.Completion(new RpcException("Call failed unhandled exception", 1726, ex6), 0);
			}
			finally
			{
				if (ExTraceGlobals.DispatchTaskTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.DispatchTaskTracer.TraceDebug(0, 0L, "{0} execution {1}. ContextHandle={2:X}. {3}", new object[]
					{
						this.description,
						(ex != null) ? "failed" : "ended",
						this.ContextHandle.ToInt64(),
						(ex != null) ? string.Format("UnhandledException={0}", ex) : string.Empty
					});
				}
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000044A8 File Offset: 0x000026A8
		internal void Completion(Exception exception, int errorCode)
		{
			if (Interlocked.CompareExchange(ref this.taskCompleted, 1, 0) == 1)
			{
				return;
			}
			if (ExTraceGlobals.DispatchTaskTracer.IsTraceEnabled(TraceType.DebugTrace) || ((errorCode != 0 || exception != null) && ExTraceGlobals.DispatchTaskTracer.IsTraceEnabled(TraceType.ErrorTrace)))
			{
				ExTraceGlobals.DispatchTaskTracer.TraceDebug(0, 0L, "{0} completion. ContextHandle={1:X}. ErrorCode={2}. Exception={3}.", new object[]
				{
					this.description,
					this.ContextHandle.ToInt64(),
					errorCode,
					exception
				});
			}
			this.exception = exception;
			this.errorCode = errorCode;
			base.InvokeCallback();
		}

		// Token: 0x06000082 RID: 130
		internal abstract int? InternalExecute();

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000083 RID: 131
		internal abstract IntPtr ContextHandle { get; }

		// Token: 0x06000084 RID: 132 RVA: 0x00004540 File Offset: 0x00002740
		internal int CheckCompletion()
		{
			base.WaitForCompletion();
			if (this.exception != null)
			{
				throw this.exception;
			}
			return this.errorCode;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004560 File Offset: 0x00002760
		protected override void InternalCancel()
		{
			if (ExTraceGlobals.DispatchTaskTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.DispatchTaskTracer.TraceDebug<string, long>(0, 0L, "{0} canceled. ContextHandle={1:X}.", this.description, this.ContextHandle.ToInt64());
			}
			this.Completion(new ServerUnavailableException("DispatchPool shutting down"), 0);
		}

		// Token: 0x0400005B RID: 91
		private int taskCompleted;

		// Token: 0x0400005C RID: 92
		private int errorCode;

		// Token: 0x0400005D RID: 93
		private Exception exception;

		// Token: 0x0400005E RID: 94
		private string description;
	}
}
