using System;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x0200003D RID: 61
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal class AmClusterNodeHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600027D RID: 637 RVA: 0x0000C8A9 File Offset: 0x0000AAA9
		public AmClusterNodeHandle() : base(true)
		{
			base.SetHandle(IntPtr.Zero);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000C8BD File Offset: 0x0000AABD
		protected override bool ReleaseHandle()
		{
			return ClusapiMethods.CloseClusterNode(this.handle);
		}
	}
}
