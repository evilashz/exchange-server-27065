using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200092A RID: 2346
	[SecurityCritical]
	[__DynamicallyInvokable]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	public abstract class SafeHandle : CriticalFinalizerObject, IDisposable
	{
		// Token: 0x060060B9 RID: 24761 RVA: 0x0014A12F File Offset: 0x0014832F
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected SafeHandle(IntPtr invalidHandleValue, bool ownsHandle)
		{
			this.handle = invalidHandleValue;
			this._state = 4;
			this._ownsHandle = ownsHandle;
			if (!ownsHandle)
			{
				GC.SuppressFinalize(this);
			}
			this._fullyInitialized = true;
		}

		// Token: 0x060060BA RID: 24762 RVA: 0x0014A15C File Offset: 0x0014835C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		~SafeHandle()
		{
			this.Dispose(false);
		}

		// Token: 0x060060BB RID: 24763
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalFinalize();

		// Token: 0x060060BC RID: 24764 RVA: 0x0014A18C File Offset: 0x0014838C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected void SetHandle(IntPtr handle)
		{
			this.handle = handle;
		}

		// Token: 0x060060BD RID: 24765 RVA: 0x0014A195 File Offset: 0x00148395
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public IntPtr DangerousGetHandle()
		{
			return this.handle;
		}

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x060060BE RID: 24766 RVA: 0x0014A19D File Offset: 0x0014839D
		[__DynamicallyInvokable]
		public bool IsClosed
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				return (this._state & 1) == 1;
			}
		}

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x060060BF RID: 24767
		[__DynamicallyInvokable]
		public abstract bool IsInvalid { [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] [__DynamicallyInvokable] get; }

		// Token: 0x060060C0 RID: 24768 RVA: 0x0014A1AA File Offset: 0x001483AA
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x060060C1 RID: 24769 RVA: 0x0014A1B3 File Offset: 0x001483B3
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060060C2 RID: 24770 RVA: 0x0014A1BC File Offset: 0x001483BC
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.InternalDispose();
				return;
			}
			this.InternalFinalize();
		}

		// Token: 0x060060C3 RID: 24771
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalDispose();

		// Token: 0x060060C4 RID: 24772
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetHandleAsInvalid();

		// Token: 0x060060C5 RID: 24773
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		protected abstract bool ReleaseHandle();

		// Token: 0x060060C6 RID: 24774
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DangerousAddRef(ref bool success);

		// Token: 0x060060C7 RID: 24775
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DangerousRelease();

		// Token: 0x04002AC4 RID: 10948
		protected IntPtr handle;

		// Token: 0x04002AC5 RID: 10949
		private int _state;

		// Token: 0x04002AC6 RID: 10950
		private bool _ownsHandle;

		// Token: 0x04002AC7 RID: 10951
		private bool _fullyInitialized;
	}
}
