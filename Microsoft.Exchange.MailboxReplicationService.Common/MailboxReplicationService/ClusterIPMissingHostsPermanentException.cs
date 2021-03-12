using System;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002A6 RID: 678
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusterIPMissingHostsPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060022F9 RID: 8953 RVA: 0x0004DB36 File Offset: 0x0004BD36
		public ClusterIPMissingHostsPermanentException(IPAddress clusterIp) : base(MrsStrings.ClusterIPMissingHosts(clusterIp))
		{
			this.clusterIp = clusterIp;
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x0004DB4B File Offset: 0x0004BD4B
		public ClusterIPMissingHostsPermanentException(IPAddress clusterIp, Exception innerException) : base(MrsStrings.ClusterIPMissingHosts(clusterIp), innerException)
		{
			this.clusterIp = clusterIp;
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x0004DB61 File Offset: 0x0004BD61
		protected ClusterIPMissingHostsPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.clusterIp = (IPAddress)info.GetValue("clusterIp", typeof(IPAddress));
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x0004DB8B File Offset: 0x0004BD8B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("clusterIp", this.clusterIp);
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x060022FD RID: 8957 RVA: 0x0004DBA6 File Offset: 0x0004BDA6
		public IPAddress ClusterIp
		{
			get
			{
				return this.clusterIp;
			}
		}

		// Token: 0x04000FAA RID: 4010
		private readonly IPAddress clusterIp;
	}
}
