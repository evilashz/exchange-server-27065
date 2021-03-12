using System;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000043 RID: 67
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal class AmClusNodeEnumHandle : SafeDisposeTrackerHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600028C RID: 652 RVA: 0x0000C9A8 File Offset: 0x0000ABA8
		protected override bool ReleaseHandle()
		{
			int num = ClusapiMethods.ClusterNodeCloseEnum(this.handle);
			return num == 0;
		}
	}
}
