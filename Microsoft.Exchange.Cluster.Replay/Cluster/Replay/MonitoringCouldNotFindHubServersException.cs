using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004E1 RID: 1249
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MonitoringCouldNotFindHubServersException : MonitoringADConfigException
	{
		// Token: 0x06002E52 RID: 11858 RVA: 0x000C3345 File Offset: 0x000C1545
		public MonitoringCouldNotFindHubServersException(string siteName, string adError) : base(ReplayStrings.MonitoringCouldNotFindHubServersException(siteName, adError))
		{
			this.siteName = siteName;
			this.adError = adError;
		}

		// Token: 0x06002E53 RID: 11859 RVA: 0x000C3367 File Offset: 0x000C1567
		public MonitoringCouldNotFindHubServersException(string siteName, string adError, Exception innerException) : base(ReplayStrings.MonitoringCouldNotFindHubServersException(siteName, adError), innerException)
		{
			this.siteName = siteName;
			this.adError = adError;
		}

		// Token: 0x06002E54 RID: 11860 RVA: 0x000C338C File Offset: 0x000C158C
		protected MonitoringCouldNotFindHubServersException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.siteName = (string)info.GetValue("siteName", typeof(string));
			this.adError = (string)info.GetValue("adError", typeof(string));
		}

		// Token: 0x06002E55 RID: 11861 RVA: 0x000C33E1 File Offset: 0x000C15E1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("siteName", this.siteName);
			info.AddValue("adError", this.adError);
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06002E56 RID: 11862 RVA: 0x000C340D File Offset: 0x000C160D
		public string SiteName
		{
			get
			{
				return this.siteName;
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06002E57 RID: 11863 RVA: 0x000C3415 File Offset: 0x000C1615
		public string AdError
		{
			get
			{
				return this.adError;
			}
		}

		// Token: 0x04001575 RID: 5493
		private readonly string siteName;

		// Token: 0x04001576 RID: 5494
		private readonly string adError;
	}
}
