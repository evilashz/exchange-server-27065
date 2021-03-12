using System;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000042 RID: 66
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal class AmClusEnumHandle : SafeDisposeTrackerHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600028A RID: 650 RVA: 0x0000C980 File Offset: 0x0000AB80
		protected override bool ReleaseHandle()
		{
			int num = ClusapiMethods.ClusterCloseEnum(this.handle);
			return num == 0;
		}
	}
}
