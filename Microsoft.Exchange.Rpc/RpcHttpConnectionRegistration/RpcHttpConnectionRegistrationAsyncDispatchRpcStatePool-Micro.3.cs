using System;
using System.Collections.Generic;
using msclr;

namespace Microsoft.Exchange.Rpc.RpcHttpConnectionRegistration
{
	// Token: 0x020003CF RID: 975
	internal abstract class RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Clear> : RpcHttpConnectionRegistrationAsyncDispatchRpcState
	{
		// Token: 0x060010F0 RID: 4336 RVA: 0x00053DB4 File Offset: 0x000531B4
		public static RpcHttpConnectionRegistrationAsyncDispatchRpcState_Clear CreateFromPool()
		{
			@lock @lock = null;
			RpcHttpConnectionRegistrationAsyncDispatchRpcState_Clear rpcHttpConnectionRegistrationAsyncDispatchRpcState_Clear = null;
			@lock lock2 = new @lock(RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Clear>.freePoolLock);
			try
			{
				@lock = lock2;
				if (RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Clear>.freePool.Count > 0)
				{
					rpcHttpConnectionRegistrationAsyncDispatchRpcState_Clear = RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Clear>.freePool.Pop();
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			if (rpcHttpConnectionRegistrationAsyncDispatchRpcState_Clear == null)
			{
				rpcHttpConnectionRegistrationAsyncDispatchRpcState_Clear = new RpcHttpConnectionRegistrationAsyncDispatchRpcState_Clear();
			}
			return rpcHttpConnectionRegistrationAsyncDispatchRpcState_Clear;
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00053838 File Offset: 0x00052C38
		public virtual int PoolSize()
		{
			return 100;
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00053E24 File Offset: 0x00053224
		public override void ReleaseToPool()
		{
			@lock @lock = null;
			RpcHttpConnectionRegistrationAsyncDispatchRpcState_Clear item = (RpcHttpConnectionRegistrationAsyncDispatchRpcState_Clear)this;
			@lock lock2 = new @lock(RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Clear>.freePoolLock);
			try
			{
				@lock = lock2;
				if (RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Clear>.freePool.Count < this.PoolSize())
				{
					RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Clear>.freePool.Push(item);
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0005339C File Offset: 0x0005279C
		public RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Clear>()
		{
		}

		// Token: 0x04000FF5 RID: 4085
		private static readonly object freePoolLock = new object();

		// Token: 0x04000FF6 RID: 4086
		private static readonly Stack<RpcHttpConnectionRegistrationAsyncDispatchRpcState_Clear> freePool = new Stack<RpcHttpConnectionRegistrationAsyncDispatchRpcState_Clear>();
	}
}
