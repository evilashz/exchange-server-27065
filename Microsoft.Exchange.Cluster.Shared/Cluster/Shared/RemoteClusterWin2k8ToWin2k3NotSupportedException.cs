using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000CD RID: 205
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RemoteClusterWin2k8ToWin2k3NotSupportedException : ClusCommonFailException
	{
		// Token: 0x06000734 RID: 1844 RVA: 0x0001BB5F File Offset: 0x00019D5F
		public RemoteClusterWin2k8ToWin2k3NotSupportedException() : base(Strings.RemoteClusterWin2k8ToWin2k3NotSupportedException)
		{
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001BB71 File Offset: 0x00019D71
		public RemoteClusterWin2k8ToWin2k3NotSupportedException(Exception innerException) : base(Strings.RemoteClusterWin2k8ToWin2k3NotSupportedException, innerException)
		{
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001BB84 File Offset: 0x00019D84
		protected RemoteClusterWin2k8ToWin2k3NotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001BB8E File Offset: 0x00019D8E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
