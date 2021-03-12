using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003BD RID: 957
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusterNetworkNullSubnetException : TransientException
	{
		// Token: 0x0600280A RID: 10250 RVA: 0x000B7121 File Offset: 0x000B5321
		public ClusterNetworkNullSubnetException(string clusterNetworkName) : base(ReplayStrings.ClusterNetworkNullSubnetError(clusterNetworkName))
		{
			this.clusterNetworkName = clusterNetworkName;
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x000B7136 File Offset: 0x000B5336
		public ClusterNetworkNullSubnetException(string clusterNetworkName, Exception innerException) : base(ReplayStrings.ClusterNetworkNullSubnetError(clusterNetworkName), innerException)
		{
			this.clusterNetworkName = clusterNetworkName;
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x000B714C File Offset: 0x000B534C
		protected ClusterNetworkNullSubnetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.clusterNetworkName = (string)info.GetValue("clusterNetworkName", typeof(string));
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x000B7176 File Offset: 0x000B5376
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("clusterNetworkName", this.clusterNetworkName);
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x0600280E RID: 10254 RVA: 0x000B7191 File Offset: 0x000B5391
		public string ClusterNetworkName
		{
			get
			{
				return this.clusterNetworkName;
			}
		}

		// Token: 0x040013BD RID: 5053
		private readonly string clusterNetworkName;
	}
}
