using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000BD RID: 189
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusterNotRunningException : ClusterException
	{
		// Token: 0x060006E4 RID: 1764 RVA: 0x0001B331 File Offset: 0x00019531
		public ClusterNotRunningException() : base(Strings.ClusterNotRunningException)
		{
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001B343 File Offset: 0x00019543
		public ClusterNotRunningException(Exception innerException) : base(Strings.ClusterNotRunningException, innerException)
		{
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001B356 File Offset: 0x00019556
		protected ClusterNotRunningException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001B360 File Offset: 0x00019560
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
