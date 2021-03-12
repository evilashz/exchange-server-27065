using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc.ExchangeServer;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000202 RID: 514
	internal abstract class BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Execute,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>
	{
		// Token: 0x06000B02 RID: 2818 RVA: 0x00022100 File Offset: 0x00021500
		private static void Callback(ICancelableAsyncResult asyncResult)
		{
			((BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Execute,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>)asyncResult.AsyncState).InternalCallback(asyncResult);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0001F21C File Offset: 0x0001E61C
		protected BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Execute,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>()
		{
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x0001F270 File Offset: 0x0001E670
		protected SafeRpcAsyncStateHandle AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x0001F284 File Offset: 0x0001E684
		protected ExchangeAsyncRpcServer_EMSMDB AsyncServer
		{
			get
			{
				return this.asyncServer;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x0001F298 File Offset: 0x0001E698
		protected IExchangeAsyncDispatch AsyncDispatch
		{
			get
			{
				return this.asyncDispatch;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x0001F2AC File Offset: 0x0001E6AC
		protected ArraySegment<byte> EmptyByteArraySegment
		{
			get
			{
				return BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Execute,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.emptyByteArraySegment;
			}
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0001F244 File Offset: 0x0001E644
		protected void InternalInitialize(SafeRpcAsyncStateHandle asyncState, ExchangeAsyncRpcServer_EMSMDB asyncServer)
		{
			this.asyncState = asyncState;
			this.asyncServer = asyncServer;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00021680 File Offset: 0x00020A80
		protected void InternalCallback(ICancelableAsyncResult asyncResult)
		{
			try
			{
				int result = this.InternalEnd(asyncResult);
				this.asyncState.CompleteCall(result);
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
			finally
			{
				this.ReleaseToPool();
			}
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x000207F8 File Offset: 0x0001FBF8
		protected void ReleaseToPool()
		{
			@lock @lock = null;
			this.InternalReset();
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
			ExchangeAsyncRpcState_Execute item = (ExchangeAsyncRpcState_Execute)this;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Execute,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Execute,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Count < this.PoolSize())
				{
					BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Execute,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Push(item);
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x000203F4 File Offset: 0x0001F7F4
		public static ExchangeAsyncRpcState_Execute CreateFromPool()
		{
			@lock @lock = null;
			ExchangeAsyncRpcState_Execute exchangeAsyncRpcState_Execute = null;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Execute,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Execute,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Count > 0)
				{
					exchangeAsyncRpcState_Execute = BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Execute,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Pop();
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			if (exchangeAsyncRpcState_Execute == null)
			{
				exchangeAsyncRpcState_Execute = new ExchangeAsyncRpcState_Execute();
			}
			return exchangeAsyncRpcState_Execute;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0001F260 File Offset: 0x0001E660
		public virtual int PoolSize()
		{
			return 100;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00020E58 File Offset: 0x00020258
		public void Begin()
		{
			bool flag = false;
			try
			{
				IExchangeAsyncDispatch exchangeAsyncDispatch = this.asyncServer.GetAsyncDispatch();
				this.asyncDispatch = exchangeAsyncDispatch;
				if (exchangeAsyncDispatch == null)
				{
					this.asyncState.AbortCall(1722U);
				}
				else
				{
					this.InternalBegin(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Execute,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.asyncCallback);
				}
				flag = true;
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
			finally
			{
				if (!flag)
				{
					this.ReleaseToPool();
				}
			}
		}

		// Token: 0x06000B0E RID: 2830
		public abstract void InternalBegin(CancelableAsyncCallback asyncCallback);

		// Token: 0x06000B0F RID: 2831
		public abstract int InternalEnd(ICancelableAsyncResult asyncResult);

		// Token: 0x06000B10 RID: 2832
		public abstract void InternalReset();

		// Token: 0x06000B11 RID: 2833 RVA: 0x00022250 File Offset: 0x00021650
		// Note: this type is marked as 'beforefieldinit'.
		static BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Execute,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>()
		{
			ArraySegment<byte> arraySegment = new ArraySegment<byte>(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Execute,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.emptyByteArray);
			BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Execute,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.emptyByteArraySegment = arraySegment;
		}

		// Token: 0x04000C25 RID: 3109
		private static readonly object freePoolLock = new object();

		// Token: 0x04000C26 RID: 3110
		private static readonly Stack<ExchangeAsyncRpcState_Execute> freePool = new Stack<ExchangeAsyncRpcState_Execute>();

		// Token: 0x04000C27 RID: 3111
		private static readonly CancelableAsyncCallback asyncCallback = new CancelableAsyncCallback(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Execute,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.Callback);

		// Token: 0x04000C28 RID: 3112
		private static readonly byte[] emptyByteArray = new byte[0];

		// Token: 0x04000C29 RID: 3113
		private static readonly ArraySegment<byte> emptyByteArraySegment;

		// Token: 0x04000C2A RID: 3114
		private SafeRpcAsyncStateHandle asyncState;

		// Token: 0x04000C2B RID: 3115
		private ExchangeAsyncRpcServer_EMSMDB asyncServer;

		// Token: 0x04000C2C RID: 3116
		private IExchangeAsyncDispatch asyncDispatch;
	}
}
