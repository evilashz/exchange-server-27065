using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc.ExchangeServer;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000200 RID: 512
	internal abstract class BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Disconnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>
	{
		// Token: 0x06000AED RID: 2797 RVA: 0x000220E0 File Offset: 0x000214E0
		private static void Callback(ICancelableAsyncResult asyncResult)
		{
			((BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Disconnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>)asyncResult.AsyncState).InternalCallback(asyncResult);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0001F178 File Offset: 0x0001E578
		protected BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Disconnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>()
		{
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x0001F1CC File Offset: 0x0001E5CC
		protected SafeRpcAsyncStateHandle AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x0001F1E0 File Offset: 0x0001E5E0
		protected ExchangeAsyncRpcServer_EMSMDB AsyncServer
		{
			get
			{
				return this.asyncServer;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x0001F1F4 File Offset: 0x0001E5F4
		protected IExchangeAsyncDispatch AsyncDispatch
		{
			get
			{
				return this.asyncDispatch;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0001F208 File Offset: 0x0001E608
		protected ArraySegment<byte> EmptyByteArraySegment
		{
			get
			{
				return BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Disconnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.emptyByteArraySegment;
			}
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0001F1A0 File Offset: 0x0001E5A0
		protected void InternalInitialize(SafeRpcAsyncStateHandle asyncState, ExchangeAsyncRpcServer_EMSMDB asyncServer)
		{
			this.asyncState = asyncState;
			this.asyncServer = asyncServer;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00021540 File Offset: 0x00020940
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

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00020770 File Offset: 0x0001FB70
		protected void ReleaseToPool()
		{
			@lock @lock = null;
			this.InternalReset();
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
			ExchangeAsyncRpcState_Disconnect item = (ExchangeAsyncRpcState_Disconnect)this;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Disconnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Disconnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Count < this.PoolSize())
				{
					BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Disconnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Push(item);
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00020384 File Offset: 0x0001F784
		public static ExchangeAsyncRpcState_Disconnect CreateFromPool()
		{
			@lock @lock = null;
			ExchangeAsyncRpcState_Disconnect exchangeAsyncRpcState_Disconnect = null;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Disconnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Disconnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Count > 0)
				{
					exchangeAsyncRpcState_Disconnect = BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Disconnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Pop();
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			if (exchangeAsyncRpcState_Disconnect == null)
			{
				exchangeAsyncRpcState_Disconnect = new ExchangeAsyncRpcState_Disconnect();
			}
			return exchangeAsyncRpcState_Disconnect;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0001F1BC File Offset: 0x0001E5BC
		public virtual int PoolSize()
		{
			return 100;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00020CE0 File Offset: 0x000200E0
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
					this.InternalBegin(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Disconnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.asyncCallback);
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

		// Token: 0x06000AF9 RID: 2809
		public abstract void InternalBegin(CancelableAsyncCallback asyncCallback);

		// Token: 0x06000AFA RID: 2810
		public abstract int InternalEnd(ICancelableAsyncResult asyncResult);

		// Token: 0x06000AFB RID: 2811
		public abstract void InternalReset();

		// Token: 0x06000AFC RID: 2812 RVA: 0x00022200 File Offset: 0x00021600
		// Note: this type is marked as 'beforefieldinit'.
		static BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Disconnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>()
		{
			ArraySegment<byte> arraySegment = new ArraySegment<byte>(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Disconnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.emptyByteArray);
			BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Disconnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.emptyByteArraySegment = arraySegment;
		}

		// Token: 0x04000C1C RID: 3100
		private static readonly object freePoolLock = new object();

		// Token: 0x04000C1D RID: 3101
		private static readonly Stack<ExchangeAsyncRpcState_Disconnect> freePool = new Stack<ExchangeAsyncRpcState_Disconnect>();

		// Token: 0x04000C1E RID: 3102
		private static readonly CancelableAsyncCallback asyncCallback = new CancelableAsyncCallback(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Disconnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.Callback);

		// Token: 0x04000C1F RID: 3103
		private static readonly byte[] emptyByteArray = new byte[0];

		// Token: 0x04000C20 RID: 3104
		private static readonly ArraySegment<byte> emptyByteArraySegment;

		// Token: 0x04000C21 RID: 3105
		private SafeRpcAsyncStateHandle asyncState;

		// Token: 0x04000C22 RID: 3106
		private ExchangeAsyncRpcServer_EMSMDB asyncServer;

		// Token: 0x04000C23 RID: 3107
		private IExchangeAsyncDispatch asyncDispatch;
	}
}
