using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000039 RID: 57
	public class EventWaitHandleEx : DisposableBase
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x0000C64F File Offset: 0x0000A84F
		public EventWaitHandleEx(bool manual, bool initialState)
		{
			if (manual)
			{
				this.waitHandle = new ManualResetEvent(initialState);
				return;
			}
			this.waitHandle = new AutoResetEvent(initialState);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000C674 File Offset: 0x0000A874
		public bool WaitOne(TimeSpan duration)
		{
			bool result;
			try
			{
				result = this.waitHandle.WaitOne(duration);
			}
			catch (ObjectDisposedException exception)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(exception);
				result = true;
			}
			return result;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000C6B4 File Offset: 0x0000A8B4
		public bool Set()
		{
			bool result;
			try
			{
				result = this.waitHandle.Set();
			}
			catch (ObjectDisposedException exception)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(exception);
				result = false;
			}
			return result;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000C6F0 File Offset: 0x0000A8F0
		public bool Reset()
		{
			bool result;
			try
			{
				result = this.waitHandle.Reset();
			}
			catch (ObjectDisposedException exception)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(exception);
				result = false;
			}
			return result;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000C72C File Offset: 0x0000A92C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<EventWaitHandleEx>(this);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000C734 File Offset: 0x0000A934
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.waitHandle != null)
			{
				this.waitHandle.Dispose();
			}
		}

		// Token: 0x040004D0 RID: 1232
		private EventWaitHandle waitHandle;
	}
}
