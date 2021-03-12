using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc.NotificationsBroker;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020002DD RID: 733
	internal abstract class BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Subscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>
	{
		// Token: 0x06000D06 RID: 3334 RVA: 0x00033AA4 File Offset: 0x00032EA4
		private static void Callback(ICancelableAsyncResult asyncResult)
		{
			((BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Subscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>)asyncResult.AsyncState).InternalCallback(asyncResult);
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x000320D8 File Offset: 0x000314D8
		protected BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Subscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>()
		{
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0003212C File Offset: 0x0003152C
		protected SafeRpcAsyncStateHandle AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x00032140 File Offset: 0x00031540
		protected NotificationsBrokerAsyncRpcServer AsyncServer
		{
			get
			{
				return this.asyncServer;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x00032154 File Offset: 0x00031554
		protected INotificationsBrokerAsyncDispatch AsyncDispatch
		{
			get
			{
				return this.asyncDispatch;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x00032168 File Offset: 0x00031568
		protected ArraySegment<byte> EmptyByteArraySegment
		{
			get
			{
				return BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Subscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.emptyByteArraySegment;
			}
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00032100 File Offset: 0x00031500
		protected void InternalInitialize(SafeRpcAsyncStateHandle asyncState, NotificationsBrokerAsyncRpcServer asyncServer)
		{
			this.asyncState = asyncState;
			this.asyncServer = asyncServer;
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x000335BC File Offset: 0x000329BC
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

		// Token: 0x06000D0E RID: 3342 RVA: 0x00032D50 File Offset: 0x00032150
		protected void ReleaseToPool()
		{
			@lock @lock = null;
			this.InternalReset();
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
			NotificationsBrokerAsyncRpcState_Subscribe item = (NotificationsBrokerAsyncRpcState_Subscribe)this;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Subscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Subscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.freePool.Count < this.PoolSize())
				{
					BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Subscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.freePool.Push(item);
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00032AE4 File Offset: 0x00031EE4
		public static NotificationsBrokerAsyncRpcState_Subscribe CreateFromPool()
		{
			@lock @lock = null;
			NotificationsBrokerAsyncRpcState_Subscribe notificationsBrokerAsyncRpcState_Subscribe = null;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Subscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Subscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.freePool.Count > 0)
				{
					notificationsBrokerAsyncRpcState_Subscribe = BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Subscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.freePool.Pop();
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			if (notificationsBrokerAsyncRpcState_Subscribe == null)
			{
				notificationsBrokerAsyncRpcState_Subscribe = new NotificationsBrokerAsyncRpcState_Subscribe();
			}
			return notificationsBrokerAsyncRpcState_Subscribe;
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0003211C File Offset: 0x0003151C
		public virtual int PoolSize()
		{
			return 100;
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00033154 File Offset: 0x00032554
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
					this.InternalBegin(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Subscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.asyncCallback);
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

		// Token: 0x06000D12 RID: 3346
		public abstract void InternalBegin(CancelableAsyncCallback asyncCallback);

		// Token: 0x06000D13 RID: 3347
		public abstract int InternalEnd(ICancelableAsyncResult asyncResult);

		// Token: 0x06000D14 RID: 3348
		public abstract void InternalReset();

		// Token: 0x06000D15 RID: 3349 RVA: 0x00033B04 File Offset: 0x00032F04
		// Note: this type is marked as 'beforefieldinit'.
		static BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Subscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>()
		{
			ArraySegment<byte> arraySegment = new ArraySegment<byte>(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Subscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.emptyByteArray);
			BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Subscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.emptyByteArraySegment = arraySegment;
		}

		// Token: 0x04000D94 RID: 3476
		private static readonly object freePoolLock = new object();

		// Token: 0x04000D95 RID: 3477
		private static readonly Stack<NotificationsBrokerAsyncRpcState_Subscribe> freePool = new Stack<NotificationsBrokerAsyncRpcState_Subscribe>();

		// Token: 0x04000D96 RID: 3478
		private static readonly CancelableAsyncCallback asyncCallback = new CancelableAsyncCallback(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Subscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.Callback);

		// Token: 0x04000D97 RID: 3479
		private static readonly byte[] emptyByteArray = new byte[0];

		// Token: 0x04000D98 RID: 3480
		private static readonly ArraySegment<byte> emptyByteArraySegment;

		// Token: 0x04000D99 RID: 3481
		private SafeRpcAsyncStateHandle asyncState;

		// Token: 0x04000D9A RID: 3482
		private NotificationsBrokerAsyncRpcServer asyncServer;

		// Token: 0x04000D9B RID: 3483
		private INotificationsBrokerAsyncDispatch asyncDispatch;
	}
}
