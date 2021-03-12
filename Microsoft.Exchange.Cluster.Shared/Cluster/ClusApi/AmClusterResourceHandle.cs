using System;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x0200003C RID: 60
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal class AmClusterResourceHandle : SafeDisposeTrackerHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600027B RID: 635 RVA: 0x0000C894 File Offset: 0x0000AA94
		protected override bool ReleaseHandle()
		{
			return ClusapiMethods.CloseClusterResource(this.handle);
		}
	}
}
