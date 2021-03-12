using System;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x0200003A RID: 58
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal class AmClusterHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000277 RID: 631 RVA: 0x0000C85E File Offset: 0x0000AA5E
		public AmClusterHandle() : base(true)
		{
			base.SetHandle(IntPtr.Zero);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000C872 File Offset: 0x0000AA72
		protected override bool ReleaseHandle()
		{
			return ClusapiMethods.CloseCluster(this.handle);
		}
	}
}
