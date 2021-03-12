using System;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000046 RID: 70
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal class AmClusNetworkEnumHandle : SafeDisposeTrackerHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000292 RID: 658 RVA: 0x0000CA20 File Offset: 0x0000AC20
		protected override bool ReleaseHandle()
		{
			int num = ClusapiMethods.ClusterNetworkCloseEnum(this.handle);
			return num == 0;
		}
	}
}
