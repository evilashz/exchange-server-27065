using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020002DB RID: 731
	internal abstract class BaseAsyncRpcServer<Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch> : RpcServerBase
	{
		// Token: 0x06000CF4 RID: 3316 RVA: 0x00032C34 File Offset: 0x00032034
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

		// Token: 0x06000CF5 RID: 3317 RVA: 0x000330F4 File Offset: 0x000324F4
		public BaseAsyncRpcServer<Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>()
		{
			this.m_hAsyncIOCompletionPort = null;
			this.m_fAsyncShutdown = false;
			this.m_thread = null;
			this.m_rundownQueueLock = new object();
			this.m_rundownQueue = new Queue<IntPtr>(1000);
			this.m_rundownThreadCount = 0;
			this.m_waitCallback = new WaitCallback(this.RundownThread);
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x000322C4 File Offset: 0x000316C4
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

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00032A28 File Offset: 0x00031E28
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

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00031FF0 File Offset: 0x000313F0
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

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0003206C File Offset: 0x0003146C
		[return: MarshalAs(UnmanagedType.U1)]
		public virtual bool CanClientConnect(WindowsIdentity windowsIdentity)
		{
			return windowsIdentity != null && windowsIdentity.IsAuthenticated && !windowsIdentity.IsGuest && !windowsIdentity.IsAnonymous;
		}

		// Token: 0x06000CFA RID: 3322
		public abstract void RundownContext(IntPtr contextHandle);

		// Token: 0x06000CFB RID: 3323
		public abstract void DroppedConnection(IntPtr contextHandle);

		// Token: 0x06000CFC RID: 3324
		public abstract INotificationsBrokerAsyncDispatch GetAsyncDispatch();

		// Token: 0x06000CFD RID: 3325
		public abstract void StartRundownQueue();

		// Token: 0x06000CFE RID: 3326
		public abstract void StopRundownQueue();

		// Token: 0x04000D8B RID: 3467
		private unsafe void* m_hAsyncIOCompletionPort;

		// Token: 0x04000D8C RID: 3468
		private bool m_fAsyncShutdown;

		// Token: 0x04000D8D RID: 3469
		private Thread m_thread;

		// Token: 0x04000D8E RID: 3470
		private static readonly int m_rundownThreadMax = 4;

		// Token: 0x04000D8F RID: 3471
		private object m_rundownQueueLock;

		// Token: 0x04000D90 RID: 3472
		private Queue<IntPtr> m_rundownQueue;

		// Token: 0x04000D91 RID: 3473
		private int m_rundownThreadCount;

		// Token: 0x04000D92 RID: 3474
		private WaitCallback m_waitCallback;
	}
}
