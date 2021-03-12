using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004DF RID: 1247
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MonitoringCouldNotFindDagException : MonitoringADConfigException
	{
		// Token: 0x06002E46 RID: 11846 RVA: 0x000C3194 File Offset: 0x000C1394
		public MonitoringCouldNotFindDagException(string dagName, string adError) : base(ReplayStrings.MonitoringCouldNotFindDagException(dagName, adError))
		{
			this.dagName = dagName;
			this.adError = adError;
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x000C31B6 File Offset: 0x000C13B6
		public MonitoringCouldNotFindDagException(string dagName, string adError, Exception innerException) : base(ReplayStrings.MonitoringCouldNotFindDagException(dagName, adError), innerException)
		{
			this.dagName = dagName;
			this.adError = adError;
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x000C31DC File Offset: 0x000C13DC
		protected MonitoringCouldNotFindDagException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dagName = (string)info.GetValue("dagName", typeof(string));
			this.adError = (string)info.GetValue("adError", typeof(string));
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x000C3231 File Offset: 0x000C1431
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dagName", this.dagName);
			info.AddValue("adError", this.adError);
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06002E4A RID: 11850 RVA: 0x000C325D File Offset: 0x000C145D
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06002E4B RID: 11851 RVA: 0x000C3265 File Offset: 0x000C1465
		public string AdError
		{
			get
			{
				return this.adError;
			}
		}

		// Token: 0x04001571 RID: 5489
		private readonly string dagName;

		// Token: 0x04001572 RID: 5490
		private readonly string adError;
	}
}
