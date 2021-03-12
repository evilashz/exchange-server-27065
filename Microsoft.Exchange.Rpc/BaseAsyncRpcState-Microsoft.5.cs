using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc.ExchangeServer;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000204 RID: 516
	internal abstract class BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationConnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>
	{
		// Token: 0x06000B18 RID: 2840 RVA: 0x00022120 File Offset: 0x00021520
		private static void Callback(ICancelableAsyncResult asyncResult)
		{
			((BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationConnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>)asyncResult.AsyncState).InternalCallback(asyncResult);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0001F2C0 File Offset: 0x0001E6C0
		protected BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationConnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>()
		{
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x0001F314 File Offset: 0x0001E714
		protected SafeRpcAsyncStateHandle AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0001F328 File Offset: 0x0001E728
		protected ExchangeAsyncRpcServer_EMSMDB AsyncServer
		{
			get
			{
				return this.asyncServer;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0001F33C File Offset: 0x0001E73C
		protected IExchangeAsyncDispatch AsyncDispatch
		{
			get
			{
				return this.asyncDispatch;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0001F350 File Offset: 0x0001E750
		protected ArraySegment<byte> EmptyByteArraySegment
		{
			get
			{
				return BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationConnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.emptyByteArraySegment;
			}
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0001F2E8 File Offset: 0x0001E6E8
		protected void InternalInitialize(SafeRpcAsyncStateHandle asyncState, ExchangeAsyncRpcServer_EMSMDB asyncServer)
		{
			this.asyncState = asyncState;
			this.asyncServer = asyncServer;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x000217C0 File Offset: 0x00020BC0
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

		// Token: 0x06000B20 RID: 2848 RVA: 0x00020880 File Offset: 0x0001FC80
		protected void ReleaseToPool()
		{
			@lock @lock = null;
			this.InternalReset();
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
			ExchangeAsyncRpcState_NotificationConnect item = (ExchangeAsyncRpcState_NotificationConnect)this;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationConnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationConnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Count < this.PoolSize())
				{
					BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationConnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Push(item);
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00020464 File Offset: 0x0001F864
		public static ExchangeAsyncRpcState_NotificationConnect CreateFromPool()
		{
			@lock @lock = null;
			ExchangeAsyncRpcState_NotificationConnect exchangeAsyncRpcState_NotificationConnect = null;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationConnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationConnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Count > 0)
				{
					exchangeAsyncRpcState_NotificationConnect = BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationConnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Pop();
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			if (exchangeAsyncRpcState_NotificationConnect == null)
			{
				exchangeAsyncRpcState_NotificationConnect = new ExchangeAsyncRpcState_NotificationConnect();
			}
			return exchangeAsyncRpcState_NotificationConnect;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0001F304 File Offset: 0x0001E704
		public virtual int PoolSize()
		{
			return 100;
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00020FD0 File Offset: 0x000203D0
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
					this.InternalBegin(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationConnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.asyncCallback);
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

		// Token: 0x06000B24 RID: 2852
		public abstract void InternalBegin(CancelableAsyncCallback asyncCallback);

		// Token: 0x06000B25 RID: 2853
		public abstract int InternalEnd(ICancelableAsyncResult asyncResult);

		// Token: 0x06000B26 RID: 2854
		public abstract void InternalReset();

		// Token: 0x06000B27 RID: 2855 RVA: 0x000222A0 File Offset: 0x000216A0
		// Note: this type is marked as 'beforefieldinit'.
		static BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationConnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>()
		{
			ArraySegment<byte> arraySegment = new ArraySegment<byte>(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationConnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.emptyByteArray);
			BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationConnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.emptyByteArraySegment = arraySegment;
		}

		// Token: 0x04000C3F RID: 3135
		private static readonly object freePoolLock = new object();

		// Token: 0x04000C40 RID: 3136
		private static readonly Stack<ExchangeAsyncRpcState_NotificationConnect> freePool = new Stack<ExchangeAsyncRpcState_NotificationConnect>();

		// Token: 0x04000C41 RID: 3137
		private static readonly CancelableAsyncCallback asyncCallback = new CancelableAsyncCallback(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationConnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.Callback);

		// Token: 0x04000C42 RID: 3138
		private static readonly byte[] emptyByteArray = new byte[0];

		// Token: 0x04000C43 RID: 3139
		private static readonly ArraySegment<byte> emptyByteArraySegment;

		// Token: 0x04000C44 RID: 3140
		private SafeRpcAsyncStateHandle asyncState;

		// Token: 0x04000C45 RID: 3141
		private ExchangeAsyncRpcServer_EMSMDB asyncServer;

		// Token: 0x04000C46 RID: 3142
		private IExchangeAsyncDispatch asyncDispatch;
	}
}
