using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004CA RID: 1226
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RepairStateLocalServerIsNotInDagException : RepairStateException
	{
		// Token: 0x06002DD0 RID: 11728 RVA: 0x000C2321 File Offset: 0x000C0521
		public RepairStateLocalServerIsNotInDagException(string serverName) : base(ReplayStrings.RepairStateLocalServerIsNotInDag(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x000C233B File Offset: 0x000C053B
		public RepairStateLocalServerIsNotInDagException(string serverName, Exception innerException) : base(ReplayStrings.RepairStateLocalServerIsNotInDag(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x000C2356 File Offset: 0x000C0556
		protected RepairStateLocalServerIsNotInDagException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x000C2380 File Offset: 0x000C0580
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06002DD4 RID: 11732 RVA: 0x000C239B File Offset: 0x000C059B
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x0400154F RID: 5455
		private readonly string serverName;
	}
}
