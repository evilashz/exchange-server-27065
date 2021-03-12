using System;
using System.Collections.Generic;
using msclr;

namespace Microsoft.Exchange.Rpc.RpcHttpConnectionRegistration
{
	// Token: 0x020003CD RID: 973
	internal abstract class RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister> : RpcHttpConnectionRegistrationAsyncDispatchRpcState
	{
		// Token: 0x060010E5 RID: 4325 RVA: 0x00053CD4 File Offset: 0x000530D4
		public static RpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister CreateFromPool()
		{
			@lock @lock = null;
			RpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister rpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister = null;
			@lock lock2 = new @lock(RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister>.freePoolLock);
			try
			{
				@lock = lock2;
				if (RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister>.freePool.Count > 0)
				{
					rpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister = RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister>.freePool.Pop();
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			if (rpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister == null)
			{
				rpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister = new RpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister();
			}
			return rpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister;
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0005380C File Offset: 0x00052C0C
		public static Guid CopyIntPtrToGuid(IntPtr guidPtr)
		{
			return <Module>.FromGUID(guidPtr.ToPointer());
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x00053828 File Offset: 0x00052C28
		public virtual int PoolSize()
		{
			return 100;
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00053D44 File Offset: 0x00053144
		public override void ReleaseToPool()
		{
			@lock @lock = null;
			RpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister item = (RpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister)this;
			@lock lock2 = new @lock(RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister>.freePoolLock);
			try
			{
				@lock = lock2;
				if (RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister>.freePool.Count < this.PoolSize())
				{
					RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister>.freePool.Push(item);
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x000532FC File Offset: 0x000526FC
		public RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister>()
		{
		}

		// Token: 0x04000FF0 RID: 4080
		private static readonly object freePoolLock = new object();

		// Token: 0x04000FF1 RID: 4081
		private static readonly Stack<RpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister> freePool = new Stack<RpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister>();
	}
}
