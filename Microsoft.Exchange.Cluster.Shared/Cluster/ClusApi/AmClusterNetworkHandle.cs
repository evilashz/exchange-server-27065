using System;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x0200003E RID: 62
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal class AmClusterNetworkHandle : SafeDisposeTrackerHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600027F RID: 639 RVA: 0x0000C8CA File Offset: 0x0000AACA
		protected override bool ReleaseHandle()
		{
			return ClusapiMethods.CloseClusterNetwork(this.handle);
		}
	}
}
