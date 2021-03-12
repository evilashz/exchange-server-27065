using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x02000029 RID: 41
	[ComVisible(false)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	internal sealed class ResourceManagerHandle : SafeHandle
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00005FA6 File Offset: 0x000041A6
		internal ResourceManagerHandle() : base(IntPtr.Zero, true)
		{
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00005FB4 File Offset: 0x000041B4
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005FC6 File Offset: 0x000041C6
		protected override bool ReleaseHandle()
		{
			return NativeMethods.AuthzFreeResourceManager(this.handle);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005FD4 File Offset: 0x000041D4
		public static ResourceManagerHandle Create(string name)
		{
			ResourceManagerHandle result;
			if (!NativeMethods.AuthzInitializeResourceManager(ResourceManagerFlags.NoAudit, name, out result))
			{
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
			return result;
		}
	}
}
