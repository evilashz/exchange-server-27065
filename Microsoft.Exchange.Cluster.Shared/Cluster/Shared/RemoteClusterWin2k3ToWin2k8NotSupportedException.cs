using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000CC RID: 204
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RemoteClusterWin2k3ToWin2k8NotSupportedException : ClusCommonFailException
	{
		// Token: 0x06000730 RID: 1840 RVA: 0x0001BB26 File Offset: 0x00019D26
		public RemoteClusterWin2k3ToWin2k8NotSupportedException() : base(Strings.RemoteClusterWin2k3ToWin2k8NotSupportedException)
		{
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001BB38 File Offset: 0x00019D38
		public RemoteClusterWin2k3ToWin2k8NotSupportedException(Exception innerException) : base(Strings.RemoteClusterWin2k3ToWin2k8NotSupportedException, innerException)
		{
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001BB4B File Offset: 0x00019D4B
		protected RemoteClusterWin2k3ToWin2k8NotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001BB55 File Offset: 0x00019D55
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
