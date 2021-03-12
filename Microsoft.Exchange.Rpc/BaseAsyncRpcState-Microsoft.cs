using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc.ExchangeServer;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020001FC RID: 508
	internal abstract class BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Connect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>
	{
		// Token: 0x06000AC2 RID: 2754 RVA: 0x000220A0 File Offset: 0x000214A0
		private static void Callback(ICancelableAsyncResult asyncResult)
		{
			((BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Connect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>)asyncResult.AsyncState).InternalCallback(asyncResult);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0001F030 File Offset: 0x0001E430
		protected BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Connect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>()
		{
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x0001F084 File Offset: 0x0001E484
		protected SafeRpcAsyncStateHandle AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0001F098 File Offset: 0x0001E498
		protected ExchangeAsyncRpcServer_EMSMDB AsyncServer
		{
			get
			{
				return this.asyncServer;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0001F0AC File Offset: 0x0001E4AC
		protected IExchangeAsyncDispatch AsyncDispatch
		{
			get
			{
				return this.asyncDispatch;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0001F0C0 File Offset: 0x0001E4C0
		protected ArraySegment<byte> EmptyByteArraySegment
		{
			get
			{
				return BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Connect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.emptyByteArraySegment;
			}
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0001F058 File Offset: 0x0001E458
		protected void InternalInitialize(SafeRpcAsyncStateHandle asyncState, ExchangeAsyncRpcServer_EMSMDB asyncServer)
		{
			this.asyncState = asyncState;
			this.asyncServer = asyncServer;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x000212C0 File Offset: 0x000206C0
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

		// Token: 0x06000ACA RID: 2762 RVA: 0x00020660 File Offset: 0x0001FA60
		protected void ReleaseToPool()
		{
			@lock @lock = null;
			this.InternalReset();
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
			ExchangeAsyncRpcState_Connect item = (ExchangeAsyncRpcState_Connect)this;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Connect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Connect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Count < this.PoolSize())
				{
					BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Connect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Push(item);
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x000202A4 File Offset: 0x0001F6A4
		public static ExchangeAsyncRpcState_Connect CreateFromPool()
		{
			@lock @lock = null;
			ExchangeAsyncRpcState_Connect exchangeAsyncRpcState_Connect = null;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Connect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Connect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Count > 0)
				{
					exchangeAsyncRpcState_Connect = BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Connect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Pop();
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			if (exchangeAsyncRpcState_Connect == null)
			{
				exchangeAsyncRpcState_Connect = new ExchangeAsyncRpcState_Connect();
			}
			return exchangeAsyncRpcState_Connect;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0001F074 File Offset: 0x0001E474
		public virtual int PoolSize()
		{
			return 100;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x000209F0 File Offset: 0x0001FDF0
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
					this.InternalBegin(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Connect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.asyncCallback);
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

		// Token: 0x06000ACE RID: 2766
		public abstract void InternalBegin(CancelableAsyncCallback asyncCallback);

		// Token: 0x06000ACF RID: 2767
		public abstract int InternalEnd(ICancelableAsyncResult asyncResult);

		// Token: 0x06000AD0 RID: 2768
		public abstract void InternalReset();

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00022160 File Offset: 0x00021560
		// Note: this type is marked as 'beforefieldinit'.
		static BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Connect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>()
		{
			ArraySegment<byte> arraySegment = new ArraySegment<byte>(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Connect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.emptyByteArray);
			BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Connect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.emptyByteArraySegment = arraySegment;
		}

		// Token: 0x04000BF2 RID: 3058
		private static readonly object freePoolLock = new object();

		// Token: 0x04000BF3 RID: 3059
		private static readonly Stack<ExchangeAsyncRpcState_Connect> freePool = new Stack<ExchangeAsyncRpcState_Connect>();

		// Token: 0x04000BF4 RID: 3060
		private static readonly CancelableAsyncCallback asyncCallback = new CancelableAsyncCallback(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Connect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.Callback);

		// Token: 0x04000BF5 RID: 3061
		private static readonly byte[] emptyByteArray = new byte[0];

		// Token: 0x04000BF6 RID: 3062
		private static readonly ArraySegment<byte> emptyByteArraySegment;

		// Token: 0x04000BF7 RID: 3063
		private SafeRpcAsyncStateHandle asyncState;

		// Token: 0x04000BF8 RID: 3064
		private ExchangeAsyncRpcServer_EMSMDB asyncServer;

		// Token: 0x04000BF9 RID: 3065
		private IExchangeAsyncDispatch asyncDispatch;
	}
}
