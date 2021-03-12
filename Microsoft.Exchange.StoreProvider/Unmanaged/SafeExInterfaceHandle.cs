using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002B0 RID: 688
	[ClassAccessLevel(AccessLevel.Implementation)]
	[ComVisible(false)]
	internal class SafeExInterfaceHandle : DisposeTrackableSafeHandleZeroOrMinusOneIsInvalid, IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000C82 RID: 3202 RVA: 0x00033FBC File Offset: 0x000321BC
		protected SafeExInterfaceHandle()
		{
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00033FC4 File Offset: 0x000321C4
		internal SafeExInterfaceHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00033FCD File Offset: 0x000321CD
		internal SafeExInterfaceHandle(SafeExInterfaceHandle innerHandle) : base(innerHandle.DangerousGetHandle(), false)
		{
			this.innerHandle = innerHandle;
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00033FE3 File Offset: 0x000321E3
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExInterfaceHandle>(this);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00033FEC File Offset: 0x000321EC
		public int QueryInterface(Guid riid, out IExInterface iObj)
		{
			SafeExInterfaceHandle safeExInterfaceHandle = null;
			iObj = null;
			int result;
			try
			{
				int num = SafeExInterfaceHandle.IUnknown_QueryInterface(this.handle, riid, out safeExInterfaceHandle);
				if (num == 0)
				{
					iObj = safeExInterfaceHandle;
					safeExInterfaceHandle = null;
				}
				result = num;
			}
			finally
			{
				safeExInterfaceHandle.DisposeIfValid();
			}
			return result;
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00034034 File Offset: 0x00032234
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.innerHandle != null)
				{
					this.innerHandle.Dispose();
					this.innerHandle = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00034078 File Offset: 0x00032278
		protected virtual void InternalReleaseHandle()
		{
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0003407A File Offset: 0x0003227A
		protected override bool ReleaseHandle()
		{
			this.InternalReleaseHandle();
			SafeExInterfaceHandle.IUnknown_Release(this.handle);
			return true;
		}

		// Token: 0x06000C8A RID: 3210
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IUnknown_Release(IntPtr iUnknown);

		// Token: 0x06000C8B RID: 3211
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IUnknown_QueryInterface(IntPtr iUnknown, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, out SafeExInterfaceHandle iObj);

		// Token: 0x040011E2 RID: 4578
		private SafeExInterfaceHandle innerHandle;
	}
}
