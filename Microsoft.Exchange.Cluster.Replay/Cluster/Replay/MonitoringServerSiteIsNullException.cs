using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004E2 RID: 1250
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MonitoringServerSiteIsNullException : MonitoringADConfigException
	{
		// Token: 0x06002E58 RID: 11864 RVA: 0x000C341D File Offset: 0x000C161D
		public MonitoringServerSiteIsNullException(string serverName) : base(ReplayStrings.MonitoringServerSiteIsNullException(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x06002E59 RID: 11865 RVA: 0x000C3437 File Offset: 0x000C1637
		public MonitoringServerSiteIsNullException(string serverName, Exception innerException) : base(ReplayStrings.MonitoringServerSiteIsNullException(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x06002E5A RID: 11866 RVA: 0x000C3452 File Offset: 0x000C1652
		protected MonitoringServerSiteIsNullException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x06002E5B RID: 11867 RVA: 0x000C347C File Offset: 0x000C167C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06002E5C RID: 11868 RVA: 0x000C3497 File Offset: 0x000C1697
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04001577 RID: 5495
		private readonly string serverName;
	}
}
