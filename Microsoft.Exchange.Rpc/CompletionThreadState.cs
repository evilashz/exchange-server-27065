using System;
using System.Collections.Generic;
using System.Threading;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020001E5 RID: 485
	public class CompletionThreadState
	{
		// Token: 0x06000A5C RID: 2652 RVA: 0x0001DAD0 File Offset: 0x0001CED0
		private unsafe void StartupThread()
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
				this.m_fShutdown = false;
				this.m_shutdownCompletionEvent.Reset();
				Thread thread = new Thread(new ThreadStart(this.AsyncCallCompletionThread));
				this.m_thread = thread;
				if (thread.Name == null)
				{
					this.m_thread.Name = "AsyncCallCompletionThread";
				}
				this.m_thread.IsBackground = true;
				this.m_thread.Start();
				AppDomain.CurrentDomain.DomainUnload += this.AppDomainUnload;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.ShutdownThread();
				}
			}
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0001B9BC File Offset: 0x0001ADBC
		private unsafe void ShutdownThread()
		{
			if (this.m_thread != null)
			{
				this.m_fShutdown = true;
				if (Thread.CurrentThread.ManagedThreadId != this.m_thread.ManagedThreadId)
				{
					<Module>.PostQueuedCompletionStatus(this.m_hAsyncIOCompletionPort, 0, 0UL, null);
					this.m_shutdownCompletionEvent.WaitOne();
				}
				this.m_thread = null;
			}
			ManualResetEvent shutdownCompletionEvent = this.m_shutdownCompletionEvent;
			if (shutdownCompletionEvent != null)
			{
				((IDisposable)shutdownCompletionEvent).Dispose();
				this.m_shutdownCompletionEvent = null;
			}
			void* hAsyncIOCompletionPort = this.m_hAsyncIOCompletionPort;
			if (hAsyncIOCompletionPort != null)
			{
				<Module>.CloseHandle(hAsyncIOCompletionPort);
				this.m_hAsyncIOCompletionPort = null;
			}
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0001BA44 File Offset: 0x0001AE44
		private void AppDomainUnload(object sender, EventArgs e)
		{
			this.ShutdownThread();
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0001C9E0 File Offset: 0x0001BDE0
		private static void AsyncCompletionRoutine(object @object)
		{
			((ClientAsyncResult)@object).Completion();
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0001DBA4 File Offset: 0x0001CFA4
		public CompletionThreadState()
		{
			this.m_lock = new object();
			this.m_hAsyncIOCompletionPort = null;
			this.m_thread = null;
			this.m_callDictionary = new Dictionary<IntPtr, ClientAsyncResult>();
			this.m_shutdownCompletionEvent = new ManualResetEvent(false);
			this.m_fShutdown = false;
			this.StartupThread();
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0001BA58 File Offset: 0x0001AE58
		public unsafe void* GetIoCompletionPort()
		{
			return this.m_hAsyncIOCompletionPort;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0001C9F8 File Offset: 0x0001BDF8
		public unsafe void AsyncCallCompletionThread()
		{
			ClientAsyncResult clientAsyncResult = null;
			@lock @lock = null;
			try
			{
				while (!this.m_fShutdown)
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
						if (this.m_fShutdown)
						{
							break;
						}
						if (ptr != null)
						{
							IntPtr key = new IntPtr((void*)ptr);
							clientAsyncResult = null;
							@lock lock2 = new @lock(this.m_lock);
							bool flag;
							try
							{
								@lock = lock2;
								flag = this.m_callDictionary.TryGetValue(key, out clientAsyncResult);
							}
							catch
							{
								((IDisposable)@lock).Dispose();
								throw;
							}
							((IDisposable)@lock).Dispose();
							if (flag && clientAsyncResult != null && !ThreadPool.QueueUserWorkItem(new WaitCallback(CompletionThreadState.AsyncCompletionRoutine), clientAsyncResult))
							{
								clientAsyncResult.Completion();
							}
						}
					}
				}
			}
			finally
			{
				ManualResetEvent shutdownCompletionEvent = this.m_shutdownCompletionEvent;
				if (shutdownCompletionEvent != null)
				{
					shutdownCompletionEvent.Set();
				}
			}
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0001CAFC File Offset: 0x0001BEFC
		public IntPtr CreateAsyncCallHandle(ClientAsyncResult clientAsyncResult)
		{
			@lock @lock = null;
			@lock lock2 = new @lock(this.m_lock);
			IntPtr intPtr;
			try
			{
				@lock = lock2;
				intPtr = new IntPtr(Interlocked.Increment(ref CompletionThreadState.m_nextAsyncCallHandle));
				this.m_callDictionary.Add(intPtr, clientAsyncResult);
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			return intPtr;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0001CB64 File Offset: 0x0001BF64
		public void DestroyAsyncCallHandle(IntPtr asyncCallHandle)
		{
			@lock @lock = null;
			@lock lock2 = new @lock(this.m_lock);
			try
			{
				@lock = lock2;
				this.m_callDictionary.Remove(asyncCallHandle);
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
		}

		// Token: 0x04000BA9 RID: 2985
		private static int m_nextAsyncCallHandle = 1;

		// Token: 0x04000BAA RID: 2986
		private object m_lock;

		// Token: 0x04000BAB RID: 2987
		private unsafe void* m_hAsyncIOCompletionPort;

		// Token: 0x04000BAC RID: 2988
		private Thread m_thread;

		// Token: 0x04000BAD RID: 2989
		private Dictionary<IntPtr, ClientAsyncResult> m_callDictionary;

		// Token: 0x04000BAE RID: 2990
		private ManualResetEvent m_shutdownCompletionEvent;

		// Token: 0x04000BAF RID: 2991
		private bool m_fShutdown;
	}
}
