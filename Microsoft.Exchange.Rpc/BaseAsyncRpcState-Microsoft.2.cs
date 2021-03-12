using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc.ExchangeServer;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020001FE RID: 510
	internal abstract class BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Dummy,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>
	{
		// Token: 0x06000AD8 RID: 2776 RVA: 0x000220C0 File Offset: 0x000214C0
		private static void Callback(ICancelableAsyncResult asyncResult)
		{
			((BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Dummy,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>)asyncResult.AsyncState).InternalCallback(asyncResult);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0001F0D4 File Offset: 0x0001E4D4
		protected BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Dummy,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>()
		{
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0001F128 File Offset: 0x0001E528
		protected SafeRpcAsyncStateHandle AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0001F13C File Offset: 0x0001E53C
		protected ExchangeAsyncRpcServer_EMSMDB AsyncServer
		{
			get
			{
				return this.asyncServer;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0001F150 File Offset: 0x0001E550
		protected IExchangeAsyncDispatch AsyncDispatch
		{
			get
			{
				return this.asyncDispatch;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0001F164 File Offset: 0x0001E564
		protected ArraySegment<byte> EmptyByteArraySegment
		{
			get
			{
				return BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Dummy,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.emptyByteArraySegment;
			}
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0001F0FC File Offset: 0x0001E4FC
		protected void InternalInitialize(SafeRpcAsyncStateHandle asyncState, ExchangeAsyncRpcServer_EMSMDB asyncServer)
		{
			this.asyncState = asyncState;
			this.asyncServer = asyncServer;
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00021400 File Offset: 0x00020800
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

		// Token: 0x06000AE0 RID: 2784 RVA: 0x000206E8 File Offset: 0x0001FAE8
		protected void ReleaseToPool()
		{
			@lock @lock = null;
			this.InternalReset();
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
			ExchangeAsyncRpcState_Dummy item = (ExchangeAsyncRpcState_Dummy)this;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Dummy,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Dummy,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Count < this.PoolSize())
				{
					BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Dummy,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Push(item);
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00020314 File Offset: 0x0001F714
		public static ExchangeAsyncRpcState_Dummy CreateFromPool()
		{
			@lock @lock = null;
			ExchangeAsyncRpcState_Dummy exchangeAsyncRpcState_Dummy = null;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Dummy,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Dummy,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Count > 0)
				{
					exchangeAsyncRpcState_Dummy = BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Dummy,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Pop();
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			if (exchangeAsyncRpcState_Dummy == null)
			{
				exchangeAsyncRpcState_Dummy = new ExchangeAsyncRpcState_Dummy();
			}
			return exchangeAsyncRpcState_Dummy;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0001F118 File Offset: 0x0001E518
		public virtual int PoolSize()
		{
			return 100;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00020B68 File Offset: 0x0001FF68
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
					this.InternalBegin(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Dummy,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.asyncCallback);
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

		// Token: 0x06000AE4 RID: 2788
		public abstract void InternalBegin(CancelableAsyncCallback asyncCallback);

		// Token: 0x06000AE5 RID: 2789
		public abstract int InternalEnd(ICancelableAsyncResult asyncResult);

		// Token: 0x06000AE6 RID: 2790
		public abstract void InternalReset();

		// Token: 0x06000AE7 RID: 2791 RVA: 0x000221B0 File Offset: 0x000215B0
		// Note: this type is marked as 'beforefieldinit'.
		static BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Dummy,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>()
		{
			ArraySegment<byte> arraySegment = new ArraySegment<byte>(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Dummy,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.emptyByteArray);
			BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Dummy,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.emptyByteArraySegment = arraySegment;
		}

		// Token: 0x04000C13 RID: 3091
		private static readonly object freePoolLock = new object();

		// Token: 0x04000C14 RID: 3092
		private static readonly Stack<ExchangeAsyncRpcState_Dummy> freePool = new Stack<ExchangeAsyncRpcState_Dummy>();

		// Token: 0x04000C15 RID: 3093
		private static readonly CancelableAsyncCallback asyncCallback = new CancelableAsyncCallback(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Dummy,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.Callback);

		// Token: 0x04000C16 RID: 3094
		private static readonly byte[] emptyByteArray = new byte[0];

		// Token: 0x04000C17 RID: 3095
		private static readonly ArraySegment<byte> emptyByteArraySegment;

		// Token: 0x04000C18 RID: 3096
		private SafeRpcAsyncStateHandle asyncState;

		// Token: 0x04000C19 RID: 3097
		private ExchangeAsyncRpcServer_EMSMDB asyncServer;

		// Token: 0x04000C1A RID: 3098
		private IExchangeAsyncDispatch asyncDispatch;
	}
}
