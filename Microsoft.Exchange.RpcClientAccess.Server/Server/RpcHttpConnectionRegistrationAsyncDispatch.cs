using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200003E RID: 62
	internal sealed class RpcHttpConnectionRegistrationAsyncDispatch : IRpcHttpConnectionRegistrationAsyncDispatch
	{
		// Token: 0x0600022F RID: 559 RVA: 0x0000C680 File Offset: 0x0000A880
		public RpcHttpConnectionRegistrationAsyncDispatch(RpcHttpConnectionRegistrationDispatch rpcHttpConnectionRegistrationDispatch, DispatchPool dispatchPool)
		{
			this.rpcHttpConnectionRegistrationDispatch = rpcHttpConnectionRegistrationDispatch;
			this.dispatchPool = dispatchPool;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000C698 File Offset: 0x0000A898
		public ICancelableAsyncResult BeginRegister(Guid associationGroupId, string token, string serverTarget, string sessionCookie, string clientIp, Guid requestId, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			if (ExTraceGlobals.RpcHttpConnectionRegistrationAsyncDispatchTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.RpcHttpConnectionRegistrationAsyncDispatchTracer.TraceDebug(0, 0L, "RpcHttpConnectionRegistrationAsyncDispatch::BeginRegister started. Guid={0}. Token={1}. ClientIP={2}. RequestId={3}.", new object[]
				{
					associationGroupId,
					token,
					clientIp,
					requestId
				});
			}
			bool flag = false;
			ICancelableAsyncResult result;
			try
			{
				RegisterDispatchTask registerDispatchTask = new RegisterDispatchTask(this.rpcHttpConnectionRegistrationDispatch, asyncCallback, asyncState, associationGroupId, token, serverTarget, sessionCookie, clientIp, requestId);
				this.SubmitTask(registerDispatchTask);
				flag = true;
				result = registerDispatchTask;
			}
			finally
			{
				if (!flag)
				{
					ExTraceGlobals.RpcHttpConnectionRegistrationAsyncDispatchTracer.TraceDebug(0, 0L, "RpcHttpConnectionRegistrationAsyncDispatch::BeginRegister failed.");
				}
			}
			return result;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000C738 File Offset: 0x0000A938
		public int EndRegister(ICancelableAsyncResult asyncResult, out string failureMessage, out string failureDetails)
		{
			failureMessage = null;
			failureDetails = null;
			RegisterDispatchTask registerDispatchTask = (RegisterDispatchTask)asyncResult;
			bool flag = false;
			int result;
			try
			{
				int num = registerDispatchTask.End(out failureMessage, out failureDetails);
				if (ExTraceGlobals.RpcHttpConnectionRegistrationAsyncDispatchTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.RpcHttpConnectionRegistrationAsyncDispatchTracer.TraceDebug<int>(0, 0L, "RpcHttpConnectionRegistrationAsyncDispatch::EndRegister succeeded. ErrorCode={0}.", num);
				}
				flag = true;
				result = num;
			}
			finally
			{
				if (!flag)
				{
					ExTraceGlobals.RpcHttpConnectionRegistrationAsyncDispatchTracer.TraceDebug(0, 0L, "RpcHttpConnectionRegistrationAsyncDispatch::EndRegister failed.");
				}
			}
			return result;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000C7AC File Offset: 0x0000A9AC
		public ICancelableAsyncResult BeginUnregister(Guid associationGroupId, Guid requestId, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			if (ExTraceGlobals.RpcHttpConnectionRegistrationAsyncDispatchTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.RpcHttpConnectionRegistrationAsyncDispatchTracer.TraceDebug<Guid, Guid>(0, 0L, "RpcHttpConnectionRegistrationAsyncDispatch::BeginUnregister started. Guid={0}. RequestId={1}.", associationGroupId, requestId);
			}
			bool flag = false;
			ICancelableAsyncResult result;
			try
			{
				UnregisterDispatchTask unregisterDispatchTask = new UnregisterDispatchTask(this.rpcHttpConnectionRegistrationDispatch, asyncCallback, asyncState, associationGroupId, requestId);
				this.SubmitTask(unregisterDispatchTask);
				flag = true;
				result = unregisterDispatchTask;
			}
			finally
			{
				if (!flag)
				{
					ExTraceGlobals.RpcHttpConnectionRegistrationAsyncDispatchTracer.TraceDebug(0, 0L, "RpcHttpConnectionRegistrationAsyncDispatch::BeginUnregister failed.");
				}
			}
			return result;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000C824 File Offset: 0x0000AA24
		public int EndUnregister(ICancelableAsyncResult asyncResult)
		{
			UnregisterDispatchTask unregisterDispatchTask = (UnregisterDispatchTask)asyncResult;
			bool flag = false;
			int result;
			try
			{
				int num = unregisterDispatchTask.End();
				if (ExTraceGlobals.RpcHttpConnectionRegistrationAsyncDispatchTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.RpcHttpConnectionRegistrationAsyncDispatchTracer.TraceDebug<int>(0, 0L, "RpcHttpConnectionRegistrationAsyncDispatch::EndUnregister succeeded. ErrorCode={0}.", num);
				}
				flag = true;
				result = num;
			}
			finally
			{
				if (!flag)
				{
					ExTraceGlobals.RpcHttpConnectionRegistrationAsyncDispatchTracer.TraceDebug(0, 0L, "RpcHttpConnectionRegistrationAsyncDispatch::EndUnregister failed.");
				}
			}
			return result;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000C890 File Offset: 0x0000AA90
		public ICancelableAsyncResult BeginClear(CancelableAsyncCallback asyncCallback, object asyncState)
		{
			if (ExTraceGlobals.RpcHttpConnectionRegistrationAsyncDispatchTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.RpcHttpConnectionRegistrationAsyncDispatchTracer.TraceDebug(0, 0L, "RpcHttpConnectionRegistrationAsyncDispatch::BeginClear started.");
			}
			bool flag = false;
			ICancelableAsyncResult result;
			try
			{
				ClearDispatchTask clearDispatchTask = new ClearDispatchTask(this.rpcHttpConnectionRegistrationDispatch, asyncCallback, asyncState);
				this.SubmitTask(clearDispatchTask);
				flag = true;
				result = clearDispatchTask;
			}
			finally
			{
				if (!flag)
				{
					ExTraceGlobals.RpcHttpConnectionRegistrationAsyncDispatchTracer.TraceDebug(0, 0L, "RpcHttpConnectionRegistrationAsyncDispatch::BeginClear failed.");
				}
			}
			return result;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000C900 File Offset: 0x0000AB00
		public int EndClear(ICancelableAsyncResult asyncResult)
		{
			ClearDispatchTask clearDispatchTask = (ClearDispatchTask)asyncResult;
			bool flag = false;
			int result;
			try
			{
				int num = clearDispatchTask.End();
				if (ExTraceGlobals.RpcHttpConnectionRegistrationAsyncDispatchTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.RpcHttpConnectionRegistrationAsyncDispatchTracer.TraceDebug<int>(0, 0L, "RpcHttpConnectionRegistrationAsyncDispatch::EndClear succeeded. ErrorCode={0}.", num);
				}
				flag = true;
				result = num;
			}
			finally
			{
				if (!flag)
				{
					ExTraceGlobals.RpcHttpConnectionRegistrationAsyncDispatchTracer.TraceDebug(0, 0L, "RpcHttpConnectionRegistrationAsyncDispatch::EndClear failed.");
				}
			}
			return result;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000C96C File Offset: 0x0000AB6C
		internal RpcHttpConnectionRegistrationDispatch RpcHttpConnectionRegistrationDispatch
		{
			get
			{
				return this.rpcHttpConnectionRegistrationDispatch;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000C974 File Offset: 0x0000AB74
		internal bool IsShuttingDown
		{
			get
			{
				return RpcClientAccessService.IsShuttingDown;
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000C97B File Offset: 0x0000AB7B
		private void SubmitTask(DispatchTask task)
		{
			if (!this.IsShuttingDown)
			{
				if (!this.dispatchPool.SubmitTask(task))
				{
					task.Completion(new ServerTooBusyException("Unable to submit task; queue full"), 0);
					return;
				}
			}
			else
			{
				task.Completion(new ServerUnavailableException("Shutting down"), 0);
			}
		}

		// Token: 0x0400011B RID: 283
		private readonly RpcHttpConnectionRegistrationDispatch rpcHttpConnectionRegistrationDispatch;

		// Token: 0x0400011C RID: 284
		private readonly DispatchPool dispatchPool;
	}
}
