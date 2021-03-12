using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Exchange.Net;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020001E4 RID: 484
	public class ClientAsyncResult : ICancelableAsyncResult
	{
		// Token: 0x06000A52 RID: 2642 RVA: 0x0001B91C File Offset: 0x0001AD1C
		public ClientAsyncResult(CancelableAsyncCallback asyncCallback, object asyncState)
		{
			this.m_completedEventLock = new object();
			this.m_completedEvent = null;
			this.m_isCompleted = false;
			this.m_isCanceled = false;
			this.m_asyncCallback = asyncCallback;
			this.m_asyncState = asyncState;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0001C71C File Offset: 0x0001BB1C
		public void Completion()
		{
			@lock @lock = null;
			if (!this.m_isCompleted)
			{
				@lock lock2 = new @lock(this.m_completedEventLock);
				try
				{
					@lock = lock2;
					this.m_isCompleted = true;
				}
				catch
				{
					((IDisposable)@lock).Dispose();
					throw;
				}
				((IDisposable)@lock).Dispose();
				ManualResetEvent completedEvent = this.m_completedEvent;
				if (completedEvent != null)
				{
					completedEvent.Set();
				}
				if (this.m_asyncCallback != null)
				{
					this.m_asyncCallback(this);
				}
			}
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0001C7A0 File Offset: 0x0001BBA0
		public void WaitForCompletion()
		{
			@lock @lock = null;
			@lock lock2 = null;
			if (!this.m_isCompleted)
			{
				ManualResetEvent manualResetEvent = null;
				if (this.m_completedEvent == null)
				{
					manualResetEvent = new ManualResetEvent(false);
				}
				try
				{
					@lock lock3 = new @lock(this.m_completedEventLock);
					try
					{
						@lock = lock3;
						if (!this.m_isCompleted)
						{
							goto IL_4C;
						}
					}
					catch
					{
						((IDisposable)@lock).Dispose();
						throw;
					}
					((IDisposable)@lock).Dispose();
					return;
					IL_4C:
					try
					{
						if (this.m_completedEvent == null)
						{
							this.m_completedEvent = manualResetEvent;
							manualResetEvent = null;
						}
					}
					catch
					{
						((IDisposable)@lock).Dispose();
						throw;
					}
					((IDisposable)@lock).Dispose();
					this.m_completedEvent.WaitOne();
				}
				finally
				{
					@lock lock4 = new @lock(this.m_completedEventLock);
					ManualResetEvent completedEvent;
					try
					{
						lock2 = lock4;
						completedEvent = this.m_completedEvent;
						this.m_completedEvent = null;
					}
					catch
					{
						((IDisposable)lock2).Dispose();
						goto EndFinally_19;
						throw;
					}
					((IDisposable)lock2).Dispose();
					if (completedEvent != null)
					{
						((IDisposable)completedEvent).Dispose();
					}
					if (manualResetEvent != null)
					{
						((IDisposable)manualResetEvent).Dispose();
					}
					EndFinally_19:;
				}
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0001B960 File Offset: 0x0001AD60
		public virtual object AsyncState
		{
			get
			{
				return this.m_asyncState;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x0001C8D0 File Offset: 0x0001BCD0
		public virtual WaitHandle AsyncWaitHandle
		{
			get
			{
				@lock @lock = null;
				if (this.m_completedEvent == null)
				{
					@lock lock2 = new @lock(this.m_completedEventLock);
					try
					{
						@lock = lock2;
						if (this.m_completedEvent == null)
						{
							this.m_completedEvent = new ManualResetEvent(this.m_isCompleted);
						}
					}
					catch
					{
						((IDisposable)@lock).Dispose();
						throw;
					}
					((IDisposable)@lock).Dispose();
				}
				return this.m_completedEvent;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x0001B974 File Offset: 0x0001AD74
		public virtual bool CompletedSynchronously
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return false;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0001B984 File Offset: 0x0001AD84
		public virtual bool IsCompleted
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_isCompleted;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0001B998 File Offset: 0x0001AD98
		public virtual bool IsCanceled
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_isCanceled;
			}
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0001C944 File Offset: 0x0001BD44
		public virtual void Cancel()
		{
			@lock @lock = null;
			if (!this.m_isCanceled)
			{
				@lock lock2 = new @lock(this.m_completedEventLock);
				try
				{
					@lock = lock2;
					if (!this.m_isCanceled)
					{
						goto IL_33;
					}
				}
				catch
				{
					((IDisposable)@lock).Dispose();
					throw;
				}
				((IDisposable)@lock).Dispose();
				return;
				IL_33:
				try
				{
					this.m_isCanceled = true;
				}
				catch
				{
					((IDisposable)@lock).Dispose();
					throw;
				}
				((IDisposable)@lock).Dispose();
				if (!this.IsCompleted)
				{
					this.InternalCancel();
				}
			}
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0001B9AC File Offset: 0x0001ADAC
		protected virtual void InternalCancel()
		{
		}

		// Token: 0x04000BA3 RID: 2979
		private CancelableAsyncCallback m_asyncCallback;

		// Token: 0x04000BA4 RID: 2980
		private object m_asyncState;

		// Token: 0x04000BA5 RID: 2981
		private object m_completedEventLock;

		// Token: 0x04000BA6 RID: 2982
		private ManualResetEvent m_completedEvent;

		// Token: 0x04000BA7 RID: 2983
		private bool m_isCompleted;

		// Token: 0x04000BA8 RID: 2984
		private bool m_isCanceled;
	}
}
