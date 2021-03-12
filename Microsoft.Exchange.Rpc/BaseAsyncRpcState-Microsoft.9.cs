using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc.NotificationsBroker;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020002E3 RID: 739
	internal abstract class BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_GetNextNotification,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>
	{
		// Token: 0x06000D34 RID: 3380 RVA: 0x00033AE4 File Offset: 0x00032EE4
		private static void Callback(ICancelableAsyncResult asyncResult)
		{
			((BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_GetNextNotification,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>)asyncResult.AsyncState).InternalCallback(asyncResult);
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00032220 File Offset: 0x00031620
		protected BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_GetNextNotification,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>()
		{
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x00032274 File Offset: 0x00031674
		protected SafeRpcAsyncStateHandle AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x00032288 File Offset: 0x00031688
		protected NotificationsBrokerAsyncRpcServer AsyncServer
		{
			get
			{
				return this.asyncServer;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x0003229C File Offset: 0x0003169C
		protected INotificationsBrokerAsyncDispatch AsyncDispatch
		{
			get
			{
				return this.asyncDispatch;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x000322B0 File Offset: 0x000316B0
		protected ArraySegment<byte> EmptyByteArraySegment
		{
			get
			{
				return BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_GetNextNotification,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.emptyByteArraySegment;
			}
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00032248 File Offset: 0x00031648
		protected void InternalInitialize(SafeRpcAsyncStateHandle asyncState, NotificationsBrokerAsyncRpcServer asyncServer)
		{
			this.asyncState = asyncState;
			this.asyncServer = asyncServer;
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0003383C File Offset: 0x00032C3C
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

		// Token: 0x06000D3C RID: 3388 RVA: 0x00032E60 File Offset: 0x00032260
		protected void ReleaseToPool()
		{
			@lock @lock = null;
			this.InternalReset();
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
			NotificationsBrokerAsyncRpcState_GetNextNotification item = (NotificationsBrokerAsyncRpcState_GetNextNotification)this;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_GetNextNotification,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_GetNextNotification,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.freePool.Count < this.PoolSize())
				{
					BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_GetNextNotification,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.freePool.Push(item);
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00032BC4 File Offset: 0x00031FC4
		public static NotificationsBrokerAsyncRpcState_GetNextNotification CreateFromPool()
		{
			@lock @lock = null;
			NotificationsBrokerAsyncRpcState_GetNextNotification notificationsBrokerAsyncRpcState_GetNextNotification = null;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_GetNextNotification,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_GetNextNotification,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.freePool.Count > 0)
				{
					notificationsBrokerAsyncRpcState_GetNextNotification = BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_GetNextNotification,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.freePool.Pop();
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			if (notificationsBrokerAsyncRpcState_GetNextNotification == null)
			{
				notificationsBrokerAsyncRpcState_GetNextNotification = new NotificationsBrokerAsyncRpcState_GetNextNotification();
			}
			return notificationsBrokerAsyncRpcState_GetNextNotification;
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00032264 File Offset: 0x00031664
		public virtual int PoolSize()
		{
			return 100;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00033444 File Offset: 0x00032844
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
					this.InternalBegin(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_GetNextNotification,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.asyncCallback);
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

		// Token: 0x06000D40 RID: 3392
		public abstract void InternalBegin(CancelableAsyncCallback asyncCallback);

		// Token: 0x06000D41 RID: 3393
		public abstract int InternalEnd(ICancelableAsyncResult asyncResult);

		// Token: 0x06000D42 RID: 3394
		public abstract void InternalReset();

		// Token: 0x06000D43 RID: 3395 RVA: 0x00033BA4 File Offset: 0x00032FA4
		// Note: this type is marked as 'beforefieldinit'.
		static BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_GetNextNotification,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>()
		{
			ArraySegment<byte> arraySegment = new ArraySegment<byte>(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_GetNextNotification,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.emptyByteArray);
			BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_GetNextNotification,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.emptyByteArraySegment = arraySegment;
		}

		// Token: 0x04000DAA RID: 3498
		private static readonly object freePoolLock = new object();

		// Token: 0x04000DAB RID: 3499
		private static readonly Stack<NotificationsBrokerAsyncRpcState_GetNextNotification> freePool = new Stack<NotificationsBrokerAsyncRpcState_GetNextNotification>();

		// Token: 0x04000DAC RID: 3500
		private static readonly CancelableAsyncCallback asyncCallback = new CancelableAsyncCallback(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_GetNextNotification,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>.Callback);

		// Token: 0x04000DAD RID: 3501
		private static readonly byte[] emptyByteArray = new byte[0];

		// Token: 0x04000DAE RID: 3502
		private static readonly ArraySegment<byte> emptyByteArraySegment;

		// Token: 0x04000DAF RID: 3503
		private SafeRpcAsyncStateHandle asyncState;

		// Token: 0x04000DB0 RID: 3504
		private NotificationsBrokerAsyncRpcServer asyncServer;

		// Token: 0x04000DB1 RID: 3505
		private INotificationsBrokerAsyncDispatch asyncDispatch;
	}
}
