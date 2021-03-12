using System;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000041 RID: 65
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal class AmClusterBatchHandle : SafeDisposeTrackerHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0000C930 File Offset: 0x0000AB30
		internal int CommitAndClose()
		{
			int num;
			int result = ClusapiMethods.ClusterRegCloseBatch(this.handle, true, out num);
			base.SetHandle(IntPtr.Zero);
			return result;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000C958 File Offset: 0x0000AB58
		protected override bool ReleaseHandle()
		{
			int num;
			return ClusapiMethods.ClusterRegCloseBatch(this.handle, false, out num) == 0;
		}
	}
}
