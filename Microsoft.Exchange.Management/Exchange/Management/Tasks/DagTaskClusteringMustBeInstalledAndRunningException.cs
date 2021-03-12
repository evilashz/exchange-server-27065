using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200103E RID: 4158
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskClusteringMustBeInstalledAndRunningException : LocalizedException
	{
		// Token: 0x0600AFF0 RID: 45040 RVA: 0x00295235 File Offset: 0x00293435
		public DagTaskClusteringMustBeInstalledAndRunningException(string serverName) : base(Strings.DagTaskClusteringMustBeInstalledAndRunningException(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600AFF1 RID: 45041 RVA: 0x0029524A File Offset: 0x0029344A
		public DagTaskClusteringMustBeInstalledAndRunningException(string serverName, Exception innerException) : base(Strings.DagTaskClusteringMustBeInstalledAndRunningException(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600AFF2 RID: 45042 RVA: 0x00295260 File Offset: 0x00293460
		protected DagTaskClusteringMustBeInstalledAndRunningException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600AFF3 RID: 45043 RVA: 0x0029528A File Offset: 0x0029348A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003815 RID: 14357
		// (get) Token: 0x0600AFF4 RID: 45044 RVA: 0x002952A5 File Offset: 0x002934A5
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x0400617B RID: 24955
		private readonly string serverName;
	}
}
