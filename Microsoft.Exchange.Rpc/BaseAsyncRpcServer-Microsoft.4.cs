using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020003A7 RID: 935
	internal abstract class BaseAsyncRpcServer<Microsoft::Exchange::Rpc::IRfriAsyncDispatch> : RpcServerBase
	{
		// Token: 0x06001071 RID: 4209 RVA: 0x0004D47C File Offset: 0x0004C87C
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

		// Token: 0x06001072 RID: 4210 RVA: 0x0004D6A8 File Offset: 0x0004CAA8
		public BaseAsyncRpcServer<Microsoft::Exchange::Rpc::IRfriAsyncDispatch>()
		{
			this.m_hAsyncIOCompletionPort = null;
			this.m_fAsyncShutdown = false;
			this.m_thread = null;
			this.m_rundownQueueLock = new object();
			this.m_rundownQueue = new Queue<IntPtr>(1000);
			this.m_rundownThreadCount = 0;
			this.m_waitCallback = new WaitCallback(this.RundownThread);
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x0004CF60 File Offset: 0x0004C360
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

		// Token: 0x06001074 RID: 4212 RVA: 0x0004D304 File Offset: 0x0004C704
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

		// Token: 0x06001075 RID: 4213 RVA: 0x0004CD30 File Offset: 0x0004C130
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

		// Token: 0x06001076 RID: 4214 RVA: 0x0004CDAC File Offset: 0x0004C1AC
		[return: MarshalAs(UnmanagedType.U1)]
		public virtual bool CanClientConnect(WindowsIdentity windowsIdentity)
		{
			return windowsIdentity != null && windowsIdentity.IsAuthenticated && !windowsIdentity.IsGuest && !windowsIdentity.IsAnonymous;
		}

		// Token: 0x06001077 RID: 4215
		public abstract void RundownContext(IntPtr contextHandle);

		// Token: 0x06001078 RID: 4216
		public abstract void DroppedConnection(IntPtr contextHandle);

		// Token: 0x06001079 RID: 4217
		public abstract IRfriAsyncDispatch GetAsyncDispatch();

		// Token: 0x0600107A RID: 4218
		public abstract void StartRundownQueue();

		// Token: 0x0600107B RID: 4219
		public abstract void StopRundownQueue();

		// Token: 0x04000F9F RID: 3999
		private unsafe void* m_hAsyncIOCompletionPort;

		// Token: 0x04000FA0 RID: 4000
		private bool m_fAsyncShutdown;

		// Token: 0x04000FA1 RID: 4001
		private Thread m_thread;

		// Token: 0x04000FA2 RID: 4002
		private static readonly int m_rundownThreadMax = 4;

		// Token: 0x04000FA3 RID: 4003
		private object m_rundownQueueLock;

		// Token: 0x04000FA4 RID: 4004
		private Queue<IntPtr> m_rundownQueue;

		// Token: 0x04000FA5 RID: 4005
		private int m_rundownThreadCount;

		// Token: 0x04000FA6 RID: 4006
		private WaitCallback m_waitCallback;
	}
}
