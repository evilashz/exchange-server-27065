using System;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002A4 RID: 676
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusterIPNotFoundPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060022F0 RID: 8944 RVA: 0x0004DA8F File Offset: 0x0004BC8F
		public ClusterIPNotFoundPermanentException(IPAddress clusterIp) : base(MrsStrings.ClusterIPNotFound(clusterIp))
		{
			this.clusterIp = clusterIp;
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x0004DAA4 File Offset: 0x0004BCA4
		public ClusterIPNotFoundPermanentException(IPAddress clusterIp, Exception innerException) : base(MrsStrings.ClusterIPNotFound(clusterIp), innerException)
		{
			this.clusterIp = clusterIp;
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x0004DABA File Offset: 0x0004BCBA
		protected ClusterIPNotFoundPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.clusterIp = (IPAddress)info.GetValue("clusterIp", typeof(IPAddress));
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x0004DAE4 File Offset: 0x0004BCE4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("clusterIp", this.clusterIp);
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x060022F4 RID: 8948 RVA: 0x0004DAFF File Offset: 0x0004BCFF
		public IPAddress ClusterIp
		{
			get
			{
				return this.clusterIp;
			}
		}

		// Token: 0x04000FA9 RID: 4009
		private readonly IPAddress clusterIp;
	}
}
