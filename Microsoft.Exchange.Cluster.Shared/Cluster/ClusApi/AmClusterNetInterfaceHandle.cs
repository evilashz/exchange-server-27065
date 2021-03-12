using System;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x0200003F RID: 63
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal class AmClusterNetInterfaceHandle : SafeDisposeTrackerHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000281 RID: 641 RVA: 0x0000C8DF File Offset: 0x0000AADF
		protected override bool ReleaseHandle()
		{
			return ClusapiMethods.CloseClusterNetInterface(this.handle);
		}
	}
}
