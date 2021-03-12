using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020001E6 RID: 486
	public abstract class ClientAsyncCallState : ClientAsyncResult, IDisposable
	{
		// Token: 0x06000A66 RID: 2662 RVA: 0x0001DBF8 File Offset: 0x0001CFF8
		private static void EnsureAsyncCallCompletionThread()
		{
			@lock @lock = null;
			if (ClientAsyncCallState.m_completionThreadState == null)
			{
				@lock lock2 = new @lock(ClientAsyncCallState.m_lock);
				try
				{
					@lock = lock2;
					if (ClientAsyncCallState.m_completionThreadState == null)
					{
						ClientAsyncCallState.m_completionThreadState = new CompletionThreadState();
					}
				}
				catch
				{
					((IDisposable)@lock).Dispose();
					throw;
				}
				((IDisposable)@lock).Dispose();
			}
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0001CBBC File Offset: 0x0001BFBC
		private static IntPtr CreateAsyncCallHandle(ClientAsyncResult clientAsyncResult)
		{
			@lock @lock = null;
			@lock lock2 = new @lock(ClientAsyncCallState.m_lock);
			IntPtr result;
			try
			{
				@lock = lock2;
				result = ClientAsyncCallState.m_completionThreadState.CreateAsyncCallHandle(clientAsyncResult);
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			return result;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0001CC10 File Offset: 0x0001C010
		private void DestroyAsyncCallHandle(IntPtr asyncCallHandle)
		{
			@lock @lock = null;
			@lock lock2 = new @lock(ClientAsyncCallState.m_lock);
			try
			{
				@lock = lock2;
				ClientAsyncCallState.m_completionThreadState.DestroyAsyncCallHandle(asyncCallHandle);
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0001BA80 File Offset: 0x0001AE80
		[HandleProcessCorruptedStateExceptions]
		private int Wrapped_InternalBegin()
		{
			int result = 0;
			try
			{
				this.m_fCallOutstanding = true;
				this.InternalBegin();
			}
			catch when (endfilter(true))
			{
				this.m_fCallOutstanding = false;
				result = Marshal.GetExceptionCode();
			}
			return result;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0001CC64 File Offset: 0x0001C064
		private void CallCleanup()
		{
			if (this.m_fCallOutstanding)
			{
				throw new InvalidOperationException("Asynchronous RPC call still outstanding");
			}
			if (this.m_asyncCallHandle != IntPtr.Zero)
			{
				this.DestroyAsyncCallHandle(this.m_asyncCallHandle);
				this.m_asyncCallHandle = IntPtr.Zero;
			}
			if (this.m_pRpcAsyncState != IntPtr.Zero)
			{
				initblk(this.m_pRpcAsyncState.ToPointer(), 0, 112L);
				Marshal.FreeHGlobal(this.m_pRpcAsyncState);
				this.m_pRpcAsyncState = IntPtr.Zero;
			}
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0001CD00 File Offset: 0x0001C100
		protected void CompleteSynchronously(Exception exception)
		{
			this.m_exception = exception;
			this.m_fCallOutstanding = false;
			base.Completion();
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0001BAD0 File Offset: 0x0001AED0
		[HandleProcessCorruptedStateExceptions]
		protected unsafe override void InternalCancel()
		{
			if (this.m_pRpcAsyncState != IntPtr.Zero && this.m_fCallOutstanding)
			{
				try
				{
					int num = <Module>.RpcAsyncCancelCall((_RPC_ASYNC_STATE*)this.m_pRpcAsyncState.ToPointer(), 1);
				}
				catch when (endfilter(true))
				{
				}
			}
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0001BB34 File Offset: 0x0001AF34
		public ClientAsyncCallState(string callName, CancelableAsyncCallback asyncCallback, object asyncState) : base(asyncCallback, asyncState)
		{
			this.m_exception = null;
			this.m_fCallOutstanding = false;
			this.m_pRpcAsyncState = IntPtr.Zero;
			this.m_callName = callName;
			this.m_asyncCallHandle = IntPtr.Zero;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0001CD24 File Offset: 0x0001C124
		private void ~ClientAsyncCallState()
		{
			this.CallCleanup();
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0001BB74 File Offset: 0x0001AF74
		public IntPtr RpcAsyncState()
		{
			return this.m_pRpcAsyncState;
		}

		// Token: 0x06000A70 RID: 2672
		public abstract void InternalBegin();

		// Token: 0x06000A71 RID: 2673 RVA: 0x0001DC5C File Offset: 0x0001D05C
		public unsafe void Begin()
		{
			this.m_fCallOutstanding = false;
			try
			{
				ClientAsyncCallState.EnsureAsyncCallCompletionThread();
				IntPtr pRpcAsyncState = Marshal.AllocHGlobal(112);
				this.m_pRpcAsyncState = pRpcAsyncState;
				initblk(this.m_pRpcAsyncState.ToPointer(), 0, 112L);
				int num = <Module>.RpcAsyncInitializeHandle((_RPC_ASYNC_STATE*)this.m_pRpcAsyncState.ToPointer(), 112U);
				if (num != null)
				{
					Exception exception = <Module>.Microsoft.Exchange.Rpc.GetRpcExceptionWithEEInfo(num, "ClientAsyncCallState.Begin: RpcAsyncInitializeHandle");
					this.m_exception = exception;
					this.m_fCallOutstanding = false;
					base.Completion();
				}
				else
				{
					IntPtr asyncCallHandle = ClientAsyncCallState.CreateAsyncCallHandle(this);
					this.m_asyncCallHandle = asyncCallHandle;
					_RPC_ASYNC_STATE* ptr = (_RPC_ASYNC_STATE*)this.m_pRpcAsyncState.ToPointer();
					*(long*)(ptr + 24L / (long)sizeof(_RPC_ASYNC_STATE)) = this.m_asyncCallHandle.ToPointer();
					*(int*)(ptr + 44L / (long)sizeof(_RPC_ASYNC_STATE)) = 3;
					*(long*)(ptr + 48L / (long)sizeof(_RPC_ASYNC_STATE)) = ClientAsyncCallState.m_completionThreadState.GetIoCompletionPort();
					*(long*)(ptr + 72L / (long)sizeof(_RPC_ASYNC_STATE)) = this.m_asyncCallHandle.ToPointer();
					int num2 = this.Wrapped_InternalBegin();
					if (num2 != null)
					{
						byte condition = (!this.m_fCallOutstanding) ? 1 : 0;
						ExAssert.Assert(condition != 0, "Failed synchronously, but call marked as outstanding");
						Exception exception2 = <Module>.Microsoft.Exchange.Rpc.GetRpcExceptionWithEEInfo(num2, "ClientAsyncCallState.Begin: InternalBegin");
						this.m_exception = exception2;
						this.m_fCallOutstanding = false;
						base.Completion();
					}
				}
			}
			finally
			{
				if (!this.m_fCallOutstanding)
				{
					this.CallCleanup();
				}
			}
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0001CD38 File Offset: 0x0001C138
		public unsafe int CheckCompletion()
		{
			int result = 0;
			try
			{
				base.WaitForCompletion();
				if (this.m_exception != null)
				{
					byte condition = (!this.m_fCallOutstanding) ? 1 : 0;
					ExAssert.Assert(condition != 0, "Stored synchronous call failure exception from Begin, but call marked as outstanding");
					throw this.m_exception;
				}
				if (!this.m_fCallOutstanding)
				{
					throw new InvalidOperationException("CheckCompletion being called with no synchronous call failure or outstanding call.");
				}
				this.m_fCallOutstanding = false;
				ExAssert.Assert(true, "Call is outstanding, but there is a synchronous failure exception from Begin");
				int num = <Module>.RpcAsyncCompleteCall((_RPC_ASYNC_STATE*)this.m_pRpcAsyncState.ToPointer(), (void*)(&result));
				if (num != null)
				{
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(num, "ClientAsyncCallState.CheckCompletion: RpcAsyncCompleteCall");
				}
			}
			finally
			{
				this.CallCleanup();
			}
			return result;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0001CDE4 File Offset: 0x0001C1E4
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.CallCleanup();
			}
			else
			{
				base.Finalize();
			}
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0001DD9C File Offset: 0x0001D19C
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000BB0 RID: 2992
		private static object m_lock = new object();

		// Token: 0x04000BB1 RID: 2993
		private static CompletionThreadState m_completionThreadState = null;

		// Token: 0x04000BB2 RID: 2994
		private IntPtr m_pRpcAsyncState;

		// Token: 0x04000BB3 RID: 2995
		private IntPtr m_asyncCallHandle;

		// Token: 0x04000BB4 RID: 2996
		private string m_callName;

		// Token: 0x04000BB5 RID: 2997
		private bool m_fCallOutstanding;

		// Token: 0x04000BB6 RID: 2998
		private Exception m_exception;
	}
}
