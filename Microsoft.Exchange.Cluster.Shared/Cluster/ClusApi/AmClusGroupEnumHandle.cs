using System;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000045 RID: 69
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal class AmClusGroupEnumHandle : SafeDisposeTrackerHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000290 RID: 656 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
		protected override bool ReleaseHandle()
		{
			int num = ClusapiMethods.ClusterGroupCloseEnum(this.handle);
			return num == 0;
		}
	}
}
