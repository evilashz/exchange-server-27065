using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020001F8 RID: 504
	internal abstract class BaseAsyncRpcServer<Microsoft::Exchange::Rpc::IExchangeAsyncDispatch> : RpcServerBase
	{
		// Token: 0x06000AA6 RID: 2726 RVA: 0x00020544 File Offset: 0x0001F944
		private void RundownThread(object state)
		{
			@lock @lock = null;
			@lock lock2 = null;
			bool flag = false;
			try
			{
				for (;;)
				{
					IntPtr intPtr = 0;
					@lock lock3 = new @lock(this.m_rundownQueueLock);
					try
					{
						@lock = lock3;
						if (this.m_rundownQueue.Count != 0)
						{
							goto IL_4E;
						}
						this.m_rundownThreadCount--;
						flag = true;
					}
					catch
					{
						((IDisposable)@lock).Dispose();
						throw;
					}
					break;
					IL_4E:
					IntPtr contextHandle;
					try
					{
						contextHandle = this.m_rundownQueue.Dequeue();
					}
					catch
					{
						((IDisposable)@lock).Dispose();
						throw;
					}
					((IDisposable)@lock).Dispose();
					this.RundownContext(contextHandle);
				}
				((IDisposable)@lock).Dispose();
			}
			finally
			{
				if (!flag)
				{
					@lock lock4 = new @lock(this.m_rundownQueueLock);
					try
					{
						lock2 = lock4;
						this.m_rundownThreadCount--;
					}
					catch
					{
						((IDisposable)lock2).Dispose();
						goto EndFinally_14;
						throw;
					}
					((IDisposable)lock2).Dispose();
				}
				EndFinally_14:;
			}
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00020990 File Offset: 0x0001FD90
		public BaseAsyncRpcServer<Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>()
		{
			this.m_hAsyncIOCompletionPort = null;
			this.m_fAsyncShutdown = false;
			this.m_thread = null;
			this.m_rundownQueueLock = new object();
			this.m_rundownQueue = new Queue<IntPtr>(1000);
			this.m_rundownThreadCount = 0;
			this.m_waitCallback = new WaitCallback(this.RundownThread);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0001EF10 File Offset: 0x0001E310
		public unsafe void RegisterConnectionDroppedNotification(SafeRpcAsyncStateHandle asyncState, IntPtr contextHandle)
		{
			_RPC_ASYNC_NOTIFICATION_INFO hAsyncIOCompletionPort;
			initblk(ref hAsyncIOCompletionPort + 8, 0, 24L);
			hAsyncIOCompletionPort = this.m_hAsyncIOCompletionPort;
			*(ref hAsyncIOCompletionPort + 24) = contextHandle.ToPointer();
			<Module>.RpcServerSubscribeForNotification(*(long*)((byte*)asyncState.DangerousGetHandle().ToPointer() + 32L), (_RPC_NOTIFICATIONS)1, (_RPC_NOTIFICATION_TYPES)3, &hAsyncIOCompletionPort);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0001EF5C File Offset: 0x0001E35C
		public unsafe void UnregisterConnectionDroppedNotification(SafeRpcAsyncStateHandle asyncState)
		{
			uint num = 0;
			<Module>.RpcServerUnsubscribeForNotification(*(long*)((byte*)asyncState.DangerousGetHandle().ToPointer() + 32L), (_RPC_NOTIFICATIONS)1, &num);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0001F488 File Offset: 0x0001E888
		public unsafe void DroppedConnectionThread()
		{
			if (!this.m_fAsyncShutdown)
			{
				do
				{
					uint num = 0;
					ulong num2 = 0UL;
					_OVERLAPPED* ptr = null;
					if (<Module>.GetQueuedCompletionStatus(this.m_hAsyncIOCompletionPort, &num, &num2, &ptr, -1) == null)
					{
						<Module>.Sleep(500);
					}
					else
					{
						if (this.m_fAsyncShutdown)
						{
							break;
						}
						if (ptr != null)
						{
							IntPtr contextHandle = new IntPtr((void*)ptr);
							this.DroppedConnection(contextHandle);
						}
					}
				}
				while (!this.m_fAsyncShutdown);
			}
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0002018C File Offset: 0x0001F58C
		public unsafe virtual void StartDroppedConnectionNotificationThread()
		{
			bool flag = false;
			try
			{
				void* ptr = <Module>.CreateIoCompletionPort(-1L, null, 0UL, 1);
				this.m_hAsyncIOCompletionPort = ptr;
				if (ptr == null)
				{
					throw new OutOfMemoryException();
				}
				Thread thread = new Thread(new ThreadStart(this.DroppedConnectionThread));
				this.m_thread = thread;
				thread.Name = "DroppedConnectionNotificationThread";
				this.m_thread.IsBackground = true;
				this.m_thread.Start();
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.StopDroppedConnectionNotificationThread();
				}
			}
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0001EF88 File Offset: 0x0001E388
		public unsafe virtual void StopDroppedConnectionNotificationThread()
		{
			Thread thread = this.m_thread;
			if (thread != null)
			{
				this.m_fAsyncShutdown = true;
				if (thread.IsAlive)
				{
					<Module>.PostQueuedCompletionStatus(this.m_hAsyncIOCompletionPort, 0, 0UL, null);
					if (this.m_thread.IsAlive)
					{
						do
						{
							<Module>.Sleep(500);
						}
						while (this.m_thread.IsAlive);
					}
				}
				this.m_thread = null;
			}
			void* hAsyncIOCompletionPort = this.m_hAsyncIOCompletionPort;
			if (hAsyncIOCompletionPort != null)
			{
				<Module>.CloseHandle(hAsyncIOCompletionPort);
				this.m_hAsyncIOCompletionPort = null;
			}
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00020224 File Offset: 0x0001F624
		public void ContextHandleRundown(IntPtr contextHandle)
		{
			@lock @lock = null;
			@lock lock2 = new @lock(this.m_rundownQueueLock);
			try
			{
				@lock = lock2;
				this.m_rundownQueue.Enqueue(contextHandle);
				if (this.m_rundownThreadCount < BaseAsyncRpcServer<Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>.m_rundownThreadMax)
				{
					if (ThreadPool.QueueUserWorkItem(this.m_waitCallback))
					{
						this.m_rundownThreadCount++;
					}
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0001F004 File Offset: 0x0001E404
		[return: MarshalAs(UnmanagedType.U1)]
		public virtual bool CanClientConnect(WindowsIdentity windowsIdentity)
		{
			return windowsIdentity != null && windowsIdentity.IsAuthenticated && !windowsIdentity.IsGuest && !windowsIdentity.IsAnonymous;
		}

		// Token: 0x06000AAF RID: 2735
		public abstract void RundownContext(IntPtr contextHandle);

		// Token: 0x06000AB0 RID: 2736
		public abstract void DroppedConnection(IntPtr contextHandle);

		// Token: 0x06000AB1 RID: 2737
		public abstract IExchangeAsyncDispatch GetAsyncDispatch();

		// Token: 0x06000AB2 RID: 2738
		public abstract void StartRundownQueue();

		// Token: 0x06000AB3 RID: 2739
		public abstract void StopRundownQueue();

		// Token: 0x04000BE8 RID: 3048
		private unsafe void* m_hAsyncIOCompletionPort;

		// Token: 0x04000BE9 RID: 3049
		private bool m_fAsyncShutdown;

		// Token: 0x04000BEA RID: 3050
		private Thread m_thread;

		// Token: 0x04000BEB RID: 3051
		private static readonly int m_rundownThreadMax = 4;

		// Token: 0x04000BEC RID: 3052
		private object m_rundownQueueLock;

		// Token: 0x04000BED RID: 3053
		private Queue<IntPtr> m_rundownQueue;

		// Token: 0x04000BEE RID: 3054
		private int m_rundownThreadCount;

		// Token: 0x04000BEF RID: 3055
		private WaitCallback m_waitCallback;
	}
}
