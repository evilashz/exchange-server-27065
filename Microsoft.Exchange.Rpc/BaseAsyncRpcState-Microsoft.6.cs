using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc.ExchangeServer;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000206 RID: 518
	internal abstract class BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationWait,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_AsyncEMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>
	{
		// Token: 0x06000B2D RID: 2861 RVA: 0x00022140 File Offset: 0x00021540
		private static void Callback(ICancelableAsyncResult asyncResult)
		{
			((BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationWait,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_AsyncEMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>)asyncResult.AsyncState).InternalCallback(asyncResult);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0001F364 File Offset: 0x0001E764
		protected BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationWait,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_AsyncEMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>()
		{
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0001F3B8 File Offset: 0x0001E7B8
		protected SafeRpcAsyncStateHandle AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0001F3CC File Offset: 0x0001E7CC
		protected ExchangeAsyncRpcServer_AsyncEMSMDB AsyncServer
		{
			get
			{
				return this.asyncServer;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0001F3E0 File Offset: 0x0001E7E0
		protected IExchangeAsyncDispatch AsyncDispatch
		{
			get
			{
				return this.asyncDispatch;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x0001F3F4 File Offset: 0x0001E7F4
		protected ArraySegment<byte> EmptyByteArraySegment
		{
			get
			{
				return BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationWait,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_AsyncEMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.emptyByteArraySegment;
			}
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0001F38C File Offset: 0x0001E78C
		protected void InternalInitialize(SafeRpcAsyncStateHandle asyncState, ExchangeAsyncRpcServer_AsyncEMSMDB asyncServer)
		{
			this.asyncState = asyncState;
			this.asyncServer = asyncServer;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00021900 File Offset: 0x00020D00
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

		// Token: 0x06000B35 RID: 2869 RVA: 0x00020908 File Offset: 0x0001FD08
		protected void ReleaseToPool()
		{
			@lock @lock = null;
			this.InternalReset();
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
			ExchangeAsyncRpcState_NotificationWait item = (ExchangeAsyncRpcState_NotificationWait)this;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationWait,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_AsyncEMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationWait,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_AsyncEMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Count < this.PoolSize())
				{
					BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationWait,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_AsyncEMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Push(item);
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x000204D4 File Offset: 0x0001F8D4
		public static ExchangeAsyncRpcState_NotificationWait CreateFromPool()
		{
			@lock @lock = null;
			ExchangeAsyncRpcState_NotificationWait exchangeAsyncRpcState_NotificationWait = null;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationWait,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_AsyncEMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationWait,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_AsyncEMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Count > 0)
				{
					exchangeAsyncRpcState_NotificationWait = BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationWait,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_AsyncEMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.freePool.Pop();
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			if (exchangeAsyncRpcState_NotificationWait == null)
			{
				exchangeAsyncRpcState_NotificationWait = new ExchangeAsyncRpcState_NotificationWait();
			}
			return exchangeAsyncRpcState_NotificationWait;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0001F3A8 File Offset: 0x0001E7A8
		public virtual int PoolSize()
		{
			return 100;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00021148 File Offset: 0x00020548
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
					this.InternalBegin(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationWait,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_AsyncEMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.asyncCallback);
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

		// Token: 0x06000B39 RID: 2873
		public abstract void InternalBegin(CancelableAsyncCallback asyncCallback);

		// Token: 0x06000B3A RID: 2874
		public abstract int InternalEnd(ICancelableAsyncResult asyncResult);

		// Token: 0x06000B3B RID: 2875
		public abstract void InternalReset();

		// Token: 0x06000B3C RID: 2876 RVA: 0x000222F0 File Offset: 0x000216F0
		// Note: this type is marked as 'beforefieldinit'.
		static BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationWait,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_AsyncEMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>()
		{
			ArraySegment<byte> arraySegment = new ArraySegment<byte>(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationWait,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_AsyncEMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.emptyByteArray);
			BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationWait,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_AsyncEMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.emptyByteArraySegment = arraySegment;
		}

		// Token: 0x04000C49 RID: 3145
		private static readonly object freePoolLock = new object();

		// Token: 0x04000C4A RID: 3146
		private static readonly Stack<ExchangeAsyncRpcState_NotificationWait> freePool = new Stack<ExchangeAsyncRpcState_NotificationWait>();

		// Token: 0x04000C4B RID: 3147
		private static readonly CancelableAsyncCallback asyncCallback = new CancelableAsyncCallback(BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationWait,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_AsyncEMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.Callback);

		// Token: 0x04000C4C RID: 3148
		private static readonly byte[] emptyByteArray = new byte[0];

		// Token: 0x04000C4D RID: 3149
		private static readonly ArraySegment<byte> emptyByteArraySegment;

		// Token: 0x04000C4E RID: 3150
		private SafeRpcAsyncStateHandle asyncState;

		// Token: 0x04000C4F RID: 3151
		private ExchangeAsyncRpcServer_AsyncEMSMDB asyncServer;

		// Token: 0x04000C50 RID: 3152
		private IExchangeAsyncDispatch asyncDispatch;
	}
}
