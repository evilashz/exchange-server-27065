using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004E0 RID: 1248
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MonitoringCouldNotFindDatabasesException : MonitoringADConfigException
	{
		// Token: 0x06002E4C RID: 11852 RVA: 0x000C326D File Offset: 0x000C146D
		public MonitoringCouldNotFindDatabasesException(string serverName, string adError) : base(ReplayStrings.MonitoringCouldNotFindDatabasesException(serverName, adError))
		{
			this.serverName = serverName;
			this.adError = adError;
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x000C328F File Offset: 0x000C148F
		public MonitoringCouldNotFindDatabasesException(string serverName, string adError, Exception innerException) : base(ReplayStrings.MonitoringCouldNotFindDatabasesException(serverName, adError), innerException)
		{
			this.serverName = serverName;
			this.adError = adError;
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x000C32B4 File Offset: 0x000C14B4
		protected MonitoringCouldNotFindDatabasesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.adError = (string)info.GetValue("adError", typeof(string));
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x000C3309 File Offset: 0x000C1509
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("adError", this.adError);
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06002E50 RID: 11856 RVA: 0x000C3335 File Offset: 0x000C1535
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06002E51 RID: 11857 RVA: 0x000C333D File Offset: 0x000C153D
		public string AdError
		{
			get
			{
				return this.adError;
			}
		}

		// Token: 0x04001573 RID: 5491
		private readonly string serverName;

		// Token: 0x04001574 RID: 5492
		private readonly string adError;
	}
}
