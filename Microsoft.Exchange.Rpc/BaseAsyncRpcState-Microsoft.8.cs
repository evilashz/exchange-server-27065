using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc.NotificationsBroker;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020002E0 RID: 736
	internal abstract class BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Unsubscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>
	{
		// Token: 0x06000D1D RID: 3357 RVA: 0x00033AC4 File Offset: 0x00032EC4
		private static void Callback(ICancelableAsyncResult asyncResult)
		{
			((BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Unsubscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>)asyncResult.AsyncState).InternalCallback(asyncResult);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0003217C File Offset: 0x0003157C
		protected BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Unsubscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>()
		{
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x000321D0 File Offset: 0x000315D0
		protected SafeRpcAsyncStateHandle AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x000321E4 File Offset: 0x000315E4
		protected NotificationsBrokerAsyncRpcServer AsyncServer
		{
			get
			{
				return this.asyncServer;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x000321F8 File Offset: 0x000315F8
		protected INotificationsBrokerAsyncDispatch AsyncDispatch
		{
			get
			{
				return this.asyncDispatch;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x0003220C File Offset: 0x0003160C
		protected ArraySegment<byte> EmptyByteArraySegment
		{
			get
			{
				return BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Unsubscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.emptyByteArraySegment;
			}
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x000321A4 File Offset: 0x000315A4
		protected void InternalInitialize(SafeRpcAsyncStateHandle asyncState, NotificationsBrokerAsyncRpcServer asyncServer)
		{
			this.asyncState = asyncState;
			this.asyncServer = asyncServer;
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x000336FC File Offset: 0x00032AFC
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

		// Token: 0x06000D25 RID: 3365 RVA: 0x00032DD8 File Offset: 0x000321D8
		protected void ReleaseToPool()
		{
			@lock @lock = null;
			this.InternalReset();
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
			NotificationsBrokerAsyncRpcState_Unsubscribe item = (NotificationsBrokerAsyncRpcState_Unsubscribe)this;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Unsubscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Unsubscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.freePool.Count < this.PoolSize())
				{
					BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Unsubscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.freePool.Push(item);
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00032B54 File Offset: 0x00031F54
		public static NotificationsBrokerAsyncRpcState_Unsubscribe CreateFromPool()
		{
			@lock @lock = null;
			NotificationsBrokerAsyncRpcState_Unsubscribe notificationsBrokerAsyncRpcState_Unsubscribe = null;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Unsubscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Unsubscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.freePool.Count > 0)
				{
					notificationsBrokerAsyncRpcState_Unsubscribe = BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Unsubscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.freePool.Pop();
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			if (notificationsBrokerAsyncRpcState_Unsubscribe == null)
			{
				notificationsBrokerAsyncRpcState_Unsubscribe = new NotificationsBrokerAsyncRpcState_Unsubscribe();
			}
			return notificationsBrokerAsyncRpcState_Unsubscribe;
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x000321C0 File Offset: 0x000315C0
		public virtual int PoolSize()
		{
			return 100;
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x000332CC File Offset: 0x000326CC
		public void Begin()
		{
			bool flag = false;
			try
			{
				INotificationsBrokerAsyncDispatch notificationsBrokerAsyncDispatch = this.asyncServer.GetAsyncDispatch();
				this.asyncDispatch = notificationsBrokerAsyncDispatch;
				if (notificationsBrokerAsyncDispatch == null)
				{
					this.asyncState.AbortCall(1722U);
				}
				else
				{
					this.InternalBegin(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Unsubscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.asyncCallback);
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

		// Token: 0x06000D29 RID: 3369
		public abstract void InternalBegin(CancelableAsyncCallback asyncCallback);

		// Token: 0x06000D2A RID: 3370
		public abstract int InternalEnd(ICancelableAsyncResult asyncResult);

		// Token: 0x06000D2B RID: 3371
		public abstract void InternalReset();

		// Token: 0x06000D2C RID: 3372 RVA: 0x00033B54 File Offset: 0x00032F54
		// Note: this type is marked as 'beforefieldinit'.
		static BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Unsubscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>()
		{
			ArraySegment<byte> arraySegment = new ArraySegment<byte>(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Unsubscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.emptyByteArray);
			BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Unsubscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.emptyByteArraySegment = arraySegment;
		}

		// Token: 0x04000D9F RID: 3487
		private static readonly object freePoolLock = new object();

		// Token: 0x04000DA0 RID: 3488
		private static readonly Stack<NotificationsBrokerAsyncRpcState_Unsubscribe> freePool = new Stack<NotificationsBrokerAsyncRpcState_Unsubscribe>();

		// Token: 0x04000DA1 RID: 3489
		private static readonly CancelableAsyncCallback asyncCallback = new CancelableAsyncCallback(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Unsubscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.Callback);

		// Token: 0x04000DA2 RID: 3490
		private static readonly byte[] emptyByteArray = new byte[0];

		// Token: 0x04000DA3 RID: 3491
		private static readonly ArraySegment<byte> emptyByteArraySegment;

		// Token: 0x04000DA4 RID: 3492
		private SafeRpcAsyncStateHandle asyncState;

		// Token: 0x04000DA5 RID: 3493
		private NotificationsBrokerAsyncRpcServer asyncServer;

		// Token: 0x04000DA6 RID: 3494
		private INotificationsBrokerAsyncDispatch asyncDispatch;
	}
}
