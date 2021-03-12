using System;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x0200003B RID: 59
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal class AmClusterGroupHandle : SafeDisposeTrackerHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000279 RID: 633 RVA: 0x0000C87F File Offset: 0x0000AA7F
		protected override bool ReleaseHandle()
		{
			return ClusapiMethods.CloseClusterGroup(this.handle);
		}
	}
}
