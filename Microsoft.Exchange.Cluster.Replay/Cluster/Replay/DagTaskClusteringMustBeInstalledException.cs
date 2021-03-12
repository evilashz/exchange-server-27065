using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003DC RID: 988
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskClusteringMustBeInstalledException : DagTaskServerException
	{
		// Token: 0x060028BA RID: 10426 RVA: 0x000B8766 File Offset: 0x000B6966
		public DagTaskClusteringMustBeInstalledException(string serverName) : base(ReplayStrings.DagTaskClusteringMustBeInstalledException(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x000B8780 File Offset: 0x000B6980
		public DagTaskClusteringMustBeInstalledException(string serverName, Exception innerException) : base(ReplayStrings.DagTaskClusteringMustBeInstalledException(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x000B879B File Offset: 0x000B699B
		protected DagTaskClusteringMustBeInstalledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x000B87C5 File Offset: 0x000B69C5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x060028BE RID: 10430 RVA: 0x000B87E0 File Offset: 0x000B69E0
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x040013F1 RID: 5105
		private readonly string serverName;
	}
}
