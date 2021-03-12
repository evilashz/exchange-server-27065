using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000524 RID: 1316
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmClusterException : AmCommonException
	{
		// Token: 0x06002FD0 RID: 12240 RVA: 0x000C646B File Offset: 0x000C466B
		public AmClusterException(string clusterError) : base(ReplayStrings.AmClusterException(clusterError))
		{
			this.clusterError = clusterError;
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x000C6485 File Offset: 0x000C4685
		public AmClusterException(string clusterError, Exception innerException) : base(ReplayStrings.AmClusterException(clusterError), innerException)
		{
			this.clusterError = clusterError;
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x000C64A0 File Offset: 0x000C46A0
		protected AmClusterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.clusterError = (string)info.GetValue("clusterError", typeof(string));
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x000C64CA File Offset: 0x000C46CA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("clusterError", this.clusterError);
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06002FD4 RID: 12244 RVA: 0x000C64E5 File Offset: 0x000C46E5
		public string ClusterError
		{
			get
			{
				return this.clusterError;
			}
		}

		// Token: 0x040015E7 RID: 5607
		private readonly string clusterError;
	}
}
