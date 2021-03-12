using System;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000040 RID: 64
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal class AmClusterRegkeyHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000C8F4 File Offset: 0x0000AAF4
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0000C8FC File Offset: 0x0000AAFC
		internal string Name { get; set; }

		// Token: 0x06000285 RID: 645 RVA: 0x0000C905 File Offset: 0x0000AB05
		public AmClusterRegkeyHandle() : base(true)
		{
			base.SetHandle(IntPtr.Zero);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000C919 File Offset: 0x0000AB19
		protected override bool ReleaseHandle()
		{
			return ClusapiMethods.ClusterRegCloseKey(this.handle, this.Name) == 0;
		}
	}
}
