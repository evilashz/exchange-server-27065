using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008B9 RID: 2233
	[ComVisible(false)]
	internal struct DependentHandle
	{
		// Token: 0x06005CDB RID: 23771 RVA: 0x001459E0 File Offset: 0x00143BE0
		[SecurityCritical]
		public DependentHandle(object primary, object secondary)
		{
			IntPtr handle = (IntPtr)0;
			DependentHandle.nInitialize(primary, secondary, out handle);
			this._handle = handle;
		}

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x06005CDC RID: 23772 RVA: 0x00145A04 File Offset: 0x00143C04
		public bool IsAllocated
		{
			get
			{
				return this._handle != (IntPtr)0;
			}
		}

		// Token: 0x06005CDD RID: 23773 RVA: 0x00145A18 File Offset: 0x00143C18
		[SecurityCritical]
		public object GetPrimary()
		{
			object result;
			DependentHandle.nGetPrimary(this._handle, out result);
			return result;
		}

		// Token: 0x06005CDE RID: 23774 RVA: 0x00145A33 File Offset: 0x00143C33
		[SecurityCritical]
		public void GetPrimaryAndSecondary(out object primary, out object secondary)
		{
			DependentHandle.nGetPrimaryAndSecondary(this._handle, out primary, out secondary);
		}

		// Token: 0x06005CDF RID: 23775 RVA: 0x00145A44 File Offset: 0x00143C44
		[SecurityCritical]
		public void Free()
		{
			if (this._handle != (IntPtr)0)
			{
				IntPtr handle = this._handle;
				this._handle = (IntPtr)0;
				DependentHandle.nFree(handle);
			}
		}

		// Token: 0x06005CE0 RID: 23776
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void nInitialize(object primary, object secondary, out IntPtr dependentHandle);

		// Token: 0x06005CE1 RID: 23777
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void nGetPrimary(IntPtr dependentHandle, out object primary);

		// Token: 0x06005CE2 RID: 23778
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void nGetPrimaryAndSecondary(IntPtr dependentHandle, out object primary, out object secondary);

		// Token: 0x06005CE3 RID: 23779
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void nFree(IntPtr dependentHandle);

		// Token: 0x04002989 RID: 10633
		private IntPtr _handle;
	}
}
