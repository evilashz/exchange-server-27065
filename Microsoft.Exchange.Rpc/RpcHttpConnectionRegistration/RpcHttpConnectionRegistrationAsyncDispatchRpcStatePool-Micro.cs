using System;
using System.Collections.Generic;
using msclr;

namespace Microsoft.Exchange.Rpc.RpcHttpConnectionRegistration
{
	// Token: 0x020003CB RID: 971
	internal abstract class RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Register> : RpcHttpConnectionRegistrationAsyncDispatchRpcState
	{
		// Token: 0x060010DA RID: 4314 RVA: 0x00053BF4 File Offset: 0x00052FF4
		public static RpcHttpConnectionRegistrationAsyncDispatchRpcState_Register CreateFromPool()
		{
			@lock @lock = null;
			RpcHttpConnectionRegistrationAsyncDispatchRpcState_Register rpcHttpConnectionRegistrationAsyncDispatchRpcState_Register = null;
			@lock lock2 = new @lock(RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Register>.freePoolLock);
			try
			{
				@lock = lock2;
				if (RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Register>.freePool.Count > 0)
				{
					rpcHttpConnectionRegistrationAsyncDispatchRpcState_Register = RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Register>.freePool.Pop();
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			if (rpcHttpConnectionRegistrationAsyncDispatchRpcState_Register == null)
			{
				rpcHttpConnectionRegistrationAsyncDispatchRpcState_Register = new RpcHttpConnectionRegistrationAsyncDispatchRpcState_Register();
			}
			return rpcHttpConnectionRegistrationAsyncDispatchRpcState_Register;
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x000537E0 File Offset: 0x00052BE0
		public static Guid CopyIntPtrToGuid(IntPtr guidPtr)
		{
			return <Module>.FromGUID(guidPtr.ToPointer());
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x000537FC File Offset: 0x00052BFC
		public virtual int PoolSize()
		{
			return 100;
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x00053C64 File Offset: 0x00053064
		public override void ReleaseToPool()
		{
			@lock @lock = null;
			RpcHttpConnectionRegistrationAsyncDispatchRpcState_Register item = (RpcHttpConnectionRegistrationAsyncDispatchRpcState_Register)this;
			@lock lock2 = new @lock(RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Register>.freePoolLock);
			try
			{
				@lock = lock2;
				if (RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Register>.freePool.Count < this.PoolSize())
				{
					RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Register>.freePool.Push(item);
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00053250 File Offset: 0x00052650
		public RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Register>()
		{
		}

		// Token: 0x04000FE5 RID: 4069
		private static readonly object freePoolLock = new object();

		// Token: 0x04000FE6 RID: 4070
		private static readonly Stack<RpcHttpConnectionRegistrationAsyncDispatchRpcState_Register> freePool = new Stack<RpcHttpConnectionRegistrationAsyncDispatchRpcState_Register>();
	}
}
