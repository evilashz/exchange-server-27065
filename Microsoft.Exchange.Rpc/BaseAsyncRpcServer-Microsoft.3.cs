using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020002F9 RID: 761
	internal abstract class BaseAsyncRpcServer<Microsoft::Exchange::Rpc::INspiAsyncDispatch> : RpcServerBase
	{
		// Token: 0x06000DA1 RID: 3489 RVA: 0x0003A5F4 File Offset: 0x000399F4
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

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0003BAA8 File Offset: 0x0003AEA8
		public BaseAsyncRpcServer<Microsoft::Exchange::Rpc::INspiAsyncDispatch>()
		{
			this.m_hAsyncIOCompletionPort = null;
			this.m_fAsyncShutdown = false;
			this.m_thread = null;
			this.m_rundownQueueLock = new object();
			this.m_rundownQueue = new Queue<IntPtr>(1000);
			this.m_rundownThreadCount = 0;
			this.m_waitCallback = new WaitCallback(this.RundownThread);
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0003803C File Offset: 0x0003743C
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

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00039BAC File Offset: 0x00038FAC
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

		// Token: 0x06000DA5 RID: 3493 RVA: 0x000371E0 File Offset: 0x000365E0
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

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00039C44 File Offset: 0x00039044
		public void ContextHandleRundown(IntPtr contextHandle)
		{
			@lock @lock = null;
			@lock lock2 = new @lock(this.m_rundownQueueLock);
			try
			{
				@lock = lock2;
				this.m_rundownQueue.Enqueue(contextHandle);
				if (this.m_rundownThreadCount < BaseAsyncRpcServer<Microsoft::Exchange::Rpc::INspiAsyncDispatch>.m_rundownThreadMax)
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

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0003725C File Offset: 0x0003665C
		[return: MarshalAs(UnmanagedType.U1)]
		public virtual bool CanClientConnect(WindowsIdentity windowsIdentity)
		{
			return windowsIdentity != null && windowsIdentity.IsAuthenticated && !windowsIdentity.IsGuest && !windowsIdentity.IsAnonymous;
		}

		// Token: 0x06000DA8 RID: 3496
		public abstract void RundownContext(IntPtr contextHandle);

		// Token: 0x06000DA9 RID: 3497
		public abstract void DroppedConnection(IntPtr contextHandle);

		// Token: 0x06000DAA RID: 3498
		public abstract INspiAsyncDispatch GetAsyncDispatch();

		// Token: 0x06000DAB RID: 3499
		public abstract void StartRundownQueue();

		// Token: 0x06000DAC RID: 3500
		public abstract void StopRundownQueue();

		// Token: 0x04000DF3 RID: 3571
		private unsafe void* m_hAsyncIOCompletionPort;

		// Token: 0x04000DF4 RID: 3572
		private bool m_fAsyncShutdown;

		// Token: 0x04000DF5 RID: 3573
		private Thread m_thread;

		// Token: 0x04000DF6 RID: 3574
		private static readonly int m_rundownThreadMax = 4;

		// Token: 0x04000DF7 RID: 3575
		private object m_rundownQueueLock;

		// Token: 0x04000DF8 RID: 3576
		private Queue<IntPtr> m_rundownQueue;

		// Token: 0x04000DF9 RID: 3577
		private int m_rundownThreadCount;

		// Token: 0x04000DFA RID: 3578
		private WaitCallback m_waitCallback;
	}
}
