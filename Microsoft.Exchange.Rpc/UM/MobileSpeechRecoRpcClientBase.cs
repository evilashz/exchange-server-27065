using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.Rpc.UM
{
	// Token: 0x02000404 RID: 1028
	internal abstract class MobileSpeechRecoRpcClientBase : RpcClientBase
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x000578A8 File Offset: 0x00056CA8
		protected Guid RequestId
		{
			get
			{
				return this.requestId;
			}
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00057FE8 File Offset: 0x000573E8
		protected MobileSpeechRecoRpcClientBase(Guid requestId, string machineName) : base(machineName)
		{
			try
			{
				ExTraceGlobals.RpcTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Entering MobileSpeechRecoRpcClientBase constructor for requestId='{0}'", requestId);
				this.requestId = requestId;
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x000582F0 File Offset: 0x000576F0
		[HandleProcessCorruptedStateExceptions]
		protected unsafe IAsyncResult BeginExecuteStep(byte[] inBlob, AsyncCallback callback)
		{
			ExTraceGlobals.RpcTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Entering BeginExecuteStep for requestId='{0}'", this.requestId);
			if (null == inBlob)
			{
				throw new ArgumentNullException("inBlob");
			}
			if (null == callback)
			{
				throw new ArgumentNullException("callback");
			}
			MobileSpeechRecoRpcClientBase.MobileSpeechRecoAsyncResult mobileSpeechRecoAsyncResult = null;
			int num = 0;
			byte* ptr = null;
			bool flag = false;
			try
			{
				ptr = <Module>.MToUBytesClient(inBlob, &num);
				mobileSpeechRecoAsyncResult = new MobileSpeechRecoRpcClientBase.MobileSpeechRecoAsyncResult(this.requestId, callback);
				int num2 = <Module>.RpcAsyncInitializeHandle((_RPC_ASYNC_STATE*)mobileSpeechRecoAsyncResult.NativeState(), 112U);
				if (num2 != null)
				{
					ExTraceGlobals.RpcTracer.TraceError<Guid, int>((long)this.GetHashCode(), "BeginExecuteStep: RpcAsyncInitializeHandle Exception - requestId='{0}' rpcStatus='{1}", this.requestId, num2);
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(num2, "BeginExecuteStep, RpcAsyncInitializeHandle");
				}
				*(long*)(mobileSpeechRecoAsyncResult.NativeState() + 24L / (long)sizeof(MobileSpeechRecoAsyncState)) = 0L;
				*(int*)(mobileSpeechRecoAsyncResult.NativeState() + 44L / (long)sizeof(MobileSpeechRecoAsyncState)) = 1;
				IntPtr handle = mobileSpeechRecoAsyncResult.AsyncWaitHandle.Handle;
				*(long*)(mobileSpeechRecoAsyncResult.NativeState() + 48L / (long)sizeof(MobileSpeechRecoAsyncState)) = handle.ToPointer();
				try
				{
					ExTraceGlobals.RpcTracer.TraceDebug<Guid>((long)this.GetHashCode(), "BeginExecuteStep: Executing step async for requestId='{0}'", this.requestId);
					<Module>.cli_ExecuteStepAsync((_RPC_ASYNC_STATE*)mobileSpeechRecoAsyncResult.NativeState(), base.BindingHandle, num, ptr, (int*)(mobileSpeechRecoAsyncResult.NativeState() + 112L / (long)sizeof(MobileSpeechRecoAsyncState)), (byte**)(mobileSpeechRecoAsyncResult.NativeState() + 120L / (long)sizeof(MobileSpeechRecoAsyncState)));
					ExTraceGlobals.RpcTracer.TraceDebug<Guid>((long)this.GetHashCode(), "BeginExecuteStep: Executed step async for requestId='{0}'", this.requestId);
					flag = true;
				}
				catch when (endfilter(true))
				{
					num2 = Marshal.GetExceptionCode();
					ExTraceGlobals.RpcTracer.TraceError<Guid, int>((long)this.GetHashCode(), "BeginExecuteStep: ExecuteStepAsync Exception - requestId='{0}' rpcStatus='{1}", this.requestId, num2);
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(num2, "BeginExecuteStep, ExecuteStepAsync");
				}
			}
			finally
			{
				ExTraceGlobals.RpcTracer.TraceDebug<Guid>((long)this.GetHashCode(), "BeginExecuteStep: Freeing input bytes for requestId='{0}'", this.requestId);
				if (null != ptr)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
				if (!flag)
				{
					ExTraceGlobals.RpcTracer.TraceDebug<Guid>((long)this.GetHashCode(), "BeginExecuteStep: Disposing async result for requestId='{0}'", this.requestId);
					if (mobileSpeechRecoAsyncResult != null)
					{
						((IDisposable)mobileSpeechRecoAsyncResult).Dispose();
						mobileSpeechRecoAsyncResult = null;
					}
				}
			}
			return mobileSpeechRecoAsyncResult;
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00058040 File Offset: 0x00057440
		protected unsafe int EndExecuteStep(IAsyncResult asyncResult, out byte[] outBlob, out bool timedOut)
		{
			ExTraceGlobals.RpcTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Entering EndExecuteStep for requestId='{0}'", this.requestId);
			int result = 0;
			outBlob = null;
			timedOut = false;
			if (null == asyncResult)
			{
				throw new ArgumentNullException("asyncResult");
			}
			MobileSpeechRecoRpcClientBase.MobileSpeechRecoAsyncResult mobileSpeechRecoAsyncResult = asyncResult as MobileSpeechRecoRpcClientBase.MobileSpeechRecoAsyncResult;
			if (null == mobileSpeechRecoAsyncResult)
			{
				throw new ArgumentException("Invalid type", "asyncResult");
			}
			try
			{
				ExTraceGlobals.RpcTracer.TraceDebug<Guid, bool>((long)this.GetHashCode(), "EndExecuteStep: Completing async operation for requestId='{0}', timedOut='{1}'", this.requestId, mobileSpeechRecoAsyncResult.TimedOut);
				result = mobileSpeechRecoAsyncResult.Complete();
				byte timedOut2 = mobileSpeechRecoAsyncResult.TimedOut ? 1 : 0;
				timedOut = (timedOut2 != 0);
				if (timedOut2 == 0)
				{
					ExTraceGlobals.RpcTracer.TraceDebug<Guid>((long)this.GetHashCode(), "EndExecuteStep: Getting outBlob for requestId='{0}'", this.requestId);
					outBlob = <Module>.UToMBytes(*(int*)(mobileSpeechRecoAsyncResult.NativeState() + 112L / (long)sizeof(MobileSpeechRecoAsyncState)), *(long*)(mobileSpeechRecoAsyncResult.NativeState() + 120L / (long)sizeof(MobileSpeechRecoAsyncState)));
				}
			}
			finally
			{
				ExTraceGlobals.RpcTracer.TraceDebug<Guid>((long)this.GetHashCode(), "EndExecuteStep: Disposing result for requestId='{0}'", this.requestId);
				if (mobileSpeechRecoAsyncResult != null)
				{
					((IDisposable)mobileSpeechRecoAsyncResult).Dispose();
				}
			}
			return result;
		}

		// Token: 0x04001031 RID: 4145
		private Guid requestId;

		// Token: 0x02000405 RID: 1029
		private class MobileSpeechRecoAsyncResult : IAsyncResult, IDisposable
		{
			// Token: 0x0600118B RID: 4491 RVA: 0x00058164 File Offset: 0x00057564
			public unsafe MobileSpeechRecoAsyncResult(Guid requestId, AsyncCallback callback)
			{
				ExTraceGlobals.RpcTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Entering MobileSpeechRecoAsyncResult constructor for requestId='{0}'", requestId);
				MobileSpeechRecoAsyncState* ptr = <Module>.@new(128UL);
				MobileSpeechRecoAsyncState* ptr2;
				try
				{
					if (ptr != null)
					{
						*(int*)(ptr + 112L / (long)sizeof(MobileSpeechRecoAsyncState)) = 0;
						*(long*)(ptr + 120L / (long)sizeof(MobileSpeechRecoAsyncState)) = 0L;
						ptr2 = ptr;
					}
					else
					{
						ptr2 = 0L;
					}
				}
				catch
				{
					<Module>.delete((void*)ptr);
					throw;
				}
				this.pAsyncState = ptr2;
				if (0L == ptr2)
				{
					throw new OutOfMemoryException();
				}
				this.requestId = requestId;
				this.callback = callback;
				this.timedOut = false;
				this.completedEvent = new ManualResetEvent(false);
				this.callbackWaitHandle = ThreadPool.RegisterWaitForSingleObject(this.completedEvent, new WaitOrTimerCallback(this.OnRpcComplete), null, MobileSpeechRecoRpcClientBase.MobileSpeechRecoAsyncResult.RequestTimeout, true);
			}

			// Token: 0x0600118C RID: 4492 RVA: 0x00058230 File Offset: 0x00057630
			private unsafe void ~MobileSpeechRecoAsyncResult()
			{
				ExTraceGlobals.RpcTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Disposing MobileSpeechRecoAsyncResult for requestId='{0}'", this.requestId);
				MobileSpeechRecoAsyncState* ptr = this.pAsyncState;
				if (null != ptr)
				{
					MobileSpeechRecoAsyncState* ptr2 = ptr;
					ulong num = (ulong)(*(long*)(ptr2 + 120L / (long)sizeof(MobileSpeechRecoAsyncState)));
					if (0UL != num)
					{
						<Module>.MIDL_user_free(num);
					}
					<Module>.delete((void*)ptr2);
					this.pAsyncState = null;
				}
				ManualResetEvent manualResetEvent = this.completedEvent;
				if (null != manualResetEvent)
				{
					RegisteredWaitHandle registeredWaitHandle = this.callbackWaitHandle;
					if (null != registeredWaitHandle)
					{
						registeredWaitHandle.Unregister(manualResetEvent);
						this.callbackWaitHandle = null;
					}
					IDisposable disposable = this.completedEvent;
					if (disposable != null)
					{
						disposable.Dispose();
					}
					this.completedEvent = null;
				}
			}

			// Token: 0x1700020B RID: 523
			// (get) Token: 0x0600118D RID: 4493 RVA: 0x000578C0 File Offset: 0x00056CC0
			public virtual bool IsCompleted
			{
				[return: MarshalAs(UnmanagedType.U1)]
				get
				{
					return this.completedEvent.WaitOne(0);
				}
			}

			// Token: 0x1700020A RID: 522
			// (get) Token: 0x0600118E RID: 4494 RVA: 0x000578DC File Offset: 0x00056CDC
			public virtual WaitHandle AsyncWaitHandle
			{
				get
				{
					return this.completedEvent;
				}
			}

			// Token: 0x17000209 RID: 521
			// (get) Token: 0x0600118F RID: 4495 RVA: 0x000578F0 File Offset: 0x00056CF0
			public virtual object AsyncState
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000208 RID: 520
			// (get) Token: 0x06001190 RID: 4496 RVA: 0x00057900 File Offset: 0x00056D00
			public virtual bool CompletedSynchronously
			{
				[return: MarshalAs(UnmanagedType.U1)]
				get
				{
					return false;
				}
			}

			// Token: 0x17000207 RID: 519
			// (get) Token: 0x06001191 RID: 4497 RVA: 0x00057910 File Offset: 0x00056D10
			public bool TimedOut
			{
				[return: MarshalAs(UnmanagedType.U1)]
				get
				{
					return this.timedOut;
				}
			}

			// Token: 0x06001192 RID: 4498 RVA: 0x00057924 File Offset: 0x00056D24
			public void OnRpcComplete(object state, [MarshalAs(UnmanagedType.U1)] bool timedOut)
			{
				ExTraceGlobals.RpcTracer.TraceDebug<Guid, bool>((long)this.GetHashCode(), "Entering MobileSpeechRecoAsyncResult.OnRpcComplete for requestId='{0}', timedOut='{1}'", this.requestId, timedOut);
				this.timedOut = timedOut;
				this.callback(this);
			}

			// Token: 0x06001193 RID: 4499 RVA: 0x00057968 File Offset: 0x00056D68
			public unsafe int Complete()
			{
				int result = 0;
				ExTraceGlobals.RpcTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Entering MobileSpeechRecoAsyncResult.Complete for requestId='{0}'", this.requestId);
				if (this.timedOut)
				{
					ExTraceGlobals.RpcTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Complete: requestId='{0}' timed out", this.requestId);
					int num = <Module>.RpcAsyncCancelCall((_RPC_ASYNC_STATE*)this.pAsyncState, 1);
					if (num != null)
					{
						ExTraceGlobals.RpcTracer.TraceError<Guid, int>((long)this.GetHashCode(), "Complete: RpcAsyncCancelCall Exception - requestId='{0}' rpcStatus='{1}", this.requestId, num);
						<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(num, "Complete, RpcAsyncCancelCall");
					}
					this.completedEvent.WaitOne();
				}
				int num2 = <Module>.RpcAsyncCompleteCall((_RPC_ASYNC_STATE*)this.pAsyncState, (void*)(&result));
				if (num2 != null)
				{
					ExTraceGlobals.RpcTracer.TraceError<Guid, int>((long)this.GetHashCode(), "Complete: RpcAsyncCompleteCall Exception - requestId='{0}' rpcStatus='{1}", this.requestId, num2);
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(num2, "Complete, RpcAsyncCompleteCall");
				}
				return result;
			}

			// Token: 0x06001194 RID: 4500 RVA: 0x00057A4C File Offset: 0x00056E4C
			public unsafe MobileSpeechRecoAsyncState* NativeState()
			{
				return this.pAsyncState;
			}

			// Token: 0x06001195 RID: 4501 RVA: 0x000582D0 File Offset: 0x000576D0
			protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
			{
				if (A_0)
				{
					this.~MobileSpeechRecoAsyncResult();
				}
				else
				{
					base.Finalize();
				}
			}

			// Token: 0x06001196 RID: 4502 RVA: 0x00058518 File Offset: 0x00057918
			public sealed void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06001197 RID: 4503 RVA: 0x00057A60 File Offset: 0x00056E60
			// Note: this type is marked as 'beforefieldinit'.
			static MobileSpeechRecoAsyncResult()
			{
				TimeSpan requestTimeout = TimeSpan.FromSeconds(90.0);
				MobileSpeechRecoRpcClientBase.MobileSpeechRecoAsyncResult.RequestTimeout = requestTimeout;
			}

			// Token: 0x04001032 RID: 4146
			private static TimeSpan RequestTimeout;

			// Token: 0x04001033 RID: 4147
			private Guid requestId;

			// Token: 0x04001034 RID: 4148
			private AsyncCallback callback;

			// Token: 0x04001035 RID: 4149
			private bool timedOut;

			// Token: 0x04001036 RID: 4150
			private ManualResetEvent completedEvent;

			// Token: 0x04001037 RID: 4151
			private RegisteredWaitHandle callbackWaitHandle;

			// Token: 0x04001038 RID: 4152
			private unsafe MobileSpeechRecoAsyncState* pAsyncState;
		}
	}
}
