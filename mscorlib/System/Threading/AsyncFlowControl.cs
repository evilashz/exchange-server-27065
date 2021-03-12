using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004CA RID: 1226
	public struct AsyncFlowControl : IDisposable
	{
		// Token: 0x06003ACD RID: 15053 RVA: 0x000DE8A0 File Offset: 0x000DCAA0
		[SecurityCritical]
		internal void Setup(SecurityContextDisableFlow flags)
		{
			this.useEC = false;
			Thread currentThread = Thread.CurrentThread;
			this._sc = currentThread.GetMutableExecutionContext().SecurityContext;
			this._sc._disableFlow = flags;
			this._thread = currentThread;
		}

		// Token: 0x06003ACE RID: 15054 RVA: 0x000DE8E0 File Offset: 0x000DCAE0
		[SecurityCritical]
		internal void Setup()
		{
			this.useEC = true;
			Thread currentThread = Thread.CurrentThread;
			this._ec = currentThread.GetMutableExecutionContext();
			this._ec.isFlowSuppressed = true;
			this._thread = currentThread;
		}

		// Token: 0x06003ACF RID: 15055 RVA: 0x000DE919 File Offset: 0x000DCB19
		public void Dispose()
		{
			this.Undo();
		}

		// Token: 0x06003AD0 RID: 15056 RVA: 0x000DE924 File Offset: 0x000DCB24
		[SecuritySafeCritical]
		public void Undo()
		{
			if (this._thread == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotUseAFCMultiple"));
			}
			if (this._thread != Thread.CurrentThread)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotUseAFCOtherThread"));
			}
			if (this.useEC)
			{
				if (Thread.CurrentThread.GetMutableExecutionContext() != this._ec)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AsyncFlowCtrlCtxMismatch"));
				}
				ExecutionContext.RestoreFlow();
			}
			else
			{
				if (!Thread.CurrentThread.GetExecutionContextReader().SecurityContext.IsSame(this._sc))
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AsyncFlowCtrlCtxMismatch"));
				}
				SecurityContext.RestoreFlow();
			}
			this._thread = null;
		}

		// Token: 0x06003AD1 RID: 15057 RVA: 0x000DE9D5 File Offset: 0x000DCBD5
		public override int GetHashCode()
		{
			if (this._thread != null)
			{
				return this._thread.GetHashCode();
			}
			return this.ToString().GetHashCode();
		}

		// Token: 0x06003AD2 RID: 15058 RVA: 0x000DE9FC File Offset: 0x000DCBFC
		public override bool Equals(object obj)
		{
			return obj is AsyncFlowControl && this.Equals((AsyncFlowControl)obj);
		}

		// Token: 0x06003AD3 RID: 15059 RVA: 0x000DEA14 File Offset: 0x000DCC14
		public bool Equals(AsyncFlowControl obj)
		{
			return obj.useEC == this.useEC && obj._ec == this._ec && obj._sc == this._sc && obj._thread == this._thread;
		}

		// Token: 0x06003AD4 RID: 15060 RVA: 0x000DEA50 File Offset: 0x000DCC50
		public static bool operator ==(AsyncFlowControl a, AsyncFlowControl b)
		{
			return a.Equals(b);
		}

		// Token: 0x06003AD5 RID: 15061 RVA: 0x000DEA5A File Offset: 0x000DCC5A
		public static bool operator !=(AsyncFlowControl a, AsyncFlowControl b)
		{
			return !(a == b);
		}

		// Token: 0x040018CF RID: 6351
		private bool useEC;

		// Token: 0x040018D0 RID: 6352
		private ExecutionContext _ec;

		// Token: 0x040018D1 RID: 6353
		private SecurityContext _sc;

		// Token: 0x040018D2 RID: 6354
		private Thread _thread;
	}
}
