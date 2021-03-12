using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200051F RID: 1311
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DumpsterCouldNotFindHubServerException : DumpsterRedeliveryException
	{
		// Token: 0x06002FB2 RID: 12210 RVA: 0x000C603B File Offset: 0x000C423B
		public DumpsterCouldNotFindHubServerException(string hubServerName) : base(ReplayStrings.DumpsterCouldNotFindHubServerException(hubServerName))
		{
			this.hubServerName = hubServerName;
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x000C6055 File Offset: 0x000C4255
		public DumpsterCouldNotFindHubServerException(string hubServerName, Exception innerException) : base(ReplayStrings.DumpsterCouldNotFindHubServerException(hubServerName), innerException)
		{
			this.hubServerName = hubServerName;
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000C6070 File Offset: 0x000C4270
		protected DumpsterCouldNotFindHubServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.hubServerName = (string)info.GetValue("hubServerName", typeof(string));
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000C609A File Offset: 0x000C429A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("hubServerName", this.hubServerName);
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06002FB6 RID: 12214 RVA: 0x000C60B5 File Offset: 0x000C42B5
		public string HubServerName
		{
			get
			{
				return this.hubServerName;
			}
		}

		// Token: 0x040015DD RID: 5597
		private readonly string hubServerName;
	}
}
