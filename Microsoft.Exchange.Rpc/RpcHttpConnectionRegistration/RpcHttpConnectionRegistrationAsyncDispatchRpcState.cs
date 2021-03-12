using System;
using System.Threading;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Rpc.RpcHttpConnectionRegistration
{
	// Token: 0x020003CA RID: 970
	internal abstract class RpcHttpConnectionRegistrationAsyncDispatchRpcState
	{
		// Token: 0x060010CD RID: 4301 RVA: 0x00053E94 File Offset: 0x00053294
		private static void Callback(ICancelableAsyncResult asyncResult)
		{
			RpcHttpConnectionRegistrationAsyncDispatchRpcState rpcHttpConnectionRegistrationAsyncDispatchRpcState = (RpcHttpConnectionRegistrationAsyncDispatchRpcState)asyncResult.AsyncState;
			rpcHttpConnectionRegistrationAsyncDispatchRpcState.InternalCallback(asyncResult);
			rpcHttpConnectionRegistrationAsyncDispatchRpcState.Reset();
			rpcHttpConnectionRegistrationAsyncDispatchRpcState.ReleaseToPool();
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x00052FDC File Offset: 0x000523DC
		protected RpcHttpConnectionRegistrationAsyncDispatchRpcState()
		{
			this.asyncState = null;
			this.asyncServer = null;
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x00053000 File Offset: 0x00052400
		protected RpcHttpConnectionRegistrationAsyncRpcServer AsyncServer
		{
			get
			{
				return this.asyncServer;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x00053014 File Offset: 0x00052414
		protected SafeRpcAsyncStateHandle AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00053028 File Offset: 0x00052428
		protected void InternalInitialize(SafeRpcAsyncStateHandle asyncState, RpcHttpConnectionRegistrationAsyncRpcServer asyncServer)
		{
			this.asyncState = asyncState;
			this.asyncServer = asyncServer;
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x00053044 File Offset: 0x00052444
		protected void InternalReset()
		{
			this.asyncState = null;
			this.asyncServer = null;
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0005385C File Offset: 0x00052C5C
		protected void InternalCallback(ICancelableAsyncResult asyncResult)
		{
			try
			{
				IRpcHttpConnectionRegistrationAsyncDispatch rpcHttpConnectionRegistrationAsyncDispatch = this.asyncServer.GetRpcHttpConnectionRegistrationAsyncDispatch();
				if (rpcHttpConnectionRegistrationAsyncDispatch != null && !this.asyncServer.IsShuttingDown())
				{
					int result = this.InternalEnd(asyncResult, rpcHttpConnectionRegistrationAsyncDispatch);
					this.asyncState.CompleteCall(result);
				}
				else
				{
					this.asyncState.AbortCall(1722U);
				}
			}
			catch (RpcException ex)
			{
				this.asyncState.AbortCall((uint)ex.ErrorCode);
			}
			catch (FailRpcException ex2)
			{
				this.asyncState.CompleteCall(ex2.ErrorCode);
			}
			catch (ThreadAbortException)
			{
				this.asyncState.AbortCall(1726U);
			}
			catch (OutOfMemoryException)
			{
				this.asyncState.AbortCall(1130U);
			}
			catch (Exception e)
			{
				<Module>.Microsoft.Exchange.Rpc.ManagedExceptionCrashWrapper.CrashMeNow(e);
			}
			catch (object o)
			{
				<Module>.Microsoft.Exchange.Rpc.ManagedExceptionCrashWrapper.CrashMeNow(o);
			}
			this.asyncState = null;
			this.asyncServer = null;
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x000539AC File Offset: 0x00052DAC
		public void Begin()
		{
			try
			{
				IRpcHttpConnectionRegistrationAsyncDispatch rpcHttpConnectionRegistrationAsyncDispatch = this.asyncServer.GetRpcHttpConnectionRegistrationAsyncDispatch();
				if (rpcHttpConnectionRegistrationAsyncDispatch != null && !this.asyncServer.IsShuttingDown())
				{
					this.InternalBegin(RpcHttpConnectionRegistrationAsyncDispatchRpcState.asyncCallback, rpcHttpConnectionRegistrationAsyncDispatch);
				}
				else
				{
					this.asyncState.AbortCall(1722U);
				}
			}
			catch (RpcException ex)
			{
				this.asyncState.AbortCall((uint)ex.ErrorCode);
			}
			catch (FailRpcException ex2)
			{
				this.asyncState.CompleteCall(ex2.ErrorCode);
			}
			catch (ThreadAbortException)
			{
				this.asyncState.AbortCall(1726U);
			}
			catch (OutOfMemoryException)
			{
				this.asyncState.AbortCall(1130U);
			}
			catch (Exception e)
			{
				<Module>.Microsoft.Exchange.Rpc.ManagedExceptionCrashWrapper.CrashMeNow(e);
			}
			catch (object o)
			{
				<Module>.Microsoft.Exchange.Rpc.ManagedExceptionCrashWrapper.CrashMeNow(o);
			}
		}

		// Token: 0x060010D5 RID: 4309
		public abstract void InternalBegin(CancelableAsyncCallback asyncCallback, IRpcHttpConnectionRegistrationAsyncDispatch asyncDispatch);

		// Token: 0x060010D6 RID: 4310
		public abstract int InternalEnd(ICancelableAsyncResult asyncResult, IRpcHttpConnectionRegistrationAsyncDispatch asyncDispatch);

		// Token: 0x060010D7 RID: 4311
		public abstract void Reset();

		// Token: 0x060010D8 RID: 4312
		public abstract void ReleaseToPool();

		// Token: 0x04000FE2 RID: 4066
		private static readonly CancelableAsyncCallback asyncCallback = new CancelableAsyncCallback(RpcHttpConnectionRegistrationAsyncDispatchRpcState.Callback);

		// Token: 0x04000FE3 RID: 4067
		private SafeRpcAsyncStateHandle asyncState;

		// Token: 0x04000FE4 RID: 4068
		private RpcHttpConnectionRegistrationAsyncRpcServer asyncServer;
	}
}
