using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000918 RID: 2328
	[SecurityCritical]
	[__DynamicallyInvokable]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	public abstract class CriticalHandle : CriticalFinalizerObject, IDisposable
	{
		// Token: 0x06005F50 RID: 24400 RVA: 0x001475E6 File Offset: 0x001457E6
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected CriticalHandle(IntPtr invalidHandleValue)
		{
			this.handle = invalidHandleValue;
			this._isClosed = false;
		}

		// Token: 0x06005F51 RID: 24401 RVA: 0x001475FC File Offset: 0x001457FC
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		~CriticalHandle()
		{
			this.Dispose(false);
		}

		// Token: 0x06005F52 RID: 24402 RVA: 0x0014762C File Offset: 0x0014582C
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private void Cleanup()
		{
			if (this.IsClosed)
			{
				return;
			}
			this._isClosed = true;
			if (this.IsInvalid)
			{
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!this.ReleaseHandle())
			{
				this.FireCustomerDebugProbe();
			}
			Marshal.SetLastWin32Error(lastWin32Error);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005F53 RID: 24403
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void FireCustomerDebugProbe();

		// Token: 0x06005F54 RID: 24404 RVA: 0x00147672 File Offset: 0x00145872
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected void SetHandle(IntPtr handle)
		{
			this.handle = handle;
		}

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x06005F55 RID: 24405 RVA: 0x0014767B File Offset: 0x0014587B
		[__DynamicallyInvokable]
		public bool IsClosed
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				return this._isClosed;
			}
		}

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x06005F56 RID: 24406
		[__DynamicallyInvokable]
		public abstract bool IsInvalid { [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] [__DynamicallyInvokable] get; }

		// Token: 0x06005F57 RID: 24407 RVA: 0x00147683 File Offset: 0x00145883
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06005F58 RID: 24408 RVA: 0x0014768C File Offset: 0x0014588C
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06005F59 RID: 24409 RVA: 0x00147695 File Offset: 0x00145895
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			this.Cleanup();
		}

		// Token: 0x06005F5A RID: 24410 RVA: 0x0014769D File Offset: 0x0014589D
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public void SetHandleAsInvalid()
		{
			this._isClosed = true;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005F5B RID: 24411
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		protected abstract bool ReleaseHandle();

		// Token: 0x04002A83 RID: 10883
		protected IntPtr handle;

		// Token: 0x04002A84 RID: 10884
		private bool _isClosed;
	}
}
