using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004DE RID: 1246
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MonitoringCouldNotFindMiniServerException : MonitoringADConfigException
	{
		// Token: 0x06002E41 RID: 11841 RVA: 0x000C3112 File Offset: 0x000C1312
		public MonitoringCouldNotFindMiniServerException(string serverName) : base(ReplayStrings.MonitoringCouldNotFindMiniServerException(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x000C312C File Offset: 0x000C132C
		public MonitoringCouldNotFindMiniServerException(string serverName, Exception innerException) : base(ReplayStrings.MonitoringCouldNotFindMiniServerException(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x000C3147 File Offset: 0x000C1347
		protected MonitoringCouldNotFindMiniServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x000C3171 File Offset: 0x000C1371
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06002E45 RID: 11845 RVA: 0x000C318C File Offset: 0x000C138C
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04001570 RID: 5488
		private readonly string serverName;
	}
}
