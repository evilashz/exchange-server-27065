using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000015 RID: 21
	internal abstract class RpcAsyncResult : IRpcAsyncResult, IDisposable
	{
		// Token: 0x06000616 RID: 1558 RVA: 0x00001834 File Offset: 0x00000C34
		protected RpcAsyncResult(AsyncCallback callback, object asyncState)
		{
			this.m_callback = callback;
			this.m_asyncState = asyncState;
			this.m_completedEvent = new ManualResetEvent(false);
			this.m_ptpWait = null;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0000186C File Offset: 0x00000C6C
		private unsafe void ~RpcAsyncResult()
		{
			_TP_WAIT* ptpWait = this.m_ptpWait;
			if (ptpWait != null)
			{
				<Module>.SetThreadpoolWait(ptpWait, null, null);
				<Module>.CloseThreadpoolWait(this.m_ptpWait);
			}
			ManualResetEvent completedEvent = this.m_completedEvent;
			if (completedEvent != null)
			{
				((IDisposable)completedEvent).Dispose();
				this.m_completedEvent = null;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x000018B0 File Offset: 0x00000CB0
		public virtual bool IsCompleted
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_completedEvent.WaitOne(0);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x000018CC File Offset: 0x00000CCC
		public virtual WaitHandle AsyncWaitHandle
		{
			get
			{
				return this.m_completedEvent;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x000018E0 File Offset: 0x00000CE0
		public virtual object AsyncState
		{
			get
			{
				return this.m_asyncState;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x000018F4 File Offset: 0x00000CF4
		public virtual bool CompletedSynchronously
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return false;
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00001904 File Offset: 0x00000D04
		public virtual void Cancel()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00051068 File Offset: 0x00050468
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe bool RegisterWait(IntPtr rootedAsyncState)
		{
			if (!(this.m_callback != null))
			{
				return false;
			}
			_TP_WAIT* ptr = <Module>.CreateThreadpoolWait(<Module>.__unep@?OnRpcCompleteCallback@@$$FYAXPEAU_TP_CALLBACK_INSTANCE@@PEAXPEAU_TP_WAIT@@K@Z, rootedAsyncState.ToPointer(), null);
			this.m_ptpWait = ptr;
			if (ptr == null)
			{
				string message = "CreateThreadpoolWait failed";
				throw <Module>.GetRpcException(<Module>.GetLastError(), message);
			}
			IntPtr handle = this.m_completedEvent.Handle;
			<Module>.SetThreadpoolWait(this.m_ptpWait, handle.ToPointer(), null);
			return true;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00001918 File Offset: 0x00000D18
		public void OnRpcComplete()
		{
			if (this.m_callback != null)
			{
				this.m_callback(this);
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00001940 File Offset: 0x00000D40
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.~RpcAsyncResult();
			}
			else
			{
				base.Finalize();
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00001A14 File Offset: 0x00000E14
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000836 RID: 2102
		private object m_asyncState;

		// Token: 0x04000837 RID: 2103
		private ManualResetEvent m_completedEvent;

		// Token: 0x04000838 RID: 2104
		private unsafe _TP_WAIT* m_ptpWait;

		// Token: 0x04000839 RID: 2105
		protected AsyncCallback m_callback;
	}
}
