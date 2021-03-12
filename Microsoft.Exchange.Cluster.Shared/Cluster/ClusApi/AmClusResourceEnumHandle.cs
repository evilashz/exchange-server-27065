using System;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000044 RID: 68
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal class AmClusResourceEnumHandle : SafeDisposeTrackerHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600028E RID: 654 RVA: 0x0000C9D0 File Offset: 0x0000ABD0
		protected override bool ReleaseHandle()
		{
			int num = ClusapiMethods.ClusterResourceCloseEnum(this.handle);
			return num == 0;
		}
	}
}
