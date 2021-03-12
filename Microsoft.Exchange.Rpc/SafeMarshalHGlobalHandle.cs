using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x0200026F RID: 623
	public class SafeMarshalHGlobalHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000BAD RID: 2989 RVA: 0x00055E50 File Offset: 0x00055250
		public SafeMarshalHGlobalHandle(IntPtr handle) : base(true)
		{
			try
			{
				base.SetHandle(handle);
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00055E3C File Offset: 0x0005523C
		public SafeMarshalHGlobalHandle() : base(true)
		{
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00055E94 File Offset: 0x00055294
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		protected override bool ReleaseHandle()
		{
			Marshal.FreeHGlobal(this.handle);
			return true;
		}
	}
}
